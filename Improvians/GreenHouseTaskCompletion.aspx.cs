using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class GreenHouseTaskCompletion : System.Web.UI.Page
    {
        string wo;
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["GTAID"] != null)
                {
                    gtaID = Request.QueryString["GTAID"].ToString();
                    BindGridCalView(Request.QueryString["GTAID"].ToString());
                }

                if (Request.QueryString["Chid"] != "0" && Request.QueryString["Chid"] != null)
                {
                    BindGridCropHealth(Convert.ToInt32(Request.QueryString["Chid"]));
                }
                txtInspectionDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                BindGridGerm();

            }
        }

        public void BindGridCalView(string  GTAID)
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@GTAID", GTAID.ToString());
            dt1 = objCommon.GetDataTable("SP_GetGerminationTaskCompletionView", nv1);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                PanelViewGJob.Visible = true;
                userinput.Visible = false;
                GridViewGDetails.DataSource = dt1;
                GridViewGDetails.DataBind();

             
            }
        }


        public void BindGridCropHealth(int Chid)
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Chid", Chid.ToString());
            dt1 = objCommon.GetDataTable("SP_GetCropHealthReportSelect", nv1);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                PanelCropHealth.Visible = true;
                gvCropHealth.DataSource = dt1;
                gvCropHealth.DataBind();

                //lblCommment.Text = "Commment   :"+ dt1.Rows[0]["CropHealthCommit"].ToString();
            }
        }

        private string gtaID
        {
            get
            {
                if (ViewState["gtaID"] != null)
                {
                    return (string)ViewState["gtaID"];
                }
                return "";
            }
            set
            {
                ViewState["gtaID"] = value;
            }
        }

        private string Jid
        {
            get
            {
                if (ViewState["Jid"] != null)
                {
                    return (string)ViewState["Jid"];
                }
                return "";
            }
            set
            {
                ViewState["Jid"] = value;
            }
        }

        public void BindGridGerm()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@GTAID", gtaID);
            nv.Add("@RoleId", Session["Role"].ToString());
            
            dt = objCommon.GetDataTable("SP_GetGreenHouseOperatorGerminationTaskByGTAIDNew", nv);


            gvGerm.DataSource = dt;
            gvGerm.DataBind();

            if (dt != null && dt.Rows.Count > 0)
            {
                lblwoid.Text = dt.Rows[0]["wo"].ToString();
                lblJobid.Text = dt.Rows[0]["jobcode"].ToString();
                lblSeedlot.Text = dt.Rows[0]["TraySize"].ToString();
                Jid = dt.Rows[0]["GrowerPutAwayId"].ToString();
                //  txtTrays.Text = dt.Rows[0]["#TraysInspected"].ToString();

                //  Bindtxttray(Convert.ToInt32(dt.Rows[0]["#TraysInspected"].ToString()));
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@InspectionDate", txtInspectionDate.Text);
            nv.Add("@GTAID", gtaID);
            nv.Add("@#TraysInspected", txtTrays.Text);
            nv.Add("@Germination", lblGerm.Text);
            nv.Add("@WorkOrderID", lblwoid.Text);
            nv.Add("@#BadPlants", lblbadplants.Text);
            nv.Add("@GermVigor", lblgermvigor.Text);
            // nv.Add("@GermHealth", lblcrophealth.Text);
            // nv.Add("@JobID", Session["JobID"].ToString());
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Jid", Jid);
            result = objCommon.GetDataInsertORUpdate("SP_AddGerminationCompletion", nv);

            

            if (result > 0)
            {
                GridViewRow row = gvGerm.Rows[0];
                var txtJobNo = (row.FindControl("lbljobID") as Label).Text;
                var txtBenchLocation = (row.FindControl("lblGreenHouseID") as Label).Text;

                NameValueCollection nameValue = new NameValueCollection();
                nameValue.Add("@LoginID", Session["LoginID"].ToString());
                nameValue.Add("@jobcode", txtJobNo);
                nameValue.Add("@GreenHouseID", txtBenchLocation);
                nameValue.Add("@TaskName", "Germination");

                var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

                // lblmsg.Text = "Completion Successful";
                clear();

                string url = "";
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                if (Session["Role"].ToString() == "12")
                {
                    url = "GerminationRequestForm.aspx";
                }
                else if (Session["Role"].ToString() == "1")
                {
                    url = "GerminationRequestForm.aspx";
                }
                else if (Session["Role"].ToString() == "2")
                {
                    url = "MyTaskGreenSupervisorFinal.aspx";
                }
                else
                {
                    url = "MyTaskSpray.aspx";
                }
                string message = "Completion Successful";
              //  string url = "MyTaskGreenSupervisorFinal.aspx";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

            }
            else
            {
                lblmsg.Text = "Completion Not Successful";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            

            GridSplitJob.DataSource = null;
            GridSplitJob.DataBind();
            PanelViewDetails.Visible = false;
        }

        public void clear()
        {
            txtTrays.Text = "";
            //  txtInspectionDate.Text = "";
            lblbadplants.Text = "";
            lblGerm.Text = "";
            lblgermvigor.Text = "";
        }

        protected void txtTrays_TextChanged(object sender, EventArgs e)
        {
            PanelViewDetails.Visible = true;
            GridSplitJob.DataSource = null;
            GridSplitJob.DataBind();
            for (int P = 1; P <= Convert.ToInt32(txtTrays.Text); P++)
            {

                AddGrowerPutRow(true);

            }
        }

    

        protected void sbtTray_Click(object sender, EventArgs e)
        {
            // lblJobid.Text = Session["JobID"].ToString();
            lblnotrays.Text = txtTrays.Text;

            Table table = (Table)Page.FindControl("tbltray");
            int count = 0;
          

            foreach (GridViewRow item in GridSplitJob.Rows)
            {

                if (item.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtTrays = (item.Cells[0].FindControl("txtTrays") as TextBox);
                    count += int.Parse(txtTrays.Text);
                }
            }

            lblbadplants.Text = count.ToString();
            Decimal germ = Convert.ToDecimal(count) / (Convert.ToDecimal(lblSeedlot.Text) * Convert.ToDecimal(txtTrays.Text));
            lblGerm.Text = ((1 - germ) * 100).ToString("0.00");
            Decimal vigor = (Convert.ToInt32(txtTrays.Text) * Convert.ToDecimal(lblSeedlot.Text) * Convert.ToDecimal(lblGerm.Text));
            lblgermvigor.Text = (100 - (Convert.ToInt32(lblbadplants.Text) / vigor) * 100).ToString("0.00");
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label lblGermNo = (Label)e.Row.FindControl("lblGermNo");

                HyperLink lnkJobID = (HyperLink)e.Row.FindControl("lnkJobID");
                lnkJobID.NavigateUrl = "~/JobReports.aspx?JobCode=" + lnkJobID.Text + "&GermNo=" + lblGermNo.Text;
                //  lnkJobID.NavigateUrl(String.Format("~/CropHealthReport.aspx?Chid={0}", Chid));
            }
        }


        //-/----------------------------------------------------------------

        //private List<GrowerputDetils> GrowerPutData
        //{
        //    get
        //    {
        //        if (ViewState["GrowerPutData"] != null)
        //        {
        //            return (List<GrowerputDetilsNew>)ViewState["GrowerPutData"];
        //        }
        //        return new List<GrowerputDetilsNew>();
        //    }
        //    set
        //    {
        //        ViewState["GrowerPutData"] = value;
        //    }
        //}

        private void AddGrowerput(ref List<GrowerputDetilsNew> objGP, string Trays)

        {
            GrowerputDetilsNew objInv = new GrowerputDetilsNew();

            objInv.RowNumber = objGP.Count + 1;

            objInv.Trays = Trays;

            objGP.Add(objInv);
            ViewState["ojbpro"] = objGP;
        }


        private void AddGrowerPutRow(bool AddBlankRow)
        {
            try
            {
                string unit = "", ddlTAX1 = "", ddlStatusVal = "", hdnWOEmployeeIDVal = "";
                string MainId = "", LocationId = "";


                List<GrowerputDetilsNew> objinvoice = new List<GrowerputDetilsNew>();

                foreach (GridViewRow item in GridSplitJob.Rows)
                {

                    TextBox txtTrays = (TextBox)item.FindControl("txtTrays");

                    AddGrowerput(ref objinvoice, txtTrays.Text);

                }
                if (AddBlankRow)
                    AddGrowerput(ref objinvoice, "");

                //  GrowerPutData = objinvoice;
                GridSplitJob.DataSource = objinvoice;
                GridSplitJob.DataBind();
                ViewState["Data"] = objinvoice;


            }
            catch (Exception ex)
            {
                //  divMessage.Visible = true;
                //  divMessageSub.Attributes.Remove("class");
                // divMessageSub.Attributes.Add("class", "errormsg");
                //  lblMsg.Text = "Unable to process request. Please verify the details.<br />" + ex;
            }

        }

        public void GridSplitjob()
        {
            GridSplitJob.DataSource = ViewState["Data"];
            GridSplitJob.DataBind();

        }

        private void AddGrowerput(ref List<GrowerputDetilsNew> objGP, int ID, string Trays)
        {
            GrowerputDetilsNew objInv = new GrowerputDetilsNew();

            objInv.RowNumber = objGP.Count + 1;

            objInv.Trays = Trays;

            objGP.Add(objInv);
            ViewState["ojbpro"] = objGP;
        }

        protected void GridViewGDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                Label lblTraysInspected = (Label)e.Row.FindControl("lblTraysInspected");
             Label lblGermination = (Label)e.Row.FindControl("lblGermination");
              Label lblBadPlants = (Label)e.Row.FindControl("lblBadPlants");
              //  Label lblTaskRequestType = (Label)e.Row.FindControl("lblTaskRequestType");

                DataTable dt1 = new DataTable();
                NameValueCollection nv1 = new NameValueCollection();
                nv1.Add("@GTAID", Request.QueryString["GTAID"].ToString());
                dt1 = objCommon.GetDataTable("SP_GetGerminationTaskCompletionView", nv1);



                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    lblTraysInspected.Text = dt1.Rows[0]["#TraysInspected"].ToString();
                    lblGermination.Text = dt1.Rows[0]["Germination%"].ToString();
                    lblBadPlants.Text = dt1.Rows[0]["#BadPlants"].ToString();

                }
             

             

            }
        }
    }
}


[Serializable]
public class GrowerputDetilsNew
{
    public int ID { get; set; }
    public int RowNumber { get; set; }

    public string Trays { get; set; }
}
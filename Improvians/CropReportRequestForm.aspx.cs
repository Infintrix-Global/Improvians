using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class CropReportRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                // BindFacility();
                BindGridPlantReady(0);
                BindSupervisorList();
            }
        }

        private string JobCode
        {
            get
            {
                if (Request.QueryString["jobId"] != null)
                {
                    return Request.QueryString["jobId"].ToString();
                }
                return "";
            }
            set
            {
                // JobCode = Request.QueryString["jobId"].ToString();
                // JobCode = value;
            }
        }

        private string benchLoc
        {
            get
            {
                if (Request.QueryString["benchLoc"] != null)
                {
                    return Request.QueryString["benchLoc"].ToString();
                }
                return "";
            }
            set
            {
                // JobCode = Request.QueryString["jobId"].ToString();
                // JobCode = value;
            }
        }


        private string wo
        {
            get
            {
                if (ViewState["wo"] != null)
                {
                    return (string)ViewState["wo"];
                }
                return "";
            }
            set
            {
                ViewState["wo"] = value;
            }
        }

        public void BindGridPlantReady(int p)
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@wo", "");
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());

            // nv.Add("@Mode", "7");
            // dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);


            dt = objCommon.GetDataTable("SP_GetCropReportRequestAssistantGrower", nv);


            gvPlantReady.DataSource = dt;
            gvPlantReady.DataBind();


           

            if (p != 1)
            {
                highlight();
            }


        }
        private void highlight()
        {
            var i = gvPlantReady.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvPlantReady.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblBenchLoc") as Label).Text;
                i--;
                if (checkJob == JobCode && checklocation == benchLoc)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check)
                {
                    gvPlantReady.PageIndex++;
                    gvPlantReady.DataBind();
                    highlight();
                }
            }
        }
        public void BindSupervisorList()
        {
            //NameValueCollection nv = new NameValueCollection();
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            //ddlSupervisor.DataTextField = "EmployeeName";
            //ddlSupervisor.DataValueField = "ID";
            //ddlSupervisor.DataBind();
            //ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            NameValueCollection nv = new NameValueCollection();
            //if (Session["Role"].ToString() == "1")
            //{

            DataTable dt = new DataTable();

            if (Session["Role"].ToString() == "12" || Session["Role"].ToString() == "1")
            {

                dt = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
            }
            else if (Session["Role"].ToString() == "2")
            {
                dt = objCommon.GetDataTable("SP_GetRoleForSupervisor", nv);
            }
            else
            {
                // dt = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
            }

            ddlCropAssignment.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlCropAssignment.DataTextField = "EmployeeName";
            ddlCropAssignment.DataValueField = "ID";
            ddlCropAssignment.DataBind();
            ddlCropAssignment.Items.Insert(0, new ListItem("--Select--", "0"));
            //}
            //if (Session["Role"].ToString() == "12")
            //{
            //    ddlDumptAssignment.DataSource = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
            //    //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            //    ddlDumptAssignment.DataTextField = "EmployeeName";
            //    ddlDumptAssignment.DataValueField = "ID";
            //    ddlDumptAssignment.DataBind();
            //    ddlDumptAssignment.Items.Insert(0, new ListItem("--Select--", "0"));
            //}
        }
        public void Bindcname()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "8");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "cname";
            ddlCustomer.DataValueField = "cname";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));

        }


        public void BindJobCode()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "7");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        }

    
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();

            BindGridPlantReady(1);
        }
        protected void gvPlantReady_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                HiddenFieldDid.Value = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();
                HiddenFieldJid.Value = gvPlantReady.DataKeys[rowIndex].Values[2].ToString();


                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@CropId", HiddenFieldDid.Value);

                dt = objCommon.GetDataTable("SP_GeCropHealthReportRequestView", nv);

                if (dt != null & dt.Rows.Count > 0)
                {
                    txtCommentsDump.Text = dt.Rows[0]["Comments"].ToString();
                    txtQuantityofTray.Text = dt.Rows[0]["QuantityOfTray"].ToString();
                    txtCropDate.Text = Convert.ToDateTime(dt.Rows[0]["CropHealthReportDate"]).ToString("yyyy-MM-dd");
                }
                //ddlSupervisor.Focus();


            }

            if (e.CommandName == "StartDump")
            {
                string ChId = "";
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string Did = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();
                string BatchLoc = gvPlantReady.DataKeys[rowIndex].Values[4].ToString();
                string JobCode = gvPlantReady.DataKeys[rowIndex].Values[3].ToString();
                //  ChId = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();

                if (ChId == "")
                {
                    ChId = "0";
                }
                else
                {
                    ChId = ChId;
                }


                NameValueCollection nv = new NameValueCollection();
                nv.Add("@OperatorID", Session["LoginID"].ToString());
                nv.Add("@Comments", "");
                nv.Add("@CropId", Did);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@QuantityOfTray", "");
                nv.Add("@CropDate", "");

                long result = objCommon.GetDataExecuteScaler("SP_AddCRopeTaskStart", nv);


                if (result > 0)
                {
                    Response.Redirect(String.Format("~/CropHealthReport.aspx?BatchLoc={0}&JobCode={1}}&CropAT={1}", BatchLoc, JobCode, result));
                  //  Response.Redirect(String.Format("~/DumpTaskCompletion.aspx?Did={0}&Chid={1}&DrId={2}", result, ChId, Did));
                }
            }

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            //  nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);
            //  nv.Add("@GrowerPutAwayId", lblGrowerID.Text);
            // nv.Add("@WO",wo);

            GridViewRow row = gvPlantReady.Rows[0];

            var txtJobNo = (row.FindControl("lbljobID") as Label).Text;
            var txtBenchLocation = (row.FindControl("lblBenchLoc") as Label).Text;

            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Aid", ddlCropAssignment.SelectedValue);
            dt = objCommon.GetDataTable("spGeEmployeeRoleDetails", nv1);

            nv.Add("@SupervisorID", ddlCropAssignment.SelectedValue);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Did", HiddenFieldDid.Value);
            nv.Add("@Comments", txtCommentsDump.Text);
            nv.Add("@wo", "0");
            nv.Add("@ManualID", HiddenFieldJid.Value);
            nv.Add("@CropDate", txtCropDate.Text);
            nv.Add("@QuantityOfTray", txtQuantityofTray.Text);
            nv.Add("@RoleId", dt.Rows[0]["RoleID"].ToString());

            nv.Add("@jobcode", txtJobNo);
            nv.Add("@GreenHouseID", txtBenchLocation);



            result = objCommon.GetDataInsertORUpdate("SP_AddCropReportManua", nv);


            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);

                string url = "";
                if (Session["Role"].ToString() == "1")
                {
                    url = "MyTaskGrower.aspx";
                }
                else
                {
                    url = "MyTaskAssistantGrower.aspx";
                }


                string message = "Assignment Successful";
                //string url = "MyTaskAssistantGrower.aspx";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                // lblmsg.Text = "Assignment Successful";
                clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }
        }

        public void clear()
        {

            //  ddlSupervisor.SelectedIndex = 0;

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskAssistantGrower.aspx");
        }

        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlantReady.PageIndex = e.NewPageIndex;
            BindGridPlantReady(1);
        }


    }

}




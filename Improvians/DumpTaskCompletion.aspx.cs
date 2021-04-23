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
    public partial class DumpTaskCompletion : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() == "13")
            {
                this.Page.MasterPageFile = "~/Customer/CustomerMaster.master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Did"] != null)
                {
                    Did = Request.QueryString["Did"].ToString();
                }

                if (Request.QueryString["Chid"] != "0" && Request.QueryString["Chid"] != null)
                {
                    BindGridCropHealth(Convert.ToInt32(Request.QueryString["Chid"]));
                }
                BindPlantReady();
                BindViewDumpDetilas(Convert.ToInt32(Request.QueryString["DrId"]));
                BindGridDumpComplition(Did);
            }
        }





        public void BindGridDumpComplition(string DUMPAID)
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@DumpTaskAssignmentId", DUMPAID.ToString());
            dt1 = objCommon.GetDataTable("SP_GetTaskAssignmenDumpTaskCompletionView", nv1);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
            //    PanelCropHealth.Visible = true;
                PanelAddDump.Visible = false;
                PanelComplition.Visible = true;
                GridDumpComplition.DataSource = dt1;
                GridDumpComplition.DataBind();
                lblComplitionUser.Text = dt1.Rows[0]["EmployeeName"].ToString();
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
                lblCommment.Text = dt1.Rows[0]["CropHealthCommit"].ToString();
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

        private string Did
        {
            get
            {
                if (ViewState["Did"] != null)
                {
                    return (string)ViewState["Did"];
                }
                return "";
            }
            set
            {
                ViewState["Did"] = value;
            }
        }
        public void BindViewDumpDetilas(int  RDid)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
           
            nv.Add("@DumpId", RDid.ToString());
            dt = objCommon.GetDataTable("SP_GetTaskAssignmenttView", nv);
            GridViewDumpView.DataSource = dt;
            GridViewDumpView.DataBind();
            lblReqUser.Text = dt.Rows[0]["EmployeeName"].ToString();
        }

        public void BindPlantReady()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@wo", wo);
            //nv.Add("@JobCode", "");
            //nv.Add("@CustomerName", "");
            //nv.Add("@Facility", "");
            //nv.Add("@Mode", "11");
            nv.Add("@DumpTaskAssignmentId", Did);
            nv.Add("@RoleId", Session["Role"].ToString());
            

            dt = objCommon.GetDataTable("SP_GetOperatorDumpTaskDetails", nv);
            gvPlantReady.DataSource = dt;
            gvPlantReady.DataBind();
         
        }

        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlantReady.PageIndex = e.NewPageIndex;
            BindPlantReady();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            //  nv.Add("@OperatorID",Session["LoginID"].ToString());
            //   nv.Add("@Notes", txtNots.Text);
            //    nv.Add("@JobID", "");
            nv.Add("@Did", Did);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Comments", txtComment.Text);
            nv.Add("@QuantityOfTray", txtQuantityOfTray.Text);
            nv.Add("@DumpDate", txtDumpDate.Text);
          


            result = objCommon.GetDataExecuteScaler("SP_AddDumpCompletion", nv);

            GridViewRow row = gvPlantReady.Rows[0];
            var txtJobNo = (row.FindControl("lblID") as Label).Text;
            var txtBenchLocation = (row.FindControl("lblGreenHouseID") as Label).Text;

            NameValueCollection nameValue = new NameValueCollection();
            nameValue.Add("@LoginID", Session["LoginID"].ToString());
            nameValue.Add("@jobcode", txtJobNo);
            nameValue.Add("@GreenHouseID", txtBenchLocation);
            nameValue.Add("@TaskName", "Dump");

            var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);


            if (result > 0)
            {
                // lblmsg.Text = "Completion Successful";
                clear();
                string message = "Completion Successful";
                string url;
                if (Session["Role"].ToString() == "2")
                {
                    url = "DumpAssignmentForm.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
                if (Session["Role"].ToString() == "12")
                {
                    url = "DumpRequestForm.aspx";
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
                    url = "DumpCompletionForm.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }


            }
            else
            {
                lblmsg.Text = "Completion Not Successful";
            }
        }

        public void clear()
        {
            //txtNots.Text = "";
            txtComment.Text = "";
            //  txtPlantHeight.Text = "";
            txtDumpDate.Text = "";
            txtQuantityOfTray.Text = "";


        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            if (Session["Role"].ToString() == "3")
            {
              //  Response.Redirect("~/PlantReadyCompletionForm.aspx");
            }

            if (Session["Role"].ToString() == "2")
            {
               // Response.Redirect("~/PlantReadyAssignmentForm.aspx");
            }
        }
    }
}
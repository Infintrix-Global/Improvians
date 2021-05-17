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
    public partial class DumpTaskAssignment : System.Web.UI.Page
    {

        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Did"] != null)
                {
                    DId = Request.QueryString["Did"].ToString();
                }
                if (Request.QueryString["Chid"] != "0" && Request.QueryString["Chid"] != null)
                {
                    BindGridCropHealth(Convert.ToInt32(Request.QueryString["Chid"]));
                }
                if (Request.QueryString["TaskRequestKey"] != "0" && Request.QueryString["TaskRequestKey"] != null)
                {
                    TaskRequestKey = Request.QueryString["TaskRequestKey"].ToString();
                }


                BindGridGerm();
                BindDumpView();
                BindOperatorList();
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
        private string TaskRequestKey
        {
            get
            {
                if (ViewState["TaskRequestKey"] != null)
                {
                    return (string)ViewState["TaskRequestKey"];
                }
                return "";
            }
            set
            {
                ViewState["TaskRequestKey"] = value;
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

        private string DId
        {
            get
            {
                if (ViewState["DId"] != null)
                {
                    return (string)ViewState["DId"];
                }
                return "";
            }
            set
            {
                ViewState["DId"] = value;
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

        public void BindOperatorList()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@RoleID", "3");
            ddlOperator.DataSource = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;
            ddlOperator.DataTextField = "EmployeeName";
            ddlOperator.DataValueField = "ID";
            ddlOperator.DataBind();
            ddlOperator.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@DId", DId);
            dt = objCommon.GetDataTable("SP_GetSupervisorDumpAssignTask", nv);
            if (dt != null && dt.Rows.Count > 0)
            {

                gvPlantReady.DataSource = dt;
                gvPlantReady.DataBind();
                Jid = dt.Rows[0]["Jid"].ToString();
            }

        }

        public void BindDumpView()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@DumpId", DId);

            dt = objCommon.GetDataTable("SP_GetDumpRequestView", nv);

            if (dt != null & dt.Rows.Count > 0)
            {
                txtNotes.Text = dt.Rows[0]["Comments"].ToString();
                txtQuantityofTray.Text = dt.Rows[0]["QuantityOfTray"].ToString();
                txtDumpDate.Text = Convert.ToDateTime(dt.Rows[0]["DumpDateR"]).ToString("yyyy-MM-dd");
            }


        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;

            GridViewRow row = gvPlantReady.Rows[0];

            var txtJobNo = (row.FindControl("lblID") as Label).Text;
            var txtBenchLocation = (row.FindControl("lblBenchLoc") as Label).Text;

            NameValueCollection nv = new NameValueCollection();
            nv.Add("@OperatorID", ddlOperator.SelectedValue);
            nv.Add("@Comments", txtNotes.Text);
            nv.Add("@DumpId", DId);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@QuantityOfTray", txtQuantityofTray.Text);
            nv.Add("@DumpDate", txtDumpDate.Text);
            nv.Add("@TaskRequestKey", TaskRequestKey);
            nv.Add("@Jid", Jid);

            //nv.Add("@jobcode", txtJobNo);
            //nv.Add("@GreenHouseID", txtBenchLocation);

            result = objCommon.GetDataExecuteScaler("SP_AddDumpTaskAssignment", nv);
            if (result > 0)
            {

                NameValueCollection nameValue = new NameValueCollection();
                nameValue.Add("@LoginID", Session["LoginID"].ToString());
                nameValue.Add("@jobcode", txtJobNo);
                nameValue.Add("@GreenHouseID", txtBenchLocation);
                nameValue.Add("@TaskName", "Dump");

                var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

                //lblmsg.Text = "Assignment Successful";
                BAL_Classes.General objGeneral = new BAL_Classes.General();
                objGeneral.SendMessage(int.Parse(ddlOperator.SelectedValue), "New Dump Task Assigned", "New Dump Task Assigned", "Dump");
                clear();
                string message = "Assignment Successful";
                string url = "DumpAssignmentForm.aspx";
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
                lblmsg.Text = "Assignment Not Successful";
            }

            var res = (Master.FindControl("r1") as Repeater);
            var lblCount = (Master.FindControl("lblNotificationCount") as Label);
            objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), res, lblCount);
        }

        public void clear()
        {
            ddlOperator.SelectedIndex = 0;
            txtNotes.Text = "";


        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/DumpAssignmentForm.aspx");
        }

        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlantReady.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }
    }
}
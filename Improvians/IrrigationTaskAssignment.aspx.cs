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
    public partial class IrrigationTaskAssignment : System.Web.UI.Page
    {

        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["IrrigationCode"] != null)
                {
                    IrrigationCode = Request.QueryString["IrrigationCode"].ToString();
                }
                if (Request.QueryString["TaskRequestKey"] != "0" && Request.QueryString["TaskRequestKey"] != null)
                {
                    TaskRequestKey = Request.QueryString["TaskRequestKey"].ToString();
                }

                BindGridGerm();
                BindOperatorList();
                BindGridIrrDetailsViewReq();
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

        private string IrrigationCode
        {
            get
            {
                if (ViewState["IrrigationCode"] != null)
                {
                    return (string)ViewState["IrrigationCode"];
                }
                return "";
            }
            set
            {
                ViewState["IrrigationCode"] = value;
            }
        }
        public void BindGridIrrDetailsViewReq()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@ICode", IrrigationCode);
            dt = objCommon.GetDataTable("SP_GetIrrigationTaskAssignmentView", nv);

            if (dt != null && dt.Rows.Count > 0)
            {
                txtNotes.Text = dt.Rows[0]["Nots"].ToString();
                txtResetSprayTaskForDays.Text = dt.Rows[0]["ResetSprayTaskForDays"].ToString();
                txtSprayDate.Text = Convert.ToDateTime(dt.Rows[0]["SprayDate"]).ToString("yyyy-MM-dd");
                txtWaterRequired.Text = dt.Rows[0]["WaterRequired"].ToString();
            }

        }

        public void BindOperatorList()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlOperator.DataSource = objCommon.GetDataTable("SP_GetRoleForSupervisor", nv);
            ddlOperator.DataTextField = "EmployeeName";
            ddlOperator.DataValueField = "ID";
            ddlOperator.DataBind();
            ddlOperator.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindGridGerm()
        {
            string ChId = "";
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@wo", wo);
            //nv.Add("@JobCode","");
            //nv.Add("@CustomerName","");
            //nv.Add("@Facility","");
            //nv.Add("@Mode", "4");
            //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            nv.Add("@IrrigationCode", IrrigationCode);
            nv.Add("@RoleId", Session["Role"].ToString());
            dt = objCommon.GetDataTable("SP_GetSupervisorIrrigationTaskByIrrigationCode", nv);
            if (dt != null && dt.Rows.Count > 0)
            {
                GridIrrigation.DataSource = dt;
                GridIrrigation.DataBind();

                ChId = dt.Rows[0]["CropHealth"].ToString();
                if (ChId == "")
                {
                    ChId = "0";
                }
                else
                {
                    ChId = ChId;
                }
                BindGridCropHealth(Convert.ToInt32(ChId));
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@OperatorID", ddlOperator.SelectedValue);
            nv.Add("@IrrigationCode", IrrigationCode);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@SprayDate", txtSprayDate.Text);
            nv.Add("@TaskRequestKey", TaskRequestKey);
            nv.Add("@ResetSprayTaskForDays", txtResetSprayTaskForDays.Text);
            nv.Add("@Comments", txtNotes.Text);
            nv.Add("@WaterRequired", txtWaterRequired.Text);


            result = objCommon.GetDataExecuteScaler("SP_AddIrrigationTaskAssignment", nv);
            if (result > 0)
            {
                GridViewRow row = GridIrrigation.Rows[0];

                var txtJobNo = (row.FindControl("lblID") as Label).Text;
                var txtBenchLocation = (row.FindControl("lblGreenHouseID") as Label).Text;

                NameValueCollection nameValue = new NameValueCollection();
                nameValue.Add("@LoginID", Session["LoginID"].ToString());
                nameValue.Add("@jobcode", txtJobNo);
                nameValue.Add("@GreenHouseID", txtBenchLocation);
                nameValue.Add("@TaskName", "Irrigation");

                var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

                //lblmsg.Text = "Assignment Successful";
                clear();
                string message = "Assignment Successful";
                string url = "IrrigationAssignmentForm.aspx";
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
        }

        public void clear()
        {
            ddlOperator.SelectedIndex = 0;
            // txtNotes.Text = "";


        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/IrrigationAssignmentForm.aspx");
        }

        protected void GridIrrigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridIrrigation.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }
    }
}
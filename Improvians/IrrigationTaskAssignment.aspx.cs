using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Improvians
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

                BindGridGerm();
                BindOperatorList();
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
            dt = objCommon.GetDataTable("SP_GetSupervisorIrrigationTaskByIrrigationCode", nv);
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
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@OperatorID", ddlOperator.SelectedValue);
            nv.Add("@IrrigationCode", IrrigationCode);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            //nv.Add("@wo", wo);
            //  nv.Add("@SprayDate","");
            // nv.Add("@TraysSprayed", "");
            // nv.Add("@SprayDuration", "");
            //nv.Add("@mode", "2");

            result = objCommon.GetDataExecuteScaler("SP_AddIrrigationTaskAssignment", nv);
            if (result > 0)
            {
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
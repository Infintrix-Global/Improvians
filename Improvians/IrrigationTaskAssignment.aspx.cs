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
                if (Request.QueryString["IRID"] != null)
                {
                    IRID = Request.QueryString["IRID"].ToString();
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

        private string IRID
        {
            get
            {
                if (ViewState["IRID"] != null)
                {
                    return (string)ViewState["IRID"];
                }
                return "";
            }
            set
            {
                ViewState["IRID"] = value;
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
            //nv.Add("@wo", wo);
            //nv.Add("@JobCode","");
            //nv.Add("@CustomerName","");
            //nv.Add("@Facility","");
            //nv.Add("@Mode", "4");
            //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            nv.Add("@IRID", IRID);
            dt = objCommon.GetDataTable("SP_GetSupervisorIrrigationTaskByIRID", nv);
            GridIrrigation.DataSource = dt;
            GridIrrigation.DataBind();

        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@OperatorID", ddlOperator.SelectedValue);
            nv.Add("@IRID", IRID);
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
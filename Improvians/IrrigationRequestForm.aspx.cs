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
    public partial class IrrigationRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                BindFacility();
                BindGridIrrigation();
                BindSupervisorList();
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

        public void BindGridIrrigation()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@wo","");
            nv.Add("@JobCode",ddlJobNo.SelectedValue);
            nv.Add("@CustomerName",ddlCustomer.SelectedValue);
            nv.Add("@Facility",ddlFacility.SelectedValue);
            nv.Add("@Mode", "1");
            dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            GridIrrigation.DataSource = dt;
            GridIrrigation.DataBind();
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

        public void BindFacility()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "9");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlFacility.DataSource = dt;
            ddlFacility.DataTextField = "loc_seedline";
            ddlFacility.DataValueField = "loc_seedline";
            ddlFacility.DataBind();
            ddlFacility.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        public void BindSupervisorList()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlSupervisor.DataTextField = "EmployeeName";
            ddlSupervisor.DataValueField = "ID";
            ddlSupervisor.DataBind();
            ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation();
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation();
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation();
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();
            BindFacility();
            BindGridIrrigation();
        }
        protected void GridIrrigation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                string rowIndex = e.CommandArgument.ToString();

                wo = rowIndex;
                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@wo", wo);
                nv.Add("@JobCode", ddlJobNo.SelectedValue);
                nv.Add("@CustomerName", ddlCustomer.SelectedValue);
                nv.Add("@Facility", ddlFacility.SelectedValue);
                nv.Add("@Mode", "2");
                dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
                lblJobID.Text = dt.Rows[0]["jobcode"].ToString();
               // txtIrrigatedNoTrays.Text = dt.Rows[0]["trays_actual"].ToString();
                //txtIrrigationDuration.Text = dt.Rows[0]["jobcode"].ToString();
                txtNotes.Focus();
                
            }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);
            nv.Add("@WO", wo);
            nv.Add("@IrrigatedNoTrays", txtIrrigatedNoTrays.Text.Trim());
            nv.Add("@WaterRequired",txtWaterRequired.Text.Trim());
            nv.Add("@IrrigationDuration","");
            nv.Add("@SprayDate",txtSprayDate.Text.Trim());
            nv.Add("@SprayTime", txtSprayTime.Text.Trim());
            nv.Add("@Nots", txtNotes.Text.Trim());
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataInsertORUpdate("SP_AddIrrigationRequest", nv);
            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                string message = "Assignment Successful";
                string url = "MyTaskGrower.aspx";
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

            ddlSupervisor.SelectedIndex = 0;
            txtWaterRequired.Text = "";
            txtNotes.Text = "";
            txtIrrigatedNoTrays.Text = "";
            txtIrrigationDuration.Text = "";
            txtSprayDate.Text = "";
            txtSprayTime.Text = "";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }

        protected void GridIrrigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridIrrigation.PageIndex = e.NewPageIndex;
            BindGridIrrigation();
        }

        
    }
}
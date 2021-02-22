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
    public partial class GerminationRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                BindFacility();
                BindBenchLocation();
                BindGridGerm();
                BindSupervisorList();
                txtDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            }
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

        public void BindBenchLocation()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "10");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlBenchLocation.DataSource = dt;
            ddlBenchLocation.DataTextField = "GreenHouseID";
            ddlBenchLocation.DataValueField = "GreenHouseID";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", ddlFacility.SelectedValue);
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@Week", radweek.SelectedValue);
            nv.Add("@Status", radStatus.SelectedValue);
            dt = objCommon.GetDataTable("SP_GetGerminationRequest", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }
        public void BindSupervisorList()
        {
            NameValueCollection nv = new NameValueCollection();
            if (Session["Role"].ToString() == "1")
            {
                ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlSupervisor.DataTextField = "EmployeeName";
                ddlSupervisor.DataValueField = "ID";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            if (Session["Role"].ToString() == "12")
            {
                ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlSupervisor.DataTextField = "EmployeeName";
                ddlSupervisor.DataValueField = "ID";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                divReschedule.Visible = false;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvGerm.Rows[rowIndex];
                //string facName = (row.FindControl("lblFacility") as Label).Text;

                DataTable dt = new DataTable();
                //   NameValueCollection nv = new NameValueCollection();
                //  nv.Add("@FacilityName", facName);
                //  dt = objCommon.GetDataTable("SP_GetSupervisorNameByFacilityID", nv);
                lblJobID.Text = (row.FindControl("lbljobID") as Label).Text;
                lblID.Text = (row.FindControl("lblID") as Label).Text;

                string Datwc = (row.FindControl("lblGermDate") as Label).Text;

                txtDate.Text = Convert.ToDateTime((row.FindControl("lblGermDate") as Label).Text).ToString("yyyy-MM-dd");

                lblBenchlocation.Text = (row.FindControl("lblBenchLocation") as Label).Text;
                lblDescription.Text = (row.FindControl("lblDescription") as Label).Text;
                lblTotalTrays.Text = (row.FindControl("lblTrays") as Label).Text;
                //   lblfacsupervisor.InnerText = "Green House Supervisor"; //+ facName;
                // lblSupervisorID.Text = dt.Rows[0]["ID"].ToString();
                //lblSupervisorName.Text = dt.Rows[0]["EmployeeName"].ToString();
                txtDate.Focus();
            }

            if (e.CommandName == "Dismiss")
            {
                int GTID = Convert.ToInt32(e.CommandArgument);
                long result = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@GTID", GTID.ToString());
                result = objCommon.GetDataInsertORUpdate("SP_DismissGerminationRequest", nv);

                BindGridGerm();
            }
            if (e.CommandName == "Reschedule")
            {
                divReschedule.Visible = true;
                userinput.Visible = false;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvGerm.Rows[rowIndex];
                //string facName = (row.FindControl("lblFacility") as Label).Text;

                DataTable dt = new DataTable();
                //   NameValueCollection nv = new NameValueCollection();
                //  nv.Add("@FacilityName", facName);
                //  dt = objCommon.GetDataTable("SP_GetSupervisorNameByFacilityID", nv);
                lblRescheduleJobID.Text = (row.FindControl("lbljobID") as Label).Text;
                lblRescheduleID.Text = (row.FindControl("lblID") as Label).Text;


                txtNewDate.Text = Convert.ToDateTime((row.FindControl("lblGermDate") as Label).Text).ToString("yyyy-MM-dd");
                //   lblfacsupervisor.InnerText = "Green House Supervisor"; //+ facName;
                // lblSupervisorID.Text = dt.Rows[0]["ID"].ToString();
                //lblSupervisorName.Text = dt.Rows[0]["EmployeeName"].ToString();
                txtNewDate.Focus();
                BindGridGerm();
            }
        }


        protected void btnReschedule_Click(object sender, EventArgs e)
        {
            int GTID = Convert.ToInt32(lblRescheduleID.Text);
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@GTID", GTID.ToString());
            nv.Add("@GermDate", txtNewDate.Text);
            result = objCommon.GetDataInsertORUpdate("SP_RescheduleGerminationRequest", nv);
        }
        protected void btnResetReschedule_Click(object sender, EventArgs e)
        {
            txtNewDate.Text = "";
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);
            nv.Add("@InspectionDueDate", txtDate.Text);
            nv.Add("@#TraysInspected", txtTrays.Text);
            nv.Add("@ID", lblID.Text);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataInsertORUpdate("SP_AddGerminationRequest", nv);
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
            txtDate.Text = "";
            txtTrays.Text = "";
            //lblSupervisorID.Text = "";
            // lblSupervisorName.Text = "";
            // lblfacsupervisor.InnerText = "";
            ddlSupervisor.SelectedIndex = 0;

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }

        protected void radweek_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        protected void btnManual_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/GerminationRequestManual.aspx");
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        protected void radStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            radStatus.SelectedValue = null;
            radweek.SelectedValue = null;

            Bindcname();
            BindJobCode();
            BindFacility();
            BindBenchLocation();
            BindGridGerm();

        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }
    }
}
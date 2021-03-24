using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class GerminationRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Fdate = "", TDate = "";
                Fdate = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(14).ToString("yyyy-MM-dd");

                txtFromDate.Text = Fdate;
                txtToDate.Text = TDate;
                Bindcname();
                BindBenchLocation(Session["Facility"].ToString());
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

        public void BindJobCode(string ddlBench)
        {

            ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();

            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlJobNo.Items[0].Selected = false;
            ddlJobNo.ClearSelection();
        }

        public void BindBenchLocation(string ddlMain)
        {

            ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataTextField = "p2";
            ddlBenchLocation.DataValueField = "p2";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
            ddlBenchLocation.Items[0].Selected = false;
            ddlBenchLocation.ClearSelection();

        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindJobCode(ddlBenchLocation.SelectedValue);
            BindGridGerm();

        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }
        protected void RadioButtonListSourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        protected void RadioButtonListF_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@Week", radweek.SelectedValue);
            nv.Add("@Status", radStatus.SelectedValue);
            nv.Add("@Jobsource", RadioButtonListSourse.SelectedValue);
            nv.Add("@GermNo", RadioButtonListGno.SelectedValue);
            nv.Add("@FromDate", txtFromDate.Text);
            nv.Add("@ToDate", txtToDate.Text);
         //   dt = objCommon.GetDataTable("SP_GetGerminationRequest", nv);

            if (Session["Role"].ToString() == "12")
            {
                dt = objCommon.GetDataTable("SP_GetGerminationRequestAssistantGrower", nv);
            }
            else
            {
                dt = objCommon.GetDataTable("SP_GetGerminationRequest", nv);
            }

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
                lblGermNo.Text = (row.FindControl("lblGermNo") as Label).Text;

                DateTime Germdt = Convert.ToDateTime((row.FindControl("lblGermDate") as Label).Text);
                lblOldDate.Text = Germdt.ToString();
                txtNewDate.Text = Germdt.ToString("yyyy-MM-dd");
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
            if (radReschedule.SelectedValue == "2" && lblGermNo.Text == "Germination 1")
            {
                double diff = (Convert.ToDateTime(txtNewDate.Text) - Convert.ToDateTime(lblOldDate.Text)).TotalDays;
                nv.Add("@diff", diff.ToString());
            }
            else
            {
                nv.Add("@diff", "0");
            }
            result = objCommon.GetDataInsertORUpdate("SP_RescheduleGerminationRequest", nv);
            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                string message = "Reschedule Successful";
                string url = "GerminationRequestForm.aspx";
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



        protected void radStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            radStatus.SelectedValue = null;
            radweek.SelectedValue = null;

            RadioButtonListSourse.Items[0].Selected = false;


            RadioButtonListSourse.ClearSelection();
            RadioButtonListGno.Items[0].Selected = false;

            RadioButtonListGno.ClearSelection();
            Bindcname();            
            BindBenchLocation(Session["Facility"].ToString());
            BindJobCode(ddlBenchLocation.SelectedValue);
            BindGridGerm();

        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblsource = (Label)e.Row.FindControl("lblsource");
                Label lblGermNo = (Label)e.Row.FindControl("lblGermNo");
                if (lblsource.Text == "Manual")
                {
                    lblsource.Text = "Navision";
                }
                HyperLink lnkJobID = (HyperLink)e.Row.FindControl("lnkJobID");
                lnkJobID.NavigateUrl = "~/JobReports.aspx?JobCode=" + lnkJobID.Text+ "&GermNo=" + lblGermNo.Text;
              //  lnkJobID.NavigateUrl(String.Format("~/CropHealthReport.aspx?Chid={0}", Chid));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridGerm();
        }
    }
}
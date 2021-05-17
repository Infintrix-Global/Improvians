using Evo.Bal;
using Evo.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        Admin.clsCommonMasters objCom = new Admin.clsCommonMasters();
        List<string> BenchLocations = new List<string>();
        List<string> Customers = new List<string>();
        List<string> AssignedBys = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Fdate = "", TDate = "";

                if (string.IsNullOrWhiteSpace(JobCode) && string.IsNullOrWhiteSpace(benchLoc))
                {
                    Fdate = Convert.ToDateTime(System.DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd");
                    TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(14).ToString("yyyy-MM-dd");

                    txtFromDate.Text = Fdate;
                    txtToDate.Text = TDate;
                }
                BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
                BindJobCode("0", "0", "0");
                Bindcname("0", "0", "0");
                BindCrop();
                BindAssignByList("0", "0", "0");
                BindGridGerm("0", 0);
                BindSupervisorList();
                txtDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
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

        private string TaskKey
        {
            get
            {
                if (Request.QueryString["Tkey"] != null)
                {
                    return Request.QueryString["Tkey"].ToString();
                }
                return "";
            }
            set
            {

            }
        }
        public void BindCrop()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@JobNo", ddlJobNo.SelectedValue);
            nv.Add("@GenusCode", "0");
            nv.Add("@Mode", "6");
            nv.Add("@Type", "Ger");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            ddlCrop.DataSource = dt;
            ddlCrop.DataTextField = "GenusCode";
            ddlCrop.DataValueField = "GenusCode";
            ddlCrop.DataBind();
            ddlCrop.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindAssignByList(string ddlBench, string jobNo, string Cust)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBench);
            nv.Add("@Customer", Cust);
            nv.Add("@JobNo", jobNo);
            nv.Add("@GenusCode", ddlCrop.SelectedValue);
            nv.Add("@Mode", "4");
            nv.Add("@Type", "Ger");


            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);

            ddlAssignedBy.DataSource = dt;
            ddlAssignedBy.DataTextField = "AssingTo";
            ddlAssignedBy.DataValueField = "AssingTo";
            ddlAssignedBy.DataBind();
            ddlAssignedBy.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlAssignedBy.Items.Insert(1, new ListItem("System", "System"));
        }
        public void Bindcname(string ddlBench, string jobNo, string Code)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", !string.IsNullOrEmpty(ddlBench) ? ddlBench : "0");
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@JobNo", !string.IsNullOrEmpty(jobNo) ? jobNo : "0");
            nv.Add("@GenusCode", !string.IsNullOrEmpty(Code) ? Code : "0");
            nv.Add("@Mode", "3");
            nv.Add("@Type", "Ger");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "Customer";
            ddlCustomer.DataValueField = "Customer";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindJobCode(string ddlBench, string Customer, string Code)
        {
            //  ddlJobNo.Items[0].Selected = false;
            // ddlJobNo.ClearSelection();
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", !string.IsNullOrEmpty(ddlBench) ? ddlBench : "0");
            nv.Add("@Customer", !string.IsNullOrEmpty(Customer) ? Customer : "0");
            nv.Add("@JobNo", "0");
            nv.Add("@GenusCode", !string.IsNullOrEmpty(Code) ? Code : "0");

            nv.Add("@Mode", "2");
            nv.Add("@Type", "Ger");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            //   ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataBind();

            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindBenchLocation(string ddlMain, string jobNo, string Customer, string Code)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", ddlMain);
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@Customer", !string.IsNullOrEmpty(Customer) ? Customer : "0");
            nv.Add("@JobNo", !string.IsNullOrEmpty(jobNo) ? jobNo : "0");
            nv.Add("@GenusCode", !string.IsNullOrEmpty(Code) ? Code : "0");
            nv.Add("@Mode", "1");
            nv.Add("@Type", "Ger");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);

            // ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataSource = dt;
            ddlBenchLocation.DataTextField = "BenchLocation";
            ddlBenchLocation.DataValueField = "BenchLocation";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));


        }

        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBenchLocation.SelectedIndex == 0)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(ddlBenchLocation.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            BindGridGerm("0", 1);
        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBenchLocation.SelectedIndex == 0)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(ddlBenchLocation.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridGerm("0", 1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlBenchLocation.SelectedIndex == 0)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, "0", ddlCrop.SelectedValue);
            BindAssignByList("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }
        protected void RadioButtonListSourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm("0", 1);
        }

        protected void RadioButtonListF_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm("0", 1);
        }

        public void BindGridGerm(string JobCode, int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", JobCode);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@Crop", ddlCrop.SelectedValue);
            nv.Add("@Status", "");
            nv.Add("@Jobsource", RadioButtonListSourse.SelectedValue);
            nv.Add("@GermNo", RadioButtonListGno.SelectedValue);
            nv.Add("@FromDate", txtFromDate.Text);
            nv.Add("@ToDate", txtToDate.Text);
            nv.Add("@AssignedBy", ddlAssignedBy.SelectedValue);


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


            //if (p == 0)
            //{
            //    BindBenchLocationList(dt.Rows);
            //    BindCustomerList(dt.Rows);
            //    BindAssignedByList(dt.Rows);
            //}

            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }


        }
        private void BindBenchLocationList(DataRowCollection Rows)
        {
            BenchLocations = Rows.Cast<DataRow>().OrderBy(f => f.ItemArray[15]).Select(c => c.ItemArray[15].ToString()).Distinct().ToList();

            ddlBenchLocation.DataSource = BenchLocations;

            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
            ddlBenchLocation.Items[0].Selected = false;
        }

        private void BindCustomerList(DataRowCollection Rows)
        {
            Customers = Rows.Cast<DataRow>().OrderBy(f => f.ItemArray[0]).Select(c => c.ItemArray[0].ToString()).Distinct().ToList();

            ddlCustomer.DataSource = Customers;
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--- Select ---", "0"));
            //            ddlCustomer.Items[0].Selected = false;
        }

        private void BindAssignedByList(DataRowCollection Rows)
        {
            AssignedBys = Rows.Cast<DataRow>().OrderBy(f => f.ItemArray[18]).Select(c => c.ItemArray[18].ToString()).Distinct().ToList();

            ddlAssignedBy.DataSource = AssignedBys;

            ddlAssignedBy.DataBind();
            ddlAssignedBy.Items.Insert(0, new ListItem("--- Select ---", "0"));
            ddlAssignedBy.Items[0].Selected = false;
        }
        private void highlight(int limit)
        {
            var i = gvGerm.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvGerm.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblBenchLocation") as Label).Text;
                var tKey = gvGerm.DataKeys[row.RowIndex].Values[7].ToString();
                i--;
                if (checkJob == JobCode && checklocation == benchLoc && tKey == TaskKey)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvGerm.PageIndex++;
                    gvGerm.DataBind();
                    highlight((limit - 10));
                }
            }
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
                //lblJobID.Text = gvGerm.DataKeys[rowIndex].Values[0].ToString();
                //lblID.Text = (row.FindControl("lblID") as Label).Text;

                //lblAGD.Text = (row.FindControl("lblIsAG") as Label).Text;

                //string Datwc = (row.FindControl("lblGermDate") as Label).Text;

                //txtDate.Text = Convert.ToDateTime((row.FindControl("lblGermDate") as Label).Text).ToString("yyyy-MM-dd");

                //lblBenchlocation.Text = (row.FindControl("lblBenchLocation") as Label).Text;
                //lblDescription.Text = (row.FindControl("lblDescription") as Label).Text;
                //lblTotalTrays.Text = (row.FindControl("lblTrays") as Label).Text;

                lblJobID.Text = gvGerm.DataKeys[rowIndex].Values[0].ToString();
                lblID.Text = gvGerm.DataKeys[rowIndex].Values[1].ToString();

                lblAGD.Text = gvGerm.DataKeys[rowIndex].Values[2].ToString();

                string Datwc = gvGerm.DataKeys[rowIndex].Values[3].ToString();

                txtDate.Text = Convert.ToDateTime(gvGerm.DataKeys[rowIndex].Values[3].ToString()).ToString("yyyy-MM-dd");

                lblBenchlocation.Text = gvGerm.DataKeys[rowIndex].Values[4].ToString();
                lblDescription.Text = gvGerm.DataKeys[rowIndex].Values[5].ToString();
                lblTotalTrays.Text = gvGerm.DataKeys[rowIndex].Values[6].ToString();
                lblTaskRequestKey.Text = gvGerm.DataKeys[rowIndex].Values[7].ToString();



                //  txtTrays.Text = (row.FindControl("lblTrays") as Label).Text;
                //   lblfacsupervisor.InnerText = "Green House Supervisor"; //+ facName;
                // lblSupervisorID.Text = dt.Rows[0]["ID"].ToString();
                //lblSupervisorName.Text = dt.Rows[0]["EmployeeName"].ToString();

                //int rowIndex = Convert.ToInt32(e.CommandArgument);
                //lblJobID.Text = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();
                //lblGrowerID.Text = gvPlantReady.DataKeys[rowIndex].Values[2].ToString();
                //lblPRRId.Text = gvPlantReady.DataKeys[rowIndex].Values[3].ToString();
                //lblJid.Text = gvPlantReady.DataKeys[rowIndex].Values[4].ToString();
                //lblIsAssistant.Text = gvPlantReady.DataKeys[rowIndex].Values[5].ToString();
                //lblBenchlocation.Text = gvPlantReady.DataKeys[rowIndex].Values[7].ToString();
                //lblTotalTrays.Text = gvPlantReady.DataKeys[rowIndex].Values[8].ToString();
                //lblDescription.Text = gvPlantReady.DataKeys[rowIndex].Values[9].ToString();

                DataTable dt1 = new DataTable();
                NameValueCollection nv1 = new NameValueCollection();
                nv1.Add("@GTRId", lblID.Text);
                dt1 = objCommon.GetDataTable("SP_GetTaskAssignmenGerminationTaskViewNew", nv1);

                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    if (dt1.Rows[0]["InspectionDueDate"].ToString() != "")
                    {
                        txtDate.Text = Convert.ToDateTime(dt1.Rows[0]["InspectionDueDate"]).ToString("yyyy-MM-dd");
                    }
                    txtTrays.Text = dt1.Rows[0]["#TraysInspected"].ToString();
                    txtGcomments.Text = dt1.Rows[0]["Comments"].ToString();
                }

                txtDate.Focus();
            }

            if (e.CommandName == "Dismiss")
            {
                int GTID = Convert.ToInt32(e.CommandArgument);
                long result = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@GTID", GTID.ToString());
                result = objCommon.GetDataInsertORUpdate("SP_DismissGerminationRequest", nv);

                BindGridGerm("0", 1);
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
                BindGridGerm("0", 1);
            }

            if (e.CommandName == "GStart")
            {
                //int rowIndex = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = gvGerm.Rows[rowIndex];
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvGerm.Rows[rowIndex];

                string ChId = "0";
                lblID.Text = gvGerm.DataKeys[rowIndex].Values[1].ToString();
                lblJobID.Text = gvGerm.DataKeys[rowIndex].Values[0].ToString();

                long result = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@OperatorID", Session["LoginID"].ToString());
                
                nv.Add("@GTID", lblID.Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@Jobcode", lblJobID.Text);
                result = objCommon.GetDataExecuteScaler("SP_AddGerminatioMyTasknGrowarStart", nv);


                NameValueCollection nvR = new NameValueCollection();
                nvR.Add("@GTAId", result.ToString());
                DataTable dtR = objCommon.GetDataTable("SP_GetTaskAssignmenGerminationRequestID", nvR);
                string GTRID = dtR.Rows[0]["GTRID"].ToString();


                // Session["WorkOrder"] = JobID;
                Response.Redirect(String.Format("~/GreenHouseTaskCompletion.aspx?GTAID={0}&Chid={1}&GTRID={2}&IsF={3}", result.ToString(), ChId, GTRID, 0));
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
                script += "'; };";
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
            nv.Add("@Role", ddlSupervisor.SelectedValue);
            nv.Add("@ISAG", lblAGD.Text);
            nv.Add("@TaskRequestKey", lblTaskRequestKey.Text);
            nv.Add("@Comments", txtGcomments.Text);


            if (Session["Role"].ToString() == "1")
            {
                result = objCommon.GetDataInsertORUpdate("SP_AddGerminationRequest", nv);
            }
            else
            {
                result = objCommon.GetDataInsertORUpdate("SP_AddGerminationRequestAS", nv);
            }

            NameValueCollection nameValue = new NameValueCollection();
            nameValue.Add("@LoginID", Session["LoginID"].ToString());
            nameValue.Add("@jobcode", lblJobID.Text);
            nameValue.Add("@GreenHouseID", lblBenchlocation.Text);
            nameValue.Add("@TaskName", "Germination");

            var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@OperatorID", ddlSupervisor.SelectedValue);
            //nv.Add("@Notes","");
            //nv.Add("@WorkOrderID", "");
            //nv.Add("@GTRID", lblID.Text);
            //nv.Add("@LoginID", Session["LoginID"].ToString());
            //result = objCommon.GetDataExecuteScaler("SP_AddGerminationAssignmentNew1", nv);

            //if (Session["Role"].ToString() == "1")
            //{
            //    Response.Redirect("MyTaskGrower.aspx");
            //}
            //else
            //{
            //    Response.Redirect("MyTaskAssistantGrower.aspx");
            //}

            if (result > 0)
            {
                General objGeneral = new General();
                objGeneral.SendMessage(int.Parse(ddlSupervisor.SelectedValue), "New Germination Task Assigned", "New Germination Task Assigned", "Germination");
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
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
                objCommon.ShowAlertAndRedirect(message, url);

                clear();
            }
            else
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
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
            BindGridGerm("0", 1);
        }

        protected void btnManual_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/GerminationRequestManual.aspx");
        }


        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            RadioButtonListSourse.Items[0].Selected = false;
            RadioButtonListSourse.ClearSelection();
            RadioButtonListGno.Items[0].Selected = false;
            RadioButtonListGno.ClearSelection();
            BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
            Bindcname("0", "0", "0");
            // BindJobCode("0");
            BindJobCode("0", "0", "0");
            BindCrop();
            BindGridGerm("0", 1);

        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGenusCode = (Label)e.Row.FindControl("lblGenusCode");
                Label lblTraySize = (Label)e.Row.FindControl("lblTraySize");
                Label lblSeededDate = (Label)e.Row.FindControl("lblSeededDate");
             //   Label lblPlantDueDate = (Label)e.Row.FindControl("lblPlantDueDate");
                //DataTable dt = new DataTable();
                //NameValueCollection nv = new NameValueCollection();
                //nv.Add("@Tray_Size", lblTraySize.Text);
                //nv.Add("@GCode", lblGenusCode.Text);
                //dt = objCommon.GetDataTable("spGetDateDhiftCreateTaskPlantNo", nv);

                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    int PlanrDDate = 0;
                //    PlanrDDate = Convert.ToInt32(dt.Rows[0]["dateshift"]);
                //    lblPlantDueDate.Text = Convert.ToDateTime(lblSeededDate.Text).AddDays(PlanrDDate).ToString("MM/dd/yyyy");
                //}

                Label lblsource = (Label)e.Row.FindControl("lblsource");
                Label lblGermNo = (Label)e.Row.FindControl("lblGermNo");
                Label lblGermDate = (Label)e.Row.FindControl("lblGermDate");
                if (lblsource.Text == "Manual")
                {
                    lblsource.Text = "Navision";
                }
                else
                {
                    lblsource.Text = "App";
                }
                HyperLink lnkJobID = (HyperLink)e.Row.FindControl("lnkJobID");
                lnkJobID.NavigateUrl = "~/JobReports.aspx?JobCode=" + lnkJobID.Text + "&GermNo=" + lblGermNo.Text;

                string dtimeString = Convert.ToDateTime(lblGermDate.Text).ToString("yyyy/MM/dd");

                DateTime dtime = Convert.ToDateTime(dtimeString);

                DateTime nowtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));

                if (nowtime > dtime)
                {
                    e.Row.CssClass = "overdue";
                }
                //  lnkJobID.NavigateUrl(String.Format("~/CropHealthReport.aspx?Chid={0}", Chid));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindJobCode(ddlBenchLocation.SelectedValue);
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname(ddlBenchLocation.SelectedValue, "0", "0");
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(ddlBenchLocation.SelectedValue, "0", "0");
            if (ddlAssignedBy.SelectedIndex == 0)
                BindAssignByList(ddlBenchLocation.SelectedValue, "0", "0");
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }

        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            BindBenchLocation(Session["Facility"].ToString(), txtSearchJobNo.Text, "0", "0");
            BindGridGerm(txtSearchJobNo.Text, 1);

        }

        protected void txtBatchLocation_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            //BindJobCode(ddlBenchLocation.SelectedValue);
            Bindcname(ddlBenchLocation.SelectedValue, "0", "0");
            BindJobCode(ddlBenchLocation.SelectedValue, "0", "0");
            BindAssignByList(ddlBenchLocation.SelectedValue, "0", "0");
            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Evo"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    string Facility = HttpContext.Current.Session["Facility"].ToString();
                    cmd.CommandText = " select distinct jobcode from gti_jobs_seeds_plan where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "'  AND jobcode like '" + prefixText + "%' order by jobcode" +
                        "";

                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(sdr["jobcode"].ToString());
                        }
                    }
                    conn.Close();

                    return customers;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchBenchLocation(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Evo"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //and t.[Location Code]= '" + Session["Facility"].ToString() + "'
                    //cmd.CommandText = "select distinct t.[Job No_] as jobcode  from[GTI$IA Job Tracking Entry] t, [GTI$Job] j where j.No_ = t.[Job No_] and j.[Job Status] = 2  " +
                    //" AND t.[Job No_] like '" + prefixText + "%'";


                    string Facility = HttpContext.Current.Session["Facility"].ToString();
                    cmd.CommandText = " select distinct jobcode from gti_jobs_seeds_plan where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%'order by jobcode" +
                        "";

                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(sdr["jobcode"].ToString());
                        }
                    }
                    conn.Close();

                    return customers;
                }
            }
        }

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }
    }
}
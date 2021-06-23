using Evo.Bal;
using Evo.BAL_Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class PlantReadyRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        static List<Job> lstJob = new List<Job>();
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!IsPostBack)
            {
                string Fdate = "", TDate = "", FRDate = "";
                Fdate = Convert.ToDateTime(System.DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd");
                TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(14).ToString("yyyy-MM-dd");
                FRDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                // txtFertilizationDate.Text = FRDate;
                //   txtFromDate.Text = Fdate;
                //  txtToDate.Text = TDate;


                BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
                BindJobCode("0", "0", "0");
                Bindcname("0", "0", "0");
                BindCrop();
                BindAssignByList("0", "0", "0");
                BindCrop();
                BindGridPlantReady("0", 0);
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
        public void BindBenchLocation(string ddlMain, string jobNo, string Customer, string Code)
        {
            lstJob.Clear();
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", ddlMain);
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Customer", !string.IsNullOrEmpty(Customer) ? Customer : "0");
            nv.Add("@JobNo", !string.IsNullOrEmpty(jobNo) ? jobNo : "0");
            nv.Add("@GenusCode", !string.IsNullOrEmpty(Code) ? Code : "0");
            nv.Add("@Mode", "1");
            nv.Add("@Type", "PR");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);

            // ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataSource = dt;
            ddlBenchLocation.DataTextField = "BenchLocation";
            ddlBenchLocation.DataValueField = "BenchLocation";
            ddlBenchLocation.DataBind();
          //  ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));


        }
        public void BindCrop()
        {
            lstJob.Clear();
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@JobNo", ddlJobNo.SelectedValue);
            nv.Add("@GenusCode", "0");
            nv.Add("@Mode", "6");
            nv.Add("@Type", "PR");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            ddlCrop.DataSource = dt;
            ddlCrop.DataTextField = "GenusCode";
            ddlCrop.DataValueField = "GenusCode";
            ddlCrop.DataBind();
            ddlCrop.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindAssignByList(string ddlBench, string jobNo, string Cust)
        {
            lstJob.Clear();
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBench);
            nv.Add("@Customer", Cust);
            nv.Add("@JobNo", jobNo);
            nv.Add("@GenusCode", ddlCrop.SelectedValue);
            nv.Add("@Mode", "4");
            nv.Add("@Type", "PR");


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
            lstJob.Clear();
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", !string.IsNullOrEmpty(ddlBench) ? ddlBench : "0");
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@JobNo", !string.IsNullOrEmpty(jobNo) ? jobNo : "0");
            nv.Add("@GenusCode", !string.IsNullOrEmpty(Code) ? Code : "0");
            nv.Add("@Mode", "3");
            nv.Add("@Type", "PR");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "Customer";
            ddlCustomer.DataValueField = "Customer";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstJob.Clear();
            string selectedValues = getBenchLocation();
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname(selectedValues, "0", "0");
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(selectedValues, "0", "0");
            if (ddlAssignedBy.SelectedIndex == 0)
                BindAssignByList(selectedValues, "0", "0");
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridPlantReady(ddlJobNo.SelectedValue, 1);
        }
        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstJob.Clear();
            if (ddlBenchLocation.SelectedIndex == -1)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(getBenchLocation(), ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            BindGridPlantReady("0", 1);
        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstJob.Clear();
            if (ddlBenchLocation.SelectedIndex == -1)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(getBenchLocation(), ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridPlantReady("0", 1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstJob.Clear();
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlBenchLocation.SelectedIndex == -1)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, "0", ddlCrop.SelectedValue);
            BindAssignByList("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridPlantReady(ddlJobNo.SelectedValue, 1);
        }

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstJob.Clear();
            BindGridPlantReady(ddlJobNo.SelectedValue, 1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lstJob.Clear();


            string Jno = "";
            if (txtSearchJobNo.Text == "")
            {
                Jno = ddlJobNo.SelectedValue;

            }
            else
            {
                Jno = txtSearchJobNo.Text;
            }

            BindGridPlantReady(Jno, 1);
        }
        protected void RadioButtonListSourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstJob.Clear();
            BindGridPlantReady("0", 1);
        }

        private string getBenchLocation()
        {
            int c = 0;
            string x = "";
            string chkSelected = "";
            foreach (ListItem item in ddlBenchLocation.Items)
            {
                if (item.Selected)
                {
                    c = 1;
                    x += item.Text + ",";
                }
            }
            if (c > 0)
            {
                chkSelected = x.Remove(x.Length - 1, 1);
            }
            return chkSelected;
        }

        public void BindTaskType()
        {

            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@ID", "0");
            //RadioButtonListGno.DataSource = objCommon.GetDataTable("SP_GetChemicalRequestDateCountNo", nv); ;
            //RadioButtonListGno.DataTextField = "DateCountNoName";
            //RadioButtonListGno.DataValueField = "DateCountNo";
            //RadioButtonListGno.DataBind();
            //RadioButtonListGno.Items.Insert(0, new ListItem("--Select--", "0"));


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

        public void BindGridPlantReady(string JobCode, int p)
        {
          


            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", JobCode);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Crop", ddlCrop.SelectedValue);
            nv.Add("@Status", "");
            nv.Add("@Jobsource", RadioButtonListSourse.SelectedValue);
            nv.Add("@GermNo", "0");
            nv.Add("@FromDate", txtFromDate.Text);
            nv.Add("@ToDate", txtToDate.Text);
            nv.Add("@AssignedBy", ddlAssignedBy.SelectedValue);

            if (Session["Role"].ToString() == "12")
            {
                dt = objCommon.GetDataTable("SP_GetPlantReadyRequestAssistantGrower", nv);
            }
            else
            {
                dt = objCommon.GetDataTable("SP_GetPlantReadyRequest", nv);
            }
         
            gvPlantReady.DataSource = dt;
            gvPlantReady.DataBind();

            //foreach (GridViewRow row in gvPlantReady.Rows)
            //{
            //    var checkJob = (row.FindControl("lbljobID") as Label).Text;
            //    if (checkJob == JobCode)
            //    {
            //        row.CssClass = "highlighted";
            //    }
            //}

            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }
        }
        private void highlight(int limit)
        {
            var i = gvPlantReady.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvPlantReady.Rows)
            {
                var checkJob = (row.FindControl("lbljobcode1") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                var tKey = gvPlantReady.DataKeys[row.RowIndex].Values[11].ToString();
                i--;
                if (checkJob == JobCode && checklocation == benchLoc && tKey == TaskKey)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvPlantReady.PageIndex++;
                    gvPlantReady.DataBind();
                    highlight((limit - 10));
                }
            }
        }
        public void BindSupervisorList()
        {
            NameValueCollection nv = new NameValueCollection();
            DataTable dt = new DataTable();
            nv.Add("@RoleID", Session["Role"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetRoleForAssignementFacility", nv);

            ddlSupervisor.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlSupervisor.DataTextField = "EmployeeName";
            ddlSupervisor.DataValueField = "ID";
            ddlSupervisor.DataBind();
            ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

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
            nv.Add("@Type", "PR");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            //   ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataBind();

            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            txtSearchJobNo.Text = "";
            RadioButtonListSourse.Items[0].Selected = false;
            RadioButtonListSourse.ClearSelection();
            BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
            Bindcname("0", "0", "0");
            BindJobCode("0", "0", "0");
            BindCrop();

            BindGridPlantReady("0", 1);
        }
        protected void gvPlantReady_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                divLabel.Visible = true;
                userinput.Visible = true;
                divReschedule.Visible = false;
               // btnMSubmit.Visible = false;
                btnSubmit.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                lblJobID.Text = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();
                lblGrowerID.Text = gvPlantReady.DataKeys[rowIndex].Values[2].ToString();
                lblPRRId.Text = gvPlantReady.DataKeys[rowIndex].Values[3].ToString();
                lblJid.Text = gvPlantReady.DataKeys[rowIndex].Values[4].ToString();
                lblIsAssistant.Text = gvPlantReady.DataKeys[rowIndex].Values[5].ToString();
                lblBenchlocation.Text = gvPlantReady.DataKeys[rowIndex].Values[7].ToString();
                lblTotalTrays.Text = gvPlantReady.DataKeys[rowIndex].Values[8].ToString();
                lblDescription.Text = gvPlantReady.DataKeys[rowIndex].Values[9].ToString();
                ViewState["tKey"] = gvPlantReady.DataKeys[rowIndex].Values[11].ToString();


           
                txtPlantDate.Text = Convert.ToDateTime(gvPlantReady.DataKeys[rowIndex].Values[6]).ToString("yyyy-MM-dd");
                ddlSupervisor.Focus();

                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@PlantReadyId", lblPRRId.Text);

                dt = objCommon.GetDataTable("SP_GetTaskAssignmenPlantReadytView", nv);

                if (dt != null & dt.Rows.Count > 0)
                {
                    txtPlantComments.Text = dt.Rows[0]["Comments"].ToString();
                    txtPlantDate.Text = Convert.ToDateTime(dt.Rows[0]["PlanDate"]).ToString("yyyy-MM-dd");
                    // txtDumpDate.Text = Convert.ToDateTime(dt.Rows[0]["DumpDateR"]).ToString("yyyy-MM-dd");
                }

                lstJob.Clear();
                lstJob.Add(new Job { ID = Convert.ToInt32(lblPRRId.Text), JobID = lblJid.Text, TaskRequestKey = ViewState["tKey"].ToString(), AGD = lblIsAssistant.Text, GreenHouseID = lblBenchlocation.Text, jobcode = lblJobID.Text,GrowerputawayID =lblGrowerID.Text });

            }

            if (e.CommandName == "GStart")
            {
                long result = 0;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                lblJobID.Text = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();
                lblGrowerID.Text = gvPlantReady.DataKeys[rowIndex].Values[2].ToString();
                lblPRRId.Text = gvPlantReady.DataKeys[rowIndex].Values[3].ToString();
                lblJid.Text = gvPlantReady.DataKeys[rowIndex].Values[4].ToString();
                lblIsAssistant.Text = gvPlantReady.DataKeys[rowIndex].Values[5].ToString();
                lblBenchlocation.Text = gvPlantReady.DataKeys[rowIndex].Values[7].ToString();
                lblTotalTrays.Text = gvPlantReady.DataKeys[rowIndex].Values[8].ToString();
                lblDescription.Text = gvPlantReady.DataKeys[rowIndex].Values[9].ToString();
                txtPlantDate.Text = Convert.ToDateTime(gvPlantReady.DataKeys[rowIndex].Values[6]).ToString("yyyy-MM-dd");
                ViewState["tKey"] = gvPlantReady.DataKeys[rowIndex].Values[11].ToString();
                if (lblPRRId.Text == "0")
                {

                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", Session["LoginID"].ToString());

                    nv.Add("@Jobcode", lblJobID.Text);
                    nv.Add("@Customer", "");
                    nv.Add("@Item", "");
                    nv.Add("@Facility", "");
                    nv.Add("@GreenHouseID", lblBenchlocation.Text);
                    nv.Add("@TotalTray", lblTotalTrays.Text);
                    nv.Add("@TraySize", "");
                    nv.Add("@Itemdesc", "");
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@ChId", "0");
                    nv.Add("@wo", "");
                    nv.Add("@Comments", txtPlantComments.Text.Trim());
                    nv.Add("@PlantDate", txtPlantDate.Text);
                    nv.Add("@Role", Session["Role"].ToString());
                    nv.Add("@SeedDate", "");
                    nv.Add("@Jid", lblJid.Text);
                    nv.Add("@TaskRequestKey", ViewState["tKey"].ToString());


                    result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyRequestManuaCreateTaskStartNew", nv);
                    NameValueCollection nv5 = new NameValueCollection();

                    nv5.Add("@PRTA", result.ToString());
                    DataTable dt = objCommon.GetDataTable("SP_GetPlantReadyTaskAssignmentSelect", nv5);

                    Response.Redirect(String.Format("~/PlantReadyTaskCompletion.aspx?PRAID={0}&PRID={1}", result.ToString(), dt.Rows[0]["PRID"].ToString()));
                }
                else
                {
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@OperatorID", Session["LoginID"].ToString());
                    nv.Add("@Notes", "");
                    nv.Add("@PRID", lblPRRId.Text);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@PlantExpirationDate", "");

                    long result1 = objCommon.GetDataExecuteScaler("SP_AddPlantReadyTaskAssignmentNew", nv);

                    //  int result = objCommon.GetDataInsertORUpdate("SP_AddPlantReadyTaskAssignment", nv);

                    if (result1 > 0)
                    {
                        Response.Redirect(String.Format("~/PlantReadyTaskCompletion.aspx?PRAID={0}&PRID={1}", result1, lblPRRId.Text));
                        //Response.Redirect(String.Format("~/PlantReadyTaskCompletion.aspx?PRAID={0}&PRID={1}", result.ToString(), dt.Rows[0]["PRID"].ToString()));
                    }
                }
            }

            if (e.CommandName == "Reschedule")
            {
                divReschedule.Visible = true;
                userinput.Visible = false;
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                lblRescheduleID.Text = gvPlantReady.DataKeys[rowIndex].Values[10].ToString();
                lblRescheduleJobID.Text = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();

                DateTime oldDate = Convert.ToDateTime(gvPlantReady.DataKeys[rowIndex].Values[6].ToString());
                lblOldDate.Text = oldDate.ToString();
                txtNewDate.Text = oldDate.ToString("yyyy-MM-dd");
                txtNewDate.Focus();
                // BindGridGerm("0", 1);
            }
            if (e.CommandName == "Dismiss")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int ID = Convert.ToInt32(gvPlantReady.DataKeys[rowIndex].Values[10].ToString());
                long result = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@GrowerPutAwayPlantReadyId", ID.ToString());
                result = objCommon.GetDataInsertORUpdate("SP_DismissPlantReadyRequest", nv);
                BindGridPlantReady("0", 1);
            }
        }

        protected void btnReschedule_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(lblRescheduleID.Text);
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@PlantReadyId", ID.ToString());
            nv.Add("@PlantReadyDate", txtNewDate.Text);

            result = objCommon.GetDataInsertORUpdate("SP_ReschedulePlantReadyRequest", nv);
            if (result > 0)
            {
                string message = "Reschedule Successful";
                string url = "PlantReadyRequestForm.aspx";
                objCommon.ShowAlertAndRedirect(message, url);
                clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
            }
        }

        protected void btnResetReschedule_Click(object sender, EventArgs e)
        {
            txtNewDate.Text = "";
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            foreach (Job item in lstJob)
            {
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);
                nv.Add("@ManualID", item.JobID);
                nv.Add("@Comments", txtPlantComments.Text);
                nv.Add("@PRid",item.ID.ToString());
                nv.Add("@PlantDate", txtPlantDate.Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@RoleId", Session["Role"].ToString());
                nv.Add("@IsAssistant", item.AGD);
                nv.Add("@TaskRequestKey",item.TaskRequestKey);
                nv.Add("@jobcode", item.jobcode);
                nv.Add("@GreenHouseID", item.GreenHouseID);
                nv.Add("@GrowerputawayID", item.GrowerputawayID);





                result = objCommon.GetDataInsertORUpdate("SP_AddPlantReadyRequestNew", nv);

                NameValueCollection nameValue = new NameValueCollection();
                nameValue.Add("@LoginID", Session["LoginID"].ToString());
                nameValue.Add("@jobcode", item.jobcode);
                nameValue.Add("@GreenHouseID", item.GreenHouseID);
                nameValue.Add("@TaskName", "Plant Ready");

                var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);
            }
            if (result > 0)
            {
                lstJob.Clear();
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                Evo.BAL_Classes.General objGeneral = new General();
                objGeneral.SendMessage(int.Parse(ddlSupervisor.SelectedValue), "New Plant Ready Task Assigned", "New Plant Ready Task Assigned", "Plant Ready");
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }

            var res = (Master.FindControl("r1") as Repeater);

            var lblCount = (Master.FindControl("lblNotificationCount") as Label);
            objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), res, lblCount);
        }

        public void clear()
        {
            ddlSupervisor.SelectedIndex = 0;
        }

        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            BindBenchLocation(Session["Facility"].ToString(), txtSearchJobNo.Text, "0", "0");
            BindGridPlantReady(txtSearchJobNo.Text, 1);

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }



        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //  gvPlantReady.PageIndex = e.NewPageIndex;
            gvPlantReady.PageIndex = e.NewPageIndex;
            gvPlantReady.DataBind();
            BindGridPlantReady("0", 0);

        }

        protected void btnManual_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PlantReadyReqManual.aspx");
        }

        protected void gvPlantReady_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lbljid = (Label)e.Row.FindControl("lbljid");
                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");

                Job item = lstJob.Find(x => x.JobID ==lbljid.Text);
                if (lstJob.Contains(item))
                    chkSelect.Checked = true;


                Label lblGermDate = (Label)e.Row.FindControl("lblPlantDate");
                string dtimeString = Convert.ToDateTime(lblGermDate.Text).ToString("yyyy/MM/dd");

                DateTime dtime = Convert.ToDateTime(dtimeString);

                DateTime nowtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));

                if (nowtime > dtime)
                {
                    e.Row.CssClass = "overdue";
                }
                Label lblsource = (Label)e.Row.FindControl("lblsource");
                if (lblsource.Text == "Manual")
                {
                    lblsource.Text = "Navision";
                }
                else
                {
                    lblsource.Text = "App";
                }
            }
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
                    //and t.[Location Code]= '" + Session["Facility"].ToString() + "'
                    //cmd.CommandText = "select distinct t.[Job No_] as jobcode  from[GTI$IA Job Tracking Entry] t, [GTI$Job] j where j.No_ = t.[Job No_] and j.[Job Status] = 2  " +
                    //" AND t.[Job No_] like '" + prefixText + "%'";


                    string Facility = HttpContext.Current.Session["Facility"].ToString();
                    cmd.CommandText = " select distinct GPD.jobcode from gti_jobs_seeds_plan GTS inner join GrowerPutAwayDetails GPD on GPD.wo=GTS.wo  where  GPD.FacilityID ='" + Facility + "'  AND GPD.jobcode like '%" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%' order by jobcode" +
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

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            userinput.Visible = true;
            divReschedule.Visible = false;
            divLabel.Visible = false;

          //  btnMSubmit.Visible = true;
            btnSubmit.Visible = true;


            txtPlantDate.Focus();
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelect = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkSelect.NamingContainer;
            if (row != null)
            {
                Label lID = (Label)row.FindControl("lblPRRID");
                Label lJobID = (Label)row.FindControl("lbljid");
                Label lblBenchLocation = (Label)row.FindControl("lblGreenHouseID");
                Label lblIsAG = (Label)row.FindControl("lblIsAssistant");
                Label lblTaskRequestKey = (Label)row.FindControl("lblTaskRequestKey");
                Label lbljobcode = (Label)row.FindControl("lbljobcode1");
                Label lblGrowerputawayID = (Label)row.FindControl("lblGrowerputawayID21");
                if (chkSelect.Checked)
                {
                    lstJob.Add(new Job { ID = Convert.ToInt32(lID.Text), JobID = lJobID.Text, TaskRequestKey = lblTaskRequestKey.Text, AGD = lblIsAG.Text, GreenHouseID = lblBenchLocation.Text, jobcode = lbljobcode.Text,GrowerputawayID= lblGrowerputawayID.Text });
                }
                else
                {
                    Job item = lstJob.Find(x => x.ID == Convert.ToInt32(lID.Text));
                    if (item != null)
                        lstJob.Remove(item);
                }

            }
             lblmsg.Text = lstJob.Count.ToString() + " records selected";
        }
    }
}
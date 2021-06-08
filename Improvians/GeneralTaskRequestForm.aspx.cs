using System;
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
    public partial class GeneralRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Fdate = "", TDate = "";
                Fdate = Convert.ToDateTime(System.DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd");
                TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(14).ToString("yyyy-MM-dd");

                //txtFromDate.Text = Fdate;
                //txtToDate.Text = TDate;

                BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
                BindJobCode("0", "0", "0");
                Bindcname("0", "0", "0");
                BindCrop();
                BindAssignByList("0", "0", "0");
                BindTaskGrid("0", 0);
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
            nv.Add("@Type", "Gen");

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
            nv.Add("@Type", "Gen");


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
            nv.Add("@Type", "Gen");

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
            nv.Add("@Type", "Gen");

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
            nv.Add("@Type", "Gen");

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
            txtFromDate.Text = "";
            txtToDate.Text = "";
            if (ddlBenchLocation.SelectedIndex == 0)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(ddlBenchLocation.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            BindTaskGrid("0", 1);
        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            if (ddlBenchLocation.SelectedIndex == 0)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(ddlBenchLocation.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindTaskGrid("0", 1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlBenchLocation.SelectedIndex == 0)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, "0", ddlCrop.SelectedValue);
            BindAssignByList("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindTaskGrid(ddlJobNo.SelectedValue, 1);
        }




        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindTaskGrid(ddlJobNo.SelectedValue, 1);
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            //BindJobCode(ddlBenchLocation.SelectedValue);
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname(ddlBenchLocation.SelectedValue, "0", "0");
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(ddlBenchLocation.SelectedValue, "0", "0");
            if (ddlAssignedBy.SelectedIndex == 0)
                BindAssignByList(ddlBenchLocation.SelectedValue, "0", "0");
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindTaskGrid(ddlJobNo.SelectedValue, 1);
        }

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            BindTaskGrid(ddlJobNo.SelectedValue, 1);
        }

        //protected void RadioButtonListSourse_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindGridPlantReady("0", 1);
        //}

        //protected void RadioButtonListF_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindGridPlantReady("0", 1);
        //}
        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            BindBenchLocation(Session["Facility"].ToString(), txtSearchJobNo.Text, "0", "0");
            BindTaskGrid(txtSearchJobNo.Text, 1);

        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            //  RadioButtonListSourse.Items[0].Selected = false;
            //  RadioButtonListSourse.ClearSelection();
            string Fdate = "", TDate = "";
            Fdate = Convert.ToDateTime(System.DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd");
            TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(14).ToString("yyyy-MM-dd");

            txtFromDate.Text = Fdate;
            txtToDate.Text = TDate;


            BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
            Bindcname("0", "0", "0");
            // BindJobCode("0");
            BindJobCode("0", "0", "0");
            BindCrop();
            BindTaskGrid("0", 1);

        }

        protected void txtBatchLocation_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            //BindJobCode(ddlBenchLocation.SelectedValue);
            Bindcname(ddlBenchLocation.SelectedValue, "0", "0");
            BindJobCode(ddlBenchLocation.SelectedValue, "0", "0");
            BindAssignByList(ddlBenchLocation.SelectedValue, "0", "0");
            BindTaskGrid(ddlJobNo.SelectedValue, 1);
        }



        public void BindTaskGrid(string JobCode, int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", JobCode);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@Crop", ddlCrop.SelectedValue);
            nv.Add("@Status", "");
            nv.Add("@Jobsource", "0");
            nv.Add("@LoginId", Session["LoginID"].ToString());
            nv.Add("@FromDate", txtFromDate.Text);
            nv.Add("@ToDate", txtToDate.Text);
            nv.Add("@AssignedBy", ddlAssignedBy.SelectedValue);

            dt = objCommon.GetDataTable("SP_GetGeneralRequestAssistantGrower", nv);

            gvTask.DataSource = dt;
            gvTask.DataBind();

            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }
        }
        private void highlight(int limit)
        {
            var i = gvTask.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvTask.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                var tKey = gvTask.DataKeys[row.RowIndex].Values[5].ToString();
                i--;
                if (checkJob == JobCode && checklocation == benchLoc && tKey == TaskKey)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvTask.PageIndex++;
                    gvTask.DataBind();
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


            ddlGeneralAssignment.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlGeneralAssignment.DataTextField = "EmployeeName";
            ddlGeneralAssignment.DataValueField = "ID";
            ddlGeneralAssignment.DataBind();
            ddlGeneralAssignment.Items.Insert(0, new ListItem("--Select--", "0"));

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

        protected void ddlTaskType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Session["SelectedAssignment"] = ddlAssignments.SelectedValue;
            if (ddlTaskType.SelectedItem.Value == "3")
            {
                divFrom.Style["display"] = "block";
                divTo.Style["display"] = "block";
            }
            else
            {
                divFrom.Style["display"] = "none";
                divTo.Style["display"] = "none";
            }

        }

        protected void gvTask_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvTask.Rows[rowIndex];
            TaskRequestKey = gvTask.DataKeys[rowIndex].Values[5].ToString();

            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                HiddenFieldDid.Value = gvTask.DataKeys[rowIndex].Values[1].ToString();
                HiddenFieldJid.Value = gvTask.DataKeys[rowIndex].Values[2].ToString();
                ViewState["jobcode"] = gvTask.DataKeys[rowIndex].Values[3].ToString();
                ViewState["benchloc"] = gvTask.DataKeys[rowIndex].Values[4].ToString();

                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@GeneralId", HiddenFieldDid.Value);

                dt = objCommon.GetDataTable("SP_GetGeneralRequestView", nv);

                if (dt != null & dt.Rows.Count > 0)
                {
                    txtGeneralDate.Text = Convert.ToDateTime(dt.Rows[0]["GeneralTaskDate"]).ToString("yyyy-MM-dd");
                    txtCommentsGeneral.Text = dt.Rows[0]["Comments"].ToString();
                    //txtGeneralDate.Text = Convert.ToDateTime((row.FindControl("lblGDate") as Label).Text).ToString("yyyy-MM-dd");
                    //txtCommentsGeneral.Text = (row.FindControl("lblComs") as Label).Text;
                    if (dt.Rows[0]["id1"].ToString() != "")
                    {
                        ddlTaskType.SelectedValue = dt.Rows[0]["id1"].ToString();
                    }
                    txtFrom.Text = dt.Rows[0]["MoveFrom"].ToString();
                    txtTo.Text = dt.Rows[0]["MoveTo"].ToString();

                    if (ddlTaskType.SelectedItem.Value == "3")
                    {
                        divFrom.Style["display"] = "block";
                        divTo.Style["display"] = "block";
                    }
                    else
                    {
                        divFrom.Style["display"] = "none";
                        divTo.Style["display"] = "none";
                    }
                }
            }
            if (e.CommandName == "Start")
            {
                string ChId = "";
                string Did = gvTask.DataKeys[rowIndex].Values[1].ToString();
                string TaskRequestKey = gvTask.DataKeys[rowIndex].Values[1].ToString();
                if (ChId == "")
                {
                    ChId = "0";
                }
                else
                {
                    ChId = ChId;
                }

                NameValueCollection nv = new NameValueCollection();
                nv.Add("@OperatorID", Session["LoginID"].ToString());
                nv.Add("@Comments", "");
                nv.Add("@GeneralId", Did);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@QuantityOfTray", "");
                nv.Add("@GeneralTaskDate", "");

                long result = objCommon.GetDataExecuteScaler("SP_AddGeneralTaskStart", nv);

                if (result > 0)
                {
                    Response.Redirect(String.Format("~/GeneralTaskCompletion.aspx?Did={0}&Chid={1}&DrId={2}&IsF={3}&TaskRequestKey={4}", result, ChId, Did, 0, TaskRequestKey));
                }

                //    userinput.Visible = true;
                //    divReschedule.Visible = false;
                //    int rowIndex = Convert.ToInt32(e.CommandArgument);
                //    GridViewRow row = gvTask.Rows[rowIndex];


                //    DataTable dt = new DataTable();

                //    lblJobID.Text = (row.FindControl("lbljobID") as Label).Text;
                //    lblID.Text = (row.FindControl("lblID") as Label).Text;

                //    string Datwc = (row.FindControl("lblGermDate") as Label).Text;

                //    txtDate.Text = Convert.ToDateTime((row.FindControl("lblGermDate") as Label).Text).ToString("yyyy-MM-dd");

                //    lblBenchlocation.Text = (row.FindControl("lblBenchLocation") as Label).Text;
                //    lblDescription.Text = (row.FindControl("lblDescription") as Label).Text;
                //    lblTotalTrays.Text = (row.FindControl("lblTrays") as Label).Text;

                //    txtDate.Focus();
            }

            if (e.CommandName == "Dismiss")
            {
                //int GTID = Convert.ToInt32(e.CommandArgument);
                //long result = 0;
                //NameValueCollection nv = new NameValueCollection();
                //nv.Add("@GTID", GTID.ToString());
                //result = objCommon.GetDataInsertORUpdate("SP_DismissGeneralRequest", nv);
                BindTaskGrid("0", 1);
            }
            if (e.CommandName == "Reschedule")
            {
                //divReschedule.Visible = true;
                //userinput.Visible = false;
                //int rowIndex = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = gvTask.Rows[rowIndex];

                //DataTable dt = new DataTable();

                //lblRescheduleJobID.Text = (row.FindControl("lbljobID") as Label).Text;
                //lblRescheduleID.Text = (row.FindControl("lblID") as Label).Text;
                //lblGermNo.Text = (row.FindControl("lblGermNo") as Label).Text;

                //DateTime Germdt = Convert.ToDateTime((row.FindControl("lblGermDate") as Label).Text);
                //lblOldDate.Text = Germdt.ToString();
                //txtNewDate.Text = Germdt.ToString("yyyy-MM-dd");

                //txtNewDate.Focus();
                BindTaskGrid("0", 1);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();

            //GridViewRow row = gvTask.Rows[0];
            var txtJobNo = ViewState["jobcode"].ToString();
            var txtBenchLocation = ViewState["benchloc"].ToString();

            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Aid", ddlGeneralAssignment.SelectedValue);
            dt = objCommon.GetDataTable("spGeEmployeeRoleDetails", nv1);

            nv.Add("@SupervisorID", ddlGeneralAssignment.SelectedValue);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Did", HiddenFieldDid.Value);
            nv.Add("@Comments", txtCommentsGeneral.Text);
            nv.Add("@wo", "0");

            nv.Add("@TaskType", ddlTaskType.SelectedValue);
            nv.Add("@MoveFrom", ddlTaskType.SelectedValue == "3" ? txtFrom.Text : "");
            nv.Add("@MoveTo", ddlTaskType.SelectedValue == "3" ? txtTo.Text : "");

            nv.Add("@ManualID", HiddenFieldJid.Value);
            nv.Add("@GeneralDate", txtGeneralDate.Text);
            nv.Add("@TaskRequestKey", TaskRequestKey);
            nv.Add("@RoleId", dt.Rows[0]["RoleID"].ToString());

            result = objCommon.GetDataInsertORUpdate("SP_AddGeneralRequestManual", nv);

            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                nv.Clear();
                nv.Add("@LoginID", Session["LoginID"].ToString());
                //nv.Add("@Did", HiddenFieldDid.Value);
                nv.Add("@jobcode", txtJobNo);
                nv.Add("@GreenHouseID", txtBenchLocation);
                nv.Add("@TaskName", "General Task");

                var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nv);

                BAL_Classes.General objGeneral = new BAL_Classes.General();
                objGeneral.SendMessage(int.Parse(ddlGeneralAssignment.SelectedValue), "New General Task Assigned", "New General Task Assigned", "General");
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
            //  ddlSupervisor.SelectedIndex = 0;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskAssistantGrower.aspx");
        }

        protected void gvTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTask.PageIndex = e.NewPageIndex;
            BindTaskGrid("0", 1);
        }

        protected void gvTask_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGermDate = (Label)e.Row.FindControl("lblGDate");
                string dtimeString = Convert.ToDateTime(lblGermDate.Text).ToString("yyyy/MM/dd");

                DateTime dtime = Convert.ToDateTime(dtimeString);

                DateTime nowtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));

                if (nowtime > dtime)
                {
                    e.Row.CssClass = "overdue";
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
                    cmd.CommandText = " select distinct jobcode from gti_jobs_seeds_plan where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%' order by jobcode" +
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
    }
}
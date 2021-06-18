using Evo.Bal;
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
    public partial class MoveRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Fdate = "", TDate = "";
                Fdate = Convert.ToDateTime(System.DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd");
                TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(7).ToString("yyyy-MM-dd");
               // txtFromDate.Text = Fdate;
               // txtToDate.Text = TDate;
                BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
                BindJobCode("0", "0", "0");
                Bindcname("0", "0", "0");
                BindCrop();
                BindAssignByList("0", "0", "0");
                // BindFacility();
                BindGridPlantReady(0);
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



        public void BindGridPlantReady(int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@wo", "");
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Crop", ddlCrop.SelectedValue);
            nv.Add("@Jobsource", RadioButtonListSourse.SelectedValue);
            nv.Add("@FromDate", txtFromDate.Text);
            nv.Add("@ToDate", txtToDate.Text);
            nv.Add("@AssignedBy", ddlAssignedBy.SelectedValue);
            dt = objCommon.GetDataTable("SP_GetMoveRequestAssistantGrower", nv);

            gvMoveReq.DataSource = dt;
            gvMoveReq.DataBind();

            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }
        }
        private void highlight(int limit)
        {
            var i = gvMoveReq.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvMoveReq.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                i--;
                if (checkJob == JobCode && checklocation == benchLoc)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvMoveReq.PageIndex++;
                    gvMoveReq.DataBind();
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

            ddlLogisticManager.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlLogisticManager.DataTextField = "EmployeeName";
            ddlLogisticManager.DataValueField = "ID";
            ddlLogisticManager.DataBind();
            ddlLogisticManager.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        public void BindCrop()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@JobNo", ddlJobNo.SelectedValue);
            nv.Add("@GenusCode", "0");
            nv.Add("@Mode", "6");
            nv.Add("@Type", "Mov");

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
            nv.Add("@Type", "Mov");


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
            nv.Add("@Type", "Mov");

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
            nv.Add("@Type", "Mov");

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
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Customer", !string.IsNullOrEmpty(Customer) ? Customer : "0");
            nv.Add("@JobNo", !string.IsNullOrEmpty(jobNo) ? jobNo : "0");
            nv.Add("@GenusCode", !string.IsNullOrEmpty(Code) ? Code : "0");
            nv.Add("@Mode", "1");
            nv.Add("@Type", "Mov");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);

            // ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataSource = dt;
            ddlBenchLocation.DataTextField = "BenchLocation";
            ddlBenchLocation.DataValueField = "BenchLocation";
            ddlBenchLocation.DataBind();
          //  ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));


        }

        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBenchLocation.SelectedIndex == -1)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(getBenchLocation(), ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            BindGridPlantReady(1);
        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBenchLocation.SelectedIndex == -1)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(getBenchLocation(), ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridPlantReady(1);
        }
        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindJobCode(ddlBenchLocation.SelectedValue);
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname(getBenchLocation(), "0", "0");
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(getBenchLocation(), "0", "0");
            if (ddlAssignedBy.SelectedIndex == 0)
                BindAssignByList(getBenchLocation(), "0", "0");
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridPlantReady(1);
        }
        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlBenchLocation.SelectedIndex == -1)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, "0", ddlCrop.SelectedValue);
            BindAssignByList("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridPlantReady(1);
        }
        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            BindBenchLocation(Session["Facility"].ToString(), txtSearchJobNo.Text, "0", "0");
            BindGridPlantReady(1);
        }
        protected void RadioButtonListSourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }
        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            RadioButtonListSourse.Items[0].Selected = false;
            RadioButtonListSourse.ClearSelection();

            BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
            Bindcname("0", "0", "0");
            // BindJobCode("0");
            BindJobCode("0", "0", "0");
            BindCrop();
            BindFacility();
            BindGridPlantReady(1);
        }

        public void BindFacility()
        {
            ddlToFacility.DataSource = objBAL.GetMainLocation();
            ddlToFacility.DataTextField = "Facility";
            ddlToFacility.DataValueField = "Facility";
            ddlToFacility.DataBind();
            ddlToFacility.Items.Insert(0, new ListItem("--- Select ---", "0"));
            ddlToFacility.SelectedValue = Session["Facility"].ToString();
            BindBench_Location();
        }

        public void BindBench_Location()
        {
            ddlToGreenHouse.DataSource = objBAL.GetLocation(ddlToFacility.SelectedValue);
            ddlToGreenHouse.DataTextField = "BenchName";
            ddlToGreenHouse.DataValueField = "BenchName";
            ddlToGreenHouse.DataBind();
            ddlToGreenHouse.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        protected void ddlToFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBench_Location();
        }

        protected void gvMoveReq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                BindFacility();
                userinput.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                HiddenFieldDid.Value = gvMoveReq.DataKeys[rowIndex].Values[1].ToString();
                HiddenFieldJid.Value = gvMoveReq.DataKeys[rowIndex].Values[2].ToString();
                lblJobID.Text = gvMoveReq.DataKeys[rowIndex].Values[3].ToString();
                lblBenchlocation.Text = gvMoveReq.DataKeys[rowIndex].Values[4].ToString();
                lblTotalTrays.Text = gvMoveReq.DataKeys[rowIndex].Values[5].ToString();
                lblDescription.Text = gvMoveReq.DataKeys[rowIndex].Values[6].ToString();

                TaskRequestKey = gvMoveReq.DataKeys[rowIndex].Values[7].ToString();
                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@MoveId", HiddenFieldDid.Value);

                dt = objCommon.GetDataTable("SP_GetMOVERequestView", nv);

                if (dt != null & dt.Rows.Count > 0)
                {
                    BindFacility();
                    txtMoveComments.Text = dt.Rows[0]["Comments"].ToString();
                    txtMoveNumberOfTrays.Text = dt.Rows[0]["TraysRequest"].ToString();
                    txtMoveDate.Text = Convert.ToDateTime(dt.Rows[0]["MoveDate"]).ToString("yyyy-MM-dd");
                    ddlToFacility.SelectedValue = dt.Rows[0]["FacilityTo"].ToString();
                    ddlToGreenHouse.SelectedValue = dt.Rows[0]["GrenHouseToRequest"].ToString();
                }
                ddlLogisticManager.Focus();
            }

            if (e.CommandName == "StartDump")
            {
                string ChId = "0";
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string MoveID = gvMoveReq.DataKeys[rowIndex].Values[1].ToString();
                TaskRequestKey = gvMoveReq.DataKeys[rowIndex].Values[7].ToString();
                //    Response.Redirect(String.Format("~/MoveCompletionStart.aspx?Did={0}&TaskRequestKey={2}", Did, TaskRequestKey));



                NameValueCollection nv = new NameValueCollection();
                nv.Add("@MoveDate", "");
                nv.Add("@Comments", "");
                nv.Add("@QuantityOfTray", "");
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@MoveID", MoveID);

                nv.Add("@OperatorID", Session["LoginID"].ToString());



                long result = objCommon.GetDataExecuteScaler("SP_AddMoveTaskAssignment", nv);





                Response.Redirect(String.Format("~/MoveTaskCompletion.aspx?Did={0}&Chid={1}&DrId={2}&IsF={3}&TaskRequestKey={4}", result, ChId, MoveID, 0, TaskRequestKey));


                //    Response.Redirect(String.Format("~/MoveTaskCompletion.aspx?PageType={0}&Did={1}&DrId={2}", "ManageTask", 0, dtR.Rows[0]["MoveID"].ToString()));
            }
        }

        protected void btnMoveSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();

            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Aid", ddlLogisticManager.SelectedValue);
            dt = objCommon.GetDataTable("spGeEmployeeRoleDetails", nv1);
            nv.Add("@SupervisorID", ddlLogisticManager.SelectedValue);
            nv.Add("@MoveNumberOfTrays", txtMoveNumberOfTrays.Text);

            nv.Add("@FromFacility", Session["LoginID"].ToString());
            nv.Add("@GrowerPutAwayID", "0");
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@ToFacility", ddlToFacility.SelectedValue);
            nv.Add("@ToGreenHouse", ddlToGreenHouse.SelectedValue);

            nv.Add("@MoveDate", txtMoveDate.Text);
            nv.Add("@Comments", txtMoveComments.Text);
            nv.Add("@mvoeId", HiddenFieldDid.Value);
            nv.Add("@RoleId", dt.Rows[0]["RoleID"].ToString());
            nv.Add("@ManualID", HiddenFieldJid.Value);
            nv.Add("@TaskRequestKey", TaskRequestKey);
            
            result = objCommon.GetDataInsertORUpdate("SP_AddMoveRequestASManua", nv);

            NameValueCollection nameValue = new NameValueCollection();
            nameValue.Add("@LoginID", Session["LoginID"].ToString());
            nameValue.Add("@jobcode", lblJobID.Text);
            nameValue.Add("@GreenHouseID", lblBenchlocation.Text);
            nameValue.Add("@TaskName", "Move");

            var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

            NameValueCollection nvn = new NameValueCollection();
            nvn.Add("@LoginID", Session["LoginID"].ToString());
            nvn.Add("@SupervisorID", ddlLogisticManager.SelectedValue);
            nvn.Add("@Jobcode", lblJobID.Text);
            nvn.Add("@TaskName", "Move");
            nvn.Add("@TaskRequestKey", "");
            nvn.Add("@GreenHouseID", lblBenchlocation.Text);
            var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);

            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                BAL_Classes.General objGeneral = new BAL_Classes.General();
                objGeneral.SendMessage(int.Parse(ddlLogisticManager.SelectedValue), "New Move Task Assigned", "New Move Task Assigned", "Move");

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
                //   string url = "MyTaskAssistantGrower.aspx";
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

            var res = (Master.FindControl("r1") as Repeater);

            var lblCount = (Master.FindControl("lblNotificationCount") as Label);
            objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), res, lblCount);

        }

        public void clear()
        {
            //  ddlSupervisor.SelectedIndex = 0;
        }

        protected void MoveReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskAssistantGrower.aspx");
        }

        protected void gvMoveReq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMoveReq.PageIndex = e.NewPageIndex;
            BindGridPlantReady(1);
        }

        protected void gvMoveReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGermDate = (Label)e.Row.FindControl("lblMoveDate");
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
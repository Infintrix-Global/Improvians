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
    public partial class PlantReadyAssignmentForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
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
                // txtToDate.Text = TDate;
                //  BindTaskType();
                BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
                BindJobCode("0", "0", "0");
                Bindcname("0", "0", "0");
                BindCrop();
                BindAssignByList("0", "0", "0");
                BindGridGerm("0", 0);
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

        //public void BindTaskType()
        //{

        //    NameValueCollection nv = new NameValueCollection();
        //    nv.Add("@ID", "0");
        //    RadioButtonListGno.DataSource = objCommon.GetDataTable("SP_GetFertilizerRequestDateCountNo", nv); ;
        //    RadioButtonListGno.DataTextField = "DateCountNoName";
        //    RadioButtonListGno.DataValueField = "DateCountNo";
        //    RadioButtonListGno.DataBind();
        //    RadioButtonListGno.Items.Insert(0, new ListItem("--Select--", "0"));


        //}

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
            nv.Add("@Type", "PR");

            dt = objCommon.GetDataTable("SP_TaskFilterSearchSupervisor", nv);
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
            nv.Add("@Type", "PR");


            dt = objCommon.GetDataTable("SP_TaskFilterSearchSupervisor", nv);

            ddlAssignedBy.DataSource = dt;
            ddlAssignedBy.DataTextField = "AssingTo";
            ddlAssignedBy.DataValueField = "AssingTo";
            ddlAssignedBy.DataBind();
            ddlAssignedBy.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlAssignedBy.Items.Insert(1, new ListItem("System", "System"));
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
            nv.Add("@Type", "PR");

            dt = objCommon.GetDataTable("SP_TaskFilterSearchSupervisor", nv);
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
            nv.Add("@Type", "PR");

            dt = objCommon.GetDataTable("SP_TaskFilterSearchSupervisor", nv);
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
            nv.Add("@Type", "PR");

            dt = objCommon.GetDataTable("SP_TaskFilterSearchSupervisor", nv);

            // ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataSource = dt;
            ddlBenchLocation.DataTextField = "BenchLocation";
            ddlBenchLocation.DataValueField = "BenchLocation";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));


        }

        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstJob.Clear();
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
            lstJob.Clear();
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
            lstJob.Clear();
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlBenchLocation.SelectedIndex == 0)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, "0", ddlCrop.SelectedValue);
            BindAssignByList("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }




        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lstJob.Clear();
            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstJob.Clear();
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

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstJob.Clear();
            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }

        protected void RadioButtonListSourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstJob.Clear();
            BindGridGerm("0", 1);
        }

        protected void RadioButtonListF_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstJob.Clear();
            BindGridGerm("0", 1);
        }
        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            lstJob.Clear();
            txtFromDate.Text = "";
            txtToDate.Text = "";
            BindBenchLocation(Session["Facility"].ToString(), txtSearchJobNo.Text, "0", "0");
            BindGridGerm(txtSearchJobNo.Text, 1);

        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            lstJob.Clear();
            RadioButtonListSourse.Items[0].Selected = false;
            RadioButtonListSourse.ClearSelection();
            // RadioButtonListGno.Items[0].Selected = false;
            // RadioButtonListGno.ClearSelection();
            BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
            Bindcname("0", "0", "0");
            // BindJobCode("0");
            BindJobCode("0", "0", "0");
            BindCrop();
            BindGridGerm("0", 1);

        }

        protected void txtBatchLocation_TextChanged(object sender, EventArgs e)
        {
            lstJob.Clear();
            txtFromDate.Text = "";
            txtToDate.Text = "";
            //BindJobCode(ddlBenchLocation.SelectedValue);
            Bindcname(ddlBenchLocation.SelectedValue, "0", "0");
            BindJobCode(ddlBenchLocation.SelectedValue, "0", "0");
            BindAssignByList(ddlBenchLocation.SelectedValue, "0", "0");
            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }


        public void BindGridGerm(string JobCode, int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@JobCode", JobCode);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@Crop", ddlCrop.SelectedValue);
            nv.Add("@Status", "");
            nv.Add("@Jobsource", RadioButtonListSourse.SelectedValue);
            nv.Add("@GermNo", "0");
            nv.Add("@FromDate", txtFromDate.Text);
            nv.Add("@ToDate", txtToDate.Text);
            nv.Add("@AssignedBy", ddlAssignedBy.SelectedValue);

            dt = objCommon.GetDataTable("SP_GetSupervisorPlantReadyTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();


            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }
        }
        private void highlight(int limit)
        {
            var i = gvGerm.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvGerm.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                var tKey = gvGerm.DataKeys[row.RowIndex].Values[2].ToString();
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

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string JobID = "";
            string ChId = "";
            string TaskID = "";
            if (e.CommandName == "Assign")
            {
                //int rowIndex = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = gvGerm.Rows[rowIndex];

                // string PRID = e.CommandArgument.ToString();
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string PRID = gvGerm.DataKeys[rowIndex].Values[0].ToString();
                ChId = gvGerm.DataKeys[rowIndex].Values[1].ToString();
                var TaskRequestKey = gvGerm.DataKeys[rowIndex].Values[2].ToString();
                if (ChId == "")
                {
                    ChId = "0";
                }
                else
                {
                    ChId = ChId;
                }
                Response.Redirect(String.Format("~/PlantReadyTaskAssignment.aspx?PRID={0}&Chid={1}&TaskRequestKey={2}", PRID, ChId, TaskRequestKey));

            }


            if (e.CommandName == "Select")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string PRID = gvGerm.DataKeys[rowIndex].Values[0].ToString();
                ChId = gvGerm.DataKeys[rowIndex].Values[1].ToString();
                var TaskRequestKey = gvGerm.DataKeys[rowIndex].Values[2].ToString();
                if (ChId == "")
                {
                    ChId = "0";
                }
                else
                {
                    ChId = ChId;
                }
                //NameValueCollection nv = new NameValueCollection();
                //nv.Add("@OperatorID", Session["LoginID"].ToString());
                //nv.Add("@Notes", "");
                //nv.Add("@JobID", JobID);
                //nv.Add("@LoginID", Session["LoginID"].ToString());
                //nv.Add("@CropId", "");
                //nv.Add("@UpdatedReadyDate", "");
                //nv.Add("@PlantExpirationDate", "");
                //nv.Add("@RootQuality", "");
                //nv.Add("@PlantHeight", "");
                //nv.Add("@wo", WO);
                //nv.Add("@mode", "4");

                //NameValueCollection nv = new NameValueCollection();
                //nv.Add("@OperatorID", Session["LoginID"].ToString());
                //nv.Add("@Notes", "");
                //nv.Add("@PRID", PRID);
                //nv.Add("@LoginID", Session["LoginID"].ToString());
                //nv.Add("@PlantExpirationDate", "");

                //long result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyTaskAssignmentNew", nv);

                //  int result = objCommon.GetDataInsertORUpdate("SP_AddPlantReadyTaskAssignment", nv);

                
                    Response.Redirect(String.Format("~/PlantReadyTaskCompletion.aspx?PRAID={0}&Chid={1}&PRID={2}&IsF={3}&TaskRequestKey={4}", 0, ChId, PRID, 0, TaskRequestKey));
                
            }
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm("0", 1);
        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lbljstatus = (Label)e.Row.FindControl("lbljstatus");
                Label lblTitla = (Label)e.Row.FindControl("lblTitla");

                if (lbljstatus.Text == "4")
                {
                    lblTitla.Text = "Plant Ready Request";
                }


            }
        }

        protected void gvGerm_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPlantReadyId = (Label)e.Row.FindControl("lblPlantReadyId");
                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");

                Job item = lstJob.Find(x => x.ID == Convert.ToInt32(lblPlantReadyId.Text));
                if (lstJob.Contains(item))
                    chkSelect.Checked = true;



                Button btnAssign = (Button)e.Row.FindControl("btnAssign");
                Button btnSelect = (Button)e.Row.FindControl("btnSelect");

                int RoleId = Convert.ToInt32(Session["Role"]);
                if (RoleId == 11 || RoleId == 3 || RoleId == 5)
                {
                    btnSelect.Visible = true;
                    btnAssign.Visible = false;
                }

                Label lblGermDate = (Label)e.Row.FindControl("lblSeededDate");
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

        public void BindOperatorList()
        {
            NameValueCollection nv = new NameValueCollection();
            DataTable dt = new DataTable();
            nv.Add("@RoleID", Session["Role"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetRoleForAssignementFacility", nv);

            ddlOperator.DataSource = dt;
            ddlOperator.DataTextField = "EmployeeName";
            ddlOperator.DataValueField = "ID";
            ddlOperator.DataBind();
            ddlOperator.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            BindOperatorList();
            AddDetails.Visible = true;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            foreach (Job item in lstJob)
            {
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@OperatorID", ddlOperator.SelectedValue);
                nv.Add("@Notes", txtPlantComments.Text);
                nv.Add("@PRID", item.ID.ToString());
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@TaskRequestKey", item.TaskRequestKey);

                result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyTaskAssignment", nv);

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
                var res = (Master.FindControl("r1") as Repeater);
                var lblCount = (Master.FindControl("lblNotificationCount") as Label);
                objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), res, lblCount);

                //lblmsg.Text = "Assignment Successful";
                clear();
                string message = "Assignment Successful";
                string url = "PlantReadyAssignmentForm.aspx";

                objCommon.ShowAlertAndRedirect(message, url);
            }
            else
            {
                lblmsg.Text = "Assignment Not Successful";
            }
        }

        public void clear()
        {
            ddlOperator.SelectedIndex = 0;
            txtPlantComments.Text = "";


        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/PlantReadyAssignmentForm.aspx");
        }
        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelect = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkSelect.NamingContainer;
            if (row != null)
            {
                Label lID = (Label)row.FindControl("lblPlantReadyId");

                Label lblBenchLocation = (Label)row.FindControl("lblGreenHouseID");

                Label lblTaskRequestKey = (Label)row.FindControl("lblTaskRequestKey");
                Label lbljobcode = (Label)row.FindControl("lbljobID");
                if (chkSelect.Checked)
                {
                    lstJob.Add(new Job { ID = Convert.ToInt32(lID.Text), TaskRequestKey = lblTaskRequestKey.Text, GreenHouseID = lblBenchLocation.Text, jobcode = lbljobcode.Text });
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
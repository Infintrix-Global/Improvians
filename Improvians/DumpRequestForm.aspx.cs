﻿using System;
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
    public partial class DumpRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Fdate = "", TDate = "";
                Fdate = Convert.ToDateTime(System.DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd");
                TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(14).ToString("yyyy-MM-dd");

              //  txtFromDate.Text = Fdate;
              //  txtToDate.Text = TDate;

                //Bindcname();
                //BindBenchLocation(Session["Facility"].ToString());
                //BindJobCode(ddlBenchLocation.SelectedValue);

                BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
                BindJobCode("0", "0", "0");
                Bindcname("0", "0", "0");
                BindCrop();
                BindAssignByList("0", "0", "0");
                // BindFacility();
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
            nv.Add("@Type", "Dum");

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
            nv.Add("@Type", "Dum");


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
            nv.Add("@Type", "Dum");

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
            nv.Add("@Type", "Dum");

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
            nv.Add("@Type", "Dum");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);

            // ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataSource = dt;
            ddlBenchLocation.DataTextField = "BenchLocation";
            ddlBenchLocation.DataValueField = "BenchLocation";
            ddlBenchLocation.DataBind();
         //   ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));


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
            BindGridPlantReady("0", 1);
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
            BindGridPlantReady("0", 1);
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
            BindGridPlantReady(ddlJobNo.SelectedValue, 1);
        }




        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridPlantReady(ddlJobNo.SelectedValue, 1);
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
            BindGridPlantReady(ddlJobNo.SelectedValue, 1);
        }

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(ddlJobNo.SelectedValue, 1);
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
            BindGridPlantReady(txtSearchJobNo.Text, 1);

        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            //  RadioButtonListSourse.Items[0].Selected = false;
            //  RadioButtonListSourse.ClearSelection();

            BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
            Bindcname("0", "0", "0");
            // BindJobCode("0");
            BindJobCode("0", "0", "0");
            BindCrop();
            BindGridPlantReady("0", 1);

        }

        protected void txtBatchLocation_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            //BindJobCode(ddlBenchLocation.SelectedValue);
            Bindcname(getBenchLocation(), "0", "0");
            BindJobCode(getBenchLocation(), "0", "0");
            BindAssignByList(getBenchLocation(), "0", "0");
            BindGridPlantReady(ddlJobNo.SelectedValue, 1);
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



        public void BindGridPlantReady(string JobCode, int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@wo", "");
            nv.Add("@JobCode", JobCode);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Crop", ddlCrop.SelectedValue);
            nv.Add("@Status", "");
            nv.Add("@Jobsource", "0");
            nv.Add("@LoginId", Session["LoginID"].ToString());
            nv.Add("@FromDate", txtFromDate.Text);
            nv.Add("@ToDate", txtToDate.Text);
            nv.Add("@AssignedBy", ddlAssignedBy.SelectedValue);

            dt = objCommon.GetDataTable("SP_GetDumpRequestAssistantGrower", nv);

            gvPlantReady.DataSource = dt;
            gvPlantReady.DataBind();

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
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblBenchLoc") as Label).Text;
                var tKey = gvPlantReady.DataKeys[row.RowIndex].Values[5].ToString();
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
            //NameValueCollection nv = new NameValueCollection();

            //DataTable dt = new DataTable();

            //if (Session["Role"].ToString() == "12" || Session["Role"].ToString() == "1")
            //{

            //    dt = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
            //}
            //else if (Session["Role"].ToString() == "2")
            //{
            //    dt = objCommon.GetDataTable("SP_GetRoleForSupervisor", nv);
            //}
            //else
            //{
            //    // dt = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
            //}
            NameValueCollection nv = new NameValueCollection();
            DataTable dt = new DataTable();
            nv.Add("@RoleID", Session["Role"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetRoleForAssignementFacility", nv); ;


            ddlDumptAssignment.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlDumptAssignment.DataTextField = "EmployeeName";
            ddlDumptAssignment.DataValueField = "ID";
            ddlDumptAssignment.DataBind();
            ddlDumptAssignment.Items.Insert(0, new ListItem("--Select--", "0"));

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

        protected void gvPlantReady_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                HiddenFieldDid.Value = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();
                HiddenFieldJid.Value = gvPlantReady.DataKeys[rowIndex].Values[2].ToString();
                ViewState["jobcode"] = gvPlantReady.DataKeys[rowIndex].Values[3].ToString();
                ViewState["benchloc"] = gvPlantReady.DataKeys[rowIndex].Values[4].ToString();
                TaskRequestKey = gvPlantReady.DataKeys[rowIndex].Values[5].ToString();

                GrowerPutAwayId.Value = gvPlantReady.DataKeys[rowIndex].Values[6].ToString();
                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@DumpId", HiddenFieldDid.Value);

                dt = objCommon.GetDataTable("SP_GetDumpRequestView", nv);

                if (dt != null & dt.Rows.Count > 0)
                {
                    txtCommentsDump.Text = dt.Rows[0]["Comments"].ToString();
                    txtQuantityofTray.Text = dt.Rows[0]["QuantityOfTray"].ToString();
                    txtDumpDate.Text = Convert.ToDateTime(dt.Rows[0]["DumpDateR"]).ToString("yyyy-MM-dd");
                }
                //ddlSupervisor.Focus();
            }

            if (e.CommandName == "StartDump")
            {
                string ChId = "";
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string Did = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();
                //  ChId = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();
                TaskRequestKey = gvPlantReady.DataKeys[rowIndex].Values[5].ToString();

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
                nv.Add("@DumpId", Did);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@QuantityOfTray", "");
                nv.Add("@DumpDate", "");

                long result = objCommon.GetDataExecuteScaler("SP_AddDumpTaskStart", nv);


                if (result > 0)
                {
                    Response.Redirect(String.Format("~/DumpTaskCompletion.aspx?Did={0}&Chid={1}&DrId={2}&TaskRequestKey={3}&IsF={4}", result, ChId, Did, TaskRequestKey,0));
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();

            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Aid", ddlDumptAssignment.SelectedValue);
            dt = objCommon.GetDataTable("spGeEmployeeRoleDetails", nv1);

            nv.Add("@SupervisorID", ddlDumptAssignment.SelectedValue);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Did", HiddenFieldDid.Value);
            nv.Add("@Comments", txtCommentsDump.Text);
            nv.Add("@wo", GrowerPutAwayId.Value);
            nv.Add("@ManualID", HiddenFieldJid.Value);
            nv.Add("@DumpDate", txtDumpDate.Text);
            nv.Add("@QuantityOfTray", txtQuantityofTray.Text);
            nv.Add("@RoleId", dt.Rows[0]["RoleID"].ToString());

            nv.Add("@jobcode", ViewState["jobcode"].ToString());
            nv.Add("@GreenHouseID", ViewState["benchloc"].ToString());
            nv.Add("@TaskRequestKey", TaskRequestKey);
            result = objCommon.GetDataInsertORUpdate("SP_AddDumpRequestManua", nv);

            NameValueCollection nameValue = new NameValueCollection();
            nameValue.Add("@LoginID", Session["LoginID"].ToString());
            nameValue.Add("@jobcode", ViewState["jobcode"].ToString());
            nameValue.Add("@GreenHouseID", ViewState["benchloc"].ToString());
            nameValue.Add("@TaskName", "Dump");

            var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);

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

        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlantReady.PageIndex = e.NewPageIndex;
            BindGridPlantReady("0", 1);
        }

        protected void gvPlantReady_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGermDate = (Label)e.Row.FindControl("lblDumpdate");
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
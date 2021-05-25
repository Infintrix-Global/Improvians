﻿using Evo.Bal;
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
    public partial class IrrigationRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Fdate = "", TDate = "";
                Fdate = Convert.ToDateTime(System.DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd");
                TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(14).ToString("yyyy-MM-dd");

                txtFromDate.Text = Fdate;
                txtToDate.Text = TDate;
                BindTaskType();
                BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
                BindJobCode("0", "0", "0");
                Bindcname("0", "0", "0");
                BindCrop();
                BindAssignByList("0", "0", "0");
                BindGridIrrigation("0", 0);
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
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@JobNo", ddlJobNo.SelectedValue);
            nv.Add("@GenusCode", "0");
            nv.Add("@Mode", "6");
            nv.Add("@Type", "Irr");

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
            nv.Add("@Type", "Irr");


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
            nv.Add("@Type", "Irr");

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
            nv.Add("@Type", "Irr");

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
            nv.Add("@Type", "Irr");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);

            // ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataSource = dt;
            ddlBenchLocation.DataTextField = "BenchLocation";
            ddlBenchLocation.DataValueField = "BenchLocation";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));


        }

        public void BindTaskType()
        {

            NameValueCollection nv = new NameValueCollection();
            nv.Add("@ID", "0");
            RadioButtonListGno.DataSource = objCommon.GetDataTable("SP_GetIrrigationRequestDateCountNo", nv); ;
            RadioButtonListGno.DataTextField = "DateCountNoName";
            RadioButtonListGno.DataValueField = "DateCountNo";
            RadioButtonListGno.DataBind();
            RadioButtonListGno.Items.Insert(0, new ListItem("--Select--", "0"));


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
            BindGridIrrigation("0", 1);
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
            BindGridIrrigation("0", 1);
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
            BindGridIrrigation(ddlJobNo.SelectedValue, 1);
        }




        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridIrrigation(ddlJobNo.SelectedValue, 1);
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
            BindGridIrrigation(ddlJobNo.SelectedValue, 1);
        }

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation(ddlJobNo.SelectedValue, 1);
        }

        protected void RadioButtonListSourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation("0", 1);
        }

        protected void RadioButtonListF_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation("0", 1);
        }
        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            BindBenchLocation(Session["Facility"].ToString(), txtSearchJobNo.Text, "0", "0");
            BindGridIrrigation(txtSearchJobNo.Text, 1);

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
            BindGridIrrigation("0", 1);

        }

        protected void txtBatchLocation_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            //BindJobCode(ddlBenchLocation.SelectedValue);
            Bindcname(ddlBenchLocation.SelectedValue, "0", "0");
            BindJobCode(ddlBenchLocation.SelectedValue, "0", "0");
            BindAssignByList(ddlBenchLocation.SelectedValue, "0", "0");
            BindGridIrrigation(ddlJobNo.SelectedValue, 1);
        }




        public void BindGridIrrigation(string JobCode, int p)
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


            if (Session["Role"].ToString() == "12")
            {
                dt = objCommon.GetDataTable("SP_GetIrrigationRequestAssistantGrower", nv);
            }
            else
            {
                dt = objCommon.GetDataTable("SP_GetIrrigationRequest", nv);
            }
            GridIrrigation.DataSource = dt;
            GridIrrigation.DataBind();

            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }


        }
        private void highlight(int limit)
        {
            var i = GridIrrigation.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                var tKey = GridIrrigation.DataKeys[row.RowIndex].Values[4].ToString();
                i--;
                if (checkJob == JobCode && checklocation == benchLoc && tKey == TaskKey)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    GridIrrigation.PageIndex++;
                    GridIrrigation.DataBind();
                    highlight((limit - 10));
                }
            }
        }

        public void BindSupervisorList()
        {
            //NameValueCollection nv = new NameValueCollection();
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            //ddlSupervisor.DataTextField = "EmployeeName";
            //ddlSupervisor.DataValueField = "ID";
            //ddlSupervisor.DataBind();
            //ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

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


        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            //RadioButtonListSourse.Items[0].Selected = false;


            //RadioButtonListSourse.ClearSelection();
            //Bindcname();
            //BindBenchLocation(Session["Facility"].ToString());
            //BindJobCode(ddlBenchLocation.SelectedValue);
            //BindGridIrrigation(1);
        }
        protected void GridIrrigation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                // string rowIndex = e.CommandArgument.ToString();

                // wo = rowIndex;
                //DataTable dt = new DataTable();
                //NameValueCollection nv = new NameValueCollection();
                //nv.Add("@wo", wo);
                //nv.Add("@JobCode", ddlJobNo.SelectedValue);
                //nv.Add("@CustomerName", ddlCustomer.SelectedValue);
                //nv.Add("@Facility", ddlFacility.SelectedValue);
                //nv.Add("@Mode", "2");
                //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
                // txtIrrigatedNoTrays.Text = dt.Rows[0]["trays_actual"].ToString();
                //txtIrrigationDuration.Text = dt.Rows[0]["jobcode"].ToString();
                //  lblJobID.Text = dt.Rows[0]["jobcode"].ToString();

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                //  lblJobID.Text = GridIrrigation.DataKeys[rowIndex].Values[1].ToString();
                // lblGrowerID.Text= GridIrrigation.DataKeys[rowIndex].Values[2].ToString();

                txtNotes.Focus();
            }

            if (e.CommandName == "Job")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string BatchLocation = GridIrrigation.DataKeys[rowIndex].Values[0].ToString();
                string jobCode = GridIrrigation.DataKeys[rowIndex].Values[1].ToString();
                string IrrigationCode = GridIrrigation.DataKeys[rowIndex].Values[3].ToString();
                string TaskRequestKey = GridIrrigation.DataKeys[rowIndex].Values[4].ToString();
                Response.Redirect(String.Format("~/IrrJobBuildUp.aspx?Bench={0}&jobCode={1}&ICode={2}&TaskRequestKey={3}", BatchLocation, jobCode, IrrigationCode, TaskRequestKey));
            }

            if (e.CommandName == "GStart")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string BatchLocation = GridIrrigation.DataKeys[rowIndex].Values[0].ToString();
                string jobCode = GridIrrigation.DataKeys[rowIndex].Values[1].ToString();
                string IrrigationCode = GridIrrigation.DataKeys[rowIndex].Values[3].ToString();
                string TaskRequestKey = GridIrrigation.DataKeys[rowIndex].Values[4].ToString();
                Response.Redirect(String.Format("~/IrrigationStart.aspx?Bench={0}&jobCode={1}&ICode={2}&TaskRequestKey={3}", BatchLocation, jobCode, IrrigationCode, TaskRequestKey));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int IrrigationCode = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Mode", "13");
            dt = objCommon.GetDataTable("GET_Common", nv1);
            IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);


            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);

                    //  nv.Add("@GrowerPutAwayId", lblGrowerID.Text);
                    nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    nv.Add("@IrrigatedNoTrays", (row.FindControl("lbltotTray") as Label).Text);
                    // nv.Add("@IrrigatedNoTrays", txtIrrigatedNoTrays.Text.Trim());
                    nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                    nv.Add("@IrrigationDuration", "");
                    nv.Add("@SprayDate", txtSprayDate.Text.Trim());
                    nv.Add("@SprayTime", "");
                    nv.Add("@Nots", txtNotes.Text.Trim());
                    nv.Add("@IrrigationCode", IrrigationCode.ToString());
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@NoOfPasses", "");
                    nv.Add("@ResetSprayTaskForDays", "");

                    result = objCommon.GetDataInsertORUpdate("SP_AddIrrigationRequest", nv);

                    var txtJobNo = (row.FindControl("lbljobID") as Label).Text;
                    var txtBenchLocation = (row.FindControl("lblGreenHouseID") as Label).Text;

                    NameValueCollection nameValue = new NameValueCollection();
                    nameValue.Add("@LoginID", Session["LoginID"].ToString());
                    nameValue.Add("@jobcode", txtJobNo);
                    nameValue.Add("@GreenHouseID", txtBenchLocation);
                    nameValue.Add("@TaskName", "Irrigation");

                    var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

                    if (result > 0)
                    {
                        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                        //  lblmsg.Text = "Assignment Not Successful";
                    }
                }
            }

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

            var res = (Master.FindControl("r1") as Repeater);

            var lblCount = (Master.FindControl("lblNotificationCount") as Label);
            objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), res, lblCount);
        }

        public void clear()
        {
            ddlSupervisor.SelectedIndex = 0;
            txtWaterRequired.Text = "";
            txtNotes.Text = "";
            //  txtIrrigatedNoTrays.Text = "";
            //txtIrrigationDuration.Text = "";
            txtSprayDate.Text = "";
            // txtSprayTime.Text = "";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }

        protected void GridIrrigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridIrrigation.PageIndex = e.NewPageIndex;
            BindGridIrrigation("0", 1);
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            userinput.Visible = true;
            ddlSupervisor.Focus();
            int tray = 0;
            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    tray = tray + Convert.ToInt32((row.FindControl("lbltotTray") as Label).Text);
                }
            }
        }

        protected void chckchanged(object sender, EventArgs e)
        {
            CheckBox chckheader = (CheckBox)GridIrrigation.HeaderRow.FindControl("CheckBoxall");
            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckheader.Checked == true)
                {
                    chckrw.Checked = true;
                }
                else
                {
                    chckrw.Checked = false;
                }
            }
        }

        protected void btnManual_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/IrrigationReqManual.aspx");
        }



        protected void GridIrrigation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblsource = (Label)e.Row.FindControl("lblsource");
                if (lblsource.Text != "")
                {
                    if (lblsource.Text == "Manual")
                    {
                        lblsource.Text = "Navision";
                    }
                }

                Label lblGermDate = (Label)e.Row.FindControl("lblIrrDate");
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
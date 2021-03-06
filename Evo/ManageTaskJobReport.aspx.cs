﻿using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class ManageTaskJobReport : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        public static DataTable AllData = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindSupervisorList("0", "0", "0");
                BindTaskRequestTypeList("0", "0", "0");
                BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
                BindJobCode("0", "0", "0");
                Bindcname("0", "0", "0");
                BindCrop("0", "0", "0");
                BindGridGerm("0");


            }
        }

        public void BindTaskRequestTypeList(string ddlBench, string jobNo, string Cust)
        {
            //  NameValueCollection nv = new NameValueCollection();

            //   ddlAssignedBy.DataSource = objCommon.GetDataTable("SP_GetSeedsRoles", nv);
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBench);
            nv.Add("@Customer", Cust);
            nv.Add("@JobNo", jobNo);
            nv.Add("@GenusCode", "0");
            nv.Add("@Mode", "5");


            dt = objCommon.GetDataTable("GetManageTaskJobHistorySearch", nv);
            ddlTaskRequestType.DataSource = dt;
            ddlTaskRequestType.DataTextField = "TaskRequestType";
            ddlTaskRequestType.DataValueField = "TaskRequestType";
            ddlTaskRequestType.DataBind();
            ddlTaskRequestType.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        public void BindSupervisorList(string ddlBench, string jobNo, string Cust)
        {
            //  NameValueCollection nv = new NameValueCollection();

            //   ddlAssignedBy.DataSource = objCommon.GetDataTable("SP_GetSeedsRoles", nv);
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBench);
            nv.Add("@Customer", Cust);
            nv.Add("@JobNo", jobNo);
            nv.Add("@GenusCode", "0");
            nv.Add("@Mode", "4");

            dt = objCommon.GetDataTable("GetManageTaskJobHistorySearch", nv);
            ddlAssignedBy.DataSource = dt;
            ddlAssignedBy.DataTextField = "AssingTo";
            ddlAssignedBy.DataValueField = "AssingTo";
            ddlAssignedBy.DataBind();
            ddlAssignedBy.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void Bindcname(string ddlBench, string jobNo, string Core)
        {

            //DataTable dt = new DataTable();
            //NameValueCollection nv = new NameValueCollection();

            //nv.Add("@Mode", "8");
            //dt = objCommon.GetDataTable("GET_Common", nv);
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBench);
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@JobNo", jobNo);
            nv.Add("@GenusCode", Core);
            nv.Add("@Mode", "3");


            dt = objCommon.GetDataTable("GetManageTaskJobHistorySearch", nv);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "Customer";
            ddlCustomer.DataValueField = "Customer";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindJobCode(string ddlBench, string Customer, string Core)
        {
            //  ddlJobNo.Items[0].Selected = false;
            ddlJobNo.ClearSelection();
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBench);
            nv.Add("@Customer", Customer);
            nv.Add("@JobNo", ddlJobNo.SelectedValue);
            nv.Add("@GenusCode", Core);

            nv.Add("@Mode", "2");


            dt = objCommon.GetDataTable("GetManageTaskJobHistorySearch", nv);
            //   ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataBind();

            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindBenchLocation(string ddlMain, string jobNo, string Customer, string Core)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@Customer", Customer);
            nv.Add("@JobNo", jobNo);
            nv.Add("@GenusCode", Core);
            nv.Add("@Mode", "1");

            dt = objCommon.GetDataTable("GetManageTaskJobHistorySearch", nv);

            // ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataSource = dt;
            ddlBenchLocation.DataTextField = "BenchLocation";
            ddlBenchLocation.DataValueField = "BenchLocation";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));


        }

        public void BindCrop(string ddlMain, string jobNo, string Customer)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlMain);
            nv.Add("@Customer", Customer);
            nv.Add("@JobNo", jobNo);
            nv.Add("@GenusCode", ddlCrop.SelectedValue);
            nv.Add("@Mode", "6");

            dt = objCommon.GetDataTable("GetManageTaskJobHistorySearch", nv);

            // ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlCrop.DataSource = dt;
            ddlCrop.DataTextField = "GenusCode";
            ddlCrop.DataValueField = "GenusCode";
            ddlCrop.DataBind();
            ddlCrop.Items.Insert(0, new ListItem("--- Select ---", "0"));


        }

        public void BindGridGerm(string JobNo)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@JobNo", JobNo);
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@RequestType", ddlTaskRequestType.SelectedValue);

            nv.Add("@AssingTo", ddlAssignedBy.SelectedValue);
            nv.Add("@WorkDateForm", txtFromDate.Text);
            nv.Add("@WorkDateTo", txtToDate.Text);
            nv.Add("@GenusCode", ddlCrop.SelectedValue);

            //if (Session["Role"].ToString() == "2")
            //{

            //    AllData = objCommon.GetDataTable("GetManageTaskJobSupervisorHistory", nv);
            //}
            //else
            //{
                AllData = objCommon.GetDataTable("GetManageTaskJobHistory", nv);
           // }


            gvGerm.DataSource = AllData;
            gvGerm.DataBind();

        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm("0");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string JNo = "";
            if (txtSearchJobNo.Text != "")
            {
                JNo = txtSearchJobNo.Text;
            }
            else
            {
                JNo = ddlJobNo.SelectedValue;
            }
            BindGridGerm(JNo);
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtSearchJobNo.Text = "";
            Bindcname("0", "0", "0");
            BindSupervisorList("0", "0", "0");
            BindTaskRequestTypeList("0", "0", "0");
            BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
            BindJobCode("0", "0", "0");
            BindCrop("0", "0", "0");
            BindGridGerm("0");
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bindcname(ddlBenchLocation.SelectedValue, "0", "0");
            BindJobCode(ddlBenchLocation.SelectedValue, "0", "0");
            BindSupervisorList(ddlBenchLocation.SelectedValue, "0", "0");
            BindTaskRequestTypeList(ddlBenchLocation.SelectedValue, "0", "0");
            BindCrop(ddlBenchLocation.SelectedValue, "0", "0");
            BindGridGerm("0");
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bindcname("0", ddlJobNo.SelectedValue, "0");
            BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, "0", "0");
            BindSupervisorList("0", ddlJobNo.SelectedValue, "0");
            BindTaskRequestTypeList("0", ddlJobNo.SelectedValue, "0");
            BindCrop("0", ddlJobNo.SelectedValue, "0");
            BindGridGerm(ddlJobNo.SelectedValue);

        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBenchLocation(Session["Facility"].ToString(), "0", ddlCustomer.SelectedValue, "0");
            BindJobCode("0", ddlCustomer.SelectedValue, "0");
            BindSupervisorList("0", "0", ddlCustomer.SelectedValue);
            BindTaskRequestTypeList("0", "0", ddlCustomer.SelectedValue);
            BindCrop("0", "0", ddlCustomer.SelectedValue);
            BindGridGerm("");
        }

        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bindcname("0", "0", ddlCrop.SelectedValue);
            BindBenchLocation(Session["Facility"].ToString(), "0", "0", ddlCrop.SelectedValue);
            BindJobCode("0", "0", ddlCrop.SelectedValue);
            BindSupervisorList("0", "0", "0");
            BindTaskRequestTypeList("0", "0", ddlCustomer.SelectedValue);
            BindGridGerm("0");
        }

        protected void ddlTaskRequestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm("0");
        }

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm("0");
        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "GStart")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string BatchLocation = gvGerm.DataKeys[rowIndex].Values[0].ToString();
                string jobCode = gvGerm.DataKeys[rowIndex].Values[1].ToString();
                string TaskRequestType = gvGerm.DataKeys[rowIndex].Values[2].ToString();
                string TaskRequestKey = gvGerm.DataKeys[rowIndex].Values[3].ToString();

                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@BenchLocation", BatchLocation);
                nv.Add("@JobNo", jobCode);
                nv.Add("@RequestType", TaskRequestType);
                nv.Add("@TaskRequestKey", TaskRequestKey);
                dt = objCommon.GetDataTable("GetManageTaskJobHistoryjobViewDetsils", nv);


                DataTable dtR = new DataTable();
                NameValueCollection nvR = new NameValueCollection();
                nvR.Add("@BenchLocation", BatchLocation);
                nvR.Add("@JobNo", jobCode);
                nvR.Add("@RequestType", TaskRequestType);
                nvR.Add("@TaskRequestKey", TaskRequestKey);
                dtR = objCommon.GetDataTable("GetManageTaskJobHistoryjobViewDetsilsRequest", nvR);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (TaskRequestType == "Fertilization")
                    {
                        Response.Redirect(String.Format("~/SprayTaskViewDetails.aspx?PageType={0}&FertilizationCode={1}&FCID={2}&TaskRequestKey={3}", "ManageTask", dt.Rows[0]["FertilizationCode"].ToString(), dt.Rows[0]["SprayId"].ToString(), dt.Rows[0]["TaskRequestKey"].ToString()));
                    }
                    if (TaskRequestType == "Chemical")
                    {
                        Response.Redirect(String.Format("~/ChemicalTaskViewDetails.aspx?PageType={0}&ChemicalCode={1}&CCID={2}&TaskRequestKey={3}", "ManageTask", dt.Rows[0]["ChemicalCode"].ToString(), dt.Rows[0]["ChemicalCompletionId"].ToString(), dt.Rows[0]["TaskRequestKey"].ToString()));
                    }
                    if (TaskRequestType == "Germination")
                    {

                        Response.Redirect(String.Format("~/GreenHouseTaskCompletion.aspx?PageType={0}&GTAID={1}&GTRID={2}&IsF={3}&TaskRequestKey={4}", "ManageTask", dt.Rows[0]["ID"].ToString(), dt.Rows[0]["GTRID"].ToString(), 1, dt.Rows[0]["TaskRequestKey"].ToString()));
                    }
                    if (TaskRequestType == "Irrigation")
                    {
                        Response.Redirect(String.Format("~/IrrigationTaskViewDetails.aspx?PageType={0}&IrrigationCode={1}&ICID={2}&TaskRequestKey={3}", "ManageTask", dt.Rows[0]["IrrigationCode"].ToString(), dt.Rows[0]["IrrigationTaskAssignmentId"].ToString(), dt.Rows[0]["TaskRequestKey"].ToString()));

                    }


                    if (TaskRequestType == "Plant Ready")
                    {

                        Response.Redirect(String.Format("~/PlantReadyTaskCompletion.aspx?PageType={0}&PRAID={1}&PRID={2}&IsF={3}&TaskRequestKey={4}", "ManageTask", dt.Rows[0]["PlantReadyTaskAssignmentId"].ToString(), dt.Rows[0]["PRID"].ToString(),1,dt.Rows[0]["TaskRequestKey"].ToString()));
                    }

                    if (TaskRequestType == "Dump")
                    {
                        Response.Redirect(String.Format("~/DumpTaskCompletion.aspx?PageType={0}&Did={1}&Chid={2}&DrId={3}&TaskRequestKey={4}&IsF={5}", "ManageTask", dt.Rows[0]["DumpTaskAssignmentId"].ToString(), 0, dt.Rows[0]["DumpId"].ToString(), dt.Rows[0]["TaskRequestKey"].ToString(),1));

                    }
                    if (TaskRequestType == "Move")
                    {

                        Response.Redirect(String.Format("~/MoveTaskCompletion.aspx?PageType={0}&Did={1}&DrId={2}&IsF={3}&TaskRequestKey={4}", "ManageTask", dt.Rows[0]["MoveTaskAssignmentId"].ToString(), dt.Rows[0]["MoveID"].ToString(), 1, dt.Rows[0]["TaskRequestKey"].ToString()));

                    }
                    if (TaskRequestType == "GeneralTask")
                    {
                        Response.Redirect(String.Format("~/GeneralTaskCompletion.aspx?PageType={0}&Did={1}&Chid={2}&DrId={3}&IsF={4}&TaskRequestKey={5}", "ManageTask", dt.Rows[0]["GeneralTaskAssignmentId"].ToString(), 0, dt.Rows[0]["GeneralId"].ToString(),1, dt.Rows[0]["TaskRequestKey"].ToString()));

                    }
                    if (TaskRequestType == "Crop Health Report")
                    {

                        Response.Redirect(String.Format("~/CropHealthReportView.aspx?PageType={0}&Did={1}&Chid={2}&DrId={3}&TaskRequestKey={4}", "ManageTask", dt.Rows[0]["CropHealthReportTaskAssignmentId"].ToString(), dt.Rows[0]["chid"].ToString(), dt.Rows[0]["CropHealthReportId"].ToString(), dt.Rows[0]["TaskRequestKey"].ToString()));
                        
                    }

                    if (TaskRequestType == "Put Away")
                    {

                        Response.Redirect(String.Format("~/ViewPutAway.aspx?PageType={0}&GrowerPutAwayId={1}", "ManageTask", dt.Rows[0]["GrowerPutAwayId"].ToString()));

                    }
                }
                else if (dtR != null && dtR.Rows.Count > 0)
                {
                    if (TaskRequestType == "Fertilization")
                    {
                        Response.Redirect(String.Format("~/SprayTaskViewDetails.aspx?PageType={0}&FertilizationCode={1}&FCID={2}&TaskRequestKey={3}", "ManageTask", dtR.Rows[0]["FertilizationCode"].ToString(), 0, dtR.Rows[0]["TaskRequestKey"].ToString()));

                    }
                    if (TaskRequestType == "Chemical")
                    {
                        Response.Redirect(String.Format("~/ChemicalTaskViewDetails.aspx?PageType={0}&ChemicalCode={1}&CCID={2}&TaskRequestKey={3}", "ManageTask", dtR.Rows[0]["ChemicalCode"].ToString(), 0, dtR.Rows[0]["TaskRequestKey"].ToString()));

                    }
                    if (TaskRequestType == "Germination")
                    {
                        Response.Redirect(String.Format("~/GreenHouseTaskCompletion.aspx?PageType={0}&GTAID={1}&GTRID={2}&IsF={3}&TaskRequestKey={4}", "ManageTask", 0, dtR.Rows[0]["ID"].ToString(), 1, dtR.Rows[0]["TaskRequestKey"].ToString()));
                    }
                    if (TaskRequestType == "Irrigation")
                    {
                        Response.Redirect(String.Format("~/IrrigationTaskViewDetails.aspx?PageType={0}&IrrigationCode={1}&ICID={2}&TaskRequestKey={3}", "ManageTask", dtR.Rows[0]["IrrigationCode"].ToString(), 0, dtR.Rows[0]["TaskRequestKey"].ToString()));

                    }


                    if (TaskRequestType == "Plant Ready")
                    {

                        Response.Redirect(String.Format("~/PlantReadyTaskCompletion.aspx?PageType={0}&PRAID={1}&PRID={2}&IsF={3}&TaskRequestKey={4}", "ManageTask", 0, dtR.Rows[0]["PlantReadyId"].ToString(),1, dtR.Rows[0]["TaskRequestKey"].ToString()));
                    }

                    if (TaskRequestType == "Dump")
                    {
                        Response.Redirect(String.Format("~/DumpTaskCompletion.aspx?PageType={0}&Did={1}&Chid={2}&DrId={3}&TaskRequestKey={4}&IsF={5}", "ManageTask", 0, 0, dtR.Rows[0]["DumpId"].ToString(), dtR.Rows[0]["TaskRequestKey"].ToString(),1));

                    }
                    if (TaskRequestType == "Move")
                    {

                        Response.Redirect(String.Format("~/MoveTaskCompletion.aspx?PageType={0}&Did={1}&DrId={2}&IsF={3}&TaskRequestKey={4}", "ManageTask", 0, dtR.Rows[0]["ID"].ToString(), 1, dtR.Rows[0]["TaskRequestKey"].ToString()));

                    }
                    if (TaskRequestType == "GeneralTask")
                    {
                        Response.Redirect(String.Format("~/GeneralTaskCompletion.aspx?PageType={0}&Did={1}&Chid={2}&DrId={3}&IsF={4}&TaskRequestKey={5}", "ManageTask", 0, 0, dtR.Rows[0]["ID"].ToString(),1, dtR.Rows[0]["TaskRequestKey"].ToString()));

                    }
                    if (TaskRequestType == "Crop Health Report")
                    {

                        Response.Redirect(String.Format("~/CropHealthReportView.aspx?PageType={0}&Did={1}&Chid={2}&DrId={3}&TaskRequestKey={4}", "ManageTask", 0, 0, dtR.Rows[0]["CropHealthReportId"].ToString(), dtR.Rows[0]["TaskRequestKey"].ToString()));

                    }


                    if (TaskRequestType == "Put Away")
                    {

                        Response.Redirect(String.Format("~/ViewPutAway.aspx?PageType={0}&GrowerPutAwayId={1}", "ManageTask", dtR.Rows[0]["GrowerPutAwayId"].ToString()));

                    }
                }
                else
                {

                }
                //    Response.Redirect(String.Format("~/ViewJobDetails.aspx?Bench={0}&jobCode={1}&CCode={2}&TaskRequestType={2}", BatchLocation, jobCode, TaskRequestType));


            }
        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnStart = (Button)e.Row.FindControl("btnStart");
                Label lblTaskStatus = (Label)e.Row.FindControl("lblTaskStatus");
                Label lblBenchLocation = (Label)e.Row.FindControl("lblBenchLocation");
                Label lblJobNo = (Label)e.Row.FindControl("lblJobNo");
                Label lblTaskRequestType = (Label)e.Row.FindControl("lblTaskRequestType");
                Label lblTaskRequestKey = (Label)e.Row.FindControl("lblTaskRequestKey");



                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@BenchLocation", lblBenchLocation.Text);
                nv.Add("@JobNo", lblJobNo.Text);
                nv.Add("@RequestType", lblTaskRequestType.Text);
                nv.Add("@TaskRequestKey", lblTaskRequestKey.Text);
                dt = objCommon.GetDataTable("GetManageTaskJobHistoryjobView", nv);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["WorkDate"].ToString() != "")
                    {

                        lblTaskStatus.Text = Convert.ToDateTime(dt.Rows[0]["WorkDate"]).ToString("MM/dd/yyyy");


                        btnStart.Attributes.Add("class", "bttn bttn-primary bttn-action my-1 mx-auto d-block w-100");
                    }
                }
                else
                {
                    lblTaskStatus.Text = "Pending";
                    //  btnStart.Enabled = false;
                    btnStart.Enabled = true;
                    btnStart.Attributes.Add("class", "bttn bttn-disabled bttn-action my-1 mx-auto d-block w-100");
                }

                //if (lblStatusValues.Text == "1" || lblStatusValues.Text == "2")
                //{
                //    lblstatus.Text = "Completed";
                //}
                //else
                //{
                //    lblstatus.Text = "Pending";
                //}

                //if (lblStatusValues.Text == "2")
                //{
                //    lblPudawayDate.Text = lblPudawayDate.Text;
                //}
                //else
                //{
                //    lblPudawayDate.Text = "Pending";
                //}

            }
        }

        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            BindGridGerm(txtSearchJobNo.Text);
        }

        //protected void gvGerm_RowDataBound1(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        Label lblstatus = (Label)e.Row.FindControl("lblstatus");
        //        Label lblStatusValues = (Label)e.Row.FindControl("lblStatusValues");
        //        Label lblPudawayDate = (Label)e.Row.FindControl("lblPudawayDate");
        //        Label lblPutawayStatusValues = (Label)e.Row.FindControl("lblPutawayStatusValues");
        //        if (lblStatusValues.Text == "1" || lblStatusValues.Text == "2")
        //        {
        //            lblstatus.Text = "Completed";
        //        }
        //        else
        //        {
        //            lblstatus.Text = "Pending";
        //        }

        //        if (lblStatusValues.Text == "2")
        //        {
        //            lblPudawayDate.Text = lblPudawayDate.Text;
        //        }
        //        else
        //        {
        //            lblPudawayDate.Text = "Pending";
        //        }

        //    }
        //}


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
    }
}
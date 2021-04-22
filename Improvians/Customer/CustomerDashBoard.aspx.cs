﻿using Evo.Admin.BAL_Classes;
using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class CustomerDashBoard : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        public static DataTable AllData = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblCustName.Text = Session["EmployeeName"].ToString();
                BindJobCode();
                BindGridGerm();
                BindFacility();
            }
        }

        public void BindJobCode()
        {
            ddlJobNo.ClearSelection();
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Customer", Session["EmployeeName"].ToString());
            nv.Add("@JobNo", ddlJobNo.SelectedValue);
            nv.Add("@WorkDateForm", txtFromDate.Text);
            nv.Add("@WorkDateTo", txtToDate.Text);
            nv.Add("@Mode", "2");
            dt = objCommon.GetDataTable("GetCustomerManageTask", nv);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataBind();

            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindFacility()
        {
            BAL_Task objTask = new BAL_Task();
            DataSet ds = objTask.GetEmployeeByID(Convert.ToInt32(Session["LoginID"]));
            ddlFacility.DataSource = ds.Tables[1];
            ddlFacility.DataTextField = "FacilityName";
            ddlFacility.DataValueField = "FacilityName";
            ddlFacility.DataBind();
            //ddlFacility.Items.Insert(0, new ListItem("--Select--", "0"));
            if (Session["Facility"] != null && Session["Facility"].ToString() != string.Empty)
            {
                ddlFacility.SelectedValue = Session["Facility"].ToString();
            }
            else
            {
                Session["Facility"] = ddlFacility.SelectedValue;
            }

        }
        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Facility"] = ddlFacility.SelectedValue;
                 }

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Customer", Session["EmployeeName"].ToString());
            nv.Add("@JobNo", ddlJobNo.SelectedValue);
            nv.Add("@WorkDateForm", txtFromDate.Text);
            nv.Add("@WorkDateTo", txtToDate.Text);
            nv.Add("@Mode", "1");
            AllData = objCommon.GetDataTable("GetCustomerManageTask", nv);
            gvGerm.DataSource = AllData;
            gvGerm.DataBind();
        }
        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";

            BindJobCode();
            BindGridGerm();
        }


        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindGridGerm();

        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "GStart")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string BatchLocation = gvGerm.DataKeys[rowIndex].Values[0].ToString();
                string jobCode = gvGerm.DataKeys[rowIndex].Values[1].ToString();
                string TaskRequestType = gvGerm.DataKeys[rowIndex].Values[2].ToString();

                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@BenchLocation", BatchLocation);
                nv.Add("@JobNo", jobCode);
                nv.Add("@RequestType", TaskRequestType);
                dt = objCommon.GetDataTable("GetManageTaskJobHistoryjobViewDetsils", nv);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (TaskRequestType == "Fertilization")
                    {
                        Response.Redirect(String.Format("~/SprayTaskViewDetails.aspx?PageType={0}&FertilizationCode={1}&FCID={2}", "ManageTask", dt.Rows[0]["FertilizationCode"].ToString(), dt.Rows[0]["SprayId"].ToString()));
                    }
                    if (TaskRequestType == "Chemical")
                    {

                        Response.Redirect(String.Format("~/ChemicalTaskViewDetails.aspx?PageType={0}&ChemicalCode={1}&CCID={2}", "ManageTask", dt.Rows[0]["ChemicalCode"].ToString(), dt.Rows[0]["ChemicalCompletionId"].ToString()));
                    }
                    if (TaskRequestType == "Germination")
                    {
                        Response.Redirect(String.Format("~/GreenHouseTaskCompletion.aspx?PageType={0}&GTAID={1}", "ManageTask", dt.Rows[0]["ID"].ToString()));
                    }
                    if (TaskRequestType == "Irrigation")
                    {
                        Response.Redirect(String.Format("~/IrrigationTaskViewDetails.aspx?PageType={0}&IrrigationCode={1}&ICID={2}", "ManageTask", dt.Rows[0]["IrrigationCode"].ToString(), dt.Rows[0]["IrrigationTaskAssignmentId"].ToString()));


                    }


                    if (TaskRequestType == "Plant Ready")
                    {

                        Response.Redirect(String.Format("~/PlantReadyTaskCompletion.aspx?PageType={0}&PRAID={1}&PRID={2}", "ManageTask", dt.Rows[0]["PlantReadyTaskAssignmentId"].ToString(), dt.Rows[0]["PRID"].ToString()));
                    }

                    if (TaskRequestType == "Dump")
                    {
                        Response.Redirect(String.Format("~/DumpTaskCompletion.aspx?PageType={0}&Did={1}&Chid={2}&DrId={3}", "ManageTask", dt.Rows[0]["DumpTaskAssignmentId"].ToString(), 0, dt.Rows[0]["DumpId"].ToString()));

                    }
                    if (TaskRequestType == "Move")
                    {

                        Response.Redirect(String.Format("~/MoveTaskCompletion.aspx?PageType={0}&Did={1}&DrId={2}", "ManageTask", dt.Rows[0]["MoveTaskAssignmentId"].ToString(), dt.Rows[0]["MoveID"].ToString()));

                    }
                    if (TaskRequestType == "GeneralTask")
                    {
                        Response.Redirect(String.Format("~/GeneralTaskCompletion.aspx?PageType={0}&Did={1}&Chid={2}&DrId={3}", "ManageTask", dt.Rows[0]["GeneralTaskAssignmentId"].ToString(), 0, dt.Rows[0]["GeneralId"].ToString()));


                    }

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

                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@BenchLocation", lblBenchLocation.Text);
                nv.Add("@JobNo", lblJobNo.Text);
                nv.Add("@RequestType", lblTaskRequestType.Text);
                dt = objCommon.GetDataTable("GetManageTaskJobHistoryjobView", nv);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["WorkDate"].ToString() != "")
                    {
                        lblTaskStatus.Text = Convert.ToDateTime(dt.Rows[0]["WorkDate"]).ToString("MM-dd-yyyy");
                        btnStart.Enabled = true;

                        btnStart.Attributes.Add("class", "bttn bttn-primary bttn-action my-1 mx-auto d-block w-100");
                    }
                }
                else
                {
                    lblTaskStatus.Text = "Pending";
                    //  btnStart.Enabled = false;

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


    }
}
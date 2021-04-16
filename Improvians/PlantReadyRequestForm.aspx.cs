﻿using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Fdate = "", TDate = "";
                Fdate = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(10).ToString("yyyy-MM-dd");


                txtFromDate.Text = Fdate;
                txtToDate.Text = TDate;

                Bindcname();
                BindBenchLocation(Session["Facility"].ToString());
                BindJobCode(ddlBenchLocation.SelectedValue);
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
            BindGridPlantReady(1);

        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }
        protected void RadioButtonListSourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
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

        public void BindGridPlantReady(int p)
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@RequestType", RadioButtonListSourse.SelectedValue);
            nv.Add("@FromDate", txtFromDate.Text);
            nv.Add("@ToDate", txtToDate.Text);


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
            if (p != 1)
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
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                i--;
                if (checkJob == JobCode && checklocation == benchLoc)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit>= 10)
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
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            //ddlSupervisor.DataTextField = "EmployeeName";
            //ddlSupervisor.DataValueField = "ID";
            //ddlSupervisor.DataBind();
            //ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

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

        //public void BindFacility()
        //{

        //    DataTable dt = new DataTable();
        //    NameValueCollection nv = new NameValueCollection();

        //    nv.Add("@Mode", "9");
        //    dt = objCommon.GetDataTable("GET_Common", nv);
        //    ddlFacility.DataSource = dt;
        //    ddlFacility.DataTextField = "loc_seedline";
        //    ddlFacility.DataValueField = "loc_seedline";
        //    ddlFacility.DataBind();
        //    ddlFacility.Items.Insert(0, new ListItem("--Select--", "0"));

        //}
        //protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindGridPlantReady(1);
        //}



        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();

            BindGridPlantReady(1);
        }
        protected void gvPlantReady_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
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
                    nv.Add("@Itemdesc","");
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@ChId", "0");
                    nv.Add("@wo", "");
                    nv.Add("@Comments", txtPlantComments.Text.Trim());
                    nv.Add("@PlantDate", txtPlantDate.Text);
                    nv.Add("@Role", Session["Role"].ToString());
                    nv.Add("@SeedDate", "");
                    nv.Add("@Jid", lblJid.Text);

                    result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyRequestManuaCreateTaskStart", nv);
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

                    if (result > 0)
                    {
                        Response.Redirect(String.Format("~/PlantReadyTaskCompletion.aspx?PRAID={0}&PRID={1}", result1, lblPRRId.Text));
                    }

                }
            }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);
            nv.Add("@ManualID", lblJid.Text);
            nv.Add("@Comments", txtPlantComments.Text);
            nv.Add("@PRid", lblPRRId.Text);
            nv.Add("@PlantDate", txtPlantDate.Text);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@RoleId", Session["Role"].ToString());
            nv.Add("@IsAssistant", lblIsAssistant.Text);

            result = objCommon.GetDataInsertORUpdate("SP_AddPlantReadyRequestNew", nv);
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
                //  string url = "MyTaskGrower.aspx";
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

            ddlSupervisor.SelectedIndex = 0;

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }

        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlantReady.PageIndex = e.NewPageIndex;
            BindGridPlantReady(1);
        }

        protected void btnManual_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PlantReadyReqManual.aspx");
        }


    }

}




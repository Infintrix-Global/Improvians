﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Bal;
using Evo.BAL_Classes;
using System.Text.RegularExpressions;
namespace Evo
{
    public partial class FerJobBuildUp : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();
        Bal_SeedingPlan objSP = new Bal_SeedingPlan();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFertilizer();
                //BindUnit();

                if (Request.QueryString["Bench"] != null)
                {
                    Bench = Request.QueryString["Bench"].ToString();
                }

                if (Request.QueryString["jobCode"] != null)
                {
                    JobCode = Request.QueryString["jobCode"].ToString();
                }

                if (Request.QueryString["FCode"] != null)
                {
                    FCode = Request.QueryString["FCode"].ToString();
                }


                if (Request.QueryString["TaskRequestKey"] != null)
                {
                    TaskRequestKey = Request.QueryString["TaskRequestKey"].ToString();
                }

                if (Request.QueryString["AssignedBy"] != null)
                {
                    AssignedBy = Request.QueryString["AssignedBy"].ToString();
                }


                txtDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                lblbench.Text = Bench;

                BenchUp = "'" + Bench + "'";
                BindGridFerReq();
                BindGridFerDetails(Bench);

                BindSupervisor();
                BindFertilizer();
                BindGridFerReqView(Request.QueryString["FCode"].ToString());
                BindSQFTofBench("'" + Bench + "'");
            }
        }

        private string StartButton
        {
            get
            {
                if (ViewState["StartButton"] != null)
                {
                    return (string)ViewState["StartButton"];
                }
                return "";
            }
            set
            {
                ViewState["StartButton"] = value;
            }
        }

        private string AssignedBy
        {
            get
            {
                if (ViewState["AssignedBy"] != null)
                {
                    return (string)ViewState["AssignedBy"];
                }
                return "";
            }
            set
            {
                ViewState["AssignedBy"] = value;
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

        private string FCode
        {
            get
            {
                if (ViewState["FCode"] != null)
                {
                    return (string)ViewState["FCode"];
                }
                return "";
            }
            set
            {
                ViewState["FCode"] = value;
            }
        }


        public void BindSupervisor()
        {

            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@RoleID", "11");
            //ddlsupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;
            //ddlsupervisor.DataTextField = "EmployeeName";
            //ddlsupervisor.DataValueField = "ID";
            //ddlsupervisor.DataBind();
            //ddlsupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            NameValueCollection nv = new NameValueCollection();
            DataTable dt = new DataTable();
            nv.Add("@RoleID", Session["Role"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetRoleForAssignementFacility", nv); ;

            ddlsupervisor.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlsupervisor.DataTextField = "EmployeeName";
            ddlsupervisor.DataValueField = "ID";
            ddlsupervisor.DataBind();
            ddlsupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        private string Jid
        {
            get
            {
                if (ViewState["Jid"] != null)
                {
                    return (string)ViewState["Jid"];
                }
                return "";
            }
            set
            {
                ViewState["Jid"] = value;
            }
        }

        private string Pid
        {
            get
            {
                if (ViewState["Pid"] != null)
                {
                    return (string)ViewState["Pid"];
                }
                return "";
            }
            set
            {
                ViewState["Pid"] = value;
            }
        }

        private string BenchUp
        {
            get
            {
                if (ViewState["BenchUp"] != null)
                {
                    return (string)ViewState["BenchUp"];
                }
                return "";
            }
            set
            {
                ViewState["BenchUp"] = value;
            }
        }

        private string Bench
        {
            get
            {
                if (ViewState["Bench"] != null)
                {
                    return (string)ViewState["Bench"];
                }
                return "";
            }
            set
            {
                ViewState["Bench"] = value;
            }
        }


        private string JobMainTray
        {
            get
            {
                if (ViewState["JobMainTray"] != null)
                {
                    return (string)ViewState["JobMainTray"];
                }
                return "";
            }
            set
            {
                ViewState["JobMainTray"] = value;
            }
        }

        private string JobCode
        {
            get
            {
                if (ViewState["JobCode"] != null)
                {
                    return (string)ViewState["JobCode"];
                }
                return "";
            }
            set
            {
                ViewState["JobCode"] = value;
            }
        }

        private string FR_ID
        {
            get
            {
                if (ViewState["FR_ID"] != null)
                {
                    return (string)ViewState["FR_ID"];
                }
                return "";
            }
            set
            {
                ViewState["FR_ID"] = value;
            }
        }



        protected void RadioBench_SelectedIndexChanged(object sender, EventArgs e)
        {
            // SelectBenchLocation();
            string chkSelected = "";
            string SQFTofBench = "";
            if (RadioBench.SelectedValue == "1")
            {
                // Bench
                //  SelectBench();
                PanelBench.Visible = true;
                PanelBenchesInHouse.Visible = false;
                PanelHouse.Visible = false;
                int P1 = 0;
                string Q1 = "";
                string B1 = "";
                string YourString = Bench;
                YourString = YourString.Remove(YourString.Length - 1);

                DataTable dt12 = objFer.GetSelectBench(YourString);
                if (dt12 != null && dt12.Rows.Count > 0)
                {
                    lblBench1.Text = dt12.Rows[0]["PositionCode"].ToString();


                    if (dt12 != null && dt12.Rows.Count > 0)
                    {
                        DataColumn col = dt12.Columns["PositionCode"];
                        foreach (DataRow row in dt12.Rows)
                        {
                            //strJsonData = row[col].ToString();

                            P1 = 1;
                            Q1 += row[col].ToString() + ",";
                            B1 += "'" + row[col].ToString() + "',";
                        }
                        if (P1 > 0)
                        {
                            chkSelected = Q1.Remove(Q1.Length - 1, 1);
                            SQFTofBench = B1.Remove(B1.Length - 1, 1);

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        chkSelected = Bench;
                        SQFTofBench = "'" + Bench + "'";
                    }

                    BenchUp = SQFTofBench;
                    DataTable dt123 = new DataTable();
                    gvJobHistory.DataSource = dt123;
                    gvJobHistory.DataBind();
                    BindGridFerDetails(chkSelected);
                    BindSQFTofBench(SQFTofBench);
                }
            }
            else if (RadioBench.SelectedValue == "2")
            {
                SelectBenchLocation();
                PanelBench.Visible = false;
                PanelBenchesInHouse.Visible = true;
                PanelHouse.Visible = false;
            }
            else if (RadioBench.SelectedValue == "3")
            {
                // House
                PanelBench.Visible = false;
                PanelBenchesInHouse.Visible = false;
                PanelHouse.Visible = true;
                int P = 0;
                string B = "";
                string Q = "";
                string[] words = Regex.Split(Bench, @"\W+");

                DataTable dt = objFer.GetSelectBenchLocation(words[0], words[1]);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataColumn col = dt.Columns["PositionCode"];
                    foreach (DataRow row in dt.Rows)
                    {
                        //strJsonData = row[col].ToString();

                        P = 1;
                        B += "'" + row[col].ToString() + "',";
                        Q += row[col].ToString() + ",";
                    }
                    if (P > 0)
                    {
                        chkSelected = Q.Remove(Q.Length - 1, 1);
                        SQFTofBench = B.Remove(B.Length - 1, 1);
                    }
                    else
                    {

                    }
                }
                else
                {
                    chkSelected = Bench;
                    SQFTofBench = "'" + Bench + "'";
                }

                BenchUp = SQFTofBench;

                DataTable dt123 = new DataTable();
                gvJobHistory.DataSource = dt123;
                gvJobHistory.DataBind();
                BindGridFerDetails(chkSelected);
                BindSQFTofBench(SQFTofBench);
            }
            else
            {
            }
        }

        protected void ListBoxBenchesInHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            int c = 0;
            string x = "";
            string B = "";
            string chkSelected = "";
            string SQFTofBench = "";
            foreach (ListItem item in ListBoxBenchesInHouse.Items)
            {

                if (item.Selected)
                {
                    c = 1;
                    B += "'" + item.Text + "',";
                    x += item.Text + ",";
                }
            }
            if (c > 0)
            {
                chkSelected = x.Remove(x.Length - 1, 1);
                SQFTofBench = B.Remove(B.Length - 1, 1);
            }
            else
            {
            }
            BenchUp = SQFTofBench;
            BindGridFerDetails(chkSelected);
            BindSQFTofBench(SQFTofBench);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string chkSelected = "";
            if (RadioBench.SelectedValue == "1")
            {
                chkSelected = lblBench1.Text;
            }
            else if (RadioBench.SelectedValue == "2")
            {
                int c = 0;
                string x = "";

                foreach (ListItem item in ListBoxBenchesInHouse.Items)
                {
                    if (item.Selected)
                    {
                        c = 1;
                        x += item.Text + ",";
                        //x += "'" + item.Text + "',";
                    }
                }
                if (c > 0)
                {
                    chkSelected = x.Remove(x.Length - 1, 1);
                }
                else
                {
                }
            }
            else if (RadioBench.SelectedValue == "3")
            {
            }

            BindGridFerDetails(chkSelected);
        }

        public void SelectBench()
        {
            string YourString = Bench;
            // ENC2 - SHADE - 2 - A
            //string input = Bench;
            //string[] array = input.Split('-');
            YourString = YourString.Remove(YourString.Length - 1);
            DataTable dt = objFer.GetSelectBench(YourString);
            lblBench1.Text = dt.Rows[0]["PositionCode"].ToString();
        }

        public void SelectBenchLocation()
        {
            string[] words = Regex.Split(Bench, @"\W+");
            DataTable dt = objFer.GetSelectBenchLocation(words[0], words[1]);
            ListBoxBenchesInHouse.DataSource = dt;
            ListBoxBenchesInHouse.DataTextField = "PositionCode";
            ListBoxBenchesInHouse.DataValueField = "PositionCode";
            ListBoxBenchesInHouse.DataBind();

        }
        public void BindGridFerReqView(string Foce)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Foce", Foce);

            dt = objCommon.GetDataTable("SP_GetTaskAssignmentFertilizationView", nv);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlFertilizer.SelectedItem.Text = dt.Rows[0]["Fertilizer"].ToString();
                txtQty.Text = dt.Rows[0]["Quantity"].ToString();

                txtComments.Text = dt.Rows[0]["Comments"].ToString();

            }
        }

        public void BindGridFerReq()
        {
            //DataTable dt = new DataTable();
            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@JobCode", JobCode);
            //nv.Add("@CustomerName", "0");
            //nv.Add("@Facility", "0");
            //nv.Add("@BenchLocation", Bench);
            //nv.Add("@RequestType", "0");
            //nv.Add("@FromDate", "");
            //nv.Add("@ToDate","");


            //if (Session["Role"].ToString() == "12")
            //{
            //    dt = objCommon.GetDataTable("SP_GetFertilizerRequestAAssistantGrower", nv);
            //}
            //else
            //{
            //    dt = objCommon.GetDataTable("SP_GetFertilizerRequest", nv);
            //}

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", JobCode);
            nv.Add("@BenchLocation", Bench);

            dt = objCommon.GetDataTable("SP_GetFertilizerRequestGrowerStart", nv);
            gvFer.DataSource = dt;
            gvFer.DataBind();

            Jid = dt.Rows[0]["Jid"].ToString();
            Pid = dt.Rows[0]["GrowerPutAwayId"].ToString();
            decimal tray = 0;
            foreach (GridViewRow row in gvFer.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                tray = tray + Convert.ToDecimal((row.FindControl("lblTotTray") as Label).Text);
                //}
            }
            JobMainTray = tray.ToString();
            txtTrays.Text = tray.ToString();
        }

        public void BindGridFerDetails(string BenchLoc)
        {
            //DataTable dt = new DataTable();
            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@BenchLocation", BenchLoc);
            //dt = objCommon.GetDataTable("SP_GetFertilizerRequestDetails", nv);

            //DataTable dtManual = objTask.GetManualRequestStart1(Session["Facility"].ToString(), BenchLoc, "'" + JobCode + "'");

            //if (dt != null && dt.Rows.Count > 0 && dtManual != null && dtManual.Rows.Count > 0)
            //{
            //    dt.Merge(dtManual);
            //    dt.AcceptChanges();
            //    gvJobHistory.DataSource = dt;
            //    gvJobHistory.DataBind();

            //}
            //else if (dtManual != null && dtManual.Rows.Count > 0)
            //{
            //    gvJobHistory.DataSource = dtManual;
            //    gvJobHistory.DataBind();

            //}
            //else
            //{
            //    gvJobHistory.DataSource = dt;
            //    gvJobHistory.DataBind();
            //}

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", BenchLoc);
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetSeedDetails", nv);

            gvJobHistory.DataSource = dt;
            gvJobHistory.DataBind();

            txtTrays.Text = "";
            decimal tray = 0;
            foreach (GridViewRow row in gvJobHistory.Rows)
            {
                if (!string.IsNullOrEmpty((row.FindControl("lblTotTray") as Label).Text))
                    tray = tray + Convert.ToDecimal((row.FindControl("lblTotTray") as Label).Text);
            }

            txtTrays.Text = tray.ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string Batchlocation = "";
            string Batchlocation1 = "";
            string Batchlocation2 = "";
            string TaskRequestKey = "";
            int FertilizationCode = 0;
            bool hasValue = false;
            string SprayTaskForDaysDate = "";



            //int FertilizationCode = 0;
            //string SprayTaskForDaysDate = "";
            //if (FCode == "0")
            //{
            //    DataTable dt = new DataTable();
            //    NameValueCollection nv1 = new NameValueCollection();
            //    nv1.Add("@Mode", "12");
            //    dt = objCommon.GetDataTable("GET_Common", nv1);
            //    FertilizationCode = Convert.ToInt32(dt.Rows[0]["FCode"]);
            //}
            //else
            //{
            //    FertilizationCode = Convert.ToInt32(FCode);
            //}


            //if (txtResetSprayTaskForDays.Text != "")
            //{
            //    SprayTaskForDaysDate = (Convert.ToDateTime(System.DateTime.Now.ToShortDateString()).AddDays(Convert.ToInt32(txtResetSprayTaskForDays.Text))).ToString();

            //}
            //else
            //{
            //    SprayTaskForDaysDate = System.DateTime.Now.ToShortDateString();

            //}


            //foreach (GridViewRow row in gvFer.Rows)
            //{
            //    long result = 0;
            //    long Mresult = 0;
            //    NameValueCollection nv = new NameValueCollection();
            //    nv.Add("@SupervisorID", ddlsupervisor.SelectedValue);
            //    nv.Add("@Type", "Fertilizer");
            //    nv.Add("@WorkOrder", (row.FindControl("lblwo") as Label).Text);
            //    nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
            //    //nv.Add("@WorkOrder", lblwo.Text);
            //    nv.Add("@LoginID", Session["LoginID"].ToString());
            //    nv.Add("@FertilizationCode", FertilizationCode.ToString());
            //    nv.Add("@FertilizationDate", txtDate.Text);
            //    nv.Add("@Jid", Jid);
            //    nv.Add("@TaskRequestKey", TaskRequestKey);
            //    nv.Add("@BanchLocation", (row.FindControl("lblGreenHouse") as Label).Text);
            //    nv.Add("@ResetTaskForDays", SprayTaskForDaysDate);

            //    result = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequest", nv);

            //    FR_ID = result.ToString();

            //    var txtBenchLocation = (row.FindControl("lblGreenHouse") as Label).Text;

            //    nv.Clear();
            //    nv.Add("@LoginID", Session["LoginID"].ToString());
            //    nv.Add("@jobcode", "");
            //    nv.Add("@GreenHouseID", txtBenchLocation);
            //    nv.Add("@TaskName", "Fertilizer");

            //    var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nv);


            //    NameValueCollection nvkey = new NameValueCollection();
            //    nvkey.Add("@Id", FR_ID);
            //    nvkey.Add("@Mode", "1");
            //    DataTable dtkey = objCommon.GetDataTable("SP_GetTaskRequest", nvkey);
            //    TaskRequestKey = dtkey.Rows[0]["TaskRequestKey"].ToString();

            //    NameValueCollection nvn = new NameValueCollection();
            //    nvn.Add("@LoginID", Session["LoginID"].ToString());
            //    nvn.Add("@SupervisorID", ddlsupervisor.SelectedValue);
            //    nvn.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
            //    nvn.Add("@TaskName", "Fertilizer");
            //    nvn.Add("@TaskRequestKey", TaskRequestKey);
            //    nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
            //    var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);




            //}
            long result = 0;
            foreach (GridViewRow row in gvJobHistory.Rows)
            {

                Batchlocation = (row.FindControl("lblGreenHouse") as Label).Text;

                if (Batchlocation1 == "" || Batchlocation1 != Batchlocation)
                {
                    dtTrays.Clear();
                    DataTable dt1 = new DataTable();
                    NameValueCollection nv14 = new NameValueCollection();
                    nv14.Add("@Mode", "12");
                    dt1 = objCommon.GetDataTable("GET_Common", nv14);
                    FertilizationCode = Convert.ToInt32(dt1.Rows[0]["FCode"]);
                    Batchlocation1 = Batchlocation;


                    string Countbanch = "";
                    DataTable dt15 = new DataTable();
                    NameValueCollection nv15 = new NameValueCollection();
                    nv15.Add("@Mode", "1");
                    nv15.Add("@GreenHouseID", Batchlocation1);
                    dt15 = objCommon.GetDataTable("SP_GetTaskRequestKeyBanchlocation", nv15);
                    Countbanch = dt15.Rows[0]["Countbanch"].ToString();

                    TaskRequestKey = Batchlocation + "_" + "Fertilization" + "_" + Countbanch;


                }
                else
                {
                    FertilizationCode = FertilizationCode;

                }


                if (txtResetSprayTaskForDays.Text != "")
                {
                    SprayTaskForDaysDate = (Convert.ToDateTime(System.DateTime.Now.ToShortDateString()).AddDays(Convert.ToInt32(txtResetSprayTaskForDays.Text))).ToString();

                }
                else
                {
                    SprayTaskForDaysDate = System.DateTime.Now.ToShortDateString();

                }


                string TodatDate;
                string ReSetSprayDate = "";

                TodatDate = System.DateTime.Now.ToShortDateString();


                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", ddlsupervisor.SelectedValue);
                nv.Add("@Type", "Fertilizer");
                nv.Add("@WorkOrder", (row.FindControl("lblwo") as Label).Text);
                nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                //nv.Add("@WorkOrder", lblwo.Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@FertilizationCode", FertilizationCode.ToString());
                nv.Add("@FertilizationDate", txtDate.Text);
                nv.Add("@Jid", (row.FindControl("lblJid") as Label).Text);
                nv.Add("@TaskRequestKey", TaskRequestKey);
                nv.Add("@BanchLocation", (row.FindControl("lblGreenHouse") as Label).Text);
                nv.Add("@ResetTaskForDays", SprayTaskForDaysDate);


                result = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequest", nv);

                //}
                //}

                if (Batchlocation2 == "" || Batchlocation2 != Batchlocation)
                {

                    dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text, "", txtTrays.Text, txtSQFT.Text);

                    objTask.AddFertilizerRequestDetailsCreatTask(dtTrays, result.ToString(), FertilizationCode, Batchlocation, "", "", "", txtResetSprayTaskForDays.Text, txtComments.Text.Trim(), txtNoOfPasses.Text.Trim());
                    Batchlocation2 = Batchlocation;


                    var txtBenchLocation = (row.FindControl("lblGreenHouse") as Label).Text;

                    nv.Clear();
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@jobcode", "");
                    nv.Add("@GreenHouseID", txtBenchLocation);
                    nv.Add("@TaskName", "Fertilizer");

                    var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nv);


                    NameValueCollection nvkey = new NameValueCollection();
                    nvkey.Add("@Id", result.ToString());
                    nvkey.Add("@Mode", "1");
                    DataTable dtkey = objCommon.GetDataTable("SP_GetTaskRequest", nvkey);
                    TaskRequestKey = dtkey.Rows[0]["TaskRequestKey"].ToString();

                    NameValueCollection nvn = new NameValueCollection();
                    nvn.Add("@LoginID", Session["LoginID"].ToString());
                    nvn.Add("@SupervisorID", ddlsupervisor.SelectedValue);
                    nvn.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                    nvn.Add("@TaskName", "Fertilizer");
                    nvn.Add("@TaskRequestKey", TaskRequestKey);
                    nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                    var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);
                }
            }




            long Mresult12 = 0;
            //   NameValueCollection nv123 = new NameValueCollection();
            //   nv123.Add("@BanchLocation", lblbench.Text);
            //   Mresult12 = objCommon.GetDataInsertORUpdate("SP_AddFertilizerRequestMenualUpdate", nv123);
            //  objTask.UpdateIsActiveDatat(BenchUp);


            objTask.UpdateIsActiveFerRole(BenchUp, Convert.ToInt32(Session["Role"].ToString()));
            if (AssignedBy == "System")
            {
                AddJobNextDate();

            }

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
            //   string url = "MyTaskGrower.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Redirect", script, true);
            Clear();

            var res = (Master.FindControl("r1") as Repeater);

            var lblCount = (Master.FindControl("lblNotificationCount") as Label);
            objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), res, lblCount);
        }



        public void AddJobNextDate()
        {
            long _isInserted = 1;
            long _isIGCodeInserted = 1;
            long _isFCdeInserted = 1;
            int SelectedItems = 0;
            string FertilizationDate = string.Empty;
            foreach (GridViewRow row in gvFer.Rows)
            {
                DataTable dtFez = objSP.GetSeedDateDatanew("FERTILIZE", (row.FindControl("lblGenusCode") as Label).Text, (row.FindControl("lblTraySize") as Label).Text);

                NameValueCollection nvChDate = new NameValueCollection();
                nvChDate.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                DataTable ChFdt = objCommon.GetDataTable("SP_GetFertilizationCheckResetSprayTask", nvChDate);

                if (dtFez != null && dtFez.Rows.Count > 0)
                {


                    DataColumn col = dtFez.Columns["DateShift"];
                    int Fcount = 0;
                    foreach (DataRow row1 in dtFez.Rows)
                    {
                        Fcount++;
                        string seeddate = (row.FindControl("lblSeededDate1") as Label).Text;
                        //string AD = row1[col].ToString().Replace("\u0002", "");
                        string AD = row1[col].ToString();
                        FertilizationDate = (Convert.ToDateTime(seeddate).AddDays(Convert.ToInt32(AD))).ToString();
                        string TodatDate;
                        string ReSetSprayDate = "";
                        string DateCountNo = "0";
                        string DateShiftId = "0";

                        DateShiftId = dtFez.Columns["pid"].ToString();


                        DateCountNo = Fcount.ToString();
                        TodatDate = System.DateTime.Now.ToShortDateString();

                        if (ChFdt != null && ChFdt.Rows.Count > 0)
                        {
                            if (ChFdt.Rows[0]["ResetSprayTaskForDays"].ToString() != "")
                            {
                                ReSetSprayDate = Convert.ToDateTime(ChFdt.Rows[0]["CreateDate"]).AddDays(Convert.ToInt32(ChFdt.Rows[0]["ResetSprayTaskForDays"])).ToString();
                            }
                        }

                        if (DateTime.Parse(FertilizationDate) > DateTime.Parse(TodatDate))
                        {

                            if (ReSetSprayDate == "" || DateTime.Parse(FertilizationDate) >= DateTime.Parse(ReSetSprayDate))
                            {


                                string WONo = (row.FindControl("lblwo") as Label).Text;
                                string jid = "";
                                FertilizationDate = FertilizationDate;

                                NameValueCollection nv11 = new NameValueCollection();
                                nv11.Add("@GrowerPutAwayId", (row.FindControl("lblGrowerputawayID") as Label).Text);
                                nv11.Add("@wo", (row.FindControl("lblwo") as Label).Text);

                                nv11.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                                nv11.Add("@FacilityID", (row.FindControl("lblFacility") as Label).Text);
                                nv11.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                                nv11.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                                nv11.Add("@SeedDate", (row.FindControl("lblSeededDate1") as Label).Text);
                                nv11.Add("@CreateBy", Session["LoginID"].ToString());
                                nv11.Add("@Supervisor", "0");
                                nv11.Add("@IrrigateSeedDate", "");
                                nv11.Add("@FertilizeSeedDate", FertilizationDate);
                                nv11.Add("@ID", "");
                                nv11.Add("@GenusCode", (row.FindControl("lblGenusCode") as Label).Text);
                                nv11.Add("@DateCountNo", DateCountNo);

                                if (WONo != "")
                                {
                                    NameValueCollection nv = new NameValueCollection();
                                    // nv.Add("@jid", _isInserted.ToString());

                                    nv.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                                    nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                                    nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                                    nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                                    nv.Add("@Seeddate", (row.FindControl("lblSeededDate1") as Label).Text);
                                    nv.Add("@germcount", "");
                                    nv.Add("@GenusCode", (row.FindControl("lblGenusCode") as Label).Text);
                                    nv.Add("@PlantDueDate", (row.FindControl("lblPlantDueDate") as Label).Text);
                                    nv.Add("@PlantReadyDate", (row.FindControl("lblPlantReadyDate") as Label).Text);
                                    nv.Add("@Wo", (row.FindControl("lblGrowerputawayID") as Label).Text);
                                    _isInserted = objCommon.GetDataExecuteScaler("SP_Addgti_jobs_Seeding_Plan_ManualNextFolaw", nv);
                                    jid = _isInserted.ToString();
                                }
                                else
                                {
                                    jid = (row.FindControl("lblJidF") as Label).Text;
                                }

                                nv11.Add("@Jid", jid);

                                _isFCdeInserted = objCommon.GetDataExecuteScaler("SP_AddGrowerPutAwayDetailsFertilizationMenual", nv11);

                                break;
                            }
                        }
                    }
                    //if (Convert.ToDateTime(TodatDate) >= Convert.ToDateTime(FertilizationDate))
                    //{
                    //    if (ReSetSprayDate == "" || Convert.ToDateTime(ReSetSprayDate) >= Convert.ToDateTime(FertilizationDate))
                    //    {
                    //        FertilizationDate = row[col].ToString();
                    //        break;
                    //    }
                    //}
                }
            }

            foreach (GridViewRow row in gvJobHistory.Rows)
            {

                string JNo = "";
                JNo = (row.FindControl("lblID") as Label).Text;
                DataTable dtFez = objSP.GetSeedDateDatanew("FERTILIZE", (row.FindControl("lblGenusCodeH") as Label).Text, (row.FindControl("lblTraySize") as Label).Text);

                NameValueCollection nvChDate = new NameValueCollection();
                nvChDate.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                DataTable ChFdt = objCommon.GetDataTable("SP_GetFertilizationCheckResetSprayTask", nvChDate);

                if (JNo != JobCode)
                {
                    if (dtFez != null && dtFez.Rows.Count > 0)
                    {


                        DataColumn col = dtFez.Columns["DateShift"];
                        int Fcount = 0;
                        foreach (DataRow row1 in dtFez.Rows)
                        {
                            Fcount++;
                            string seeddate = (row.FindControl("lblSeededDate11") as Label).Text;
                            //string AD = row1[col].ToString().Replace("\u0002", "");
                            string AD = row1[col].ToString();
                            FertilizationDate = (Convert.ToDateTime(seeddate).AddDays(Convert.ToInt32(AD))).ToString();
                            string TodatDate;
                            string ReSetSprayDate = "";
                            string DateCountNo = "0";

                            DateCountNo = Fcount.ToString();
                            TodatDate = System.DateTime.Now.ToShortDateString();

                            if (ChFdt != null && ChFdt.Rows.Count > 0)
                            {
                                if (ChFdt.Rows[0]["ResetSprayTaskForDays"].ToString() != "")
                                {
                                    ReSetSprayDate = Convert.ToDateTime(ChFdt.Rows[0]["CreateDate"]).AddDays(Convert.ToInt32(ChFdt.Rows[0]["ResetSprayTaskForDays"])).ToString();
                                }
                            }

                            if (DateTime.Parse(FertilizationDate) > DateTime.Parse(TodatDate))
                            {

                                if (ReSetSprayDate == "" || DateTime.Parse(FertilizationDate) >= DateTime.Parse(ReSetSprayDate))
                                {
                                    FertilizationDate = FertilizationDate;
                                    NameValueCollection nv11 = new NameValueCollection();
                                    nv11.Add("@GrowerPutAwayId", (row.FindControl("lblGrowerputawayID") as Label).Text);
                                    nv11.Add("@wo", (row.FindControl("lblwo") as Label).Text);
                                    nv11.Add("@Jid", (row.FindControl("lblJid") as Label).Text);
                                    nv11.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                                    nv11.Add("@FacilityID", (row.FindControl("lblFacility") as Label).Text);
                                    nv11.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                                    nv11.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                                    nv11.Add("@SeedDate", (row.FindControl("lblSeededDate11") as Label).Text);
                                    nv11.Add("@CreateBy", Session["LoginID"].ToString());
                                    nv11.Add("@Supervisor", "0");
                                    nv11.Add("@IrrigateSeedDate", "");
                                    nv11.Add("@FertilizeSeedDate", FertilizationDate);
                                    nv11.Add("@ID", "");
                                    nv11.Add("@GenusCode", (row.FindControl("lblGenusCodeH") as Label).Text);
                                    nv11.Add("@DateCountNo", DateCountNo);

                                    _isFCdeInserted = objCommon.GetDataExecuteScaler("SP_AddGrowerPutAwayDetailsFertilizationMenual", nv11);

                                    break;
                                }
                            }
                        }
                        //if (Convert.ToDateTime(TodatDate) >= Convert.ToDateTime(FertilizationDate))
                        //{
                        //    if (ReSetSprayDate == "" || Convert.ToDateTime(ReSetSprayDate) >= Convert.ToDateTime(FertilizationDate))
                        //    {
                        //        FertilizationDate = row[col].ToString();
                        //        break;
                        //    }
                        //}
                    }
                }


            }





        }



        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtQty.Text = "";
            txtSQFT.Text = "";
            txtTrays.Text = "";
            //txtBenchIrrigationFlowRate.Text = "";
            //txtBenchIrrigationCoverage.Text = "";
            //txtSprayCoverageperminutes.Text = "";

            BindFertilizer();
            dtTrays.Clear();
        }

        public void BindSQFTofBench(string Bench)
        {
            //  DataTable dtSQFT = objFer.GetSQFTofBench(lblbench.Text);
            DataTable dtSQFT = objFer.GetSQFTofBenchNew(Bench);
            if (dtSQFT != null && dtSQFT.Rows.Count > 0)
            {
                txtSQFT.Text = Convert.ToDecimal(dtSQFT.Rows[0]["Sqft"]).ToString("#,0000.00");
            }
            else
            {
                txtSQFT.Text = "0.00";
            }
        }

        public void BindFertilizer()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlFertilizer.DataSource = objFer.GetFertilizerList();
            ddlFertilizer.DataTextField = "Name";
            ddlFertilizer.DataValueField = "No_";
            ddlFertilizer.DataBind();
            ddlFertilizer.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            RadioBench.Items[0].Selected = false;
            ListBoxBenchesInHouse.Items.Clear();
            //To unselect all Items
            RadioBench.ClearSelection();
            BindGridFerDetails("'" + Bench + "'");
            PanelBench.Visible = false;
            PanelBenchesInHouse.Visible = false;
        }

        //public void BindUnit()
        //{
        //    NameValueCollection nv = new NameValueCollection();
        //    ddlUnit.DataSource = objFer.GetUnitList();
        //    ddlUnit.DataTextField = "Description";
        //    ddlUnit.DataValueField = "Code";
        //    ddlUnit.DataBind();
        //    ddlUnit.Items.Insert(0, new ListItem("--- Select ---", "0"));
        //}
    }
}
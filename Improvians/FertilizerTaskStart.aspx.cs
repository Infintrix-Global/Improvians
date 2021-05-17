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
    public partial class FertilizerTaskStart1 : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();
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

                txtDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                lblbench.Text = Bench;

                BindGridFerReq();
                BindGridFerDetails("'" + Bench + "'");


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

        protected void RadioBench_SelectedIndexChanged(object sender, EventArgs e)
        {
            // SelectBenchLocation();
            string chkSelected = "";
            if (RadioBench.SelectedValue == "1")
            {
                // Bench
                //  SelectBench();
                PanelBench.Visible = true;
                PanelBenchesInHouse.Visible = false;
                PanelHouse.Visible = false;
                int P1 = 0;
                string Q1 = "";

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
                            Q1 += "'" + row[col].ToString() + "',";
                        }
                        if (P1 > 0)
                        {
                            chkSelected = Q1.Remove(Q1.Length - 1, 1);
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        chkSelected = "'" + Bench + "'";
                    }

                    DataTable dt123 = new DataTable();
                    gvJobHistory.DataSource = dt123;
                    gvJobHistory.DataBind();
                    BindGridFerDetails(chkSelected);
                    BindSQFTofBench(chkSelected);
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
                        Q += "'" + row[col].ToString() + "',";
                    }
                    if (P > 0)
                    {
                        chkSelected = Q.Remove(Q.Length - 1, 1);
                    }
                    else
                    {

                    }
                }
                else
                {
                    chkSelected = "'" + Bench + "'";
                }

                DataTable dt123 = new DataTable();
                gvJobHistory.DataSource = dt123;
                gvJobHistory.DataBind();
                BindGridFerDetails(chkSelected);
                BindSQFTofBench(chkSelected);
            }
            else
            {

            }
        }

        protected void ListBoxBenchesInHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            int c = 0;
            string x = "";
            string chkSelected = "";
            foreach (ListItem item in ListBoxBenchesInHouse.Items)
            {
                if (item.Selected)
                {
                    c = 1;
                    x += "'" + item.Text + "',";
                }
            }
            if (c > 0)
            {
                chkSelected = x.Remove(x.Length - 1, 1);
            }
            else
            {

            }

            BindGridFerDetails(chkSelected);
            BindSQFTofBench(chkSelected);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string chkSelected = "";
            if (RadioBench.SelectedValue == "1")
            {
                chkSelected = "'" + lblBench1.Text + "'";
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
                        x += "'" + item.Text + "',";
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
            // ENC2 - SHADE - 2 - A
            //string input = Bench;
            //string[] array = input.Split('-');

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
                //    Quantity
                txtQty.Text = dt.Rows[0]["Quantity"].ToString();
                txtComments.Text = dt.Rows[0]["Comments"].ToString();
            }
        }

        public void BindGridFerReq()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", JobCode);
            nv.Add("@BenchLocation", Bench);

            //  dt = objCommon.GetDataTable("SP_GetFertilizerRequest", nv);
            dt = objCommon.GetDataTable("SP_GetFertilizerRequestGrowerStart", nv);

            gvFer.DataSource = dt;
            gvFer.DataBind();

            Jid = dt.Rows[0]["Jid"].ToString();
            Pid = dt.Rows[0]["GrowerPutAwayId"].ToString();
        }

        public void BindGridFerDetails(string BenchLoc)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", BenchLoc);
            dt = objCommon.GetDataTable("SP_GetFertilizerRequestDetails", nv);

            //  DataTable dtManual = objFer.GetManualFertilizerRequestSelect("", BenchLoc, "");
            //  dt = objTask.GetCreateTaskRequestSelect(Session["Facility"].ToString(), BenchLoc, "");

            DataTable dtManual = objTask.GetManualRequestStart1(Session["Facility"].ToString(), BenchLoc, "'" + JobCode + "'");

            if (dt != null && dt.Rows.Count > 0 && dtManual != null && dtManual.Rows.Count > 0)
            {
                dt.Merge(dtManual);
                dt.AcceptChanges();
                gvJobHistory.DataSource = dt;
                gvJobHistory.DataBind();

            }
            else if (dtManual != null && dtManual.Rows.Count > 0)
            {
                gvJobHistory.DataSource = dtManual;
                gvJobHistory.DataBind();
            }
            else
            {
                gvJobHistory.DataSource = dt;
                gvJobHistory.DataBind();
            }
            decimal tray = 0;
            foreach (GridViewRow row in gvJobHistory.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                tray = tray + Convert.ToDecimal((row.FindControl("lblTotTray1") as Label).Text);
                //}

            }
            txtTrays.Text = tray.ToString();
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int FertilizationCode = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Mode", "12");
            dt = objCommon.GetDataTable("GET_Common", nv1);
            FertilizationCode = Convert.ToInt32(dt.Rows[0]["FCode"]);

            foreach (GridViewRow row in gvFer.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{

                long result = 0;
                long Mresult = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", Session["LoginID"].ToString());
                nv.Add("@Type", "Fertilizer");
                nv.Add("@WorkOrder", (row.FindControl("lblwo") as Label).Text);
                nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                //nv.Add("@WorkOrder", lblwo.Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@FertilizationCode", FertilizationCode.ToString());
                nv.Add("@FertilizationDate", txtDate.Text);
                nv.Add("@Jid", Jid);
                nv.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                nv.Add("@seedDate", (row.FindControl("lblSeededDate1") as Label).Text);

                result = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManualCreateTaskStartJobbuil", nv);
                FR_ID = result.ToString();

                //NameValueCollection nv123 = new NameValueCollection();
                //nv123.Add("@Banchloc", lblbench.Text);
                //Mresult = objCommon.GetDataInsertORUpdate("SP_AddFertilizerRequestMenualUpdateBanch", nv123);
                //  }
                //NameValueCollection nvn = new NameValueCollection();
                //nvn.Add("@LoginID", Session["LoginID"].ToString());
                //nvn.Add("@SupervisorID", Session["LoginID"].ToString());
                //nvn.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                //nvn.Add("@TaskName", "Fertilizer");
                //nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                //var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);
            }

            foreach (GridViewRow row in gvJobHistory.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                //if ((row.FindControl("lblGrowerputawayID") as Label).Text == "0")
                //{
                //    long result = 0;
                //    NameValueCollection nv = new NameValueCollection();
                //    nv.Add("@SupervisorID", Session["LoginID"].ToString());
                //    nv.Add("@Type", "Fertilizer");
                //    nv.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                //    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                //    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                //    nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                //    nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                //    nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                //    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                //    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                //    //nv.Add("@WorkOrder", lblwo.Text);
                //    nv.Add("@LoginID", Session["LoginID"].ToString());
                //    nv.Add("@FertilizationCode", FertilizationCode.ToString());
                //    nv.Add("@FertilizationDate", txtDate.Text);

                //    result = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManual", nv);
                //}
                //else
                //{

                long result = 0;
                NameValueCollection nv = new NameValueCollection();

                nv.Add("@SupervisorID", Session["LoginID"].ToString());
                nv.Add("@Type", "Fertilizer");
                nv.Add("@WorkOrder", (row.FindControl("lblwo1") as Label).Text);
                nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID1") as Label).Text);
                //nv.Add("@WorkOrder", lblwo.Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@FertilizationCode", FertilizationCode.ToString());
                nv.Add("@FertilizationDate", txtDate.Text);
                nv.Add("@Jid", Jid);
                nv.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                nv.Add("@Facility", (row.FindControl("lblFacility1") as Label).Text);
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray1") as Label).Text);
                nv.Add("@seedDate", (row.FindControl("lblSeededDate1") as Label).Text);
                result = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManualCreateTaskStartJobbuil", nv);

            }

            dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text, "", txtTrays.Text, txtSQFT.Text);
            objTask.AddFertilizerRequestDetailsCreatTask(dtTrays, FR_ID, FertilizationCode, lblbench.Text, "", "", "", txtResetSprayTaskForDays.Text, txtComments.Text);

            long Mresult1 = 0;
            NameValueCollection nv123 = new NameValueCollection();
            nv123.Add("@BanchLocation", lblbench.Text);
            Mresult1 = objCommon.GetDataInsertORUpdate("SP_AddFertilizerRequestMenualUpdate", nv123);

            GridViewRow rowG = gvFer.Rows[0];
            var txtJobNo = (rowG.FindControl("lblID") as Label).Text;
            var txtBenchLocation = (rowG.FindControl("lblGreenHouse") as Label).Text;

            NameValueCollection nameValue = new NameValueCollection();
            nameValue.Add("@LoginID", Session["LoginID"].ToString());
            nameValue.Add("@jobcode", txtJobNo);
            nameValue.Add("@GreenHouseID", txtBenchLocation);
            nameValue.Add("@TaskName", "Fertilizer");

            var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);
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
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

            Clear();            

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

        //public void BindChemical()
        //{
        //    NameValueCollection nv = new NameValueCollection();
        //    ddlFertilizer.DataSource = objFer.GetChemicalList();
        //    ddlFertilizer.DataTextField = "Name";
        //    ddlFertilizer.DataValueField = "No_";
        //    ddlFertilizer.DataBind();
        //    ddlFertilizer.Items.Insert(0, new ListItem("--- Select ---", "0"));
        //}

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
    }
}
﻿using Evo.BAL_Classes;
using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Evo
{
    public partial class CreateTask : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();
        static string ReceiverEmail = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                Bindcname();
                BindBenchLocation(Session["Facility"].ToString());
                dtTrays.Clear();

                BindSupervisor();


                BindSupervisorList();
                BindFertilizer();
                BindJobCode("");
                BindChemical();
                // txtDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
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
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", ""));
        }

        public void BindBenchLocation(string ddlMain)
        {
            ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataTextField = "p2";
            ddlBenchLocation.DataValueField = "p2";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", ""));
        }


        private string Bench1
        {
            get
            {
                if (ViewState["Bench1"] != null)
                {
                    return (string)ViewState["Bench1"];
                }
                return "";
            }
            set
            {
                ViewState["Bench1"] = value;
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

                string YourString = Bench1;

                YourString = YourString.Remove(YourString.Length - 1);

                DataTable dt12 = objFer.GetSelectBench(YourString);

                lblBench1.Text = dt12.Rows[0]["PositionCode"].ToString();


                if (dt12.Rows.Count > 0)
                {
                    DataColumn col = dt12.Columns["PositionCode"];
                    foreach (DataRow row in dt12.Rows)
                    {
                        //strJsonData = row[col].ToString();

                        P1 = 1;
                        Q1 += "'" + row[col].ToString() + "',";
                    }
                }

                if (P1 > 0)
                {
                    chkSelected = Q1.Remove(Q1.Length - 1, 1);

                }
                else
                {

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
                string[] words = Regex.Split(Bench1, @"\W+");

                DataTable dt = objFer.GetSelectBenchLocation(words[0], words[1]);

                if (dt.Rows.Count > 0)
                {
                    DataColumn col = dt.Columns["PositionCode"];
                    foreach (DataRow row in dt.Rows)
                    {
                        //strJsonData = row[col].ToString();

                        P = 1;
                        Q += "'" + row[col].ToString() + "',";
                    }
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

            }

            DataTable dt85 = new DataTable();
            gvFer.DataSource = dt85;
            gvFer.DataBind();

            BindGridFerReq(chkSelected, "");
            BindSQFTofBench(chkSelected);
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

            BindGridFerReq(chkSelected, "");
            BindSQFTofBench(chkSelected);
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            RadioBench.Items[0].Selected = false;
            ListBoxBenchesInHouse.Items.Clear();
            PanelBench.Visible = false;
            PanelBenchesInHouse.Visible = false;
            //To unselect all Items
            RadioBench.ClearSelection();
            BindGridFerReq("'" + ddlBenchLocation.SelectedValue + "'", "");
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
                int P = 0;
                string Q = "";
                string[] words = Regex.Split(Bench1, @"\W+");

                DataTable dt = objFer.GetSelectBenchLocation(words[0], words[1]);

                if (dt.Rows.Count > 0)
                {
                    DataColumn col = dt.Columns["PositionCode"];
                    foreach (DataRow row in dt.Rows)
                    {
                        //strJsonData = row[col].ToString();

                        P = 1;
                        Q += "'" + row[col].ToString() + "',";
                    }
                }

                if (P > 0)
                {
                    chkSelected = Q.Remove(Q.Length - 1, 1);

                }
                else
                {

                }

            }

            BindGridFerReq(chkSelected, "");


        }

        public void SelectBench()
        {
            string YourString = Bench1;

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

            string[] words = Regex.Split(Bench1, @"\W+");

            DataTable dt = objFer.GetSelectBenchLocation(words[0], words[1]);

            ListBoxBenchesInHouse.DataSource = dt;
            ListBoxBenchesInHouse.DataTextField = "PositionCode";
            ListBoxBenchesInHouse.DataValueField = "PositionCode";
            ListBoxBenchesInHouse.DataBind();

        }


        public void BindGridFerReq(string BenchLoc, string jobNo)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", BenchLoc);
            dt = objCommon.GetDataTable("SP_GetFertilizerRequestDetails", nv);

            DataTable dtManual = objFer.GetManualFertilizerRequestSelect(Session["Facility"].ToString(), BenchLoc, jobNo);
            if (dtManual != null && dtManual.Rows.Count > 0)
            {
                dt.Merge(dtManual);
                dt.AcceptChanges();
            }
            gvFer.DataSource = dt;
            gvFer.DataBind();


            decimal tray = 0;
            string BatchLocd = string.Empty;
            foreach (GridViewRow row in gvFer.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                tray = tray + Convert.ToDecimal((row.FindControl("lblTotTray") as Label).Text);
                //}
                // BatchLocd = (row.FindControl("lblGreenHouse1") as Label).Text;
            }
            txtTGerTrays.Text = "10";
            txtFTrays.Text = tray.ToString();

            //   BindSQFTofBench(BatchLocd);


        }


        protected void gvFer_RowCommand(object sender, GridViewCommandEventArgs e)
        {


        }




        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            ddlBenchLocation.SelectedIndex = 0;
            ddlCustomer.SelectedIndex = 0;
            ddlJobNo.SelectedIndex = 0;

            dtTrays.Clear();
        }





        protected void chckchanged(object sender, EventArgs e)
        {
            CheckBox chckheader = (CheckBox)gvFer.HeaderRow.FindControl("CheckBoxall");
            foreach (GridViewRow row in gvFer.Rows)
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




        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    BindGridFerReq();
        //}
        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            ddlBenchLocation.SelectedIndex = 0;
            ddlCustomer.SelectedIndex = 0;
            ddlJobNo.SelectedIndex = 0;
            //BindGridFerReq();
            gvFer.DataSource = null;
            gvFer.DataBind();
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq("", "");
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq("", ddlJobNo.SelectedValue);
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindJobCode(ddlBenchLocation.SelectedValue);

            if (ddlBenchLocation.SelectedValue == "")
            {
                Panel_Bench.Visible = false;
            }
            else
            {
                Panel_Bench.Visible = true;
                Bench1 = ddlBenchLocation.SelectedItem.Text;
                BindGridFerReq("'" + Bench1 + "'", "");
                BindSQFTofBench("'" + Bench1 + "'");
            }

        }


        //---------------------------------------------------------------- Tab Details-------
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
        public void BindSupervisor()
        {


            NameValueCollection nv = new NameValueCollection();

            DataTable dt = new DataTable();
            if (Session["Role"].ToString() == "1")
            {
                dt = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
            }
            else if (Session["Role"].ToString() == "12")
            {
                dt = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
            }
            else
            { }


            ddlgerminationSupervisor.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlgerminationSupervisor.DataTextField = "EmployeeName";
            ddlgerminationSupervisor.DataValueField = "ID";
            ddlgerminationSupervisor.DataBind();
            ddlgerminationSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlFertilizationSupervisor.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlFertilizationSupervisor.DataTextField = "EmployeeName";
            ddlFertilizationSupervisor.DataValueField = "ID";
            ddlFertilizationSupervisor.DataBind();
            ddlFertilizationSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlirrigationSupervisor.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlirrigationSupervisor.DataTextField = "EmployeeName";
            ddlirrigationSupervisor.DataValueField = "ID";
            ddlirrigationSupervisor.DataBind();
            ddlirrigationSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlplant_readySupervisor.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlplant_readySupervisor.DataTextField = "EmployeeName";
            ddlplant_readySupervisor.DataValueField = "ID";
            ddlplant_readySupervisor.DataBind();
            ddlplant_readySupervisor.Items.Insert(0, new ListItem("--Select--", "0"));


        }
        public void BindChemical()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlChemical.DataSource = objFer.GetChemicalList();
            ddlChemical.DataTextField = "Name";
            ddlChemical.DataValueField = "No_";
            ddlChemical.DataBind();
            ddlChemical.Items.Insert(0, new ListItem("--- Select ---", "0"));
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



        protected void btnFReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnFSubmit_Click(object sender, EventArgs e)
        {
            string Batchlocation = "";
            int FertilizationCode = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv14 = new NameValueCollection();
            NameValueCollection nvimg = new NameValueCollection();
            nv14.Add("@Mode", "12");
            dt = objCommon.GetDataTable("GET_Common", nv14);
            FertilizationCode = Convert.ToInt32(dt.Rows[0]["FCode"]);




            foreach (GridViewRow row in gvFer.Rows)
            {

                long result2 = 0;
                NameValueCollection nv4 = new NameValueCollection();
                nv4.Add("@SupervisorID", ddlFertilizationSupervisor.SelectedValue);
                nv4.Add("@Type", "Fertilizer");
                nv4.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                nv4.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                nv4.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                nv4.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                nv4.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                nv4.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                nv4.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                nv4.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                //nv.Add("@WorkOrder", lblwo.Text);
                nv4.Add("@LoginID", Session["LoginID"].ToString());
                nv4.Add("@FertilizationCode", FertilizationCode.ToString());
                nv4.Add("@FertilizationDate", txtFDate.Text);
                result2 = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManual", nv4);
                Batchlocation = (row.FindControl("lblGreenHouse") as Label).Text;

            }

            dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text, "", txtFTrays.Text, txtSQFT.Text);

            objTask.AddFertilizerRequestDetails(dtTrays, "0", FertilizationCode, Batchlocation, txtBenchIrrigationFlowRate.Text, txtBenchIrrigationCoverage.Text, txtSprayCoverageperminutes.Text, txtResetSprayTaskForDays.Text);

            string message = "Assignment Successful";
            string url = "MyTaskGrower.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

        }

        protected void btngerminationSumit_Click(object sender, EventArgs e)
        {



            long result16 = 0;

            foreach (GridViewRow row in gvFer.Rows)
            {
                //NameValueCollection nv = new NameValueCollection();
                //nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                //nv.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                //nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                //nv.Add("@Facility", "");
                //nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                //nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                //nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                //nv.Add("@Seeddate", (row.FindControl("lblSeededDate") as Label).Text);
                //nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                //nv.Add("@SupervisorID", ddlgerminationSupervisor.SelectedValue);
                //nv.Add("@InspectionDueDate", txtGerDate.Text);
                //nv.Add("@TraysInspected", txtTGerTrays.Text);
                //nv.Add("@Chid", "");
                //nv.Add("@LoginId", Session["LoginID"].ToString());

                //result16 = objCommon.GetDataInsertORUpdate("SP_AddCropHealthGerminationReques", nv);

                NameValueCollection nv = new NameValueCollection();
                nv.Add("@Customer", "");
                nv.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                nv.Add("@Seeddate", (row.FindControl("lblSeededDate") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                nv.Add("@SupervisorID", ddlgerminationSupervisor.SelectedValue);
                nv.Add("@InspectionDueDate", txtGerDate.Text);
                nv.Add("@TraysInspected", txtTGerTrays.Text);

                nv.Add("@LoginId", Session["LoginID"].ToString());

                result16 = objCommon.GetDataInsertORUpdate("SP_AddGerminationRequesMenualDetails", nv);


            }

            if (result16 > 0)
            {


                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
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
                //  clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }
        }

        protected void btngerminationReset_Click(object sender, EventArgs e)
        {

        }

        //protected void ddlAssignments_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    NameValueCollection nv = new NameValueCollection();
        //    nv.Add("@Uid", ddlAssignments.SelectedValue);
        //    DataTable dt = objCommon.GetDataTable("getReceiverEmail", nv);
        //    ReceiverEmail = dt.Rows[0]["Email"].ToString();
        //}
        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                long result = 0;

                NameValueCollection nv = new NameValueCollection();
                // nv.Add("@OperatorID", Session["LoginID"].ToString());
                //nv.Add("@wo", wo);
                nv.Add("@Comments", txtgeneralCommnet.Text.Trim());
                nv.Add("@AsssigneeID", ddlAssignments.SelectedValue);
                nv.Add("@TaskType", ddlTaskType.SelectedValue);
                nv.Add("@MoveFrom", txtFrom.Text.Trim());
                nv.Add("@MoveTo", txtTo.Text.Trim());
                nv.Add("@IsActive", "1");


                result = objCommon.GetDataInsertORUpdate("InsertGeneralTask", nv);
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                string FromMail = WebConfigurationManager.AppSettings["FromEmail"];
                string FromEmailPassword = WebConfigurationManager.AppSettings["FromEmailPassword"];
                smtpClient.Credentials = new System.Net.NetworkCredential(FromMail, FromEmailPassword);
                // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.Subject = "Crop Health Report";
                mail.Body = "Crop Health Report Comments:" + "";
                //Setting From , To and CC

                mail.From = new MailAddress(FromMail);
                mail.To.Add(new MailAddress(ReceiverEmail));
                //  Attachment atc = new Attachment(folderPath, "Uploded Picture");
                //   mail.Attachments.Add(atc);
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void BindSupervisorList()
        {
            NameValueCollection nv = new NameValueCollection();

            ddlAssignments.DataSource = objCommon.GetDataTable("SP_GetSeedsRoles", nv);
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlAssignments.DataTextField = "EmployeeName";
            ddlAssignments.DataValueField = "ID";
            ddlAssignments.DataBind();
            ddlAssignments.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void ddlAssignments_SelectedIndexChanged(object sender, EventArgs e)
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Uid", ddlAssignments.SelectedValue);
            DataTable dt = objCommon.GetDataTable("getReceiverEmail", nv);
            ReceiverEmail = dt.Rows[0]["Email"].ToString();
        }

        protected void btnirrigationReset_Click1(object sender, EventArgs e)
        {

        }

        protected void btnirrigationSubmit_Click(object sender, EventArgs e)
        {
            int IrrigationCode = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv17 = new NameValueCollection();
            NameValueCollection nvimg = new NameValueCollection();
            nv17.Add("@Mode", "13");
            dt = objCommon.GetDataTable("GET_Common", nv17);
            IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);



            foreach (GridViewRow row in gvFer.Rows)
            {

                long result16 = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", ddlirrigationSupervisor.SelectedValue);

                nv.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);

                nv.Add("@IrrigationCode", IrrigationCode.ToString());
                // nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                nv.Add("@IrrigatedNoTrays", (row.FindControl("lblTotTray") as Label).Text);
                nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                nv.Add("@IrrigationDuration", "");
                nv.Add("@SprayDate", txtirrigationSprayDate.Text.Trim());
                //nv.Add("@SprayTime", txtSprayTime.Text.Trim());
                nv.Add("@Nots", txtirrigationNotes.Text.Trim());
                nv.Add("@LoginID", Session["LoginID"].ToString());
                result16 = objCommon.GetDataExecuteScaler("SP_AddIrrigationRequestManual", nv);


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
        }

        protected void btnplant_readySubmit_Click(object sender, EventArgs e)
        {
            int IrrigationCode = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv11 = new NameValueCollection();
            nv11.Add("@Mode", "13");
            dt = objCommon.GetDataTable("GET_Common", nv11);
            IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);



            foreach (GridViewRow row in gvFer.Rows)
            {

                long result = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", ddlplant_readySupervisor.SelectedValue);

                nv.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@ChId", "0");

                result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyRequestManuaNew", nv);


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
        }

        protected void btnplant_readyReset_Click(object sender, EventArgs e)
        {

        }




        protected void ddlTaskType_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        protected void btnSearchDet_Click(object sender, EventArgs e)
        {
            BindGridFerReq("", txtSearchJobNo.Text);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            txtSearchJobNo.Text = "";
            txtSearchJobNo.Text = "JB";
            BindGridFerReq("", txtSearchJobNo.Text);
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["EvoNavision"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //and t.[Location Code]= '" + Session["Facility"].ToString() + "'
                    cmd.CommandText = "select distinct t.[Job No_] as jobcode  from[GTI$IA Job Tracking Entry] t, [GTI$Job] j where j.No_ = t.[Job No_] and j.[Job Status] = 2  " +
                    " AND t.[Job No_] like '" + prefixText + "%'";
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
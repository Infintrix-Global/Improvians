using Evo.BAL_Classes;
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
        public static string JobCode;
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };

        public static DataTable dtCTrays = new DataTable()
        { Columns = { "Fertilizer", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();
        clsCommonMasters objCom = new clsCommonMasters();
        General objGeneral = new General();
        BAL_CommonMasters objCOm = new BAL_CommonMasters();
        static string ReceiverEmail = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtSearchJobNo.Focus();
                Bindcname();
                BindBenchLocation(Session["Facility"].ToString());
                dtTrays.Clear();
                BintTaskType();
                BindSupervisor();

                //BindFacility();
                BindSupervisorList();
                BindFertilizer();
                BindJobCode("");
                BindChemical();
                BindBench_Location();
                txtGerDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtFDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtChemicalSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtMoveDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtirrigationSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                //     txtPlantDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtDumpDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtgeneralDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtCropHealthDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");

                if (Request.QueryString["jobCode"] != null)
                {
                    txtSearchJobNo.Text = Request.QueryString["jobCode"].ToString();
                    BindGridFerReq("", txtSearchJobNo.Text);
                    string ViewD = Request.QueryString["View"].ToString();

                    if (ViewD == "Germination")
                    {
                        germination_count.Attributes.Add("class", "request__block-collapse collapse show");
                        btngermination.Attributes.Add("class", "request__block-head");
                    }
                    else if (ViewD == "Fertilization")
                    {
                        fertilization_count.Attributes.Add("class", "request__block-collapse collapse show");
                        btnFertilization.Attributes.Add("class", "request__block-head");
                    }
                    else if (ViewD == "Chemical")
                    {
                        Chemical_count.Attributes.Add("class", "request__block-collapse collapse show");
                        btnChemical.Attributes.Add("class", "request__block-head");
                    }
                    else if (ViewD == "Irrigation")
                    {
                        irrigation_count.Attributes.Add("class", "request__block-collapse collapse show");
                        btnIrrigation.Attributes.Add("class", "request__block-head");
                    }
                    else if (ViewD == "PlantReady")
                    {
                        plant_ready_count.Attributes.Add("class", "request__block-collapse collapse show");
                        btnPlantReady.Attributes.Add("class", "request__block-head");
                    }
                    else if (ViewD == "Move")
                    {
                        btnMoveRequest.Attributes.Add("class", "request__block-collapse collapse show");
                    }
                    else if (ViewD == "Dump")
                    {
                        dump_request.Attributes.Add("class", "request__block-collapse collapse show");
                        btnDump.Attributes.Add("class", "request__block-head");
                    }
                    else if (ViewD == "GeneralTask")
                    {
                        general_task_request.Attributes.Add("class", "request__block-collapse collapse show");
                        btnGeneral_Task.Attributes.Add("class", "request__block-head");
                    }
                    else
                    {

                    }

                }
            }
        }


    





        public void Bindcname()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetCustomer", nv);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "cname";
            ddlCustomer.DataValueField = "cname";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--Select--", ""));

        }

        public void BindJobCode(string ddlBench)
        {
            DataTable dt = new DataTable();

            NameValueCollection nv = new NameValueCollection();

            nv.Add("@BatchLocation", ddlBench);
            nv.Add("@Mode", "1");
            dt = objCommon.GetDataTable("SP_GeJobNo", nv);


            ddlJobNo.Items.Clear();
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", ""));



            //   DataTable dtNV = objBAL.GetJobsForBenchLocation(ddlBench);


            //if (dt != null && dt.Rows.Count > 0 && dtNV != null && dtNV.Rows.Count > 0)
            //{
            //    dt.Merge(dtNV);
            //    dt.AcceptChanges();
            //    ddlJobNo.Items.Clear();
            //    ddlJobNo.DataSource = dt;
            //    ddlJobNo.DataTextField = "Jobcode";
            //    ddlJobNo.DataValueField = "Jobcode";
            //    ddlJobNo.DataBind();
            //    ddlJobNo.Items.Insert(0, new ListItem("--Select--", ""));
            //}
            //else
            //{
            //    ddlJobNo.Items.Clear();
            //    ddlJobNo.DataSource = dtNV;
            //    ddlJobNo.DataTextField = "Jobcode";
            //    ddlJobNo.DataValueField = "Jobcode";
            //    ddlJobNo.DataBind();
            //    ddlJobNo.Items.Insert(0, new ListItem("--Select--", ""));
            //}



        }

        public void BindBenchLocation(string ddlMain)
        {

            //DataTable dt = new DataTable();

            //NameValueCollection nv = new NameValueCollection();

            //nv.Add("@FacilityID", ddlMain);

            //dt = objCommon.GetDataTable("SP_GetBatchLocation", nv);

            //DataTable dtNV = objCom.GetLocationDetsil(ddlMain);

            DataTable dtNV = objCOm.GetLocation(ddlMain);


            ddlBenchLocation.DataSource = dtNV;
            ddlBenchLocation.DataTextField = "BenchName";
            ddlBenchLocation.DataValueField = "BenchName";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", ""));

            //if (dt != null && dt.Rows.Count > 0 && dtNV != null && dtNV.Rows.Count > 0)
            //{
            //    dt.Merge(dtNV);
            //    dt.AcceptChanges();
            //    ddlBenchLocation.DataSource = dt;
            //    ddlBenchLocation.DataTextField = "p2";
            //    ddlBenchLocation.DataValueField = "p2";
            //    ddlBenchLocation.DataBind();
            //    ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", ""));
            //}
            //else
            //{
            //ddlBenchLocation.DataSource = dtNV;
            //    ddlBenchLocation.DataTextField = "p2";
            //    ddlBenchLocation.DataValueField = "p2";
            //    ddlBenchLocation.DataBind();
            //    ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", ""));
            // }



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
                DataTable dt85 = new DataTable();
                gvFer.DataSource = dt85;
                gvFer.DataBind();

                BindGridFerReq(chkSelected, "");
                BindSQFTofBench(chkSelected);
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
                DataTable dt85 = new DataTable();
                gvFer.DataSource = dt85;
                gvFer.DataBind();

                BindGridFerReq(chkSelected, "");
                BindSQFTofBench(chkSelected);
            }
            else
            {

            }


        }

        protected void ListBoxBenchesInHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            int c = 0;
            string x = "'" + Bench1 + "'" + ",";
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
            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@BenchLocation",BenchLoc);
            // dt = objCommon.GetDataTable("SP_GetFertilizerRequestDetails", nv);
            dt = objTask.GetCreateTaskRequestSelectNew(Session["Facility"].ToString(), BenchLoc, jobNo, ddlCustomer.SelectedValue);
            //  DataTable dtManual = objFer.GetManualFertilizerRequestSelect(Session["Facility"].ToString(), BenchLoc, jobNo);

            DataTable dtManual = objTask.GetManualRequestSelectNew(Session["Facility"].ToString(), BenchLoc, jobNo, ddlCustomer.SelectedValue);

            if (dt != null && dt.Rows.Count > 0 && dtManual != null && dtManual.Rows.Count > 0)
            {
                dt.Merge(dtManual);
                dt.AcceptChanges();
                gvFer.DataSource = dt;
                gvFer.DataBind();
                BindSlotSelect(dt.Rows[0]["GreenHouseID"].ToString(), dt.Rows[0]["GrowerPutAwayId"].ToString());

            }
            else if (dtManual != null && dtManual.Rows.Count > 0)
            {
                gvFer.DataSource = dtManual;
                gvFer.DataBind();

                BindSlotSelect(dtManual.Rows[0]["GreenHouseID"].ToString(), dtManual.Rows[0]["GrowerPutAwayId"].ToString());

            }
            else
            {
                gvFer.DataSource = dt;
                gvFer.DataBind();

                BindSlotSelect(dt.Rows[0]["GreenHouseID"].ToString(), dt.Rows[0]["GrowerPutAwayId"].ToString());

            }



            decimal tray = 0;
            string BatchLocd = string.Empty;
            string x = "'" + Bench1 + "'" + ",";
            string chkSelected = "";
            int c = 0;
            foreach (GridViewRow row in gvFer.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                tray = tray + Convert.ToDecimal((row.FindControl("lblTotTray") as Label).Text);
                //}
                BatchLocd = (row.FindControl("lblGreenHouse") as Label).Text;


                c = 1;
                x += "'" + BatchLocd + "',";


            }


            if (c > 0)
            {
                chkSelected = x.Remove(x.Length - 1, 1);

            }
            else
            {

            }



            txtTGerTrays.Text = "10";
            txtFTrays.Text = tray.ToString();
            txtChemicalTrays.Text = tray.ToString();

            if (chkSelected != "")
            {
                BindSQFTofBench(chkSelected);
            }
        }


        public void BindSlotSelect(string bench,string GPId)
        {



            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();

            nv1.Add("@BanchLocation", bench);
            nv1.Add("@Facility", "");
            nv1.Add("@Mode", "3");
            dt1 = objCommon.GetDataTable("SP_GetBanchLocation", nv1);




            if (dt1 != null && dt1.Rows.Count > 0)
            {
                int TotalTrays = 0;
                decimal availableSlot = 0;

                if (dt1.Rows[0]["Automation"].ToString() == "Auto")
                {


                    for (double i = 1; i < 53; i++)
                    {
                        ListItem item = new ListItem();
                        item.Text = i.ToString();
                        item.Value = i.ToString();

                        ddlSlotPositionStart.Items.Add(item);
                    }

                    for (double i = 1; i < 53; i++)
                    {
                        ListItem item = new ListItem();
                        item.Text = i.ToString();
                        item.Value = i.ToString();

                        ddlSlotPositionEnd.Items.Add(item);
                    }
                    ddlLogisticManager.Items.Clear();
                    NameValueCollection nv = new NameValueCollection();
                    DataTable dt = new DataTable();
                    nv.Add("@RoleID", "16");
                    nv.Add("@Facility", Session["Facility"].ToString());
                    dt = objCommon.GetDataTable("SP_GetRoleForAssignementFacilityNew", nv);

                    ddlLogisticManager.DataSource = dt;
                    //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                    ddlLogisticManager.DataTextField = "EmployeeName";
                    ddlLogisticManager.DataValueField = "ID";
                    ddlLogisticManager.DataBind();
                    ddlLogisticManager.Items.Insert(0, new ListItem("--Select--", "0"));



                    //DataTable dtSlot = new DataTable();
                    //NameValueCollection nvSlot = new NameValueCollection();
                    //nvSlot.Add("@GrowerPutAwayId", GPId.ToString());

                    //dtSlot = objCommon.GetDataTable("SP_GetGrowerPutAwaySlotPositionSelect", nvSlot);

                    //if (dtSlot != null && dtSlot.Rows.Count > 0)
                    //{
                    //    ddlSlotPositionStart.SelectedValue = Convert.ToInt32(dtSlot.Rows[0]["SlotPositionStart"]).ToString();
                    //    ddlSlotPositionEnd.SelectedValue = Convert.ToInt32(dtSlot.Rows[0]["SlotPositionEnd"]).ToString();
                    //}
                }
                else
                {

                    SlotStart.Visible = false;
                    SlotEnd.Visible = false;
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



                    for (double i = 0.5; i < 54; i += 0.5)
                    {
                        ListItem item = new ListItem();
                        item.Text = i.ToString();
                        item.Value = i.ToString();


                        ddlSlotPositionStart.Items.Add(item);
                    }

                    for (double i = 0.5; i < 54; i += 0.5)
                    {
                        ListItem item = new ListItem();
                        item.Text = i.ToString();
                        item.Value = i.ToString();

                        ddlSlotPositionEnd.Items.Add(item);
                    }



                }

               


            }





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

            gvFer.DataSource = null;
            gvFer.DataBind();

            dtTrays.Clear();
        }
        public void ClearGeneral()
        {
            ddlAssignments.SelectedValue = "0";
            ddlTaskType.SelectedValue = "0";
            //  txtgeneralDate.Text = DateTime.Now.ToString("yyyy/mm/dd");
            txtgeneralDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            txtgeneralComment.Text = "";
            txtFrom.Text = "";
            txtTo.Text = "";
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
            BindGridFerReq("'" + ddlBenchLocation.SelectedValue + "'", ddlJobNo.SelectedValue);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq("'" + ddlBenchLocation.SelectedValue + "'", ddlJobNo.SelectedValue);
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearchJobNo.Text = "";
            txtBatchLocation.Text = "";
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

               // BindSlotSelect(Bench1);


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
                txtChemicalSQFTofBench.Text = Convert.ToDecimal(dtSQFT.Rows[0]["Sqft"]).ToString("#,0000.00");
            }
            else
            {
                txtSQFT.Text = "0.00";
                txtChemicalSQFTofBench.Text = "0.00";
            }


        }
        public void BindSupervisor()
        {


            NameValueCollection nv = new NameValueCollection();

            DataTable dt = new DataTable();
            //if (Session["Role"].ToString() == "1")
            //{
            //    dt = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
            //    //   dt = objCommon.GetDataTable("SP_GetRoleForGrowerNew", nv);

            //}
            //else if (Session["Role"].ToString() == "12")
            //{
            //    dt = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
            //}
            //else
            //{
            //    nv.Add("@RoleID", "3");
            //    dt = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;

            //}

            nv.Add("@RoleID", Session["Role"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetRoleForAssignementFacility", nv); ;


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

            ddlChemical_supervisor.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlChemical_supervisor.DataTextField = "EmployeeName";
            ddlChemical_supervisor.DataValueField = "ID";
            ddlChemical_supervisor.DataBind();
            ddlChemical_supervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlLogisticManager.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlLogisticManager.DataTextField = "EmployeeName";
            ddlLogisticManager.DataValueField = "ID";
            ddlLogisticManager.DataBind();
            ddlLogisticManager.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlDumptAssignment.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlDumptAssignment.DataTextField = "EmployeeName";
            ddlDumptAssignment.DataValueField = "ID";
            ddlDumptAssignment.DataBind();
            ddlDumptAssignment.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlCropHealthAssignment.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlCropHealthAssignment.DataTextField = "EmployeeName";
            ddlCropHealthAssignment.DataValueField = "ID";
            ddlCropHealthAssignment.DataBind();
            ddlCropHealthAssignment.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        public void BindChemical()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlChemical.DataSource = objFer.GetChemicalList();
            ddlChemical.DataTextField = "Name";
            ddlChemical.DataValueField = "No_";
            ddlChemical.DataBind();
            ddlChemical.Items.Insert(0, new ListItem("--- Select ---", "0"));

            ddlMethod.DataSource = objCom.GetAllChemicalList();
            ddlMethod.DataTextField = "ChemicalName";
            ddlMethod.DataValueField = "ChemicalName";
            ddlMethod.DataBind();
            ddlMethod.Items.Insert(0, new ListItem("--- Select ---", "0"));

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


        public void BintTaskType()
        {

            NameValueCollection nv = new NameValueCollection();

            DataTable dt = new DataTable();

            nv.Add("@mode", "22");

            dt = objCommon.GetDataTable("GET_Common", nv);

            ddlTaskType.DataSource = dt;
            ddlTaskType.DataTextField = "TaskType";
            ddlTaskType.DataValueField = "ID";
            ddlTaskType.DataBind();
            ddlTaskType.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }




        protected void btnFReset_Click(object sender, EventArgs e)
        {

            txtResetSprayTaskForDays.Text = "";
            txtFComments.Text = "";
            BindFertilizer();
            BindSupervisor();
            txtQty.Text = "";
            txtFDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");

        }





        public void FertilizationSubmit(string Assigned, int IsF)
        {



            string Batchlocation = "";
            string Batchlocation1 = "";
            string Batchlocation2 = "";
            string TaskRequestKey = "";
            int FertilizationCode = 0;
            bool hasValue = false;
            string SprayTaskForDaysDate = "";






            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");

                if (chckrw.Checked == true)
                {
                    hasValue = true;


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



                    long result2 = 0;
                    NameValueCollection nv4 = new NameValueCollection();
                    nv4.Add("@SupervisorID", Assigned);
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
                    nv4.Add("@seedDate", (row.FindControl("lblSeededDate") as Label).Text);
                    //  nv4.Add("@Jid", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    nv4.Add("@Jid", (row.FindControl("lblJIdPU") as Label).Text);
                    nv4.Add("@TaskRequestKey", TaskRequestKey);
                    nv4.Add("@ResetTaskForDays", SprayTaskForDaysDate);

                    //  nv4.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    result2 = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManualCreateTask", nv4);

                    if (Batchlocation2 == "" || Batchlocation2 != Batchlocation)
                    {
                        dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text, "", txtFTrays.Text, txtSQFT.Text);
                        objTask.AddFertilizerRequestDetailsCreatTask(dtTrays, result2.ToString(), FertilizationCode, Batchlocation, "", "", "", txtResetSprayTaskForDays.Text, txtFComments.Text.Trim(),txtNoOfPasses.Text);
                        Batchlocation2 = Batchlocation;

                        NameValueCollection nvn = new NameValueCollection();
                        nvn.Add("@LoginID", Session["LoginID"].ToString());
                        nvn.Add("@SupervisorID", Assigned);
                        nvn.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                        nvn.Add("@TaskName", "Fertilizer");
                        nvn.Add("@TaskRequestKey", TaskRequestKey);
                        nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                        var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);
                    }
                    objGeneral.SendMessage(int.Parse(Assigned), "New Fertilizer Task Assigned", "New Fertilizer Task Assigned", "Crop Health Report");




                    string message = "Assignment Successful";
                    string url = "CreateTask.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                    if (IsF == 1)
                    {
                        string BenchUp = "'" + Batchlocation + "'";
                        objTask.UpdateIsActiveDatat(BenchUp);
                    }
                    //string message1 = "$('#confirmModal').modal('show')";
                    //string script1 = "window.onload = function(){";
                    //script1 += message1;
                    //script1 += "'; }";
                    //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script1, true);





                    //$('#confirmModal').modal('show');

                    //NameValueCollection nvn = new NameValueCollection();
                    //nvn.Add("@LoginID", Session["LoginID"].ToString());
                    //nvn.Add("@SupervisorID", Assigned);
                    //nvn.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                    //nvn.Add("@TaskName", "Fertilizer");
                    //nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                    //var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);

                }
            }
            if (!hasValue)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Bench Location');", true);

            }
            else
            {

            }

            updateNotification();
        }

        protected void btnFSubmit_Click(object sender, EventArgs e)
        {
            FertilizationSubmit(ddlFertilizationSupervisor.SelectedValue, 0);
        }

        protected void btnSaveFLSubmit_Click(object sender, EventArgs e)
        {
            FertilizationSubmit(Session["LoginID"].ToString(), 0);
        }

        protected void btnChekFSubmit_Click(object sender, EventArgs e)
        {
            FertilizationSubmit(ddlFertilizationSupervisor.SelectedValue, 1);
        }

        protected void btnChekFCancel_Click(object sender, EventArgs e)
        {
            FertilizationSubmit(ddlFertilizationSupervisor.SelectedValue, 0);
        }
        public void GerminationSubmit(string Assigned)
        {
            long result16 = 0;
            bool hasValue = false;

            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    hasValue = true;
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
                    nv.Add("@SupervisorID", Assigned);
                    nv.Add("@InspectionDueDate", txtGerDate.Text);
                    nv.Add("@TraysInspected", txtTGerTrays.Text);
                    nv.Add("@LoginId", Session["LoginID"].ToString());
                    nv.Add("@Comments", txtGcomments.Text);
                    nv.Add("@Role", Session["Role"].ToString());
                    //  nv.Add("@Jid", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    nv.Add("@Jid", (row.FindControl("lblJIdPU") as Label).Text);
                    // nv.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    result16 = objCommon.GetDataInsertORUpdate("SP_AddGerminationRequesMenualDetailsCreateTask", nv);
                }
            }
            if (!hasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Bench Location');", true);
            }
            else
            {
                if (result16 > 0)
                {
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                    objGeneral.SendMessage(int.Parse(Assigned), "New Germination Task Assigned", "New Germination Task Assigned", "Germination");
                    string message = "Assignment Successful";
                    string url = "CreateTask.aspx";
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

            updateNotification();
        }

        protected void btngerminationSumit_Click(object sender, EventArgs e)
        {
            GerminationSubmit(ddlgerminationSupervisor.SelectedValue);
        }
        protected void btnBSaveSubmit_Click(object sender, EventArgs e)
        {
            GerminationSubmit(Session["LoginID"].ToString());
        }
        protected void btngerminationReset_Click(object sender, EventArgs e)
        {
            txtGerDate.Text = "";
            txtTGerTrays.Text = "";
            txtGcomments.Text = "";
            BindSupervisor();
            txtGerDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            //irrigation_count.Attributes.Add("class", "request__block-collapse collapse show");
            //btnIrrigation.Attributes.Add("class", "request__block-head");
            germination_count.Attributes.Add("class", "request__block-collapse collapse show");
            btngermination.Attributes.Add("class", "request__block-head");
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                long result = 0;

                NameValueCollection nv = new NameValueCollection();
                // nv.Add("@OperatorID", Session["LoginID"].ToString());
                //nv.Add("@wo", wo);
                nv.Add("@Comments", txtgeneralComment.Text.Trim());
                nv.Add("@AsssigneeID", Session["SelectedAssignment"].ToString());
                //nv.Add("@AsssigneeID", ddlAssignments.SelectedValue);

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

            nv.Add("@RoleID", Session["Role"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            DataTable dt = objCommon.GetDataTable("SP_GetRoleForAssignementFacility", nv); ;


            ddlAssignments.DataSource = dt;

            ddlAssignments.DataTextField = "EmployeeName";
            ddlAssignments.DataValueField = "ID";
            ddlAssignments.DataBind();
            ddlAssignments.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void ddlAssignments_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List<int> toBeSubmitted = new List<int>()
            //{
            //   2, 3 , 6, 11, 15
            //};
            //var val = Convert.ToInt32(ddlAssignments.SelectedValue);
            //// if (ddlAssignments.SelectedValue == "13" || ddlAssignments.SelectedValue == "11" || ddlAssignments.SelectedValue == "16")
            //if (!toBeSubmitted.Contains(val))
            //{
            //    btnSendMail.Visible = true;
            //    btnGeneraltask.Visible = false;
            //}
            //else
            //{
            //    btnSendMail.Visible = false;
            //    btnGeneraltask.Visible = true;
            //}

            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@Uid", ddlAssignments.SelectedValue);
            //DataTable dt = objCommon.GetDataTable("getReceiverEmail", nv);
            //ReceiverEmail = dt.Rows[0]["Email"].ToString();

            Session["SelectedAssignment"] = ddlAssignments.SelectedValue;


        }
        protected void btnirrigationReset_Click1(object sender, EventArgs e)
        {
            txtWaterRequired.Text = "";
            txtIrrComments.Text = "";
            txtirrigationResetSprayTaskForDays.Text = "";
            BindSupervisor();
            irrigation_count.Attributes.Add("class", "request__block-collapse collapse show");
            btnIrrigation.Attributes.Add("class", "request__block-head");
            txtirrigationSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
        }

        public void irrigationSubmit(string Assigned, int FID)
        {
            int IrrigationCode = 0;
            string SprayTaskForDaysDate = "";
            string Batchlocation1 = "";
            string Batchlocation2 = "";
            string TaskRequestKey = "";

            bool hasValue = false;


            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    long result16 = 0;
                    hasValue = true;

                    string TodatDate;
                    string ReSetSprayDate = "";
                    string Batchlocation = "";

                    Batchlocation = (row.FindControl("lblGreenHouse") as Label).Text;


                    if (Batchlocation1 == "" || Batchlocation1 != Batchlocation)
                    {
                        DataTable dt = new DataTable();
                        NameValueCollection nv17 = new NameValueCollection();
                        NameValueCollection nvimg = new NameValueCollection();
                        nv17.Add("@Mode", "13");
                        dt = objCommon.GetDataTable("GET_Common", nv17);
                        IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);

                        Batchlocation1 = Batchlocation;

                        string Countbanch = "";
                        DataTable dt15 = new DataTable();
                        NameValueCollection nv15 = new NameValueCollection();
                        nv15.Add("@Mode", "3");
                        nv15.Add("@GreenHouseID", Batchlocation1);
                        dt15 = objCommon.GetDataTable("SP_GetTaskRequestKeyBanchlocation", nv15);
                        Countbanch = dt15.Rows[0]["Countbanch"].ToString();

                        TaskRequestKey = Batchlocation + "_" + "Irrigation" + "_" + Countbanch;


                    }
                    else
                    {
                        IrrigationCode = IrrigationCode;

                    }


                    if (txtResetSprayTaskForDays.Text != "")
                    {
                        SprayTaskForDaysDate = (Convert.ToDateTime(System.DateTime.Now.ToShortDateString()).AddDays(Convert.ToInt32(txtResetSprayTaskForDays.Text))).ToString();

                    }
                    else
                    {
                        SprayTaskForDaysDate = System.DateTime.Now.ToShortDateString();

                    }




                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", Assigned);

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
                    nv.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);

                    nv.Add("@Nots", txtIrrComments.Text.Trim());
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@Role", Session["Role"].ToString());
                    //  nv4.Add("@Jid", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    nv.Add("@Jid", (row.FindControl("lblJIdPU") as Label).Text);
                    nv.Add("@TaskRequestKey", TaskRequestKey);
                    nv.Add("@ResetTaskForDays", SprayTaskForDaysDate);
                    //  nv4.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);

                    result16 = objCommon.GetDataExecuteScaler("SP_AddIrrigationRequestManualCreateTask", nv);

                    if (Batchlocation2 == "" || Batchlocation2 != Batchlocation)
                    {
                        var txtBenchLocation = (row.FindControl("lblGreenHouse") as Label).Text;

                        Batchlocation2 = Batchlocation;

                        NameValueCollection nvn = new NameValueCollection();
                        nvn.Add("@LoginID", Session["LoginID"].ToString());
                        nvn.Add("@SupervisorID", Assigned);
                        nvn.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                        nvn.Add("@TaskName", "Irrigation");
                        nvn.Add("@TaskRequestKey", TaskRequestKey);
                        nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                        var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);


                    }


                    objGeneral.SendMessage(int.Parse(Assigned), "New Irrigation Task Assigned", "New Irrigation Task Assigned", "Irrigation");
                    string message = "Assignment Successful";
                    string url = "CreateTask.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                    if (FID == 1)
                    {
                        string BenchUp = "'" + Batchlocation + "'";
                        objTask.UpdateIsActiveIrrigation(BenchUp);
                    }

                }
            }

            //if(RadioBench.SelectedValue != "")
            //{
            //    NameValueCollection nv1 = new NameValueCollection();

            //    nv1.Add("@BatchHouseType", RadioBench.SelectedValue);
            //    nv1.Add("@TypeOfTask", "Irrigation");
            //    nv1.Add("@TypeOfCode", IrrigationCode.ToString());
            //    objCommon.GetDataInsertORUpdate("SP_AddBatchLocationType", nv1);
            //}
            if (!hasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Bench Location');", true);
            }
            else
            {
            }
            updateNotification();
        }
        protected void btnirrigationSubmit_Click(object sender, EventArgs e)
        {
            irrigationSubmit(ddlirrigationSupervisor.SelectedValue, 0);
        }

        protected void btnSaveirrigation_Click(object sender, EventArgs e)
        {
            irrigationSubmit(Session["LoginID"].ToString(), 0);
        }

        protected void btnChekIrrigationCancel_Click_Click(object sender, EventArgs e)
        {
            irrigationSubmit(ddlirrigationSupervisor.SelectedValue, 0);
        }

        protected void btnChekIrrigationSubmit_Click_Click(object sender, EventArgs e)
        {
            irrigationSubmit(ddlirrigationSupervisor.SelectedValue, 1);
        }
        public void PlantReadySubmit(string Assigned)
        {
            int IrrigationCode = 0;

            DataTable dt = new DataTable();
            NameValueCollection nv11 = new NameValueCollection();
            nv11.Add("@Mode", "13");
            dt = objCommon.GetDataTable("GET_Common", nv11);
            IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);

            bool hasValue = false;
            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    string PlanDate = string.Empty;

                    hasValue = true;
                    DataTable dt1 = new DataTable();
                    //NameValueCollection nv1 = new NameValueCollection();

                    //string TraySize = (row.FindControl("lblTraySize") as Label).Text;
                    //string GCode = (row.FindControl("lblGenusCode") as Label).Text;
                    //nv1.Add("@TraySize", TraySize);
                    //nv1.Add("@GCode", GCode);


                    //dt1 = objCommon.GetDataTable("spGetDateDhift", nv1);
                    //if (dt1 != null && dt1.Rows.Count > 0)
                    //{
                    //    int DF = Convert.ToInt32(dt1.Rows[0]["dateshift"]);
                    //    if (DF > 0)
                    //    {
                    //        PlanDate = (Convert.ToDateTime((row.FindControl("lblSeededDate") as Label).Text).AddDays(DF)).ToString();
                    //        txtPlantDate.Text = PlanDate;
                    //    }
                    //    else
                    //    {
                    //        txtPlantDate.Text = (row.FindControl("lblSeededDate") as Label).Text;
                    //    }
                    //}
                    //else
                    //{
                    //    txtPlantDate.Text = (row.FindControl("lblSeededDate") as Label).Text;
                    //}

                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", Assigned);

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
                    nv.Add("@wo", (row.FindControl("lblwo") as Label).Text);
                    nv.Add("@Comments", txtPlantComments.Text.Trim());
                    nv.Add("@PlantDate", txtPlantDate.Text);
                    nv.Add("@Role", Session["Role"].ToString());
                    nv.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                    nv.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    nv.Add("@Jid", (row.FindControl("lblJIdPU") as Label).Text);
                    //  nv4.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyRequestManuaCreateTask", nv);
                }

            }

            if (!hasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Bench Location');", true);
            }
            else
            {
                objGeneral.SendMessage(int.Parse(Assigned), "New Plant Ready Task Assigned", "New Plant Ready Task Assigned", "Plant Ready");

                string message = "Assignment Successful";
                string url = "CreateTask.aspx";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }

            updateNotification();
        }
        protected void btnSavePlantReady_Click(object sender, EventArgs e)
        {
            PlantReadySubmit(Session["LoginID"].ToString());
        }

        protected void btnplant_readySubmit_Click(object sender, EventArgs e)
        {
            PlantReadySubmit(ddlplant_readySupervisor.SelectedValue);
        }

        protected void btnplant_readyReset_Click(object sender, EventArgs e)
        {
            BindSupervisor();
            txtPlantComments.Text = "";
            txtPlantDate.Text = "";
            //  txtPlantDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            plant_ready_count.Attributes.Add("class", "request__block-collapse collapse show");
            btnPlantReady.Attributes.Add("class", "request__block-head");
        }

        protected void ddlTaskType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SelectedAssignment"] = ddlAssignments.SelectedValue;
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
            ddlAssignments.Focus();
        }

        protected void btnSearchDet_Click(object sender, EventArgs e)
        {

            BindGridFerReq("", txtSearchJobNo.Text);

        }

        protected void btnResetBenchLocation_Click(object sender, EventArgs e)
        {
            txtBatchLocation.Text = "";
            Bench1 = txtBatchLocation.Text;

            gvFer.DataSource = null;
            gvFer.DataBind();

        }

        protected void btlSearchBenchLocation_Click(object sender, EventArgs e)
        {

            Bench1 = txtBatchLocation.Text;
            // BindGridFerReq("'" + Bench1 + "'", txtSearchJobNo.Text);
            gvFer.DataSource = null;
            gvFer.DataBind();
            txtSearchJobNo.Text = "";
            BindJobCode(Bench1);
            BindGridFerReq("'" + Bench1 + "'", "");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            txtSearchJobNo.Text = "";
            //  txtSearchJobNo.Text = "JB";

            // BindGridFerReq("", txtSearchJobNo.Text);
            gvFer.DataSource = null;
            gvFer.DataBind();
        }

        protected void btnChemicalReset_Click(object sender, EventArgs e)
        {

            BindSupervisor();
            BindChemical();
            txtResetChemicalSprayTask.Text = "";
            txtCComments.Text = "";
            txtChemicalSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            Chemical_count.Attributes.Add("class", "request__block-collapse collapse show");
            btnChemical.Attributes.Add("class", "request__block-head");
        }

        protected void btnChemicalSubmit_Click(object sender, EventArgs e)
        {

            SubmitChemical(ddlChemical_supervisor.SelectedValue, 0);
        }

        protected void btnChemicalSFLSubmit_Click(object sender, EventArgs e)
        {
            SubmitChemical(Session["LoginID"].ToString(), 0);
        }

        protected void btnChekCemSubmit_Click(object sender, EventArgs e)
        {
            SubmitChemical(ddlChemical_supervisor.SelectedValue, 1);
        }

        protected void btnChekCemCancel_Click(object sender, EventArgs e)
        {
            SubmitChemical(ddlChemical_supervisor.SelectedValue, 0);
        }
        public void SubmitChemical(string Assigned, int Fid)
        {
            int ChemicalCode = 0;
            string Batchlocation = "", Batchlocation1 = "", Batchlocation2 = "";
            string TaskRequestKey = "";
            string SprayTaskForDaysDate = "";
            // string Assigned = "";
            bool hasValue = false;

            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    dtCTrays.Clear();
                    hasValue = true;
                    Batchlocation = (row.FindControl("lblGreenHouse") as Label).Text;

                    string TodatDate;
                    string ReSetSprayDate = "";

                    TodatDate = System.DateTime.Now.ToShortDateString();



                    if (Batchlocation1 == "" || Batchlocation1 != Batchlocation)
                    {
                        dtCTrays.Clear();
                        DataTable dt1 = new DataTable();
                        NameValueCollection nv1 = new NameValueCollection();
                        nv1.Add("@Mode", "16");
                        dt1 = objCommon.GetDataTable("GET_Common", nv1);
                        ChemicalCode = Convert.ToInt32(dt1.Rows[0]["CCode"]);

                        Batchlocation1 = Batchlocation;

                        string Countbanch = "";
                        DataTable dt15 = new DataTable();
                        NameValueCollection nv15 = new NameValueCollection();
                        nv15.Add("@Mode", "2");
                        nv15.Add("@GreenHouseID", Batchlocation1);
                        dt15 = objCommon.GetDataTable("SP_GetTaskRequestKeyBanchlocation", nv15);
                        Countbanch = dt15.Rows[0]["Countbanch"].ToString();

                        TaskRequestKey = Batchlocation + "_" + "Chemical" + "_" + Countbanch;


                    }
                    else
                    {
                        ChemicalCode = ChemicalCode;

                    }


                    if (txtResetSprayTaskForDays.Text != "")
                    {
                        SprayTaskForDaysDate = (Convert.ToDateTime(System.DateTime.Now.ToShortDateString()).AddDays(Convert.ToInt32(txtResetSprayTaskForDays.Text))).ToString();

                    }
                    else
                    {
                        SprayTaskForDaysDate = System.DateTime.Now.ToShortDateString();

                    }


                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", Assigned);
                    nv.Add("@Type", "Chemical");
                    nv.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                    nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                    nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    //nv.Add("@WorkOrder", lblwo.Text);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@ChemicalCode", ChemicalCode.ToString());
                    nv.Add("@ChemicalDate", txtChemicalSprayDate.Text);
                    nv.Add("@Comments", txtCComments.Text);
                    nv.Add("@Method", ddlMethod.SelectedValue);
                    nv.Add("@seedDate", (row.FindControl("lblSeededDate") as Label).Text);
                    //  nv4.Add("@Jid", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    nv.Add("@Jid", (row.FindControl("lblJIdPU") as Label).Text);
                    nv.Add("@TaskRequestKey", TaskRequestKey);
                    nv.Add("@ResetTaskForDays", SprayTaskForDaysDate);

                    //  nv4.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    result = objCommon.GetDataExecuteScaler("SP_AddChemicalRequestManualCreateTask", nv);

                    //NameValueCollection nvn = new NameValueCollection();
                    //nvn.Add("@LoginID", Session["LoginID"].ToString());
                    //nvn.Add("@SupervisorID", Assigned);
                    //nvn.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                    //nvn.Add("@TaskName", "Chemical");
                    //nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                    //var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);
                    if (Batchlocation2 == "" || Batchlocation2 != Batchlocation)
                    {

                        dtCTrays.Rows.Add(ddlChemical.SelectedItem.Text, txtChemicalTrays.Text, txtSQFT.Text);
                        objTask.AddChemicalRequestDetails(dtCTrays, result.ToString(), getChemicalSelect(), ChemicalCode, Batchlocation, txtResetSprayTaskForDays.Text, ddlMethod.SelectedValue, txtCComments.Text);
                        Batchlocation2 = Batchlocation;


                        NameValueCollection nvn = new NameValueCollection();
                        nvn.Add("@LoginID", Session["LoginID"].ToString());
                        nvn.Add("@SupervisorID", Assigned);
                        nvn.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                        nvn.Add("@TaskName", "Chemical");
                        nvn.Add("@TaskRequestKey", TaskRequestKey);
                        nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                        var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);


                    }
                    objGeneral.SendMessage(int.Parse(Assigned), "New Chemical Task Assigned", "New Chemical Task Assigned", "Chemical");

                    string message = "Assignment Successful";
                    string url = "CreateTask.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                    if (Fid == 1)
                    {
                        string BenchUp = "'" + Batchlocation + "'";
                        objTask.UpdateIsActiveChemical(BenchUp);
                    }
                }

            }

            if (!hasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Bench Location');", true);
            }
            else
            {
            }

            updateNotification();
        }

        //public void BindFacility()
        //{
        //    ddlToFacility.DataSource = objBAL.GetMainLocation();
        //    ddlToFacility.DataTextField = "Facility";
        //    ddlToFacility.DataValueField = "Facility";
        //    ddlToFacility.DataBind();
        //    ddlToFacility.Items.Insert(0, new ListItem("--- Select ---", "0"));
        //    ddlToFacility.SelectedValue = Session["Facility"].ToString();
        //    BindBench_Location();
        //}

        public void BindBench_Location()
        {
            //  nv.Add("@FacilityID", ddlToFacility.SelectedValue);
            ddlToGreenHouse.DataSource = objBAL.GetLocation(Session["Facility"].ToString());
            ddlToGreenHouse.DataTextField = "BenchName";
            ddlToGreenHouse.DataValueField = "BenchName";
            ddlToGreenHouse.DataBind();
            ddlToGreenHouse.Items.Insert(0, new ListItem("--- Select ---", "0"));


        }

        protected void ddlToFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBench_Location();
        }

        protected void MoveReset_Click(object sender, EventArgs e)
        {
            BindSupervisor();
      //      BindFacility();
            BindBench_Location();
            txtMoveComments.Text = "";
            txtMoveDate.Text = "";
          //  txtMoveNumberOfTrays.Text = "";
            //btnMoveRequest.Attributes.Add("class", "request__block-collapse collapse show");

        }
        public void MoveSubmit(string Assigned)
        {
            bool hasValue = false;
            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    long result = 0;
                    hasValue = true;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", Assigned);
                    nv.Add("@WorkOrder", (row.FindControl("lblwo") as Label).Text);
                    nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);

                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@FromFacility", (row.FindControl("lblFacility") as Label).Text);
                    nv.Add("@ToFacility", "");
                    nv.Add("@ToGreenHouse", ddlToGreenHouse.SelectedValue);
                    nv.Add("@FormBanchlocation", (row.FindControl("lblGreenHouse") as Label).Text);
                    nv.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                    nv.Add("@RTrays", "");
                    nv.Add("@MoveDate", txtMoveDate.Text);
                    nv.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);

                    nv.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    nv.Add("@ChId", "0");
                    nv.Add("@Comments", txtMoveComments.Text.Trim());
                   
                    nv.Add("@Jid", (row.FindControl("lblJIdPU") as Label).Text);
                    //  nv4.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    result = objCommon.GetDataExecuteScaler("SP_AddMoveRequestManualCreateTask", nv);

                    NameValueCollection nvn = new NameValueCollection();
                    nvn.Add("@LoginID", Session["LoginID"].ToString());
                    nvn.Add("@SupervisorID", Assigned);
                    nvn.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                    nvn.Add("@TaskName", "Move");
                    nvn.Add("@TaskRequestKey", "");
                    nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                    var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);
                }
            }

            if (!hasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Bench Location');", true);
            }
            else
            {
                objGeneral.SendMessage(int.Parse(Assigned), "New Move Task Assigned", "New Move Task Assigned", "Move");

                string message = "Assignment Successful";
                string url = "CreateTask.aspx";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }

            updateNotification();
        }
        protected void btnMoveSubmit_Click(object sender, EventArgs e)
        {
            MoveSubmit(ddlLogisticManager.SelectedValue);
        }

        protected void btnSaveMove_Click(object sender, EventArgs e)
        {
            MoveSubmit(Session["LoginID"].ToString());
        }

        public void DumpSubmit(string Assigned)
        {
            bool hasValue = false;

            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Aid", Assigned);
            dt = objCommon.GetDataTable("spGeEmployeeRoleDetails", nv1);

            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    long result = 0;
                    hasValue = true;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", Assigned);

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
                    nv.Add("@Comments", txtCommentsDump.Text.Trim());
                    nv.Add("@QuantityOfTray", txtQuantityofTray.Text.Trim());
                    nv.Add("@wo", (row.FindControl("lblwo") as Label).Text);
                    nv.Add("@DumpDate", txtDumpDate.Text);
                    nv.Add("@RoleId", dt.Rows[0]["RoleID"].ToString());
                    nv.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    nv.Add("@Jid", (row.FindControl("lblJIdPU") as Label).Text);
                    //  nv4.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    result = objCommon.GetDataExecuteScaler("SP_AddDumpRequestManuaCreateTask", nv);
                }
            }


            if (!hasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Bench Location');", true);
            }
            else
            {
                objGeneral.SendMessage(int.Parse(Assigned), "New Dump Task Assigned", "New Dump Task Assigned", "Dump");

                string message = "Assignment Successful";
                string url = "CreateTask.aspx";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }
            updateNotification();
        }
        protected void btnSaveDump_Click(object sender, EventArgs e)
        {
            DumpSubmit(Session["LoginID"].ToString());
        }
        protected void btnDumpSumbit_Click(object sender, EventArgs e)
        {
            DumpSubmit(ddlDumptAssignment.SelectedValue);
        }

        protected void btnDumpReset_Click(object sender, EventArgs e)
        {
            txtCommentsDump.Text = "";
            txtQuantityofTray.Text = "";
            BindSupervisor();
            txtDumpDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            dump_request.Attributes.Add("class", "request__block-collapse collapse show");
            btnDump.Attributes.Add("class", "request__block-head");
        }

        public void GeneraltaskSubmit(string Assigned)
        {
            bool hasValue = false;
            long result16 = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            //nv1.Add("@Aid", Session["SelectedAssignment"].ToString());
            nv1.Add("@Aid", Assigned);
            dt = objCommon.GetDataTable("spGeEmployeeRoleDetails", nv1);
            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {


                    hasValue = true;
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

                    nv.Add("@SupervisorID", Assigned);
                    //nv.Add("@SupervisorID", ddlAssignments.SelectedValue);
                    nv.Add("@TaskType", ddlTaskType.SelectedValue);
                    nv.Add("@MoveFrom", txtFrom.Text);
                    nv.Add("@MoveTo", txtTo.Text);
                    nv.Add("@date", txtgeneralDate.Text);
                    nv.Add("@RoleId", dt.Rows[0]["RoleID"].ToString());

                    nv.Add("@LoginId", Session["LoginID"].ToString());
                    nv.Add("@Comments", txtgeneralComment.Text);
                    nv.Add("@Jid", (row.FindControl("lblJIdPU") as Label).Text);
                    nv.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    result16 = objCommon.GetDataInsertORUpdate("SP_AddGeneralRequesMenualDetailsCreateTask", nv);
                }
            }
            if (!hasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Bench Location');", true);
            }
            else
            {
                if (result16 > 0)
                {
                    objGeneral.SendMessage(int.Parse(Assigned), "New General Task Assigned", "New General Task Assigned", "General");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                    string CCEmail = "";

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

                    NameValueCollection nv = new NameValueCollection();

                    //var getToMail = Session["SelectedAssignment"].ToString();
                    // var getToMail = ddlAssignments.SelectedValue;
                    var getToMail = Assigned;
                    nv.Add("@Uid", getToMail);
                    DataTable dt1 = objCommon.GetDataTable("getReceiverEmail", nv);
                    ReceiverEmail = dt1.Rows[0]["Email"].ToString();

                    mail.To.Add(new MailAddress(ReceiverEmail));

                    nv.Clear();
                    var getCCMail = Session["Role"].ToString();
                    nv.Add("@Uid", getCCMail);

                    dt1 = objCommon.GetDataTable("getReceiverEmail", nv);
                    CCEmail = dt1.Rows[0]["Email"].ToString();
                    mail.CC.Add(new MailAddress(CCEmail));

                    smtpClient.Send(mail);

                    Clear();
                    ClearGeneral();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                }
            }
            updateNotification();
        }
        protected void btnSaveGeneral_Click(object sender, EventArgs e)
        {
            GeneraltaskSubmit(Session["LoginID"].ToString());
        }
        protected void btnGeneraltask_Click(object sender, EventArgs e)
        {
            GeneraltaskSubmit(Session["SelectedAssignment"].ToString());
        }

        protected void chckchanged1(object sender, EventArgs e)
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

        public void CropeSubmit(string Assigned)
        {
            bool hasValue = false;

            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Aid", Assigned);
            dt = objCommon.GetDataTable("spGeEmployeeRoleDetails", nv1);

            foreach (GridViewRow row in gvFer.Rows)
            {
                hasValue = true;

                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", Assigned);

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
                    nv.Add("@Comments", txtCommentsDump.Text.Trim());
                    nv.Add("@QuantityOfTray", txtQuantityofTray.Text.Trim());
                    nv.Add("@wo", (row.FindControl("lblwo") as Label).Text);
                    nv.Add("@CropDate", txtDumpDate.Text);
                    nv.Add("@RoleId", dt.Rows[0]["RoleID"].ToString());
                    nv.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    nv.Add("@Jid", (row.FindControl("lblJIdPU") as Label).Text);
                    //  nv4.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    result = objCommon.GetDataExecuteScaler("SP_AddCropReportRequestManuaCreateTask", nv);

                }
            }
            if (!hasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Bench Location');", true);
            }
            else
            {
                objGeneral.SendMessage(int.Parse(Assigned), "New Crop Health Report Assigned", "New Crop Health Report Task Assigned", "Crop Health Report");

                string message = "Assignment Successful";
                string url = "CreateTask.aspx";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }

            updateNotification();
        }

        protected void btnCropHealthSubmit_Click(object sender, EventArgs e)
        {
            CropeSubmit(ddlCropHealthAssignment.SelectedValue);
        }

        protected void btnCropHealthSave_Click(object sender, EventArgs e)
        {
            CropeSubmit(Session["LoginID"].ToString());
        }

        protected void btnCropHealthStart_Click(object sender, EventArgs e)
        {
            string x = "";
            string chkSelected = "";
            int c = 0;
            string BatchLocd = "";
            string JobCode = "";
            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    BatchLocd = (row.FindControl("lblGreenHouse") as Label).Text;
                    JobCode = (row.FindControl("lblID") as Label).Text;
                    c = c + 1;
                    x += "'" + BatchLocd + "',";

                }
            }


            if (c == 1)
            {
                chkSelected = x.Remove(x.Length - 1, 1);
                Response.Redirect(String.Format("~/CropHealthReport.aspx?BatchLoc={0}&JobCode={1}&CropAT={2}&PageType={3}", chkSelected, JobCode, 0, "CreateTask"));
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select only One JobCode')", true);
            }




        }

        protected void btnCropReset_Click(object sender, EventArgs e)
        {
            txtCropHealthDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            BindSupervisor();
            txtCropHealthComments.Text = "";
        }

        protected void btngermination_Click1(object sender, EventArgs e)
        {
            germination_count.Attributes.Add("class", "request__block-collapse collapse show");

            fertilization_count.Attributes.Add("class", "request__block-collapse collapse");

            Chemical_count.Attributes.Add("class", "request__block-collapse collapse");

            irrigation_count.Attributes.Add("class", "request__block-collapse collapse");

            plant_ready_count.Attributes.Add("class", "request__block-collapse collapse");

            move_request.Attributes.Add("class", "request__block-collapse collapse");

            dump_request.Attributes.Add("class", "request__block-collapse collapse");
            general_task_request.Attributes.Add("class", "request__block-collapse collapse ");
            btngermination.Attributes.Add("class", "request__block-head");
            btnFertilization.Attributes.Add("class", "request__block-head collapsed");
            btnChemical.Attributes.Add("class", "request__block-head collapsed");
            btnIrrigation.Attributes.Add("class", "request__block-head collapsed");
            btnPlantReady.Attributes.Add("class", "request__block-head collapsed");
            btnMoveRequest.Attributes.Add("class", "request__block-head collapsed");
            btnDump.Attributes.Add("class", "request__block-head collapsed");
            btnGeneral_Task.Attributes.Add("class", "request__block-head collapsed");
            ddlgerminationSupervisor.Focus();
            txtGerDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            CropHealthReportID.Attributes.Add("class", "request__block-collapse collapse");
            btnCropHealthReport.Attributes.Add("class", "request__block-head collapsed");
        }

        protected void btnFertilization_Click(object sender, EventArgs e)
        {
            germination_count.Attributes.Add("class", "request__block-collapse collapse");

            fertilization_count.Attributes.Add("class", "request__block-collapse collapse show");

            Chemical_count.Attributes.Add("class", "request__block-collapse collapse");

            irrigation_count.Attributes.Add("class", "request__block-collapse collapse");

            plant_ready_count.Attributes.Add("class", "request__block-collapse collapse");

            move_request.Attributes.Add("class", "request__block-collapse collapse");

            dump_request.Attributes.Add("class", "request__block-collapse collapse");


            general_task_request.Attributes.Add("class", "request__block-collapse collapse ");
            ddlFertilizationSupervisor.Focus();

            btngermination.Attributes.Add("class", "request__block-head collapsed");
            btnFertilization.Attributes.Add("class", "request__block-head ");
            btnChemical.Attributes.Add("class", "request__block-head collapsed");
            btnIrrigation.Attributes.Add("class", "request__block-head collapsed");
            btnPlantReady.Attributes.Add("class", "request__block-head collapsed");
            btnMoveRequest.Attributes.Add("class", "request__block-head collapsed");
            btnDump.Attributes.Add("class", "request__block-head collapsed");
            btnGeneral_Task.Attributes.Add("class", "request__block-head collapsed");

            txtFDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            CropHealthReportID.Attributes.Add("class", "request__block-collapse collapse");
            btnCropHealthReport.Attributes.Add("class", "request__block-head collapsed");

        }

        protected void btnChemical_Click(object sender, EventArgs e)
        {
            germination_count.Attributes.Add("class", "request__block-collapse collapse ");

            fertilization_count.Attributes.Add("class", "request__block-collapse collapse ");

            Chemical_count.Attributes.Add("class", "request__block-collapse collapse show");

            irrigation_count.Attributes.Add("class", "request__block-collapse collapse ");

            plant_ready_count.Attributes.Add("class", "request__block-collapse collapse ");

            move_request.Attributes.Add("class", "request__block-collapse collapse ");

            dump_request.Attributes.Add("class", "request__block-collapse collapse ");
            general_task_request.Attributes.Add("class", "request__block-collapse collapse ");
            ddlChemical_supervisor.Focus();
            btngermination.Attributes.Add("class", "request__block-head collapsed");
            btnFertilization.Attributes.Add("class", "request__block-head collapsed");
            btnChemical.Attributes.Add("class", "request__block-head ");
            btnIrrigation.Attributes.Add("class", "request__block-head collapsed");
            btnPlantReady.Attributes.Add("class", "request__block-head collapsed");
            btnMoveRequest.Attributes.Add("class", "request__block-head collapsed");
            btnDump.Attributes.Add("class", "request__block-head collapsed");
            btnGeneral_Task.Attributes.Add("class", "request__block-head collapsed");
            txtChemicalSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            CropHealthReportID.Attributes.Add("class", "request__block-collapse collapse");
            btnCropHealthReport.Attributes.Add("class", "request__block-head collapsed");
        }

        protected void btnIrrigation_Click(object sender, EventArgs e)
        {
            germination_count.Attributes.Add("class", "request__block-collapse collapse ");

            fertilization_count.Attributes.Add("class", "request__block-collapse collapse ");

            Chemical_count.Attributes.Add("class", "request__block-collapse collapse ");

            irrigation_count.Attributes.Add("class", "request__block-collapse collapse show");

            plant_ready_count.Attributes.Add("class", "request__block-collapse collapse ");

            move_request.Attributes.Add("class", "request__block-collapse collapse ");

            dump_request.Attributes.Add("class", "request__block-collapse collapse ");
            general_task_request.Attributes.Add("class", "request__block-collapse collapse ");
            ddlirrigationSupervisor.Focus();
            btngermination.Attributes.Add("class", "request__block-head collapsed");
            btnFertilization.Attributes.Add("class", "request__block-head collapsed");
            btnChemical.Attributes.Add("class", "request__block-head collapsed");
            btnIrrigation.Attributes.Add("class", "request__block-head ");
            btnPlantReady.Attributes.Add("class", "request__block-head collapsed");
            btnMoveRequest.Attributes.Add("class", "request__block-head collapsed");
            btnDump.Attributes.Add("class", "request__block-head collapsed");
            btnGeneral_Task.Attributes.Add("class", "request__block-head collapsed");
            txtirrigationSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            CropHealthReportID.Attributes.Add("class", "request__block-collapse collapse");
            btnCropHealthReport.Attributes.Add("class", "request__block-head collapsed");
        }

        protected void btnPlantReady_Click(object sender, EventArgs e)
        {
            germination_count.Attributes.Add("class", "request__block-collapse collapse ");

            fertilization_count.Attributes.Add("class", "request__block-collapse collapse ");

            Chemical_count.Attributes.Add("class", "request__block-collapse collapse ");

            irrigation_count.Attributes.Add("class", "request__block-collapse collapse ");

            plant_ready_count.Attributes.Add("class", "request__block-collapse collapse show");

            move_request.Attributes.Add("class", "request__block-collapse collapse ");

            dump_request.Attributes.Add("class", "request__block-collapse collapse ");
            general_task_request.Attributes.Add("class", "request__block-collapse collapse ");

            ddlplant_readySupervisor.Focus();

            //  txtPlantDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");


            btngermination.Attributes.Add("class", "request__block-head collapsed");
            btnFertilization.Attributes.Add("class", "request__block-head collapsed");
            btnChemical.Attributes.Add("class", "request__block-head collapsed");
            btnIrrigation.Attributes.Add("class", "request__block-head collapsed");
            btnPlantReady.Attributes.Add("class", "request__block-head ");
            btnMoveRequest.Attributes.Add("class", "request__block-head collapsed");
            btnDump.Attributes.Add("class", "request__block-head collapsed");
            btnGeneral_Task.Attributes.Add("class", "request__block-head collapsed");
            CropHealthReportID.Attributes.Add("class", "request__block-collapse collapse");
            btnCropHealthReport.Attributes.Add("class", "request__block-head collapsed");
        }

        protected void btnMoveRequest_Click(object sender, EventArgs e)
        {
            germination_count.Attributes.Add("class", "request__block-collapse collapse ");

            fertilization_count.Attributes.Add("class", "request__block-collapse collapse ");

            Chemical_count.Attributes.Add("class", "request__block-collapse collapse ");

            irrigation_count.Attributes.Add("class", "request__block-collapse collapse ");

            plant_ready_count.Attributes.Add("class", "request__block-collapse collapse ");

            move_request.Attributes.Add("class", "request__block-collapse collapse show");

            dump_request.Attributes.Add("class", "request__block-collapse collapse ");
            general_task_request.Attributes.Add("class", "request__block-collapse collapse ");
            ddlLogisticManager.Focus();

            txtMoveDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            btngermination.Attributes.Add("class", "request__block-head collapsed");
            btnFertilization.Attributes.Add("class", "request__block-head collapsed");
            btnChemical.Attributes.Add("class", "request__block-head collapsed");
            btnIrrigation.Attributes.Add("class", "request__block-head collapsed");
            btnPlantReady.Attributes.Add("class", "request__block-head collapsed");
            btnMoveRequest.Attributes.Add("class", "request__block-head ");
            btnDump.Attributes.Add("class", "request__block-head collapsed");
            btnGeneral_Task.Attributes.Add("class", "request__block-head collapsed");
            CropHealthReportID.Attributes.Add("class", "request__block-collapse collapse");
            btnCropHealthReport.Attributes.Add("class", "request__block-head collapsed");
        }

        protected void btnDump_Click(object sender, EventArgs e)
        {
            germination_count.Attributes.Add("class", "request__block-collapse collapse ");

            fertilization_count.Attributes.Add("class", "request__block-collapse collapse ");

            Chemical_count.Attributes.Add("class", "request__block-collapse collapse ");

            irrigation_count.Attributes.Add("class", "request__block-collapse collapse ");

            plant_ready_count.Attributes.Add("class", "request__block-collapse collapse ");

            move_request.Attributes.Add("class", "request__block-collapse collapse ");

            dump_request.Attributes.Add("class", "request__block-collapse collapse show");
            general_task_request.Attributes.Add("class", "request__block-collapse collapse ");
            ddlDumptAssignment.Focus();

            txtDumpDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");

            btngermination.Attributes.Add("class", "request__block-head collapsed");
            btnFertilization.Attributes.Add("class", "request__block-head collapsed");
            btnChemical.Attributes.Add("class", "request__block-head collapsed");
            btnIrrigation.Attributes.Add("class", "request__block-head collapsed");
            btnPlantReady.Attributes.Add("class", "request__block-head collapsed");
            btnMoveRequest.Attributes.Add("class", "request__block-head collapsed");
            btnDump.Attributes.Add("class", "request__block-head ");
            btnGeneral_Task.Attributes.Add("class", "request__block-head collapsed");
            CropHealthReportID.Attributes.Add("class", "request__block-collapse collapse");
            btnCropHealthReport.Attributes.Add("class", "request__block-head collapsed");
        }

        protected void btnGeneralTask_Click(object sender, EventArgs e)
        {
            germination_count.Attributes.Add("class", "request__block-collapse collapse ");

            fertilization_count.Attributes.Add("class", "request__block-collapse collapse");

            Chemical_count.Attributes.Add("class", "request__block-collapse collapse");

            irrigation_count.Attributes.Add("class", "request__block-collapse collapse");

            plant_ready_count.Attributes.Add("class", "request__block-collapse collapse");

            move_request.Attributes.Add("class", "request__block-collapse collapse");

            dump_request.Attributes.Add("class", "request__block-collapse collapse");

            general_task_request.Attributes.Add("class", "request__block-collapse collapse show");
            CropHealthReportID.Attributes.Add("class", "request__block-collapse collapse");
            btnCropHealthReport.Attributes.Add("class", "request__block-head collapsed");
            ddlAssignments.Focus();

            txtgeneralDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");

            btngermination.Attributes.Add("class", "request__block-head collapsed");
            btnFertilization.Attributes.Add("class", "request__block-head collapsed");
            btnChemical.Attributes.Add("class", "request__block-head collapsed");
            btnIrrigation.Attributes.Add("class", "request__block-head collapsed");
            btnPlantReady.Attributes.Add("class", "request__block-head collapsed");
            btnMoveRequest.Attributes.Add("class", "request__block-head collapsed");
            btnDump.Attributes.Add("class", "request__block-head collapsed");
            btnGeneral_Task.Attributes.Add("class", "request__block-head ");
        }

        protected void btnCropHealthReport_Click(object sender, EventArgs e)
        {
            germination_count.Attributes.Add("class", "request__block-collapse collapse ");

            fertilization_count.Attributes.Add("class", "request__block-collapse collapse");

            Chemical_count.Attributes.Add("class", "request__block-collapse collapse");

            irrigation_count.Attributes.Add("class", "request__block-collapse collapse");

            plant_ready_count.Attributes.Add("class", "request__block-collapse collapse");

            move_request.Attributes.Add("class", "request__block-collapse collapse");

            dump_request.Attributes.Add("class", "request__block-collapse collapse");

            general_task_request.Attributes.Add("class", "request__block-collapse collapse");

            CropHealthReportID.Attributes.Add("class", "request__block-collapse collapse show");

            ddlAssignments.Focus();

            txtgeneralDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");

            btngermination.Attributes.Add("class", "request__block-head collapsed");
            btnFertilization.Attributes.Add("class", "request__block-head collapsed");
            btnChemical.Attributes.Add("class", "request__block-head collapsed");
            btnIrrigation.Attributes.Add("class", "request__block-head collapsed");
            btnPlantReady.Attributes.Add("class", "request__block-head collapsed");
            btnMoveRequest.Attributes.Add("class", "request__block-head collapsed");
            btnDump.Attributes.Add("class", "request__block-head collapsed");
            btnGeneral_Task.Attributes.Add("class", "request__block-head collapsed");
            btnCropHealthReport.Attributes.Add("class", "request__block-head ");

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
                    cmd.CommandText = " select distinct jobcode from gti_jobs_seeds_plan where (jstatus=2 or jstatus=0) and  loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%' order by jobcode" +
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchBenchLocation(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {

                //conn.ConnectionString = ConfigurationManager.ConnectionStrings["EvoNavision"].ConnectionString;
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Evo"].ConnectionString;

                using (SqlCommand cmd = new SqlCommand())
                {

                    string Facility = HttpContext.Current.Session["Facility"].ToString();

                    //cmd.CommandText = "Select s.[Position Code], s.[Position Code] p2 from [GTI$IA Subsection] s where Level =3 and s.[Position Code]  like '%" + prefixText + "%' and s.[Location Code]='" + Facility + "' ";
                    cmd.CommandText = "Select distinct GreenHouseID from  gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "' and GreenHouseId  like '%" + prefixText + "%' order by GreenHouseID";

                    // Select* from gti_jobs_seeds_plan_Manual where loc_seedline = 'ENC2' and GreenHouseId = 'ENC2-H02-05-B'
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();


                    List<string> BenchLocation = new List<string>();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            BenchLocation.Add(sdr["GreenHouseId"].ToString());
                        }
                    }
                    conn.Close();

                    return BenchLocation;
                }
            }
        }

        protected void btnGeneralReset_Click(object sender, EventArgs e)
        {
            // Clear();
            ClearGeneral();
        }

        protected void gvFer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblGenusCode = (Label)e.Row.FindControl("lblGenusCode");

                Label lblTraySize = (Label)e.Row.FindControl("lblTraySize");
                Label lblSeededDate = (Label)e.Row.FindControl("lblSeededDate");
                //   Label lblPlantReadyDate = (Label)e.Row.FindControl("lblPlantReadyDate");
                //  Label lblPlantDueDate = (Label)e.Row.FindControl("lblPlantDueDate");

                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@Tray_Size", lblTraySize.Text);
                nv.Add("@GCode", lblGenusCode.Text);
                dt = objCommon.GetDataTable("spGetDateDhiftCreateTaskPlantNo", nv);

                if (dt != null && dt.Rows.Count > 0)
                {
                    int PlantPDate = 0;
                    int PlanrDDate = 0;
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    PlantPDate =
                    //}
                    PlanrDDate = Convert.ToInt32(dt.Rows[0]["dateshift"]);
                    PlantPDate = Convert.ToInt32(dt.Rows[1]["dateshift"]);
                    //   lblPlantReadyDate.Text = Convert.ToDateTime(lblSeededDate.Text).AddDays(PlantPDate).ToString("MM/dd/yyyy");
                    //  lblPlantDueDate.Text = Convert.ToDateTime(lblSeededDate.Text).AddDays(PlanrDDate).ToString("MM/dd/yyyy");

                }


            }
        }

        protected void btnGeneralStart_Click(object sender, EventArgs e)
        {

        }

        protected void btnStartDumpDetails_Click(object sender, EventArgs e)
        {
            string x = "'" + Bench1 + "'" + ",";
            string chkSelected = "";
            int c = 0;
            string BatchLocd = "";
            string JobCode = "";
            long result = 0;
            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    BatchLocd = (row.FindControl("lblGreenHouse") as Label).Text;
                    JobCode = (row.FindControl("lblID") as Label).Text;
                    c = c + 1;
                    x += "'" + BatchLocd + "',";

                }
            }


            if (c == 1)
            {

                foreach (GridViewRow row in gvFer.Rows)
                {
                    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                    if (chckrw.Checked == true)
                    {

                        NameValueCollection nv = new NameValueCollection();
                        nv.Add("@SupervisorID", Session["LoginID"].ToString());

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
                        nv.Add("@Comments", txtCommentsDump.Text.Trim());
                        nv.Add("@QuantityOfTray", txtQuantityofTray.Text.Trim());
                        nv.Add("@wo", (row.FindControl("lblwo") as Label).Text);
                        nv.Add("@DumpDate", txtDumpDate.Text);
                        nv.Add("@RoleId", "0");
                        nv.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                        nv.Add("@Jid", (row.FindControl("lblJIdPU") as Label).Text);
                        //  nv4.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                        result = objCommon.GetDataExecuteScaler("SP_AddDumpRequestManuaCreateTaskStart", nv);

                    }
                }
            }
            NameValueCollection nv5 = new NameValueCollection();
            nv5.Add("@DTAID", result.ToString());
            DataTable dt = objCommon.GetDataTable("SP_GetDumpTaskAssignmentSelect", nv5);
            Response.Redirect(String.Format("~/DumpTaskCompletion.aspx?Did={0}&DrId={1}&PageType={2}", result.ToString(), dt.Rows[0]["DumpId"].ToString(), "CreateTask"));

        }

        protected void btnStartMove_Click(object sender, EventArgs e)
        {
            string x = "'" + Bench1 + "'" + ",";
            string chkSelected = "";
            int c = 0;
            string BatchLocd = "";
            string JobCode = "";
            long result = 0;
            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    BatchLocd = (row.FindControl("lblGreenHouse") as Label).Text;
                    JobCode = (row.FindControl("lblID") as Label).Text;
                    c = c + 1;
                    x += "'" + BatchLocd + "',";

                }
            }


            if (c == 1)
            {
                foreach (GridViewRow row in gvFer.Rows)
                {
                    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                    if (chckrw.Checked == true)
                    {

                        NameValueCollection nv = new NameValueCollection();
                        nv.Add("@SupervisorID", Session["LoginID"].ToString());
                        nv.Add("@WorkOrder", (row.FindControl("lblwo") as Label).Text);
                        nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);

                        nv.Add("@LoginID", Session["LoginID"].ToString());
                        nv.Add("@FromFacility", (row.FindControl("lblFacility") as Label).Text);
                        nv.Add("@ToFacility", "");
                        nv.Add("@ToGreenHouse", ddlToGreenHouse.SelectedValue);
                        nv.Add("@FormBanchlocation", (row.FindControl("lblGreenHouse") as Label).Text);
                        nv.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                        nv.Add("@RTrays", "");
                        nv.Add("@MoveDate", txtMoveDate.Text);
                        nv.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);

                        nv.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                        nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                        nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                        nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                        nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                        nv.Add("@ChId", "0");
                        nv.Add("@Comments", txtMoveComments.Text.Trim());
                        //  nv4.Add("@Jid", (row.FindControl("lblGrowerputawayID") as Label).Text);
                        nv.Add("@Jid", (row.FindControl("lblJIdPU") as Label).Text);
                        //  nv4.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                        result = objCommon.GetDataExecuteScaler("SP_AddMoveRequestManualCreateTaskStart", nv);


                    }
                }
            }

            //NameValueCollection nv5 = new NameValueCollection();
            //nv5.Add("@MTAID", result.ToString());
            //DataTable dt = objCommon.GetDataTable("SP_GetMoveTaskAssignmentSelect", nv5);

            Response.Redirect(String.Format("~/MoveCompletionStart.aspx?Did={0}&PageType={1}&IsF={2}", result, "CreateTask", 0));


        }

        protected void btnStartPlantReady_Click(object sender, EventArgs e)
        {
            string x = "'" + Bench1 + "'" + ",";
            string chkSelected = "";
            int c = 0;
            string BatchLocd = "";
            string JobCode = "";
            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    BatchLocd = (row.FindControl("lblGreenHouse") as Label).Text;
                    JobCode = (row.FindControl("lblID") as Label).Text;
                    c = c + 1;
                    x += "'" + BatchLocd + "',";

                }
            }


            if (c == 1)
            {
                long result = 0;
                foreach (GridViewRow row in gvFer.Rows)
                {

                    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                    if (chckrw.Checked == true)
                    {
                        string PlanDate = string.Empty;


                        DataTable dt1 = new DataTable();
                        NameValueCollection nv1 = new NameValueCollection();

                        string TraySize = (row.FindControl("lblTraySize") as Label).Text;
                        string GCode = (row.FindControl("lblGenusCode") as Label).Text;
                        nv1.Add("@TraySize", TraySize);
                        nv1.Add("@GCode", GCode);


                        dt1 = objCommon.GetDataTable("spGetDateDhift", nv1);
                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            int DF = Convert.ToInt32(dt1.Rows[0]["dateshift"]);
                            if (DF > 0)
                            {
                                PlanDate = (Convert.ToDateTime((row.FindControl("lblSeededDate") as Label).Text).AddDays(DF)).ToString();
                                txtPlantDate.Text = PlanDate;
                            }
                            else
                            {
                                txtPlantDate.Text = (row.FindControl("lblSeededDate") as Label).Text;
                            }
                        }
                        else
                        {
                            txtPlantDate.Text = (row.FindControl("lblSeededDate") as Label).Text;
                        }


                        NameValueCollection nv = new NameValueCollection();
                        nv.Add("@SupervisorID", Session["LoginID"].ToString());

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
                        nv.Add("@wo", (row.FindControl("lblwo") as Label).Text);
                        nv.Add("@Comments", txtPlantComments.Text.Trim());
                        nv.Add("@PlantDate", txtPlantDate.Text);
                        nv.Add("@Role", Session["Role"].ToString());
                        nv.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        //  nv4.Add("@Jid", (row.FindControl("lblGrowerputawayID") as Label).Text);
                        nv.Add("@Jid", (row.FindControl("lblJIdPU") as Label).Text);
                        //  nv4.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);

                        result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyRequestManuaCreateTaskStart", nv);
                    }

                }
                NameValueCollection nv5 = new NameValueCollection();

                nv5.Add("@PRTA", result.ToString());
                DataTable dt = objCommon.GetDataTable("SP_GetPlantReadyTaskAssignmentSelect", nv5);

                Response.Redirect(String.Format("~/PlantReadyTaskCompletion.aspx?PRAID={0}&PRID={1}&PageType={2}&IsF={3}", result.ToString(), dt.Rows[0]["PRID"].ToString(), "CreateTask", 0));
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select only One JobCode')", true);
            }


        }

        protected void btnStartirrigation_Click(object sender, EventArgs e)
        {
            string x = "'" + Bench1 + "'" + ",";
            string chkSelected = "";
            int c = 0;
            string BatchLocd = "";
            string JobCode = "";
            //foreach (GridViewRow row in gvFer.Rows)
            //{
            //    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            //    if (chckrw.Checked == true)
            //    {
            //        BatchLocd = (row.FindControl("lblGreenHouse") as Label).Text;
            //        JobCode = (row.FindControl("lblID") as Label).Text;
            //        c = c + 1;
            //        x += "'" + BatchLocd + "',";

            //    }
            //}
            int IrrigationCode = 0;
            string Batchlocation = "";
            string JobCOde = "";
            //DataTable dt = new DataTable();
            //NameValueCollection nv17 = new NameValueCollection();
            //NameValueCollection nvimg = new NameValueCollection();
            //nv17.Add("@Mode", "13");
            //dt = objCommon.GetDataTable("GET_Common", nv17);
            //IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);
            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    if (BatchLocd != "" & BatchLocd != (row.FindControl("lblGreenHouse") as Label).Text)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select only one Bench. Mutiple bench selection can be done after starting this task in the completion form.')", true);
                        break;
                    }
                    else
                    {
                        Batchlocation = (row.FindControl("lblGreenHouse") as Label).Text;

                        JobCOde = (row.FindControl("lblID") as Label).Text;
                        //long result16 = 0;
                        //NameValueCollection nv = new NameValueCollection();
                        //nv.Add("@SupervisorID", Session["LoginID"].ToString());

                        //nv.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                        //nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                        //nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                        //nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                        //nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                        //nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                        //nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                        //nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);

                        //nv.Add("@IrrigationCode", IrrigationCode.ToString());
                        //// nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                        //nv.Add("@IrrigatedNoTrays", (row.FindControl("lblTotTray") as Label).Text);
                        //nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                        //nv.Add("@IrrigationDuration", "");
                        //nv.Add("@SprayDate", txtirrigationSprayDate.Text.Trim());
                        ////nv.Add("@SprayTime", txtSprayTime.Text.Trim());
                        //nv.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);


                        //nv.Add("@Nots", txtIrrComments.Text.Trim());
                        //nv.Add("@LoginID", Session["LoginID"].ToString());
                        //nv.Add("@Role", Session["Role"].ToString());
                        //nv.Add("@Jid", (row.FindControl("lblGrowerputawayID") as Label).Text);
                        //result16 = objCommon.GetDataExecuteScaler("SP_AddIrrigationRequestManualCreateTaskStart", nv);
                    }

                }
            }
            Response.Redirect(String.Format("~/IrrigationStart.aspx?Bench={0}&jobCode={1}&ICode={2}&PageType={3}", Batchlocation, JobCOde, IrrigationCode, "CreateTask"));

            //if (c == 1)
            //{





            //    foreach (GridViewRow row in gvFer.Rows)
            //    {
            //        CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            //        if (chckrw.Checked == true)
            //        {


            //        }
            //    }


            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select only one Bench. Mutiple bench selection can be done after starting this task in the completion form.')", true);
            //}
        }

        protected void btnStartChemical_Click(object sender, EventArgs e)
        {
            string x = "'" + Bench1 + "'" + ",";
            string chkSelected = "";
            int c = 0;
            int Q = 0;
            string BatchLocd = "";
            string JobCode = "";


            int ChemicalCode = 0;
            string Batchlocation = "";
            string JobCOde = "";
            //dtCTrays.Clear();
            //DataTable dt1 = new DataTable();
            //NameValueCollection nv1 = new NameValueCollection();
            //nv1.Add("@Mode", "16");
            //dt1 = objCommon.GetDataTable("GET_Common", nv1);
            //ChemicalCode = Convert.ToInt32(dt1.Rows[0]["CCode"]);


            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    if (BatchLocd != "" & BatchLocd != (row.FindControl("lblGreenHouse") as Label).Text)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select only one Bench. Mutiple bench selection can be done after starting this task in the completion form.')", true);
                        break;
                    }
                    else
                    {

                        BatchLocd = (row.FindControl("lblGreenHouse") as Label).Text;
                        //JobCode = (row.FindControl("lblID") as Label).Text;
                        //c = c + 1;
                        //x += "'" + BatchLocd + "',";

                        Batchlocation = (row.FindControl("lblGreenHouse") as Label).Text;

                        JobCOde = (row.FindControl("lblID") as Label).Text;
                        //dtCTrays.Rows.Add(ddlChemical.SelectedItem.Text, txtChemicalTrays.Text, txtSQFT.Text);
                        //objTask.AddChemicalRequestDetails(dtCTrays, "0", ChemicalCode, Batchlocation, txtResetSprayTaskForDays.Text, ddlMethod.SelectedValue, txtCComments.Text);


                        //long result = 0;
                        //NameValueCollection nv = new NameValueCollection();
                        //nv.Add("@SupervisorID", Session["LoginID"].ToString());
                        //nv.Add("@Type", "Chemical");
                        //nv.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                        //nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                        //nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                        //nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                        //nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                        //nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                        //nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                        //nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                        ////nv.Add("@WorkOrder", lblwo.Text);
                        //nv.Add("@LoginID", Session["LoginID"].ToString());
                        //nv.Add("@ChemicalCode", ChemicalCode.ToString());
                        //nv.Add("@ChemicalDate", txtChemicalSprayDate.Text);
                        //nv.Add("@Comments", txtCComments.Text);
                        //nv.Add("@Method", ddlMethod.SelectedValue);
                        //nv.Add("@seedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        //nv.Add("@Jid", (row.FindControl("lblGrowerputawayID") as Label).Text);
                        //result = objCommon.GetDataExecuteScaler("SP_AddChemicalRequestManualCreateTask", nv);

                        //NameValueCollection nvn = new NameValueCollection();
                        //nvn.Add("@LoginID", Session["LoginID"].ToString());
                        //nvn.Add("@SupervisorID", Session["LoginID"].ToString());
                        //nvn.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                        //nvn.Add("@TaskName", "Chemical");
                        //nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                        //var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);
                    }

                }
            }






            //foreach (GridViewRow row in gvFer.Rows)
            //{
            //    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            //    if (chckrw.Checked == true)
            //    {


            //    }

            //}

            Response.Redirect(String.Format("~/ChemicalStart.aspx?Bench={0}&jobCode={1}&CCode={2}&PageType={3}", Batchlocation, JobCOde, ChemicalCode, "CreateTask"));


        }

        protected void btnStartFertilization_Click(object sender, EventArgs e)
        {

            string x = "'" + Bench1 + "'" + ",";
            string chkSelected = "";
            int c = 0;
            string BatchLocd = "";
            string JobCode = "";
            //foreach (GridViewRow row in gvFer.Rows)
            //{
            //    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            //    if (chckrw.Checked == true)
            //    {
            //        BatchLocd = (row.FindControl("lblGreenHouse") as Label).Text;
            //        JobCode = (row.FindControl("lblID") as Label).Text;

            //        c = c + 1;
            //        x += "'" + BatchLocd + "',";

            //    }


            //}
            string Batchlocation = "";
            string JobCOde = "";
            int FertilizationCode = 0;
            DataTable dt1 = new DataTable();
            NameValueCollection nv14 = new NameValueCollection();
            NameValueCollection nvimg = new NameValueCollection();
            nv14.Add("@Mode", "12");
            dt1 = objCommon.GetDataTable("GET_Common", nv14);
            FertilizationCode = Convert.ToInt32(dt1.Rows[0]["FCode"]);


            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    if (BatchLocd != "" & BatchLocd != (row.FindControl("lblGreenHouse") as Label).Text)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select only one Bench. Mutiple bench selection can be done after starting this task in the completion form.')", true);
                        break;
                    }
                    else
                    {

                        Batchlocation = (row.FindControl("lblGreenHouse") as Label).Text;

                        JobCOde = (row.FindControl("lblID") as Label).Text;

                        //dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text, "", txtFTrays.Text, txtSQFT.Text);

                        //objTask.AddFertilizerRequestDetailsCreatTask(dtTrays, "0", FertilizationCode, Batchlocation, "", "", "", txtResetSprayTaskForDays.Text, txtFComments.Text.Trim());


                        //long result2 = 0;
                        //NameValueCollection nv4 = new NameValueCollection();
                        //nv4.Add("@SupervisorID", Session["LoginID"].ToString());
                        //nv4.Add("@Type", "Fertilizer");
                        //nv4.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                        //nv4.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                        //nv4.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                        //nv4.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                        //nv4.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                        //nv4.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                        //nv4.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                        //nv4.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                        ////nv.Add("@WorkOrder", lblwo.Text);
                        //nv4.Add("@LoginID", Session["LoginID"].ToString());
                        //nv4.Add("@FertilizationCode", FertilizationCode.ToString());
                        //nv4.Add("@FertilizationDate", txtFDate.Text);
                        //nv4.Add("@seedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        //nv4.Add("@Jid", (row.FindControl("lblGrowerputawayID") as Label).Text);
                        //result2 = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManualCreateTask", nv4);

                        //NameValueCollection nvn = new NameValueCollection();
                        //nvn.Add("@LoginID", Session["LoginID"].ToString());
                        //nvn.Add("@SupervisorID", Session["LoginID"].ToString());
                        //nvn.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                        //nvn.Add("@TaskName", "Fertilizer");
                        //nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                        //var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);

                    }

                }
            }


            Response.Redirect(String.Format("~/FertilizerTaskStart.aspx?Bench={0}&jobCode={1}&FCode={2}&PageType={3}", Batchlocation, JobCOde, FertilizationCode, "CreateTask"));

            //if (c == 1)
            //{

            //    FertilizationCode = Convert.ToInt32(dt1.Rows[0]["FCode"]);
            //    foreach (GridViewRow row in gvFer.Rows)
            //    {
            //        CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            //        if (chckrw.Checked == true)
            //        {


            //        }



            //    }



            //    //  Response.Redirect(String.Format("~/FertilizerTaskStart.aspx?&FCode={0}", FertilizationCode));
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select only one Bench. Mutiple bench selection can be done after starting this task in the completion form.')", true);
            //}

        }

        protected void btnStartGermination_Click(object sender, EventArgs e)
        {
            string x = "'" + Bench1 + "'" + ",";
            string chkSelected = "";
            int c = 0;
            string BatchLocd = "";
            string JobCode = "";
            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    BatchLocd = (row.FindControl("lblGreenHouse") as Label).Text;
                    JobCode = (row.FindControl("lblID") as Label).Text;
                    c = c + 1;
                    x += "'" + BatchLocd + "',";

                }
            }


            if (c == 1)
            {
                long result16 = 0;

                foreach (GridViewRow row in gvFer.Rows)
                {

                    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                    if (chckrw.Checked == true)
                    {
                        NameValueCollection nv = new NameValueCollection();

                        nv.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                        nv.Add("@SupervisorID", Session["LoginID"].ToString());
                        nv.Add("@InspectionDueDate", txtGerDate.Text);
                        nv.Add("@TraysInspected", txtTGerTrays.Text);
                        nv.Add("@LoginId", Session["LoginID"].ToString());
                        nv.Add("@Comments", txtGcomments.Text);
                        //  nv4.Add("@Jid", (row.FindControl("lblGrowerputawayID") as Label).Text);
                        nv.Add("@Jid", (row.FindControl("lblJIdPU") as Label).Text);
                        //  nv4.Add("@GrowerputawayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                        result16 = objCommon.GetDataExecuteScaler("SP_AddGerminationRequesMenualDetailsCreateTaskStart", nv);
                    }
                }

                NameValueCollection nvR = new NameValueCollection();
                nvR.Add("@GTAId", result16.ToString());
                DataTable dtR = objCommon.GetDataTable("SP_GetTaskAssignmenGerminationRequestID", nvR);
                string GTRID = dtR.Rows[0]["GTRID"].ToString();


                Response.Redirect(String.Format("~/GreenHouseTaskCompletion.aspx?GTAID={0}&PageType={1}&GTRID={2}&IsF={3}", result16, "CreateTask", GTRID, 0));
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select only One JobCode')", true);
            }


        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            string Trays1 = "";
            int Trays = 0;
            foreach (GridViewRow row in gvFer.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
                    string TodatDate = System.DateTime.Now.ToShortDateString();
                    if (chkRow.Checked)
                    {
                        Trays1 = (row.FindControl("lblTotTray") as Label).Text;

                        Trays = Trays + Convert.ToInt32(Trays1);

                        NameValueCollection nv11 = new NameValueCollection();
                        nv11.Add("@BenchLocation", (row.FindControl("lblGreenHouse") as Label).Text);
                        DataTable dtSDate = objCommon.GetDataTable("SP_GetFertilizerRequestResetTaskForDaysCheck", nv11);

                        if (dtSDate != null && dtSDate.Rows.Count > 0)
                        {
                            if (dtSDate.Rows[0]["ResetTaskForDays"].ToString() != "")
                            {
                                SprayTaskForDaysDate.Value = Convert.ToDateTime(dtSDate.Rows[0]["ResetTaskForDays"]).ToShortDateString();
                                ToDaydate.Value = TodatDate;
                                lblDateOfShip.Value = Convert.ToDateTime(dtSDate.Rows[0]["CreatedOn"]).ToShortDateString();
                                lblDayOfShip.Value = dtSDate.Rows[0]["ResetSprayTaskForDays"].ToString();
                                //  FerDate.Value = dtSDate.Rows[0]["FertilizationDate"].ToString();

                            }
                        }
                        else
                        {
                            SprayTaskForDaysDate.Value = "";
                            ToDaydate.Value = "";
                            // SprayTaskForDaysDate = System.DateTime.Now.ToShortDateString();
                        }


                        NameValueCollection nvGP = new NameValueCollection();
                        nvGP.Add("@BenchLocation", (row.FindControl("lblGreenHouse") as Label).Text);
                        DataTable dtGPDate = objCommon.GetDataTable("SP_GetFertilizerGrowerPutAwayCheck", nvGP);

                        if (dtGPDate != null && dtGPDate.Rows.Count > 0)
                        {

                            FerDate.Value = Convert.ToDateTime(dtGPDate.Rows[0]["FertilizeSeedDate"]).ToString("yyyy-MM-dd");

                        }
                        else
                        {

                        }

                        NameValueCollection nv111 = new NameValueCollection();
                        nv111.Add("@BenchLocation", (row.FindControl("lblGreenHouse") as Label).Text);
                        DataTable dtSDate1 = objCommon.GetDataTable("SP_GetIrrigationIdResetTaskForDaysCheck", nv111);

                        if (dtSDate1 != null && dtSDate1.Rows.Count > 0)
                        {
                            if (dtSDate1.Rows[0]["ResetTaskForDays"].ToString() != "")
                            {
                                SprayTaskForDaysDateirr.Value = Convert.ToDateTime(dtSDate1.Rows[0]["ResetTaskForDays"]).ToShortDateString();
                                ToDaydateIrr.Value = TodatDate;
                                lblDateOfShip.Value = Convert.ToDateTime(dtSDate1.Rows[0]["CreatedOn"]).ToShortDateString();
                                lblDayOfShip.Value = dtSDate1.Rows[0]["ResetSprayTaskForDays"].ToString();

                                //   IrrDate.Value = dtSDate1.Rows[0]["SprayDate"].ToString();
                            }
                        }
                        else
                        {
                            // SprayTaskForDaysDate.Value = "";
                            // ToDaydate.Value = "";
                            // SprayTaskForDaysDate = System.DateTime.Now.ToShortDateString();
                        }


                        NameValueCollection nvirrGP = new NameValueCollection();
                        nvirrGP.Add("@BenchLocation", (row.FindControl("lblGreenHouse") as Label).Text);
                        DataTable dtirrGPDate = objCommon.GetDataTable("SP_GetIrrigationGrowerPutAwayCheck", nvirrGP);

                        if (dtirrGPDate != null && dtirrGPDate.Rows.Count > 0)
                        {

                            IrrDate.Value = Convert.ToDateTime(dtirrGPDate.Rows[0]["IrrigateSeedDate"]).ToString("yyyy-MM-dd");

                        }
                        else
                        {

                        }



                        NameValueCollection nvC11 = new NameValueCollection();
                        nvC11.Add("@BenchLocation", (row.FindControl("lblGreenHouse") as Label).Text);
                        DataTable dtSDateC = objCommon.GetDataTable("SP_GetChemicalRequestResetTaskForDaysCheck", nvC11);

                        if (dtSDateC != null && dtSDateC.Rows.Count > 0)
                        {
                            if (dtSDateC.Rows[0]["ResetTaskForDays"].ToString() != "")
                            {

                                SprayTaskForDaysDateCem.Value = Convert.ToDateTime(dtSDateC.Rows[0]["ResetTaskForDays"]).ToShortDateString();
                                ToDaydateCem.Value = TodatDate;
                                lblDateOfShip.Value = Convert.ToDateTime(dtSDateC.Rows[0]["CreatedOn"]).ToShortDateString();
                                lblDayOfShip.Value = dtSDateC.Rows[0]["ResetSprayTaskForDays"].ToString();
                                // CemDate.Value = dtSDateC.Rows[0]["ChemicalDate"].ToString();



                            }
                        }
                        else
                        {
                            //  SprayTaskForDaysDate.Value = "";
                            //  ToDaydate.Value = "";
                        }


                        NameValueCollection nvCGP = new NameValueCollection();
                        nvCGP.Add("@BenchLocation", (row.FindControl("lblGreenHouse") as Label).Text);
                        DataTable dtCGPDate = objCommon.GetDataTable("SP_GetChemicalGrowerPutAwayCheck", nvCGP);

                        if (dtCGPDate != null && dtCGPDate.Rows.Count > 0)
                        {

                            CemDate.Value = Convert.ToDateTime(dtCGPDate.Rows[0]["ChemicalSeedDate"]).ToString("yyyy-MM-dd");

                        }
                        else
                        {

                        }
                    }
                }
            }

            txtFTrays.Text = Trays.ToString();
            txtChemicalTrays.Text = Trays.ToString();
        }

        public void updateNotification()
        {
            var r1 = (Master.FindControl("r1") as Repeater);
            var lblCount = (Master.FindControl("lblNotificationCount") as Label);
            objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), r1, lblCount);
        }

        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            BindGridFerReq("", txtSearchJobNo.Text);
        }


        private string getChemicalSelect()
        {
            int c = 0;
            string x = "";
            string chkSelected = "";
            foreach (ListItem item in ddlChemical.Items)
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


        public void BindSlotSelectBanch(string bench)
        {



            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();

            nv1.Add("@BanchLocation", bench);
            nv1.Add("@Facility", "");
            nv1.Add("@Mode", "3");
            dt1 = objCommon.GetDataTable("SP_GetBanchLocation", nv1);




            if (dt1 != null && dt1.Rows.Count > 0)
            {
                int TotalTrays = 0;
                decimal availableSlot = 0;

                if (dt1.Rows[0]["Automation"].ToString() == "Auto")
                {


                    for (double i = 1; i < 53; i++)
                    {
                        ListItem item = new ListItem();
                        item.Text = i.ToString();
                        item.Value = i.ToString();

                        ddlToSlotPositionStart.Items.Add(item);
                    }

                    for (double i = 1; i < 53; i++)
                    {
                        ListItem item = new ListItem();
                        item.Text = i.ToString();
                        item.Value = i.ToString();

                        ddlToSlotPositionEnd.Items.Add(item);
                    }
                   


                }
                else
                {


                    for (double i = 0.5; i < 54; i += 0.5)
                    {
                        ListItem item = new ListItem();
                        item.Text = i.ToString();
                        item.Value = i.ToString();


                        ddlToSlotPositionStart.Items.Add(item);
                    }

                    for (double i = 0.5; i < 54; i += 0.5)
                    {
                        ListItem item = new ListItem();
                        item.Text = i.ToString();
                        item.Value = i.ToString();

                        ddlToSlotPositionEnd.Items.Add(item);
                    }



                }




            }





        }



        protected void ddlToGreenHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSlotSelectBanch(ddlToGreenHouse.SelectedValue);
        }

        protected void ddlToSlotPositionStart_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            int FromSlotPosition = 0;
            int ToSlotPosition = 0;
            FromSlotPosition = (Convert.ToInt32(ddlSlotPositionEnd.SelectedValue) - Convert.ToInt32(ddlSlotPositionStart.SelectedValue)) + 1;

            ToSlotPosition = FromSlotPosition + Convert.ToInt32(ddlToSlotPositionStart.SelectedValue);

            ddlToSlotPositionEnd.SelectedValue = ToSlotPosition.ToString();
        }
    }
}
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

namespace Evo
{
    public partial class CropHealthReport : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();
        static string ReceiverEmail = "";
        static string folderPath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Chid"] != null)
                {
                    Chid = Request.QueryString["Chid"].ToString();
                    BindGridCropHealth();

                    PanelView.Visible = true;
                    PanelList.Visible = false;
                }
                else
                {
                    PanelView.Visible = false;
                    PanelList.Visible = true;
                    //    BindUnit();
                    //  BindJobCode(ddlBenchLocation.SelectedValue);
                    Bindcname();
                    BindFacility();

                    dtTrays.Clear();
                }

                BindSupervisor();

                BindChemical();
                BindSupervisorList();
            }




            if (Request.Browser.IsMobileDevice)
            {
                divMobile.Visible = true;
            }
            else
            {
                divLaptop.Visible = true;
            }
        }

        private string Chid
        {
            get
            {
                if (ViewState["Chid"] != null)
                {
                    return (string)ViewState["Chid"];
                }
                return "";
            }
            set
            {
                ViewState["Chid"] = value;
            }
        }

        public void BindGridCropHealth()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Chid", Chid);
            dt = objCommon.GetDataTable("SP_GetCropHealthReportSelectView", nv);

            GridViewView.DataSource = dt;
            GridViewView.DataBind();

            decimal tray = 0;
            string BatchLocd = string.Empty;
            foreach (GridViewRow row in GridViewView.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                tray = tray + Convert.ToDecimal((row.FindControl("lblTotTray1") as Label).Text);
                //}
                BatchLocd = (row.FindControl("lblGreenHouse1") as Label).Text;
            }
            txtTGerTrays.Text = tray.ToString();
            txtFTrays.Text = tray.ToString();

            BindSQFTofBench(BatchLocd);


            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Chid", Chid);
            dt1 = objCommon.GetDataTable("SP_GetCropHealthReportSelect", nv1);

            if (dt1 != null && dt1.Rows.Count > 0)
            {
                ddlpr.SelectedItem.Text = dt1.Rows[0]["typeofProblem"].ToString();
                DropDownListCause.SelectedItem.Text = dt1.Rows[0]["Causeofproblem"].ToString();
                DropDownListSv.SelectedValue = dt1.Rows[0]["Severityofproblem"].ToString();
                txtTrays.Text = dt1.Rows[0]["NoTrays"].ToString();
                percentageDamage.Text = dt1.Rows[0]["PerDamage"].ToString();
                txtDate.Text = Convert.ToDateTime(dt1.Rows[0]["Date"]).ToString("yyyy-MM-dd");
            }

        }


        public void BindGridFerReq(string BenchLoc, string JobNo)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", BenchLoc);
            dt = objCommon.GetDataTable("SP_GetFertilizerRequestDetails", nv);

            DataTable dtManual = objFer.GetManualFertilizerRequest(ddlFacility.SelectedValue, BenchLoc, JobNo);
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
                BatchLocd = (row.FindControl("lblGreenHouse") as Label).Text;
            }
            txtTGerTrays.Text = tray.ToString();
            txtFTrays.Text = tray.ToString();

            BindSQFTofBench(BatchLocd);

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

        public void BindFacility()
        {
            ddlFacility.DataSource = objBAL.GetMainLocation();
            ddlFacility.DataTextField = "l1";
            ddlFacility.DataValueField = "l1";
            ddlFacility.DataBind();
            ddlFacility.Items.Insert(0, new ListItem("--Select--", ""));
            BindBenchLocation("");
        }

        public void BindBenchLocation(string ddlMain)
        {
            ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataTextField = "p2";
            ddlBenchLocation.DataValueField = "p2";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", ""));

        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq("", "");
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq(ddlBenchLocation.SelectedValue,ddlJobNo.SelectedValue);
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBenchLocation(ddlFacility.SelectedValue);
            //BindGridFerReq();
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindJobCode(ddlBenchLocation.SelectedValue);

            BindGridFerReq(ddlBenchLocation.SelectedValue, "");

        }

        protected void btnSearchDet_Click(object sender, EventArgs e)
        {

            BindGridFerReq(ddlBenchLocation.SelectedValue, txtSearchJobNo.Text.Trim());
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;

            NameValueCollection nv = new NameValueCollection();
            if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
            {
                folderPath = Server.MapPath("~/images/") + Path.GetFileName(FileUpload1.FileName);
                FileUpload1.SaveAs(folderPath);
            }
            else
            {
                folderPath = "";
            }
            nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
            nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
            nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
            nv.Add("@NoTrays", txtTrays.Text);
            nv.Add("@PerDamage", percentageDamage.Text);
            nv.Add("@Date", txtDate.Text);
            nv.Add("@Filepath", folderPath);
            nv.Add("@CropHealthCommit", txtcomments.Text);

            result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);


            foreach (GridViewRow row in gvFer.Rows)
            {

                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {

                    long result1 = 0;
                    NameValueCollection nv1 = new NameValueCollection();
                    nv1.Add("@chid", result.ToString());
                    nv1.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                    nv1.Add("@itemno", (row.FindControl("lblitem") as Label).Text);
                    nv1.Add("@itemdescp", (row.FindControl("lblitemdesc") as Label).Text);
                    nv1.Add("@cname", (row.FindControl("lblCustomer") as Label).Text);
                    nv1.Add("@loc_seedline", "");
                    nv1.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                    nv1.Add("@seedsreceived", "");
                    nv1.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                    nv1.Add("@SoDate", "");
                    nv1.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv1.Add("@GenusCode", "");
                    nv1.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                    result1 = objCommon.GetDataInsertORUpdate("SP_AddCropHealthReportDetails", nv1);
                }
            }

            string message = "Assignment Successful";
            string url = "CropHealthDetails.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);




            //  Clear();
        }

     


        public void Clear()
        {

            ddlpr.SelectedValue = "0";
            DropDownListCause.SelectedValue = "0";
            DropDownListSv.SelectedValue = "0";
            txtTrays.Text = "";
            txtDate.Text = "";
            percentageDamage.Text = "";
            dtTrays.Clear();
            ddlFacility.SelectedIndex = 0;
            ddlBenchLocation.SelectedIndex = 0;
            ddlCustomer.SelectedIndex = 0;
            //ddlJobNo.SelectedIndex = 0;
            //BindGridFerReq();
            gvFer.DataSource = null;
            gvFer.DataBind();
        }
        protected void UploadFile(object sender, EventArgs e)
        {
            string folderPath = Server.MapPath("~/Files/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            //Save the File to the Directory (Folder).
            FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

            //Display the success message.
            lblMessage.Text = Path.GetFileName(FileUpload1.FileName) + " has been uploaded.";

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            ddlFacility.SelectedIndex = 0;
            ddlBenchLocation.SelectedIndex = 0;
            ddlCustomer.SelectedIndex = 0;
           // ddlJobNo.SelectedIndex = 0;
            //BindGridFerReq();
            gvFer.DataSource = null;
            gvFer.DataBind();
        }



        //---------------------------------------------------------------- Tab Details-------
        public void BindSQFTofBench(string benchLoc)
        {

            DataTable dtSQFT = objFer.GetSQFTofBench(benchLoc);
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
            ddlFertilizer.DataSource = objFer.GetChemicalList();
            ddlFertilizer.DataTextField = "Name";
            ddlFertilizer.DataValueField = "No_";
            ddlFertilizer.DataBind();
            ddlFertilizer.Items.Insert(0, new ListItem("--- Select ---", "0"));
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

        protected void radtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radtype.SelectedValue == "Fertilizer")
            {

                lbltype.Text = "Fertilizer";
                dtTrays.Rows.Clear();

                BindFertilizer();


            }
            else if (radtype.SelectedValue == "Chemical")
            {


                lbltype.Text = "Chemical";
                dtTrays.Rows.Clear();

                BindChemical();
            }
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
            nv14.Add("@Mode", "12");
            dt = objCommon.GetDataTable("GET_Common", nv14);
            FertilizationCode = Convert.ToInt32(dt.Rows[0]["FCode"]);

            if (Chid == "")
            {
                long result = 0;
                string folderPath = "";
                NameValueCollection nv = new NameValueCollection();
                if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
                {
                    folderPath = Server.MapPath("~/images/");
                    FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));
                }
                else
                {
                    folderPath = "";
                }
                nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
                nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
                nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
                nv.Add("@NoTrays", txtTrays.Text);
                nv.Add("@PerDamage", percentageDamage.Text);
                nv.Add("@Date", txtDate.Text);
                nv.Add("@Filepath", folderPath);
                nv.Add("@CropHealthCommit", txtcomments.Text);
                result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);


                foreach (GridViewRow row in gvFer.Rows)
                {
                    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                    if (chckrw.Checked == true)
                    {
                        long result1 = 0;
                        NameValueCollection nv1 = new NameValueCollection();
                        nv1.Add("@chid", result.ToString());
                        nv1.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                        nv1.Add("@itemno", (row.FindControl("lblitem") as Label).Text);
                        nv1.Add("@itemdescp", (row.FindControl("lblitemdesc") as Label).Text);
                        nv1.Add("@cname", (row.FindControl("lblCustomer") as Label).Text);
                        nv1.Add("@loc_seedline", "");
                        nv1.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                        nv1.Add("@seedsreceived", "");
                        nv1.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        nv1.Add("@SoDate", "");
                        nv1.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                        nv1.Add("@GenusCode", "");
                        nv1.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                        result1 = objCommon.GetDataInsertORUpdate("SP_AddCropHealthReportDetails", nv1);
                    }

                }

                if (result > 0)
                {
                    Chid = result.ToString();
                    BindGridCropHealth();
                    BindSupervisorList();
                    PanelView.Visible = false;
                    PanelList.Visible = true;
                }

            }



            foreach (GridViewRow row in GridViewView.Rows)
            {

                long result2 = 0;
                NameValueCollection nv4 = new NameValueCollection();
                nv4.Add("@SupervisorID", ddlFertilizationSupervisor.SelectedValue);
                nv4.Add("@Type", radtype.SelectedValue);
                nv4.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                nv4.Add("@Customer", (row.FindControl("lblCustomer1") as Label).Text);
                nv4.Add("@Item", (row.FindControl("lblitem1") as Label).Text);
                nv4.Add("@Facility", "");
                nv4.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                nv4.Add("@TotalTray", (row.FindControl("lblTotTray1") as Label).Text);
                nv4.Add("@TraySize", (row.FindControl("lblTraySize1") as Label).Text);
                nv4.Add("@Itemdesc", (row.FindControl("lblitemdesc1") as Label).Text);
                //nv.Add("@WorkOrder", lblwo.Text);
                nv4.Add("@LoginID", Session["LoginID"].ToString());
                nv4.Add("@FertilizationCode", FertilizationCode.ToString());
                nv4.Add("@FertilizationDate", txtFDate.Text);
                result2 = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManual", nv4);
                Batchlocation = (row.FindControl("lblGreenHouse1") as Label).Text;
            }

            dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text, "", txtTrays.Text, txtSQFT.Text);

            objTask.AddFertilizerRequestDetails(dtTrays, "0", FertilizationCode, Batchlocation, txtBenchIrrigationFlowRate.Text, txtBenchIrrigationCoverage.Text, txtSprayCoverageperminutes.Text, txtResetSprayTaskForDays.Text);
            long result16 = 0;
            NameValueCollection nv11 = new NameValueCollection();
            nv11.Add("@Chid", Chid);
            result16 = objCommon.GetDataInsertORUpdate("SP_UpdateCropHealthReport", nv11);

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

            if (Chid == "")
            {
                long result = 0;
                string folderPath = "";
                NameValueCollection nv = new NameValueCollection();
                if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
                {
                    folderPath = Server.MapPath("~/images/");
                    FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));
                }
                else
                {
                    folderPath = "";
                }
                nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
                nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
                nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
                nv.Add("@NoTrays", txtTrays.Text);
                nv.Add("@PerDamage", percentageDamage.Text);
                nv.Add("@Date", txtDate.Text);
                nv.Add("@Filepath", folderPath);
                nv.Add("@CropHealthCommit", txtcomments.Text);
                result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);


                foreach (GridViewRow row in gvFer.Rows)
                {
                    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                    if (chckrw.Checked == true)
                    {
                        long result1 = 0;
                        NameValueCollection nv1 = new NameValueCollection();
                        nv1.Add("@chid", result.ToString());
                        nv1.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                        nv1.Add("@itemno", (row.FindControl("lblitem") as Label).Text);
                        nv1.Add("@itemdescp", (row.FindControl("lblitemdesc") as Label).Text);
                        nv1.Add("@cname", (row.FindControl("lblCustomer") as Label).Text);
                        nv1.Add("@loc_seedline", "");
                        nv1.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                        nv1.Add("@seedsreceived", "");
                        nv1.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        nv1.Add("@SoDate", "");
                        nv1.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                        nv1.Add("@GenusCode", "");
                        nv1.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                        result1 = objCommon.GetDataInsertORUpdate("SP_AddCropHealthReportDetails", nv1);
                    }

                }

                if (result > 0)
                {
                    Chid = result.ToString();
                    BindGridCropHealth();
                    BindSupervisorList();
                    PanelView.Visible = false;
                    PanelList.Visible = true;
                }

            }

            long result16 = 0;

            foreach (GridViewRow row in GridViewView.Rows)
            {
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@Customer", (row.FindControl("lblCustomer1") as Label).Text);
                nv.Add("@jobcode", (row.FindControl("lblID1") as Label).Text);
                nv.Add("@Item", (row.FindControl("lblitem1") as Label).Text);
                nv.Add("@Facility", "");
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray1") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize1") as Label).Text);
                nv.Add("@Seeddate", (row.FindControl("lblSeededDate1") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc1") as Label).Text);
                nv.Add("@SupervisorID", ddlgerminationSupervisor.SelectedValue);
                nv.Add("@InspectionDueDate", txtGerDate.Text);
                nv.Add("@TraysInspected", txtTGerTrays.Text);
                nv.Add("@Chid", Chid);
                nv.Add("@LoginId", Session["LoginID"].ToString());

                result16 = objCommon.GetDataInsertORUpdate("SP_AddCropHealthGerminationReques", nv);
            }

            if (result16 > 0)
            {
                long result1 = 0;
                NameValueCollection nv11 = new NameValueCollection();
                nv11.Add("@Chid", Chid);
                result1 = objCommon.GetDataInsertORUpdate("SP_UpdateCropHealthReport", nv11);
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
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                string FromMail = WebConfigurationManager.AppSettings["FromEmail"];
                string FromEmailPassword = WebConfigurationManager.AppSettings["FromEmailPassword"];
                smtpClient.Credentials = new System.Net.NetworkCredential(FromMail, FromEmailPassword);
                // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.Subject = "Crop Health Report";
                mail.Body = "Crop Health Report Comments:" + txtcomments.Text;
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
        //----------------------------------------------------------------------irrigatio

        protected void btnirrigationReset_Click1(object sender, EventArgs e)
        {

        }

        protected void btnirrigationSubmit_Click(object sender, EventArgs e)
        {
            int IrrigationCode = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv17 = new NameValueCollection();
            nv17.Add("@Mode", "13");
            dt = objCommon.GetDataTable("GET_Common", nv17);
            IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);


            if (Chid == "")
            {
                long result = 0;
                string folderPath = "";
                NameValueCollection nv = new NameValueCollection();
                if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
                {
                    folderPath = Server.MapPath("~/images/");
                    FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));
                }
                else
                {
                    folderPath = "";
                }
                nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
                nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
                nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
                nv.Add("@NoTrays", txtTrays.Text);
                nv.Add("@PerDamage", percentageDamage.Text);
                nv.Add("@Date", txtDate.Text);
                nv.Add("@Filepath", folderPath);
                nv.Add("@CropHealthCommit", txtcomments.Text);
                result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);


                foreach (GridViewRow row in gvFer.Rows)
                {
                    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                    if (chckrw.Checked == true)
                    {
                        long result11 = 0;
                        NameValueCollection nv1 = new NameValueCollection();
                        nv1.Add("@chid", result.ToString());
                        nv1.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                        nv1.Add("@itemno", (row.FindControl("lblitem") as Label).Text);
                        nv1.Add("@itemdescp", (row.FindControl("lblitemdesc") as Label).Text);
                        nv1.Add("@cname", (row.FindControl("lblCustomer") as Label).Text);
                        nv1.Add("@loc_seedline", "");
                        nv1.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                        nv1.Add("@seedsreceived", "");
                        nv1.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        nv1.Add("@SoDate", "");
                        nv1.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                        nv1.Add("@GenusCode", "");
                        nv1.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                        result11 = objCommon.GetDataInsertORUpdate("SP_AddCropHealthReportDetails", nv1);
                    }

                }

                if (result > 0)
                {
                    Chid = result.ToString();
                    BindGridCropHealth();
                    BindSupervisorList();
                    PanelView.Visible = false;
                    PanelList.Visible = true;
                }

            }





            foreach (GridViewRow row in GridViewView.Rows)
            {

                long result16 = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", ddlirrigationSupervisor.SelectedValue);

                nv.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                nv.Add("@Customer", (row.FindControl("lblCustomer1") as Label).Text);
                nv.Add("@Item", (row.FindControl("lblitem1") as Label).Text);
                nv.Add("@Facility", "");
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray1") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize1") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc1") as Label).Text);

                nv.Add("@IrrigationCode", IrrigationCode.ToString());
                // nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                nv.Add("@IrrigatedNoTrays", (row.FindControl("lblTotTray1") as Label).Text);
                nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                nv.Add("@IrrigationDuration", "");
                nv.Add("@SprayDate", txtirrigationSprayDate.Text.Trim());
                //nv.Add("@SprayTime", txtSprayTime.Text.Trim());
                nv.Add("@Nots", txtirrigationNotes.Text.Trim());
                nv.Add("@LoginID", Session["LoginID"].ToString());
                result16 = objCommon.GetDataExecuteScaler("SP_AddIrrigationRequestManual", nv);


            }


            long result18 = 0;
            NameValueCollection nv11 = new NameValueCollection();
            nv11.Add("@Chid", Chid);
            result18 = objCommon.GetDataInsertORUpdate("SP_UpdateCropHealthReport", nv11);

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


            if (Chid == "")
            {
                long result = 0;
                string folderPath = "";
                NameValueCollection nv = new NameValueCollection();
                if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
                {
                    folderPath = Server.MapPath("~/images/");
                    FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));
                }
                else
                {
                    folderPath = "";
                }
                nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
                nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
                nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
                nv.Add("@NoTrays", txtTrays.Text);
                nv.Add("@PerDamage", percentageDamage.Text);
                nv.Add("@Date", txtDate.Text);
                nv.Add("@Filepath", folderPath);
                nv.Add("@CropHealthCommit", txtcomments.Text);
                result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);


                foreach (GridViewRow row in gvFer.Rows)
                {
                    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                    if (chckrw.Checked == true)
                    {
                        long result11 = 0;
                        NameValueCollection nv1 = new NameValueCollection();
                        nv1.Add("@chid", result.ToString());
                        nv1.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                        nv1.Add("@itemno", (row.FindControl("lblitem") as Label).Text);
                        nv1.Add("@itemdescp", (row.FindControl("lblitemdesc") as Label).Text);
                        nv1.Add("@cname", (row.FindControl("lblCustomer") as Label).Text);
                        nv1.Add("@loc_seedline", "");
                        nv1.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                        nv1.Add("@seedsreceived", "");
                        nv1.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        nv1.Add("@SoDate", "");
                        nv1.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                        nv1.Add("@GenusCode", "");
                        nv1.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                        result11 = objCommon.GetDataInsertORUpdate("SP_AddCropHealthReportDetails", nv1);
                    }

                }

                if (result > 0)
                {
                    Chid = result.ToString();
                    BindGridCropHealth();
                    BindSupervisorList();
                    PanelView.Visible = false;
                    PanelList.Visible = true;
                }

            }



            foreach (GridViewRow row in GridViewView.Rows)
            {

                long result = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", ddlplant_readySupervisor.SelectedValue);

                nv.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                nv.Add("@Customer", (row.FindControl("lblCustomer1") as Label).Text);
                nv.Add("@Item", (row.FindControl("lblitem1") as Label).Text);
                nv.Add("@Facility", "");
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray1") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize1") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc1") as Label).Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@ChId",Chid);
                
                result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyRequestManuaNew", nv);


            }


            long result1 = 0;
            NameValueCollection nv111 = new NameValueCollection();
            nv111.Add("@Chid", Chid);
            result1 = objCommon.GetDataInsertORUpdate("SP_UpdateCropHealthReport", nv111);

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

    }
}
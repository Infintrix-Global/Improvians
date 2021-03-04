using Improvians.BAL_Classes;
using Improvians.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
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


        public void BindGridFerReq(string BenchLoc)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", BenchLoc);
            dt = objCommon.GetDataTable("SP_GetFertilizerRequestDetails", nv);

            DataTable dtManual = objFer.GetManualFertilizerRequest(ddlFacility.SelectedValue, BenchLoc, ddlJobNo.SelectedValue);
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
            BindGridFerReq("");
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq(ddlBenchLocation.SelectedValue);
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBenchLocation(ddlFacility.SelectedValue);
            //BindGridFerReq();
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindJobCode(ddlBenchLocation.SelectedValue);

            BindGridFerReq(ddlBenchLocation.SelectedValue);

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
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

            result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);


            foreach (GridViewRow row in gvFer.Rows)
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

            string message = "Assignment Successful";
            string url = "MyTaskGrower.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

            Clear();
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
            ddlJobNo.SelectedIndex = 0;
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
            ddlJobNo.SelectedIndex = 0;
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
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Mode", "12");
            dt = objCommon.GetDataTable("GET_Common", nv1);
            FertilizationCode = Convert.ToInt32(dt.Rows[0]["FCode"]);
            if (Request.QueryString["Chid"] != null)
            {

                foreach (GridViewRow row in GridViewView.Rows)
                {

                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlFertilizationSupervisor.SelectedValue);
                    nv.Add("@Type", radtype.SelectedValue);
                    nv.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                    nv.Add("@Customer", (row.FindControl("lblCustomer1") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem1") as Label).Text);
                    nv.Add("@Facility", "");
                    nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                    nv.Add("@TotalTray", (row.FindControl("lblTotTray1") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize1") as Label).Text);
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc1") as Label).Text);
                    //nv.Add("@WorkOrder", lblwo.Text);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@FertilizationCode", FertilizationCode.ToString());
                    nv.Add("@FertilizationDate", txtFDate.Text);
                    result = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManual", nv);
                    Batchlocation = (row.FindControl("lblGreenHouse1") as Label).Text;
                }

                dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text, "", txtTrays.Text, txtSQFT.Text);

                objTask.AddFertilizerRequestDetails(dtTrays, "0", FertilizationCode, Batchlocation, txtBenchIrrigationFlowRate.Text, txtBenchIrrigationCoverage.Text, txtSprayCoverageperminutes.Text, txtResetSprayTaskForDays.Text);

            }
            else
            {

                foreach (GridViewRow row in gvFer.Rows)
                {

                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlFertilizationSupervisor.SelectedValue);
                    nv.Add("@Type", radtype.SelectedValue);
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
                    nv.Add("@FertilizationCode", FertilizationCode.ToString());
                    nv.Add("@FertilizationDate", txtFDate.Text);
                    result = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManual", nv);
                    Batchlocation = (row.FindControl("lblGreenHouse1") as Label).Text;
                }

                dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text, "", txtTrays.Text, txtSQFT.Text);

                objTask.AddFertilizerRequestDetails(dtTrays, "0", FertilizationCode, Batchlocation, txtBenchIrrigationFlowRate.Text, txtBenchIrrigationCoverage.Text, txtSprayCoverageperminutes.Text, txtResetSprayTaskForDays.Text);

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

        protected void btngerminationSumit_Click(object sender, EventArgs e)
        {
            long result = 0;
            if (Request.QueryString["Chid"] != null)
            {
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

                    nv.Add("@LoginId", Session["LoginID"].ToString());

                    result = objCommon.GetDataInsertORUpdate("SP_AddCropHealthGerminationReques", nv);
                }
            }
            else
            {
                foreach (GridViewRow row in gvFer.Rows)
                {
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@Facility", "");
                    nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                    nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv.Add("@Seeddate", (row.FindControl("lblSeededDate") as Label).Text);
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    nv.Add("@SupervisorID", ddlgerminationSupervisor.SelectedValue);
                    nv.Add("@InspectionDueDate", txtGerDate.Text);
                    nv.Add("@TraysInspected", txtTGerTrays.Text);

                    nv.Add("@LoginId", Session["LoginID"].ToString());

                    result = objCommon.GetDataInsertORUpdate("SP_AddCropHealthGerminationReques", nv);
                }
            }
            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                string message = "Assignment Successful";
                string url = "";
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
    }
}
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





namespace Evo
{
    public partial class CropHealthReport : System.Web.UI.Page
    {

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
        static string ReceiverEmail = "";
        static string folderPath = "";
        General objGeneral = new General();

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

                    Facility = Session["Facility"].ToString();

                    dtTrays.Clear();
                }


                if (Request.QueryString["BatchLoc"] != null)
                {
                    Bench = Request.QueryString["BatchLoc"].ToString();
                }

                if (Request.QueryString["CropAT"] != null)
                {
                    CropRId = Request.QueryString["CropAT"].ToString();
                }


                if (Request.QueryString["JobCode"] != null)
                {
                    JobCode = Request.QueryString["JobCode"].ToString();
                }

                if (Request.QueryString["BatchLoc"] != null && Request.QueryString["JobCode"] != null)
                {

                    BindGridFerReq(Bench, "'" + JobCode + "'");
                }
                BindSupervisor();

                BindFacility();
                BindSupervisorList();
                BindFertilizer();

                BindChemical();
                BinCauseofproblem();
                BindTypeofproblem();
                txtGerDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtFDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtChemicalSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtMoveDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtirrigationSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtgeneralDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");


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

        private string CropRId
        {
            get
            {
                if (ViewState["CropRId"] != null)
                {
                    return (string)ViewState["CropRId"];
                }
                return "";
            }
            set
            {
                ViewState["CropRId"] = value;
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


        private string Facility
        {
            get
            {
                if (ViewState["Facility"] != null)
                {
                    return (string)ViewState["Facility"];
                }
                return "";
            }
            set
            {
                ViewState["Facility"] = value;
            }
        }

        public void BindTypeofproblem()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@mode", "23");

            ddlpr.DataSource = objCommon.GetDataTable("GET_Common", nv);
            ddlpr.DataTextField = "TypeOfProblem";
            ddlpr.DataValueField = "TypeOfProblem";
            ddlpr.DataBind();
            ddlpr.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }


        public void BinCauseofproblem()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@mode", "23");

            DropDownListCause.DataSource = objCommon.GetDataTable("GET_Common", nv);
            DropDownListCause.DataTextField = "CauseOfProblem";
            DropDownListCause.DataValueField = "CauseOfProblem";
            DropDownListCause.DataBind();
            DropDownListCause.Items.Insert(0, new ListItem("--- Select ---", "0"));
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
            txtTGerTrays.Text = "10";
            txtFTrays.Text = tray.ToString();
            txtChemicalTrays.Text = tray.ToString();

            if (BatchLocd != string.Empty)
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
            // dt = objCommon.GetDataTable("SP_GetFertilizerRequestDetails", nv);

            //   DataTable dtManual = objFer.GetManualFertilizerRequestSelect(Session["Facility"].ToString(), BenchLoc, JobNo);

            dt = objTask.GetCreateTaskRequestStart(Session["Facility"].ToString(), BenchLoc, JobNo);

            DataTable dtManual = objTask.GetManualRequestStart(Session["Facility"].ToString(), BenchLoc, JobNo);

            //if (dtManual != null && dtManual.Rows.Count > 0)
            //{
            //    dt.Merge(dtManual);
            //    dt.AcceptChanges();
            //}

            if (dt != null && dt.Rows.Count > 0 && dtManual != null && dtManual.Rows.Count > 0)
            {
                dt.Merge(dtManual);
                dt.AcceptChanges();
                gvFer.DataSource = dt;
                gvFer.DataBind();
                lblJid.Text = dt.Rows[0]["jid"].ToString();
            }
            else if (dtManual != null && dtManual.Rows.Count > 0)
            {
                gvFer.DataSource = dtManual;
                gvFer.DataBind();
                lblJid.Text = dtManual.Rows[0]["jid"].ToString();
            }
            else
            {
                gvFer.DataSource = dt;
                gvFer.DataBind();

                lblJid.Text = dt.Rows[0]["jid"].ToString();
            }

            // lblJid.Text = dt.Rows[0]["jid"].ToString();
            //gvFer.DataSource = dt;
            //gvFer.DataBind();



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
            txtTGerTrays.Text = "10";
            txtFTrays.Text = tray.ToString();
            txtChemicalTrays.Text = tray.ToString();


            BindSQFTofBench(BatchLocd);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            long imgresult = 0;
            NameValueCollection nv = new NameValueCollection();

            //if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
            //{
            //    folderPath = Server.MapPath("~/images/") + Path.GetFileName(FileUpload1.FileName);
            //    FileUpload1.SaveAs(folderPath);
            //}
            //else
            //{
            //    folderPath = "";
            //}
            nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
            nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
            nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
            nv.Add("@NoTrays", txtTrays.Text);
            nv.Add("@PerDamage", percentageDamage.Text);
            nv.Add("@Date", txtDate.Text);
            nv.Add("@Filepath", folderPath);
            nv.Add("@CropHealthCommit", txtcomments.Text);
            nv.Add("@CropRId", CropRId);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@jid", lblJid.Text);
            result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReportStart", nv);

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile file = Request.Files[i];
                if (file.ContentLength > 0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    string folderSavePath = WebConfigurationManager.AppSettings["PortalURL"] + @"\uploads\" + fname;
                    NameValueCollection nvimg = new NameValueCollection();
                    folderPath = Server.MapPath("~/uploads/");
                    file.SaveAs(folderPath + Path.GetFileName(fname));
                    nvimg.Add("@chid ", result.ToString());
                    nvimg.Add("@ImageName", fname);
                    nvimg.Add("@Imagepath", folderSavePath);
                    imgresult = objCommon.GetDataExecuteScaler("InsertCropHealthImage", nvimg);
                }
            }


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
                    nv1.Add("@loc_seedline", Session["Facility"].ToString());
                    nv1.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                    nv1.Add("@seedsreceived", "");
                    nv1.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                    nv1.Add("@SoDate", "");
                    nv1.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);

                    nv1.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                    nv1.Add("@Jid", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    nv1.Add("@GenusCode", (row.FindControl("lblGenusCode") as Label).Text);
                    result1 = objCommon.GetDataInsertORUpdate("SP_AddCropHealthReportDetails", nv1);
                }
            }
            string url = "";

            if (Session["Role"].ToString() == "1")
            {
                url = "MyTaskGrower.aspx";
            }
            else if (Session["Role"].ToString() == "12")
            {
                url = "MyTaskAssistantGrower.aspx";
            }
            else if (Session["Role"].ToString() == "2")
            {
                url = "MyTaskGreenSupervisorFinal.aspx";
            }
            else
            {
                url = "MyTaskSpray.aspx";
            }
            string message = "Completion Successful";

            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            Clear();
            //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Completion Successful')", true);

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
            //FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

            ////Display the success message.
            //lblMessage.Text = Path.GetFileName(FileUpload1.FileName) + " has been uploaded.";

        }


        //---------------------------------------------------------------- Tab Details-------
        public void BindSQFTofBench(string benchLoc)
        {
            txtSQFT.Text = "0.00";
            if (!string.IsNullOrEmpty(benchLoc))
            {
                DataTable dtSQFT = objFer.GetSQFTofBench("'" + benchLoc + "'");
                if (dtSQFT != null && dtSQFT.Rows.Count > 0)
                {
                    txtSQFT.Text = Convert.ToDecimal(dtSQFT.Rows[0]["Sqft"]).ToString("#,0000.00");
                    txtChemicalSQFTofBench.Text = Convert.ToDecimal(dtSQFT.Rows[0]["Sqft"]).ToString("#,0000.00");
                }
            }
        }
        public void BindSupervisor()
        {


            NameValueCollection nv = new NameValueCollection();

            DataTable dt = new DataTable();
            if (Session["Role"].ToString() == "1")
            {
                dt = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
                //   dt = objCommon.GetDataTable("SP_GetRoleForGrowerNew", nv);

            }
            else if (Session["Role"].ToString() == "12")
            {
                dt = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
            }
            else
            {


                nv.Add("@RoleID", "3");
                dt = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;

            }


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

        protected void btnFReset_Click(object sender, EventArgs e)
        {

        }

        public void SubmitFertilization(string Assign)
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
                long imgresult = 0;
                string folderPath = "";
                NameValueCollection nv = new NameValueCollection();

                nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
                nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
                nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
                nv.Add("@NoTrays", txtTrays.Text);
                nv.Add("@PerDamage", percentageDamage.Text);
                nv.Add("@Date", txtDate.Text);
                nv.Add("@Filepath", folderPath);
                nv.Add("@CropHealthCommit", txtcomments.Text);
                result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile file = Request.Files[i];
                    if (file.ContentLength > 0)
                    {
                        string fname = Path.GetFileName(file.FileName);
                        string folderSavePath = WebConfigurationManager.AppSettings["PortalURL"] + @"\uploads\" + fname;
                        NameValueCollection nvimg = new NameValueCollection();
                        folderPath = Server.MapPath("~/uploads/");
                        file.SaveAs(folderPath + Path.GetFileName(fname));
                        nvimg.Add("@chid ", result.ToString());
                        nvimg.Add("@ImageName", fname);
                        nvimg.Add("@Imagepath", folderSavePath);
                        imgresult = objCommon.GetDataExecuteScaler("InsertCropHealthImage", nvimg);
                    }
                }
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
                        nv1.Add("@loc_seedline", Session["Facility"].ToString());
                        nv1.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                        nv1.Add("@seedsreceived", "");
                        nv1.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        nv1.Add("@SoDate", "");
                        nv1.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);

                        nv1.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                        nv1.Add("@Jid", (row.FindControl("lblj_id") as Label).Text);
                        nv1.Add("@GenusCode", (row.FindControl("lblGenusCode") as Label).Text);
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
                //nv4.Add("@SupervisorID", ddlFertilizationSupervisor.SelectedValue);
                //nv4.Add("@Type", "Fertilizer");
                //nv4.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                //nv4.Add("@Customer", (row.FindControl("lblCustomer1") as Label).Text);
                //nv4.Add("@Item", (row.FindControl("lblitem1") as Label).Text);
                //nv4.Add("@Facility", Session["Facility"].ToString());
                //nv4.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                //nv4.Add("@TotalTray", (row.FindControl("lblTotTray1") as Label).Text);
                //nv4.Add("@TraySize", (row.FindControl("lblTraySize1") as Label).Text);
                //nv4.Add("@Itemdesc", (row.FindControl("lblitemdesc1") as Label).Text);
                //nv4.Add("@SeedDate", (row.FindControl("lblSeededDate1") as Label).Text);
                //nv4.Add("@LoginID", Session["LoginID"].ToString());
                //nv4.Add("@FertilizationCode", FertilizationCode.ToString());
                //nv4.Add("@FertilizationDate", txtFDate.Text);
                //result2 = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManual", nv4);
                Batchlocation = (row.FindControl("lblGreenHouse1") as Label).Text;


                //  NameValueCollection nv4 = new NameValueCollection();

                var jobCode = (row.FindControl("lblID1") as Label).Text;

                nv4.Add("@SupervisorID", Assign);
                nv4.Add("@Type", "Fertilizer");
                nv4.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                nv4.Add("@Customer", (row.FindControl("lblCustomer1") as Label).Text);
                nv4.Add("@Item", (row.FindControl("lblitem1") as Label).Text);
                nv4.Add("@Facility", Session["Facility"].ToString());
                nv4.Add("@GreenHouseID", Batchlocation);
                nv4.Add("@TotalTray", (row.FindControl("lblTotTray1") as Label).Text);
                nv4.Add("@TraySize", (row.FindControl("lblTraySize1") as Label).Text);
                nv4.Add("@Itemdesc", (row.FindControl("lblSeededDate1") as Label).Text);
                //nv.Add("@WorkOrder", lblwo.Text);
                nv4.Add("@LoginID", Session["LoginID"].ToString());
                nv4.Add("@FertilizationCode", FertilizationCode.ToString());
                nv4.Add("@FertilizationDate", txtFDate.Text);
                nv4.Add("@seedDate", (row.FindControl("lblSeededDate1") as Label).Text);
                nv4.Add("@Jid", (row.FindControl("lblJid") as Label).Text);
                result2 = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManualCreateTask", nv4);


                NameValueCollection nvn = new NameValueCollection();
                nvn.Add("@LoginID", Session["LoginID"].ToString());
                nvn.Add("@SupervisorID", Assign);
                nvn.Add("@Jobcode", jobCode);
                nvn.Add("@TaskName", "Fertilizer");
                nvn.Add("@GreenHouseID", Batchlocation);
                var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);

            }

            dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text, "", txtFTrays.Text, txtSQFT.Text);

            objTask.AddFertilizerRequestDetailsCreatTask(dtTrays, "0", FertilizationCode, Batchlocation, "", "", "", txtResetSprayTaskForDays.Text, txtcomments.Text);
            long result16 = 0;
            NameValueCollection nv11 = new NameValueCollection();
            nv11.Add("@Chid", Chid);
            result16 = objCommon.GetDataInsertORUpdate("SP_UpdateCropHealthReport", nv11);
            objGeneral.SendMessage(int.Parse(Assign), "New Fertilizer Task Assigned", "New Fertilizer Task Assigned", "Fertilizer");

            //string message = "Assignment Successful";
            //string url = "MyTaskGrower.aspx";
            //string script = "window.onload = function(){ alert('";
            //script += message;
            //script += "');";
            //script += "window.location = '";
            //script += url;
            //script += "'; }";
            //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
            updateNotification();
        }

        protected void btnFSubmit_Click(object sender, EventArgs e)
        {
            SubmitFertilization(ddlFertilizationSupervisor.SelectedValue);
        }

        protected void btnSaveFLSubmit_Click(object sender, EventArgs e)
        {
            SubmitFertilization(Session["LoginID"].ToString());
        }

        protected void btnBSaveSubmit_Click(object sender, EventArgs e)
        {
            SubmitGermination(Session["LoginID"].ToString());
        }
        protected void btngerminationSumit_Click(object sender, EventArgs e)
        {
            SubmitGermination(ddlgerminationSupervisor.SelectedValue);
        }

        public void SubmitGermination(string Assign)
        {
            if (Chid == "")
            {
                long result = 0;
                long imgresult = 0;
                string folderPath = "";
                NameValueCollection nv = new NameValueCollection();

                nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
                nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
                nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
                nv.Add("@NoTrays", txtTrays.Text);
                nv.Add("@PerDamage", percentageDamage.Text);
                nv.Add("@Date", txtDate.Text);
                nv.Add("@Filepath", folderPath);
                nv.Add("@CropHealthCommit", txtcomments.Text);
                result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile file = Request.Files[i];
                    if (file.ContentLength > 0)
                    {
                        string fname = Path.GetFileName(file.FileName);
                        string folderSavePath = WebConfigurationManager.AppSettings["PortalURL"] + @"\uploads\" + fname;
                        NameValueCollection nvimg = new NameValueCollection();
                        folderPath = Server.MapPath("~/uploads/");
                        file.SaveAs(folderPath + Path.GetFileName(fname));
                        nvimg.Add("@chid ", result.ToString());
                        nvimg.Add("@ImageName", fname);
                        nvimg.Add("@Imagepath", folderSavePath);
                        imgresult = objCommon.GetDataExecuteScaler("InsertCropHealthImage", nvimg);
                    }
                }
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
                        nv1.Add("@loc_seedline", Session["Facility"].ToString());
                        nv1.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                        nv1.Add("@seedsreceived", "");
                        nv1.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        nv1.Add("@SoDate", "");
                        nv1.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);

                        nv1.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                        nv1.Add("@Jid", (row.FindControl("lblj_id") as Label).Text);
                        nv1.Add("@GenusCode", (row.FindControl("lblGenusCode") as Label).Text);
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
                nv.Add("@Facility", Session["Facility"].ToString());
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray1") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize1") as Label).Text);
                nv.Add("@Seeddate", (row.FindControl("lblSeededDate1") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc1") as Label).Text);
                nv.Add("@SupervisorID", Assign);
                nv.Add("@InspectionDueDate", txtGerDate.Text);
                nv.Add("@TraysInspected", txtTGerTrays.Text);
                nv.Add("@Chid", Chid);
                nv.Add("@LoginId", Session["LoginID"].ToString());
                nv.Add("@Comments", txtcomments.Text);

                result16 = objCommon.GetDataInsertORUpdate("SP_AddCropHealthGerminationReques", nv);
            }

            if (result16 > 0)
            {
                long result1 = 0;
                NameValueCollection nv11 = new NameValueCollection();
                nv11.Add("@Chid", Chid);
                result1 = objCommon.GetDataInsertORUpdate("SP_UpdateCropHealthReport", nv11);
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                objGeneral.SendMessage(int.Parse(Assign), "New Germination Task Assigned", "New Germination Task Assigned", "Germination");

                //string message = "Assignment Successful";
                //string url = "MyTaskGrower.aspx";
                //string script = "window.onload = function(){ alert('";
                //script += message;
                //script += "');";
                //script += "window.location = '";
                //script += url;
                //script += "'; }";
                //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                // lblmsg.Text = "Assignment Successful";
                //  clear();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }

            updateNotification();
        }

        protected void btngerminationReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                long result = 0;

                NameValueCollection nv = new NameValueCollection();
                // nv.Add("@OperatorID", Session["LoginID"].ToString());
                //nv.Add("@wo", wo);
                nv.Add("@Comments", txtcomments.Text.Trim());
                // nv.Add("@AsssigneeID", ddlAssignments.SelectedValue);
                nv.Add("@AsssigneeID", Session["SelectedAssignment"].ToString());
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

            Session["SelectedAssignment"] = ddlAssignments.SelectedValue;
            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@Uid", ddlAssignments.SelectedValue);
            //DataTable dt = objCommon.GetDataTable("getReceiverEmail", nv);
            //ReceiverEmail = dt.Rows[0]["Email"].ToString();
            divcomments.Focus();
        }
        //----------------------------------------------------------------------irrigatio

        protected void btnirrigationReset_Click1(object sender, EventArgs e)
        {
            //            
        }
        protected void btnSaveirrigation_Click(object sender, EventArgs e)
        {
            SubmitIrrigation(Session["LoginID"].ToString());
        }

        public void SubmitIrrigation(string Assign)
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
                long imgresult = 0;
                string folderPath = "";
                NameValueCollection nv = new NameValueCollection();

                nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
                nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
                nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
                nv.Add("@NoTrays", txtTrays.Text);
                nv.Add("@PerDamage", percentageDamage.Text);
                nv.Add("@Date", txtDate.Text);
                nv.Add("@Filepath", folderPath);
                nv.Add("@CropHealthCommit", txtcomments.Text);
                result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile file = Request.Files[i];
                    if (file.ContentLength > 0)
                    {
                        string fname = Path.GetFileName(file.FileName);
                        string folderSavePath = WebConfigurationManager.AppSettings["PortalURL"] + @"\uploads\" + fname;
                        NameValueCollection nvimg = new NameValueCollection();
                        folderPath = Server.MapPath("~/uploads/");
                        file.SaveAs(folderPath + Path.GetFileName(fname));
                        nvimg.Add("@chid ", result.ToString());
                        nvimg.Add("@ImageName", fname);
                        nvimg.Add("@Imagepath", folderSavePath);
                        imgresult = objCommon.GetDataExecuteScaler("InsertCropHealthImage", nvimg);
                    }
                }


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
                        nv1.Add("@loc_seedline", Session["Facility"].ToString());
                        nv1.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                        nv1.Add("@seedsreceived", "");
                        nv1.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        nv1.Add("@SoDate", "");
                        nv1.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                        nv1.Add("@Jid", (row.FindControl("lblj_id") as Label).Text);
                        nv1.Add("@GenusCode", (row.FindControl("lblGenusCode") as Label).Text);
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
                nv.Add("@SupervisorID", Assign);

                nv.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                nv.Add("@Customer", (row.FindControl("lblCustomer1") as Label).Text);
                nv.Add("@Item", (row.FindControl("lblitem1") as Label).Text);
                nv.Add("@Facility", Session["Facility"].ToString());
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
                nv.Add("@SeedDate", (row.FindControl("lblSeededDate1") as Label).Text);


                nv.Add("@Nots", txtcomments.Text.Trim());
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@Role", Session["Role"].ToString());
                nv.Add("@Jid", (row.FindControl("lblJid") as Label).Text);
                result16 = objCommon.GetDataExecuteScaler("SP_AddIrrigationRequestManualCreateTask", nv);

            }


            long result18 = 0;
            NameValueCollection nv11 = new NameValueCollection();
            nv11.Add("@Chid", Chid);
            result18 = objCommon.GetDataInsertORUpdate("SP_UpdateCropHealthReport", nv11);
            objGeneral.SendMessage(int.Parse(Assign), "New Irrigation Task Assigned", "New Irrigation Task Assigned", "Irrigation");


            //string message = "Assignment Successful";
            //string url = "MyTaskGrower.aspx";
            //string script = "window.onload = function(){ alert('";
            //script += message;
            //script += "');";
            //script += "window.location = '";
            //script += url;
            //script += "'; }";
            //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);

            updateNotification();
        }

        protected void btnirrigationSubmit_Click(object sender, EventArgs e)
        {
            SubmitIrrigation(ddlirrigationSupervisor.SelectedValue);
        }

        public void SubmitPlantReady(string Assign)
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
                long imgresult = 0;
                string folderPath = "";
                NameValueCollection nv = new NameValueCollection();

                //if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
                //{
                //    folderPath = Server.MapPath("~/images/");
                //    FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));
                //}
                //else
                //{
                //    folderPath = "";
                //}
                nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
                nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
                nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
                nv.Add("@NoTrays", txtTrays.Text);
                nv.Add("@PerDamage", percentageDamage.Text);
                nv.Add("@Date", txtDate.Text);
                nv.Add("@Filepath", folderPath);
                nv.Add("@CropHealthCommit", txtcomments.Text);
                result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile file = Request.Files[i];
                    if (file.ContentLength > 0)
                    {
                        string fname = Path.GetFileName(file.FileName);
                        string folderSavePath = WebConfigurationManager.AppSettings["PortalURL"] + @"\uploads\" + fname;
                        NameValueCollection nvimg = new NameValueCollection();
                        folderPath = Server.MapPath("~/uploads/");
                        file.SaveAs(folderPath + Path.GetFileName(fname));
                        nvimg.Add("@chid ", result.ToString());
                        nvimg.Add("@ImageName", fname);
                        nvimg.Add("@Imagepath", folderSavePath);
                        imgresult = objCommon.GetDataExecuteScaler("InsertCropHealthImage", nvimg);
                    }
                }

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
                        nv1.Add("@loc_seedline", Session["Facility"].ToString());
                        nv1.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                        nv1.Add("@seedsreceived", "");
                        nv1.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        nv1.Add("@SoDate", "");
                        nv1.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                        nv1.Add("@Jid", (row.FindControl("lblj_id") as Label).Text);
                        nv1.Add("@GenusCode", (row.FindControl("lblGenusCode") as Label).Text);
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
                nv.Add("@SupervisorID", Assign);
                nv.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                nv.Add("@Customer", (row.FindControl("lblCustomer1") as Label).Text);
                nv.Add("@Item", (row.FindControl("lblitem1") as Label).Text);
                nv.Add("@Facility", Session["Facility"].ToString());
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray1") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize1") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc1") as Label).Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@ChId", Chid);
                nv.Add("@Comments", txtcomments.Text.Trim());
                nv.Add("@SeedDate", (row.FindControl("lblSeededDate1") as Label).Text);
                nv.Add("@PlantDate", txtPlantDate.Text);
                nv.Add("@Comments", txtcomments.Text);
                nv.Add("@Role", Session["Role"].ToString());
                nv.Add("@Jid", (row.FindControl("lblJid") as Label).Text);

                result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyRequestManuaCreateTask", nv);

            }


            long result1 = 0;
            NameValueCollection nv111 = new NameValueCollection();
            nv111.Add("@Chid", Chid);
            result1 = objCommon.GetDataInsertORUpdate("SP_UpdateCropHealthReport", nv111);
            objGeneral.SendMessage(int.Parse(Assign), "New Plant Ready Task Assigned", "New Plant Ready Task Assigned", "Plant Ready");

            //string message = "Assignment Successful";
            //string url = "MyTaskGrower.aspx";
            //string script = "window.onload = function(){ alert('";
            //script += message;
            //script += "');";
            //script += "window.location = '";
            //script += url;
            //script += "'; }";
            //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);

            updateNotification();
        }
        protected void btnplant_readySubmit_Click(object sender, EventArgs e)
        {
            SubmitPlantReady(ddlplant_readySupervisor.SelectedValue);
        }

        protected void btnSavePlantReady_Click(object sender, EventArgs e)
        {
            SubmitPlantReady(Session["LoginID"].ToString());
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

        protected void ddlTaskType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SelectedAssignment"] = ddlAssignments.SelectedValue;
            // general_task_request.Focus();
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
            general_task_request.Attributes.Add("class", "request__block-collapse collapse show");

            divcomments.Focus();
        }

        public void Submitgeneraltask(string Assign)
        {
            if (Chid == "")
            {
                long result = 0;
                long imgresult = 0;
                string folderPath = "";
                NameValueCollection nv = new NameValueCollection();

                nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
                nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
                nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
                nv.Add("@NoTrays", txtTrays.Text);
                nv.Add("@PerDamage", percentageDamage.Text);
                nv.Add("@Date", txtDate.Text);
                nv.Add("@Filepath", folderPath);
                nv.Add("@CropHealthCommit", txtcomments.Text);
                result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile file = Request.Files[i];
                    if (file.ContentLength > 0)
                    {
                        string fname = Path.GetFileName(file.FileName);
                        string folderSavePath = WebConfigurationManager.AppSettings["PortalURL"] + @"\uploads\" + fname;
                        NameValueCollection nvimg = new NameValueCollection();
                        folderPath = Server.MapPath("~/uploads/");
                        file.SaveAs(folderPath + Path.GetFileName(fname));
                        nvimg.Add("@chid", result.ToString());
                        nvimg.Add("@ImageName", fname);
                        nvimg.Add("@Imagepath", folderSavePath);
                        imgresult = objCommon.GetDataInsertORUpdate("InsertCropHealthImage", nvimg);
                    }
                }

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
                        nv1.Add("@loc_seedline", Session["Facility"].ToString());
                        nv1.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                        nv1.Add("@seedsreceived", "");
                        nv1.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        nv1.Add("@SoDate", "");
                        nv1.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                        nv1.Add("@Jid", (row.FindControl("lblj_id") as Label).Text);
                        nv1.Add("@GenusCode", (row.FindControl("lblGenusCode") as Label).Text);
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
                nv.Add("@Facility", Session["Facility"].ToString());
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray1") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize1") as Label).Text);
                nv.Add("@Seeddate", (row.FindControl("lblSeededDate1") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc1") as Label).Text);
                nv.Add("@SupervisorID", Assign);

                nv.Add("@TaskType", ddlTaskType.SelectedValue);
                nv.Add("@MoveFrom", txtFrom.Text);
                nv.Add("@MoveTo", txtTo.Text);
                nv.Add("@date", txtgeneralDate.Text);
                nv.Add("@RoleId", Session["Role"].ToString());
                nv.Add("@Comments", txtgeneralComment.Text);
                nv.Add("@Chid", Chid);
                nv.Add("@LoginId", Session["LoginID"].ToString());

                result16 = objCommon.GetDataInsertORUpdate("SP_AddCropHealthGeneralTaskRequest", nv);

                //NameValueCollection nvn = new NameValueCollection();
                //nvn.Add("@LoginID", Session["LoginID"].ToString());
                //nvn.Add("@SupervisorID", Assign);
                //nvn.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                //nvn.Add("@TaskName", "GeneralTask");
                //nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                //var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);
            }

            if (result16 > 0)
            {
                long result1 = 0;
                NameValueCollection nv11 = new NameValueCollection();
                nv11.Add("@Chid", Chid);
                result1 = objCommon.GetDataInsertORUpdate("SP_UpdateCropHealthReport", nv11);

                /*
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                string FromMail = WebConfigurationManager.AppSettings["FromEmail"];
                string FromEmailPassword = WebConfigurationManager.AppSettings["FromEmailPassword"];
                smtpClient.Credentials = new System.Net.NetworkCredential(FromMail, FromEmailPassword);
               
                // smtpClient.U'seDefaultCredentials = true; // uncomment if you don't want to use the network credentials
                
                string CCEmail = "";
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.Subject = "Crop Health Report";
                mail.Body = "Crop Health Report Comments:" + txtcomments.Text;
                //Setting From , To and CC

                mail.From = new MailAddress(FromMail);
                NameValueCollection nv = new NameValueCollection();

                var getToMail = Session["SelectedAssignment"].ToString();
                //var getToMail = ddlAssignments.SelectedValue;
                nv.Add("@Uid", getToMail);
                DataTable dt = objCommon.GetDataTable("getReceiverEmail", nv);
                ReceiverEmail = dt.Rows[0]["Email"].ToString();

                mail.To.Add(new MailAddress(ReceiverEmail));

                nv.Clear();
                var getCCMail = Session["Role"].ToString();
                nv.Add("@Uid", getCCMail);
                dt = objCommon.GetDataTable("getReceiverEmail", nv);
                CCEmail = dt.Rows[0]["Email"].ToString();
                mail.CC.Add(new MailAddress(CCEmail));
                //  Attachment atc = new Attachment(folderPath, "Uploded Picture");
                //   mail.Attachments.Add(atc);
                smtpClient.Send(mail);
                */

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);

                //Response.Redirect("MyTaskGrower.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }
            updateNotification();
        }

        protected void btnSaveGeneral_Click(object sender, EventArgs e)
        {

            Submitgeneraltask(Session["LoginID"].ToString());
        }
        protected void btnGeneralSubmit_Click(object sender, EventArgs e)
        {
            Submitgeneraltask(Session["SelectedAssignment"].ToString());
        }

        public void SubmitChemical(string Assign)
        {
            string Batchlocation = "";
            int ChemicalCode = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv14 = new NameValueCollection();

            nv14.Add("@Mode", "16");
            dt = objCommon.GetDataTable("GET_Common", nv14);
            ChemicalCode = Convert.ToInt32(dt.Rows[0]["CCode"]);

            if (Chid == "")
            {
                long result = 0;
                long imgresult = 0;
                string folderPath = "";
                NameValueCollection nv = new NameValueCollection();

                nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
                nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
                nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
                nv.Add("@NoTrays", txtTrays.Text);
                nv.Add("@PerDamage", percentageDamage.Text);
                nv.Add("@Date", txtDate.Text);
                nv.Add("@Filepath", folderPath);
                nv.Add("@CropHealthCommit", txtcomments.Text);
                result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile file = Request.Files[i];
                    if (file.ContentLength > 0)
                    {
                        string fname = Path.GetFileName(file.FileName);
                        string folderSavePath = WebConfigurationManager.AppSettings["PortalURL"] + @"\uploads\" + fname;
                        NameValueCollection nvimg = new NameValueCollection();
                        folderPath = Server.MapPath("~/uploads/");
                        file.SaveAs(folderPath + Path.GetFileName(fname));
                        nvimg.Add("@chid ", result.ToString());
                        nvimg.Add("@ImageName", fname);
                        nvimg.Add("@Imagepath", folderSavePath);
                        imgresult = objCommon.GetDataExecuteScaler("InsertCropHealthImage", nvimg);
                    }
                }
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
                        nv1.Add("@loc_seedline", Session["Facility"].ToString());
                        nv1.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                        nv1.Add("@seedsreceived", "");
                        nv1.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        nv1.Add("@SoDate", "");
                        nv1.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                        nv1.Add("@Jid", (row.FindControl("lblj_id") as Label).Text);
                        nv1.Add("@GenusCode", (row.FindControl("lblGenusCode") as Label).Text);
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

            long result3 = 0;
            foreach (GridViewRow row in GridViewView.Rows)
            {
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", Assign);
                nv.Add("@Type", "Chemical");
                nv.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                nv.Add("@Customer", (row.FindControl("lblCustomer1") as Label).Text);
                nv.Add("@Item", (row.FindControl("lblitem1") as Label).Text);
                nv.Add("@Facility", Session["Facility"].ToString());
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray1") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize1") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc1") as Label).Text);
                //nv.Add("@WorkOrder", lblwo.Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@ChemicalCode", ChemicalCode.ToString());
                nv.Add("@ChemicalDate", txtChemicalSprayDate.Text);
                nv.Add("@Comments", txtcomments.Text);
                nv.Add("@Method", ddlMethod.SelectedValue);
                nv.Add("@seedDate", (row.FindControl("lblSeededDate1") as Label).Text);
                nv.Add("@Jid", (row.FindControl("lblJid") as Label).Text);

                result3 = objCommon.GetDataExecuteScaler("SP_AddChemicalRequestManualCreateTask", nv);
                Batchlocation = (row.FindControl("lblGreenHouse1") as Label).Text;

                NameValueCollection nvn = new NameValueCollection();
                nvn.Add("@LoginID", Session["LoginID"].ToString());
                nvn.Add("@SupervisorID", Assign);
                nvn.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                nvn.Add("@TaskName", "Chemical");
                nvn.Add("@TaskRequestKey", "");
                nvn.Add("@GreenHouseID", Batchlocation);
                var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);

            }

            dtCTrays.Rows.Add(ddlChemical.SelectedItem.Text, txtChemicalTrays.Text, txtSQFT.Text);
            objTask.AddChemicalRequestDetails(dtCTrays, result3.ToString(), ddlChemical.SelectedValue, ChemicalCode, Batchlocation, txtResetSprayTaskForDays.Text, ddlMethod.SelectedValue, txtcomments.Text);

            long result16 = 0;
            NameValueCollection nv11 = new NameValueCollection();
            nv11.Add("@Chid", Chid);
            result16 = objCommon.GetDataInsertORUpdate("SP_UpdateCropHealthReport", nv11);

            objGeneral.SendMessage(int.Parse(Assign), "New Chemical Task Assigned", "New Chemical Task Assigned", "Chemical");

            //string message = "Assignment Successful";
            //string url = "MyTaskGrower.aspx";
            //string script = "window.onload = function(){ alert('";
            //script += message;
            //script += "');";
            //script += "window.location = '";
            //script += url;
            //script += "'; }";
            //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);

            updateNotification();
        }

        protected void btnChemicalSubmit_Click(object sender, EventArgs e)
        {
            SubmitChemical(ddlChemical_supervisor.SelectedValue);
        }

        protected void btnChemicalSFLSubmit_Click(object sender, EventArgs e)
        {
            SubmitChemical(Session["LoginID"].ToString());
        }

        public void BindFacility()
        {
            ddlToFacility.DataSource = objBAL.GetMainLocation();
            ddlToFacility.DataTextField = "l1";
            ddlToFacility.DataValueField = "l1";
            ddlToFacility.DataBind();
            ddlToFacility.Items.Insert(0, new ListItem("--- Select ---", "0"));
            ddlToFacility.SelectedValue = Session["Facility"].ToString();
            BindBench_Location();
        }

        public void BindBench_Location()
        {
            //  nv.Add("@FacilityID", ddlToFacility.SelectedValue);
            ddlToGreenHouse.DataSource = objBAL.GetLocation(ddlToFacility.SelectedValue);
            ddlToGreenHouse.DataTextField = "p2";
            ddlToGreenHouse.DataValueField = "p2";
            ddlToGreenHouse.DataBind();
            ddlToGreenHouse.Items.Insert(0, new ListItem("--- Select ---", "0"));

        }

        protected void btnChemicalReset_Click(object sender, EventArgs e)
        {

        }

        protected void ddlToFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBench_Location();
        }

        protected void btnMoveSubmit_Click(object sender, EventArgs e)
        {
            MoveSubmit(ddlLogisticManager.SelectedValue);
        }

        protected void btnSaveMove_Click(object sender, EventArgs e)
        {
            MoveSubmit(Session["LoginID"].ToString());
        }

        public void MoveSubmit(string Assign)
        {
            if (Chid == "")
            {
                long result = 0;
                long imgresult = 0;
                string folderPath = "";
                NameValueCollection nv = new NameValueCollection();


                nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
                nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
                nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
                nv.Add("@NoTrays", txtTrays.Text);
                nv.Add("@PerDamage", percentageDamage.Text);
                nv.Add("@Date", txtDate.Text);
                nv.Add("@Filepath", folderPath);
                nv.Add("@CropHealthCommit", txtcomments.Text);
                result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile file = Request.Files[i];
                    if (file.ContentLength > 0)
                    {
                        string fname = Path.GetFileName(file.FileName);
                        string folderSavePath = WebConfigurationManager.AppSettings["PortalURL"] + @"\uploads\" + fname;
                        NameValueCollection nvimg = new NameValueCollection();
                        folderPath = Server.MapPath("~/uploads/");
                        file.SaveAs(folderPath + Path.GetFileName(fname));
                        nvimg.Add("@chid", result.ToString());
                        nvimg.Add("@ImageName", fname);
                        nvimg.Add("@Imagepath", folderSavePath);
                        imgresult = objCommon.GetDataInsertORUpdate("InsertCropHealthImage", nvimg);
                    }
                }
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
                        nv1.Add("@loc_seedline", Session["Facility"].ToString());
                        nv1.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                        nv1.Add("@seedsreceived", "");
                        nv1.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        nv1.Add("@SoDate", "");
                        nv1.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                        nv1.Add("@Jid", (row.FindControl("lblj_id") as Label).Text);
                        nv1.Add("@GenusCode", (row.FindControl("lblGenusCode") as Label).Text);
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
                nv.Add("@SupervisorID", Assign);
                nv.Add("@WorkOrder", "0");
                nv.Add("@GrowerPutAwayID", "0");

                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@FromFacility", Session["Facility"].ToString());
                nv.Add("@ToFacility", ddlToFacility.SelectedValue);
                nv.Add("@ToGreenHouse", ddlToGreenHouse.SelectedValue);
                nv.Add("@Trays", (row.FindControl("lblTotTray1") as Label).Text);
                nv.Add("@MoveDate", txtMoveDate.Text);


                nv.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                nv.Add("@Customer", (row.FindControl("lblCustomer1") as Label).Text);
                nv.Add("@Item", (row.FindControl("lblitem1") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize1") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc1") as Label).Text);
                nv.Add("@ChId", Chid);
                nv.Add("@Comments", txtcomments.Text.Trim());

                nv.Add("@SeedDate", (row.FindControl("lblSeededDate1") as Label).Text);

                nv.Add("@FormBanchlocation", (row.FindControl("lblGreenHouse1") as Label).Text);
                nv.Add("@RTrays", txtMoveNumberOfTrays.Text);
                nv.Add("@Jid", (row.FindControl("lblJid") as Label).Text);
                result16 = objCommon.GetDataExecuteScaler("SP_AddMoveRequestManualCreateTask", nv);

                NameValueCollection nvn = new NameValueCollection();
                nvn.Add("@LoginID", Session["LoginID"].ToString());
                nvn.Add("@SupervisorID", Assign);
                nvn.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                nvn.Add("@TaskName", "Move");
                nvn.Add("@TaskRequestKey", "");
                nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);

            }

            if (result16 > 0)
            {
                long result1 = 0;
                NameValueCollection nv11 = new NameValueCollection();
                nv11.Add("@Chid", Chid);
                result1 = objCommon.GetDataInsertORUpdate("SP_UpdateCropHealthReport", nv11);
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                objGeneral.SendMessage(int.Parse(Assign), "New Move Task Assigned", "New Move Task Assigned", "Move");

                //string message = "Assignment Successful";
                //string url = "MyTaskGrower.aspx";
                //string script = "window.onload = function(){ alert('";
                //script += message;
                //script += "');";
                //script += "window.location = '";
                //script += url;
                //script += "'; }";
                //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                // lblmsg.Text = "Assignment Successful";
                //  clear();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }

            updateNotification();
        }

        protected void MoveReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnGeneralReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void SubmitDump(string Assign)
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
                long imgresult = 0;
                string folderPath = "";

                NameValueCollection nv = new NameValueCollection();

                //if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
                //{
                //    folderPath = Server.MapPath("~/images/");
                //    FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));
                //}
                //else
                //{
                //    folderPath = "";
                //}
                nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
                nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
                nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
                nv.Add("@NoTrays", txtTrays.Text);
                nv.Add("@PerDamage", percentageDamage.Text);
                nv.Add("@Date", txtDate.Text);
                nv.Add("@Filepath", folderPath);
                nv.Add("@CropHealthCommit", txtcomments.Text);
                result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile file = Request.Files[i];
                    if (file.ContentLength > 0)
                    {
                        // string folderSavePath = WebConfigurationManager.AppSettings["PortalURL"] + @"\EmployeeProfile\" + Imgname;
                        string fname = Path.GetFileName(file.FileName);
                        string folderSavePath = WebConfigurationManager.AppSettings["PortalURL"] + @"\uploads\" + fname;
                        NameValueCollection nvimg = new NameValueCollection();
                        folderPath = Server.MapPath("~/uploads/");

                        file.SaveAs(folderPath + Path.GetFileName(fname));
                        nvimg.Add("@chid ", result.ToString());
                        nvimg.Add("@ImageName", fname);
                        nvimg.Add("@Imagepath", folderSavePath);
                        imgresult = objCommon.GetDataExecuteScaler("InsertCropHealthImage", nvimg);
                    }
                }

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
                        nv1.Add("@loc_seedline", Session["Facility"].ToString());
                        nv1.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                        nv1.Add("@seedsreceived", "");
                        nv1.Add("@SeedDate", (row.FindControl("lblSeededDate") as Label).Text);
                        nv1.Add("@SoDate", "");
                        nv1.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                        nv1.Add("@Jid", (row.FindControl("lblj_id") as Label).Text);
                        nv1.Add("@GenusCode", (row.FindControl("lblGenusCode") as Label).Text);
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
                ////NameValueCollection nv = new NameValueCollection();
                ////nv.Add("@SupervisorID", Assign);
                ////nv.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                ////nv.Add("@Customer", (row.FindControl("lblCustomer1") as Label).Text);
                ////nv.Add("@Item", (row.FindControl("lblitem1") as Label).Text);
                ////nv.Add("@Facility", Session["Facility"].ToString());
                ////nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                ////nv.Add("@TotalTray", (row.FindControl("lblTotTray1") as Label).Text);
                ////nv.Add("@TraySize", (row.FindControl("lblTraySize1") as Label).Text);
                ////nv.Add("@Itemdesc", (row.FindControl("lblitemdesc1") as Label).Text);
                ////nv.Add("@LoginID", Session["LoginID"].ToString());
                ////nv.Add("@ChId", Chid);
                ////nv.Add("@Comments", txtcomments.Text.Trim());
                ////nv.Add("@SeedDate", (row.FindControl("lblSeededDate1") as Label).Text);
                ////nv.Add("@PlantDate", txtPlantDate.Text);
                ////nv.Add("@Comments", txtcomments.Text);
                ////nv.Add("@Role", Session["Role"].ToString());
                ////nv.Add("@Jid", (row.FindControl("lblJid") as Label).Text);

                ////result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyRequestManuaCreateTask", nv);
                ///

                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", Assign);

                nv.Add("@Jobcode", (row.FindControl("lblID1") as Label).Text);
                nv.Add("@Customer", (row.FindControl("lblCustomer1") as Label).Text);
                nv.Add("@Item", (row.FindControl("lblitem1") as Label).Text);
                nv.Add("@Facility", Session["Facility"].ToString());
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse1") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray1") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize1") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc1") as Label).Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@ChId", Chid);
                nv.Add("@Comments", txtcomments.Text.Trim());
                nv.Add("@QuantityOfTray", txtQuantityofTray.Text.Trim());
                nv.Add("@wo", "0");
                nv.Add("@DumpDate", txtDumpDate.Text);
                nv.Add("@RoleId", Session["Role"].ToString());
                nv.Add("@Jid", (row.FindControl("lblJid") as Label).Text);
                result = objCommon.GetDataExecuteScaler("SP_AddDumpRequestManuaCreateTask", nv);

            }

            long result1 = 0;
            NameValueCollection nv111 = new NameValueCollection();
            nv111.Add("@Chid", Chid);
            result1 = objCommon.GetDataInsertORUpdate("SP_UpdateCropHealthReport", nv111);
            objGeneral.SendMessage(int.Parse(Assign), "New Plant Ready Task Assigned", "New Plant Ready Task Assigned", "Plant Ready");

            //string message = "Assignment Successful";
            //string url = "MyTaskGrower.aspx";
            //string script = "window.onload = function(){ alert('";
            //script += message;
            //script += "');";
            //script += "window.location = '";
            //script += url;
            //script += "'; }";
            //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);

            updateNotification();
        }

        protected void btnDumpSumbit_Click(object sender, EventArgs e)
        {
            SubmitDump(ddlDumptAssignment.SelectedValue);
        }

        protected void btnSaveDump_Click(object sender, EventArgs e)
        {
            SubmitDump(Session["LoginID"].ToString());
        }

        public void updateNotification()
        {
            var r1 = (Master.FindControl("r1") as Repeater);
            var lblCount = (Master.FindControl("lblNotificationCount") as Label);
            objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), r1, lblCount);
        }
    }
}
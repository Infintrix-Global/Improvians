using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Bal;

namespace Evo
{
    public partial class GetGrowerPutAwayForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        BAL_CommonMasters objCOm = new BAL_CommonMasters();
        Bal_SeedingPlan objSP = new Bal_SeedingPlan();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Fdate = "", TDate = "";


                Fdate = Convert.ToDateTime(System.DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd");
                TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(14).ToString("yyyy-MM-dd");

                txtFromDate.Text = Fdate;
                txtToDate.Text = TDate;


                BindJobCode("0", "0", "0");
                Bindcname("0", "0", "0");
                BindCrop();

                BindGridGerm("0", 0);
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

        private string PutAwayFacility
        {
            get
            {
                if (ViewState["PutAwayFacility"] != null)
                {
                    return (string)ViewState["PutAwayFacility"];
                }
                return "";
            }
            set
            {
                ViewState["PutAwayFacility"] = value;
            }
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

        private string TraySize
        {
            get
            {
                if (ViewState["TraySize"] != null)
                {
                    return (string)ViewState["TraySize"];
                }
                return "";
            }
            set
            {
                ViewState["TraySize"] = value;
            }
        }


        public void BindCrop()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", "0");
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@JobNo", ddlJobNo.SelectedValue);
            nv.Add("@GenusCode", "0");
            nv.Add("@Mode", "6");
            nv.Add("@Type", "Put");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            ddlCrop.DataSource = dt;
            ddlCrop.DataTextField = "GenusCode";
            ddlCrop.DataValueField = "GenusCode";
            ddlCrop.DataBind();
            ddlCrop.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void Bindcname(string ddlBench, string jobNo, string Core)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", !string.IsNullOrEmpty(ddlBench) ? ddlBench : "0");
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@JobNo", !string.IsNullOrEmpty(jobNo) ? jobNo : "0");
            nv.Add("@GenusCode", ddlCrop.SelectedValue);
            nv.Add("@Mode", "3");
            nv.Add("@Type", "Put");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
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
            nv.Add("@BenchLocation", !string.IsNullOrEmpty(ddlBench) ? ddlBench : "0");
            nv.Add("@Customer", !string.IsNullOrEmpty(Customer) ? Customer : "0");
            nv.Add("@JobNo", ddlJobNo.SelectedValue);
            nv.Add("@GenusCode", ddlCrop.SelectedValue);

            nv.Add("@Mode", "2");
            nv.Add("@Type", "Put");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            //   ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataBind();

            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        public void BindJobCode(string ddlBench)
        {
            //  ddlJobNo.Items[0].Selected = false;
            ddlJobNo.ClearSelection();
            ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();

            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindJobCode("0", ddlCustomer.SelectedValue, "0");

            Bindcname("0", ddlJobNo.SelectedValue, "0");
            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindJobCode("0", ddlCustomer.SelectedValue, "0");

            BindCrop();
            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bindcname("0", ddlJobNo.SelectedValue, "0");

            BindCrop();

            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }

        public void BindSupervisorList()
        {
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

        public void BindGridGerm(string JobCode, int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Mode", "1");
            nv.Add("@wo", "");
            nv.Add("@Facility", Session["Facility"].ToString());

            nv.Add("@JobCode", JobCode);
            nv.Add("@CustomerName", !string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? ddlCustomer.SelectedValue : "0");

            nv.Add("@Crop", ddlCrop.SelectedValue);
            nv.Add("@FromDate", txtFromDate.Text);
            nv.Add("@ToDate", txtToDate.Text);

            dt = objCommon.GetDataTable("SP_GetGrowerPutAway", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }
        }

        private void highlight(int limit)
        {
            var i = gvGerm.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvGerm.Rows)
            {
                var checkJob = (row.FindControl("lbljobcode") as Label).Text;
                // var checklocation = (row.FindControl("lblBenchLocation") as Label).Text;
                //var tKey = gvGerm.DataKeys[row.RowIndex].Values[0].ToString();
                i--;
                if (checkJob == JobCode/* && checklocation == benchLoc*/)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvGerm.PageIndex++;
                    gvGerm.DataBind();
                    highlight((limit - 10));
                }
            }
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm("0", 1);
        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Assign")
            {
                PanelAdd.Visible = true;
                PanelList.Visible = false;
                string wo_No = e.CommandArgument.ToString();
                wo = wo_No;
                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@Mode", "2");
                nv.Add("@wo", wo_No);
                nv.Add("@JobCode", "0");
                nv.Add("@CustomerName", "0");
                nv.Add("@Facility", Session["Facility"].ToString());
                nv.Add("@Crop", "0");
                nv.Add("@FromDate", "");
                nv.Add("@ToDate", "");


                dt = objCommon.GetDataTable("SP_GetGrowerPutAway", nv);

                if (dt != null && dt.Rows.Count > 0)
                {
                    lblJobID.Text = dt.Rows[0]["JobID"].ToString();
                    lblSeedDate.Text = Convert.ToDateTime(dt.Rows[0]["SeededDate"]).ToString("MM-dd-yyyy");
                    lblSeededTrays.Text = dt.Rows[0]["ActualTraySeeded"].ToString();
                    PutAwayFacility = dt.Rows[0]["loc_seedline"].ToString();
                    lblGenusCode.Text = dt.Rows[0]["GenusCode"].ToString();
                    TraySize = dt.Rows[0]["TraySize"].ToString();
                }
                AddGrowerPutRow(true);
            }
        }

        protected void ButtonAddGridInvoice_Click(object sender, EventArgs e)
        {
            //  AddNewRowToGridSetInitialInvoice();
            AddGrowerPutRow(true);
        }

        protected void GridSplitJob_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlMain = (DropDownList)e.Row.FindControl("ddlMain");
                DropDownList ddlLocation = (DropDownList)e.Row.FindControl("ddlLocation");
                Label lblMain = (Label)e.Row.FindControl("lblMain");
                Label lblLocation = (Label)e.Row.FindControl("lblLocation");

                ddlMain.DataSource = objCOm.GetMainLocation();
                ddlMain.DataTextField = "l1";
                ddlMain.DataValueField = "l1";
                ddlMain.DataBind();
                ddlMain.Items.Insert(0, new ListItem("--- Select ---", "0"));

                BindLocationNew(ref ddlLocation, PutAwayFacility);
                ddlMain.SelectedValue = PutAwayFacility;
                ddlLocation.SelectedValue = lblLocation.Text;
            }
        }

        protected void ddlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlMain = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlMain.NamingContainer;
            if (row != null)
            {
                DropDownList ddlLocation = (DropDownList)row.FindControl("ddlLocation");

                ddlLocation.DataSource = objCOm.GetLocation(ddlMain.SelectedValue);
                ddlLocation.DataTextField = "p2";
                ddlLocation.DataValueField = "p2";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
        }

        public void BindLocation()
        {
            foreach (GridViewRow row in GridSplitJob.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlLocation = (row.Cells[0].FindControl("ddlLocation") as DropDownList);
                    DropDownList ddlMain = (row.Cells[0].FindControl("ddlMain") as DropDownList);

                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@FacilityID", ddlMain.SelectedValue);
                    ddlLocation.DataSource = objCommon.GetDataTable("SP_GetGreenhouseByFacility", nv); ;
                    ddlLocation.DataTextField = "GreenHouseName";
                    ddlLocation.DataValueField = "GreenHouseID";
                    ddlLocation.DataBind();
                    ddlLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
                }
            }
        }

        public void BindLocationNew(ref DropDownList ddlLocation, string ddlMain)
        {
            ddlLocation.DataSource = objCOm.GetLocation(ddlMain);
            ddlLocation.DataTextField = "p2";
            ddlLocation.DataValueField = "p2";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        protected void txtTrays_TextChanged(object sender, EventArgs e)
        {
            CalData();
        }

        public void CalData()
        {
            int Total = 0;
            foreach (GridViewRow row in GridSplitJob.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtTrays = (row.Cells[0].FindControl("txtTrays") as TextBox);
                    if (txtTrays.Text != "")
                    {
                        Total += Convert.ToInt32(txtTrays.Text);
                    }
                }
            }

            lblRemaining.Text = (Convert.ToInt32(lblSeededTrays.Text) - Total).ToString();
        }

        public void Clear()
        {
            BindGridGerm("0", 1);
            PanelAdd.Visible = false;
            PanelList.Visible = true;
            GridSplitJob.DataSource = null;
            GridSplitJob.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                long _isInserted = 1;
                int SelectedItems = 0;

                string IrrigateSeedDate = "";
                string FertilizeSeedDate = "";
                string ChemicalSeedDate = "";
                // IrrigateSeedDate 
                // GetSeedDataCheck
                DataTable dtISD = objSP.GetSeedDateData("IRRIGATE", lblGenusCode.Text, TraySize);
                DataTable dtFez = objSP.GetSeedDateData("FERTILIZE", lblGenusCode.Text, TraySize);
                DataTable dtChemical = objSP.GetSeedDateData("SPRAYING", lblGenusCode.Text, TraySize);

                foreach (GridViewRow item in GridSplitJob.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtTrays = (item.Cells[0].FindControl("txtTrays") as TextBox);
                        DropDownList ddlMain = (item.Cells[0].FindControl("ddlMain") as DropDownList);
                        DropDownList ddlLocation = (item.Cells[0].FindControl("ddlLocation") as DropDownList);
                        //-------------
                        string FertilizationDate = string.Empty;
                        string ChemicalDate = string.Empty;
                        string IrrigateDate = string.Empty;
                        string IrrigateNoCount = string.Empty;
                        string FertilizeNoCount = string.Empty;
                        string ChemicalNoCount = string.Empty;
                        NameValueCollection nvChDate = new NameValueCollection();

                        nvChDate.Add("@GreenHouseID", ddlLocation.SelectedValue);
                        DataTable ChFdt = objCommon.GetDataTable("SP_GetFertilizationCheckResetSprayTask", nvChDate);

                        if (dtFez != null && dtFez.Rows.Count > 0)
                        {
                            DataColumn col = dtFez.Columns["DateShift"];

                            int Fcount = 0;
                            foreach (DataRow row in dtFez.Rows)
                            {
                                Fcount++;
                                string AD = row[col].ToString().Replace("\u0002", "");

                                FertilizationDate = (Convert.ToDateTime(lblSeedDate.Text).AddDays(Convert.ToInt32(AD))).ToString();

                                string TodatDate;
                                string ReSetSprayDate = "";

                                TodatDate = System.DateTime.Now.ToShortDateString();


                                if (ChFdt != null && ChFdt.Rows.Count > 0)
                                {
                                    ReSetSprayDate = Convert.ToDateTime(ChFdt.Rows[0]["CreateDate"]).AddDays(Convert.ToInt32(ChFdt.Rows[0]["ResetSprayTaskForDays"])).ToString();
                                }

                                if (DateTime.Parse(FertilizationDate) >= DateTime.Parse(TodatDate))
                                {

                                    if (ReSetSprayDate == "" || DateTime.Parse(FertilizationDate) >= DateTime.Parse(ReSetSprayDate))
                                    {
                                        FertilizationDate = FertilizationDate;
                                        FertilizeNoCount = Fcount.ToString();
                                        break;
                                    }
                                    else
                                    {
                                        FertilizationDate = ReSetSprayDate;
                                    }
                                }
                            }
                        }
                        else
                        {
                            FertilizationDate = lblSeedDate.Text;
                        }
                        ///-----

                        //------ Chemical

                        NameValueCollection nvChemChDate = new NameValueCollection();

                        nvChemChDate.Add("@GreenHouseID", ddlLocation.SelectedValue);
                        DataTable ChChemidt = objCommon.GetDataTable("SP_GetChemicalCheckResetSprayTas", nvChemChDate);


                        if (dtChemical != null && dtChemical.Rows.Count > 0)
                        {
                            DataColumn col = dtChemical.Columns["DateShift"];
                            int Ccount = 0;
                            foreach (DataRow row in dtChemical.Rows)
                            {
                                Ccount++;
                                string FDay = row[col].ToString().Replace("\u0002", "");
                                ChemicalDate = (Convert.ToDateTime(lblSeedDate.Text).AddDays(Convert.ToInt32(FDay))).ToString();
                                string TodatDate;
                                string ReSetChemicalDate = "";
                                TodatDate = System.DateTime.Now.ToShortDateString();

                                if (ChChemidt != null && ChChemidt.Rows.Count > 0)
                                {
                                    ReSetChemicalDate = Convert.ToDateTime(ChChemidt.Rows[0]["CreateDate"]).AddDays(Convert.ToInt32(ChChemidt.Rows[0]["ResetSprayTaskForDays"])).ToString();
                                }
                                if (DateTime.Parse(ChemicalDate) >= DateTime.Parse(TodatDate))
                                {

                                    if (ReSetChemicalDate == "" || DateTime.Parse(ChemicalDate) >= DateTime.Parse(ReSetChemicalDate))
                                    {
                                        ChemicalDate = ChemicalDate;
                                        ChemicalNoCount = Ccount.ToString();
                                        break;
                                    }
                                    else
                                    {
                                        ChemicalDate = ReSetChemicalDate;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ChemicalDate = lblSeedDate.Text;
                        }

                        //---

                        //---
                        NameValueCollection nvIRRChDate = new NameValueCollection();

                        nvIRRChDate.Add("@GreenHouseID", ddlLocation.SelectedValue);
                        DataTable Irrigationdt = objCommon.GetDataTable("SP_GetIrrigationResetSprayTask", nvIRRChDate);

                        if (dtISD != null && dtISD.Rows.Count > 0)
                        {
                            DataColumn col = dtISD.Columns["DateShift"];
                            int Irrcount = 0;
                            foreach (DataRow row in dtISD.Rows)
                            {
                                Irrcount++;
                                string IDay = row[col].ToString().Replace("\u0002", "");
                                IrrigateDate = (Convert.ToDateTime(lblSeedDate.Text).AddDays(Convert.ToInt32(IDay))).ToString();

                                string TodatDate1;
                                string ReSetIrrigateDate = "";

                                TodatDate1 = System.DateTime.Now.ToShortDateString();
                                if (Irrigationdt != null && Irrigationdt.Rows.Count > 0)
                                {
                                    ReSetIrrigateDate = Convert.ToDateTime(Irrigationdt.Rows[0]["CreatedOn"]).AddDays(Convert.ToInt32(Irrigationdt.Rows[0]["ResetSprayTaskForDays"])).ToString();
                                }

                                if (DateTime.Parse(IrrigateDate) >= DateTime.Parse(TodatDate1))
                                {
                                    if (ReSetIrrigateDate == "" || DateTime.Parse(IrrigateDate) >= DateTime.Parse(ReSetIrrigateDate))
                                    {
                                        IrrigateDate = IrrigateDate;
                                        IrrigateNoCount = Irrcount.ToString();
                                        break;
                                    }
                                    else
                                    {
                                        IrrigateDate = ReSetIrrigateDate;
                                    }
                                }
                            }
                        }

                        else
                        {
                            IrrigateDate = lblSeedDate.Text;
                        }
                        long result = 0;
                        NameValueCollection nv = new NameValueCollection();
                        nv.Add("@GrowerPutAwayId", "");
                        nv.Add("@wo", wo);

                        nv.Add("@jobcode", lblJobID.Text);
                        nv.Add("@FacilityID", ddlMain.SelectedValue);
                        nv.Add("@GreenHouseID", ddlLocation.SelectedValue);
                        nv.Add("@Trays", txtTrays.Text);

                        nv.Add("@SeedDate", lblSeedDate.Text);
                        nv.Add("@CreateBy", Session["LoginID"].ToString());
                        nv.Add("@Supervisor", ddlSupervisor.SelectedValue);
                        nv.Add("@IrrigateSeedDate", IrrigateDate);
                        nv.Add("@FertilizeSeedDate", FertilizationDate);
                        nv.Add("@ChemicalSeedDate", ChemicalDate);
                        nv.Add("@IrrigateNoCount", IrrigateNoCount);
                        nv.Add("@FertilizeNoCount", FertilizeNoCount);
                        nv.Add("@ChemicalNoCount", ChemicalNoCount);
                        nv.Add("@RolId", ddlSupervisor.SelectedValue);

                        if (txtTrays.Text != "")
                        {
                            nv.Add("@mode", "1");
                            _isInserted = objCommon.GetDataExecuteScalerRetObj("SP_AddGrowerPutAwayDetails", nv);

                            NameValueCollection nameValue = new NameValueCollection();
                            nameValue.Add("@LoginID", Session["LoginID"].ToString());
                            nameValue.Add("@jobcode", lblJobID.Text);
                            nameValue.Add("@GreenHouseID", ddlLocation.SelectedValue);
                            nameValue.Add("@TaskName", "PutAway");

                            var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);
                        }
                        SelectedItems++;
                    }

                    var res = (Master.FindControl("r1") as Repeater);
                    var lblCount = (Master.FindControl("lblNotificationCount") as Label);
                    objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), res, lblCount);

                    NameValueCollection nv1 = new NameValueCollection();
                    nv1.Add("@WorkOrder", wo);
                    _isInserted = objCommon.GetDataInsertORUpdate("SP_UpdateGrowerPutAwayDetails", nv1);

                    // string message = "Grower Put Away Save  Successful";
                    string url = "MyTaskGrower.aspx";

                    string message = "Assignment Successful";
                    objCommon.ShowAlertAndRedirect(message, url);
                    //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Grower Put Away Save  Successful')", true);
                    Clear();
                }

            }
            catch (Exception ex)
            {
            }
        }

        private List<GrowerputDetils> GrowerPutData
        {
            get
            {
                if (ViewState["GrowerPutData"] != null)
                {
                    return (List<GrowerputDetils>)ViewState["GrowerPutData"];
                }
                return new List<GrowerputDetils>();
            }
            set
            {
                ViewState["GrowerPutData"] = value;
            }
        }

        private void AddGrowerPutRow(bool AddBlankRow)
        {
            try
            {
                string unit = "", ddlTAX1 = "", ddlStatusVal = "", hdnWOEmployeeIDVal = "";
                string MainId = "", LocationId = "";

                List<GrowerputDetils> objinvoice = new List<GrowerputDetils>();

                foreach (GridViewRow item in GridSplitJob.Rows)
                {
                    hdnWOEmployeeIDVal = ((HiddenField)item.FindControl("hdnWOEmployeeID")).Value;

                    MainId = ((DropDownList)item.FindControl("ddlMain")).SelectedValue;
                    LocationId = ((DropDownList)item.FindControl("ddlLocation")).SelectedValue;
                    TextBox txtTrays = (TextBox)item.FindControl("txtTrays");

                    AddGrowerput(ref objinvoice, Convert.ToInt32(hdnWOEmployeeIDVal), MainId, LocationId, txtTrays.Text);
                }
                if (AddBlankRow)
                    AddGrowerput(ref objinvoice, 1, "", "", "");

                GrowerPutData = objinvoice;
                GridSplitJob.DataSource = objinvoice;
                GridSplitJob.DataBind();
                ViewState["Data"] = objinvoice;
            }
            catch (Exception ex)
            {

            }
        }

        public void GridSplitjob()
        {
            GridSplitJob.DataSource = ViewState["Data"];
            GridSplitJob.DataBind();
        }

        private void AddGrowerput(ref List<GrowerputDetils> objGP, int ID, string FacilityID, string GreenHouseID, string Trays)

        {
            GrowerputDetils objInv = new GrowerputDetils();
            objInv.ID = ID;
            objInv.RowNumber = objGP.Count + 1;
            objInv.FacilityID = FacilityID;

            objInv.GreenHouseID = GreenHouseID;
            objInv.Trays = Trays;

            objGP.Add(objInv);
            ViewState["ojbpro"] = objGP;
        }

        protected void GridSplitJob_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<GrowerputDetils> objinvoice = ViewState["ojbpro"] as List<GrowerputDetils>;
            objinvoice.RemoveAt(e.RowIndex);
            GridSplitJob.DataSource = objinvoice;
            GridSplitJob.DataBind();
        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGermDate = (Label)e.Row.FindControl("lblSeededDate");
                string dtimeString = Convert.ToDateTime(lblGermDate.Text).ToString("yyyy/MM/dd");

                DateTime dtime = Convert.ToDateTime(dtimeString);

                DateTime nowtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));

                if (nowtime > dtime)
                {
                    e.Row.CssClass = "overdue";
                }
            }
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {

            Bindcname("0", "0", "0");
            // BindJobCode("0");
            BindJobCode("0", "0", "0");
            BindCrop();
            BindGridGerm("0", 1);

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }

        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            BindGridGerm(txtSearchJobNo.Text, 1);
        }

        protected void txtBatchLocation_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            //BindJobCode(ddlBenchLocation.SelectedValue);
            Bindcname("0", "0", "0");
            BindJobCode("0", "0", "0");
            BindGridGerm(ddlJobNo.SelectedValue, 1);
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
                    cmd.CommandText = " Select * from gti_jobs_seeds_plan GJSP join  SeedLineTaskCompletion SLTC on SLTC.wo =GJSP.wo  where GJSP.loc_seedline ='" + Facility + "'  AND GJSP.jobcode like '%" + prefixText + "%' " +
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


[Serializable]
public class GrowerputDetils
{
    public int ID { get; set; }
    public int RowNumber { get; set; }
    public string FacilityID { get; set; }
    public string GreenHouseID { get; set; }
    public string Trays { get; set; }
}
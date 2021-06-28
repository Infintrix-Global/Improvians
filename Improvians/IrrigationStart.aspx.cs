using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Bal;
using System.Text.RegularExpressions;
using Evo.BAL_Classes;
namespace Evo
{
    public partial class IrrigationStart1 : System.Web.UI.Page
    {
        BAL_Task objTask = new BAL_Task();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        Bal_SeedingPlan objSP = new Bal_SeedingPlan();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Bench"] != null)
                {
                    Bench = Request.QueryString["Bench"].ToString();
                }
                if (Request.QueryString["jobCode"] != null)
                {
                    JobCode = Request.QueryString["jobCode"].ToString();
                }
                if (Request.QueryString["ICode"] != null)
                {
                    ICode = Request.QueryString["ICode"].ToString();
                }
                if (Request.QueryString["AssignedBy"] != null)
                {
                    AssignedBy = Request.QueryString["AssignedBy"].ToString();
                }

                BenchUp = "'" + Bench + "'";
                txtSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                BindGridIrrigation();

                BindGridIrrDetails("'" + Bench + "'");
                BindGridIrrDetailsViewReq();
                lblbench.Text = Bench;

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
        private string ICode
        {
            get
            {
                if (ViewState["ICode"] != null)
                {
                    return (string)ViewState["ICode"];
                }
                return "";
            }
            set
            {
                ViewState["ICode"] = value;
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

        protected void RadioBench_SelectedIndexChanged(object sender, EventArgs e)
        {
            // SelectBenchLocation();
            string chkSelected = "";
            if (RadioBench.SelectedValue == "1")
            {
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
                BenchUp = chkSelected;
                DataTable dt123 = new DataTable();
                gvJobHistory.DataSource = dt123;
                gvJobHistory.DataBind();
                BindGridIrrDetails(chkSelected);

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
                BenchUp = chkSelected;
                DataTable dt123 = new DataTable();
                gvJobHistory.DataSource = dt123;
                gvJobHistory.DataBind();
                BindGridIrrDetails(chkSelected);

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
            BenchUp = chkSelected;
            BindGridIrrDetails(chkSelected);
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
                string[] words = Regex.Split(Bench, @"\W+");

                DataTable dt = objFer.GetSelectBenchLocation(words[0], words[1]);

                if (dt.Rows.Count > 0)
                {
                    DataColumn col = dt.Columns["PositionCode"];
                    foreach (DataRow row in dt.Rows)
                    {
                        //strJsonData = row[col].ToString()
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
            BenchUp = chkSelected;
            BindGridIrrDetails(chkSelected);
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

        public void BindGridIrrigation()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@JobCode", JobCode);
            nv.Add("@BenchLocation", Bench);
            dt = objCommon.GetDataTable("SP_GetIrrigationRequesStart", nv);

            GridIrrigation.DataSource = dt;
            GridIrrigation.DataBind();
            int tray = 0;

            Jid = dt.Rows[0]["GrowerPutAwayId"].ToString();
            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                tray = tray + Convert.ToInt32((row.FindControl("lbltotTray") as Label).Text);
            }
        }

        public void BindGridIrrDetailsViewReq()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@ICode", ICode);
            dt = objCommon.GetDataTable("SP_GetIrrigationTaskAssignmentView", nv);

            if (dt != null && dt.Rows.Count > 0)
            {
                txtNotes.Text = dt.Rows[0]["Nots"].ToString();
                txtResetSprayTaskForDays.Text = dt.Rows[0]["ResetSprayTaskForDays"].ToString();
                txtSprayDate.Text = Convert.ToDateTime(dt.Rows[0]["SprayDate"]).ToString("yyyy-MM-dd");
                txtWaterRequired.Text = dt.Rows[0]["WaterRequired"].ToString();
            }
        }

        public void BindGridIrrDetails(string BenchLoc)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", BenchLoc);
            dt = objCommon.GetDataTable("SP_GetIrrigationRequestSelect", nv);

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


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int IrrigationCode = 0;

            foreach (GridViewRow row1 in GridIrrigation.Rows)
            {
                string IrrigationCode1 = (row1.FindControl("lblIrrigationCode") as Label).Text;
                if (IrrigationCode1 != "")
                {
                    IrrigationCode = Convert.ToInt32(IrrigationCode1);
                    if (IrrigationCode == 0)
                    {
                        DataTable dt = new DataTable();
                        NameValueCollection nv1 = new NameValueCollection();
                        nv1.Add("@Mode", "13");
                        dt = objCommon.GetDataTable("GET_Common", nv1);
                        IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);
                    }
                    else
                    {
                        IrrigationCode = IrrigationCode;
                    }
                }
                else
                {
                    DataTable dt = new DataTable();
                    NameValueCollection nv1 = new NameValueCollection();
                    nv1.Add("@Mode", "13");
                    dt = objCommon.GetDataTable("GET_Common", nv1);
                    IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);
                }
            }

            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                long result = 0;
                long Mresult = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", Session["LoginID"].ToString());

                nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                nv.Add("@IrrigatedNoTrays", (row.FindControl("lbltotTray") as Label).Text);

                nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                nv.Add("@IrrigationDuration", "");
                nv.Add("@SprayDate", txtSprayDate.Text.Trim());
                nv.Add("@SprayTime", "");
                nv.Add("@Nots", txtNotes.Text.Trim());
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                nv.Add("@IrrigationCode", IrrigationCode.ToString());
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@NoOfPasses", "");
                nv.Add("@ResetSprayTaskForDays", txtResetSprayTaskForDays.Text);

                nv.Add("@jid", Jid);

                result = objCommon.GetDataInsertORUpdate("SP_AddIrrigationRequestStart", nv);

                if (result > 0)
                {

                }
                else
                {

                }
            }

            foreach (GridViewRow row in gvJobHistory.Rows)
            {


                long result = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", Session["LoginID"].ToString());

                nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                nv.Add("@IrrigatedNoTrays", (row.FindControl("lbltotTray") as Label).Text);

                nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                nv.Add("@IrrigationDuration", "");

                nv.Add("@SprayDate", txtSprayDate.Text.Trim());
                nv.Add("@SprayTime", "");
                nv.Add("@Nots", txtNotes.Text.Trim());
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                nv.Add("@IrrigationCode", IrrigationCode.ToString());
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@NoOfPasses", "");
                nv.Add("@jid", (row.FindControl("lbljid") as Label).Text);
                nv.Add("@ResetSprayTaskForDays", txtResetSprayTaskForDays.Text);

                result = objCommon.GetDataInsertORUpdate("SP_AddIrrigationRequestStart", nv);

            }

            GridViewRow rowG = GridIrrigation.Rows[0];
            var txtJobNo = (rowG.FindControl("lbljobID") as Label).Text;
            var txtBenchLocation = (rowG.FindControl("lblGreenHouse") as Label).Text;

            NameValueCollection nameValue = new NameValueCollection();
            nameValue.Add("@LoginID", Session["LoginID"].ToString());
            nameValue.Add("@jobcode", txtJobNo);
            nameValue.Add("@GreenHouseID", txtBenchLocation);
            nameValue.Add("@TaskName", "Irrigation");

            var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);
            //long Mresult1 = 0;
            //NameValueCollection nv123 = new NameValueCollection();
            //nv123.Add("@BanchLocation", lblbench.Text);
            //Mresult1 = objCommon.GetDataInsertORUpdate("SP_AddIrrigationRequestMenualUpdate", nv123);

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
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            clear();
        }


        public void AddJobNextDate()
        {
            long _isInserted = 1;
            long _isIGCodeInserted = 1;
            long _isFCdeInserted = 1;
            int SelectedItems = 0;

            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                DataTable dtISD = objSP.GetSeedDateDatanew("IRRIGATE", (row.FindControl("lblGenusCode") as Label).Text, (row.FindControl("lblTraySize") as Label).Text);

                int IrrigateCode = 0;
                DataTable dtIG = new DataTable();
                NameValueCollection nv1IG = new NameValueCollection();
                nv1IG.Add("@Mode", "13");
                dtIG = objCommon.GetDataTable("GET_Common", nv1IG);
                IrrigateCode = Convert.ToInt32(dtIG.Rows[0]["ICode"]);

                NameValueCollection nvIRRChDate = new NameValueCollection();

                nvIRRChDate.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                DataTable Irrigationdt = objCommon.GetDataTable("SP_GetIrrigationResetSprayTask", nvIRRChDate);

                if (dtISD != null && dtISD.Rows.Count > 0)
                {
                    int Irrcount = 0;
                    DataColumn col = dtISD.Columns["DateShift"];
                    foreach (DataRow row1 in dtISD.Rows)
                    {
                        string IrrigateDate = string.Empty;
                        //string IDay = row1[col].ToString().Replace("\u0002", "");
                        string IDay = row1[col].ToString();
                        string seeddate = (row.FindControl("lblSeededDate1") as Label).Text;
                        IrrigateDate = (Convert.ToDateTime(seeddate).AddDays(Convert.ToInt32(IDay))).ToString();
                        Irrcount++;
                        string DateCountNo = "0";
                        string TodatDate1;
                        string ReSetIrrigateDate = "";

                        DateCountNo = Irrcount.ToString();
                        TodatDate1 = System.DateTime.Now.ToShortDateString();

                        if (Irrigationdt != null && Irrigationdt.Rows.Count > 0 && Irrigationdt.Rows[0]["ResetSprayTaskForDays"] != System.DBNull.Value)
                        {
                            if (Irrigationdt.Rows[0]["ResetSprayTaskForDays"].ToString() != "")
                            {
                                ReSetIrrigateDate = Convert.ToDateTime(Irrigationdt.Rows[0]["CreatedOn"]).AddDays(Convert.ToInt32(Irrigationdt.Rows[0]["ResetSprayTaskForDays"])).ToString();
                            }
                        }

                        if (DateTime.Parse(IrrigateDate) >= DateTime.Parse(TodatDate1))
                        {

                            if (ReSetIrrigateDate == "" || DateTime.Parse(IrrigateDate) >= DateTime.Parse(ReSetIrrigateDate))
                            {

                                string WONo = (row.FindControl("lblwo") as Label).Text;
                                string jid = "";

                                IrrigateDate = IrrigateDate;

                                NameValueCollection nv11 = new NameValueCollection();

                                nv11.Add("@GrowerPutAwayIrrigatId", (row.FindControl("lblGrowerputawayID") as Label).Text);
                                nv11.Add("@wo", (row.FindControl("lblwo") as Label).Text);
                              //  nv11.Add("@Jid", (row.FindControl("lblJidF") as Label).Text);
                                nv11.Add("@jobcode", (row.FindControl("lbljobID") as Label).Text);
                                nv11.Add("@FacilityID", (row.FindControl("lblFacility") as Label).Text);
                                nv11.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                                nv11.Add("@Trays", (row.FindControl("lbltotTray") as Label).Text);

                                nv11.Add("@SeedDate", seeddate);
                                nv11.Add("@CreateBy", Session["LoginID"].ToString());
                                nv11.Add("@Supervisor", "0");
                                nv11.Add("@IrrigateSeedDate", IrrigateDate);
                                nv11.Add("@FertilizeSeedDate", "");
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
                                _isIGCodeInserted = objCommon.GetDataExecuteScaler("SP_AddGrowerPutAwayDetailsIrrigationMenual", nv11);
                                break;
                            }
                        }
                    }
                }


            }


            foreach (GridViewRow row in gvJobHistory.Rows)
            {

                DataTable dtISD = objSP.GetSeedDateDatanew("IRRIGATE", (row.FindControl("lblGenusCode") as Label).Text, (row.FindControl("lblTraySize") as Label).Text);

                int IrrigateCode = 0;
                DataTable dtIG = new DataTable();
                NameValueCollection nv1IG = new NameValueCollection();
                nv1IG.Add("@Mode", "13");
                dtIG = objCommon.GetDataTable("GET_Common", nv1IG);
                IrrigateCode = Convert.ToInt32(dtIG.Rows[0]["ICode"]);

                NameValueCollection nvIRRChDate = new NameValueCollection();

                nvIRRChDate.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                DataTable Irrigationdt = objCommon.GetDataTable("SP_GetIrrigationResetSprayTask", nvIRRChDate);

                if (dtISD != null && dtISD.Rows.Count > 0)
                {
                    int Irrcount = 0;
                    DataColumn col = dtISD.Columns["DateShift"];
                    foreach (DataRow row1 in dtISD.Rows)
                    {
                        string IrrigateDate = string.Empty;
                        //string IDay = row1[col].ToString().Replace("\u0002", "");
                        string IDay = row1[col].ToString();
                        string seeddate = (row.FindControl("lblSeededDate1") as Label).Text;
                        IrrigateDate = (Convert.ToDateTime(seeddate).AddDays(Convert.ToInt32(IDay))).ToString();
                        Irrcount++;
                        string DateCountNo = "0";
                        string TodatDate1;
                        string ReSetIrrigateDate = "";

                        DateCountNo = Irrcount.ToString();
                        TodatDate1 = System.DateTime.Now.ToShortDateString();

                        if (Irrigationdt != null && Irrigationdt.Rows.Count > 0 && Irrigationdt.Rows[0]["ResetSprayTaskForDays"] != System.DBNull.Value)
                        {
                            if (Irrigationdt.Rows[0]["ResetSprayTaskForDays"].ToString() != "")
                            {
                                ReSetIrrigateDate = Convert.ToDateTime(Irrigationdt.Rows[0]["CreatedOn"]).AddDays(Convert.ToInt32(Irrigationdt.Rows[0]["ResetSprayTaskForDays"])).ToString();
                            }
                        }

                        if (DateTime.Parse(IrrigateDate) >= DateTime.Parse(TodatDate1))
                        {

                            if (ReSetIrrigateDate == "" || DateTime.Parse(IrrigateDate) >= DateTime.Parse(ReSetIrrigateDate))
                            {
                                IrrigateDate = IrrigateDate;

                                NameValueCollection nv11 = new NameValueCollection();

                                nv11.Add("@GrowerPutAwayIrrigatId", "");
                                nv11.Add("@wo", "");
                                nv11.Add("@Jid", (row.FindControl("lbljid") as Label).Text);
                                nv11.Add("@jobcode", (row.FindControl("lbljobID") as Label).Text);
                                nv11.Add("@FacilityID", (row.FindControl("lblFacility") as Label).Text);
                                nv11.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                                nv11.Add("@Trays", (row.FindControl("lbltotTray") as Label).Text);

                                nv11.Add("@SeedDate", seeddate);
                                nv11.Add("@CreateBy", Session["LoginID"].ToString());
                                nv11.Add("@Supervisor", "0");
                                nv11.Add("@IrrigateSeedDate", IrrigateDate);
                                nv11.Add("@FertilizeSeedDate", "");
                                nv11.Add("@ID", "");
                                nv11.Add("@GenusCode", (row.FindControl("lblGenusCode") as Label).Text);
                                nv11.Add("@DateCountNo", DateCountNo);

                                _isIGCodeInserted = objCommon.GetDataExecuteScaler("SP_AddGrowerPutAwayDetailsIrrigationMenual", nv11);
                                break;
                            }
                        }
                    }
                }

            }
        }





        public void clear()
        {
            txtWaterRequired.Text = "";
            txtNotes.Text = "";
            //  txtIrrigatedNoTrays.Text = "";
            //txtIrrigationDuration.Text = "";
            txtSprayDate.Text = "";
            // txtSprayTime.Text = "";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            RadioBench.Items[0].Selected = false;
            ListBoxBenchesInHouse.Items.Clear();
            //To unselect all Items
            RadioBench.ClearSelection();
            BindGridIrrDetails("'" + Bench + "'");

            PanelBench.Visible = false;
            PanelBenchesInHouse.Visible = false;
        }
    }
}
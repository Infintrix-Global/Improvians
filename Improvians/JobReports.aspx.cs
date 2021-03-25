
using Evo.Bal;
using Evo.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class JobReports : System.Web.UI.Page
    {
        public static string JobCode;
        CommonControlNavision objNav = new CommonControlNavision();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        clsCommonMasters objMaster = new clsCommonMasters();
        BAL_Task objTask = new BAL_Task();
        Bal_SeedingPlan objSP = new Bal_SeedingPlan();
        Evo_General objGeneral = new Evo_General();

        static string ReceiverEmail = "";


        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };

        public static DataTable dtCTrays = new DataTable()
        { Columns = { "Fertilizer", "Tray", "SQFT" } };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                JobCode = Request.QueryString["jobCode"];

                if (string.IsNullOrEmpty(JobCode))
                {
                    divFilter.Visible = true;
                    divFilter1.Visible = true;
                    BindBenchLocation(Session["Facility"].ToString());
                }
                else
                {
                    divJobNo.Visible = true;
                    lblJobNo.Text = JobCode;
                    PanelView.Visible = true;
                    BindGridOne();
                }

                BindSupervisor();

                BindFacility();
                BindSupervisorList();
                BindFertilizer();
                BindJobCode("");
                BindChemical();
                txtGerDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtFDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtChemicalSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtMoveDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtirrigationSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            }
        }


        public void BindGridOne()
        {
            FillDGHeader01();
            BindJobHistoryDropdown();
            BindGridJobHistory();
            string chkSelected = "";
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobID", JobCode);
            DataSet ds = objCommon.GetDataSet("GetJobTracibilityReport", nv);
            dt5 = ds.Tables[0];
            dt6 = ds.Tables[1];

            GV5.DataSource = dt5;

            DataTable dtProfile = new DataTable();
            dtProfile.Columns.Add("activitycode", typeof(String));
            dtProfile.Columns.Add("plan_date", typeof(DateTime));

            if (GV2.Rows.Count > 0)
            {
                string seeddt = (GV2.Rows[0].FindControl("lblSeededDate") as Label).Text;
                if (!string.IsNullOrEmpty(seeddt))
                {
                    DataRow dr = dtProfile.NewRow();
                    dr[0] = "SEEDING";
                    DateTime seeddate = DateTime.Parse(seeddt);
                    dr[1] = seeddate;
                    dtProfile.Rows.Add(dr);
                }
            }
            DataTable dthistory = objBAL.GetJobHistoryDateFromNavision(JobCode);
            if (dthistory != null && dthistory.Rows.Count > 0)
            {
                dtProfile.Merge(dthistory);
                dtProfile.AcceptChanges();
            }
            if (dt6.Rows.Count > 0 && dt6 != null)
            {
                dtProfile.Merge(dt6);
                dtProfile.AcceptChanges();
            }
            GV6.DataSource = dtProfile;
            GV5.DataBind();
            GV6.DataBind();

            int P = 0;
            string Q = "";
            if (dt5.Rows.Count > 0)
            {
                DataColumn col = dt5.Columns["GreenHouseID"];
                foreach (DataRow row in dt5.Rows)
                {
                    P = 1;
                    Q += "'" + row[col].ToString() + "',";
                }
            }

            if (P > 0)
            {
                chkSelected = Q.Remove(Q.Length - 1, 1);
                BindSQFTofBench(chkSelected);
            }
            else
            {

            }
            decimal tray = 0;
            foreach (GridViewRow row in GV5.Rows)
            {
                Label lblTray = row.FindControl("lblTrays") as Label;
                if (lblTray != null)
                    tray = tray + Convert.ToDecimal(lblTray.Text);
            }

            txtTGerTrays.Text = "10";
            txtFTrays.Text = tray.ToString();
            lblTotalTrays.Text = tray.ToString();
            txtChemicalTrays.Text = tray.ToString();
        }

        public void BindJobHistoryDropdown()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobID", JobCode);
            DataSet ds = objCommon.GetDataSet("GetJobTracibilityReportJobHistory", nv);
            DataTable dt = ds.Tables[0];
            ddlDescription.DataSource = SelectDistinct(dt, "Description");
            ddlDescription.DataBind();
            ddlDescription.Items.Insert(0, new ListItem("--- Select ---", ""));
            ddlBench.DataSource = SelectDistinct(dt, "GreenhouseID");
            ddlBench.DataBind();
            ddlBench.Items.Insert(0, new ListItem("--- Select ---", ""));
            ddlAssignedBy.DataSource = SelectDistinct(dt, "AssignedBy");
            ddlAssignedBy.DataBind();
            ddlAssignedBy.Items.Insert(0, new ListItem("--- Select ---", ""));
            ddlAssignedTo.DataSource = SelectDistinct(dt, "AssignedTo");
            ddlAssignedTo.DataBind();
            ddlAssignedTo.Items.Insert(0, new ListItem("--- Select ---", ""));
        }

        public void BindGridJobHistory()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobID", JobCode);
            DataSet ds = objCommon.GetDataSet("GetJobTracibilityReportJobHistory", nv);
            DataTable dt = ds.Tables[0];
          
            DataView dataView = dt.DefaultView;
            if (ddlDescription.SelectedIndex > 0)
            {
                dataView.RowFilter = " Description = '" + ddlDescription.SelectedValue + "'";
            }
            if (ddlBench.SelectedIndex > 0)
            {
                dataView.RowFilter = " GreenhouseID = '" + ddlBench.SelectedValue + "'";
            }
            if (ddlAssignedBy.SelectedIndex > 0)
            {
                dataView.RowFilter = " AssignedBy = '" + ddlAssignedBy.SelectedValue + "'";
            }
            if (ddlAssignedTo.SelectedIndex > 0)
            {
                dataView.RowFilter = " AssignedTo = '" + ddlAssignedTo.SelectedValue + "'";
            }
            GV4.DataSource = dataView;
            GV4.DataBind();
        }

        protected void ddlDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridJobHistory();            
        }

        public void FillDGHeader01()
        {

            string sql = "select j.No_ jobcode, j.[Shortcut Property 1 Value] germct, j.[Bill-to Name] cname, j.[Item No_] itemno, j.[Item Description] itemdescp," +
                            " sum(t.Quantity) trays, j.[Delivery Date] ready_date, m.[Production Phase] pphase, j.[Original Production Qty_] / j.[Variant Code] origtrays," +
                            " j.[Source No_] + '-' + convert(nvarchar, j.[Source Line No_] / 1000) solines, j.[Variant Code] ts, j.[Source No_] so,j.[Source Line No_] / 1000 soline, " +
                            " j.[Genus Code] crop, j.[Shortcut Property 10 Value] overage, j.[Delivery Date] duedate, j.[Original Start Date] plandate," +
                            " CASE WHEN m.[Closed at Date] < '2000-01-01' THEN m.[Posting Date] ELSE m.[Closed at Date] END seeddt ," +
                            " DATEDIFF(day, CASE WHEN m.[Closed at Date] < '2000-01-01' THEN m.[Posting Date] ELSE m.[Closed at Date] END,GETDATE()) as NoOfDay," +
                            " CASE WHEN j.[Shortcut Property 2 Value] = 'Yes' THEN 'Yes' ELSE 'NO' END org " +
                            " from[GTI$Job] j" +
                            " LEFT OUTER JOIN[GTI$IA Job Tracking Entry] t ON j.No_ = t.[Job No_] " +
                            " LEFT OUTER JOIN[GTI$IA Job Mutation Entry] m ON j.No_ = m.[Job No_] and m.[Production Phase] in ('SEEDING', 'RETURNS') " +
                            " where j.No_ = '" + JobCode + "'" +
                            " group by j.No_, j.[Shortcut Property 2 Value], j.[Shortcut Property 1 Value], j.[Bill-to Name], j.[Item No_], j.[Item Description], " +
                            " j.[Delivery Date], m.[Closed at Date], m.[Production Phase], m.[Posting Date], j.[Source No_], j.[Source Line No_], j.[Variant Code], j.[Genus Code], " +
                            " j.[Shortcut Property 10 Value], j.[Delivery Date], j.[Original Start Date], j.[Original Production Qty_]";
            DataTable ds = objGeneral.GetDatasetByCommand(sql);
            GV2.DataSource = ds;
            GV2.DataBind();

            DGHead02.DataSource = ds;
            DGHead02.DataBind();

            FillDGSeeds();
        }


        public void FillDGSeeds()
        {
            string sql2;
            sql2 = "select le.[Item No_] seed, le.[Lot No_] lot, (le.Quantity * -1) qty " + "from [GTI$Item Ledger Entry] le " + "where le.[Job No_] = '" + JobCode + "' and le.[Item Category Code] = 'SEED'";
            DGSeeds.DataSource = objGeneral.GetDatasetByCommand(sql2);
            DGSeeds.DataBind();
        }
        public void BindBenchLocation(string ddlMain)
        {
            ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataTextField = "p2";
            ddlBenchLocation.DataValueField = "p2";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", ""));

        }
        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearchJobNo.Text = "";
            BindJobCode(ddlBenchLocation.SelectedValue);
        }
        public void BindJobCode(string ddlBench)
        {
            ddlJobNo.Items.Clear();
            ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", ""));
        }
        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PanelView.Visible = true;
            JobCode = ddlJobNo.SelectedValue.Trim();
            lblJobNo.Text = JobCode;
            BindGridOne();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PanelView.Visible = true;
            JobCode = txtSearchJobNo.Text.Trim();
            lblJobNo.Text = JobCode;
            BindGridOne();
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            txtSearchJobNo.Text = "";
            JobCode = Request.QueryString["jobCode"];

            if (string.IsNullOrEmpty(JobCode))
            {
                divFilter.Visible = true;
                divFilter1.Visible = true;
                JobCode = "";
                BindBenchLocation(Session["Facility"].ToString());
            }
            else
            {
                divJobNo.Visible = true;
                lblJobNo.Text = JobCode;
                PanelView.Visible = true;
                BindGridOne();
            }

            BindSupervisor();

            BindFacility();
            BindSupervisorList();
            BindFertilizer();
            BindJobCode("");
            BindChemical();
            txtGerDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            txtFDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            txtChemicalSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            txtMoveDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            txtirrigationSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            BindGridOne();
            PanelView.Visible = false;
            //   txtSearchJobNo.Text = "JB";
            // BindGridOne();
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
                    cmd.CommandText = " select distinct jobcode from gti_jobs_seeds_plan where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "'  AND jobcode like '" + prefixText + "%' order by jobcode" +
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
        
        protected void GV5_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV5.EditIndex = e.NewEditIndex;
            Label lblTray = (Label)(GV5.Rows[GV5.EditIndex].FindControl("lblTrays"));
            Label lblLocation = (Label)(GV5.Rows[GV5.EditIndex].FindControl("lblGHD"));
            Session["trays"] = lblTray.Text;
            Session["location"] = lblLocation.Text;
            BindGridOne();
            DropDownList ddlPbx = (DropDownList)(GV5.Rows[GV5.EditIndex].FindControl("ddlBenchLocation"));
            ddlPbx.SelectedValue = lblLocation.Text;
            ddlPbx.Focus();
        }

        protected void GV5_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            Label lblid = GV5.Rows[e.RowIndex].FindControl("lblgrowerId") as Label;
            Label lbljid = GV5.Rows[e.RowIndex].FindControl("lbljid") as Label;

            TextBox city = GV5.Rows[e.RowIndex].FindControl("txt_Name") as TextBox;
            DropDownList ddlBenchLocation = GV5.Rows[e.RowIndex].FindControl("ddlBenchLocation") as DropDownList;
            long result1 = 0;
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@GrowerPutAwayID", lblid.Text);
            nv1.Add("@jid", lbljid.Text);
            nv1.Add("@GreenHouseID", ddlBenchLocation.SelectedValue);
            nv1.Add("@Trays", city.Text);
            nv1.Add("@JobId", JobCode);
            nv1.Add("@FromLocation", Session["location"].ToString());
            nv1.Add("@ToLocation", ddlBenchLocation.SelectedValue);
            nv1.Add("@OldTotalTrays", Session["trays"].ToString());
            nv1.Add("@NewTotalTrays", city.Text);
            nv1.Add("@UserId", Session["LoginID"].ToString());
            result1 = objCommon.GetDataInsertORUpdate("UpdateJobFacilityHouseDetail", nv1);
            GV5.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            BindGridOne();
        }

        protected void GV5_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV5.EditIndex = -1;
            BindGridOne();
        }

        protected void GV5_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DataSet ds = new DataSet();
                    DropDownList ddList = (DropDownList)e.Row.FindControl("ddlBenchLocation");

                    ddList.DataSource = objBAL.GetLocation(Session["Facility"].ToString()); ;
                    ddList.DataTextField = "p2";
                    ddList.DataValueField = "p2";
                    ddList.DataBind();

                    //DataRowView dr = e.Row.DataItem as DataRowView;
                    // ddList.SelectedValue = dr["department_id"].ToString();
                }
            }
        }

        //---------------------------------------------------------------------------------------------TASK Create ------------------

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

            ddlMethod.DataSource = objMaster.GetAllChemicalList();
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

        protected void btnFSubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row1 in GV5.Rows)
            {
                dtTrays.Clear();
                string Batchlocation = "";
                int FertilizationCode = 0;


                foreach (GridViewRow row in GV2.Rows)
                {
                    Batchlocation = (row1.FindControl("lblGHD") as Label).Text;

                    NameValueCollection nv5 = new NameValueCollection();
                    nv5.Add("@Mode", "1");
                    nv5.Add("@Batchlocation", Batchlocation);
                    DataTable dt = objCommon.GetDataTable("GET_CheckBatchlocation", nv5);

                    if (dt != null && dt.Rows.Count > 0)
                    {

                        FertilizationCode = Convert.ToInt32(dt.Rows[0]["FertilizationCode"]);
                    }
                    else
                    {
                        dtTrays.Clear();
                        DataTable dt1 = new DataTable();
                        NameValueCollection nv14 = new NameValueCollection();
                        NameValueCollection nvimg = new NameValueCollection();
                        nv14.Add("@Mode", "12");
                        dt1 = objCommon.GetDataTable("GET_Common", nv14);
                        FertilizationCode = Convert.ToInt32(dt1.Rows[0]["FCode"]);

                        dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text, "", txtFTrays.Text, txtSQFT.Text);

                        objTask.AddFertilizerRequestDetailsCreatTask(dtTrays, "0", FertilizationCode, Batchlocation, "", "", "", txtResetSprayTaskForDays.Text, txtFComments.Text.Trim());
                    }
                    long result2 = 0;
                    NameValueCollection nv4 = new NameValueCollection();
                    nv4.Add("@SupervisorID", ddlFertilizationSupervisor.SelectedValue);
                    nv4.Add("@Type", "Fertilizer");
                    nv4.Add("@Jobcode", JobCode);
                    nv4.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv4.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv4.Add("@Facility", Session["Facility"].ToString());
                    nv4.Add("@GreenHouseID", (row1.FindControl("lblGHD") as Label).Text);
                    nv4.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                    nv4.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv4.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    //nv.Add("@WorkOrder", lblwo.Text);
                    nv4.Add("@LoginID", Session["LoginID"].ToString());
                    nv4.Add("@FertilizationCode", FertilizationCode.ToString());
                    nv4.Add("@FertilizationDate", txtFDate.Text);
                    result2 = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManual", nv4);
                    // Batchlocation = (row.FindControl("lblGreenHouse") as Label).Text;

                }

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



            long result16 = 0;
            foreach (GridViewRow row1 in GV5.Rows)
            {
                foreach (GridViewRow row in GV2.Rows)
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
                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv.Add("@jobcode", JobCode);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@Facility", Session["Facility"].ToString());
                    nv.Add("@GreenHouseID", (row1.FindControl("lblGHD") as Label).Text);
                    nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv.Add("@Seeddate", (row.FindControl("lblSeededDate") as Label).Text);
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    nv.Add("@SupervisorID", ddlgerminationSupervisor.SelectedValue);
                    nv.Add("@InspectionDueDate", txtGerDate.Text);
                    nv.Add("@TraysInspected", txtTGerTrays.Text);

                    nv.Add("@LoginId", Session["LoginID"].ToString());
                    nv.Add("@Comments", txtGcomments.Text);

                    result16 = objCommon.GetDataInsertORUpdate("SP_AddGerminationRequesMenualDetailsCreateTask", nv);


                }
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
            foreach (GridViewRow row1 in GV5.Rows)
            {

                int IrrigationCode = 0;
                DataTable dt = new DataTable();
                NameValueCollection nv17 = new NameValueCollection();
                NameValueCollection nvimg = new NameValueCollection();
                nv17.Add("@Mode", "13");
                dt = objCommon.GetDataTable("GET_Common", nv17);
                IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);

                foreach (GridViewRow row in GV2.Rows)
                {

                    long result16 = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlirrigationSupervisor.SelectedValue);

                    nv.Add("@Jobcode", JobCode);
                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@Facility", Session["Facility"].ToString());
                    nv.Add("@GreenHouseID", (row1.FindControl("lblGHD") as Label).Text);
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
                    nv.Add("@Nots", txtIrrComments.Text.Trim());
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    result16 = objCommon.GetDataExecuteScaler("SP_AddIrrigationRequestManual", nv);


                }

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
            foreach (GridViewRow row1 in GV5.Rows)
            {
                int IrrigationCode = 0;
                DataTable dt = new DataTable();
                NameValueCollection nv11 = new NameValueCollection();
                nv11.Add("@Mode", "13");
                dt = objCommon.GetDataTable("GET_Common", nv11);
                IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);

                foreach (GridViewRow row in GV2.Rows)
                {

                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlplant_readySupervisor.SelectedValue);

                    nv.Add("@Jobcode", JobCode);
                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@Facility", Session["Facility"].ToString());
                    nv.Add("@GreenHouseID", (row1.FindControl("lblGHD") as Label).Text);
                    nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@ChId", "0");
                    nv.Add("@Comments", txtPlantComments.Text.Trim());
                    nv.Add("@wo", (row.FindControl("lblwo") as Label).Text);
                    nv.Add("@PlantDate", txtPlantDate.Text);
                    result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyRequestManuaCreateTask", nv);


                }

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



        protected void btnChemicalReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnChemicalSubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row1 in GV5.Rows)
            {
                dtCTrays.Clear();

                int ChemicalCode = 0;
                string Batchlocation = "";

                foreach (GridViewRow row in GV2.Rows)
                {
                    Batchlocation = (row1.FindControl("lblGHD") as Label).Text;

                    NameValueCollection nv5 = new NameValueCollection();
                    nv5.Add("@Mode", "2");
                    nv5.Add("@Batchlocation", Batchlocation);
                    DataTable dt = objCommon.GetDataTable("GET_CheckBatchlocation", nv5);

                    if (dt != null && dt.Rows.Count > 0)
                    {

                        ChemicalCode = Convert.ToInt32(dt.Rows[0]["ChemicalCode"]);
                    }
                    else
                    {
                        dtCTrays.Clear();
                        DataTable dt1 = new DataTable();
                        NameValueCollection nv1 = new NameValueCollection();
                        nv1.Add("@Mode", "16");
                        dt1 = objCommon.GetDataTable("GET_Common", nv1);
                        ChemicalCode = Convert.ToInt32(dt1.Rows[0]["CCode"]);


                        dtCTrays.Rows.Add(ddlChemical.SelectedItem.Text, txtChemicalTrays.Text, txtSQFT.Text);
                        objTask.AddChemicalRequestDetails(dtCTrays, "0", ChemicalCode, Batchlocation, txtResetSprayTaskForDays.Text, ddlMethod.SelectedValue, txtCComments.Text);

                    }


                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlChemical_supervisor.SelectedValue);
                    nv.Add("@Type", "Chemical");
                    nv.Add("@Jobcode", JobCode);
                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@Facility", Session["Facility"].ToString());
                    //    nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                    nv.Add("@GreenHouseID", "");
                    nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    //nv.Add("@WorkOrder", lblwo.Text);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@ChemicalCode", ChemicalCode.ToString());
                    nv.Add("@ChemicalDate", txtChemicalSprayDate.Text);
                    // nv.Add("@Comments", txtcomments.Text);
                    nv.Add("@Method", ddlMethod.SelectedValue);
                    result = objCommon.GetDataExecuteScaler("SP_AddChemicalRequestManual", nv);
                }

                //   dtCTrays.Rows.Add(ddlChemical.SelectedItem.Text, txtChemicalTrays.Text, txtSQFT.Text);
                //  objTask.AddChemicalRequestDetails(dtCTrays, ddlChemical.SelectedValue, ChemicalCode, (row1.FindControl("lblGHD") as Label).Text, txtResetSprayTaskForDays.Text, ddlMethod.SelectedValue, txtCComments.Text);

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



        protected void ddlToFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBench_Location();
        }

        protected void MoveReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnMoveSubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row1 in GV5.Rows)
            {

                foreach (GridViewRow row in GV2.Rows)
                {

                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlLogisticManager.SelectedValue);
                    nv.Add("@WorkOrder", "0");
                    nv.Add("@GrowerPutAwayID", "0");

                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@FromFacility", Session["Facility"].ToString());
                    nv.Add("@ToFacility", ddlToFacility.SelectedValue);
                    nv.Add("@ToGreenHouse", (row1.FindControl("lblGHD") as Label).Text);
                    nv.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                    nv.Add("@MoveDate", txtMoveDate.Text);

                    nv.Add("@Jobcode", JobCode);
                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    nv.Add("@ChId", "0");
                    nv.Add("@Comments", txtMoveComments.Text.Trim());
                    result = objCommon.GetDataExecuteScaler("SP_AddMoveRequestManualCreateTask", nv);

                }

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


        protected void btnDumpSumbit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row1 in GV5.Rows)
            {


                foreach (GridViewRow row in GV2.Rows)
                {

                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlDumptAssignment.SelectedValue);

                    nv.Add("@Jobcode", JobCode);
                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@Facility", Session["Facility"].ToString());
                    nv.Add("@GreenHouseID", (row1.FindControl("lblGHD") as Label).Text);
                    nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@ChId", "0");
                    nv.Add("@Comments", txtCommentsDump.Text.Trim());
                    nv.Add("@QuantityOfTray", txtQuantityofTray.Text.Trim());
                    nv.Add("@wo", (row.FindControl("lblwo") as Label).Text);
                    nv.Add("@DumpDate", txtDumpDate.Text);
                    result = objCommon.GetDataExecuteScaler("SP_AddDumpRequestManuaCreateTask", nv);


                }

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

        protected void btnDumpReset_Click(object sender, EventArgs e)
        {

        }




        protected void btngermination_Click1(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/CreateTask.aspx?jobCode={0}&View={1}", JobCode, "Germination"));
        }

        protected void GV2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < GV2.Columns.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("data-head", GV2.Columns[i].HeaderText);
                }

            }
        }

        protected void btnFertilization_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/CreateTask.aspx?jobCode={0}&View={1}", JobCode, "Fertilization"));
        }

        protected void btnChemical_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/CreateTask.aspx?jobCode={0}&View={1}", JobCode, "Chemical"));
        }

        protected void btnIrrigation_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/CreateTask.aspx?jobCode={0}&View={1}", JobCode, "Irrigation"));
        }

        protected void btnPlantReady_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/CreateTask.aspx?jobCode={0}&View={1}", JobCode, "PlantReady"));
        }


        protected void btnMoveRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/CreateTask.aspx?jobCode={0}&View={1}", JobCode, "Move"));
        }

        protected void btnDump_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/CreateTask.aspx?jobCode={0}&View={1}", JobCode, "Dump"));
        }

        protected void btnGeneralTask_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/CreateTask.aspx?jobCode={0}&View={1}", JobCode, "GeneralTask"));
        }

        protected void DGHead02_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < DGHead02.Columns.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("data-head", DGHead02.Columns[i].HeaderText);
                }

            }
        }
        #region DATASET HELPER  
        private bool ColumnEqual(object A, object B)
        {
            // Compares two values to see if they are equal. Also compares DBNULL.Value.             
            if (A == DBNull.Value && B == DBNull.Value) //  both are DBNull.Value  
                return true;
            if (A == DBNull.Value || B == DBNull.Value) //  only one is BNull.Value  
                return false;
            return (A.Equals(B)); // value type standard comparison  
        }
        public DataTable SelectDistinct(DataTable SourceTable, string FieldName)
        {
            // Create a Datatable – datatype same as FieldName  
            DataTable dt = new DataTable(SourceTable.TableName);
            dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);
            // Loop each row & compare each value with one another  
            // Add it to datatable if the values are mismatch  
            object LastValue = null;
            foreach (DataRow dr in SourceTable.Select("", FieldName))
            {
                if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
                {
                    LastValue = dr[FieldName];
                    dt.Rows.Add(new object[] { LastValue });
                }
            }
            return dt;
        }
        #endregion

    }
}
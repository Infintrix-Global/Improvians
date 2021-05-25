
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
        General objGen = new General();

        static string ReceiverEmail = "";


        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };

        public static DataTable dtCTrays = new DataTable()
        { Columns = { "Fertilizer", "Tray", "SQFT" } };


        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["LoginID"] == null)
            {
                Response.Redirect("~/CustomerLogin.aspx");
            }
            if (Session["Role"].ToString() == "13" || Session["Role"].ToString() == "14")
            {
                this.Page.MasterPageFile = "~/Customer/CustomerMaster.master";
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                JobCode = Request.QueryString["jobCode"];
                txtSearchJobNo.Focus();
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

                BindJobCode("");

                if (Session["Role"].ToString() == "13") //Customer
                {
                    divSalesComment.Visible = true;
                    divTaskRequest.Visible = false;
                }
                else if (Session["Role"].ToString() == "14" || Session["Role"].ToString() == "7")// Sales representative
                {
                    divTaskRequest.Visible = false;
                }
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

        public DataView BindGridJobHistory()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobID", JobCode);
            DataSet ds = objCommon.GetDataSet("GetJobTracibilityReportJobHistory", nv);
            DataTable dt = ds.Tables[0];

            DataView dataView = dt.DefaultView;
            string filter = string.Empty;
            if (ddlDescription.SelectedIndex > 0)
            {
                filter = " AND Description = '" + ddlDescription.SelectedValue + "'";
            }
            if (ddlBench.SelectedIndex > 0)
            {
                filter += " AND ( GreenhouseID = '" + ddlBench.SelectedValue + "' OR GreenhouseID = '')";
            }
            if (ddlAssignedBy.SelectedIndex > 0)
            {
                filter += " AND AssignedBy = '" + ddlAssignedBy.SelectedValue + "'";
            }
            if (ddlAssignedTo.SelectedIndex > 0)
            {
                filter += " AND AssignedTo = '" + ddlAssignedTo.SelectedValue + "'";
            }
            if (filter != string.Empty)
                dataView.RowFilter = filter.Substring(4);
            // dataView.Sort = " EndingDate  ASC ";
            GV4.DataSource = dataView;
            GV4.DataBind();
            return dataView;
        }

        protected void ddlDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dataView = BindGridJobHistory();
            if (ddlBench.SelectedIndex == 0)
            {
                ddlBench.DataSource = dataView.ToTable(true, "GreenhouseID");
                ddlBench.DataBind();
                ddlBench.Items.Insert(0, new ListItem("--- Select ---", ""));
            }
            if (ddlAssignedBy.SelectedIndex == 0)
            {
                ddlAssignedBy.DataSource = dataView.ToTable(true, "AssignedBy");
                ddlAssignedBy.DataBind();
                ddlAssignedBy.Items.Insert(0, new ListItem("--- Select ---", ""));
            }
            if (ddlAssignedTo.SelectedIndex == 0)
            {
                ddlAssignedTo.DataSource = dataView.ToTable(true, "AssignedTo");
                ddlAssignedTo.DataBind();
                ddlAssignedTo.Items.Insert(0, new ListItem("--- Select ---", ""));
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlAssignedBy.SelectedIndex = 0;
            ddlDescription.SelectedIndex = 0;
            ddlAssignedTo.SelectedIndex = 0;
            ddlBench.SelectedIndex = 0;
            BindGridJobHistory();
        }
        protected void ddlGreenhouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dataView = BindGridJobHistory();
            if (ddlDescription.SelectedIndex == 0)
            {
                ddlDescription.DataSource = dataView.ToTable(true, "Description");
                ddlDescription.DataBind();
                ddlDescription.Items.Insert(0, new ListItem("--- Select ---", ""));
            }
            if (ddlAssignedBy.SelectedIndex == 0)
            {
                ddlAssignedBy.DataSource = dataView.ToTable(true, "AssignedBy");
                ddlAssignedBy.DataBind();
                ddlAssignedBy.Items.Insert(0, new ListItem("--- Select ---", ""));
            }
            if (ddlAssignedTo.SelectedIndex == 0)
            {
                ddlAssignedTo.DataSource = dataView.ToTable(true, "AssignedTo");
                ddlAssignedTo.DataBind();
                ddlAssignedTo.Items.Insert(0, new ListItem("--- Select ---", ""));
            }
        }
        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dataView = BindGridJobHistory();
            if (ddlDescription.SelectedIndex == 0)
            {
                ddlDescription.DataSource = dataView.ToTable(true, "Description");
                ddlDescription.DataBind();
                ddlDescription.Items.Insert(0, new ListItem("--- Select ---", ""));
            }
            if (ddlBench.SelectedIndex == 0)
            {
                ddlBench.DataSource = dataView.ToTable(true, "GreenhouseID");
                ddlBench.DataBind();
                ddlBench.Items.Insert(0, new ListItem("--- Select ---", ""));
            }
            if (ddlAssignedTo.SelectedIndex == 0)
            {
                ddlAssignedTo.DataSource = dataView.ToTable(true, "AssignedTo");
                ddlAssignedTo.DataBind();
                ddlAssignedTo.Items.Insert(0, new ListItem("--- Select ---", ""));
            }
        }
        protected void ddlAssignedTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dataView = BindGridJobHistory();
            if (ddlDescription.SelectedIndex == 0)
            {
                ddlDescription.DataSource = dataView.ToTable(true, "Description");
                ddlDescription.DataBind();
                ddlDescription.Items.Insert(0, new ListItem("--- Select ---", ""));
            }
            if (ddlBench.SelectedIndex == 0)
            {
                ddlBench.DataSource = dataView.ToTable(true, "GreenhouseID");
                ddlBench.DataBind();
                ddlBench.Items.Insert(0, new ListItem("--- Select ---", ""));
            }
            if (ddlAssignedBy.SelectedIndex == 0)
            {
                ddlAssignedBy.DataSource = dataView.ToTable(true, "AssignedBy");
                ddlAssignedBy.DataBind();
                ddlAssignedBy.Items.Insert(0, new ListItem("--- Select ---", ""));
            }
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
            BindJobCode("");
            BindGridOne();
            PanelView.Visible = false;
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
                    if (string.IsNullOrEmpty(Facility))
                        cmd.CommandText = " select distinct jobcode from gti_jobs_seeds_plan where jobcode like '%" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where  jobcode like '" + prefixText + "%' order by jobcode";
                    else
                        cmd.CommandText = " select distinct jobcode from gti_jobs_seeds_plan where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "'  AND jobcode like '" + prefixText + "%' order by jobcode";

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
            //do not show edit button to customer
            if (Session["Role"].ToString() == "13" || Session["Role"].ToString() == "14")
            {
                e.Row.Cells[3].Visible = false;
            }
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
                }
            }
        }

        //---------------------------------------------------------------------------------------------TASK Create ------------------



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
        protected void btngermination_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/CreateTask.aspx?jobCode={0}&View={1}", JobCode, "Germination"));
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

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string CCMail = Session["Email"].ToString();
            DataTable dt = GetSalesData();
            if (dt != null && dt.Rows.Count > 0)
            {
                string ToMail = dt.Rows[0]["Email"].ToString();
                string Subject = "Contact Request is made by " + Session["EmployeeName"].ToString();
                string Sales = dt.Rows[0]["EmployeeName"].ToString();
                string msg = "Hi " + Sales + "," + "<br /><br />";
                msg = msg + "You have received below message from customer name " + Session["EmployeeName"].ToString() + "<br />";
                msg = msg + msgs.Text + "<br />" + "<br />";
                msg = msg + "Job Information page associated with this message: <a href='" + HttpContext.Current.Request.Url + "'>" + JobCode + "</a><br/>";
                msg = msg + "<br />Thanks, <br/> Customer Information Portal";
                objGen.SendMail(ToMail, CCMail, Subject, msg);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Thank you for your message. Our Sales Representative " + Sales + " has been sent this request over an email. " + Sales + " will get in touch with you soon')", true);
                msgs.Text = "";
            }
        }

        private DataTable GetSalesData()
        {
            string sqr = "Select L.ID,EmployeeName,Mobile,Email,Photo from Login L join CustomerSalesMapping C on L.ID=C.SalesID where C.CustomerID=" + Session["LoginID"].ToString();
            return objGen.GetDatasetByCommand(sqr);
        }



    }
}
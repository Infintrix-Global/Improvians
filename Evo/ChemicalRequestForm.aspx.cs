using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.BAL_Classes;
using Evo.Bal;
using System.Data.SqlClient;
using System.Configuration;

namespace Evo
{
    public partial class ChemicalRequestForm : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };


        public static DataTable dtCTrays = new DataTable()
        { Columns = { "Fertilizer", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Fdate = "", TDate = "";
                Fdate = Convert.ToDateTime(System.DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd");
                TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(14).ToString("yyyy-MM-dd");

              //  txtFromDate.Text = Fdate;
               // txtToDate.Text = TDate;

                //Bindcname();
                //BindBenchLocation(Session["Facility"].ToString());
                //BindJobCode(ddlBenchLocation.SelectedValue);
                BindTaskType();
                BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
                BindJobCode("0", "0", "0");
                Bindcname("0", "0", "0");
                BindCrop();
                BindAssignByList("0", "0", "0");

                BindGridFerReq("0", 0);
                dtTrays.Clear();
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

        private string TaskKey
        {
            get
            {
                if (Request.QueryString["Tkey"] != null)
                {
                    return Request.QueryString["Tkey"].ToString();
                }
                return "";
            }
            set
            {
                // JobCode = Request.QueryString["jobId"].ToString();
                // JobCode = value;
            }
        }

        public void BindCrop()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@JobNo", ddlJobNo.SelectedValue);
            nv.Add("@GenusCode", "0");
            nv.Add("@Mode", "6");
            nv.Add("@Type", "Chem");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            ddlCrop.DataSource = dt;
            ddlCrop.DataTextField = "GenusCode";
            ddlCrop.DataValueField = "GenusCode";
            ddlCrop.DataBind();
            ddlCrop.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindAssignByList(string ddlBench, string jobNo, string Cust)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBench);
            nv.Add("@Customer", Cust);
            nv.Add("@JobNo", jobNo);
            nv.Add("@GenusCode", ddlCrop.SelectedValue);
            nv.Add("@Mode", "4");
            nv.Add("@Type", "Chem");


            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);

            ddlAssignedBy.DataSource = dt;
            ddlAssignedBy.DataTextField = "AssingTo";
            ddlAssignedBy.DataValueField = "AssingTo";
            ddlAssignedBy.DataBind();
            ddlAssignedBy.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlAssignedBy.Items.Insert(1, new ListItem("System", "System"));
        }
        public void Bindcname(string ddlBench, string jobNo, string Code)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", !string.IsNullOrEmpty(ddlBench) ? ddlBench : "0");
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@JobNo", !string.IsNullOrEmpty(jobNo) ? jobNo : "0");
            nv.Add("@GenusCode", !string.IsNullOrEmpty(Code) ? Code : "0");
            nv.Add("@Mode", "3");
            nv.Add("@Type", "Chem");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "Customer";
            ddlCustomer.DataValueField = "Customer";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindJobCode(string ddlBench, string Customer, string Code)
        {
            //  ddlJobNo.Items[0].Selected = false;
            // ddlJobNo.ClearSelection();
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", !string.IsNullOrEmpty(ddlBench) ? ddlBench : "0");
            nv.Add("@Customer", !string.IsNullOrEmpty(Customer) ? Customer : "0");
            nv.Add("@JobNo", "0");
            nv.Add("@GenusCode", !string.IsNullOrEmpty(Code) ? Code : "0");

            nv.Add("@Mode", "2");
            nv.Add("@Type", "Chem");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            //   ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataBind();

            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindBenchLocation(string ddlMain, string jobNo, string Customer, string Code)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", ddlMain);
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Customer", !string.IsNullOrEmpty(Customer) ? Customer : "0");
            nv.Add("@JobNo", !string.IsNullOrEmpty(jobNo) ? jobNo : "0");
            nv.Add("@GenusCode", !string.IsNullOrEmpty(Code) ? Code : "0");
            nv.Add("@Mode", "1");
            nv.Add("@Type", "Chem");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);

            // ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataSource = dt;
            ddlBenchLocation.DataTextField = "BenchLocation";
            ddlBenchLocation.DataValueField = "BenchLocation";
            ddlBenchLocation.DataBind();
        //    ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));


        }

        private string getBenchLocation()
        {
            int c = 0;
            string x = "";
            string chkSelected = "";
            foreach (ListItem item in ddlBenchLocation.Items)
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

        public void BindTaskType()
        {

            NameValueCollection nv = new NameValueCollection();
            nv.Add("@ID", "0");
            RadioButtonListGno.DataSource = objCommon.GetDataTable("SP_GetChemicalRequestDateCountNo", nv); ;
            RadioButtonListGno.DataTextField = "DateCountNoName";
            RadioButtonListGno.DataValueField = "DateCountNo";
            RadioButtonListGno.DataBind();
            RadioButtonListGno.Items.Insert(0, new ListItem("--Select--", "0"));


        }

        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBenchLocation.SelectedIndex == -1)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(getBenchLocation(), ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            BindGridFerReq("0", 1);
        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBenchLocation.SelectedIndex == -1)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(getBenchLocation(), ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridFerReq("0", 1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedIndex == -1)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlBenchLocation.SelectedIndex == -1)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, "0", ddlCrop.SelectedValue);
            BindAssignByList("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridFerReq(ddlJobNo.SelectedValue, 1);
        }




        protected void btnSearch_Click(object sender, EventArgs e)
        {



            string Jno = "";
            if (txtSearchJobNo.Text == "")
            {
                Jno = ddlJobNo.SelectedValue;

            }
            else
            {
                Jno = txtSearchJobNo.Text;
            }
            BindGridFerReq(Jno, 1);
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindJobCode(ddlBenchLocation.SelectedValue);
            string selectedValues = getBenchLocation();
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname(selectedValues, "0", "0");
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(selectedValues, "0", "0");
            if (ddlAssignedBy.SelectedIndex == 0)
                BindAssignByList(selectedValues, "0", "0");
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridFerReq(ddlJobNo.SelectedValue, 1);
        }

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq(ddlJobNo.SelectedValue, 1);
        }

        protected void RadioButtonListSourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq("0", 1);
        }

        protected void RadioButtonListF_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq("0", 1);
        }
        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            BindBenchLocation(Session["Facility"].ToString(), txtSearchJobNo.Text, "0", "0");
            BindGridFerReq(txtSearchJobNo.Text, 1);

        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            txtSearchJobNo.Text = "";
            RadioButtonListSourse.Items[0].Selected = false;
            RadioButtonListSourse.ClearSelection();
            RadioButtonListGno.Items[0].Selected = false;
            RadioButtonListGno.ClearSelection();
            BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
            Bindcname("0", "0", "0");
            // BindJobCode("0");
            BindJobCode("0", "0", "0");
            BindCrop();
            BindGridFerReq("0", 1);

        }

        protected void txtBatchLocation_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            //BindJobCode(ddlBenchLocation.SelectedValue);
            Bindcname(getBenchLocation(), "0", "0");
            BindJobCode(getBenchLocation(), "0", "0");
            BindAssignByList(getBenchLocation(), "0", "0");
            BindGridFerReq(ddlJobNo.SelectedValue, 1);
        }

        public void BindGridFerReq(string JobCode, int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", JobCode);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Crop", ddlCrop.SelectedValue);
            nv.Add("@Status", "");
            nv.Add("@Jobsource", RadioButtonListSourse.SelectedValue);
            nv.Add("@GermNo", RadioButtonListGno.SelectedValue);
            nv.Add("@FromDate", txtFromDate.Text);
            nv.Add("@ToDate", txtToDate.Text);
            nv.Add("@AssignedBy", ddlAssignedBy.SelectedValue);

            if (Session["Role"].ToString() == "12")
            {
                dt = objCommon.GetDataTable("SP_GetChemicalRequestAssistantGrower", nv);
            }
            else
            {
                dt = objCommon.GetDataTable("SP_GetChemicalRequest", nv);
            }

            gvFer.DataSource = dt;
            gvFer.DataBind();

            //foreach (GridViewRow row in gvFer.Rows)
            //{
            //    var checkJob = (row.FindControl("lblID") as Label).Text;
            //    if (checkJob == JobCode)
            //    {
            //        row.CssClass = "highlighted";
            //    }
            //}
            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }
        }

        // Show Details
        private void highlight(int limit)
        {
            var i = gvFer.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvFer.Rows)
            {
                var checkJob = (row.FindControl("lblID") as Label).Text;
                var checklocation = (row.FindControl("lblBatchlocation1") as Label).Text;
                var tKey = gvFer.DataKeys[row.RowIndex].Values[4].ToString();
                i--;
                if (checkJob == JobCode && checklocation == benchLoc && tKey == TaskKey)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvFer.PageIndex++;
                    gvFer.DataBind();
                    highlight((limit - 10));
                }
            }
        }

        public void BindGridFerDetails()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", getBenchLocation());
            dt = objCommon.GetDataTable("SP_GetFertilizerRequestDetails", nv);
            //gvJobHistory.DataSource = dt;
            // gvJobHistory.DataBind();
        }

        protected void gvFer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Job")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string BatchLocation = gvFer.DataKeys[rowIndex].Values[0].ToString();
                string jobCode = gvFer.DataKeys[rowIndex].Values[1].ToString();
                string CCode = gvFer.DataKeys[rowIndex].Values[2].ToString();
                string TaskRequestKey = gvFer.DataKeys[rowIndex].Values[4].ToString();
                string AssignedBy = gvFer.DataKeys[rowIndex].Values[5].ToString();
                //  Response.Redirect(String.Format("~/ChemicalJobBuildUp.aspx?Bench={0}&jobCode={1},&CCode={2}", BatchLocation, jobCode, CCode));
                Response.Redirect(String.Format("~/ChemicalJobBuildUp.aspx?Bench={0}&jobCode={1}&CCode={2}&TaskRequestKey={3}&AssignedBy={4}", BatchLocation, jobCode, CCode, TaskRequestKey, AssignedBy));
            }

            if (e.CommandName == "GStart")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string BatchLocation = gvFer.DataKeys[rowIndex].Values[0].ToString();
                string jobCode = gvFer.DataKeys[rowIndex].Values[1].ToString();
                string CCode = gvFer.DataKeys[rowIndex].Values[2].ToString();
                string TaskRequestKey = gvFer.DataKeys[rowIndex].Values[4].ToString();
                string AssignedBy = gvFer.DataKeys[rowIndex].Values[5].ToString();
                Response.Redirect(String.Format("~/ChemicalStart.aspx?Bench={0}&jobCode={1}&CCode={2}&TaskRequestKey={3}&AssignedBy={4}", BatchLocation, jobCode, CCode, TaskRequestKey, AssignedBy));
            }
        }

        protected void gvFer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFer.PageIndex = e.NewPageIndex;
            BindGridFerReq("0", 1);
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

        protected void btnManual_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ChemicalReqManual.aspx");
        }

       

        protected void btnJob_Click(object sender, EventArgs e)
        {
            //if(gvJobHistory.Visible==true)
            //{
            //    gvJobHistory.Visible = false;
            //}
            //else if (gvJobHistory.Visible == false)
            //{
            //    gvJobHistory.Visible = true;
            //}
        }

        
    

        protected void gvFer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblsource = (Label)e.Row.FindControl("lblsource");
                if (lblsource.Text == "Manual")
                {
                    lblsource.Text = "Navision";
                }

                Label lblGermDate = (Label)e.Row.FindControl("lblChemDate");
                string dtimeString = Convert.ToDateTime(lblGermDate.Text).ToString("yyyy/MM/dd");
                DateTime dtime = Convert.ToDateTime(dtimeString);
                DateTime nowtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                if (nowtime > dtime)
                {
                    e.Row.CssClass = "overdue";
                }
            }
        }

        protected void btnManual_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/ChemicalReqManual.aspx");
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
                    cmd.CommandText = " select distinct GPD.jobcode from gti_jobs_seeds_plan GTS inner join GrowerPutAwayDetails GPD on GPD.wo=GTS.wo  where  GPD.FacilityID ='" + Facility + "'  AND GPD.jobcode like '%" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%' order by jobcode" +
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
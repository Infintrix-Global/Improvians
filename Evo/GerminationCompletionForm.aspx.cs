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

namespace Evo
{
    public partial class GerminationCompletionForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              

                BindBenchLocation(Session["Facility"].ToString(), "0");
                BindJobCode("0");
            
                BindAssignByList("0", "0");

                BindGridGerm("0", 0);
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

            }
        }



        public void BindAssignByList(string ddlBench, string jobNo)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Customer","0");
            nv.Add("@JobNo", jobNo);
            nv.Add("@GenusCode","0");
            nv.Add("@Mode", "4");
            nv.Add("@Type", "Ger");


            dt = objCommon.GetDataTable("SP_TaskFilterSearchSupervisor", nv);

            ddlAssignedBy.DataSource = dt;
            ddlAssignedBy.DataTextField = "AssingTo";
            ddlAssignedBy.DataValueField = "AssingTo";
            ddlAssignedBy.DataBind();
            ddlAssignedBy.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlAssignedBy.Items.Insert(1, new ListItem("System", "System"));
        }


        public void BindJobCode(string ddlBench)
        {
            //  ddlJobNo.Items[0].Selected = false;
            // ddlJobNo.ClearSelection();
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Customer",  "0");
            nv.Add("@JobNo", "0");
            nv.Add("@GenusCode",  "0");

            nv.Add("@Mode", "2");
            nv.Add("@Type", "Ger");

            dt = objCommon.GetDataTable("SP_TaskFilterSearchSupervisor", nv);
            //   ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataBind();

            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindBenchLocation(string ddlMain, string jobNo)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", ddlMain);
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Customer",  "0");
            nv.Add("@JobNo", !string.IsNullOrEmpty(jobNo) ? jobNo : "0");
            nv.Add("@GenusCode",  "0");
            nv.Add("@Mode", "1");
            nv.Add("@Type", "Ger");

            dt = objCommon.GetDataTable("SP_TaskFilterSearchSupervisor", nv);

            // ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataSource = dt;
            ddlBenchLocation.DataTextField = "BenchLocation";
            ddlBenchLocation.DataValueField = "BenchLocation";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));


        }


        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (ddlBenchLocation.SelectedIndex == -1)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue);
            BindAssignByList("0", ddlJobNo.SelectedValue);

            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }
        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindJobCode(ddlBenchLocation.SelectedValue);
            string selectedValues = getBenchLocation();
          
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(selectedValues);
            if (ddlAssignedBy.SelectedIndex == 0)
                BindAssignByList(selectedValues, "0");
            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm(ddlJobNo.SelectedValue, 1);
        }

        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
           
            BindBenchLocation(Session["Facility"].ToString(), txtSearchJobNo.Text);
            BindGridGerm(txtSearchJobNo.Text, 1);

        }



        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            txtSearchJobNo.Text = "";


            BindBenchLocation(Session["Facility"].ToString(), "0");
            BindJobCode("0");

            BindAssignByList("0", "0");

            BindGridGerm("0", 1);
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
        public void BindGridGerm(string JobNo,int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@JobCode", JobNo);
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@AssignedBy", ddlAssignedBy.SelectedValue);
            dt = objCommon.GetDataTable("SP_GetOperatorGerminationTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();


            //foreach (GridViewRow row in gvGerm.Rows)
            //{
            //    var checkJob = (row.FindControl("lbljobID") as Label).Text;
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
        private void highlight(int limit)
        {
            var i = gvGerm.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvGerm.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                var tKey = gvGerm.DataKeys[row.RowIndex].Values[2].ToString();
                i--;
                if (checkJob == JobCode && checklocation == benchLoc && tKey == TaskKey)
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
            BindGridGerm("",1);
        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string Wo = "";
            string GTAID = "", TaskRequestKey="";
            long result = 0;
            if (e.CommandName == "Start")
            {
                //int rowIndex = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = gvGerm.Rows[rowIndex];

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvGerm.Rows[rowIndex];

                GTAID = gvGerm.DataKeys[rowIndex].Values[0].ToString();
                lblGTR_ID.Text = gvGerm.DataKeys[rowIndex].Values[1].ToString();
                TaskRequestKey = gvGerm.DataKeys[rowIndex].Values[2].ToString();
              
                Response.Redirect(String.Format("~/GreenHouseTaskCompletion.aspx?GTAID={0}&GTRID={1}&IsF={2}&TaskRequestKey={3}", GTAID, lblGTR_ID.Text, 0, TaskRequestKey));
            }
        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGermNo = (Label)e.Row.FindControl("lblGermNo");
                HyperLink lnkJobID = (HyperLink)e.Row.FindControl("lnkJobID");
                lnkJobID.NavigateUrl = "~/JobReports.aspx?JobCode=" + lnkJobID.Text + "&GermNo=" + lblGermNo.Text;

                Label lblGermDate = (Label)e.Row.FindControl("lblGermDate");
                string dtimeString = Convert.ToDateTime(lblGermDate.Text).ToString("yyyy/MM/dd");

                DateTime dtime = Convert.ToDateTime(dtimeString);

                DateTime nowtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));

                if (nowtime > dtime)
                {
                    e.Row.CssClass = "overdue";
                }
            }
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
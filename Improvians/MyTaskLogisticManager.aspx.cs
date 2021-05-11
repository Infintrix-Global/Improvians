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
    public partial class MyTaskLogisticManager : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
                BindBenchLocation("0", "0", "0","0");
                BindAssignByList("0", "0", "0");
                Bindcname("0", "0", "0");
                BindJobCode("0", "0", "0");
                BindGridGerm("0");
            }
        }

        public void BindGridGerm(string JobCode)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@JobCode", JobCode);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@AssignedBy", ddlAssignedBy.SelectedValue);

            dt = objCommon.GetDataTable("SP_GetMoveSiteTeamTasknew", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();
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
            nv.Add("@GenusCode", "0");
            nv.Add("@Mode", "4");
            nv.Add("@Type", "PutAwy");


            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);

            ddlAssignedBy.DataSource = dt;
            ddlAssignedBy.DataTextField = "AssingTo";
            ddlAssignedBy.DataValueField = "AssingTo";
            ddlAssignedBy.DataBind();
            ddlAssignedBy.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlAssignedBy.Items.Insert(1, new ListItem("System", "System"));
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
            nv.Add("@GenusCode", Core);
            nv.Add("@Mode", "3");
            nv.Add("@Type", "PutAwy");

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
            nv.Add("@GenusCode", Core);

            nv.Add("@Mode", "2");
            nv.Add("@Type", "PutAwy");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            //   ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataBind();

            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindBenchLocation(string ddlMain, string jobNo, string Customer, string Core)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@Customer", !string.IsNullOrEmpty(Customer) ? Customer : "0");
            nv.Add("@JobNo", !string.IsNullOrEmpty(jobNo) ? jobNo : "0");
            nv.Add("@GenusCode", Core);
            nv.Add("@Mode", "1");
            nv.Add("@Type", "PutAwy");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);

            // ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataSource = dt;
            ddlBenchLocation.DataTextField = "BenchLocation";
            ddlBenchLocation.DataValueField = "BenchLocation";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Assign")
            {
                string GrowerPutAwayId = e.CommandArgument.ToString();
                Response.Redirect(String.Format("~/MoveTaskAssignment.aspx?GrowerPutAwayId={0}", GrowerPutAwayId));
            }
            if (e.CommandName == "Select")
            {
                string GrowerPutAwayId = e.CommandArgument.ToString();
                NameValueCollection nv1 = new NameValueCollection();
                nv1.Add("@CoordinatorId", Session["LoginID"].ToString());
                nv1.Add("@GrowerPutAwayId", GrowerPutAwayId);
                nv1.Add("@CreateBy", Session["LoginID"].ToString());

                long result = objCommon.GetDataInsertORUpdate("SP_AddAssign_Task_Shipping_Coordinator", nv1);

                Response.Redirect(String.Format("~/MoveCompletionForm.aspx?GrowerPutAwayId={0}", GrowerPutAwayId));

            }
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm(ddlJobNo.SelectedValue);
        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnAssign = (Button)e.Row.FindControl("btnAssign");
                Button btnSelect = (Button)e.Row.FindControl("btnSelect");

                int RoleId = Convert.ToInt32(Session["Role"]);
                if (RoleId == 11 || RoleId == 3 || RoleId == 5)
                {
                    btnSelect.Visible = true;
                    btnAssign.Visible = false;
                }

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridGerm(ddlJobNo.SelectedValue);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            BindGridGerm("0");
        }

        public void clear()
        {
            ddlAssignedBy.SelectedIndex = 0;
            ddlBenchLocation.SelectedIndex = 0;
            ddlCustomer.SelectedIndex = 0;
            ddlJobNo.SelectedIndex = 0;
            txtSearchJobNo.Text = "";
        }


        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            BindBenchLocation(Session["Facility"].ToString(), txtSearchJobNo.Text, "0", "0");
            BindGridGerm(string.IsNullOrEmpty(txtSearchJobNo.Text) ? ddlJobNo.SelectedValue : txtSearchJobNo.Text);
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bindcname(ddlBenchLocation.SelectedValue, "0", "0");
            BindJobCode(ddlBenchLocation.SelectedValue, "0", "0");
            BindAssignByList(ddlBenchLocation.SelectedValue, "0", "0");
            BindGridGerm(string.IsNullOrEmpty(txtSearchJobNo.Text) ? ddlJobNo.SelectedValue : txtSearchJobNo.Text);
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, "0");
            BindJobCode(ddlBenchLocation.SelectedValue, ddlCustomer.SelectedValue, "0");
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            BindGridGerm(string.IsNullOrEmpty(txtSearchJobNo.Text) ? ddlJobNo.SelectedValue : txtSearchJobNo.Text);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bindcname("0", ddlJobNo.SelectedValue, "0");
            BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, "0", "0");
            BindAssignByList("0", ddlJobNo.SelectedValue, "0");

            BindGridGerm(string.IsNullOrEmpty(txtSearchJobNo.Text) ? ddlJobNo.SelectedValue : txtSearchJobNo.Text);
        }

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm(string.IsNullOrEmpty(txtSearchJobNo.Text) ? ddlJobNo.SelectedValue : txtSearchJobNo.Text);
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
    }
}
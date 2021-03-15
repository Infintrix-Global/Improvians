using Evo.Admin.BAL_Classes;
using Evo.Bal;
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
    public partial class JobReports : System.Web.UI.Page
    {
        public static string JobCode;
        CommonControlNavision objNav = new CommonControlNavision();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();

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
                    BindGridOne();
                }

            }
        }



        public void BindGridOne()
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobID", JobCode);
            DataSet ds = objCommon.GetDataSet("GetJobTracibilityReport", nv);
            dt = ds.Tables[0];
            dt2 = ds.Tables[1];
            dt3 = ds.Tables[2];
            dt4 = ds.Tables[3];
            dt5 = ds.Tables[4];
            gv1.DataSource = dt;
            GV2.DataSource = dt2;
            Gv3.DataSource = dt3;
            GV4.DataSource = dt4;
            GV5.DataSource = dt5;
            gv1.DataBind();
            GV2.DataBind();
            Gv3.DataBind();
            GV4.DataBind();
            GV5.DataBind();

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
            BindJobCode(ddlBenchLocation.SelectedValue);
        }
        public void BindJobCode(string ddlBench)
        {
            ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", ""));
        }
        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            JobCode = ddlJobNo.SelectedValue.Trim();
            BindGridOne();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            JobCode = txtSearchJobNo.Text.Trim();
            BindGridOne();
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            txtSearchJobNo.Text = "JB";
            BindGridOne();
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
                    cmd.CommandText = " select distinct jobcode from gti_jobs_seeds_plan where loc_seedline ='" + Facility + "'  AND jobcode like '" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "'  AND jobcode like '" + prefixText + "%' order by jobcode" +
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
            //DropDownList ddlPbx = (DropDownList)(GV5.Rows[GV5.EditIndex].FindControl("ddlBenchLocation"));
            //if (ddlPbx != null)
            //    ddlPbx.DataSource = objBAL.GetLocation(Session["Facility"].ToString());
            //ddlPbx.DataTextField = "p2";
            //ddlPbx.DataValueField = "p2";
            //ddlPbx.DataBind();
            //ddlPbx.Items.Insert(0, new ListItem("--- Select ---", ""));

        }

        protected void GV5_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            Label lblid = GV5.Rows[e.RowIndex].FindControl("lblgrowerId") as Label;

            HiddenField field = GV5.Rows[e.RowIndex].FindControl("HiddenField1") as HiddenField;
            TextBox city = GV5.Rows[e.RowIndex].FindControl("txt_Name") as TextBox;
            DropDownList ddlBenchLocation = GV5.Rows[e.RowIndex].FindControl("ddlBenchLocation") as DropDownList;
            long result1 = 0;
            General objGeneral = new General();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@GrowerPutAwayID", lblid.Text);
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
    }
}
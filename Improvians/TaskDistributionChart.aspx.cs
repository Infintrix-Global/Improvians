using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class TaskDistributionChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                txtToDate.Text = Convert.ToDateTime(System.DateTime.Now.AddDays(7)).ToString("yyyy-MM-dd");

                Session["Start"] = txtFromDate.Text;
                Session["End"] = txtToDate.Text;
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["Evo"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand("SP_GetTaskDisctributionChartData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter();
                        List<ReportChartDetails> lstReportChartDetails = new List<ReportChartDetails>();
                        int i = 0;

                        DateTime StartDate = Convert.ToDateTime(HttpContext.Current.Session["Start"].ToString());
                        DateTime EndDate = Convert.ToDateTime(HttpContext.Current.Session["End"].ToString());
                        for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
                        {
                            ReportChartDetails reportdetails = new ReportChartDetails();
                            i = i + 1;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@WorkDate", date);
                            cmd.Parameters.AddWithValue("@LoginID", HttpContext.Current.Session["LoginID"].ToString());
                            cmd.Parameters.AddWithValue("@Facility", HttpContext.Current.Session["Facility"].ToString());
                            string strRoles = string.Empty;
                            if (HttpContext.Current.Session["Role"].ToString() == "1")
                            {
                                strRoles = "1,2,3,5,11,12";
                            }
                            else if (HttpContext.Current.Session["Role"].ToString() == "12")
                            {
                                strRoles = "2,3,5,11,12";
                            }
                            else if (HttpContext.Current.Session["Role"].ToString() == "2")
                            {
                                strRoles = "3,5,11";
                            }
                            else
                            {
                                strRoles = HttpContext.Current.Session["Role"].ToString();
                            }
                            cmd.Parameters.AddWithValue("@RoleIDs", strRoles);
                            da.SelectCommand = cmd;
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            List<ChartDetails> lstChartDetails = new List<ChartDetails>();
                            foreach (DataRow dtrow in dt.Rows)
                            {
                                ChartDetails details = new ChartDetails();
                                details.EmployeeName = dtrow[0].ToString();
                                details.TaskHours = Convert.ToInt32(dtrow[1]);

                                lstChartDetails.Add(details);
                            }
                            reportdetails.ID = i;
                            reportdetails.WorkDate = date;
                            reportdetails.lstChartDetail = lstChartDetails;
                            lstReportChartDetails.Add(reportdetails);
                        }
                        conn.Close();
                        repChart.DataSource = lstReportChartDetails;
                        repChart.DataBind();
                    }
                }

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void repChart_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<ReportChartDetails> GetTaskDisctributionChartData()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Evo"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("SP_GetTaskDisctributionChartData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    List<ReportChartDetails> lstReportChartDetails = new List<ReportChartDetails>();
                    int i = 0;

                    DateTime StartDate = Convert.ToDateTime(HttpContext.Current.Session["Start"].ToString());
                    DateTime EndDate = Convert.ToDateTime(HttpContext.Current.Session["End"].ToString());
                    for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
                    {
                        ReportChartDetails reportdetails = new ReportChartDetails();
                        i = i + 1;
                        cmd.Parameters.AddWithValue("@WorkDate", date);
                        cmd.Parameters.AddWithValue("@LoginID", HttpContext.Current.Session["LoginID"].ToString());
                        cmd.Parameters.AddWithValue("@Facility", HttpContext.Current.Session["Facility"].ToString());
                        string strRoles = string.Empty;
                        if (HttpContext.Current.Session["Role"].ToString() == "1")
                        {
                            strRoles = "1,2,3,5,11,12";
                        }
                        else if (HttpContext.Current.Session["Role"].ToString() == "12")
                        {
                            strRoles = "2,3,5,11,12";
                        }
                        else if (HttpContext.Current.Session["Role"].ToString() == "2")
                        {
                            strRoles = "3,5,11";
                        }
                        else
                        {
                            strRoles = HttpContext.Current.Session["Role"].ToString();
                        }
                        cmd.Parameters.AddWithValue("@RoleIDs", strRoles);
                        da.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        List<ChartDetails> lstChartDetails = new List<ChartDetails>();
                        foreach (DataRow dtrow in dt.Rows)
                        {
                            ChartDetails details = new ChartDetails();
                            details.EmployeeName = dtrow[0].ToString();
                            details.TaskHours = Convert.ToInt32(dtrow[1]);

                            lstChartDetails.Add(details);
                        }
                        reportdetails.ID = i;
                        reportdetails.WorkDate = date;
                        reportdetails.lstChartDetail = lstChartDetails;
                        lstReportChartDetails.Add(reportdetails);
                    }
                    conn.Close();
                    return lstReportChartDetails;
                }
            }
        }

    }
}
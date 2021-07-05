using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class TaskDistributionChart : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        static string strPreference="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = Convert.ToDateTime(System.DateTime.Now.AddDays(-1)).ToString("yyyy-MM-dd");
                txtToDate.Text = Convert.ToDateTime(System.DateTime.Now.AddDays(4)).ToString("yyyy-MM-dd");
                BindRoles();
                BindData();
            }
        }
        private void BindRoles()
        {
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@UserID", Session["LoginID"].ToString());
            DataTable dt = objCommon.GetDataTable("SP_GetPreferenceTaskDistribution", nv1);
            if (dt != null && dt.Rows.Count > 0)
                strPreference = dt.Rows[0][0].ToString();

            NameValueCollection nv = new NameValueCollection();
            nv.Add("@RoleID", Session["Role"].ToString());
             dt = objCommon.GetDataTable("SP_GetRoleForAssignement", nv);
            repRoles.DataSource = dt;
            repRoles.DataBind();
          
        }
        private void BindData()
        {
            List<ReportChartDetails> lstReportChartDetails = new List<ReportChartDetails>();
            int i = 0;

            DateTime StartDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime EndDate = Convert.ToDateTime(txtToDate.Text);
            for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
            {
                ReportChartDetails reportdetails = new ReportChartDetails();
                i = i + 1;
                reportdetails.ID = i;
                reportdetails.DayofWorkDate = date.DayOfWeek.ToString();
                reportdetails.WorkDate = date;
                lstReportChartDetails.Add(reportdetails);
            }
            repChart.DataSource = lstReportChartDetails;
            repChart.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData();
            SavePreference();
        }

        protected void repChart_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ReportChartDetails dr = (ReportChartDetails)e.Item.DataItem;
            Literal ltScript = (Literal)e.Item.FindControl("ltScripts");
            HtmlControl chartDivid = (HtmlControl)e.Item.FindControl("divTask");
            BindChart(dr.WorkDate, chartDivid.ClientID, ltScript);
        }
        private void SavePreference()
        {
            NameValueCollection nv = new NameValueCollection();
            string strEmployeeIDs = string.Empty;
            foreach (RepeaterItem item in repRoles.Items)
            {
                CheckBox chkRole = (CheckBox)item.FindControl("chkRole");
                if (chkRole.Checked)
                {
                    strEmployeeIDs += ((HiddenField)item.FindControl("hdnValue")).Value + ",";
                }
            }
            nv.Add("@PreferredTaskDistributionID", strEmployeeIDs);
            nv.Add("@UserID", Session["LoginID"].ToString());
            objCommon.GetDataInsertORUpdate("AddPreferenceTaskDistribution", nv);
        }

        public DataTable GetChartData(string date)
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@WorkDate", date);
            string strEmployeeIDs = string.Empty;
            foreach (RepeaterItem item in repRoles.Items)
            {
                CheckBox chkRole = (CheckBox)item.FindControl("chkRole");
                if (chkRole.Checked)
                {
                    strEmployeeIDs += ((HiddenField)item.FindControl("hdnValue")).Value + ",";
                }
            }
            nv.Add("@RoleIDs", strEmployeeIDs);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            return objCommon.GetDataTable("SP_GetTaskDisctributionChartFilterData", nv);
        }
        private void BindChart(DateTime date, string chartDivid, Literal ltScript)
        {
            DataTable dsChartData = new DataTable();
            StringBuilder strScript = new StringBuilder();

            try
            {
                dsChartData = GetChartData(date.ToString("MM-dd-yyyy"));

                strScript.Append(@" <script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']}); </script>  
                      
                    <script type='text/javascript'>  
                     
                    function drawChart() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Task Distribution of Profiles', 'Hours / Day','TaskLimit'],");

                foreach (DataRow row in dsChartData.Rows)
                {
                    strScript.Append("['" + row[0].ToString() + "'," + row[1].ToString() + ",8],");
                }

                strScript.Remove(strScript.Length - 1, 1);
                strScript.Append("]);");

                strScript.Append(@" var options = {     
                                    chartArea: {
                                                    left: 150,
                                                    bottom: 70,
                                                },
                                                height: 300,
                                                bar: { groupWidth: 30 },
                                                legend: { position: 'none' },
                                                colors: ['#438644'],
                                                hAxis: { title: 'Task Distribution of Profiles', titleTextStyle: { fontStyle: 'bold' } },
                                                vAxis: { title: 'Hours/Day', },
                                                seriesType: 'bars',
                                                series: { 1: { type: 'line', color: '#FF0000', lineWidth: 1, lineDashStyle: [10, 2] } },
                                                fontSize: 14,          
                                    };   ");

                strScript.Append(@"var chart = new google.visualization.ColumnChart(document.getElementById('");
                strScript.Append(chartDivid);
                strScript.Append(@"'));           
                chart.draw(data, options);
            }
                            google.setOnLoadCallback(drawChart);
            ");
                strScript.Append(" </script>");

                ltScript.Text = strScript.ToString();
            }
            catch
            {
            }
            finally
            {
                dsChartData.Dispose();
                strScript.Clear();
            }
        }

        protected void repRoles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CheckBox chkRole = e.Item.FindControl("chkRole") as CheckBox;
            HiddenField hdnValue = e.Item.FindControl("hdnValue") as HiddenField;
            string[] subs = strPreference.Split(',');
            chkRole.Checked = subs.Contains(hdnValue.Value);
        }
    }
}
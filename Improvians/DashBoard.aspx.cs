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
    public partial class DashBoard1 : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFacility();
                BindData();
                BindPlantReadyAVG();
                SetLinkTrackTasks();
                ltrDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
                ltrDayofWeek.Text = DateTime.Now.DayOfWeek.ToString();
            }
        }
        public void BindData()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetHomeDetails", nv);
            if (dt != null && dt.Rows.Count > 0)
            {
                ltrGerminationRate.Text = Convert.ToString(dt.Rows[0]["Germination"]);
            }
            SetLink();

        }
        public void SetLink()
        {
            if (Session["Role"].ToString() == "1")
            {
                amytask.HRef = "MyTaskGrower.aspx";
            }
            if (Session["Role"].ToString() == "2")
            {
                amytask.HRef = "MyTaskGreenSupervisorFinal.aspx";
            }
            if (Session["Role"].ToString() == "3")
            {
                //   amytask.HRef = "MyTaskGreenOperatorFinal.aspx";
                amytask.HRef = "MyTaskSpray.aspx";


            }
            if (Session["Role"].ToString() == "5")
            {
                //  amytask.HRef = "MyTaskLogisticManager.aspx";
                // amytask.HRef = "MyTaskSiteMoveTeam.aspx";

                amytask.HRef = "MyTaskSpray.aspx";
            }
            if (Session["Role"].ToString() == "6")
            {
                amytask.HRef = "MyTaskShippingCoordinator.aspx";
            }
            if (Session["Role"].ToString() == "7")
            {
                //amytask.HRef = "SeedingPlanForm.aspx";
                amytask.HRef = "MyTaskSeedlinePlanner.aspx";
                 ddlFacility.Visible = false;
                //  JobReports.HRef = "ReportSeedlinePlanner.aspx";
            }
            if (Session["Role"].ToString() == "8")
            {
                amytask.HRef = "MyTaskSeedingTeam.aspx";
            }
            if (Session["Role"].ToString() == "9")
            {
                amytask.HRef = "MyTaskSeedLineOperator.aspx";
            }
            if (Session["Role"].ToString() == "10")
            {
                amytask.HRef = "MyTaskProductionPlanner.aspx";
                JobReports.HRef = "TrackTaskSeedlinePlanner.aspx";
               // ddlFacility.Visible = false;
            }
            if (Session["Role"].ToString() == "11")
            {
                amytask.HRef = "MyTaskSpray.aspx";
            }
            if (Session["Role"].ToString() == "12")
            {
                amytask.HRef = "MyTaskAssistantGrower.aspx";
            }
            if (Session["Role"].ToString() == "15")
            {
                amytask.HRef = "MyTaskHeadGrower.aspx";
            }
        }

        public void BindFacility()
        {
            BAL_Task objTask = new BAL_Task();
            DataSet ds = objTask.GetEmployeeByID(Convert.ToInt32(Session["LoginID"]));
            ddlFacility.DataSource = ds.Tables[1];
            ddlFacility.DataTextField = "FacilityName";
            ddlFacility.DataValueField = "FacilityName";
            ddlFacility.DataBind();
            //ddlFacility.Items.Insert(0, new ListItem("--Select--", "0"));
            if (Session["Facility"] != null && Session["Facility"].ToString() != string.Empty)
            {
                ddlFacility.SelectedValue = Session["Facility"].ToString();
            }
            else
            {
                Session["Facility"] = ddlFacility.SelectedValue;
            }

        }
        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Facility"] = ddlFacility.SelectedValue;
            var res = (Master.FindControl("r1") as Repeater);
            BindPlantReadyAVG();
            BindData();
            var lblCount = (Master.FindControl("lblNotificationCount") as Label);
            objCommon.GetAllNotifications(Session["LoginID"].ToString(), ddlFacility.SelectedValue, res, lblCount);
        }

        public void BindPlantReadyAVG()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            //nv.Add("@Mode", "11");
            //dt = objCommon.GetDataTable("GET_Common", nv);


            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetPlantReadyAVGRootQty", nv);
            if (dt != null & dt.Rows.Count > 0)
            {
                lblPlantReadyQuality.Text = dt.Rows[0]["PlantReadyAVG"].ToString();
            }
            else
            {
                lblPlantReadyQuality.Text = "00:00";
            }
        }


        public void SetLinkTrackTasks()
        {

            if (Session["Role"].ToString() == "7" || Session["Role"].ToString() == "10")
            {
                TrackTasks.HRef = "TrackTaskSeedlinePlanner.aspx";
                //  JobReports.HRef = "JobReports.aspx
                //  TrackTasks.HRef = "ReportSeedlinePlanner.aspx";
                JobReports.HRef = "ReportSeedlinePlanner.aspx";
            }

            if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "2" || Session["Role"].ToString() == "12")
            {

                CreateTask.HRef = "CreateTask.aspx";
                JobReports.HRef = "JobReports.aspx";
                TrackTasks.HRef = "ManageTaskJobReport.aspx";
            }
            else
            {


            }

            if (Session["Role"].ToString() == "5" || Session["Role"].ToString() == "3" || Session["Role"].ToString() == "11")
            {

                JobReports.HRef = "JobReports.aspx";
            }


        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<ChartDetails> GetTaskDisctributionChartData()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Evo"].ConnectionString;
                string strRoles = string.Empty;
                using (SqlCommand cmdPreference = new SqlCommand("SP_GetPreferenceTaskDistribution", conn))
                {
                    cmdPreference.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    cmdPreference.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["LoginID"].ToString());
                    da.SelectCommand = cmdPreference;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                        strRoles = dt.Rows[0][0].ToString();
                }
                using (SqlCommand cmd = new SqlCommand("SP_GetTaskDisctributionChartFilterData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    cmd.Parameters.AddWithValue("@WorkDate", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@LoginID", HttpContext.Current.Session["LoginID"].ToString());
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
                    conn.Close();
                    return lstChartDetails;
                }
            }
        }
    }
}
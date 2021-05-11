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
                JobReports.HRef = "ReportSeedlinePlanner.aspx";
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
                ddlFacility.Visible = false;
            }
            if (Session["Role"].ToString() == "11")
            {
                amytask.HRef = "MyTaskSpray.aspx";
            }
            if (Session["Role"].ToString() == "12")
            {
                amytask.HRef = "MyTaskAssistantGrower.aspx";
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
            var totalCount = 0;
            NameValueCollection nv = new NameValueCollection();
            DataTable dtSearch1 = new DataTable();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@facility", Session["Facility"].ToString());

            dtSearch1 = objCommon.GetDataTable("SP_GetAllNotifications", nv);

            if (dtSearch1 != null)
            {
                foreach (DataRow dr in dtSearch1.Rows)
                {
                    if ((bool)dr["IsViewed"] == false)
                    {
                        totalCount += 1;
                    }
                }

            }

            var lblCount = (Master.FindControl("lblNotificationCount") as Label);

            lblCount.Text = totalCount.ToString();

            res.DataSource = dtSearch1;
            res.DataBind();


        }

        public void BindPlantReadyAVG()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "11");
            dt = objCommon.GetDataTable("GET_Common", nv);

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
              //  JobReports.HRef = "JobReports.aspx";
            }

            if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "2" || Session["Role"].ToString() == "12")
            {

                CreateTask.HRef = "CreateTask.aspx";
                JobReports.HRef = "JobReports.aspx";
             
            }
            else
            {


            }
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<ChartDetails> GetTaskDisctributionChartData()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Evo"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("SP_GetTaskDisctributionChartData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    cmd.Parameters.AddWithValue("@WorkDate", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@LoginID", HttpContext.Current.Session["LoginID"].ToString());                    
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
                        strRoles = "2,3,5,11";
                    }
                    else 
                    {
                        strRoles = HttpContext.Current.Session["Role"].ToString();
                    }
                    cmd.Parameters.AddWithValue("@RoleIDs",strRoles) ;
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
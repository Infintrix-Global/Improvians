using Evo.Admin.BAL_Classes;
using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
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

            if (Session["Role"].ToString() == "7")
            {
                TrackTasks.HRef = "TrackTaskSeedlinePlanner.aspx";
            }

            if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "2" || Session["Role"].ToString() == "12")
            {
                CreateTask.HRef = "CreateTask.aspx";

            }
        }

    }
}
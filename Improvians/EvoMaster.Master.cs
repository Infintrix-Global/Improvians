using Evo.Admin.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class EvoMaster : System.Web.UI.MasterPage
    {
        General objGeneral = new General();
        CommonControl objCommon = new CommonControl();
        List<int> Operators = new List<int> { 3, 5, 11 };
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["LoginID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                lblEmpName.Text = Session["EmployeeName"].ToString();
                if (!string.IsNullOrEmpty(Session["Photo"].ToString()))
                {
                    imgprofilepic.ImageUrl = @"~\Admin\EmployeeProfile\" + Session["Photo"].ToString();
                }
            }

            String activepage = Request.RawUrl;
            if (activepage.Contains("Dash"))
            {
                dashlink.Attributes.Add("class", "active");
                lnkmytask.Attributes.Remove("class");
            }
            else
            {
                lblFacility.Text = Session["Facility"].ToString();
                dashlink.Attributes.Remove("class");
                lnkmytask.Attributes.Add("class", "active");
            }
            checkNotification(1);

        }

        protected void checkNotification(int n)
        {
            var totalCount = "";
            NameValueCollection nv = new NameValueCollection();
            DataTable dtSearch1 = new DataTable();
            string sqr = "";
            if (n == 2)
            {
                nv.Add("@uId", Session["LoginID"].ToString());

                var result = objCommon.GetDataExecuteScaler("SP_ClearAllNotification", nv);
            }

            sqr = "Select * FROM NotificationMaster WHERE IsViewed=0 AND UserID = '" + Session["LoginID"] + "'  order by ID desc";
            if (!string.IsNullOrEmpty(sqr))
            {
                dtSearch1 = objGeneral.GetDatasetByCommand(sqr);
            }
            if (dtSearch1 != null)
            {
                totalCount = dtSearch1.Rows.Count.ToString();
            }
            if (string.IsNullOrEmpty(totalCount))
            {
                totalCount = "0";
            }
            lblNotificationCount.Text = totalCount;

            r1.DataSource = dtSearch1;
            r1.DataBind();
        }

        protected void lnkmytask_Click(object sender, EventArgs e)
        {

            if (Session["Role"].ToString() == "1")
            {
                Response.Redirect("MyTaskGrower.aspx");
            }
            if (Session["Role"].ToString() == "2")
            {
                Response.Redirect("MyTaskGreenSupervisorFinal.aspx");
            }
            if (Session["Role"].ToString() == "3")
            {
                //  Response.Redirect("MyTaskGreenOperatorFinal.aspx");
                Response.Redirect("MyTaskSpray.aspx");
            }
            if (Session["Role"].ToString() == "5")
            {
                // Response.Redirect("MyTaskSiteMoveTeam.aspx");
                Response.Redirect("MyTaskSpray.aspx");
            }
            if (Session["Role"].ToString() == "6")
            {
                Response.Redirect("MyTaskShippingCoordinator.aspx");
            }
            if (Session["Role"].ToString() == "7")
            {
                Response.Redirect("SeedingPlanForm.aspx");
            }
            if (Session["Role"].ToString() == "8")
            {
                Response.Redirect("MyTaskSeedingTeam.aspx");
            }
            if (Session["Role"].ToString() == "9")
            {
                Response.Redirect("MyTaskSeedLineOperator.aspx");
            }
            if (Session["Role"].ToString() == "10")
            {
                Response.Redirect("MyTaskProductionPlanner.aspx");
            }
            if (Session["Role"].ToString() == "11")
            {
                Response.Redirect("MyTaskSpray.aspx");
            }

            if (Session["Role"].ToString() == "12")
            {
                Response.Redirect("MyTaskAssistantGrower.aspx");

            }

        }
        protected void lnkdashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void link_Click(object sender, EventArgs e)
        {
            NameValueCollection nv = new NameValueCollection();

            LinkButton link = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)link.NamingContainer;
            string id = ((Label)row.FindControl("lblID")).Text;
            nv.Add("@Nid", id);
            string TaskName = ((Label)row.FindControl("lblTaskName")).Text;

            var result = objCommon.GetDataExecuteScaler("SP_UpdateNotification", nv);

            if (TaskName != null)
            {
                if (Session["Role"].ToString() == "12")
                {
                    Response.Redirect(TaskName + "RequestForm.aspx");
                }
                else if (Operators.Contains(Convert.ToInt32(Session["Role"])))
                {
                    Response.Redirect(TaskName + "CompletionForm.aspx");
                }
                else
                {

                    Response.Redirect(TaskName + "AssignmentForm.aspx");


                }

            }
        }

        protected void clearNotification_Click(object sender, EventArgs e)
        {
            checkNotification(2);
        }

        //[WebMethod]
        //public static void updateNotification(int id)
        //{
        //    NameValueCollection nv = new NameValueCollection();

        //    nv.Add("@Nid", id.ToString());

        //    var result = objCommon.GetDataExecuteScaler("SP_UpdateNotification", nv);
        //}
    }
}
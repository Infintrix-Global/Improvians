using Evo.Admin.BAL_Classes;
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
    public partial class EvoMaster : System.Web.UI.MasterPage
    {
        General objGeneral = new General();
        CommonControl objCommon = new CommonControl();
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
                dashlink.Attributes.Remove("class");
                lnkmytask.Attributes.Add("class", "active");
            }
            checkNotification();

        }

        protected void checkNotification()
        {
            DataTable dtSearch1;
            string sqr = "Select * FROM NotificationMaster WHERE UserID = '" + Session["LoginID"]+"'";

            dtSearch1 = objGeneral.GetDatasetByCommand(sqr);
           
            lblNotificationCount.Text = dtSearch1.Rows.Count.ToString();
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

        protected void updateNotification(string id)
        {
            NameValueCollection nv = new NameValueCollection();
            
            nv.Add("@Nid", id);
          
            var result = objCommon.GetDataExecuteScaler("SP_UpdateNotification", nv);
        }

        protected void linkBtn_Click(object sender, EventArgs e)
        {
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Nid", "1");

            var result = objCommon.GetDataExecuteScaler("SP_UpdateNotification", nv);
        }
    }
}
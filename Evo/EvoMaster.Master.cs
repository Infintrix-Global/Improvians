﻿using Evo.Admin.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
                lblEmpName.Text= lblUser.Text= Session["EmployeeName"].ToString();
                if (!string.IsNullOrEmpty(Session["Photo"].ToString()))
                {
                    imgprofilepic.ImageUrl = @"~\EmployeeProfile\" + Session["Photo"].ToString();
                }
                if (Session["Role"].ToString() == "1")
                {
                    SiteMapPath1.SiteMapProvider = "SitemapGrower";
                    SiteMapPath1.DataBind();
                }
                else if (Session["Role"].ToString() == "2")
                {
                    SiteMapPath1.SiteMapProvider = "SitemapSupervisor";
                    SiteMapPath1.DataBind();
                }
                else if (Session["Role"].ToString() == "3" || Session["Role"].ToString() == "5" || Session["Role"].ToString() == "11")
                {
                    SiteMapPath1.SiteMapProvider = "SitemapOperator";
                    SiteMapPath1.DataBind();
                }
                else if (Session["Role"].ToString() == "12")
                {
                    SiteMapPath1.SiteMapProvider = "SitemapAssistantGrower";
                    SiteMapPath1.DataBind();
                }
                else if (Session["Role"].ToString() == "7")
                {
                    SiteMapPath1.SiteMapProvider = "SitemapSeedlinePlanner";
                    SiteMapPath1.DataBind();
                }
                else if (Session["Role"].ToString() == "10")
                {
                    SiteMapPath1.SiteMapProvider = "SitemapSeedlineSupervisor";
                    SiteMapPath1.DataBind();
                }
                else if (Session["Role"].ToString() == "13" || Session["Role"].ToString() == "14")
                {
                    SiteMapPath1.SiteMapProvider = "SitemapCustomer";
                    SiteMapPath1.DataBind();
                }
            }

            if (Session["Role"].ToString() == "5" || Session["Role"].ToString() == "3" || Session["Role"].ToString() == "11")
            {
                lnkmanagetask.Visible = false;
                lnkcreatetask.Visible = false;
            }
            else
            {
                lnkmanagetask.Visible = true;
                lnkcreatetask.Visible = true;
            }

            String activepage = Request.RawUrl.ToLower();
            if (activepage.Contains("jobreports"))
            {
                dashlink.Attributes.Add("class", "active");
                lnkmytask.Attributes.Remove("class");
                lnkcreatetask.Attributes.Remove("class");
                lnkmanagetask.Attributes.Remove("class");
            }
            else if (activepage.Contains("dashboard"))
            {

                divFacility.Visible = false;
                divSitemap.Visible = false;
            }
            else if (activepage.Contains("mytask"))
            {
                lnkmytask.Attributes.Add("class", "active");
                dashlink.Attributes.Remove("class");
                lnkcreatetask.Attributes.Remove("class");
                lnkmanagetask.Attributes.Remove("class");
            }
            else if (activepage.Contains("createtask"))
            {
                lnkcreatetask.Attributes.Add("class", "active");
                dashlink.Attributes.Remove("class");
                lnkmytask.Attributes.Remove("class");
                lnkmanagetask.Attributes.Remove("class");
            }
            else if (activepage.Contains("managetask"))
            {
                lnkmanagetask.Attributes.Add("class", "active");
                dashlink.Attributes.Remove("class");
                lnkcreatetask.Attributes.Remove("class");
                lnkmytask.Attributes.Remove("class");
            }
            if (Session["Role"].ToString() == "7")
            {
                lblFacility.Text = "";

            }
            else
            {
                lblFacility.Text = Session["Facility"].ToString();
            }
          //  lblFacility.Text = Session["Facility"].ToString();
            checkNotification(1);
        }

        protected void checkNotification(int n)
        {
            var totalCount = 0;
            NameValueCollection nv = new NameValueCollection();
            //  DataTable dtSearch1 = new DataTable();
            string sqr = "";
            if (n == 2)
            {
                nv.Add("@uId", Session["LoginID"].ToString());

                var result = objCommon.GetDataExecuteScaler("SP_ClearAllNotification", nv);
            }

            //sqr = "Select * FROM NotificationMaster WHERE IsDeleted=0 And IsViewed=0 AND UserID = '" + Session["LoginID"] + "'  order by ID desc";        

            objCommon.GetAllNotifications(Session["LoginID"].ToString(), lblFacility.Text == "" ? Session["Facility"].ToString() : lblFacility.Text, r1, lblNotificationCount);

            //sqr = "Select * FROM NotificationMaster WHERE IsDeleted=0 AND UserID = '" + Session["LoginID"] + "'  order by ID desc";
            //if (!string.IsNullOrEmpty(sqr))
            //{
            //    dtSearch1 = objGeneral.GetDatasetByCommand(sqr);
            //}
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
            string job = ((Label)row.FindControl("lblJobId")).Text;
            string benchLoc = ((Label)row.FindControl("lblBenchLoc")).Text;
            string taskKey = ((HiddenField)row.FindControl("taskKey")).Value;

            nv.Add("@Nid", id);
            string TaskName = ((Label)row.FindControl("lblTaskName")).Text;

            var result = objCommon.GetDataExecuteScaler("SP_UpdateNotification", nv);

            if (TaskName != null)
            {
                TaskName = TaskName.Replace(" ", "");
                if (Session["Role"].ToString() == "12" || Session["Role"].ToString() == "1")   // for grower and assistant grower
                {
                    if (TaskName == "PutAway")
                    {
                        if (Session["Role"].ToString() == "12")
                        {
                            Response.Redirect("MyTaskLogisticManager.aspx?jobId=" + job + "&benchLoc=" + benchLoc);
                        }
                        else
                        {
                            Response.Redirect("GrowerPutAwayForm.aspx?jobId=" + job + "&benchLoc=" + benchLoc);
                        }
                    }
                    if (TaskName == "Fertilizer")
                    {
                        Response.Redirect("FertilizerTaskReq.aspx?jobId=" + job + "&benchLoc=" + benchLoc + "&Tkey=" + taskKey);
                    }
                    if (TaskName == "CropHealthReport")
                    {
                        Response.Redirect("CropReportRequestForm.aspx?jobId=" + job + "&benchLoc=" + benchLoc);
                    }
                    else
                    {
                        Response.Redirect(TaskName + "RequestForm.aspx?jobId=" + job + "&benchLoc=" + benchLoc + "&Tkey=" + taskKey);
                    }

                }
                else if (Operators.Contains(Convert.ToInt32(Session["Role"])))
                {
                    switch (TaskName)
                    {
                        case "PutAway":
                            Response.Redirect("MyTaskShippingCoordinator.aspx?jobId=" + job + "&benchLoc=" + benchLoc + "&Tkey=" + taskKey);
                            break;
                        case "CropHealthReport":
                            Response.Redirect("CropReportRequestForm.aspx?jobId=" + job + "&benchLoc=" + benchLoc);
                            break;
                        case "Chemical":
                            Response.Redirect("ChemicalTaskRequest.aspx?benchLoc=" + benchLoc + "&Tkey=" + taskKey);
                            break;
                        case "Move":
                            Response.Redirect("MoveReqAsssignment.aspx?jobId=" + job + "&benchLoc=" + benchLoc);
                            break;
                        case "Fertilizer":
                            Response.Redirect("SprayTaskRequest.aspx?benchLoc=" + benchLoc + "&Tkey=" + taskKey);
                            break;
                        case "Irrigation":
                            Response.Redirect(TaskName + "CompletionForm.aspx?benchLoc=" + benchLoc + "&Tkey=" + taskKey);
                            break;
                        default:
                            Response.Redirect(TaskName + "CompletionForm.aspx?jobId=" + job + "&benchLoc=" + benchLoc + "&Tkey=" + taskKey);
                            break;
                    }

                }
                else
                {
                    switch (TaskName)
                    {
                        case "PutAway":
                            Response.Redirect("MyTaskLogisticManager.aspx?jobId=" + job + "&benchLoc=" + benchLoc + "&Tkey=" + taskKey);
                            break;
                        case "CropHealthReport":
                            Response.Redirect("CropReportRequestForm.aspx?jobId=" + job + "&benchLoc=" + benchLoc);
                            break;
                        case "Chemical":
                            Response.Redirect("ChemicalTaskRequest.aspx?benchLoc=" + benchLoc + "&Tkey=" + taskKey);
                            break;
                        case "Move":
                            Response.Redirect("MoveRequestForm.aspx?jobId=" + job + "&benchLoc=" + benchLoc);
                            break;
                        case "Fertilizer":
                            Response.Redirect("SprayTaskRequest.aspx?benchLoc=" + benchLoc + "&Tkey=" + taskKey);
                            break;
                        case "Irrigation":
                            Response.Redirect(TaskName + "AssignmentForm.aspx?benchLoc=" + benchLoc + "&Tkey=" + taskKey);
                            break;
                        default:
                            Response.Redirect(TaskName + "AssignmentForm.aspx?jobId=" + job + "&benchLoc=" + benchLoc + "&Tkey=" + taskKey);
                            break;
                    }
                }
            }
        }

        protected void clearNotification_Click(object sender, EventArgs e)
        {
            checkNotification(2);
        }

        protected void remoteNotificationLink_Click(object sender, EventArgs e)
        {
            NameValueCollection nv = new NameValueCollection();

            LinkButton link = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)link.NamingContainer;
            string id = ((Label)row.FindControl("lblID")).Text;
            nv.Add("@Nid", id);

            var result = objCommon.GetDataExecuteScaler("SP_ClearNotificationById", nv);
            checkNotification(1);

            //notificationDiv.Attributes.Add("class", "dropdown-menu dropdown-menu-left dropdown-menu-sm-right show");
            notificationDiv1.Attributes.Add("class", "dropdown alert__dropdown ml-auto dropdown-showonload");
            //DataTable dtSearch1 = new DataTable();
        }

        protected void r1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HtmlControl control = e.Item.FindControl("lblLogo") as HtmlControl;

            string Task = (e.Item.FindControl("lblTaskName") as Label).Text;
            Task = Task.Replace(" ", "");
            switch (Task)
            {
                case "CropHealthReport":
                    control.Attributes["class"] = "imgicon-crophealth";
                    break;
                case "Chemical":
                    control.Attributes["class"] = "imgicon-chemical";
                    break;
                case "Move":
                    control.Attributes["class"] = "imgicon-moverequest";
                    break;
                case "Fertilizer":
                    control.Attributes["class"] = "imgicon-fertilization";
                    break;

                case "GeneralTask":
                    control.Attributes["class"] = "imgicon-generaltask";
                    break;
                case "Dump":
                    control.Attributes["class"] = "imgicon-dumprequest";
                    break;
                case "Germination":
                    control.Attributes["class"] = "imgicon-germination";
                    break;

                case "PlantReady":
                    control.Attributes["class"] = "imgicon-plantready";
                    break;
                case "Irrigation":
                    control.Attributes["class"] = "imgicon-irrigation";
                    break;
                default:
                    control.Attributes["class"] = "imgicon-putaway";
                    break;
            }
        }

        protected void dashlink_Click(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() == "7" || Session["Role"].ToString() == "10")
            {
                Response.Redirect("ReportSeedlinePlanner.aspx");
            }

            if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "2" || Session["Role"].ToString() == "12" || Session["Role"].ToString() == "5" || Session["Role"].ToString() == "3" || Session["Role"].ToString() == "11")
            {

                Response.Redirect("JobReports.aspx");
            }
            else
            {

            }
        }

        protected void lnkmanagetask_Click(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() == "7" || Session["Role"].ToString() == "10")
            {
                Response.Redirect("TrackTaskSeedlinePlanner.aspx");
            }

            if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "2" || Session["Role"].ToString() == "12")
            {

                Response.Redirect("ManageTaskJobReport.aspx");
            }
            else
            {


            }
        }
    }



    //[WebMethod]
    //public static void updateNotification(int id)
    //{
    //    NameValueCollection nv = new NameValueCollection();

    //    nv.Add("@Nid", id.ToString());

    //    var result = objCommon.GetDataExecuteScaler("SP_UpdateNotification", nv);
    //}
}

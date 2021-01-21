﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class ImproviansMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {

                lblEmpName.Text = Session["EmployeeName"].ToString();
            }

            String activepage = Request.RawUrl;
            if (activepage.Contains("Dashboard"))
            {
                dashlink.Attributes.Add("class", "active");
                lnkmytask.Attributes.Remove("class");
            }
            else
            {
                dashlink.Attributes.Remove("class");
                lnkmytask.Attributes.Add("class", "active");
            }

        }

        protected void lnkmytask_Click(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() == "1")
            {
                Response.Redirect("MyTaskGrower.aspx");
            }
            if (Session["Role"].ToString() == "2")
            {
                Response.Redirect("MyTaskGreenSupervisor.aspx");
            }
            if (Session["Role"].ToString() == "3")
            {
                Response.Redirect("MyTaskGreenOperator.aspx");
            }
            if (Session["Role"].ToString() == "5")
            {
                Response.Redirect("MyTaskLogisticManager.aspx");
            }
            if (Session["Role"].ToString() == "6")
            {
                Response.Redirect("MyTaskShippingCoordinator.aspx");
            }
        }
        protected void lnkdashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
}
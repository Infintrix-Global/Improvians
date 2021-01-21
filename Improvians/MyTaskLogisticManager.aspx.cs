﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class MyTaskLogisticManager : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridGerm();

            }
        }

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("SP_GetGreenHouseLogisticTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Assign")
            {
                
                if (Session["Role"].ToString() == "5")
                {
                    Session["MoveID"] = e.CommandArgument.ToString();
                    Response.Redirect("~/MoveTaskAssignment.aspx");
                }
            }
            if (e.CommandName == "Select")
            {
                
                if (Session["Role"].ToString() == "5")
                {
                    Session["MoveID"] = e.CommandArgument.ToString();
                    Response.Redirect("~/MoveCompletionForm.aspx");
                }
            }
        }
    }
}
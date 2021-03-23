﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Evo
{
    public partial class GeneralTaskAssignment : System.Web.UI.Page
    {

        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Did"] != null)
                {
                    DId = Request.QueryString["Did"].ToString();
                }
                if (Request.QueryString["Chid"] != "0" && Request.QueryString["Chid"] != null)
                {
                    BindGridCropHealth(Convert.ToInt32(Request.QueryString["Chid"]));
                }

                BindTask();
                BindOperatorList();
            }
        }

        public void BindGridCropHealth(int Chid)
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Chid", Chid.ToString());
            dt1 = objCommon.GetDataTable("SP_GetCropHealthReportSelect", nv1);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                PanelCropHealth.Visible = true;
                gvCropHealth.DataSource = dt1;
                gvCropHealth.DataBind();

                lblCommment.Text = dt1.Rows[0]["CropHealthCommit"].ToString();
            }

        }

        private string wo
        {
            get
            {
                if (ViewState["wo"] != null)
                {
                    return (string)ViewState["wo"];
                }
                return "";
            }
            set
            {
                ViewState["wo"] = value;
            }
        }

        private string DId
        {
            get
            {
                if (ViewState["DId"] != null)
                {
                    return (string)ViewState["DId"];
                }
                return "";
            }
            set
            {
                ViewState["DId"] = value;
            }
        }


        public void BindOperatorList()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@RoleID", "3");
            ddlOperator.DataSource = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;
            ddlOperator.DataTextField = "EmployeeName";
            ddlOperator.DataValueField = "ID";
            ddlOperator.DataBind();
            ddlOperator.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindTask()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@DId", DId);
            dt = objCommon.GetDataTable("SP_GetSupervisorGeneralAssignTask", nv);
            gvTask.DataSource = dt;
            gvTask.DataBind();
           
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;


            NameValueCollection nv = new NameValueCollection();
            nv.Add("@OperatorID", ddlOperator.SelectedValue);
            nv.Add("@Comments", txtNotes.Text);
            nv.Add("@GeneralId", DId);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@QuantityOfTray", "");
            nv.Add("@GeneralTaskDate", "");


            result = objCommon.GetDataExecuteScaler("SP_AddGeneralTaskAssignment", nv);
            if (result > 0)
            {
                //lblmsg.Text = "Assignment Successful";
                clear();
                string message = "Assignment Successful";
                string url = "GeneralTaskAssignmentForm.aspx";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }
            else
            {
                lblmsg.Text = "Assignment Not Successful";
            }
        }

        public void clear()
        {
            ddlOperator.SelectedIndex = 0;
            txtNotes.Text = "";


        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/GeneralTaskAssignmentForm.aspx");
        }

        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTask.PageIndex = e.NewPageIndex;
            BindTask();
        }

        protected void gvTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTask.PageIndex = e.NewPageIndex;
            BindTask();
        }
    }
}
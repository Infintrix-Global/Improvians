﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class DumpRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                // BindFacility();
                BindGridPlantReady();
                BindSupervisorList();
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

        public void BindGridPlantReady()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@wo", "");
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            // nv.Add("@Mode", "7");
            // dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);



            if (Session["Role"].ToString() == "12")
            {
                dt = objCommon.GetDataTable("SP_GetPlantReadyRequestAssistantGrower", nv);
            }
            else
            {
                dt = objCommon.GetDataTable("SP_GetPlantReadyRequest", nv);
            }


            gvPlantReady.DataSource = dt;
            gvPlantReady.DataBind();


        }
        public void BindSupervisorList()
        {
            //NameValueCollection nv = new NameValueCollection();
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            //ddlSupervisor.DataTextField = "EmployeeName";
            //ddlSupervisor.DataValueField = "ID";
            //ddlSupervisor.DataBind();
            //ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            NameValueCollection nv = new NameValueCollection();
            if (Session["Role"].ToString() == "1")
            {
                ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlSupervisor.DataTextField = "EmployeeName";
                ddlSupervisor.DataValueField = "ID";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            if (Session["Role"].ToString() == "12")
            {
                ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlSupervisor.DataTextField = "EmployeeName";
                ddlSupervisor.DataValueField = "ID";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        public void Bindcname()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "8");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "cname";
            ddlCustomer.DataValueField = "cname";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));

        }


        public void BindJobCode()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "7");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        //public void BindFacility()
        //{

        //    DataTable dt = new DataTable();
        //    NameValueCollection nv = new NameValueCollection();

        //    nv.Add("@Mode", "9");
        //    dt = objCommon.GetDataTable("GET_Common", nv);
        //    ddlFacility.DataSource = dt;
        //    ddlFacility.DataTextField = "loc_seedline";
        //    ddlFacility.DataValueField = "loc_seedline";
        //    ddlFacility.DataBind();
        //    ddlFacility.Items.Insert(0, new ListItem("--Select--", "0"));

        //}
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady();
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady();
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady();
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();

            BindGridPlantReady();
        }
        protected void gvPlantReady_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                //userinput.Visible = true;
                //string rowIndex = e.CommandArgument.ToString();

                //wo = rowIndex;

                //DataTable dt = new DataTable();
                //NameValueCollection nv = new NameValueCollection();
                //nv.Add("@wo", wo);
                //nv.Add("@JobCode", ddlJobNo.SelectedValue);
                //nv.Add("@CustomerName", ddlCustomer.SelectedValue);
                //nv.Add("@Facility", ddlFacility.SelectedValue);
                //nv.Add("@Mode", "2");
                //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);

                //lblJobID.Text = dt.Rows[0]["jobcode"].ToString();


                userinput.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                lblJobID.Text = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();
                lblGrowerID.Text = gvPlantReady.DataKeys[rowIndex].Values[2].ToString();
                ddlSupervisor.Focus();

            }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);
            nv.Add("@GrowerPutAwayId", lblGrowerID.Text);
            // nv.Add("@WO",wo);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataInsertORUpdate("SP_AddPlantReadyRequest", nv);
            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                string message = "Assignment Successful";
                string url = "MyTaskGrower.aspx";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                // lblmsg.Text = "Assignment Successful";
                clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }
        }

        public void clear()
        {

            ddlSupervisor.SelectedIndex = 0;

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }

        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlantReady.PageIndex = e.NewPageIndex;
            BindGridPlantReady();
        }

        protected void btnManual_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PlantReadyReqManual.aspx");
        }
    }

}




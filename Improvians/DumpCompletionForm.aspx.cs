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
    public partial class DumpCompletionForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                BindFacility();
                BindGridGerm(0);

            }
        }

        private string JobCode
        {
            get
            {
                if (Request.QueryString["jobId"] != null)
                {
                    return Request.QueryString["jobId"].ToString();
                }
                return "";
            }
            set
            {
                // JobCode = Request.QueryString["jobId"].ToString();
                // JobCode = value;
            }
        }

        private string benchLoc
        {
            get
            {
                if (Request.QueryString["benchLoc"] != null)
                {
                    return Request.QueryString["benchLoc"].ToString();
                }
                return "";
            }
            set
            {
                // JobCode = Request.QueryString["jobId"].ToString();
                // JobCode = value;
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

        public void BindFacility()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "9");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlFacility.DataSource = dt;
            ddlFacility.DataTextField = "loc_seedline";
            ddlFacility.DataValueField = "loc_seedline";
            ddlFacility.DataBind();
            ddlFacility.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        public void BindGridGerm(int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //  nv.Add("@wo", "");
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", ddlFacility.SelectedValue);
            //  nv.Add("@Mode", "9");
            nv.Add("@LoginID", Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("SP_GetOperatorDumpTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

            //foreach (GridViewRow row in gvGerm.Rows)
            //{
            //    var checkJob = (row.FindControl("lbljobID") as Label).Text;
            //    if (checkJob == JobCode)
            //    {
            //        row.CssClass = "highlighted";
            //    }
            //}

            if (p != 1)
            {
                highlight();
            }
        }
        private void highlight()
        {
            var i = gvGerm.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvGerm.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                i--;
                if (checkJob == JobCode && checklocation == benchLoc)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check)
                {
                    gvGerm.PageIndex++;
                    gvGerm.DataBind();
                    highlight();
                }
            }
        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm(1);
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm(1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm(1);
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();
            BindFacility();
            BindGridGerm(1);
        }
        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string JobID = "";


            if (e.CommandName == "Select")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string PRAID = gvGerm.DataKeys[rowIndex].Values[0].ToString();
                string DRID = gvGerm.DataKeys[rowIndex].Values[1].ToString();
                // string PRAID = e.CommandArgument.ToString();
                //  Response.Redirect(String.Format("~/PlantReadyTaskCompletion.aspx?PRAID={0}", PRAID));
                Response.Redirect(String.Format("~/DumpTaskCompletion.aspx?Did={0}&Chid={1}&DrId={2}", PRAID, 0, DRID));

            }
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm(1);
        }


    }
}
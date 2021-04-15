﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.BAL_Classes;
using Evo.Bal;

namespace Evo
{
    public partial class ChemicalTaskRequest : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridSprayReq(0);
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





        public void BindGridSprayReq(int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetChemical_RequestDetailsNew", nv);
            gvSpray.DataSource = dt;
            gvSpray.DataBind();

            //foreach (GridViewRow row in gvSpray.Rows)
            //{
            //    var checkJob = (row.FindControl("lblGreenHouseID") as Label).Text;
            //    if (checkJob == benchLoc)
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
            var i = gvSpray.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvSpray.Rows)
            {
                //var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                i--;
                if (checklocation == benchLoc)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check)
                {
                    gvSpray.PageIndex++;
                    gvSpray.DataBind();
                    highlight();
                }
            }
        }


        protected void gvSpray_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string ChemicalCode = gvSpray.DataKeys[rowIndex].Values[0].ToString();
                Response.Redirect(String.Format("~/ChemicalTaskCompletion.aspx?ChemicalCode={0}", ChemicalCode));
                //userinput.Visible = true;



            }

            if (e.CommandName == "ViewDetails")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string ChemicalCode = gvSpray.DataKeys[rowIndex].Values[0].ToString();
                Response.Redirect(String.Format("~/ChemicalTaskViewDetails.aspx?ChemicalCode={0}", ChemicalCode));


            }
        }

        protected void gvSpray_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSpray.PageIndex = e.NewPageIndex;
            BindGridSprayReq(1);
        }
    }
}
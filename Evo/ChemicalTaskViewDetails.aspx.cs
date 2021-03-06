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
    public partial class ChemicalTaskViewDetails : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() == "13")
            {
                this.Page.MasterPageFile = "~/Customer/CustomerMaster.master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ChemicalCode"] != null)
                {
                    ChemicalCode = Request.QueryString["ChemicalCode"].ToString();
                }

                if (Request.QueryString["CCID"] != null)
                {
                    BindGridSprayCompletionDetails(Request.QueryString["CCID"].ToString());
                        PanlTaskComplition.Visible = true;
                }
                else
                {
                    PanlTaskComplition.Visible = false;
                }




                if (Request.QueryString["CCID"] != "0")
                {
                    BindGridSprayCompletionDetails(Request.QueryString["CCID"].ToString());
                    PanlTaskComplition.Visible = true;
                }
                else
                {
                    PanlTaskComplition.Visible = false;
                }

                if (Request.QueryString["TaskRequestKey"] != null)
                {
                    TaskRequestKey = Request.QueryString["TaskRequestKey"].ToString();

                }

                BindGridSprayReq();
                BindenchLocation();

                BindGridSprayDetails();
          

            }
        }


        public void BindenchLocation()
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();

            nv1.Add("@ChemicalCode", ChemicalCode);

            dt1 = objCommon.GetDataTable("SP_GetChemicalRequestSelectDetails", nv1);
            lblBenchLocation.Text = dt1.Rows[0]["GreenHouseID"].ToString();



        }

        private string TaskRequestKey
        {
            get
            {
                if (ViewState["TaskRequestKey"] != null)
                {
                    return (string)ViewState["TaskRequestKey"];
                }
                return "";
            }
            set
            {
                ViewState["TaskRequestKey"] = value;
            }
        }


        private string ChemicalCode
        {
            get
            {
                if (ViewState["ChemicalCode"] != null)
                {
                    return (string)ViewState["ChemicalCode"];
                }
                return "";
            }
            set
            {
                ViewState["ChemicalCode"] = value;
            }
        }


        private string TraysTotal
        {
            get
            {
                if (ViewState["TraysTotal"] != null)
                {
                    return (string)ViewState["TraysTotal"];
                }
                return "";
            }
            set
            {
                ViewState["TraysTotal"] = value;
            }
        }

        public void BindGridSprayReq()
        {
            string ChId = "";
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
         
            nv.Add("@ChemicalCode", ChemicalCode);
            dt = objCommon.GetDataTable("SP_GetChemicalBenchLocationView", nv);
            gvSpray.DataSource = dt;
            gvSpray.DataBind();
            lbljid.Text = dt.Rows[0]["jid"].ToString();
            ChId = dt.Rows[0]["CropHealth"].ToString();
            if (ChId == "")
            {
                ChId = "0";
            }
            else
            {
                ChId = ChId;
            }
            int tray = 0;
            foreach (GridViewRow row in gvSpray.Rows)
            {
                tray = tray + Convert.ToInt32((row.FindControl("lblTotTray") as Label).Text);
            }
            TraysTotal = tray.ToString();

            BindGridCropHealth(Convert.ToInt32(ChId));
            BindGridCropHealthImage(ChId);
        }
        public void BindGridCropHealthImage(string ChId)
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Chid", ChId);
            dt1 = objCommon.GetDataTable("SP_GetCropHealthReportImages", nv1);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                PanelCropHealth.Visible = true;
                CropePhotos.DataSource = dt1;
                CropePhotos.DataBind();


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

        public void BindGridSprayCompletionDetails(string ChemicalCompletionId)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@ChemicalCompletionId", ChemicalCompletionId);

            dt = objCommon.GetDataTable("SP_GetTaskAssignmenChemicalTaskCompletionView", nv);


            GridViewCompletion.DataSource = dt;
            GridViewCompletion.DataBind();
          //  lblComplitionUser.Text = dt.Rows[0]["EmployeeName"].ToString();
        }

        public void BindGridSprayDetails()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@TaskRequestKey", TaskRequestKey);


            if (Request.QueryString["CCID"] != "0")
            {
                nv.Add("@Login", "0");
            }
            else
            {
                nv.Add("@Login", Session["LoginID"].ToString());
            }

            dt = objCommon.GetDataTable("SP_GetChemicalRequestSelectDetailsView", nv);

            GridViewDetails.DataSource = dt;
            GridViewDetails.DataBind();
          
        }

        protected void gvSpray_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                //userinput.Visible = true;
                //int rowIndex = Convert.ToInt32(e.CommandArgument);
                //lblJobID.Text = gvSpray.DataKeys[rowIndex].Values[1].ToString();
                //lblGrowerID.Text = gvSpray.DataKeys[rowIndex].Values[2].ToString();

                //txtNotes.Focus();

            }


        }

        protected void gvSpray_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSpray.PageIndex = e.NewPageIndex;
            BindGridSprayReq();
        }

        protected void GridViewDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTray = (Label)e.Row.FindControl("lblTray");
                lblTray.Text = TraysTotal;
            }
        }
    }
}
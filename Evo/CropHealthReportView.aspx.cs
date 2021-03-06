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
    public partial class CropHealthReportView : System.Web.UI.Page
    {
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
                if (Request.QueryString["Did"] != null)
                {
                    Did = Request.QueryString["Did"].ToString();
                }
                if (Request.QueryString["TaskRequestKey"] != "0" && Request.QueryString["TaskRequestKey"] != null)
                {
                    TaskRequestKey = Request.QueryString["TaskRequestKey"].ToString();
                }

                BindPlantReady(Convert.ToInt32(Request.QueryString["DrId"]));
                BindViewDumpDetilas(Convert.ToInt32(Request.QueryString["DrId"]));
                BindGridCropHealth(Convert.ToInt32(Request.QueryString["Chid"]));
                BindGridCropHealthImage(Convert.ToInt32(Request.QueryString["Chid"]));
            }
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
               // lblCommment.Text = dt1.Rows[0]["CropHealthCommit"].ToString();
            }

        }

        public void BindGridCropHealthImage(int Chid)
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Chid", Chid.ToString());
            dt1 = objCommon.GetDataTable("SP_GetCropHealthReportImages", nv1);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                PanelCropHealth.Visible = true;
                CropePhotos.DataSource = dt1;
                CropePhotos.DataBind();
             

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

        private string Did
        {
            get
            {
                if (ViewState["Did"] != null)
                {
                    return (string)ViewState["Did"];
                }
                return "";
            }
            set
            {
                ViewState["Did"] = value;
            }
        }
        public void BindViewDumpDetilas(int RDid)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@TaskRequestKey", TaskRequestKey);
            dt = objCommon.GetDataTable("[SP_GetTaskAssignmenttCropHealthReportRequest]", nv);
            GridViewDumpView.DataSource = dt;
            GridViewDumpView.DataBind();

        }

        public void BindPlantReady(int CropR)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@wo", wo);
            //nv.Add("@JobCode", "");
            //nv.Add("@CustomerName", "");
            //nv.Add("@Facility", "");
            //nv.Add("@Mode", "11");
            nv.Add("@CropHealthReportId", CropR.ToString());
            nv.Add("@RoleId", Session["Role"].ToString());


            dt = objCommon.GetDataTable("SP_GetCropHealthReportTaskAssignment1", nv);
            gvCorpHelthDetails.DataSource = dt;
            gvCorpHelthDetails.DataBind();
          //  lbljid.Text = dt.Rows[0]["jid"].ToString();
           // lblChid.Text = dt.Rows[0]["chid"].ToString();
        }

      
    }
}
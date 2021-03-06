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
    public partial class IrrigationTaskViewDetails : System.Web.UI.Page
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
                if (Request.QueryString["IrrigationCode"] != null)
                {
                    IrrigationCode = Request.QueryString["IrrigationCode"].ToString();
                }
                if (Request.QueryString["ICID"] != null)
                {
                    BindGridSprayCompletionDetails(Request.QueryString["ICID"].ToString());
                    PanlTaskComplition.Visible = true;
                }
                else
                {
                    PanlTaskComplition.Visible = false;
                }


                if (Request.QueryString["ICID"] != "0")
                {
                    BindGridSprayCompletionDetails(Request.QueryString["ICID"].ToString());
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



                BindgvIrrigation();
                BindGridViewDetailsGerm();
                BindenchLocation();
               

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
        public void BindGridSprayCompletionDetails(string CompletionId)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@IrrigationTaskAssignmentId", CompletionId);

            dt = objCommon.GetDataTable("SP_GetTaskAssignmenIrrigationTaskCompletionView", nv);

            GridViewCompletion.DataSource = dt;
            GridViewCompletion.DataBind();
          

        }


        public void BindenchLocation()
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@IrrigationCode", IrrigationCode);
            dt1 = objCommon.GetDataTable("SP_GetIrrigationRequestGreenHouseDetails", nv1);
           // lblBenchLocation.Text = dt1.Rows[0]["GreenHouseID"].ToString();

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

        private string IrrigationCode
        {
            get
            {
                if (ViewState["IrrigationCode"] != null)
                {
                    return (string)ViewState["IrrigationCode"];
                }
                return "";
            }
            set
            {
                ViewState["IrrigationCode"] = value;
            }
        }


        public void BindGridViewDetailsGerm()
        {
            string ChId = "";
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@wo", "");
            //nv.Add("@JobCode", ddlJobNo.SelectedValue);
            //nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            //nv.Add("@Facility", ddlFacility.SelectedValue);
            //  nv.Add("@Jid", lbljid.Text);
            //nv.Add("@Mode", "6");
            //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            nv.Add("@TaskRequestKey", TaskRequestKey);


            if (Request.QueryString["ICID"] != "0")
            {
                nv.Add("@Login", "0");
            }
            else
            {
                nv.Add("@Login", Session["LoginID"].ToString());
            }

            dt = objCommon.GetDataTable("SP_GetOperatorIrrigationTaskViewDetailsStartView", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        
            //lblBenchLocation.Text = dt.Rows[0]["BenchLocation"].ToString();

            ChId = dt.Rows[0]["CropHealth"].ToString();
            if (ChId == "")
            {
                ChId = "0";
            }
            else
            {
                ChId = ChId;
            }
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

        public void BindgvIrrigation()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@wo", wo);
            //nv.Add("@JobCode", "");
            //nv.Add("@CustomerName", "");
            //nv.Add("@Facility", "");
            //nv.Add("@Mode", "5");
            //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            nv.Add("@IrrigationCode", IrrigationCode);
            nv.Add("@RoleId", Session["Role"].ToString());
           
           dt = objCommon.GetDataTable("SP_GetOperatorIrrigationTaskByIrrigationCode1", nv);
         
            gvIrrigation.DataSource = dt;
            gvIrrigation.DataBind();
      //      lbljid.Text = dt.Rows[0]["jid"].ToString();

        }

        protected void gvIrrigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIrrigation.PageIndex = e.NewPageIndex;
            BindgvIrrigation();
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
           
            if (Session["Role"].ToString() == "3")
            {
                Response.Redirect("~/IrrigationCompletionForm.aspx");
            }


        }


    }
}
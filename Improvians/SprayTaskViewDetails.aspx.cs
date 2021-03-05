using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Improvians.BAL_Classes;
using Improvians.Bal;

namespace Improvians
{
    public partial class SprayTaskViewDetails : System.Web.UI.Page
    {

        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["FertilizationCode"] != null)
                {
                    FertilizationCode = Request.QueryString["FertilizationCode"].ToString();
                }
                BindenchLocation();
               
                BindGridSprayDetails();
                BindGridSprayReq();

            }
        }


        public void BindenchLocation()
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            //nv1.Add("@JobCode", ddlJobNo.SelectedValue);
            //nv1.Add("@CustomerName", ddlCustomer.SelectedValue);
            //nv1.Add("@Facility", ddlFacility.SelectedValue);
            //nv1.Add("@LoginID", Session["LoginID"].ToString());
            //nv1.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            //nv1.Add("@FertilizationCode", lblFertilizationCode.Text);
            nv1.Add("@FertilizationCode", FertilizationCode);

            dt1 = objCommon.GetDataTable("SP_GetSprayRequestSelectDetails", nv1);
            lblBenchLocation.Text = dt1.Rows[0]["GreenHouseID"].ToString();



        }


        private string FertilizationCode
        {
            get
            {
                if (ViewState["FertilizationCode"] != null)
                {
                    return (string)ViewState["FertilizationCode"];
                }
                return "";
            }
            set
            {
                ViewState["FertilizationCode"] = value;
            }
        }


    
        public void BindGridSprayReq()
        {
            string ChId = "";
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode","0");
            nv.Add("@TraySize", "0");
            nv.Add("@itemno","0");
            nv.Add("@BenchLocation","0");
            nv.Add("@FertilizationCode", FertilizationCode);
            dt = objCommon.GetDataTable("SP_GetSprayRequestst", nv);
            gvSpray.DataSource = dt;
            gvSpray.DataBind();

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
            }
        }


        public void BindGridSprayDetails()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@FertilizationId", FertilizationCode);

            dt = objCommon.GetDataTable("SP_GetSprayRequestFerChemDetails", nv);

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



    
    }
}
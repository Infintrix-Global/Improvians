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
                BindJobCode();
                BindBenchLocation();
                Binditemno();
                BindTraySize();
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
            nv1.Add("@FertilizationId", FertilizationCode);

            dt1 = objCommon.GetDataTable("SP_GetSprayRequestGreenHouseDetails", nv1);
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

        public void Binditemno()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Mode", "14");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlItem.DataSource = dt;
            ddlItem.DataTextField = "itemno";
            ddlItem.DataValueField = "itemno";
            ddlItem.DataBind();
            ddlItem.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindTraySize()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Mode", "15");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddltraysize.DataSource = dt;
            ddltraysize.DataTextField = "TraySize";
            ddltraysize.DataValueField = "TraySize";
            ddltraysize.DataBind();
            ddltraysize.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindBenchLocation()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "10");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlBenchLocation.DataSource = dt;
            ddlBenchLocation.DataTextField = "GreenHouseID";
            ddlBenchLocation.DataValueField = "GreenHouseID";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindGridSprayReq()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@TraySize", ddltraysize.SelectedValue);
            nv.Add("@itemno", ddlItem.SelectedValue);
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@FertilizationCode", FertilizationCode);
            dt = objCommon.GetDataTable("SP_GetSprayRequestst", nv);
            gvSpray.DataSource = dt;
            gvSpray.DataBind();

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


        protected void btnReset_Click(object sender, EventArgs e)
        {
          
            Response.Redirect("~/MyTaskGrower.aspx");
        }


      

   



        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridSprayReq();
        }

        protected void ddlJobNo_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindGridSprayReq();
        }

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridSprayReq();
        }

        protected void ddltraysize_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridSprayReq();
        }

        protected void btnSearchRest_Click1(object sender, EventArgs e)
        {
            BindJobCode();
            BindBenchLocation();
            Binditemno();
            BindTraySize();
            BindGridSprayReq();
        }

        protected void btnBank_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SprayTaskRequest.aspx");
        }
    }
}
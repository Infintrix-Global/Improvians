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
    public partial class SprayTaskReq : System.Web.UI.Page
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
              //  Bindcname();
                //BindJobCode();
               // BindBenchLocation();
               // BindFacility();
                BindGridSprayReq();
            }
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


        //public void Bindcname()
        //{

        //    DataTable dt = new DataTable();
        //    NameValueCollection nv = new NameValueCollection();

        //    nv.Add("@Mode", "8");
        //    dt = objCommon.GetDataTable("GET_Common", nv);
        //    ddlCustomer.DataSource = dt;
        //    ddlCustomer.DataTextField = "cname";
        //    ddlCustomer.DataValueField = "cname";
        //    ddlCustomer.DataBind();
        //    ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));

        //}


        //public void BindJobCode()
        //{

        //    DataTable dt = new DataTable();
        //    NameValueCollection nv = new NameValueCollection();
        //    nv.Add("@Mode", "7");
        //    dt = objCommon.GetDataTable("GET_Common", nv);
        //    ddlJobNo.DataSource = dt;
        //    ddlJobNo.DataTextField = "Jobcode";
        //    ddlJobNo.DataValueField = "Jobcode";
        //    ddlJobNo.DataBind();
        //    ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        //}

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

        //public void BindBenchLocation()
        //{

        //    DataTable dt = new DataTable();
        //    NameValueCollection nv = new NameValueCollection();

        //    nv.Add("@Mode", "10");
        //    dt = objCommon.GetDataTable("GET_Common", nv);
        //    ddlBenchLocation.DataSource = dt;
        //    ddlBenchLocation.DataTextField = "GreenHouseID";
        //    ddlBenchLocation.DataValueField = "GreenHouseID";
        //    ddlBenchLocation.DataBind();
        //    ddlBenchLocation.Items.Insert(0, new ListItem("--Select--", "0"));

        //}

        public void BindGridSprayReq()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@JobCode", ddlJobNo.SelectedValue);
            //nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            //nv.Add("@Facility", ddlFacility.SelectedValue);
            //nv.Add("@LoginID", Session["LoginID"].ToString());
            //nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@FertilizationCode", FertilizationCode);
            dt = objCommon.GetDataTable("SP_GetSprayRequestst", nv);
            gvSpray.DataSource = dt;
            gvSpray.DataBind();

        }


        protected void gvSpray_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                lblJobID.Text = gvSpray.DataKeys[rowIndex].Values[1].ToString();
                lblGrowerID.Text = gvSpray.DataKeys[rowIndex].Values[2].ToString();

                txtNotes.Focus();

            }


        }

        protected void gvSpray_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSpray.PageIndex = e.NewPageIndex;
            BindGridSprayReq();
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }


        public void clear()
        {


            txtNotes.Text = "";

            txtSprayDate.Text = "";

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@SupervisorID", Session["LoginID"].ToString());

            nv.Add("@GrowerPutAwayId", lblGrowerID.Text);

            nv.Add("@SprayDate", txtSprayDate.Text.Trim());

            nv.Add("@Nots", txtNotes.Text.Trim());
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataInsertORUpdate("SP_AddSprayRequest", nv);
            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                string message = "Completion Successful";
                string url = "SprayTaskReq.aspx";
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('not Completion')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridSprayReq();
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridSprayReq();
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridSprayReq();
        }



        protected void ddlBenchLocation_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindGridSprayReq();
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();
            BindFacility();
            BindBenchLocation();
            BindGridSprayReq();
        }

        protected void gvSpray_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblFertilizationId = (Label)e.Row.FindControl("lblFertilizationId");
                GridView GridViewFields = e.Row.FindControl("GridViewDetails") as GridView;

                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@FertilizationId", lblFertilizationId.Text);
              
                dt = objCommon.GetDataTable("SP_GetSprayRequeststDetails", nv);

                GridViewFields.DataSource = dt;
                GridViewFields.DataBind();

            }
        }
    }
}
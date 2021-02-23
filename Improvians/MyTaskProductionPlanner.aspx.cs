using Improvians.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class MyTaskProductionPlanner : System.Web.UI.Page
    {
        Bal_SeedingPlan objSP = new Bal_SeedingPlan();
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                BindFacility();
                BindGridGerm();
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
        public void BindSeedlineLocation()
        {
            ddlSeedlineLocation.DataSource = objSP.GetSeedlineLocationProductionPlanner();
            ddlSeedlineLocation.DataTextField = "loc";
            ddlSeedlineLocation.DataValueField = "loc";
            ddlSeedlineLocation.DataBind();
            ddlSeedlineLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();
            BindFacility();
            BindGridGerm();
        }

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", ddlFacility.SelectedValue);
            dt = objCommon.GetDataTable("SP_GetProductionPlannerTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        private string WO
        {
            get
            {
                if (ViewState["WO"] != null)
                {
                    return (string)ViewState["WO"];
                }
                return "";
            }
            set
            {
                ViewState["WO"] = value;
            }
        }
        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string WO = e.CommandArgument.ToString();
                //  Session["WorkOrder"] = e.CommandArgument.ToString();
                // Response.Redirect("~/ProductionPlannerTaskCompletionForm.aspx");
                //  Response.Redirect("~/SeedLineCompletionFinal.aspx");

                Response.Redirect(String.Format("~/SeedLineCompletionFinal.aspx?WOId={0}", WO));

            }

            if (e.CommandName == "Assign")
            {
                string WO1 = e.CommandArgument.ToString();
                WO = WO1;
                BindSeedlineLocation();
              
            }
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@OperatorID", ddlOperator.SelectedValue);
            //nv.Add("@Notes", txtNotes.Text);
            //nv.Add("@JobID","");
            //nv.Add("@LoginID", Session["LoginID"].ToString());
            //nv.Add("@CropId","");
            //nv.Add("@UpdatedReadyDate", "");
            //nv.Add("@PlantExpirationDate","");
            //nv.Add("@RootQuality","");
            //nv.Add("@wo",wo);
            //nv.Add("@PlantHeight","");

            //nv.Add("@mode", "3");
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@loc_seedline", ddlSeedlineLocation.SelectedItem.Text);
       
            nv.Add("@WO",WO);
            result = objCommon.GetDataExecuteScaler("SP_UpdateProductionPlanner", nv);
            if (result > 0)
            {
                //lblmsg.Text = "Assignment Successful";
                clear();
                string message = "reassignment Successful";
                string url = "MyTaskProductionPlanner.aspx";
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
            ddlSeedlineLocation.SelectedIndex = 0;
        

        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskProductionPlanner.aspx");
        }
    }
}
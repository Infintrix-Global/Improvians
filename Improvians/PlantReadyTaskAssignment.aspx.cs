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
    public partial class PlantReadyTaskAssignment : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridGerm();
                BindOperatorList();
            }
        }

        public void BindOperatorList()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@RoleID", "3");
            ddlOperator.DataSource = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;
            ddlOperator.DataTextField = "EmployeeName";
            ddlOperator.DataValueField = "ID";
            ddlOperator.DataBind();
            ddlOperator.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobID", Session["JobID"].ToString());
            dt = objCommon.GetDataTable("SP_GetGreenHouseSupervisorAssignedJobByJobID", nv);
            gvPlantReady.DataSource = dt;
            gvPlantReady.DataBind();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@OperatorID", ddlOperator.SelectedValue);
            nv.Add("@Notes", txtNotes.Text);
            nv.Add("@JobID", Session["JobID"].ToString());
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@CropId", Session["LoginID"].ToString());
            nv.Add("@UpdatedReadyDate", "");
            nv.Add("@PlantExpirationDate","");
            nv.Add("@RootQuality","");

            nv.Add("@PlantHeight","");

            nv.Add("@mode", "3");

            result = objCommon.GetDataInsertORUpdate("SP_AddPlantReadyTaskAssignment", nv);
            if (result > 0)
            {
                //lblmsg.Text = "Assignment Successful";
                clear();
                string message = "Assignment Successful";
                string url = "MyTaskGreenSupervisor.aspx";
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
            ddlOperator.SelectedIndex = 0;
            txtNotes.Text = "";


        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGreenSupervisor.aspx");
        }

        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlantReady.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }
    }
}
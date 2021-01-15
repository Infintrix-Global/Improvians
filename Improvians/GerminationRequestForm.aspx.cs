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
    public partial class GerminationRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGridGerm();
                BindSupervisorList();
            }
        }

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
          
            dt = objCommon.GetDataTable("SP_GetGerminationRequest", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();
           
        }
        public void BindSupervisorList()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlSupervisor.DataTextField = "EmployeeName";
            ddlSupervisor.DataValueField = "ID";
            ddlSupervisor.DataBind();
            ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Select")
            {
                userinput.Visible = true;
                lblJobID.Text = e.CommandArgument.ToString();
            }
        }

      

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);
            nv.Add("@InspectionDueDate", txtDate.Text);
            nv.Add("@#TraysInspected", txtTrays.Text);
            nv.Add("@JobID", lblJobID.Text);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataInsertORUpdate("SP_AddGerminationRequest", nv);
            if(result>0)
            {
                lblmsg.Text = "Assignment Successful";
                clear();
            }
            else
            {
                lblmsg.Text = "Assignment Not Successful";
            }
        }

        public void clear()
        {
            txtDate.Text = "";
            txtTrays.Text = "";
            ddlSupervisor.SelectedIndex = 0;
            Response.Redirect("~/MyTaskGrower.aspx");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }
    }
}
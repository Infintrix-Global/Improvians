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
    public partial class GerminationRequestManual : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridGerm();
                BindSupervisorList();
            }
            }


        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@JobCode", ddlJobNo.SelectedValue);
            //nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            //nv.Add("@Facility", ddlFacility.SelectedValue);
            //nv.Add("@Week", radweek.SelectedValue);
            //nv.Add("@Status", radStatus.SelectedValue);
            dt = objCommon.GetDataTable("SP_GetGerminationManualRequest", nv);
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
            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvGerm.Rows[rowIndex];
                //string facName = (row.FindControl("lblFacility") as Label).Text;

                DataTable dt = new DataTable();
                //   NameValueCollection nv = new NameValueCollection();
                //  nv.Add("@FacilityName", facName);
                //  dt = objCommon.GetDataTable("SP_GetSupervisorNameByFacilityID", nv);
                lblJobID.Text = (row.FindControl("lbljobID") as Label).Text;
                 lblGrowerID.Text = (row.FindControl("lblGrowerID") as Label).Text;
                lblfacsupervisor.InnerText = "Green House Supervisor"; //+ facName;
                                                                       // lblSupervisorID.Text = dt.Rows[0]["ID"].ToString();
                                                                       //lblSupervisorName.Text = dt.Rows[0]["EmployeeName"].ToString();
                txtDate.Focus();
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
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);
            nv.Add("@InspectionDueDate", txtDate.Text);
            nv.Add("@#TraysInspected", txtTrays.Text);
            nv.Add("@GrowerID", lblGrowerID.Text);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataInsertORUpdate("SP_AddGerminationRequestManual", nv);
            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                string message = "Assignment Successful";
                string url = "MyTaskGrower.aspx";
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }
        }

        public void clear()
        {
            txtDate.Text = "";
            txtTrays.Text = "";
            //lblSupervisorID.Text = "";
            // lblSupervisorName.Text = "";
            lblfacsupervisor.InnerText = "";
            ddlSupervisor.SelectedIndex = 0;

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }
    }
}
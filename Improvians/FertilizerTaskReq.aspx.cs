using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Improvians.BAL_Classes;

namespace Improvians
{
    public partial class FertilizerTaskReq : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridFerReq();
                BindSupervisor();
                BindFertilizer();
                BindUnit();
            }
        }
        public void BindGridFerReq()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            dt = objCommon.GetDataTable("SP_GetFertilizerRequest", nv);
            gvFer.DataSource = dt;
            gvFer.DataBind();
          
        }

        public void BindSupervisor()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@RoleID", "2");
            ddlsupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;
            ddlsupervisor.DataTextField = "EmployeeName";
            ddlsupervisor.DataValueField = "ID";
            ddlsupervisor.DataBind();
            ddlsupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void btnAddTray_Click(object sender, EventArgs e)
        {

            try
            {
                if (Convert.ToDouble(txtTrays.Text) <= Convert.ToDouble(lblUnMovedTrays.Text))
                {
                    lblerrmsg.Text = "";
                    dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text,txtQty.Text,ddlUnit.SelectedItem.Text,txtTrays.Text,txtSQFT.Text);
                    gvFerDetails.DataSource = dtTrays;
                    gvFerDetails.DataBind();
                    lblUnMovedTrays.Text = (Convert.ToInt32(lblUnMovedTrays.Text) - Convert.ToInt32(txtTrays.Text)).ToString();
                    txtTrays.Text = "";
                    ddlFertilizer.SelectedIndex = 0;
                    ddlUnit.SelectedIndex = 0;
                    txtQty.Text = "";
                    txtSQFT.Text = "";
                }
                else
                {

                    lblerrmsg.Text = "Number of Trays exceed Remaining trays";

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvFerDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string tray = (gvFerDetails.Rows[e.RowIndex].FindControl("lblTray") as Label).Text;
                lblUnMovedTrays.Text = ((Convert.ToInt32(lblUnMovedTrays.Text) + Convert.ToInt32(tray)).ToString());
                dtTrays.Rows.RemoveAt(e.RowIndex);
                gvFerDetails.DataSource = dtTrays;
                gvFerDetails.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvFer_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvFer.Rows[rowIndex];
                lblUnMovedTrays.Text = (row.FindControl("lblTotTray") as Label).Text;
                lblJobID.Text = (row.FindControl("lblID") as Label).Text;
                ddlsupervisor.Focus();
            }
        }

        protected void gvFer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@SupervisorID", ddlsupervisor.SelectedValue);
            nv.Add("@Type", radtype.SelectedValue);
            nv.Add("@JobID", lblJobID.Text);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataInsertORUpdate("SP_AddFertilizerRequest", nv);
            if (result > 0)
            {
                objTask.AddFertilizerRequestDetails(dtTrays,result.ToString());
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
                Clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {

        }

        protected void radtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(radtype.SelectedValue=="Fertilizer")
            {
                lbltype.Text = "Fertilizer";
                BindFertilizer();
            }
            else if (radtype.SelectedValue == "Chemical")
            {
                lbltype.Text = "Chemical";
                BindChemical();
            }
        }

        public void BindChemical()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlFertilizer.DataSource = objNav.GetDataTable("SP_GetChemicalList", nv); ;
            ddlFertilizer.DataTextField = "Name";
            ddlFertilizer.DataValueField = "No_";
            ddlFertilizer.DataBind();
            ddlFertilizer.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindFertilizer()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlFertilizer.DataSource = objNav.GetDataTable("SP_GetFertilizerList", nv); ;
            ddlFertilizer.DataTextField = "Name";
            ddlFertilizer.DataValueField = "No_";
            ddlFertilizer.DataBind();
            ddlFertilizer.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindUnit()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlUnit.DataSource = objNav.GetDataTable("SP_GetUnitList", nv); ;
            ddlUnit.DataTextField = "Description";
            ddlUnit.DataValueField = "Code";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }
    }
}
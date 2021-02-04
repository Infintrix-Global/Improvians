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
    public partial class SeedLineTaskCompletion : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                BindGridSeedLine();
              
            }
        }

        public void BindGridSeedLine()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobID", Session["JobID"].ToString());
            dt = objCommon.GetDataTable("SP_GetSeedLineCompletionByJobID", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

            txtRequestedTrays.Text = dt.Rows[0]["#TraysSeeded"].ToString();
            lblTraySize.Text= dt.Rows[0]["SeedLots"].ToString();
            lblID.Text = dt.Rows[0]["PTCID"].ToString();
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@PTCID", dt.Rows[0]["PTCID"].ToString());
            dt1 = objCommon.GetDataTable("SP_GetPTCSeedMapByPTCID", nv1);
            gvDetails.DataSource = dt1;
            gvDetails.DataBind();
        }

        protected void radJobCompletion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radJobCompletion.SelectedValue == "Full")
            {
                txtTrays.Text = "";
                txtTrays.Visible = false;
            }
            else
            {
                
                txtTrays.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@ConfirmTraySize", radtraysize.SelectedValue);
            nv.Add("@ActualTraySeeded", txtActualTrays.Text);
            nv.Add("@JobCompletion", radJobCompletion.SelectedValue);
            if(radJobCompletion.SelectedValue=="Full")
            {
                nv.Add("@CompletedTrays", "");
            }
            else
            {
                nv.Add("@CompletedTrays", txtTrays.Text);
            }
          
            nv.Add("@SeededDate", txtSeededDate.Text);
            nv.Add("@PTCID", lblID.Text);
            if(chkSeedReturn.Checked)
            {
                nv.Add("@IsSeedReturnComplete", "Yes");
            }
            else
            {
                nv.Add("@IsSeedReturnComplete", "No");
            }
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataInsertORUpdate("SP_UpdateSeedLineTaskCompletion", nv);
            if (result > 0)
            {
                foreach (GridViewRow row in gvDetails.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                       
                            string ID = (row.Cells[0].FindControl("lblID") as Label).Text;
                            string ActualTray = (row.Cells[1].FindControl("txtActualTray") as TextBox).Text;
                            string SeedNo= (row.Cells[2].FindControl("lblSeed") as Label).Text;
                            string Type = (row.Cells[3].FindControl("ddlType") as DropDownList).Text;
                            string Partial = (row.Cells[4].FindControl("txtPartial") as TextBox).Text;
                            
                            objTask.UpdatePTCSeedAllocation(ID, ActualTray, SeedNo, Type, Partial);

                        
                    }
                }
                Clear();
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                string message = "SeedLine Completion Successful";
                string url = "MyTaskSeedingTeam.aspx";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                // Response.Redirect("MyTaskProductionPlanner.aspx");

            }
        }

            protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtSeededDate.Text = "";
            chkSeedReturn.Checked = false;
            radJobCompletion.SelectedValue = "Full";
            txtTrays.Text = "";

        }

        protected void txtActualTray_TextChanged(object sender, EventArgs e)
        {
            txtActualTrays.Text = "0";
            foreach (GridViewRow row in gvDetails.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtActual = (row.Cells[1].FindControl("txtActualTray") as TextBox);
                    if (txtActual.Text != "")
                    {

                        (row.Cells[2].FindControl("lblSeed") as Label).Text = (Convert.ToInt32(txtActual.Text) * Convert.ToInt32(lblTraySize.Text)).ToString();
                        txtActualTrays.Text = (Convert.ToInt32(txtActual.Text) + Convert.ToInt32(txtActualTrays.Text)).ToString();
                       
                    }
                }
            }
        }
    }
}
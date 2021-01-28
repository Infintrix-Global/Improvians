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
    public partial class ProductionPlannerTaskCompletionForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFacility();
                BindSeedLineFacility();
                BindGridProduction();
                BindGridSeedLot();
            }
        }

        public void BindGridSeedLot()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@mode", "6");
            dt = objCommon.GetDataTable("GET_Common", nv);
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
            
        }

        public void BindGridProduction()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobID", Session["JobID"].ToString());
            dt = objCommon.GetDataTable("SP_GetProductionPlannerTaskByJobID", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();
            if (dt != null && dt.Rows.Count > 0)
            {
                lblJobID.Text = dt.Rows[0]["JobID"].ToString();
                lblTrays.Text= dt.Rows[0]["#Tray"].ToString();
                lblSeedRequired.Text = ((Convert.ToInt32(dt.Rows[0]["#Tray"].ToString())) * (Convert.ToInt32(dt.Rows[0]["SeedLots"].ToString()))).ToString();
            }
        }

        public void BindSeedLineFacility()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@mode", "5");
            ddlSeedlineFacility.DataSource = objCommon.GetDataTable("GET_Common", nv); ;
            ddlSeedlineFacility.DataTextField = "SeedLineFacilityName";
            ddlSeedlineFacility.DataValueField = "SeedLineFacilityID";
            ddlSeedlineFacility.DataBind();
            ddlSeedlineFacility.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindFacility()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@mode", "4");
            ddlLocation.DataSource = objCommon.GetDataTable("GET_Common", nv); ;
            ddlLocation.DataTextField = "FacilityName";
            ddlLocation.DataValueField = "FacilityID";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlLocation.SelectedIndex != 0)
            //{
            //    NameValueCollection nv = new NameValueCollection();
            //    nv.Add("@FacilityID", ddlLocation.SelectedValue);
            //    ddlBenchLocation.DataSource = objCommon.GetDataTable("SP_GetGreenhouseByFacility", nv); ;
            //    ddlBenchLocation.DataTextField = "GreenHouseName";
            //    ddlBenchLocation.DataValueField = "GreenHouseID";
            //    ddlBenchLocation.DataBind();
            //    ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
            //}
        }

        protected void chkBenchLocation_CheckedChanged(object sender, EventArgs e)
        {
            //if(chkBenchLocation.Checked)
            //{
            //    ddlBenchLocation.Visible = false;
            //}
            //else
            //{
            //    ddlBenchLocation.Visible = true;
            //}
        }

        protected void radOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(radOrder.SelectedValue== "Complete")
                {
                txtTrays.Text= Convert.ToInt32(lblTrays.Text).ToString();
                txtTrays.Enabled = false;
            }
            else
            {
                txtTrays.Enabled = true;
                txtTrays.Text = "";
               // txtTrays.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@SeedingDueDate", txtSeedingDueDate.Text);
            nv.Add("@SeedLineFacility", ddlSeedlineFacility.SelectedValue);
            if(radOrder.SelectedValue == "Complete")
            {
                nv.Add("@#TraysSeeded", lblTrays.Text);
            }
            else
            {
                nv.Add("@#TraysSeeded", txtTrays.Text);
            }
         
            nv.Add("@PutAwayFacility", ddlLocation.SelectedValue);
            //if (chkBenchLocation.Checked)
            //{
            //    nv.Add("@PutawayBenchLocation","" );
            //}
            //else
            //{
            //    nv.Add("@PutawayBenchLocation", ddlBenchLocation.SelectedValue);
            //}
            nv.Add("@OrderType", radOrder.SelectedValue);
            nv.Add("@SeedsAllocated", txtSeedsAllocated.Text);
            nv.Add("@JobID", lblJobID.Text);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataExecuteScaler("SP_AddProductionPlannerCompletion", nv);
            if (result > 0)
            {
                foreach (GridViewRow row in gvDetails.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[2].FindControl("chkselect") as CheckBox);
                        if (chkRow.Checked)
                        {
                            string ID = (row.Cells[0].FindControl("lblID") as Label).Text;
                            objTask.AddPTCSeedAllocation(result.ToString(), ID);

                        }
                    }
                }
                Clear();
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                string message = "Seedline Completion Successful";
                string url = "MyTaskProductionPlanner.aspx";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

               // Response.Redirect("MyTaskProductionPlanner.aspx");
                
            }
       
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Production Completion not Successful')", true);
                //  lblmsg.Text = "Production Completion Not Successful";
            }
        }

        protected void chkselect_CheckedChanged(object sender, EventArgs e)
        {
            txtSeedsAllocated.Text = "0";
            foreach (GridViewRow row in gvDetails.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[2].FindControl("chkselect") as CheckBox);
                    if (chkRow.Checked)
                    {
                      
                        string lotseed = (row.Cells[1].FindControl("lblSeed") as Label).Text;
                        txtSeedsAllocated.Text = (Convert.ToInt32(txtSeedsAllocated.Text)+Convert.ToInt32(lotseed)).ToString();
                        if(Convert.ToDouble(txtSeedsAllocated.Text)>=Convert.ToDouble(lblSeedRequired.Text))
                        {
                            txtSeedsAllocated.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            txtSeedsAllocated.ForeColor = System.Drawing.Color.Black;
                        }
                    }
                }
            }
           
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            ddlSeedlineFacility.SelectedIndex = 0;
            ddlLocation.SelectedIndex = 0;
            //ddlBenchLocation.SelectedIndex = 0;
            //chkBenchLocation.Checked = false;
            //ddlBenchLocation.Visible = true;
            txtSeedingDueDate.Text = "";
            txtTrays.Text = "";
            txtSeedsAllocated.Text = "";
        }
    }
}
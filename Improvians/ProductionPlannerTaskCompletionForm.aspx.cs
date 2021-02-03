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
        public static DataTable dtTrays = new DataTable()
        { Columns = { "SeedLot","SeedID", "#ActualTray", "#Seed", "Type", "LeftOver" } };
        CommonControl objCommon = new CommonControl();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindFacility();
               // BindSeedLineFacility();
                BindGridProduction();
               BindSeedLot();
            }
        }

       

        public void BindGridProduction()
        {
            dtTrays.Clear();
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@WorkOrder", Session["WorkOrder"].ToString());
            dt = objCommon.GetDataTable("SP_GetProductionPlannerTaskByWorkOrder", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();
            if (dt != null && dt.Rows.Count > 0)
            {
                lblJobID.Text = dt.Rows[0]["jobcode"].ToString();
                txtRequestedTrays.Text = dt.Rows[0]["trays_plan"].ToString();
                lblTrays.Text= dt.Rows[0]["trays_plan"].ToString();
                lblTraySize.Text = dt.Rows[0]["TraySize"].ToString();
                lblSeedRequired.Text = ((Convert.ToInt32(dt.Rows[0]["trays_plan"].ToString())) * (Convert.ToInt32(dt.Rows[0]["TraySize"].ToString()))).ToString();
                txtActualTraysNo.Text = "0";
            }
        }

        public void BindSeedLot()
        {

                NameValueCollection nv = new NameValueCollection();
               nv.Add("@mode", "6");
                ddlSeedLot.DataSource = objCommon.GetDataTable("GET_Common", nv); ;
            ddlSeedLot.DataTextField = "SeedLotName";
            ddlSeedLot.DataValueField = "ID";
            ddlSeedLot.DataBind();
            ddlSeedLot.Items.Insert(0, new ListItem("--- Select ---", "0"));
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

        protected void radJobCompletion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radJobCompletion.SelectedValue == "Full")
            {
                txtCompletedTrays.Text = "";
                txtCompletedTrays.Visible = false;
            }
            else
            {

                txtCompletedTrays.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@ConfirmTraySize", radtraysize.SelectedValue);
            nv.Add("@SeedingDueDate", txtSeedingDueDate.Text);
            //   nv.Add("@SeedLineFacility", ddlSeedlineFacility.SelectedValue);
            nv.Add("@OrderType", radOrder.SelectedValue);
            if (radOrder.SelectedValue == "Complete")
            {
                nv.Add("@#TraysSeeded", lblTrays.Text);
            }
            else
            {
                nv.Add("@#TraysSeeded", txtTrays.Text);
            }

            nv.Add("@ActualTraySeeded", txtActualTraysNo.Text);
            nv.Add("@JobCompletion", radJobCompletion.SelectedValue);
            if (radJobCompletion.SelectedValue == "Full")
            {
                nv.Add("@CompletedTrays", "");
            }
            else
            {
                nv.Add("@CompletedTrays", txtTrays.Text);
            }
            if (chkSeedReturn.Checked)
            {
                nv.Add("@IsSeedReturnComplete", "Yes");
            }
            else
            {
                nv.Add("@IsSeedReturnComplete", "No");
            }

            //   nv.Add("@SeedsAllocated", txtSeedsAllocated.Text);
            nv.Add("@JobID", lblJobID.Text);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@WorkOrder", Session["WorkOrder"].ToString());
            result = objCommon.GetDataExecuteScaler("SP_AddSeedLineTaskCompletion", nv);
            if (result > 0)
            {
                foreach (GridViewRow row in gvDetails.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                       
                            string ID = (row.Cells[0].FindControl("lblID") as Label).Text;
                            string ActualTray = (row.Cells[1].FindControl("lblactualseed") as Label).Text;
                            string SeedNo = (row.Cells[2].FindControl("lblSeed") as Label).Text;
                            string Type = (row.Cells[3].FindControl("lbltype") as Label).Text;
                            string Partial = (row.Cells[4].FindControl("lblPartial") as Label).Text;

                            objTask.AddPTCSeedAllocation(result.ToString(), ID,ActualTray,SeedNo,Type,Partial);

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

     
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
           // ddlSeedlineFacility.SelectedIndex = 0;
           // ddlLocation.SelectedIndex = 0;
            //ddlBenchLocation.SelectedIndex = 0;
            //chkBenchLocation.Checked = false;
            //ddlBenchLocation.Visible = true;
            txtSeedingDueDate.Text = "";
            txtTrays.Text = "";
            dtTrays.Clear();
           // txtSeedsAllocated.Text = "";
        }

        protected void btnAddTray_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtRequestedTrays.Text) >= Convert.ToDouble(txtActualTraysNo.Text))
                {
                    txtActualTraysNo.Text = (Convert.ToInt32(txtActualTray.Text) + Convert.ToInt32(txtActualTraysNo.Text)).ToString();
                    lblerrmsg.Text = "";
                    dtTrays.Rows.Add(ddlSeedLot.SelectedItem.Text,ddlSeedLot.SelectedValue, txtActualTray.Text, (Convert.ToInt32(txtActualTray.Text) * Convert.ToInt32(lblTraySize.Text)).ToString(), ddlType.SelectedItem.Text,txtPartial.Text);
                    gvDetails.DataSource = dtTrays;
                    gvDetails.DataBind();
                   // lblUnmovedTrays.Text = (Convert.ToInt32(lblUnmovedTrays.Text) - Convert.ToInt32(txtTrays.Text)).ToString();
                    txtActualTray.Text = "";
                    ddlSeedLot.SelectedIndex = 0;
                    ddlType.SelectedIndex = 0;
                    txtPartial.Text = "";
                }
                else
                {

                    lblerrmsg.Text = "Number of Trays exceed Requested trays";

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using Improvians.BAL_Classes;

namespace Improvians
{
    public partial class SeedLineCompletionFinal : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        //{ Columns = { "SeedLot", "SeedID", "#ActualTray", "#Seed", "Type", "LeftOver" } };
        { Columns = { "SeedLot", "SeedID","NoOfSeed" } };
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
                txtSeedingDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
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
                lbltraysizecon.Text = dt.Rows[0]["TraySize"].ToString();
                lblTrays.Text = dt.Rows[0]["trays_plan"].ToString();
                lblTraySize.Text = dt.Rows[0]["TraySize"].ToString();
                lblSeedRequired.Text = ((Convert.ToInt32(dt.Rows[0]["trays_plan"].ToString())) * (Convert.ToInt32(dt.Rows[0]["TraySize"].ToString()))).ToString();
                txtActualTraysNo.Text = "0";
                txtTrays.Text = Convert.ToInt32(lblTrays.Text).ToString();
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
            if (radOrder.SelectedValue == "Complete")
            {
                txtTrays.Text = Convert.ToInt32(lblTrays.Text).ToString();
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
            nv.Add("@SeedingDate", txtSeedingDate.Text);
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
                        string ActualTray = (row.Cells[2].FindControl("txtActualTray") as TextBox).Text;
                        string SeedNo = (row.Cells[3].FindControl("lblSeed") as Label).Text;
                        string Type = (row.Cells[4].FindControl("ddlType") as DropDownList).Text;
                        string Partial = (row.Cells[5].FindControl("txtPartial") as TextBox).Text;

                        objTask.AddPTCSeedAllocation(result.ToString(), ID, ActualTray, SeedNo, Type, Partial);

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
            txtSeedingDate.Text = "";
            txtTrays.Text = "";
            dtTrays.Clear();
            // txtSeedsAllocated.Text = "";
        }

        protected void btnAddTray_Click(object sender, EventArgs e)
        {
            try
            {
                BindGridDetails();
                //if (Convert.ToDouble(txtRequestedTrays.Text) >= Convert.ToDouble(txtActualTraysNo.Text))
                //{
                //  txtActualTraysNo.Text = (Convert.ToInt32(txtActualTray.Text) + Convert.ToInt32(txtActualTraysNo.Text)).ToString();
                // lblerrmsg.Text = "";

                //DataTable dtSeed = objTask.GetSeedNoBySeedLotID(ddlSeedLot.SelectedValue);
                //dtTrays.Rows.Add(ddlSeedLot.SelectedItem.Text, ddlSeedLot.SelectedValue,dtSeed.Rows[0]["NoOFSeed"].ToString());
                //    gvDetails.DataSource = dtTrays;
                //    gvDetails.DataBind();
                //ddlSeedLot.SelectedIndex = 0;


                // lblUnmovedTrays.Text = (Convert.ToInt32(lblUnmovedTrays.Text) - Convert.ToInt32(txtTrays.Text)).ToString();
                // txtActualTray.Text = "";
                //  ddlType.SelectedIndex = 0;
                //  txtPartial.Text = "";
                //}
                //else
                //{
                //    lblerrmsg.Text = "Number of Trays exceed Requested trays";
                //}
            }
            catch (Exception ex)
            {

            }
        }

        public void BindGridDetails()
        {
            List<SeedLineTrayDetails> objinvoice = new List<SeedLineTrayDetails>();
            string type = "" , seedLot = "", seedLotID = "", Seed = "", ActualSeed = "";
            string NoOfTray = "", LeftOver = "";
            txtSeedsAllocated.Text = "0";
            foreach (GridViewRow item in gvDetails.Rows)
            {
               // hdnWOEmployeeIDVal = ((HiddenField)item.FindControl("hdnWOEmployeeID")).Value;

                type = ((DropDownList)item.FindControl("ddlType")).SelectedValue;
                seedLot = ((Label)item.FindControl("lblLotName")).Text;
                seedLotID = ((Label)item.FindControl("lblID")).Text;
                ActualSeed = ((Label)item.FindControl("lblactualseed")).Text;
                NoOfTray = ((TextBox)item.FindControl("txtActualTray")).Text;
                Seed = ((Label)item.FindControl("lblSeed")).Text;
                LeftOver = ((TextBox)item.FindControl("txtPartial")).Text;
                AddGrowerput(ref objinvoice, seedLotID, seedLot, ActualSeed, NoOfTray, Seed, type, LeftOver);
              
               
            }
            DataTable dtSeed = objTask.GetSeedNoBySeedLotID(ddlSeedLot.SelectedValue);
            //dtTrays.Rows.Add(ddlSeedLot.SelectedItem.Text, ddlSeedLot.SelectedValue, dtSeed.Rows[0]["NoOFSeed"].ToString());



            AddGrowerput(ref objinvoice, ddlSeedLot.SelectedValue, ddlSeedLot.SelectedItem.Text, dtSeed.Rows[0]["NoOFSeed"].ToString(), "", "", "", "");

            //GrowerPutData = objinvoice;
            gvDetails.DataSource = objinvoice;
            gvDetails.DataBind();
            ViewState["Data"] = objinvoice;
            ddlSeedLot.SelectedIndex = 0;
            foreach (GridViewRow item in gvDetails.Rows)
            {
                ActualSeed = ((Label)item.FindControl("lblactualseed")).Text;
                txtSeedsAllocated.Text = (Convert.ToInt32(txtSeedsAllocated.Text) + Convert.ToInt32(ActualSeed)).ToString();
                if (Convert.ToDouble(txtSeedsAllocated.Text) >= Convert.ToDouble(lblSeedRequired.Text))
                {
                    txtSeedsAllocated.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    txtSeedsAllocated.ForeColor = System.Drawing.Color.Black;
                }
            }
        }

        private void AddGrowerput(ref List<SeedLineTrayDetails> objGP, string seedLotID, string seedLot, string ActualSeed, string NoOfTray, string Seed, string type, string LeftOver)

        {
            SeedLineTrayDetails objInv = new SeedLineTrayDetails();
            objInv.SeedLot = seedLot;
            objInv.RowNumber = objGP.Count + 1;
            objInv.SeedLotID = seedLotID;
            objInv.ActualSeed = ActualSeed;
            objInv.Type = type;
            objInv.LeftOver = LeftOver;
            objInv.Seed = Seed;
            objInv.NoOftray = NoOfTray;
            objGP.Add(objInv);
        }

        private List<SeedLineTrayDetails> GrowerPutData
        {
            get
            {
                if (ViewState["GrowerPutData"] != null)
                {
                    return (List<SeedLineTrayDetails>)ViewState["GrowerPutData"];
                }
                return new List<SeedLineTrayDetails>();
            }
            set
            {
                ViewState["GrowerPutData"] = value;
            }
        }

        protected void txtActualTray_TextChanged(object sender, EventArgs e)
        {
            //txtSeedsAllocated.Text = "0";
           txtActualTraysNo.Text = "0";
            foreach (GridViewRow row in gvDetails.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtActual = (row.Cells[2].FindControl("txtActualTray") as TextBox);
                    if (txtActual.Text != "")
                    {

                        (row.Cells[3].FindControl("lblSeed") as Label).Text = (Convert.ToInt32(txtActual.Text) * Convert.ToInt32(lblTraySize.Text)).ToString();
                        txtActualTraysNo.Text = (Convert.ToInt32(txtActual.Text) + Convert.ToInt32(txtActualTraysNo.Text)).ToString();

                    }
                  //  string lotseed = (row.Cells[1].FindControl("lblactualseed") as Label).Text;
                    //txtSeedsAllocated.Text = (Convert.ToInt32(txtSeedsAllocated.Text) + Convert.ToInt32(lotseed)).ToString();
                    //if (Convert.ToDouble(txtSeedsAllocated.Text) >= Convert.ToDouble(lblSeedRequired.Text))
                    //   {
                    //       txtSeedsAllocated.ForeColor = System.Drawing.Color.Green;
                    //   }
                    //    else
                    //   {
                    //        txtSeedsAllocated.ForeColor = System.Drawing.Color.Black;
                    //   }
                }
            }
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ((DropDownList)e.Row.FindControl("ddlType")).SelectedValue = ((Label)(e.Row.FindControl("lblType"))).Text;
            }
        }
    }
}

[Serializable]
public class SeedLineTrayDetails
{
    public int ID { get; set; }

    public int RowNumber { get; set; }
    public string SeedLot { get; set; }
    public string SeedLotID { get; set; }

    public string Seed { get; set; }
    public string ActualSeed { get; set; }

    public string NoOftray { get; set; }
    public string Type { get; set; }

    public string LeftOver { get; set; }
}
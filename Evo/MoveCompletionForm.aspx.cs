﻿using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class MoveCompletionForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objCOm = new BAL_CommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["GrowerPutAwayId"] != null)
                {
                    GrowerPutAwayId = Request.QueryString["GrowerPutAwayId"].ToString();
                }
                txtMoveDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");

                BindGridMove();
                BindToFacility();
                BindToLocation();
            }
        }

        private string wo
        {
            get
            {
                if (ViewState["wo"] != null)
                {
                    return (string)ViewState["wo"];
                }
                return "";
            }
            set
            {
                ViewState["wo"] = value;
            }
        }

        private string GrowerPutAwayId
        {
            get
            {
                if (ViewState["GrowerPutAwayId"] != null)
                {
                    return (string)ViewState["GrowerPutAwayId"];
                }
                return "";
            }
            set
            {
                ViewState["GrowerPutAwayId"] = value;
            }
        }

        private string TraysRequest
        {
            get
            {
                if (ViewState["TraysRequest"] != null)
                {
                    return (string)ViewState["TraysRequest"];
                }
                return "";
            }
            set
            {
                ViewState["TraysRequest"] = value;
            }
        }


        private string TraysTotal
        {
            get
            {
                if (ViewState["TraysTotal"] != null)
                {
                    return (string)ViewState["TraysTotal"];
                }
                return "";
            }
            set
            {
                ViewState["TraysTotal"] = value;
            }
        }


        private string RemTraysTotal
        {
            get
            {
                if (ViewState["RemTraysTotal"] != null)
                {
                    return (string)ViewState["RemTraysTotal"];
                }
                return "";
            }
            set
            {
                ViewState["RemTraysTotal"] = value;
            }
        }


        public void BindGridMove()
        {
            DataSet dt = new DataSet();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@WoId", wo);
            //  nv.Add("@mode", "3");
            nv.Add("@GrowerPutAwayId", GrowerPutAwayId);
            dt = objCommon.GetDataSet("SP_GetShipmentCoordinatorTaskByMoveID", nv);
            gvMove.DataSource = dt.Tables[0];
            gvMove.DataBind();


            //lblToFacility.Text = dt.Tables[0].Rows[0]["FacilityToID"].ToString();
            if (string.IsNullOrEmpty(dt.Tables[1].Rows[0]["CompletedTrays"].ToString()))
            {

                lblRemainingTrays.Text = dt.Tables[0].Rows[0]["Trays"].ToString();
                lblRemainingTraysTotal.Text = dt.Tables[0].Rows[0]["Trays"].ToString();
                TraysTotal = dt.Tables[0].Rows[0]["Trays"].ToString();
            }
            else
            {
                lblRemainingTrays.Text = (Convert.ToInt32(dt.Tables[0].Rows[0]["Trays"].ToString()) - Convert.ToInt32(dt.Tables[1].Rows[0]["CompletedTrays"].ToString())).ToString();
                lblRemainingTraysTotal.Text = (Convert.ToInt32(dt.Tables[0].Rows[0]["Trays"].ToString()) - Convert.ToInt32(dt.Tables[1].Rows[0]["CompletedTrays"].ToString())).ToString();
                TraysTotal = lblRemainingTrays.Text;
            }
        }

        protected void gvMove_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMove.PageIndex = e.NewPageIndex;
            BindGridMove();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //if (Convert.ToDouble(txtTrays.Text) <= Convert.ToDouble(lblRemainingTrays.Text))
            //{

            //int TotalTrays = 0;
            //int DumpTrays = 0;
            //if (txtDumpTrays.Text == "")
            //{
            //    DumpTrays = 0;
            //}
            //else
            //{
            //    DumpTrays = Convert.ToInt32(txtDumpTrays.Text);
            //}

            // TotalTrays = Convert.ToInt32(txtTrays.Text) + DumpTrays;

            if (Convert.ToInt32(TraysTotal) == Convert.ToInt32(RemTraysTotal))
            {
                long result = 0;
                string TypeID = "";
                NameValueCollection nv = new NameValueCollection();
                //nv.Add("@MoveID", MoveID);
                //nv.Add("GrowerPutAwayId", GrowerPutAwayId);
                //nv.Add("MoveAssignID", lblMoveAssignID.Text);
                //nv.Add("@MoveDate", txtMoveDate.Text.Trim());
                //nv.Add("@Put_Away_Location", txtPutAwayLocation.Text.Trim());
                //nv.Add("@TraysMoved", txtTrays.Text.Trim());
                //nv.Add("@Barcode", txtBarcode.Text.Trim());
                //nv.Add("@CreateBy", Session["LoginID"].ToString());
                //result = objCommon.GetDataExecuteScalerRetObj("SP_AddMoveCompletionDetails", nv);

                if(RadioMoveCompletion.SelectedValue=="1")
                {
                    TypeID = "Bench";
                }
                else if(RadioMoveCompletion.SelectedValue == "2")
                {
                    TypeID = "Facility";
                }
                else
                {
                    TypeID = "";
                }
                nv.Add("GrowerPutAwayId", GrowerPutAwayId);
                nv.Add("MoveAssignID", lblMoveAssignID.Text);
                nv.Add("@MoveDate", txtMoveDate.Text.Trim());
                nv.Add("@Put_Away_Location", txtPutAwayLocation.Text.Trim());
                nv.Add("@TraysMoved", txtTrays.Text.Trim());
                nv.Add("@Barcode", txtBarcode.Text.Trim());
                nv.Add("@CreateBy", Session["LoginID"].ToString());

                nv.Add("@Facility", ddlFacility.SelectedValue);
                nv.Add("@FacilityMoveTrays", txtFTrays.Text);
                nv.Add("@FacilityDump", txtFDumpTrays.Text);
                nv.Add("@type", TypeID);
                nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
                nv.Add("@BenchLocationDump", txtBDumpTrays.Text);
                nv.Add("@BenchLocationTrays", txtBTrays.Text);




                
                result = objCommon.GetDataExecuteScalerRetObj("SP_AddGrowPutAwayMoveCompletionDetails", nv);

                GridViewRow row = gvMove.Rows[0];
                var txtJobNo = (row.FindControl("lblID") as Label).Text;
                var txtBenchLocation = (row.FindControl("lblGreenHouseName") as Label).Text;

                NameValueCollection nameValue = new NameValueCollection();
                nameValue.Add("@LoginID", Session["LoginID"].ToString());
                nameValue.Add("@jobcode", txtJobNo);
                nameValue.Add("@GreenHouseID", txtBenchLocation);
                nameValue.Add("@TaskName", "PutAway");

                var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

                var res = (Master.FindControl("r1") as Repeater);
                var lblCount = (Master.FindControl("lblNotificationCount") as Label);
                objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), res, lblCount);

                if (result > 0)
                {
                    lblmsg.Text = "Completion Successful";

                    if (lblRemainingTrays.Text == "0")
                    {

                        NameValueCollection nv1 = new NameValueCollection();
                        //nv1.Add("@WoId", wo);
                        //nv1.Add("@JobID", "");
                        //nv1.Add("@MoveID", result.ToString());
                        //nv1.Add("@GrowerPutAwayId","");
                        nv1.Add("@GrowerPutAwayId", GrowerPutAwayId);
                        nv1.Add("MoveAssignID", lblMoveAssignID.Text);
                        nv1.Add("@CreatedBy", Session["LoginID"].ToString());

                        int result1 = objCommon.GetDataInsertORUpdate("SP_AddCompletMoveForm", nv1);

                        
                    }


                    Clear();
                    if (Session["Role"].ToString() == "3" || Session["Role"].ToString() == "5" || Session["Role"].ToString() == "11" || Session["Role"].ToString() == "6")
                    {
                        string message = "Completion Successful";
                        string url = "MyTaskShippingCoordinator.aspx";
                        objCommon.ShowAlertAndRedirect(message, url);
                        //  Response.Redirect("~/MyTaskShippingCoordinator.aspx");
                    }

                    else if (Session["Role"].ToString() == "2" || Session["Role"].ToString() == "12")
                    {

                        string message = "Completion Successful";
                        string url = "MyTaskLogisticManager.aspx";
                        objCommon.ShowAlertAndRedirect(message, url);
                    }
                }
                else
                {
                    lblmsg.Text = "Completion Not Successful";
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Trays Moved + Dump Trays is not equal to requested Trays moved by Grower')", true);
            }


        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtMoveDate.Text = "";
            txtTrays.Text = "";
            // ddlLocation.SelectedIndex = 0;
        }

        protected void gvMove_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select1")
            {
                AddDetails.Visible = true;
                string traysTotal = "";
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvMove.Rows[rowIndex];


                //  MoveId = (row.FindControl("lblGrowerPutAwayId") as Label).Text;


                txtPutAwayLocation.Text = (row.FindControl("lblGreenHouseName") as Label).Text;

                //   traysTotal = (row.FindControl("lblTraysRequest") as Label).Text;

                lblRemainingTrays.Text = (row.FindControl("lblTraysRequest") as Label).Text;
                TraysRequest = (row.FindControl("lblTraysRequest") as Label).Text;

                txtMoveDate.Focus();
            }
        }

        protected void txtTrays_TextChanged(object sender, EventArgs e)
        {
            TraysTotalDetails();


            if (lblRemainingTrays.Text != "0")
            {
                MoveCompletion.Visible = true;
            }
            else
            {
                MoveCompletion.Visible = false;
            }

            //if (txtTrays.Text != "")
            //{
            //    if (Convert.ToInt32(txtTrays.Text) <= Convert.ToInt32(lblRemainingTrays.Text))
            //    {
            //        lblRemainingTrays.Text = (Convert.ToInt32(lblRemainingTrays.Text) - Convert.ToInt32(txtTrays.Text)).ToString();

            //    }
            //    else
            //    {
            //       // txtTrays.Text = "";

            //        // lblRemainingTrays.Text = TraysRequest;
            //    }


            //}
        }

        protected void gvMove_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // Label lblGrowerPutAwayId = (Label)e.Row.FindControl("lblGrowerPutAwayId");
                lblMoveAssignID.Text = ((Label)e.Row.FindControl("lblMoveAssignID")).Text;
                Label lblTray = (Label)e.Row.FindControl("lblTray");
                Label lblTraysRequest = (Label)e.Row.FindControl("lblTraysRequest");
                txtPutAwayLocation.Text = ((Label)e.Row.FindControl("lblGreenHouseName")).Text;
                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                // nv.Add("@WoId", lblGrowerPutAwayId.Text);
                //nv.Add("@mode", "2");
                nv.Add("@GrowerPutAwayId", GrowerPutAwayId);
                dt = objCommon.GetDataTable("SP_GetMovedTraysByGrowerPutAwayId", nv);
                //   dt = objCommon.GetDataTable("SP_GetGrowerPutAwayLogisticManagerAssignedJobByMoveID", nv);

                lblTraysRequest.Text = (Convert.ToInt32(lblTray.Text) - Convert.ToInt32(dt.Rows[0]["TraysMovedTotal"])).ToString();
            }
        }

        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {

            if (txtPutAwayLocation.Text.Trim() != txtBarcode.Text.Trim())
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowAlert", "alert('Barcode does not match with Bench Location.');", true);
                txtBarcode.Text = "";
            }
        }

        protected void txtDumpTrays_TextChanged(object sender, EventArgs e)
        {
            TraysTotalDetails();

            if (lblRemainingTrays.Text != "0")
            {
                MoveCompletion.Visible = true;
            }
            else
            {
                MoveCompletion.Visible = false;
            }


            //if (txtTrays.Text != "")
            //{
            //    lblRemainingTrays.Text = (Convert.ToInt32(TraysTotal) - Convert.ToInt32(txtTrays.Text)).ToString();
            //}
            //else
            //{
            //    lblRemainingTrays.Text = TraysTotal.ToString();
            //}
            //if (txtDumpTrays.Text != "")
            //{
            //if (Convert.ToInt32(txtDumpTrays.Text) <= Convert.ToInt32(lblRemainingTrays.Text))
            //{
            //    lblRemainingTrays.Text = (Convert.ToInt32(lblRemainingTrays.Text) - Convert.ToInt32(txtDumpTrays.Text)).ToString();

            //}
            //else
            //{
            // //   txtDumpTrays.Text = "";

            //    // lblRemainingTrays.Text = TraysRequest;
            //}

            //  lblRemainingTrays.Text = (Convert.ToInt32(lblRemainingTrays.Text) - Convert.ToInt32(txtDumpTrays.Text)).ToString();

            // }
        }


        public void TraysTotalDetails()
        {
            int Mtrays = 0, MDumpTrays = 0; 
            int Btrays = 0, BDumpTrays = 0; 
            int Ftrays = 0, FDumpTrays = 0;
            int TrayTotal = 0;
            if(txtTrays.Text !="")
            {
                Mtrays = Convert.ToInt32(txtTrays.Text);
            }
            else
            {
                Mtrays = 0;
            }

            if (txtDumpTrays.Text != "")
            {
                MDumpTrays = Convert.ToInt32(txtDumpTrays.Text);
            }
            else
            {
                MDumpTrays = 0;
            }
            //---------------------------


            if (txtFTrays.Text != "")
            {
                Ftrays = Convert.ToInt32(txtFTrays.Text);
            }
            else
            {
                Ftrays = 0;
            }

            if (txtFDumpTrays.Text != "")
            {
                FDumpTrays = Convert.ToInt32(txtFDumpTrays.Text);
            }
            else
            {
                FDumpTrays = 0;
            }


            if (txtBTrays.Text != "")
            {
                Btrays = Convert.ToInt32(txtBTrays.Text);
            }
            else
            {
                Btrays = 0;
            }

            if (txtBDumpTrays.Text != "")
            {
                BDumpTrays = Convert.ToInt32(txtBDumpTrays.Text);
            }
            else
            {
                BDumpTrays = 0;
            }

           
            TrayTotal = Mtrays + MDumpTrays + Btrays + BDumpTrays + Ftrays + FDumpTrays;

            RemTraysTotal = TrayTotal.ToString();
            lblRemainingTrays.Text = (Convert.ToInt32(lblRemainingTraysTotal.Text) - TrayTotal).ToString();

         

        }

        public void BindToLocation()
        {
            ddlBenchLocation.DataSource = objCOm.GetLocation(Session["Facility"].ToString());
            ddlBenchLocation.DataTextField = "BenchName";
            ddlBenchLocation.DataValueField = "BenchName";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindToFacility()
        {
            ddlFacility.DataSource = objCOm.GetMainLocation();
            ddlFacility.DataTextField = "Facility";
            ddlFacility.DataValueField = "Facility";
            ddlFacility.DataBind();
            ddlFacility.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }


        

        protected void RadioMoveCompletion_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(RadioMoveCompletion.SelectedValue=="1")
            {
                PanelFacility.Visible = false;
                Panelbench.Visible = true;
            }
            else if(RadioMoveCompletion.SelectedValue == "2")
            {
                PanelFacility.Visible = true;
                Panelbench.Visible = false;
            }
            else
            {
                PanelFacility.Visible = false;
                Panelbench.Visible = false;
            }
            
        }

        protected void txtFTrays_TextChanged(object sender, EventArgs e)
        {
            TraysTotalDetails();
        }

        protected void txtFDumpTrays_TextChanged(object sender, EventArgs e)
        {
            TraysTotalDetails();
        }

        protected void txtBTrays_TextChanged(object sender, EventArgs e)
        {
            TraysTotalDetails();
        }

        protected void txtBDumpTrays_TextChanged(object sender, EventArgs e)
        {
            TraysTotalDetails();
        }
    }
}
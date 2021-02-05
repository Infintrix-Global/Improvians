﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Improvians
{
    public partial class GetGrowerPutAwayForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindGridGerm();
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

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Mode", "1");
            nv.Add("@wo", "");
            dt = objCommon.GetDataTable("SP_GetGrowerPutAway", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }



        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Assign")
            {
                AddGrowerPutRow(true);
                // invoiceSplitJob();
                PanelAdd.Visible = true;
                PanelList.Visible = false;
                string wo_No = e.CommandArgument.ToString();
                wo = wo_No;
                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@Mode", "2");
                nv.Add("@wo", wo_No);
                dt = objCommon.GetDataTable("SP_GetGrowerPutAway", nv);

                if (dt != null && dt.Rows.Count > 0)
                {
                    lblJobID.Text = dt.Rows[0]["jobcode"].ToString();
                    lblSeedDate.Text = Convert.ToDateTime(dt.Rows[0]["SeededDate"]).ToString("MM-dd-yyyy");
                    lblSeededTrays.Text = dt.Rows[0]["#TraysSeeded"].ToString();

                }


            }


        }


        protected void ButtonAddGridInvoice_Click(object sender, EventArgs e)
        {
            //  AddNewRowToGridSetInitialInvoice();
            AddGrowerPutRow(true);

        }

        protected void GridSplitJob_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlMain = (DropDownList)e.Row.FindControl("ddlMain");
                DropDownList ddlLocation = (DropDownList)e.Row.FindControl("ddlLocation");
                Label lblMain = (Label)e.Row.FindControl("lblMain");
                Label lblLocation = (Label)e.Row.FindControl("lblLocation");

                NameValueCollection nv = new NameValueCollection();
                nv.Add("@mode", "4");
                ddlMain.DataSource = objCommon.GetDataTable("GET_Common", nv); ;
                ddlMain.DataTextField = "FacilityName";
                ddlMain.DataValueField = "FacilityID";
                ddlMain.DataBind();
                ddlMain.Items.Insert(0, new ListItem("--- Select ---", "0"));

                //  BindLocation();

                BindLocationNew(ref ddlLocation, lblMain.Text);
                ddlMain.SelectedValue = lblMain.Text;
                ddlLocation.SelectedValue = lblLocation.Text;

            }
        }

        protected void ddlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            // BindLocation();
            DropDownList ddlMain = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlMain.NamingContainer;
            if (row != null)
            {

                DropDownList ddlLocation = (DropDownList)row.FindControl("ddlLocation");
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@FacilityID", ddlMain.SelectedValue);
                ddlLocation.DataSource = objCommon.GetDataTable("SP_GetGreenhouseByFacility", nv); ;
                ddlLocation.DataTextField = "GreenHouseName";
                ddlLocation.DataValueField = "GreenHouseID";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));

            }
        }

        public void BindLocation()
        {


            foreach (GridViewRow row in GridSplitJob.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    DropDownList ddlLocation = (row.Cells[0].FindControl("ddlLocation") as DropDownList);
                    DropDownList ddlMain = (row.Cells[0].FindControl("ddlMain") as DropDownList);

                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@FacilityID", ddlMain.SelectedValue);
                    ddlLocation.DataSource = objCommon.GetDataTable("SP_GetGreenhouseByFacility", nv); ;
                    ddlLocation.DataTextField = "GreenHouseName";
                    ddlLocation.DataValueField = "GreenHouseID";
                    ddlLocation.DataBind();
                    ddlLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));

                    //if(A=="")
                    //{
                    //    A = "0";
                    //}

                    //ddlLocation.SelectedValue = A;

                }

            }




        }



        public void BindLocationNew(ref DropDownList ddlLocation, string ddlMain)
        {

            NameValueCollection nv = new NameValueCollection();
            nv.Add("@FacilityID", ddlMain);
            ddlLocation.DataSource = objCommon.GetDataTable("SP_GetGreenhouseByFacility", nv); ;
            ddlLocation.DataTextField = "GreenHouseName";
            ddlLocation.DataValueField = "GreenHouseID";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }



        protected void txtTrays_TextChanged(object sender, EventArgs e)
        {
            CalData();
        }

        public void CalData()
        {

            int Total = 0;

            foreach (GridViewRow row in GridSplitJob.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    TextBox txtTrays = (row.Cells[0].FindControl("txtTrays") as TextBox);


                    //  TexTotal = Total * Convert.ToInt32(ddlTAX.SelectedItem.Text) / 100;
                    if (txtTrays.Text != "")
                    {
                        Total += Convert.ToInt32(txtTrays.Text);
                    }


                }
            }

            lblRemaining.Text = (Convert.ToInt32(lblSeededTrays.Text) - Total).ToString();


        }

        public void Clear()
        {
            BindGridGerm();
            PanelAdd.Visible = false;
            PanelList.Visible = true;
            GridSplitJob.DataSource = null;
            GridSplitJob.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                long _isInserted = 1;
                int SelectedItems = 0;

                foreach (GridViewRow item in GridSplitJob.Rows)
                {

                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtTrays = (item.Cells[0].FindControl("txtTrays") as TextBox);
                        DropDownList ddlMain = (item.Cells[0].FindControl("ddlMain") as DropDownList);
                        DropDownList ddlLocation = (item.Cells[0].FindControl("ddlLocation") as DropDownList);

                        long result = 0;
                        NameValueCollection nv = new NameValueCollection();
                        nv.Add("@GrowerPutAwayId", "");
                        nv.Add("@wo", wo);

                        nv.Add("@jobcode", lblJobID.Text);
                        nv.Add("@FacilityID", ddlMain.SelectedValue);
                        nv.Add("@GreenHouseID", ddlLocation.SelectedValue);
                        nv.Add("@Trays", txtTrays.Text);

                        nv.Add("@SeedDate", lblSeedDate.Text);
                        nv.Add("@CreateBy", Session["LoginID"].ToString());

                        nv.Add("@mode", "1");
                        _isInserted = objCommon.GetDataInsertORUpdate("SP_AddGrowerPutAwayDetails", nv);
                        SelectedItems++;
                    }


                    NameValueCollection nv1 = new NameValueCollection();
                    nv1.Add("@WorkOrder", wo);
                    _isInserted = objCommon.GetDataInsertORUpdate("SP_UpdateGrowerPutAwayDetails", nv1);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Grower Put Away Save  Successful')", true);

                    Clear();
                }


            }
            catch (Exception ex)
            {
            }
        }

        private List<GrowerputDetils> GrowerPutData
        {
            get
            {
                if (ViewState["GrowerPutData"] != null)
                {
                    return (List<GrowerputDetils>)ViewState["GrowerPutData"];
                }
                return new List<GrowerputDetils>();
            }
            set
            {
                ViewState["GrowerPutData"] = value;
            }
        }

        private void AddGrowerPutRow(bool AddBlankRow)
        {
            try
            {
                string unit = "", ddlTAX1 = "", ddlStatusVal = "", hdnWOEmployeeIDVal = "";
                string MainId = "", LocationId = "";


                List<GrowerputDetils> objinvoice = new List<GrowerputDetils>();

                foreach (GridViewRow item in GridSplitJob.Rows)
                {
                    hdnWOEmployeeIDVal = ((HiddenField)item.FindControl("hdnWOEmployeeID")).Value;

                    MainId = ((DropDownList)item.FindControl("ddlMain")).SelectedValue;
                    LocationId = ((DropDownList)item.FindControl("ddlLocation")).SelectedValue;
                    TextBox txtTrays = (TextBox)item.FindControl("txtTrays");

                    AddGrowerput(ref objinvoice, Convert.ToInt32(hdnWOEmployeeIDVal), Convert.ToInt32(MainId), Convert.ToInt32(LocationId), txtTrays.Text);

                }
                if (AddBlankRow)
                    AddGrowerput(ref objinvoice, 1, 0, 0, "");

                GrowerPutData = objinvoice;
                GridSplitJob.DataSource = objinvoice;
                GridSplitJob.DataBind();
                ViewState["Data"] = objinvoice;


            }
            catch (Exception ex)
            {
                //  divMessage.Visible = true;
                //  divMessageSub.Attributes.Remove("class");
                // divMessageSub.Attributes.Add("class", "errormsg");
                //  lblMsg.Text = "Unable to process request. Please verify the details.<br />" + ex;
            }

        }

        public void GridSplitjob()
        {
            GridSplitJob.DataSource = ViewState["Data"];
            GridSplitJob.DataBind();

        }

        private void AddGrowerput(ref List<GrowerputDetils> objGP, int ID, int FacilityID, int GreenHouseID, string Trays)

        {
            GrowerputDetils objInv = new GrowerputDetils();
            objInv.ID = ID;
            objInv.RowNumber = objGP.Count + 1;
            objInv.FacilityID = FacilityID;

            objInv.GreenHouseID = GreenHouseID;
            objInv.Trays = Trays;

            objGP.Add(objInv);
        }
    }
}



[Serializable]
public class GrowerputDetils
{
    public int ID { get; set; }

    public int RowNumber { get; set; }
    public int FacilityID { get; set; }
    public int GreenHouseID { get; set; }

    public string Trays { get; set; }
}
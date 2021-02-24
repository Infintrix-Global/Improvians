﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Improvians.Bal;
using Improvians.BAL_Classes;

namespace Improvians
{
    public partial class MovesReqManual : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSupervisor();               
                BindJobCode();
                Bindcname();
                BindFacility();
              
                dtTrays.Clear();
            }
        }

        public void Bindcname()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "8");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "cname";
            ddlCustomer.DataValueField = "cname";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindJobCode()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "7");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindFacility()
        {
            ddlFacility.DataSource = objBAL.GetMainLocation();
            ddlFacility.DataTextField = "l1";
            ddlFacility.DataValueField = "l1";
            ddlFacility.DataBind();
            ddlFacility.Items.Insert(0, new ListItem("--Select--", ""));

           
                // NameValueCollection nv = new NameValueCollection();
                // nv.Add("@mode", "4");
                // ddlToFacility.DataSource = objCommon.GetDataTable("GET_Common", nv); ;
                ddlToFacility.DataSource = objBAL.GetMainLocation();
                ddlToFacility.DataTextField = "l1";
                ddlToFacility.DataValueField = "l1";
                ddlToFacility.DataBind();
                ddlToFacility.Items.Insert(0, new ListItem("--- Select ---", "0"));
           
            BindBenchLocation("");
        }

        public void BindBenchLocation(string ddlMain)
        {
            ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataTextField = "p2";
            ddlBenchLocation.DataValueField = "p2";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", ""));
        }

        public void BindGridFerReq()
        {
            gvFer.DataSource = objFer.GetManualFertilizerRequest(ddlFacility.SelectedValue, ddlBenchLocation.SelectedValue);
            gvFer.DataBind();

        }
        protected void ddlToFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlToFacility.SelectedIndex != 0)
            {
                //  NameValueCollection nv = new NameValueCollection();
                //  nv.Add("@FacilityID", ddlToFacility.SelectedValue);
                ddlToGreenHouse.DataSource = objBAL.GetLocation(ddlToFacility.SelectedValue);
                ddlToGreenHouse.DataTextField = "p2";
                ddlToGreenHouse.DataValueField = "p2";
                ddlToGreenHouse.DataBind();
                ddlToGreenHouse.Items.Insert(0, new ListItem("--- Select ---", "0"));



            }
        }
       
      

        protected void btnAddTray_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Convert.ToDouble(txtTrays.Text) <= Convert.ToDouble(lblUnmovedTrays.Text))
                //{
                //    lblerrmsg.Text = "";
                //    dtTrays.Rows.Add(lblFromFacility.Text, ddlToFacility.SelectedItem.Text, ddlToFacility.SelectedValue, ddlToGreenHouse.SelectedItem.Text, ddlToGreenHouse.SelectedValue, txtTrays.Text);
                //    GridMove.DataSource = dtTrays;
                //    GridMove.DataBind();
                //    lblUnmovedTrays.Text = (Convert.ToInt32(lblUnmovedTrays.Text) - Convert.ToInt32(txtTrays.Text)).ToString();
                //    txtTrays.Text = "";
                //    ddlToFacility.SelectedIndex = 0;
                //    ddlToGreenHouse.SelectedIndex = 0;

                //}
                //else
                //{

                //    lblerrmsg.Text = "Number of Trays exceed Remaining trays";

                //}
            }
            catch (Exception ex)
            {

            }
        }
        public void BindSupervisor()
        {
            NameValueCollection nv = new NameValueCollection();
            if (Session["Role"].ToString() == "1")
            {
                ddlLogisticManager.DataSource = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
                //ddlLogisticManager.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlLogisticManager.DataTextField = "EmployeeName";
                ddlLogisticManager.DataValueField = "ID";
                ddlLogisticManager.DataBind();
                ddlLogisticManager.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            if (Session["Role"].ToString() == "12")
            {
                ddlLogisticManager.DataSource = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
                //ddlLogisticManager.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlLogisticManager.DataTextField = "EmployeeName";
                ddlLogisticManager.DataValueField = "ID";
                ddlLogisticManager.DataBind();
                ddlLogisticManager.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }


        protected void gvFer_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvFer.Rows[rowIndex];
                
                //ddlLogisticManager.Focus();
            }
        }

        protected void gvFer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFer.PageIndex = e.NewPageIndex;
            BindGridFerReq();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvFer.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlLogisticManager.SelectedValue);
                    nv.Add("@WorkOrder", (row.FindControl("lblwo") as Label).Text);
                    nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                   
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@FromFacility", lblFromFacility.Text);
                    nv.Add("@ToFacility", ddlToFacility.SelectedValue);
                    nv.Add("@ToGreenHouse", ddlToGreenHouse.Text);
                    nv.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text); 
                    nv.Add("@MoveDate", txtDate.Text);

                    nv.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    result = objCommon.GetDataExecuteScaler("SP_AddMoveRequestManual", nv);
                    //result = objTask.AddMoveRequest(dtTrays, lbljobid.Text, txtReqDate.Text, Session["LoginID"].ToString(), ddlLogisticManager.SelectedValue,wo);
                    // result = objTask.AddMoveRequest(dtTrays, lbljobid.Text, txtReqDate.Text, Session["LoginID"].ToString(), ddlLogisticManager.SelectedValue, "", lblGrowerputawayID.Text);
                    //if (result > 0)
                    //{
                     
                      
                    //}
                    //else
                    //{
                    //    lblmsg.Text = "Request Not Successful";
                    //}
                }
            }
            string message = "Assignment Successful";
            string url = "MyTaskGrower.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

            Clear();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            ddlFacility.SelectedIndex = 0;
            ddlBenchLocation.SelectedIndex = 0;
            ddlCustomer.SelectedIndex = 0;
            ddlJobNo.SelectedIndex = 0;        
          
            dtTrays.Clear();
        }

       

        private string Bench
        {
            get
            {
                if (ViewState["Bench"] != null)
                {
                    return (string)ViewState["Bench"];
                }
                return "";
            }
            set
            {
                ViewState["Bench"] = value;
            }
        }


        protected void btnAssign_Click(object sender, EventArgs e)
        {
            int tray = 0;
            foreach (GridViewRow row in gvFer.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    tray = tray + Convert.ToInt32((row.FindControl("lblTotTray") as Label).Text);
                }

            }
            txtTrays.Text = tray.ToString();

            userinput.Visible = true;
            //ddlLogisticManager.Focus();
            lblFromFacility.Text = ddlFacility.SelectedValue;

        }

        protected void chckchanged(object sender, EventArgs e)
        {
            CheckBox chckheader = (CheckBox)gvFer.HeaderRow.FindControl("CheckBoxall");
            foreach (GridViewRow row in gvFer.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckheader.Checked == true)
                {
                    chckrw.Checked = true;
                }
                else
                {
                    chckrw.Checked = false;
                }
            }

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridFerReq();
        }
        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            ddlFacility.SelectedIndex = 0;
            ddlBenchLocation.SelectedIndex = 0;
            ddlCustomer.SelectedIndex = 0;
            ddlJobNo.SelectedIndex = 0;
            //BindGridFerReq();
            gvFer.DataSource = null;
            gvFer.DataBind();
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq();
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq();
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBenchLocation(ddlFacility.SelectedValue);
            //BindGridFerReq();
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq();
        }

    }
}
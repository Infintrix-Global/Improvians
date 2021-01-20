﻿using System;
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
    public partial class MoveForm : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "FromFacility", "ToFacility","Greenhouse","Trays" } };
        CommonControl objCommon = new CommonControl();
       BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridMoveReq();
                BindFacility();
            }
        }

        public void BindFacility()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@mode", "4");
            ddlToFacility.DataSource = objCommon.GetDataTable("GET_Common", nv); ;
            ddlToFacility.DataTextField = "FacilityName";
            ddlToFacility.DataValueField = "FacilityID";
            ddlToFacility.DataBind();
            ddlToFacility.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindGridMoveReq()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            dt = objCommon.GetDataTable("SP_GetMoveRequest", nv);
            gvMove.DataSource = dt;
            gvMove.DataBind();

        }

        protected void gvMove_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMove.PageIndex = e.NewPageIndex;
            BindGridMoveReq();
        }

        protected void gvMove_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvMove.Rows[rowIndex];
                lblFromFacility.Text = (row.FindControl("lblFacility") as Label).Text;
                lbljobid.Text = (row.FindControl("lblID") as Label).Text;
                ddlToFacility.Focus();
            }
        }

        protected void GridMove_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                dtTrays.Rows.RemoveAt(e.RowIndex);
                GridMove.DataSource = dtTrays;
                GridMove.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void GridMove_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridMove.EditIndex)
            {
                (e.Row.Cells[0].Controls[0] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this record?');";
            }
        }

        protected void btnAddTray_Click(object sender, EventArgs e)
        {
            try
            {
                dtTrays.Rows.Add(lblFromFacility.Text,ddlToFacility.SelectedItem.Text,ddlToGreenHouse.SelectedItem.Text);
                GridMove.DataSource = dtTrays;
                GridMove.DataBind();
                txtTrays.Text = "";
                ddlToFacility.SelectedIndex = 0;
                ddlToGreenHouse.SelectedIndex = 0;

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            objTask.AddMoveRequest(dtTrays, lbljobid.Text, txtReqDate.Text, Session["LoginID"].ToString());
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
    }
}
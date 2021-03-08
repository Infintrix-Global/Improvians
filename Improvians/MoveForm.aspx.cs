using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Bal;
using Evo.BAL_Classes;
namespace Evo
{
    public partial class MoveForm : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "FromFacility", "ToFacility", "ToFacilityID", "Greenhouse", "GreenHouseID", "Trays" } };
        CommonControl objCommon = new CommonControl();
        BAL_Task objTask = new BAL_Task();
        BAL_CommonMasters objCOm = new BAL_CommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridMoveReq();
                BindFacility();
                BindLogisticList();
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

        public void BindLogisticList()
        {
            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@RoleID", "5");
            //ddlLogisticManager.DataSource = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;
            //ddlLogisticManager.DataTextField = "EmployeeName";
            //ddlLogisticManager.DataValueField = "ID";
            //ddlLogisticManager.DataBind();
            //ddlLogisticManager.Items.Insert(0, new ListItem("--Select--", "0"));


            NameValueCollection nv = new NameValueCollection();
            if (Session["Role"].ToString() == "1")
            {
                ddlLogisticManager.DataSource = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlLogisticManager.DataTextField = "EmployeeName";
                ddlLogisticManager.DataValueField = "ID";
                ddlLogisticManager.DataBind();
                ddlLogisticManager.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            if (Session["Role"].ToString() == "12")
            {
                ddlLogisticManager.DataSource = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlLogisticManager.DataTextField = "EmployeeName";
                ddlLogisticManager.DataValueField = "ID";
                ddlLogisticManager.DataBind();
                ddlLogisticManager.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        public void BindFacility()
        {
            // NameValueCollection nv = new NameValueCollection();
            // nv.Add("@mode", "4");
            // ddlToFacility.DataSource = objCommon.GetDataTable("GET_Common", nv); ;
            ddlToFacility.DataSource=objCOm.GetMainLocation();
            ddlToFacility.DataTextField = "l1";
            ddlToFacility.DataValueField = "l1";
            ddlToFacility.DataBind();
            ddlToFacility.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindGridMoveReq()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@mode", "1");
            // nv.Add("@wo", "");
            nv.Add("@GrowerPutAwayID", "");
            dt = objCommon.GetDataTable("SP_GetMoveRequest", nv);
            gvMove.DataSource = dt;
            gvMove.DataBind();

        }

        protected void gvMove_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblGrowerPutAwayId = (Label)e.Row.FindControl("lblGrowerPutAwayId");
                Label lblTray = (Label)e.Row.FindControl("lblTray");
                Label lblTraysRequest = (Label)e.Row.FindControl("lblTraysRequest");

                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@WoId", lblGrowerPutAwayId.Text);
                nv.Add("@mode", "2");
                dt = objCommon.GetDataTable("SP_GetGrowerPutAwayLogisticManagerAssignedJobByMoveID", nv);

                lblTraysRequest.Text = (Convert.ToInt32(lblTray.Text) - Convert.ToInt32(dt.Rows[0]["TraysMovedTotal"])).ToString();


            }
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

                string rowIndex = e.CommandArgument.ToString();
                //  GridViewRow row = gvMove.Rows[rowIndex];
                //  lblFromFacility.Text = (row.FindControl("lblFacility") as Label).Text;
                //lbljobid.Text = (row.FindControl("lblID") as Label).Text;

                DataTable dt11 = new DataTable();
                NameValueCollection nv11 = new NameValueCollection();
                nv11.Add("@mode","2");
                //  nv11.Add("@wo", rowIndex);
                nv11.Add("@GrowerPutAwayID", rowIndex);
                dt11 = objCommon.GetDataTable("SP_GetMoveRequest", nv11);

                lblFromFacility.Text = dt11.Rows[0]["FacilityID"].ToString();
                lbljobid.Text = dt11.Rows[0]["JobCode"].ToString();
                lblGrowerputawayID.Text = dt11.Rows[0]["GrowerPutAwayID"].ToString();


                wo = rowIndex;
                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@JobID", rowIndex.ToString());
                dt = objCommon.GetDataTable("SP_GetUnMovedTraysByJobID", nv);


                //if (string.IsNullOrEmpty(dt.Rows[0]["UnMovedTrays"].ToString()))
                //{
                if(dt != null && dt.Rows.Count >0)
                { 
                    lblUnmovedTrays.Text = dt11.Rows[0]["Trays"].ToString();

                }
                else
                {
                    lblUnmovedTrays.Text = (Convert.ToInt32(dt11.Rows[0]["Trays"].ToString()) - (Convert.ToInt32(dt.Rows[0]["UnMovedTrays"].ToString()))).ToString();
                }
                ddlToFacility.Focus();
            }
        }

        protected void GridMove_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string tray = (GridMove.Rows[e.RowIndex].FindControl("lblTray") as Label).Text;
                lblUnmovedTrays.Text = ((Convert.ToInt32(lblUnmovedTrays.Text) + Convert.ToInt32(tray)).ToString());
                dtTrays.Rows.RemoveAt(e.RowIndex);
                GridMove.DataSource = dtTrays;
                GridMove.DataBind();
            }
            catch (Exception ex)
            {

            }
        }


        protected void btnAddTray_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtTrays.Text) <= Convert.ToDouble(lblUnmovedTrays.Text))
                {
                    lblerrmsg.Text = "";
                    dtTrays.Rows.Add(lblFromFacility.Text, ddlToFacility.SelectedItem.Text, ddlToFacility.SelectedValue, ddlToGreenHouse.SelectedItem.Text, ddlToGreenHouse.SelectedValue, txtTrays.Text);
                    GridMove.DataSource = dtTrays;
                    GridMove.DataBind();
                    lblUnmovedTrays.Text = (Convert.ToInt32(lblUnmovedTrays.Text) - Convert.ToInt32(txtTrays.Text)).ToString();
                    txtTrays.Text = "";
                    ddlToFacility.SelectedIndex = 0;
                    ddlToGreenHouse.SelectedIndex = 0;

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
           //result = objTask.AddMoveRequest(dtTrays, lbljobid.Text, txtReqDate.Text, Session["LoginID"].ToString(), ddlLogisticManager.SelectedValue,wo);
            result = objTask.AddMoveRequest(dtTrays, lbljobid.Text, txtReqDate.Text, Session["LoginID"].ToString(), ddlLogisticManager.SelectedValue, wo, lblGrowerputawayID.Text);
            if (result > 0)
            {
                // lblmsg.Text = "Request Successful";
                Clear();
                BindGridMoveReq();
                string message = "Request Successful";
                string url = "MoveForm.aspx";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
              //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request Successful')", true);
                userinput.Visible = false;
            }
            else
            {
                lblmsg.Text = "Request Not Successful";
            }


        }

        public void Clear()
        {
            dtTrays.Clear();
            txtReqDate.Text = "";
            ddlToGreenHouse.SelectedIndex = 0;
            ddlToFacility.SelectedIndex = 0;
            txtTrays.Text = "";
            userinput.Visible = false;

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void ddlToFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlToFacility.SelectedIndex != 0)
            {
              //  NameValueCollection nv = new NameValueCollection();
              //  nv.Add("@FacilityID", ddlToFacility.SelectedValue);
                ddlToGreenHouse.DataSource = objCOm.GetLocation(ddlToFacility.SelectedValue);
                ddlToGreenHouse.DataTextField = "p2";
                ddlToGreenHouse.DataValueField = "p2";
                ddlToGreenHouse.DataBind();
                ddlToGreenHouse.Items.Insert(0, new ListItem("--- Select ---", "0"));


             
            }
        }
        protected void btnManual_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MovesReqManual.aspx");
        }
    }
}
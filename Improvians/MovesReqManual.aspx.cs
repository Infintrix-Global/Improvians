using System;
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
        protected void GridMove_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
               // string tray = (GridMove.Rows[e.RowIndex].FindControl("lblTray") as Label).Text;
                //lblUnmovedTrays.Text = ((Convert.ToInt32(lblUnmovedTrays.Text) + Convert.ToInt32(tray)).ToString());
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

            long result = 0;
            //result = objTask.AddMoveRequest(dtTrays, lbljobid.Text, txtReqDate.Text, Session["LoginID"].ToString(), ddlLogisticManager.SelectedValue,wo);
           // result = objTask.AddMoveRequest(dtTrays, lbljobid.Text, txtReqDate.Text, Session["LoginID"].ToString(), ddlLogisticManager.SelectedValue, "", lblGrowerputawayID.Text);
            if (result > 0)
            {
                // lblmsg.Text = "Request Successful";
                Clear();
                //BindGridMoveReq();
                string message = "Request Successful";
                string url = "MovesReqManual.aspx";
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
            userinput.Visible = true;
            //ddlLogisticManager.Focus();
            lblFromFacility.Text = ddlFacility.SelectedValue;

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
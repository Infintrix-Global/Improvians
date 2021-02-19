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
    public partial class FertilizerReqManual : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridFerReq();
                BindSupervisor();
                BindFertilizer();
                BindUnit();
                BindJobCode();
                BindBenchLocation();
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

        public void BindBenchLocation()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "10");
            dt = objCommon.GetDataTable("GET_Common", nv);
            //ddlBenchLocation.DataSource = dt;
            //ddlBenchLocation.DataTextField = "GreenHouseID";
            //ddlBenchLocation.DataValueField = "GreenHouseID";
            //ddlBenchLocation.DataBind();
            //ddlBenchLocation.Items.Insert(0, new ListItem("--Select--", "0"));
            repBench.DataSource = dt;
            repBench.DataBind();
        }

        public void BindFacility()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "9");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlFacility.DataSource = dt;
            ddlFacility.DataTextField = "loc_seedline";
            ddlFacility.DataValueField = "loc_seedline";
            ddlFacility.DataBind();
            ddlFacility.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindGridFerReq()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", ddlFacility.SelectedValue);
            //nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            int c = 0;
            string x = "";
            foreach (RepeaterItem item in repBench.Items)
            {
                CheckBox chkBench = (CheckBox)item.FindControl("chkBench");
                if (chkBench.Checked)
                {
                    c = 1;
                    x += "'" + ((HiddenField)item.FindControl("hdnValue")).Value + "',";

                }
            }
            if (c > 0)
            {
                string chkSelected = x.Remove(x.Length - 1, 1);
                nv.Add("@BenchLocation", chkSelected);
            }
            else
            {
                nv.Add("@BenchLocation", "0");
            }
            //dt = objCommon.GetDataTable("SP_GetFertilizerRequestManual", nv);
            dt = objFer.GetManualFertilizerRequest(x);
            
            gvFer.DataSource = dt;
            gvFer.DataBind();

        }

        public void BindSupervisor()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@RoleID", "11");
            ddlsupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;
            ddlsupervisor.DataTextField = "EmployeeName";
            ddlsupervisor.DataValueField = "ID";
            ddlsupervisor.DataBind();
            ddlsupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void btnAddTray_Click(object sender, EventArgs e)
        {

            try
            {
                // if (Convert.ToDouble(txtTrays.Text) <= Convert.ToDouble(lblUnMovedTrays.Text))
                //  {
                //lblerrmsg.Text = "";
                dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text, ddlUnit.SelectedItem.Text, txtTrays.Text, txtSQFT.Text);
                gvFerDetails.DataSource = dtTrays;
                gvFerDetails.DataBind();
                //   lblUnMovedTrays.Text = (Convert.ToInt32(lblUnMovedTrays.Text) - Convert.ToInt32(txtTrays.Text)).ToString();
                //  txtTrays.Text = "";
                ddlFertilizer.SelectedIndex = 0;
                ddlUnit.SelectedIndex = 0;
                txtQty.Text = "";
                txtSQFT.Text = "";
                //  }
                //  else
                //  {

                //lblerrmsg.Text = "Number of Trays exceed Remaining trays";

                // }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvFerDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                // string tray = (gvFerDetails.Rows[e.RowIndex].FindControl("lblTray") as Label).Text;
                // lblUnMovedTrays.Text = ((Convert.ToInt32(lblUnMovedTrays.Text) + Convert.ToInt32(tray)).ToString());
                dtTrays.Rows.RemoveAt(e.RowIndex);
                gvFerDetails.DataSource = dtTrays;
                gvFerDetails.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvFer_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvFer.Rows[rowIndex];
                lblUnMovedTrays.Text = (row.FindControl("lblTotTray") as Label).Text;
                // lblJobID.Text = (row.FindControl("lblID") as Label).Text;
                //  lblwo.Text= (row.FindControl("lblwo") as Label).Text;
                ddlsupervisor.Focus();
            }
        }

        protected void gvFer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            int FertilizationCode = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Mode", "12");
            dt = objCommon.GetDataTable("GET_Common", nv1);
            FertilizationCode = Convert.ToInt32(dt.Rows[0]["FCode"]);

            foreach (GridViewRow row in gvFer.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {

                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlsupervisor.SelectedValue);
                    nv.Add("@Type", radtype.SelectedValue);
                    nv.Add("@WorkOrder", (row.FindControl("lblwo") as Label).Text);
                    nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    //nv.Add("@WorkOrder", lblwo.Text);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                      nv.Add("@FertilizationCode", FertilizationCode.ToString());
                    result = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManual", nv);
                   

                }
            }

            objTask.AddFertilizerRequestDetails(dtTrays,"0", FertilizationCode);

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
            txtQty.Text = "";
            txtSQFT.Text = "";
            txtTrays.Text = "";
            radtype.SelectedValue = "Fertilizer";
            BindFertilizer();
            dtTrays.Clear();
        }

        protected void radtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radtype.SelectedValue == "Fertilizer")
            {
                lbltype.Text = "Fertilizer";
                dtTrays.Rows.Clear();
                gvFerDetails.DataSource = dtTrays;
                gvFerDetails.DataBind();
                BindFertilizer();
            }
            else if (radtype.SelectedValue == "Chemical")
            {
                lbltype.Text = "Chemical";
                dtTrays.Rows.Clear();
                gvFerDetails.DataSource = dtTrays;
                gvFerDetails.DataBind();
                BindChemical();
            }
        }

        public void BindChemical()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlFertilizer.DataSource = objFer.GetChemicalList();
            ddlFertilizer.DataTextField = "Name";
            ddlFertilizer.DataValueField = "No_";
            ddlFertilizer.DataBind();
            ddlFertilizer.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindFertilizer()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlFertilizer.DataSource = objFer.GetFertilizerList();
            ddlFertilizer.DataTextField = "Name";
            ddlFertilizer.DataValueField = "No_";
            ddlFertilizer.DataBind();
            ddlFertilizer.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindUnit()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlUnit.DataSource = objFer.GetUnitList();
            ddlUnit.DataTextField = "Description";
            ddlUnit.DataValueField = "Code";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem("--- Select ---", "0"));
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

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            userinput.Visible = true;
            ddlsupervisor.Focus();
            int tray = 0;
            foreach (GridViewRow row in gvFer.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    tray = tray + Convert.ToInt32((row.FindControl("lblTotTray") as Label).Text);
                }

            }
            txtTrays.Text = tray.ToString();


        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {

            txtSQFT.Text = Convert.ToString(1.23 * Convert.ToInt32(txtTrays.Text) * Convert.ToInt32(txtQty.Text));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridFerReq();
        }
        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            BindJobCode();
            Bindcname();
            BindBenchLocation();

            BindFacility();
            BindGridFerReq();
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
            BindGridFerReq();
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq();
        }
    }
}
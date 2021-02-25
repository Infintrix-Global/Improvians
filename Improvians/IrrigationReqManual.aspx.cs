using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Improvians.Bal;

namespace Improvians
{
    public partial class IrrigationReqManual : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Bindcname();
                //BindJobCode();
                //BindFacility();
                //BindBenchLocation();
                //BindGridIrrigation();
                BindFacility();                
                txtSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                BindSupervisorList();
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

        public void BindGridIrrigation()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@wo","");
            //  nv.Add("@JobCode",ddlJobNo.SelectedValue);
            //  nv.Add("@CustomerName",ddlCustomer.SelectedValue);
            //   nv.Add("@Facility",ddlFacility.SelectedValue);
            //  nv.Add("@Mode", "1");
            // dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", ddlFacility.SelectedValue);
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);

            dt = objCommon.GetDataTable("SP_GetIrrigationRequest", nv);

            DataTable dtManual = objFer.GetManualFertilizerRequest(ddlFacility.SelectedValue, ddlBenchLocation.SelectedValue, ddlJobNo.SelectedValue);
            if (dtManual != null && dtManual.Rows.Count > 0)
            {
                dt.Merge(dtManual);
                dt.AcceptChanges();
            }
            GridIrrigation.DataSource = dt;
            GridIrrigation.DataBind();
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


        public void BindJobCode(string ddlBench)
        {
            ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", ""));
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

       
        public void BindSupervisorList()
        {
            //NameValueCollection nv = new NameValueCollection();
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            //ddlSupervisor.DataTextField = "EmployeeName";
            //ddlSupervisor.DataValueField = "ID";
            //ddlSupervisor.DataBind();
            //ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            NameValueCollection nv = new NameValueCollection();
            if (Session["Role"].ToString() == "1")
            {
                ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlSupervisor.DataTextField = "EmployeeName";
                ddlSupervisor.DataValueField = "ID";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            if (Session["Role"].ToString() == "12")
            {
                ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlSupervisor.DataTextField = "EmployeeName";
                ddlSupervisor.DataValueField = "ID";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation();
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBenchLocation(ddlFacility.SelectedValue);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation();
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation();
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            ddlFacility.SelectedIndex = 0;
            ddlBenchLocation.SelectedIndex = 0;
            ddlCustomer.SelectedIndex = 0;
            ddlJobNo.SelectedIndex = 0;
            BindGridIrrigation();
        }
        protected void GridIrrigation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                // string rowIndex = e.CommandArgument.ToString();

                // wo = rowIndex;
                //DataTable dt = new DataTable();
                //NameValueCollection nv = new NameValueCollection();
                //nv.Add("@wo", wo);
                //nv.Add("@JobCode", ddlJobNo.SelectedValue);
                //nv.Add("@CustomerName", ddlCustomer.SelectedValue);
                //nv.Add("@Facility", ddlFacility.SelectedValue);
                //nv.Add("@Mode", "2");
                //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
                // txtIrrigatedNoTrays.Text = dt.Rows[0]["trays_actual"].ToString();
                //txtIrrigationDuration.Text = dt.Rows[0]["jobcode"].ToString();
                //  lblJobID.Text = dt.Rows[0]["jobcode"].ToString();

                int rowIndex = Convert.ToInt32(e.CommandArgument);
              //  lblJobID.Text = GridIrrigation.DataKeys[rowIndex].Values[1].ToString();
              //  lblGrowerID.Text = GridIrrigation.DataKeys[rowIndex].Values[2].ToString();


                txtNotes.Focus();

            }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{

                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);

                    nv.Add("@Jobcode", (row.FindControl("lbljobID") as Label).Text);
                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                    nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                    nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);

      
                   // nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    nv.Add("@IrrigatedNoTrays", (row.FindControl("lbltotTray") as Label).Text);
                    nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                    nv.Add("@IrrigationDuration", "");
                    nv.Add("@SprayDate", txtSprayDate.Text.Trim());
                    //nv.Add("@SprayTime", txtSprayTime.Text.Trim());
                    nv.Add("@Nots", txtNotes.Text.Trim());
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    result = objCommon.GetDataExecuteScaler("SP_AddIrrigationRequestManual", nv);
                    if (result > 0)
                    {
                        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                        //  lblmsg.Text = "Assignment Not Successful";
                    }
               // }
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
            // lblmsg.Text = "Assignment Successful";
            clear();
        }

        public void clear()
        {

            ddlSupervisor.SelectedIndex = 0;
            txtWaterRequired.Text = "";
            txtNotes.Text = "";
            //txtIrrigatedNoTrays.Text = "";
            //txtIrrigationDuration.Text = "";
            txtSprayDate.Text = "";
            //txtSprayTime.Text = "";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }

        protected void GridIrrigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridIrrigation.PageIndex = e.NewPageIndex;
            BindGridIrrigation();
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            userinput.Visible = true;
            ddlSupervisor.Focus();
            int tray = 0;
            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                    tray = tray + Convert.ToInt32((row.FindControl("lbltotTray") as Label).Text);
               // }

            }
            //txtIrrigatedNoTrays.Text = tray.ToString();


        }

        protected void chckchanged(object sender, EventArgs e)
        {
            CheckBox chckheader = (CheckBox)GridIrrigation.HeaderRow.FindControl("CheckBoxall");
            foreach (GridViewRow row in GridIrrigation.Rows)
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

    }
}
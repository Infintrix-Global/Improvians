﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.BAL_Classes;
using Evo.Bal;

namespace Evo
{
    public partial class FertilizerTaskReq : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Fdate = "", TDate = "";
                Fdate = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(7).ToString("yyyy-MM-dd");

                txtFromDate.Text = Fdate;
                txtToDate.Text = TDate;
                BindSupervisor();
                BindFertilizer();
                BindUnit();
                Bindcname();
                BindBenchLocation(Session["Facility"].ToString());
                BindJobCode(ddlBenchLocation.SelectedValue);
                BindGridFerReq();
                dtTrays.Clear();
            }
        }
        private string JobCode
        {
            get
            {
                if (Request.QueryString["jobId"] != null)
                {
                    return Request.QueryString["jobId"].ToString();
                }
                return "";
            }
            set
            {
                // JobCode = Request.QueryString["jobId"].ToString();
                // JobCode = value;
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

        public void BindJobCode(string ddlBench)
        {
            ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlJobNo.Items[0].Selected = false;
            ddlJobNo.ClearSelection();
        }

        public void BindBenchLocation(string ddlMain)
        {
            ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataTextField = "p2";
            ddlBenchLocation.DataValueField = "p2";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
            ddlBenchLocation.Items[0].Selected = false;
            ddlBenchLocation.ClearSelection();
        }
        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindJobCode(ddlBenchLocation.SelectedValue);
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

        public void BindGridFerReq()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@RequestType", RadioButtonListSourse.SelectedValue);
            nv.Add("@FromDate", txtFromDate.Text);
            nv.Add("@ToDate", txtToDate.Text);

            //int c = 0;
            //string x = "";
            //foreach (RepeaterItem item in repBench.Items)
            //{
            //    CheckBox chkBench = (CheckBox)item.FindControl("chkBench");
            //    if (chkBench.Checked)
            //    {
            //        c = 1;
            //        x += "'" +((HiddenField)item.FindControl("hdnValue")).Value + "',";

            //    }
            //}
            //if (c > 0)
            //{
            //    string chkSelected = x.Remove(x.Length - 1, 1);
            //    nv.Add("@BenchLocation", chkSelected);
            //}
            //else
            //{
            //    nv.Add("@BenchLocation", "0");
            //}
            if (Session["Role"].ToString() == "12")
            {
                dt = objCommon.GetDataTable("SP_GetFertilizerRequestAAssistantGrower", nv);
            }
            else
            {
                dt = objCommon.GetDataTable("SP_GetFertilizerRequest", nv);
            }

            gvFer.DataSource = dt;
            gvFer.DataBind();

            foreach (GridViewRow row in gvFer.Rows)
            {
                var checkJob = (row.FindControl("lblID") as Label).Text;
                if (checkJob == JobCode)
                {
                    row.CssClass = "highlighted";
                }
            }
        }

        public void BindGridFerDetails()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            dt = objCommon.GetDataTable("SP_GetFertilizerRequestDetails", nv);
            //gvJobHistory.DataSource = dt;
            // gvJobHistory.DataBind();
        }

        public void BindSupervisor()
        {

            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@RoleID", "11");
            //ddlsupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;
            //ddlsupervisor.DataTextField = "EmployeeName";
            //ddlsupervisor.DataValueField = "ID";
            //ddlsupervisor.DataBind();
            //ddlsupervisor.Items.Insert(0, new ListItem("--Select--", "0"));


            NameValueCollection nv = new NameValueCollection();
            if (Session["Role"].ToString() == "1")
            {
                ddlsupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlsupervisor.DataTextField = "EmployeeName";
                ddlsupervisor.DataValueField = "ID";
                ddlsupervisor.DataBind();
                ddlsupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            if (Session["Role"].ToString() == "12")
            {
                ddlsupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlsupervisor.DataTextField = "EmployeeName";
                ddlsupervisor.DataValueField = "ID";
                ddlsupervisor.DataBind();
                ddlsupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
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

            if (e.CommandName == "Job")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string BatchLocation = gvFer.DataKeys[rowIndex].Values[0].ToString();
                string jobCode = gvFer.DataKeys[rowIndex].Values[1].ToString();
                string FCode = gvFer.DataKeys[rowIndex].Values[2].ToString();


                Response.Redirect(String.Format("~/FerJobBuildUp.aspx?Bench={0}&jobCode={1}&FCode={2}", BatchLocation, jobCode, FCode));
            }

            if (e.CommandName == "GStart")
            {
                int FertilizationCode = 0;

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string BatchLocation = gvFer.DataKeys[rowIndex].Values[0].ToString();
                string jobCode = gvFer.DataKeys[rowIndex].Values[1].ToString();
                string FCode = gvFer.DataKeys[rowIndex].Values[2].ToString();

                string jid = gvFer.DataKeys[rowIndex].Values[3].ToString();


                if (FCode == "0")
                {


                    dtTrays.Clear();

                    NameValueCollection nv14 = new NameValueCollection();

                    nv14.Add("@Mode", "12");
                    DataTable dt1 = objCommon.GetDataTable("GET_Common", nv14);
                    FertilizationCode = Convert.ToInt32(dt1.Rows[0]["FCode"]);

                    dtTrays.Rows.Add("", "", "", "", "");

                    objTask.AddFertilizerRequestDetailsCreatTask(dtTrays, "0", FertilizationCode, BatchLocation, "", "", "", "", "");


                    long result2 = 0;
                    NameValueCollection nv4 = new NameValueCollection();
                    nv4.Add("@SupervisorID", Session["LoginID"].ToString());
                    nv4.Add("@Type", "Fertilizer");
                    nv4.Add("@Jobcode", jobCode);
                    nv4.Add("@Customer", "");
                    nv4.Add("@Item", "");
                    nv4.Add("@Facility","");
                    nv4.Add("@GreenHouseID", BatchLocation) ;
                    nv4.Add("@TotalTray", "");
                    nv4.Add("@TraySize", "");
                    nv4.Add("@Itemdesc", "");
                    //nv.Add("@WorkOrder", lblwo.Text);
                    nv4.Add("@LoginID", Session["LoginID"].ToString());
                    nv4.Add("@FertilizationCode", FertilizationCode.ToString());
                    nv4.Add("@FertilizationDate","");
                    nv4.Add("@seedDate", "");
                    nv4.Add("@Jid", jid);
                    result2 = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManualCreateTask", nv4);
                    Response.Redirect(String.Format("~/SprayTaskReq.aspx?FertilizationCode={0}", FertilizationCode));
                }
                else
                {
                    Response.Redirect(String.Format("~/SprayTaskReq.aspx?FertilizationCode={0}", FCode));
                }

            }
        }

        protected void gvFer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFer.PageIndex = e.NewPageIndex;
            BindGridFerReq();
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
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{

                long result = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", ddlsupervisor.SelectedValue);
                nv.Add("@Type", radtype.SelectedValue);
                nv.Add("@WorkOrder", (row.FindControl("lblwo") as Label).Text);
                nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                //nv.Add("@WorkOrder", lblwo.Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@FertilizationCode", FertilizationCode.ToString());
                result = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequest", nv);



                //  }

            }
            dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text, ddlUnit.SelectedItem.Text, txtTrays.Text, txtSQFT.Text);
            objTask.AddFertilizerRequestDetails(dtTrays, "0", FertilizationCode, "", "", "", "", "");

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
                // gvFerDetails.HeaderRow.Cells[0].Text = "Fertilizer";
                lbltype.Text = "Fertilizer";
                dtTrays.Rows.Clear();
                gvFerDetails.DataSource = dtTrays;
                gvFerDetails.DataBind();
                BindFertilizer();


            }
            else if (radtype.SelectedValue == "Chemical")
            {

                //gvFerDetails.HeaderRow.Cells[0].Text = "Chemical";
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
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                tray = tray + Convert.ToInt32((row.FindControl("lblTotTray") as Label).Text);
                //}

            }
            txtTrays.Text = tray.ToString();


        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {

            txtSQFT.Text = Convert.ToString(1.23 * Convert.ToInt32(txtTrays.Text) * Convert.ToInt32(txtQty.Text));
        }

        protected void btnManual_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FertilizerReqManual.aspx");
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            RadioButtonListSourse.Items[0].Selected = false;


            RadioButtonListSourse.ClearSelection();
            Bindcname();
            BindBenchLocation(Session["Facility"].ToString());
            BindJobCode(ddlBenchLocation.SelectedValue);

            BindGridFerReq();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridFerReq();
            BindGridFerDetails();
        }

        protected void btnJob_Click(object sender, EventArgs e)
        {
            //if(gvJobHistory.Visible==true)
            //{
            //    gvJobHistory.Visible = false;
            //}
            //else if (gvJobHistory.Visible == false)
            //{
            //    gvJobHistory.Visible = true;
            //}
        }

        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            BindGridFerReq();
        }

        protected void RadioButtonListSourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq();
        }



        protected void gvFer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblsource = (Label)e.Row.FindControl("lblsource");
                if (lblsource.Text == "Manual")
                {
                    lblsource.Text = "Navision";
                }
            }
        }
    }
}
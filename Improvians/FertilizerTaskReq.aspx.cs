using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.BAL_Classes;
using Evo.Bal;
using System.Data.SqlClient;
using System.Configuration;

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
                string Fdate = "", TDate = "", FRDate = "";
                if (string.IsNullOrWhiteSpace(JobCode) && string.IsNullOrWhiteSpace(benchLoc))
                {
                    Fdate = Convert.ToDateTime(System.DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd");
                    TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(14).ToString("yyyy-MM-dd");
                  //  txtFromDate.Text = Fdate;
                   // txtToDate.Text = TDate;
                }
                FRDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                // txtFertilizationDate.Text = FRDate;
              
                BindTaskType();
                BindSupervisor();
                BindFertilizer();
                BindUnit();
                BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
                BindJobCode("0", "0", "0");
                Bindcname("0", "0", "0");
                BindCrop();
                BindAssignByList("0", "0", "0");

                BindGridFerReq("0", 0);
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

        private string benchLoc
        {
            get
            {
                if (Request.QueryString["benchLoc"] != null)
                {
                    return Request.QueryString["benchLoc"].ToString();
                }
                return "";
            }
            set
            {
                // JobCode = Request.QueryString["jobId"].ToString();
                // JobCode = value;
            }
        }
        private string TaskKey
        {
            get
            {
                if (Request.QueryString["Tkey"] != null)
                {
                    return Request.QueryString["Tkey"].ToString();
                }
                return "";
            }
            set
            {
                // JobCode = Request.QueryString["jobId"].ToString();
                // JobCode = value;
            }
        }

        public void BindCrop()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@JobNo", ddlJobNo.SelectedValue);
            nv.Add("@GenusCode", "0");
            nv.Add("@Mode", "6");
            nv.Add("@Type", "Fer");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            ddlCrop.DataSource = dt;
            ddlCrop.DataTextField = "GenusCode";
            ddlCrop.DataValueField = "GenusCode";
            ddlCrop.DataBind();
            ddlCrop.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindAssignByList(string ddlBench, string jobNo, string Cust)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBench);
            nv.Add("@Customer", Cust);
            nv.Add("@JobNo", jobNo);
            nv.Add("@GenusCode", ddlCrop.SelectedValue);
            nv.Add("@Mode", "4");
            nv.Add("@Type", "Fer");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);

            ddlAssignedBy.DataSource = dt;
            ddlAssignedBy.DataTextField = "AssingTo";
            ddlAssignedBy.DataValueField = "AssingTo";
            ddlAssignedBy.DataBind();
            ddlAssignedBy.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlAssignedBy.Items.Insert(1, new ListItem("System", "System"));
        }
        public void Bindcname(string ddlBench, string jobNo, string Code)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", !string.IsNullOrEmpty(ddlBench) ? ddlBench : "0");
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@JobNo", !string.IsNullOrEmpty(jobNo) ? jobNo : "0");
            nv.Add("@GenusCode", !string.IsNullOrEmpty(Code) ? Code : "0");
            nv.Add("@Mode", "3");
            nv.Add("@Type", "Fer");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "Customer";
            ddlCustomer.DataValueField = "Customer";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        public void BindJobCode(string ddlBench, string Customer, string Code)
        {
            //  ddlJobNo.Items[0].Selected = false;
            // ddlJobNo.ClearSelection();
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", !string.IsNullOrEmpty(ddlBench) ? ddlBench : "0");
            nv.Add("@Customer", !string.IsNullOrEmpty(Customer) ? Customer : "0");
            nv.Add("@JobNo", "0");
            nv.Add("@GenusCode", !string.IsNullOrEmpty(Code) ? Code : "0");

            nv.Add("@Mode", "2");
            nv.Add("@Type", "Fer");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);
            //   ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataBind();

            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        public void BindBenchLocation(string ddlMain, string jobNo, string Customer, string Code)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", ddlMain);
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Customer", !string.IsNullOrEmpty(Customer) ? Customer : "0");
            nv.Add("@JobNo", !string.IsNullOrEmpty(jobNo) ? jobNo : "0");
            nv.Add("@GenusCode", !string.IsNullOrEmpty(Code) ? Code : "0");
            nv.Add("@Mode", "1");
            nv.Add("@Type", "Fer");

            dt = objCommon.GetDataTable("SP_TaskFilterSearch", nv);

            // ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            lstBenchLocation.DataSource = dt;
            lstBenchLocation.DataTextField = "BenchLocation";
            lstBenchLocation.DataValueField = "BenchLocation";
            lstBenchLocation.DataBind();
        }

        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBenchLocation.SelectedIndex == -1)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(getBenchLocation(), ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            BindGridFerReq("0", 1);
        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBenchLocation.SelectedIndex == -1)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(getBenchLocation(), ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridFerReq("0", 1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (lstBenchLocation.SelectedIndex == -1)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, "0", ddlCrop.SelectedValue);
            BindAssignByList("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridFerReq(ddlJobNo.SelectedValue, 1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            string Jno = "";
            if (txtSearchJobNo.Text == "")
            {
                Jno = ddlJobNo.SelectedValue;

            }
            else
            {
                Jno = txtSearchJobNo.Text;
            }

            BindGridFerReq(Jno, 1);
        }

        private string getBenchLocation()
        {
            int c = 0;
            string x = "";
            string chkSelected = "";
            foreach (ListItem item in lstBenchLocation.Items)
            {
                if (item.Selected)
                {
                    c = 1;
                    x += item.Text + ",";
                }
            }
            if (c > 0)
            {
                chkSelected = x.Remove(x.Length - 1, 1);
            }
            return chkSelected;
        }

        protected void lstBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindJobCode(getBenchLocation());
            string selectedValues = getBenchLocation();
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname(selectedValues, "0", "0");
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(selectedValues, "0", "0");
            if (ddlAssignedBy.SelectedIndex == 0)
                BindAssignByList(selectedValues, "0", "0");
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridFerReq(ddlJobNo.SelectedValue, 1);
        }

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq(ddlJobNo.SelectedValue, 1);
        }

        protected void RadioButtonListSourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq("0", 1);
        }

        protected void RadioButtonListF_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq("0", 1);
        }
        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            BindBenchLocation(Session["Facility"].ToString(), txtSearchJobNo.Text, "0", "0");
            BindGridFerReq(txtSearchJobNo.Text, 1);

        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            txtSearchJobNo.Text = "";
            RadioButtonListSourse.Items[0].Selected = false;
            RadioButtonListSourse.ClearSelection();
            RadioButtonListGno.Items[0].Selected = false;
            RadioButtonListGno.ClearSelection();
            BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
            Bindcname("0", "0", "0");
            // BindJobCode("0");
            BindJobCode("0", "0", "0");
            BindCrop();
            BindGridFerReq("0", 1);

        }

        protected void txtBatchLocation_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            //BindJobCode(getBenchLocation());
            Bindcname(getBenchLocation(), "0", "0");
            BindJobCode(getBenchLocation(), "0", "0");
            BindAssignByList(getBenchLocation(), "0", "0");
            BindGridFerReq(ddlJobNo.SelectedValue, 1);
        }


        public void BindGridFerReq(string JobCode, int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", JobCode);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", getBenchLocation());
            nv.Add("@Crop", ddlCrop.SelectedValue);
            nv.Add("@Status", "");
            nv.Add("@Jobsource", RadioButtonListSourse.SelectedValue);
            nv.Add("@GermNo", RadioButtonListGno.SelectedValue);
            nv.Add("@FromDate", txtFromDate.Text);
            nv.Add("@ToDate", txtToDate.Text);
            nv.Add("@AssignedBy", ddlAssignedBy.SelectedValue);


            if (Session["Role"].ToString() == "12")
            {
                dt = objCommon.GetDataTable("SP_GetFertilizerRequestAAssistantGrower", nv);
            }
            else
            {
                dt = objCommon.GetDataTable("SP_GetFertilizerRequest", nv);

                //dt = objTask.GetManualRequestStartfff(txtBenchLocationNew.Text, ddlJobNo.SelectedValue, Session["Facility"].ToString(), RadioButtonListSourse.SelectedValue, "D");

            }

            gvFer.DataSource = dt;
            gvFer.DataBind();


            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }
        }
        private void highlight(int limit)
        {
            var i = gvFer.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvFer.Rows)
            {
                var checkJob = (row.FindControl("lblID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                var tKey = (row.FindControl("lblTaskRequestKey") as Label).Text;

                i--;
                if (checkJob == JobCode && checklocation == benchLoc && tKey == TaskKey)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvFer.PageIndex++;
                    gvFer.DataBind();
                    highlight((limit - 10));
                }
            }
        }

        public void BindGridFerDetails()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", getBenchLocation());
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
            DataTable dt = new DataTable();
            nv.Add("@RoleID", Session["Role"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetRoleForAssignementFacility", nv); ;

            ddlsupervisor.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlsupervisor.DataTextField = "EmployeeName";
            ddlsupervisor.DataValueField = "ID";
            ddlsupervisor.DataBind();
            ddlsupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindTaskType()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@ID", "0");
            RadioButtonListGno.DataSource = objCommon.GetDataTable("SP_GetFertilizerRequestDateCountNo", nv); ;
            RadioButtonListGno.DataTextField = "DateCountNoName";
            RadioButtonListGno.DataValueField = "DateCountNo";
            RadioButtonListGno.DataBind();
            RadioButtonListGno.Items.Insert(0, new ListItem("--Select--", "0"));
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
                string TaskRequestKey = gvFer.DataKeys[rowIndex].Values[4].ToString();
                string AssignedBy = gvFer.DataKeys[rowIndex].Values[5].ToString();
                Response.Redirect(String.Format("~/FerJobBuildUp.aspx?Bench={0}&jobCode={1}&FCode={2}&TaskRequestKey={3}&AssignedBy={4}", BatchLocation, jobCode, FCode, TaskRequestKey, AssignedBy));
            }

            if (e.CommandName == "GStart")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string BatchLocation = gvFer.DataKeys[rowIndex].Values[0].ToString();
                string jobCode = gvFer.DataKeys[rowIndex].Values[1].ToString();
                string FCode = gvFer.DataKeys[rowIndex].Values[2].ToString();
                string TaskRequestKey = gvFer.DataKeys[rowIndex].Values[4].ToString();
                string AssignedBy = gvFer.DataKeys[rowIndex].Values[5].ToString();
                Response.Redirect(String.Format("~/FertilizerTaskStart.aspx?Bench={0}&jobCode={1}&FCode={2}&TaskRequestKey={3}&AssignedBy={4}", BatchLocation, jobCode, FCode, TaskRequestKey, AssignedBy));
            }
        }

        protected void gvFer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFer.PageIndex = e.NewPageIndex;
            BindGridFerReq("0", 1);
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

        protected void gvFer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblsource = (Label)e.Row.FindControl("lblsource");
                if (lblsource.Text == "Manual")
                {
                    lblsource.Text = "Navision";
                }

                Label lblGermDate = (Label)e.Row.FindControl("lblFertilizeDate");
                string dtimeString = Convert.ToDateTime(lblGermDate.Text).ToString("yyyy/MM/dd");

                DateTime dtime = Convert.ToDateTime(dtimeString);

                DateTime nowtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));

                if (nowtime > dtime)
                {
                    e.Row.CssClass = "overdue";
                }
            }
        }

        //public void CheckFdateSet()
        //{
        //    string name = "";
        //    string name1 = "";
        //    string lID = "";
        //    try
        //    {
        //        for (int i = 0; i < ddlBenchLocation.Items.Count; i++)
        //        {
        //            if (ddlBenchLocation.Items[i].Selected)
        //            {
        //                name += ddlBenchLocation.Items[i].Text + ",";
        //                lID += ddlBenchLocation.Items[i].Value + ",";
        //                name1 += "'" + ddlBenchLocation.Items[i].Text + "',";
        //            }
        //        }
        //        txtBenchLocationNew.Text = name1.Remove(name1.Length - 1, 1);

        //        //   txtFertilizationDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        //        //  PanelFertilizationDate.Visible = true;
        //        BindGridFerReq(0);

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //protected void btiFertilizationDate_Click(object sender, EventArgs e)
        //{
        //    objTask.UpdateFDate(txtBenchLocationNew.Text, txtFertilizationDate.Text);
        //    CheckFdateSet();

        //}

        //protected void RadioButtonListFdateChange_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataTable dt = new DataTable();
        //    if (RadioButtonListFdateChange.SelectedValue == "0")
        //    {
        //        dt = objTask.GetManualRequestStartfff(txtBenchLocationNew.Text, ddlJobNo.SelectedValue, Session["Facility"].ToString(), RadioButtonListSourse.SelectedValue, "A");
        //        txtFertilizationDate.Text = Convert.ToDateTime(dt.Rows[0]["FertilizeSeedDate"]).ToString("yyyy-MM-dd");
        //    }
        //    else if (RadioButtonListFdateChange.SelectedValue == "1")
        //    {

        //        dt = objTask.GetManualRequestStartfff(txtBenchLocationNew.Text, ddlJobNo.SelectedValue, Session["Facility"].ToString(), RadioButtonListSourse.SelectedValue, "D");
        //        txtFertilizationDate.Text = Convert.ToDateTime(dt.Rows[0]["FertilizeSeedDate"]).ToString("yyyy-MM-dd");
        //    }
        //    else
        //    {
        //        txtFertilizationDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        //    }
        //}

        //protected void btnFCReset_Click(object sender, EventArgs e)
        //{

        //    PanelFertilizationDate.Visible = false;
        //    txtBenchLocationNew.Text = "";
        //    BindGridFerReq(0);

        //}
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Evo"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //and t.[Location Code]= '" + Session["Facility"].ToString() + "'
                    //cmd.CommandText = "select distinct t.[Job No_] as jobcode  from[GTI$IA Job Tracking Entry] t, [GTI$Job] j where j.No_ = t.[Job No_] and j.[Job Status] = 2  " +
                    //" AND t.[Job No_] like '" + prefixText + "%'";


                    string Facility = HttpContext.Current.Session["Facility"].ToString();
                    cmd.CommandText = " select distinct GPD.jobcode from gti_jobs_seeds_plan GTS inner join GrowerPutAwayDetails GPD on GPD.wo=GTS.wo  where  GPD.FacilityID ='" + Facility + "'  AND GPD.jobcode like '%" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%' order by jobcode" +
                    "";

                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(sdr["jobcode"].ToString());
                        }
                    }
                    conn.Close();

                    return customers;
                }
            }
        }
    }
}
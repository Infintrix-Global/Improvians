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
                Fdate = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(7).ToString("yyyy-MM-dd");
                FRDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                txtFertilizationDate.Text = FRDate;
                txtFromDate.Text = Fdate;
                txtToDate.Text = TDate;
                BindSupervisor();
                BindFertilizer();
                BindUnit();
                Bindcname();
                BindBenchLocation(Session["Facility"].ToString());
                BindJobCode(ddlBenchLocation.SelectedValue);
                BindGridFerReq(0);
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
        private string TaskRequestKey
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

            DataTable dt = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataSource = dt;
            ddlBenchLocation.DataTextField = "p2";
            ddlBenchLocation.DataValueField = "p2";
            ddlBenchLocation.DataBind();

            //ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
            //ddlBenchLocation.Items[0].Selected = false;
            //ddlBenchLocation.ClearSelection();
        }
        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindJobCode(ddlBenchLocation.SelectedValue);
            BindGridFerReq(1);

        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq(1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq(1);
        }

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq(1);
        }
        public void BindGridFerReq(int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            //nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@BenchLocation", !string.IsNullOrEmpty(txtBenchLocationNew.Text) ? txtBenchLocationNew.Text : "0");
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
                // dt = objCommon.GetDataTable("SP_GetFertilizerRequest", nv);

                dt = objTask.GetManualRequestStartfff(txtBenchLocationNew.Text, ddlJobNo.SelectedValue, Session["Facility"].ToString(), RadioButtonListSourse.SelectedValue, "D");

            }

            gvFer.DataSource = dt;
            gvFer.DataBind();

            //foreach (GridViewRow row in gvFer.Rows)
            //{
            //    var checkJob = (row.FindControl("lblID") as Label).Text;
            //    if (checkJob == JobCode)
            //    {
            //        row.CssClass = "highlighted";
            //    }
            //}
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
                var tKey = gvFer.DataKeys[row.RowIndex].Values[4].ToString();

                i--;
                if (checkJob == JobCode && checklocation == benchLoc && tKey == TaskRequestKey)
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
                string TaskRequestKey = gvFer.DataKeys[rowIndex].Values[4].ToString();


                Response.Redirect(String.Format("~/FerJobBuildUp.aspx?Bench={0}&jobCode={1}&FCode={2}&TaskRequestKey={3}", BatchLocation, jobCode, FCode, TaskRequestKey));
            }

            if (e.CommandName == "GStart")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string BatchLocation = gvFer.DataKeys[rowIndex].Values[0].ToString();
                string jobCode = gvFer.DataKeys[rowIndex].Values[1].ToString();
                string FCode = gvFer.DataKeys[rowIndex].Values[2].ToString();
                string TaskRequestKey = gvFer.DataKeys[rowIndex].Values[4].ToString();

                Response.Redirect(String.Format("~/FertilizerTaskStart.aspx?Bench={0}&jobCode={1}&FCode={2}&TaskRequestKey={3}", BatchLocation, jobCode, FCode, TaskRequestKey));
            }
        }

        protected void gvFer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFer.PageIndex = e.NewPageIndex;
            BindGridFerReq(1);
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

            BindGridFerReq(1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridFerReq(1);
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
            txtFertilizationDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            BindGridFerReq(1);
        }

        protected void RadioButtonListSourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridFerReq(1);
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

        protected void ddlBenchLocation_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CheckFdateSet();
        }

        public void CheckFdateSet()
        {
            string name = "";
            string name1 = "";
            string lID = "";
            try
            {
                for (int i = 0; i < ddlBenchLocation.Items.Count; i++)
                {
                    if (ddlBenchLocation.Items[i].Selected)
                    {
                        name += ddlBenchLocation.Items[i].Text + ",";
                        lID += ddlBenchLocation.Items[i].Value + ",";
                        name1 += "'" + ddlBenchLocation.Items[i].Text + "',";
                    }
                }
                txtBenchLocationNew.Text = name1.Remove(name1.Length - 1, 1);

                //   txtFertilizationDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                //  PanelFertilizationDate.Visible = true;
                BindGridFerReq(0);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btiFertilizationDate_Click(object sender, EventArgs e)
        {
            objTask.UpdateFDate(txtBenchLocationNew.Text, txtFertilizationDate.Text);
            CheckFdateSet();

        }

        protected void RadioButtonListFdateChange_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (RadioButtonListFdateChange.SelectedValue == "0")
            {
                dt = objTask.GetManualRequestStartfff(txtBenchLocationNew.Text, ddlJobNo.SelectedValue, Session["Facility"].ToString(), RadioButtonListSourse.SelectedValue, "A");
                txtFertilizationDate.Text = Convert.ToDateTime(dt.Rows[0]["FertilizeSeedDate"]).ToString("yyyy-MM-dd");
            }
            else if (RadioButtonListFdateChange.SelectedValue == "1")
            {

                dt = objTask.GetManualRequestStartfff(txtBenchLocationNew.Text, ddlJobNo.SelectedValue, Session["Facility"].ToString(), RadioButtonListSourse.SelectedValue, "D");
                txtFertilizationDate.Text = Convert.ToDateTime(dt.Rows[0]["FertilizeSeedDate"]).ToString("yyyy-MM-dd");
            }
            else
            {
                txtFertilizationDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnFCReset_Click(object sender, EventArgs e)
        {

            PanelFertilizationDate.Visible = false;
            txtBenchLocationNew.Text = "";
            BindGridFerReq(0);

        }
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
                    cmd.CommandText = " select distinct jobcode from gti_jobs_seeds_plan where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "'  AND jobcode like '" + prefixText + "%' order by jobcode" +
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

        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            BindGridFerReq(1);
        }
    }
}
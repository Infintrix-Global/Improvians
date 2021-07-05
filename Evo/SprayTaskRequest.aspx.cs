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
    public partial class SprayTaskRequest : System.Web.UI.Page
    {

        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Fdate = "", TDate = "", FRDate = "";
                Fdate = Convert.ToDateTime(System.DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd");
                TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(14).ToString("yyyy-MM-dd");
                FRDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                // txtFertilizationDate.Text = FRDate;
              //  txtFromDate.Text = Fdate;
               // txtToDate.Text = TDate;
                BindTaskType();
                BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
                BindJobCode("0", "0", "0");
                Bindcname("0", "0", "0");
                BindCrop();
                BindAssignByList("0", "0", "0");

                txtSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");

                BindGridSprayReq("0",0);
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

            }
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

        public void BindCrop()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@Customer", ddlCustomer.SelectedValue);
            nv.Add("@JobNo", ddlJobNo.SelectedValue);
            nv.Add("@GenusCode", "0");
            nv.Add("@Mode", "6");
            nv.Add("@Type", "Fer");

            dt = objCommon.GetDataTable("SP_TaskFilterSearchSupervisor", nv);
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


            dt = objCommon.GetDataTable("SP_TaskFilterSearchSupervisor", nv);

            ddlAssignedBy.DataSource = dt;
            ddlAssignedBy.DataTextField = "AssingTo";
            ddlAssignedBy.DataValueField = "AssingTo";
            ddlAssignedBy.DataBind();
            ddlAssignedBy.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlAssignedBy.Items.Insert(1, new ListItem("System", "System"));
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

            dt = objCommon.GetDataTable("SP_TaskFilterSearchSupervisor", nv);
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

            dt = objCommon.GetDataTable("SP_TaskFilterSearchSupervisor", nv);
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
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@Customer", !string.IsNullOrEmpty(Customer) ? Customer : "0");
            nv.Add("@JobNo", !string.IsNullOrEmpty(jobNo) ? jobNo : "0");
            nv.Add("@GenusCode", !string.IsNullOrEmpty(Code) ? Code : "0");
            nv.Add("@Mode", "1");
            nv.Add("@Type", "Fer");

            dt = objCommon.GetDataTable("SP_TaskFilterSearchSupervisor", nv);

            // ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataSource = dt;
            ddlBenchLocation.DataTextField = "BenchLocation";
            ddlBenchLocation.DataValueField = "BenchLocation";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));


        }

        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBenchLocation.SelectedIndex == 0)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(ddlBenchLocation.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            BindGridSprayReq("0", 1);
        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBenchLocation.SelectedIndex == 0)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(ddlBenchLocation.SelectedValue, ddlCustomer.SelectedValue, ddlCrop.SelectedValue);
            BindAssignByList("0", "0", ddlCustomer.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridSprayReq("0", 1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlBenchLocation.SelectedIndex == 0)
                BindBenchLocation(Session["Facility"].ToString(), ddlJobNo.SelectedValue, "0", ddlCrop.SelectedValue);
            BindAssignByList("0", ddlJobNo.SelectedValue, ddlCrop.SelectedValue);
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridSprayReq(ddlJobNo.SelectedValue, 1);
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridSprayReq(ddlJobNo.SelectedValue, 1);
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindJobCode(ddlBenchLocation.SelectedValue);
            if (ddlCustomer.SelectedIndex == 0)
                Bindcname(ddlBenchLocation.SelectedValue, "0", "0");
            if (ddlJobNo.SelectedIndex == 0)
                BindJobCode(ddlBenchLocation.SelectedValue, "0", "0");
            if (ddlAssignedBy.SelectedIndex == 0)
                BindAssignByList(ddlBenchLocation.SelectedValue, "0", "0");
            if (ddlCrop.SelectedIndex == 0)
                BindCrop();
            BindGridSprayReq(ddlJobNo.SelectedValue, 1);
        }

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridSprayReq(ddlJobNo.SelectedValue, 1);
        }

        protected void RadioButtonListSourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridSprayReq("0", 1);
        }

        protected void RadioButtonListF_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridSprayReq("0", 1);
        }
        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            BindBenchLocation(Session["Facility"].ToString(), txtSearchJobNo.Text, "0", "0");
            BindGridSprayReq(txtSearchJobNo.Text, 1);

        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            RadioButtonListSourse.Items[0].Selected = false;
            RadioButtonListSourse.ClearSelection();
            RadioButtonListGno.Items[0].Selected = false;
            RadioButtonListGno.ClearSelection();
            BindBenchLocation(Session["Facility"].ToString(), "0", "0", "0");
            Bindcname("0", "0", "0");
            // BindJobCode("0");
            BindJobCode("0", "0", "0");
            BindCrop();
            BindGridSprayReq("0", 1);

        }

        protected void txtBatchLocation_TextChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            //BindJobCode(ddlBenchLocation.SelectedValue);
            Bindcname(ddlBenchLocation.SelectedValue, "0", "0");
            BindJobCode(ddlBenchLocation.SelectedValue, "0", "0");
            BindAssignByList(ddlBenchLocation.SelectedValue, "0", "0");
            BindGridSprayReq(ddlJobNo.SelectedValue, 1);
        }

        public void BindGridSprayReq(string JobCode, int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
        
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@JobCode", JobCode);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@Crop", ddlCrop.SelectedValue);
            nv.Add("@Status", "");
            nv.Add("@Jobsource", RadioButtonListSourse.SelectedValue);
            nv.Add("@GermNo", RadioButtonListGno.SelectedValue);
            nv.Add("@FromDate", txtFromDate.Text);
            nv.Add("@ToDate", txtToDate.Text);
            nv.Add("@AssignedBy", ddlAssignedBy.SelectedValue);
            dt = objCommon.GetDataTable("SP_GetSprayRequestDetailsNew", nv);
            gvSpray.DataSource = dt;
            gvSpray.DataBind();

            //foreach (GridViewRow row in gvSpray.Rows)
            //{
            //    var checkJob = (row.FindControl("lblGreenHouseID") as Label).Text;
            //    if (checkJob == benchLoc)
            //    {
            //        row.CssClass = "highlighted";
            //    }
            //}

            if (p != 1 && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }
        }
        private void highlight(int limit)
        {
            var i = gvSpray.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvSpray.Rows)
            {
                //var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                var tKey = gvSpray.DataKeys[row.RowIndex].Values[1].ToString();

                i--;
                if (checklocation == benchLoc && tKey == TaskKey)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvSpray.PageIndex++;
                    gvSpray.DataBind();
                    highlight((limit - 10));
                }
            }
        }


        protected void gvSpray_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string FertilizationCode = gvSpray.DataKeys[rowIndex].Values[0].ToString();
                Response.Redirect(String.Format("~/SprayTaskReq.aspx?FertilizationCode={0}", FertilizationCode));
                //userinput.Visible = true;


                //lblGrowerID.Text = gvSpray.DataKeys[rowIndex].Values[0].ToString();

                //txtNotes.Focus();

            }

            if (e.CommandName == "ViewDetails")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string FertilizationCode = gvSpray.DataKeys[rowIndex].Values[0].ToString();
                string TaskRequestKey = gvSpray.DataKeys[rowIndex].Values[1].ToString();

                //  Response.Redirect(String.Format("~/SprayTaskViewDetails.aspx?FertilizationCode={0}", FertilizationCode));
                //userinput.Visible = true;

                Response.Redirect(String.Format("~/SprayTaskViewDetails.aspx?PageType={0}&FertilizationCode={1}&FCID={2}&TaskRequestKey={3}", "ManageTask", FertilizationCode, 0, TaskRequestKey));

                //lblGrowerID.Text = gvSpray.DataKeys[rowIndex].Values[0].ToString();

                //txtNotes.Focus();

            }
        }

        protected void gvSpray_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSpray.PageIndex = e.NewPageIndex;
            BindGridSprayReq("0",1);
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }


        public void clear()
        {


            txtNotes.Text = "";

            txtSprayDate.Text = "";

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@SupervisorID", Session["LoginID"].ToString());

            nv.Add("@GrowerPutAwayId", lblGrowerID.Text);

            nv.Add("@SprayDate", txtSprayDate.Text.Trim());

            nv.Add("@Nots", txtNotes.Text.Trim());
            nv.Add("@LoginID", Session["LoginID"].ToString());

            result = objCommon.GetDataInsertORUpdate("SP_AddSprayRequest", nv);
            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                string message = "Completion Successful";
                string url = "SprayTaskRequest.aspx";
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
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('not Completion')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }
        }

   

        //protected void btnSearchRest_Click(object sender, EventArgs e)
        //{
        //    //Bindcname();
        //    //BindJobCode();
        //    //BindFacility();
        //    //BindBenchLocation();
        //    BindGridSprayReq(1);
        //}

        protected void gvSpray_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGermDate = (Label)e.Row.FindControl("lblFDate");
                string dtimeString = Convert.ToDateTime(lblGermDate.Text).ToString("yyyy/MM/dd");
                Label lblFertilizationCode = (Label)e.Row.FindControl("lblFertilizationCode");
                Label lblTray = (Label)e.Row.FindControl("lblTray");
                DateTime dtime = Convert.ToDateTime(dtimeString);

                DateTime nowtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));

                if (nowtime > dtime)
                {
                    e.Row.CssClass = "overdue";
                }
                //Label lblFertilizationCode = (Label)e.Row.FindControl("lblFertilizationCode");
                //GridView GridViewFields = e.Row.FindControl("GridViewDetails") as GridView;
                //GridView GridViewFShow = e.Row.FindControl("GridViewFShow") as GridView;

                //DataTable dt = new DataTable();
                //NameValueCollection nv = new NameValueCollection();
                //nv.Add("@FertilizationId", lblFertilizationCode.Text);

                //dt = objCommon.GetDataTable("SP_GetSprayRequestFerChemDetails", nv);

                //GridViewFields.DataSource = dt;
                //GridViewFields.DataBind();

                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();

                nv.Add("@FertilizationCode", lblFertilizationCode.Text);
                dt = objCommon.GetDataTable("SP_GetFertilizerBenchLocationView", nv);
                int tray = 0;
                foreach (DataRow row in dt.Rows)
                {
                    tray += Convert.ToInt32(row["Trays"]);
                }

                lblTray.Text = tray.ToString();


                //DataTable dt1 = new DataTable();
                //NameValueCollection nv1 = new NameValueCollection();
                ////nv1.Add("@JobCode", ddlJobNo.SelectedValue);
                ////nv1.Add("@CustomerName", ddlCustomer.SelectedValue);
                ////nv1.Add("@Facility", ddlFacility.SelectedValue);
                ////nv1.Add("@LoginID", Session["LoginID"].ToString());
                ////nv1.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
                ////nv1.Add("@FertilizationCode", lblFertilizationCode.Text);
                //nv1.Add("@FertilizationId", lblFertilizationCode.Text);

                //dt1 = objCommon.GetDataTable("SP_GetSprayRequestGreenHouseDetails", nv1);
                //GridViewFShow.DataSource = dt1;
                //GridViewFShow.DataBind();
            }
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
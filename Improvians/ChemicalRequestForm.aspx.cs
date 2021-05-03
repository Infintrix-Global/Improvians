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

namespace Evo
{
    public partial class ChemicalRequestForm : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };


        public static DataTable dtCTrays = new DataTable()
        { Columns = { "Fertilizer", "Tray", "SQFT" } };
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

        public void BindGridFerReq(int p)
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
                dt = objCommon.GetDataTable("SP_GetChemicalRequestAssistantGrower", nv);
            }
            else
            {
                dt = objCommon.GetDataTable("SP_GetChemicalRequest", nv);
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



        // Show Details
        private void highlight(int limit)
        {
            var i = gvFer.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvFer.Rows)
            {
                var checkJob = (row.FindControl("lblID") as Label).Text;
                var checklocation = (row.FindControl("lblBatchlocation1") as Label).Text;
                i--;
                if (checkJob == JobCode && checklocation == benchLoc)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit>= 10)
                {
                    gvFer.PageIndex++;
                    gvFer.DataBind();
                    highlight((limit-10));
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





        protected void gvFer_RowCommand(object sender, GridViewCommandEventArgs e)
        {



            if (e.CommandName == "Job")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string BatchLocation = gvFer.DataKeys[rowIndex].Values[0].ToString();
                string jobCode = gvFer.DataKeys[rowIndex].Values[1].ToString();
                string CCode = gvFer.DataKeys[rowIndex].Values[2].ToString();
                string TaskRequestKey = gvFer.DataKeys[rowIndex].Values[4].ToString();

                
                //  Response.Redirect(String.Format("~/ChemicalJobBuildUp.aspx?Bench={0}&jobCode={1},&CCode={2}", BatchLocation, jobCode, CCode));
                Response.Redirect(String.Format("~/ChemicalJobBuildUp.aspx?Bench={0}&jobCode={1}&CCode={2}&TaskRequestKey={3}", BatchLocation, jobCode, CCode, TaskRequestKey));
            }

            if (e.CommandName == "GStart")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string BatchLocation = gvFer.DataKeys[rowIndex].Values[0].ToString();
                string jobCode = gvFer.DataKeys[rowIndex].Values[1].ToString();
                string CCode = gvFer.DataKeys[rowIndex].Values[2].ToString();
                string TaskRequestKey = gvFer.DataKeys[rowIndex].Values[4].ToString();

                Response.Redirect(String.Format("~/ChemicalStart.aspx?Bench={0}&jobCode={1}&CCode={2}&TaskRequestKey={3}", BatchLocation, jobCode, CCode, TaskRequestKey));
         

            }
        }

        protected void gvFer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFer.PageIndex = e.NewPageIndex;
            BindGridFerReq(1);
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

        protected void btnManual_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ChemicalReqManual.aspx");
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
            }
        }

        protected void btnManual_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/ChemicalReqManual.aspx");
        }
    }
}
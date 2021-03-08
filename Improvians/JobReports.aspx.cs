using Evo.Admin.BAL_Classes;
using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class JobReports : System.Web.UI.Page
    {
        CommonControlNavision objNav = new CommonControlNavision();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string JobCode = Request.QueryString["jobCode"];
                if (string.IsNullOrEmpty(JobCode))
                {
                    divFilter.Visible = true;

                    BindJobCode();
                }
                else
                {
                    BindGridOne(JobCode);
                    JobCode= Request.QueryString["jobCode"];
                }

            }
        }

        private string JobCode
        {
            get
            {
                if (ViewState["JobCode"] != null)
                {
                    return (string)ViewState["JobCode"];
                }
                return "";
            }
            set
            {
                ViewState["JobCode"] = value;
            }
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

        //public void BindFacility()
        //{

        //    DataTable dt = new DataTable();
        //    NameValueCollection nv = new NameValueCollection();

        //    nv.Add("@Mode", "9");
        //    dt = objCommon.GetDataTable("GET_Common", nv);
        //    ddlFacility.DataSource = dt;
        //    ddlFacility.DataTextField = "loc_seedline";
        //    ddlFacility.DataValueField = "loc_seedline";
        //    ddlFacility.DataBind();
        //    ddlFacility.Items.Insert(0, new ListItem("--Select--", "0"));

        //}

        //public void BindBenchLocation()
        //{

        //    DataTable dt = new DataTable();
        //    NameValueCollection nv = new NameValueCollection();

        //    nv.Add("@Mode", "10");
        //    dt = objCommon.GetDataTable("GET_Common", nv);
        //    ddlBenchLocation.DataSource = dt;
        //    ddlBenchLocation.DataTextField = "GreenHouseID";
        //    ddlBenchLocation.DataValueField = "GreenHouseID";
        //    ddlBenchLocation.DataBind();
        //    ddlBenchLocation.Items.Insert(0, new ListItem("--Select--", "0"));

        //}
        public void BindGridOne(string jobCode)
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobID", jobCode);
            DataSet ds = objCommon.GetDataSet("GetJobTracibilityReport", nv);
            dt = ds.Tables[0];
            dt2 = ds.Tables[1];
            dt3 = ds.Tables[2];
            dt4 = ds.Tables[3];
            dt5 = ds.Tables[4];
            gv1.DataSource = dt;
            GV2.DataSource = dt2;
            Gv3.DataSource = dt3;
            GV4.DataSource = dt4;
            GV5.DataSource = dt5;
            gv1.DataBind();
            GV2.DataBind();
            Gv3.DataBind();
            GV4.DataBind();
            GV5.DataBind();

        }

        //protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindBenchLocation(ddlFacility.SelectedValue);

        //}

        //protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindJobCode(ddlBenchLocation.SelectedValue);


        //}
        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridOne(ddlJobNo.SelectedValue);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridOne(txtJobNo.Text.Trim());
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            txtJobNo.Text = "";
            BindGridOne(txtJobNo.Text);
        }
    }
}
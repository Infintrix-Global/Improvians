using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class CropReportRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                // BindFacility();
                BindGridPlantReady(0);
              
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

        public void BindGridPlantReady(int p)
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@wo", "");
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());

            // nv.Add("@Mode", "7");
            // dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);


            dt = objCommon.GetDataTable("SP_GetCropReportRequestAssistantGrower", nv);


            gvPlantReady.DataSource = dt;
            gvPlantReady.DataBind();




            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }


        }
        private void highlight(int limit)
        {
            var i = gvPlantReady.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvPlantReady.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblBenchLoc") as Label).Text;
                i--;
                if (checkJob == JobCode && checklocation == benchLoc)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvPlantReady.PageIndex++;
                    gvPlantReady.DataBind();
                    highlight((limit - 10));
                }
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

    
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();

            BindGridPlantReady(1);
        }
        protected void gvPlantReady_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string ChId = "";
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string Did = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();
                string BatchLoc = gvPlantReady.DataKeys[rowIndex].Values[4].ToString();
                string JobCode = gvPlantReady.DataKeys[rowIndex].Values[3].ToString();
                Response.Redirect(String.Format("~/CropHealthReport.aspx?BatchLoc={0}&JobCode={1}&CropAT={2}", "'" + BatchLoc + "'", JobCode, Did));


            }

         

        }



        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlantReady.PageIndex = e.NewPageIndex;
            BindGridPlantReady(1);
        }


    }

}




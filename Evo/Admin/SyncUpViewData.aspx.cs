using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Evo.Admin
{
    public partial class SyncUpViewData : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["SyncDate"] != null)
            {
                SyncDate = Request.QueryString["SyncDate"].ToString();
            }
            if (!IsPostBack)
            {
                BindFacility();
                BindGridBanchLoaction();
            }
        }

        private string SyncDate
        {
            get
            {
                if (ViewState["SyncDate"] != null)
                {
                    return (string)ViewState["SyncDate"];
                }
                return "";
            }
            set
            {
                ViewState["SyncDate"] = value;
            }
        }

        public void BindGridBanchLoaction()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@BanchLocation", "");
            nv.Add("@CreateDate", SyncDate);
            nv.Add("@Facility", ddlFacility.SelectedValue);
            nv.Add("@Mode", "1");
            dt = objCommon.GetDataTable("SP_Getgti_jobs_seeds_plan_Manual", nv);

            GridSyncUpdate.DataSource = dt;
            GridSyncUpdate.DataBind();

            count.Text = "Total : " + dt.Rows.Count.ToString();
        }

        public void BindFacility()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@CreateDate", "");
            nv.Add("@BanchLocation", "");
            nv.Add("@Facility", "");
            nv.Add("@Mode", "2");
            dt = objCommon.GetDataTable("SP_Getgti_jobs_seeds_plan_Manual", nv);

            ddlFacility.ClearSelection();
            ddlFacility.DataSource = dt;
            ddlFacility.DataTextField = "loc_seedline";
            ddlFacility.DataValueField = "loc_seedline";
            ddlFacility.DataBind();
            ddlFacility.Items.Insert(0, new ListItem("--Select--", "0"));



        }
        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridBanchLoaction();
        }

        protected void GridSyncUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridSyncUpdate.PageIndex = e.NewPageIndex;
            BindGridBanchLoaction();
        }
    }
}
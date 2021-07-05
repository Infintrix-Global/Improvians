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
    public partial class ViewPutAway : System.Web.UI.Page
    {

        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() == "13")
            {
                this.Page.MasterPageFile = "~/Customer/CustomerMaster.master";
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["GrowerPutAwayId"] != null)
                {
                    GrowerPutAwayId = Request.QueryString["GrowerPutAwayId"].ToString();
                }

                BindGridgvPutAway();
                BindGridPutAwayDetails();
                BindGridPutAwayCompletionDetails();
            }


        }

        private string GrowerPutAwayId
        {
            get
            {
                if (ViewState["GrowerPutAwayId"] != null)
                {
                    return (string)ViewState["GrowerPutAwayId"];
                }
                return "";
            }
            set
            {
                ViewState["GrowerPutAwayId"] = value;
            }
        }

        public void BindGridgvPutAway()
        {
            string ChId = "";
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@GrowerPutAwayId", GrowerPutAwayId);
            nv.Add("@Mode","1");
            dt = objCommon.GetDataTable("SP_GetPutAwayViewDetails", nv);
            gvPutAway.DataSource = dt;
            gvPutAway.DataBind();
        }

        public void BindGridPutAwayDetails()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@GrowerPutAwayId", GrowerPutAwayId);
            nv.Add("@Mode", "2");
            dt = objCommon.GetDataTable("SP_GetPutAwayViewDetails", nv);
            GridViewDetails.DataSource = dt;
            GridViewDetails.DataBind();

        }

        public void BindGridPutAwayCompletionDetails()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@GrowerPutAwayId", GrowerPutAwayId);
            nv.Add("@Mode", "3");
            dt = objCommon.GetDataTable("SP_GetPutAwayViewDetails", nv);
            if (dt != null && dt.Rows.Count > 0)
            {
                PanlTaskComplition.Visible = true;
                GridViewCompletion.DataSource = dt;
                GridViewCompletion.DataBind();
            }
        }

    }
}
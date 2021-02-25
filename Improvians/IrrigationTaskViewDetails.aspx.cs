using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Improvians
{
    public partial class IrrigationTaskViewDetails : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["IrrigationCode"] != null)
                {
                    IrrigationCode = Request.QueryString["IrrigationCode"].ToString();
                }
                BindGridViewDetailsGerm();
                BindenchLocation();
                BindgvIrrigation();

            }
        }




        public void BindenchLocation()
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@IrrigationCode", IrrigationCode);
            dt1 = objCommon.GetDataTable("SP_GetIrrigationRequestGreenHouseDetails", nv1);
            lblBenchLocation.Text = dt1.Rows[0]["GreenHouseID"].ToString();

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

        private string IrrigationCode
        {
            get
            {
                if (ViewState["IrrigationCode"] != null)
                {
                    return (string)ViewState["IrrigationCode"];
                }
                return "";
            }
            set
            {
                ViewState["IrrigationCode"] = value;
            }
        }


        public void BindGridViewDetailsGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@wo", "");
            //nv.Add("@JobCode", ddlJobNo.SelectedValue);
            //nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            //nv.Add("@Facility", ddlFacility.SelectedValue);
            nv.Add("@IrrigationCode", IrrigationCode);
            //nv.Add("@Mode", "6");
            //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            dt = objCommon.GetDataTable("SP_GetOperatorIrrigationTaskViewDetails", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        public void BindgvIrrigation()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@wo", wo);
            //nv.Add("@JobCode", "");
            //nv.Add("@CustomerName", "");
            //nv.Add("@Facility", "");
            //nv.Add("@Mode", "5");
            //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            nv.Add("@IrrigationCode", IrrigationCode);
            dt = objCommon.GetDataTable("SP_GetOperatorIrrigationTaskByIrrigationCode", nv);

            gvIrrigation.DataSource = dt;
            gvIrrigation.DataBind();

          
        }

        protected void gvIrrigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIrrigation.PageIndex = e.NewPageIndex;
            BindgvIrrigation();
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
           
            if (Session["Role"].ToString() == "3")
            {
                Response.Redirect("~/IrrigationCompletionForm.aspx");
            }


        }


    }
}
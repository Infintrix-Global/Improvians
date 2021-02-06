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
    public partial class IrrigationCompletionForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["WOId"] != null)
                {
                    wo = Request.QueryString["WOId"].ToString();
                }

                BindgvIrrigation();

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

        public void BindgvIrrigation()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@wo", wo);
            nv.Add("@JobCode", "");
            nv.Add("@CustomerName", "");
            nv.Add("@Facility", "");
            nv.Add("@Mode", "5");
            dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            gvIrrigation.DataSource = dt;
            gvIrrigation.DataBind();

        }

        protected void gvIrrigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIrrigation.PageIndex = e.NewPageIndex;
            BindgvIrrigation();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            long result = 0;
       
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@OperatorID", Session["LoginID"].ToString());
            nv.Add("@wo", wo);
            nv.Add("@SprayDate",txtSprayDate.Text.Trim());
            nv.Add("@TraysSprayed",txtTraysSprayed.Text.Trim());
            nv.Add("@SprayDuration",txtSprayDuration.Text.Trim());
            nv.Add("@LoginID", Session["LoginID"].ToString());
            if (Request.QueryString["ICom"] =="1")
            {
                nv.Add("@mode", "1");

            }
            else
            {
                nv.Add("@mode", "3");
            }

            result = objCommon.GetDataInsertORUpdate("SP_AddIrrigationTaskAssignment", nv);

         
            if (result > 0)
            {
                // lblmsg.Text = "Completion Successful";
                clear();
                string message = "Completion Successful";
                string url;
                if (Session["Role"].ToString() == "3")
                {
                    url = "IrrigationCompletionForm.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }



            }
            else
            {
                lblmsg.Text = "Completion Not Successful";
            }
        }

        public void clear()
        {
            txtSprayDate.Text = "";
            txtSprayDuration.Text = "";
            txtTraysSprayed.Text = "";


        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            if (Session["Role"].ToString() == "3")
            {
                Response.Redirect("~/IrrigationCompletionForm.aspx");
            }


        }

    
    }
}
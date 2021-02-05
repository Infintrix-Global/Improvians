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
    public partial class GerminationCompletionForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridGerm();

            }
        }

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("SP_GetGreenHouseOperatorGerminationTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string Wo = "";
            string GTAID = "";
            long result = 0;
            if (e.CommandName == "Start")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvGerm.Rows[rowIndex];
                GTAID = (row.FindControl("lblID") as Label).Text;
                Response.Redirect(String.Format("~/GreenHouseTaskCompletion.aspx?GTAID={0}", GTAID));
            }
        }
    }
}
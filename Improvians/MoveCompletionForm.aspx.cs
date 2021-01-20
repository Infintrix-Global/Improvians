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
    public partial class MoveCompletionForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridMove();
            }
        }

        public void BindGridMove()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@MoveID", Session["MoveID"].ToString());
            dt = objCommon.GetDataTable("SP_GetShipmentPlannerTaskByMoveID", nv);
            gvMove.DataSource = dt;
            gvMove.DataBind();
        }

        protected void gvMove_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMove.PageIndex = e.NewPageIndex;
            BindGridMove();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
    }
}
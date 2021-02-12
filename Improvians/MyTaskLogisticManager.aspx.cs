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
    public partial class MyTaskLogisticManager : System.Web.UI.Page
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
            // nv.Add("@Mode", "2");
            dt = objCommon.GetDataTable("SP_GetMoveSiteTeamTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Assign")
            {

                if (Session["Role"].ToString() == "5")
                {
                    // string Wid = "";
                    // Wid = e.CommandArgument.ToString();
                    string GrowerPutAwayId = e.CommandArgument.ToString();
                    Response.Redirect(String.Format("~/MoveTaskAssignment.aspx?GrowerPutAwayId={0}", GrowerPutAwayId));

                }
            }
            if (e.CommandName == "Select")
            {

                if (Session["Role"].ToString() == "5")
                {
                    // string Wid = "";
                    // Wid = e.CommandArgument.ToString();
                    NameValueCollection nv = new NameValueCollection();
                    string GrowerPutAwayId = e.CommandArgument.ToString();
                    // Session["MoveID"] = e.CommandArgument.ToString();
                    nv.Add("@CoordinatorId", Session["LoginID"].ToString());
                    nv.Add("@GrowerPutAwayId", GrowerPutAwayId);
                    nv.Add("@CreateBy", Session["LoginID"].ToString());
                    long result = objCommon.GetDataInsertORUpdate("SP_AddAssign_Task_Shipping_Coordinator", nv);
                    if (result > 0)
                    {
                        Response.Redirect(String.Format("~/MoveCompletionForm.aspx?GrowerPutAwayId={0}", GrowerPutAwayId));
                    }
                }
            }
        }


        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();

        }
    }
}
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
    public partial class MyTaskShippingCoordinator : System.Web.UI.Page
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
           // nv.Add("@Mode", "3");
           // dt = objCommon.GetDataTable("SP_GetGreenHouseLogisticTask", nv);
            dt = objCommon.GetDataTable("SP_GetShippingCoordinatorTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                //string Wid = "";
              //  Wid = e.CommandArgument.ToString();
              string GrowerPutAwayId = e.CommandArgument.ToString();
                //Session["MoveID"] = e.CommandArgument.ToString();
                
                Response.Redirect(String.Format("~/MoveCompletionForm.aspx?GrowerPutAwayId={0}", GrowerPutAwayId));
                //Session["MoveID"] = e.CommandArgument.ToString();
                //Response.Redirect("~/MoveCompletionForm.aspx");
            }
        }


        //protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        Label lbljstatus = (Label)e.Row.FindControl("lbljstatus");
        //        Label lblTitla = (Label)e.Row.FindControl("lblTitla");

        //        if (lbljstatus.Text == "3")
        //        {
        //            lblTitla.Text = "Move Completion";
        //        }


        //    }
        //}
    }
}
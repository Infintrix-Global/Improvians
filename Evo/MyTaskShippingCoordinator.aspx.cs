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
    public partial class MyTaskShippingCoordinator : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridGerm(0);
                
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


        public void BindGridGerm(int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            // nv.Add("@Mode", "3");
            // dt = objCommon.GetDataTable("SP_GetGreenHouseLogisticTask", nv);
            dt = objCommon.GetDataTable("SP_GetShippingCoordinatorTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }
        }

        private void highlight(int limit)
        {
            var i = gvGerm.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvGerm.Rows)
            {
                var checkJob = (row.FindControl("lblID") as Label).Text;
                 var checklocation = (row.FindControl("lblGreenHouseName") as Label).Text;
                //var tKey = gvGerm.DataKeys[row.RowIndex].Values[0].ToString();
                i--;
                if (checkJob == JobCode && checklocation == benchLoc)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvGerm.PageIndex++;
                    gvGerm.DataBind();
                    highlight((limit - 10));
                }
            }
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

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGermDate = (Label)e.Row.FindControl("lblSeededDate");
                string dtimeString = Convert.ToDateTime(lblGermDate.Text).ToString("yyyy/MM/dd");

                DateTime dtime = Convert.ToDateTime(dtimeString);

                DateTime nowtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));

                if (nowtime > dtime)
                {
                    e.Row.CssClass = "overdue";
                }
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
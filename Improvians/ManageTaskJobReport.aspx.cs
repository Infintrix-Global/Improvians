using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class ManageTaskJobReport : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        public static DataTable AllData = new DataTable();
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
            // nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());

            

            AllData = objCommon.GetDataTable("GetManageTaskJobHistory", nv);
            gvGerm.DataSource = AllData;
            gvGerm.DataBind();

        }



        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }

      

        //protected void gvGerm_RowDataBound1(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        Label lblstatus = (Label)e.Row.FindControl("lblstatus");
        //        Label lblStatusValues = (Label)e.Row.FindControl("lblStatusValues");
        //        Label lblPudawayDate = (Label)e.Row.FindControl("lblPudawayDate");
        //        Label lblPutawayStatusValues = (Label)e.Row.FindControl("lblPutawayStatusValues");
        //        if (lblStatusValues.Text == "1" || lblStatusValues.Text == "2")
        //        {
        //            lblstatus.Text = "Completed";
        //        }
        //        else
        //        {
        //            lblstatus.Text = "Pending";
        //        }

        //        if (lblStatusValues.Text == "2")
        //        {
        //            lblPudawayDate.Text = lblPudawayDate.Text;
        //        }
        //        else
        //        {
        //            lblPudawayDate.Text = "Pending";
        //        }

        //    }
        //}

       
    }
}
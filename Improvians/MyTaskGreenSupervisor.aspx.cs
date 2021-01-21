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
    public partial class MyTaskGreenSupervisor : System.Web.UI.Page
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
            dt = objCommon.GetDataTable("SP_GetGreenHouseSupervisorTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName == "Assign")
            {
                if (Session["Role"].ToString() == "2")
                {
                    Session["JobID"] = e.CommandArgument.ToString();
                    Response.Redirect("~/GerminationTaskAssignment.aspx");
                }
               
            }
            if (e.CommandName == "Select")
            {
                if (Session["Role"].ToString() == "2")
                {
                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@OperatorID", Session["LoginID"].ToString());
                    nv.Add("@Notes", "");
                    nv.Add("@JobID", e.CommandArgument.ToString());
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    result = objCommon.GetDataInsertORUpdate("SP_AddGerminationAssignment", nv);
                    Session["JobID"] = e.CommandArgument.ToString();
                    Response.Redirect("~/GreenHouseTaskCompletion.aspx");
                }
                if (Session["Role"].ToString() == "5")
                {

                }
                }
        }
    }
}
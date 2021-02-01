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
    public partial class MyTaskGreenhouseOperator : System.Web.UI.Page
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
            dt = objCommon.GetDataTable("SP_GetGreenHouseOperatorTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string JobID = "";
                string TaskID = "";
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvGerm.Rows[rowIndex];

                JobID = (row.FindControl("lblID") as Label).Text;
                TaskID = (row.FindControl("HiddenFieldTaskID") as HiddenField).Value;


               
                    if (TaskID == "5")
                    {
                        Session["JobID"] = JobID;
                        Response.Redirect("~/GreenHouseTaskCompletion.aspx");
                    }
                    else if (TaskID == "11")
                    {
                        Session["JobID"] = JobID;
                        Response.Redirect("~/PlantReadyTaskCompletion.aspx");
                    }
                

               // Session["JobID"] = e.CommandArgument.ToString();
             //   Response.Redirect("~/GreenHouseTaskCompletion.aspx");
            }
        }

      

        protected void gvGerm_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }
    }
}
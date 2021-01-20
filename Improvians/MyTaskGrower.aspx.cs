using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class MyTask1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ddlTaskRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlTaskRequest.SelectedValue=="4")
            {
                Response.Redirect("~/GerminationRequestForm.aspx");
            }
            if (ddlTaskRequest.SelectedValue == "5")
            {
                Response.Redirect("~/MoveForm.aspx");
            }
        }
    }
}
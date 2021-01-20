using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginID"] == null  || Session["Role"].ToString() !="4")
            {
                Response.Redirect("~/Admin/Login.aspx");
            }
            if (!IsPostBack)
            {

                lblEmpName.Text = Session["EmployeeName"].ToString();
            }
        }
    }
}
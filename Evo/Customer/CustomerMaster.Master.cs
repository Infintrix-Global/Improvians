using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class CustomerMaster : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["LoginID"] == null)
            {
                Response.Redirect("~/CustomerLogin.aspx");
               
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblEmpName.Text = Session["EmployeeName"].ToString();
                if (!string.IsNullOrEmpty(Session["Photo"].ToString()))
                {
                    imgprofilepic.ImageUrl = Session["Photo"].ToString();
                }
                String activepage = Request.RawUrl.ToLower();
                if (activepage.Contains("dashboard") || Session["Role"].ToString() == "14")
                {
                    divSitemap.Visible = false;
                }
                else
                {
                    SiteMapPath1.SiteMapProvider = "SitemapCustomer";
                    SiteMapPath1.DataBind();
                }
            }
        }
    }
}
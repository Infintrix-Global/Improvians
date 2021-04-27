using Evo.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo.Customer
{
    public partial class ContactSalesRepresentative : System.Web.UI.Page
    {
        General objGeneral = new General();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GetSalesData();
        }
        private void GetSalesData()
        {
            DataTable dt = new DataTable();
            string sqr = "Select L.ID,EmployeeName,Mobile,Email,Photo from Login L join CustomerSalesMapping C on L.ID=C.SalesID where C.CustomerID=" + Session["LoginID"].ToString();

            dt = objGeneral.GetDatasetByCommand(sqr);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["EmployeeName"].ToString();
                lblName1.Text = dt.Rows[0]["EmployeeName"].ToString();
                lblPhone.Text = dt.Rows[0]["Mobile"].ToString();
                lnkPhone.HRef = "tel:" + dt.Rows[0]["Mobile"].ToString();
                lblEmail.Text = dt.Rows[0]["Email"].ToString();
                lnkEmail.HRef = "mailto:" + dt.Rows[0]["Email"].ToString();
                ImageProfile.Src = @"..\EmployeeProfile\" + dt.Rows[0]["Photo"].ToString();
            }
        }
    }
}
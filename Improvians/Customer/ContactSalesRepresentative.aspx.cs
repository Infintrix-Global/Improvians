using Evo.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        CommonControl objCommon = new CommonControl();
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
                if (!string.IsNullOrEmpty(dt.Rows[0]["Photo"].ToString()))
                    ImageProfile.Src = @"..\EmployeeProfile\" + dt.Rows[0]["Photo"].ToString();
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Uid", Session["LoginID"].ToString());
            DataTable dt1 = objCommon.GetDataTable("getReceiverEmail", nv);
            string CCMail = dt1.Rows[0]["Email"].ToString();

            string ToMail = lblEmail.Text;
            string Subject = "Contact Request is made by " + Session["EmployeeName"].ToString();
            string msg = "Hi " + lblName.Text + "," + "<br /><br />";
            msg = msg + "You have received following message from customer: " + Session["EmployeeName"].ToString() + "<br />";
            msg = msg + msgs.Text + "<br />" + "<br />";
            msg = msg + "<br />Thanks, <br/> Customer Information Portal";
            objGeneral.SendMail(ToMail, CCMail, Subject, msg);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Thanks! We will contact you soon.')", true);
            msgs.Text = "";
        }
    }
}
using Evo.BAL_Classes;
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
    public partial class ForgetCustomerPassword : System.Web.UI.Page
    {
        General objGeneral = new General();
        clsCommonMasters objCom = new clsCommonMasters();
        CommonControl objCommon = new CommonControl();
        General objGen = new General();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtUserName.Focus();
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@UserName", txtUserName.Text);
            DataTable dt = objCommon.GetDataTable("GetReceiverEmailByUserName", nv);

            if (dt != null && dt.Rows.Count > 0)
            {
                string ReceiverEmail = dt.Rows[0]["Email"].ToString();
                if (string.IsNullOrEmpty(ReceiverEmail))
                    lblmsg.Text = "Email ID not found";
                else
                {
                    string ReceiverPassword = dt.Rows[0]["Password"].ToString();
                    string Subject = "Forget Password";
                    string msg = "Hi " + txtUserName.Text + "," + "<br /><br />";
                    msg = msg + "Your password to access customer portal is: " + objCom.Decrypt(ReceiverPassword) + "<br />";

                    msg = msg + "<br />Thanks, <br/> Customer Information Portal";
                    objGen.SendMail(ReceiverEmail, "", Subject, msg);

                    string message = "Thank you for your request. Password has been sent to you over registered email id.";
                    string url = "CustomerLogin.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
            }
            else
            {
                lblmsg.Text = "Incorrect User Name";
            }
        }
    }
}
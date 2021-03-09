using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Admin;
using Evo.Admin.BAL_Classes;

namespace Evo.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        clsCommonMasters objCommon = new clsCommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            LoginEntity _LoginEntity = new LoginEntity();
            _LoginEntity.UserName = txtUserName.Text;
            _LoginEntity.Password = objCommon.Encrypt(txtPassword.Text.Trim());

            BAL_Login _ballogin = new BAL_Login();
            DataTable _dtLogin = _ballogin.getLoginDetails(_LoginEntity);


            if (Convert.ToInt32(_dtLogin.Rows[0][0].ToString()) == -1)
            {

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Login", "alert('Enter Correct User Name or Password')", true);
            }
            else if(_dtLogin.Rows[0]["RoleID"].ToString()=="4")
            {
                //string x = txtimgcode.Text;
                //string y = Session["CaptchaImageText"].ToString();
                //if (txtimgcode.Text == Session["CaptchaImageText"].ToString())
                //{
                //    //lblmsg.Text = "Excellent.......";
                //    if (CheckBoxRemember.Checked)
                //    {
                //        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                //        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                //    }
                //    else
                //    {
                //        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                //        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                //    }
                Response.Cookies["UserName"].Value = txtUserName.Text.Trim();
                Response.Cookies["Password"].Value = txtPassword.Text.Trim();
                Session["LoginID"] = int.Parse(_dtLogin.Rows[0]["ID"].ToString());

                Session["Role"] = _dtLogin.Rows[0]["RoleID"].ToString();
                Session["Mobile"] = _dtLogin.Rows[0]["EmployeeCode"].ToString();

                Session["EmployeeName"] = _dtLogin.Rows[0]["EmployeeName"].ToString();
                /*admin */
                Response.Redirect("~/Admin/AdminDashBoard.aspx");
                //}
                //else
                //{
                //    lblmsg.Text = "image code is not valid.";
                //}
                //this.txtimgcode.Text = "";


            }

            else
                {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Login", "alert('Enter Correct User Name or Password')", true);
            }
        }
    }
}
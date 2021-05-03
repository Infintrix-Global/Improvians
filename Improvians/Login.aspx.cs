using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Evo.BAL_Classes;

namespace Evo
{
    public partial class Login : System.Web.UI.Page
    {
        clsCommonMasters objCommon = new clsCommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Abandon();
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
            else
            {
                Session["LoginID"] = int.Parse(_dtLogin.Rows[0]["ID"].ToString());
                Session["Role"] = _dtLogin.Rows[0]["RoleID"].ToString();
                Session["Mobile"] = _dtLogin.Rows[0]["EmployeeCode"].ToString();
                Session["Email"] = _dtLogin.Rows[0]["Email"].ToString();
                Session["Photo"] = _dtLogin.Rows[0]["Photo"].ToString();
                Session["EmployeeName"] = _dtLogin.Rows[0]["EmployeeName"].ToString();
                int result = _ballogin.UpdateFCMToken(int.Parse(_dtLogin.Rows[0]["ID"].ToString()), token.Value);

                /*admin */
                if (_dtLogin.Rows[0]["RoleID"].ToString() == "4")
                {
                    Response.Redirect("~/Admin/AdminDashBoard.aspx");
                }               
                else
                {
                    Response.Redirect("~/DashBoard.aspx");
                }
            }
        }

    }
}
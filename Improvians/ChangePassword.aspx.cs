using Evo.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        General objGeneral = new General();
        clsCommonMasters objCommon = new clsCommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtOldPassword.Focus();
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            LoginEntity _LoginEntity = new LoginEntity();
            _LoginEntity.UserName = Session["EmployeeCode"].ToString();
            _LoginEntity.Password = objCommon.Encrypt(txtOldPassword.Text.Trim());

            BAL_Login _ballogin = new BAL_Login();
            DataTable _dtLogin = _ballogin.getLoginDetails(_LoginEntity);

            if (Convert.ToInt32(_dtLogin.Rows[0][0].ToString()) == -1)
            {
               lblmsg.Text= "Enter Correct Password";
            }
            else
            {              
                int result = _ballogin.UpdatePassword(int.Parse(Session["LoginID"].ToString()), objCommon.Encrypt(txtPassword.Text));
                lblmsg.Text = "Password changed successfully";
            }
        }
    }
}
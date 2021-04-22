using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Admin.BAL_Classes;
using Evo.Admin;
using System.Collections.Specialized;

namespace Evo.Admin
{
    public partial class NotificationPreference : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        General objGeneral = new General();
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                BindUserName();
                BindUserDetails();
            }
        }

        private void BindUserName()
        {
            var sqr = "";
            DataTable dt = new DataTable();
            sqr = "select distinct RTRIM(L.EmployeeName) + '_' + R.RoleAbbreviation as UserName from Role R inner join Login L on L.RoleID = R.RoleID " +
                     "where L.ISActive = 1  and R.RoleAbbreviation Is Not Null";
            dt = objGeneral.GetDatasetByCommand(sqr);
            dt.Columns.Add("Action");
            gvUsers.DataSource = dt;
            gvUsers.DataBind();
        }

        private void BindUserDetails()
        {
            var sqr = "";
            DataTable dt = new DataTable();
            sqr = "select Id,UserName,TaskType ,'App,Email' as SendType from  dbo.NotificationPreference order by Id desc";
            dt.Clear();
            dt = objGeneral.GetDatasetByCommand(sqr);
            dt.Columns.Add("Action");
            gvUserDetails.DataSource = dt;
            gvUserDetails.DataBind();
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
         
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        protected void submitPreference_Click(object sender, EventArgs e)
        {
            foreach(GridViewRow row in gvUsers.Rows)
            {
                CheckBox chk1 = (row.FindControl("chkSelect") as CheckBox);
                if (chk1.Checked)
                {
                    CheckBox chk2 = (row.FindControl("chkApp") as CheckBox);
                    CheckBox chk3 = (row.FindControl("chkEmail") as CheckBox);

                    var UserName= (row.FindControl("userRoleNames") as Label).Text;
                    
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@UserName", UserName);
                    nv.Add("@ViaApp", chk2.Checked.ToString());
                    nv.Add("@ViaEmail", chk3.Checked.ToString());
                    nv.Add("@TaskName", ddlTasks.SelectedValue);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    var result = objCommon.GetDataExecuteScaler("SP_AddNotificationPreference", nv);

                    chk1.Checked = false;
                    chk2.Checked = false;
                    chk3.Checked = false;
                }
            }
            BindUserDetails();
        }

        protected void ddlTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList list = (DropDownList)sender;
            if(list.SelectedValue != "0")
            {
                submitPreference.Attributes.Remove("disabled");
                submitErrorMsg.Text = "";
                submitErrorMsg.Visible = false;
            }
            else
            {
                submitPreference.Attributes.Add("disabled", "disabled");
                submitErrorMsg.Text = "Please select task type.";
                submitErrorMsg.Visible = true;
            }
        }

        protected void gvUserDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvUserDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            NameValueCollection nv = new NameValueCollection();
            switch (e.CommandName){
                case "Edit":

                    break;
                case "Delete":
                    nv.Add("@Id", e.CommandArgument.ToString());
                    var result = objCommon.GetDataExecuteScaler("SP_DeleteNotificationPreference", nv);
                    break;
            }
            BindUserDetails();
        }
    }
}
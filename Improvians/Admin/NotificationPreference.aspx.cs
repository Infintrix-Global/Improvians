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
using System.Drawing;

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
                Session["UserName"] = "";
                //BindUserName("");
                BindUserDetails();
                if (ddlTasks.SelectedValue != "0")
                {
                    submitPreference.Attributes.Remove("disabled");
                }
            }
        }

        //private void BindUserName(string p)
        //{

        //    var sqr = "";
        //    DataTable dt = new DataTable();
        //    if (!string.IsNullOrEmpty(p))
        //    {
        //        sqr = "select distinct RTRIM(L.EmployeeName) + '_' + R.RoleAbbreviation as UserName from Role R inner join Login L on L.RoleID = R.RoleID " +
        //            "where RTRIM(L.EmployeeName) + '_' + R.RoleAbbreviation ='" + p + "'";
        //    }
        //    else
        //    {
        //        sqr = "select distinct RTRIM(L.EmployeeName) + '_' + R.RoleAbbreviation as UserName from Role R inner join Login L on L.RoleID = R.RoleID " +
        //            "where L.ISActive = 1  and R.RoleAbbreviation Is Not Null";
        //    }


        //    dt = objGeneral.GetDatasetByCommand(sqr);
        //    dt.Columns.Add("Action");
        //    gvUsers.DataSource = dt;
        //    gvUsers.DataBind();

        //    if (ddlTasks.SelectedValue != "0")
        //    {
        //        submitPreference.Attributes.Remove("disabled");
        //        if (!string.IsNullOrEmpty(p))
        //        {
        //            GridViewRow row = gvUsers.Rows[0];
        //            CheckBox checkName = row.FindControl("chkSelect") as CheckBox;
        //            checkName.Checked = true;
        //            sqr = "select * from NotificationPreference where UserName ='" + checkName.Text + "' and TaskType = '" + ddlTasks.SelectedValue + "'";
        //            dt = objGeneral.GetDatasetByCommand(sqr);
        //            if (dt != null)
        //            {
        //                var res1 = dt.Rows[0]["IsApp"];
        //                var res2 = dt.Rows[0]["IsEmail"];
        //                CheckBox checkApp = row.FindControl("chkApp") as CheckBox;
        //                checkApp.Checked = (bool)res1;
        //                CheckBox chkEmail = row.FindControl("chkEmail") as CheckBox;
        //                chkEmail.Checked = (bool)res2; ;
        //            }                    
        //        }
        //    }


        //}

        private void BindUserDetails()
        {            
                var sqr = "";
            DataTable dt = new DataTable();
            if (ddlTasks.SelectedValue != "0")
            {
                sqr = "select Id,UserName,TaskType ,IsApp,IsEmail from dbo.NotificationPreference where TaskType='" +ddlTasks.SelectedValue +"' order by Id desc";
            }
            else
            {
                sqr = "select Id,UserName,TaskType,IsApp,IsEmail  from  dbo.NotificationPreference order by Id desc";
            }
            dt.Clear();
            dt = objGeneral.GetDatasetByCommand(sqr);
            gvUserDetails.DataSource = dt;
            gvUserDetails.DataBind();
            if (ddlTasks.SelectedValue != "0")
            {
                submitPreference.Attributes.Remove("disabled");
            }
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //DataTable dt = new DataTable();
            //var sqr = "";
            //if(gvUsers.Rows.Count == 1)
            //{

            //  }
        }



        protected void submitPreference_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvUserDetails.Rows)
            {
                //CheckBox chk1 = (row.FindControl("chkSelect") as CheckBox);
                //if (chk1.Checked)
                //{
                    CheckBox chk2 = (row.FindControl("chkApp") as CheckBox);
                    CheckBox chk3 = (row.FindControl("chkEmail") as CheckBox);

                    //var UserName = (row.FindControl("userRoleNames") as Label).Text;

                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@UserName", "");
                    nv.Add("@ViaApp", chk2.Checked.ToString());
                    nv.Add("@ViaEmail", chk3.Checked.ToString());
                    nv.Add("@TaskName", ddlTasks.SelectedValue);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    var result = objCommon.GetDataExecuteScaler("SP_AddNotificationPreference", nv);

                  //  chk1.Checked = false;
                    chk2.Checked = false;
                    chk3.Checked = false;
            //    }
            }
            BindUserDetails();
            //BindUserName("");
        }

        protected void ddlTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList list = (DropDownList)sender;
            if (list.SelectedValue != "0")
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



        protected void gvUserDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            NameValueCollection nv = new NameValueCollection();
            switch (e.CommandName)
            {
                case "Edit":
                    // var highlight = gvUsers.Rows.Cast<GridViewRow>().FirstOrDefault(z => (z.FindControl("userRoleNames") as Label).Text.ToString() == e.CommandArgument.ToString()).RowIndex;
                    //BindUserName(e.CommandArgument.ToString());
                    break;
                case "Delete":
                    nv.Add("@Id", e.CommandArgument.ToString());
                    var result = objCommon.GetDataExecuteScaler("SP_DeleteNotificationPreference", nv);
                    BindUserDetails();
                    break;
            }
        }

        protected void gvUserDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void gvUserDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvUserDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void reset_Click(object sender, EventArgs e)
        {
            ddlTasks.SelectedValue = "0";
            //BindUserName("");
            BindUserDetails();
        }

        protected void GridAddUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvAddUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvAddUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}
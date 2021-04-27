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
              //  BindUserDetails();
                if (ddlTTypes.SelectedValue != "0")
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
            if (ddlTTypes.SelectedValue != "0")
            {
                sqr = "select Id,UserName,TaskType ,IsApp,IsEmail from dbo.NotificationPreference where TaskType='" + ddlTTypes.SelectedValue +"' order by Id desc";
            }
            else
            {
                sqr = "select Id,UserName,TaskType,IsApp,IsEmail  from  dbo.NotificationPreference order by Id desc";
            }
            dt.Clear();
            dt = objGeneral.GetDatasetByCommand(sqr);
          //  gvUserDetails.DataSource = dt;
          //  gvUserDetails.DataBind();
            if (ddlTTypes.SelectedValue != "0")
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
            long result1 = 0;
            foreach (GridViewRow row in gvUsersProfile.Rows)
            {
                if (!string.IsNullOrEmpty(((TextBox)row.FindControl("viaApp")).Text) && !string.IsNullOrEmpty(((TextBox)row.FindControl("viaEmail")).Text))
                {
                    string id = ((Label)row.FindControl("NPId")).Text;

                    CheckBox chk2 = (row.FindControl("viaApp") as CheckBox);
                    CheckBox chk3 = (row.FindControl("viaEmail") as CheckBox);

                    NotificationPreferenceMaster obj = new NotificationPreferenceMaster()
                    {
                        id = Convert.ToInt32(id),
                        IsApp = chk2.Checked,
                        IsEmail = chk3.Checked
                    };

                    //need to add a code to update
                    result1 = objCommon.UpdateNotificationPreference(obj);
                }
            }
            string message = "Record updated Successful";
            string url = "NotificationPreference.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

            BindUserDetails();
            //BindUserName("");
        }

        private List<NotificationPreferenceDetails> NotificationPreferenceData
        {
            get
            {
                if (ViewState["NotificationPreferenceData"] != null)
                {
                    return (List<NotificationPreferenceDetails>)ViewState["NotificationPreferenceData"];
                }
                return new List<NotificationPreferenceDetails>();
            }
            set
            {
                ViewState["NotificationPreferenceData"] = value;
            }
        }

        protected void btnAddProfile_Click(object sender, EventArgs e)
        {
            pnlAdd.Visible = true;
            pnlList.Visible = false;
            AddNewRow(true);
        }

        private void AddNewRow(bool AddBlankRow)
        {
          
                List<NotificationPreferenceDetails> objProfile = new List<NotificationPreferenceDetails>();

                foreach (GridViewRow row in gvAddUsers.Rows)
                {
                    CheckBox chk2 = (row.FindControl("chkApp") as CheckBox);
                    CheckBox chk3 = (row.FindControl("chkEmail") as CheckBox);
                DropDownList task= (row.FindControl("ddlTasks") as DropDownList);
                DropDownList userName = (row.FindControl("ddlUsers") as DropDownList);

                //var UserName = (row.FindControl("userRoleNames") as Label).Text;
                AddProfileDetail(ref objProfile, task.SelectedValue,userName.SelectedValue, chk2.Checked, chk3.Checked);

                NotificationPreferenceData = objProfile;
                //GridProfileBind();

            }                              

        }

        private void AddProfileDetail(ref List<NotificationPreferenceDetails> objPD, string TaskType, string UserName, bool IsEmail, bool IsApp)
        {
            NotificationPreferenceDetails objProfile = new NotificationPreferenceDetails();

            objProfile.Id = objPD.Count + 1;
            objProfile.TaskType = TaskType;
            objProfile.UserName = UserName;
            objProfile.viaEmail = IsEmail;           
            objProfile.viaApp = IsApp;           

            objPD.Add(objProfile);
            ViewState["objPD"] = objPD;
        }
        protected void ddlTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList list = (DropDownList)sender;
            if (list.SelectedValue != "0")
            {
                submitPreference.Attributes.Remove("disabled");
               
            }
            else
            {
                submitPreference.Attributes.Add("disabled", "disabled");
             
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
            ddlTTypes.SelectedValue = "0";
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

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {

        }

        protected void gvUsersProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
   
    [Serializable]
    public class NotificationPreferenceDetails
    {
        public int Id { get; set; }
        public string TaskType { get; set; }
        public string UserName { get; set; }
        public bool viaApp { get; set; }
        public bool viaEmail { get; set; }
    }
}
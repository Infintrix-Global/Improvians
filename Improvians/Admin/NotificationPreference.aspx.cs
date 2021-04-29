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
    public partial class NotificationPreference : Page
    {
        clsCommonMasters objCommon = new clsCommonMasters();
        General objGeneral = new General();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUserDetails();
            }
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

        private void BindUserNames(DropDownList ddlUsers)
        {
            var sqr = "";
            DataTable dt = new DataTable();

            sqr = "select distinct RTRIM(L.EmployeeName) + '_' + R.RoleAbbreviation as UserName from Role R inner join Login L on L.RoleID = R.RoleID " +
                "where L.ISActive = 1  and R.RoleAbbreviation Is Not Null and L.RoleID not in('13','14')";

            dt = objGeneral.GetDatasetByCommand(sqr);
            ddlUsers.DataSource = dt;
            ddlUsers.DataTextField = "UserName";
            ddlUsers.DataValueField = "UserName";
            ddlUsers.DataBind();
            ddlUsers.Items.Insert(0, new ListItem("Select User", "0"));
            //ddlUsers.DataBind();
        }

        private void BindUserDetails()
        {
            var sqr = "";
            DataTable dt = new DataTable();
            if (ddlTTypes.SelectedValue != "0")
            {
                sqr = "select Id,UserName,TaskType ,IsApp,IsEmail from dbo.NotificationPreference where TaskType='" + ddlTTypes.SelectedValue + "' order by Id desc";
            }
            else
            {
                sqr = "select Id,UserName,TaskType,IsApp,IsEmail from  dbo.NotificationPreference order by Id desc";
            }
            dt.Clear();
            dt = objGeneral.GetDatasetByCommand(sqr);
            gvUsersProfile.DataSource = dt;
            gvUsersProfile.DataBind();
            if (ddlTTypes.SelectedValue != "0")
            {
                submitPreference.Attributes.Remove("disabled");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //  int _isInserted = -1;
            foreach (GridViewRow item in gvAddUsers.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlTasksTypes = (DropDownList)item.Cells[0].FindControl("ddlTasks");
                    DropDownList ddlUsers = (DropDownList)item.Cells[0].FindControl("ddlUsers");
                    CheckBox chkApps = (item.Cells[0].FindControl("chkApp") as CheckBox);
                    CheckBox chkEmails = (item.Cells[0].FindControl("chkEmail") as CheckBox);

                    NotificationPreferenceMaster obj = new NotificationPreferenceMaster()
                    {
                        Task = ddlTasksTypes.SelectedValue,
                        User = ddlUsers.SelectedValue,
                        IsApp = chkApps.Checked,
                        IsEmail = chkEmails.Checked
                    };
                    var _Inserted = objCommon.InsertNotificationPreference(obj);
                }
            }

            lblmsg.Text = "";
            pnlList.Visible = true;
            pnlAdd.Visible = false;
            BindUserDetails();


        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
            AddNewRow(true);
        }
        public void Clear()
        {
            gvAddUsers.DataSource = null;
            gvAddUsers.DataBind();
        }

        protected void submitPreference_Click(object sender, EventArgs e)
        {
            long result1 = 0;
            foreach (GridViewRow row in gvUsersProfile.Rows)
            {
                string id = ((Label)row.FindControl("NPId")).Text;

                CheckBox chk2 = (row.FindControl("viaApp") as CheckBox);
                CheckBox chk3 = (row.FindControl("viaEmail") as CheckBox);
                var tasks = (row.FindControl("lblTask") as Label).Text;
                var user = (row.FindControl("lblUserName") as Label).Text;

                NotificationPreferenceMaster obj = new NotificationPreferenceMaster()
                {
                    id = Convert.ToInt32(id),
                    Task = tasks,
                    User = user,
                    IsApp = chk2.Checked,
                    IsEmail = chk3.Checked
                };

                //methdo can be used to update as well.
                result1 = objCommon.InsertNotificationPreference(obj);

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
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            // BindTask();
            ddlTTypes.SelectedIndex = 0;
            BindUserDetails();
            //gvUsersProfile.DataSource = null;
            //gvUsersProfile.DataBind();
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
            DropDownList task = new DropDownList();
            foreach (GridViewRow row in gvAddUsers.Rows)
            {
                CheckBox chk2 = (row.FindControl("chkApp") as CheckBox);
                CheckBox chk3 = (row.FindControl("chkEmail") as CheckBox);
                task = (row.FindControl("ddlTasks") as DropDownList);
                DropDownList userName = (row.FindControl("ddlUsers") as DropDownList);

                AddProfileDetail(ref objProfile, task.SelectedValue, userName.SelectedValue, chk2.Checked, chk3.Checked);
            }
            if (AddBlankRow)
                AddProfileDetail(ref objProfile, string.IsNullOrWhiteSpace(task.SelectedValue) ? "0" : task.SelectedValue, "0", false, false);
            NotificationPreferenceData = objProfile;
            GridProfileBind();

        }
        public void GridProfileBind()
        {
            gvAddUsers.DataSource = NotificationPreferenceData;
            gvAddUsers.DataBind();

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
            BindUserDetails();
        }

        protected void reset_Click(object sender, EventArgs e)
        {
            ddlTTypes.SelectedValue = "0";
            BindUserDetails();
        }

        protected void gvAddUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<NotificationPreferenceDetails> objProfile = NotificationPreferenceData;
            objProfile.RemoveAt(e.RowIndex);
            NotificationPreferenceData = objProfile;
            GridProfileBind();
        }

        protected void gvAddUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlTaskTypess= (DropDownList)e.Row.FindControl("ddlTasks");
                DropDownList ddlUserName = (DropDownList)e.Row.FindControl("ddlUsers");
                CheckBox check1 = (CheckBox)e.Row.FindControl("chkApp");
                CheckBox check2 = (CheckBox)e.Row.FindControl("chkEmail");

                BindUserNames(ddlUserName);

                HiddenField hdnTasktypes = (HiddenField)e.Row.FindControl("hdnTask");
                HiddenField hdnusers= (HiddenField)e.Row.FindControl("hdnUserNames");
                HiddenField hdnViaapp= (HiddenField)e.Row.FindControl("hdnViaApps");
                HiddenField hdnViaEmail= (HiddenField)e.Row.FindControl("hdnViaEmails");
                
                ddlTaskTypess.SelectedValue = hdnTasktypes.Value;
                ddlUserName.SelectedValue = hdnusers.Value;

                check1.Checked = hdnViaapp.Value == "True" ? true : false; 
                check2.Checked = hdnViaEmail.Value == "True" ? true : false; 
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRow(true);
        }

        protected void gvUsersProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CommonControl objCommonC = new CommonControl();
            NameValueCollection nv = new NameValueCollection();

            switch (e.CommandName)
            {
                case "Delete":
                    nv.Add("@Id", e.CommandArgument.ToString());
                    var result = objCommonC.GetDataExecuteScaler("SP_DeleteNotificationPreference", nv);
                    break;
            }
            BindUserDetails();
        }

        protected void gvUsersProfile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkApp = e.Row.FindControl("viaApp") as CheckBox;
                CheckBox chkEmail = e.Row.FindControl("viaEmail") as CheckBox;
                var appVal = (e.Row.FindControl("hdnApps") as HiddenField).Value;
                var appEmail = (e.Row.FindControl("hdnEmails") as HiddenField).Value;

                chkApp.Checked = appVal == "False" ? false : true;
                chkEmail.Checked = appEmail == "False" ? false : true;

            }

        }

        protected void gvUsersProfile_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void viaApp_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        protected void viaEmail_CheckedChanged(object sender, EventArgs e)
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
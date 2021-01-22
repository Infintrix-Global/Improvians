using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Improvians.Admin;
using Improvians.Admin.BAL_Classes;

namespace Improvians.Admin
{
   
    public partial class EditProfile : System.Web.UI.Page
    {
        clsCommonMasters objCommon = new clsCommonMasters();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDepartment();
                BindRole();
                string eid = Session["EmployeeID"].ToString();
                int x = Convert.ToInt32(eid);
                bindEmployeeProfile(x);
            }
        }

        public void BindDepartment()
        {
            ddlDepartment.DataSource = objCommon.GetDepartmentMaster();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();

        }

        public void BindRole()
        {
            ddlDesignation.DataSource = objCommon.GetRoleMaster();
            ddlDesignation.DataTextField = "RoleName";
            ddlDesignation.DataValueField = "RoleID";
            ddlDesignation.DataBind();

        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            UploadImageProfile();
        }

        public void UploadImageProfile()
        {

            string filename = "", newfile = "";
            string[] validFileTypes = { "jpeg", "png", "jpg" };

            if (!FileUpProfile.HasFile)
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "ShowAlert", "alert('Please select a file.');", true);
                FileUpProfile.Focus();
            }
            //string DD = txtFristName.Text;
            string aa = FileUpProfile.FileName;
            string ext = System.IO.Path.GetExtension(FileUpProfile.PostedFile.FileName).ToLower();
            bool isValidFile = false;
            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break;
                }
            }
            if (isValidFile == true)
            {

                if (FileUpProfile.HasFile)
                {

                    filename = Server.MapPath(FileUpProfile.FileName);
                    newfile = FileUpProfile.PostedFile.FileName;
                    //                filecontent = System.IO.File.ReadAllText(filename);
                    FileInfo fi = new FileInfo(newfile);

                    // check folder exist or not
                    if (!System.IO.Directory.Exists(@"~\EmployeeProfile"))
                    {
                        try
                        {

                            string Imgname = newfile;

                            string path = Server.MapPath(@"~\EmployeeProfile\");
                            System.IO.Directory.CreateDirectory(path);
                            FileUpProfile.SaveAs(path + @"\" + Imgname);

                            ImageProfile.ImageUrl = @"~\EmployeeProfile\" + Imgname ;
                            ImageProfile.Visible = true;
                            lblProfile.Visible = true;
                            lblProfile.Text = Imgname ;

                            //  IdentityPolicyImageUrl = Imgname + ext;


                        }
                        catch (Exception ex)
                        {
                            lblProfile.Text = "Not able to create new directory";
                        }

                    }
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "ShowAlert", "alert('Please select valid file.');", true);
            }



        }


        public void bindEmployeeProfile(int eid)
        {
            try
            {
                DataTable dt1 = objTask.GetEmployeeByID(eid);


                if (dt1.Rows.Count > 0)
                {
                    lblProfile.Text = dt1.Rows[0]["Photo"].ToString();
                    lblProfile.Visible = false;
                    ImageProfile.ImageUrl = @"~\EmployeeProfile\" + dt1.Rows[0]["Photo"].ToString();
                    txtPassword.Text = objCommon.Decrypt(dt1.Rows[0]["Password"].ToString());
                    ddlDepartment.SelectedValue = dt1.Rows[0]["DepartmentID"].ToString();
                    txtName.Text = dt1.Rows[0]["EmployeeName"].ToString();
                    ddlDesignation.SelectedValue = dt1.Rows[0]["RoleID"].ToString();
                    txtMobileNo.Text = dt1.Rows[0]["Mobile"].ToString();
                    txtEmail.Text = dt1.Rows[0]["Email"].ToString();
                    txtUserName.Text = dt1.Rows[0]["EmployeeCode"].ToString();
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int _isInserted = -1;
                Employee objEmployee = new Employee()
                {
                    EmployeeID = Convert.ToInt32(Session["EmployeeID"].ToString()),
                    Name = txtName.Text,
                    Mobile = txtMobileNo.Text,
                    Email = txtEmail.Text,
                  Department=ddlDepartment.SelectedValue,
                  Designation=ddlDesignation.SelectedValue,
                    Photo = lblProfile.Text,
                    EmployeeCode=txtUserName.Text,
                    Password = objCommon.Encrypt(txtPassword.Text)
                };
                _isInserted = objCommon.UpdateEmployee(objEmployee);
                if (_isInserted == -1)
                {

                    lblmsg.Text = "Failed to Update Employee";
                    lblmsg.ForeColor = System.Drawing.Color.Red;

                }

                else
                {

                    lblmsg.Text = "Employee Updated ";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    Response.Redirect("~/EmployeeProfile.aspx");

                }
            }
            catch (Exception ex)
            {
                General.ErrorMessage(ex.StackTrace + ex.Message);
            }
        }
    }
}
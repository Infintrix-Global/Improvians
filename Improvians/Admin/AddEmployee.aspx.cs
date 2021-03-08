﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Admin.BAL_Classes;
using Evo.Admin;

namespace Evo.Admin
{
    public partial class AddEmployee : System.Web.UI.Page
    {
        clsCommonMasters objCommon = new clsCommonMasters();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                BindRole();
                BindFacility();
            }
        }      

        public void BindRole()
        {
            repDesignation.DataSource = objCommon.GetRoleMaster(); 
            repDesignation.DataBind();           
        }

        public void BindFacility()
        {
            repFacility.DataSource = objCommon.GetFacilityMaster();
            repFacility.DataBind();
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCheckEmail = new DataTable();
                if ((dtCheckEmail = objCommon.CheckEmailExists(txtEmail.Text.Trim())).Rows.Count == 0)
                {
                    int _isInserted = -1;
                    string strDesignation = string.Empty;
                    foreach (RepeaterItem item in repDesignation.Items)
                    {
                        CheckBox chkFacility = (CheckBox)item.FindControl("chkRole");
                        if (chkFacility.Checked)
                        {
                           strDesignation=((HiddenField)item.FindControl("hdnRoleValue")).Value;
                        }
                    }
                    Employee objEmployee = new Employee()
                    {

                        Name = txtName.Text,
                        Mobile = txtMobile.Text,
                        Password = objCommon.Encrypt(txtPassword.Text),
                        EmployeeCode=txtUserName.Text,
                        Email = txtEmail.Text,
                        Designation = strDesignation,
                        Department = "",
                        Photo = lblProfile.Text
                    };

                    _isInserted = objCommon.InsertEmployee(objEmployee);

                    if (_isInserted == -1)
                    {

                        lblmsg.Text = "Failed to Add Employee";
                        lblmsg.ForeColor = System.Drawing.Color.Red;

                    }
                    //else if (_isInserted == 0)
                    //{

                    //    lblmsg.Text = "Mobile Number Exists";
                    //    lblmsg.ForeColor = System.Drawing.Color.Red;
                    //}
                    else if (_isInserted == -2)
                    {

                        lblmsg.Text = "User Name Exists";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {

                        lblmsg.Text = "Employee Added ";
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        foreach (RepeaterItem item in repFacility.Items)
                        {
                            CheckBox chkFacility = (CheckBox)item.FindControl("chkFacility");
                            if (chkFacility.Checked)
                            {
                                objCommon.AddEmployeeFacility(_isInserted, ((HiddenField)item.FindControl("hdnValue")).Value);
                            }
                        }
                        // objCommon.AddEmployeeFacility(_isInserted, ddlFacility.SelectedValue);
                        Response.Redirect("~/Admin/ViewEmployee.aspx");
                        btclear_Click(sender, e);
                    }
                }
                else
                {
                    lblStatus.Text = "Email already exists.Use Another Email ID";
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btclear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            repDesignation.DataBind();
            repDesignation.DataBind();
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

                            ImageProfile.ImageUrl = @"~\EmployeeProfile\" + Imgname;
                            ImageProfile.Visible = true;
                            lblProfile.Visible = true;
                            lblProfile.Text = Imgname;

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

    }
}
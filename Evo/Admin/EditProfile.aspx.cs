﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Admin;
using Evo.Admin.BAL_Classes;

namespace Evo.Admin
{

    public partial class EditProfile : System.Web.UI.Page
    {
        clsCommonMasters objCommon = new clsCommonMasters();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFacility();
                BindFacilitySelect();
                BindRole();
                string eid = Session["EmployeeID"].ToString();
                int x = Convert.ToInt32(eid);
                bindEmployeeProfile(x);
            }
        }

        public void BindFacility()
        {
            repFacility.DataSource = objCommon.GetFacilityMaster();
            repFacility.DataBind();
            //ddlFacility.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindFacilitySelect()
        {


            ddlFacility.DataSource = objCommon.GetFacilityMaster();
            ddlFacility.DataTextField = "FacilityName";
            ddlFacility.DataValueField = "FacilityID";
            ddlFacility.DataBind();
            ddlFacility.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindRole()
        {
            ddlDesignation.DataSource = objCommon.GetRoleMaster();
            ddlDesignation.DataTextField = "RoleName";
            ddlDesignation.DataValueField = "RoleID";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("--- Select ---", "0"));
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


        public void bindEmployeeProfile(int eid)
        {
            try
            {
                DataSet dt1 = objTask.GetEmployeeByID(eid);


                if (dt1.Tables[0].Rows.Count > 0)
                {
                    lblProfile.Text = dt1.Tables[0].Rows[0]["Photo"].ToString();
                    lblProfile.Visible = false;
                    ImageProfile.ImageUrl = @"..\EmployeeProfile\" + dt1.Tables[0].Rows[0]["Photo"].ToString();
                    txtPassword.Text = objCommon.Decrypt(dt1.Tables[0].Rows[0]["Password"].ToString());
                    //ddlDepartment.SelectedValue = dt1.Tables[0].Rows[0]["DepartmentID"].ToString();
                    txtName.Text = dt1.Tables[0].Rows[0]["EmployeeName"].ToString();
                    ddlDesignation.SelectedValue = dt1.Tables[0].Rows[0]["RoleID"].ToString();
                    txtMobile.Text = dt1.Tables[0].Rows[0]["Mobile"].ToString();
                    txtEmail.Text = dt1.Tables[0].Rows[0]["Email"].ToString();
                    txtUserName.Text = dt1.Tables[0].Rows[0]["EmployeeCode"].ToString();
                }


                if (ddlDesignation.SelectedValue == "1" || ddlDesignation.SelectedValue == "7" || ddlDesignation.SelectedValue == "12" || ddlDesignation.SelectedValue == "15" || ddlDesignation.SelectedValue == "16")
                {
                    BindFacility();
                    ddlFacility.Visible = false;
                    repFacility.Visible = true;

                    if (dt1.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt1.Tables[1].Rows)
                        {
                            foreach (RepeaterItem item in repFacility.Items)
                            {
                                if (((HiddenField)item.FindControl("hdnValue")).Value == dr["FacilityID"].ToString())
                                    ((CheckBox)item.FindControl("chkFacility")).Checked = true;
                            }
                            //chkDepartment.SelectedValue = dr["DepartmentID"].ToString();
                        }
                    }
                }
                else
                {
                    repFacility.Visible = false;
                    ddlFacility.Visible = true;
                    if (dt1.Tables[1].Rows.Count > 0)
                    {
                        BindFacilitySelect();


                        ddlFacility.SelectedValue = dt1.Tables[1].Rows[0]["FacilityID"].ToString();
                    }

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
                    Mobile = txtMobile.Text,
                    Email = txtEmail.Text,
                    Department = "",
                    Designation = ddlDesignation.SelectedValue,
                    Photo = lblProfile.Text,
                    EmployeeCode = txtUserName.Text,
                    Password = objCommon.Encrypt(txtPassword.Text),
                    NavisionCustomerID = ""
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
                    if (ddlDesignation.SelectedValue == "1" || ddlDesignation.SelectedValue == "7" || ddlDesignation.SelectedValue == "12" || ddlDesignation.SelectedValue == "15" || ddlDesignation.SelectedValue == "16")
                    {
                        string Facility = "";
                        foreach (RepeaterItem item in repFacility.Items)
                        {
                            CheckBox chkFacility = (CheckBox)item.FindControl("chkFacility");
                            if (chkFacility.Checked)
                            {
                                // objCommon.AddEmployeeFacility(Convert.ToInt32(Session["EmployeeID"].ToString()), ((HiddenField)item.FindControl("hdnValue")).Value);
                                Facility += ((HiddenField)item.FindControl("hdnValue")).Value + ",";
                            }
                        }

                        if (Facility != "")
                        {
                            Facility = Facility.Remove(Facility.Length - 1);
                        }

                        objCommon.AddEmployeeFacility(Convert.ToInt32(Session["EmployeeID"].ToString()), Facility);
                    }
                    else
                    {
                        objCommon.AddEmployeeFacility(Convert.ToInt32(Session["EmployeeID"].ToString()), ddlFacility.SelectedValue);
                    }
                    Response.Redirect("~/Admin/ViewEmployee.aspx");

                }
            }
            catch (Exception ex)
            {
                General.ErrorMessage(ex.StackTrace + ex.Message);
            }
        }
    }
}
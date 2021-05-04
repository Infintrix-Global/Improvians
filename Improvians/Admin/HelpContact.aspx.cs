using Evo.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo.Admin
{
    public partial class HelpContact : System.Web.UI.Page
    {
        General objGeneral = new General();
        clsCommonMasters objCommon = new clsCommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                BindData();
            }
        }
        private void BindData()
        {
            DataTable dt = new DataTable();
            string sqr = "Select * from HelpContact";

            dt = objGeneral.GetDatasetByCommand(sqr);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtMobile.Text = dt.Rows[0]["Phone"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["Photo"].ToString()))
                    ImageProfile.ImageUrl = dt.Rows[0]["Photo"].ToString();
            }
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
                            lblProfile.Text = ImageProfile.ImageUrl;

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
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int _isInserted = -1;               
                _isInserted = objCommon.UpdateHelpContact(txtName.Text,  txtEmail.Text, txtMobile.Text, lblProfile.Text);
                if (_isInserted == -1)
                {

                    lblmsg.Text = "Failed to Update Help Contact";
                    lblmsg.ForeColor = System.Drawing.Color.Red;

                }

                else
                {

                    lblmsg.Text = "Help Contact Updated ";
                    lblmsg.ForeColor = System.Drawing.Color.Green;

                }
            }
            catch (Exception ex)
            {
                General.ErrorMessage(ex.StackTrace + ex.Message);
            }
        }
    }
}
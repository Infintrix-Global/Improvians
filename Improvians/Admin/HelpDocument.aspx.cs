using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.BAL_Classes;
using System.Data;
using System.IO;
using System.Web.Configuration;

namespace Evo.Admin
{
    public partial class HelpDocument : System.Web.UI.Page
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
            string sqr = "Select * from HelpDocument where IsActive=1";

            dt = objGeneral.GetDatasetByCommand(sqr);
            gvDocument.DataSource = dt;
            gvDocument.DataBind();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["DocumentID"] == null)
                {
                    int _isInserted = -1;
                    _isInserted = objCommon.InsertHelpDocument(txtName.Text, txtDocumentLink.Text, txtVideoLink.Text);
                    if (_isInserted == -1)
                    {
                        lblmsg.Text = "Failed to Add Document";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }

                    else
                    {
                        BindData();
                        txtName.Text = "";
                        txtDocumentLink.Text = "";
                        txtVideoLink.Text = "";
                        lblmsg.Text = "";
                    }
                }
                else
                {
                    int _isInserted = objCommon.UpdateHelpDocument(Convert.ToInt32(Session["DocumentID"]), txtName.Text, txtDocumentLink.Text, txtVideoLink.Text);
                    if (_isInserted == -1)
                    {
                        lblmsg.Text = "Failed to Update Document";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }

                    else
                    {
                        BindData();
                        txtName.Text = "";
                        txtDocumentLink.Text = "";
                        txtVideoLink.Text = "";
                        lblmsg.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            txtDocumentLink.Text = UploadImageProfile(DocumentUpload);
        }

        protected void btnVideoUpload_Click(object sender, EventArgs e)
        {
            txtVideoLink.Text = UploadImageProfile(VideoUpload);
        }
        public string UploadImageProfile(FileUpload DocumentUpload)
        {
            string filename = "", newfile = "";

            if (DocumentUpload.HasFile)
            {
                filename = Server.MapPath(DocumentUpload.FileName);
                newfile = DocumentUpload.PostedFile.FileName;
                FileInfo fi = new FileInfo(newfile);

                // check folder exist or not
                if (!System.IO.Directory.Exists(@"~\Documents"))
                {
                    try
                    {
                        string Imgname = newfile;
                        string path = Server.MapPath(@"~\Documents\");
                        System.IO.Directory.CreateDirectory(path);
                        DocumentUpload.SaveAs(path + @"\" + Imgname);

                      return WebConfigurationManager.AppSettings["PortalURL"] + @"/Documents/" + Imgname;
                    }
                    catch (Exception ex)
                    {
                        lblmsg.Text = "Not able to create new directory";
                    }
                }
            }
            return string.Empty;
        }
        protected void gvDocument_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditProfile")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Session["DocumentID"] = id;
                DataTable dt = new DataTable();
                string sqr = "Select * from HelpDocument where ID=" + id.ToString();
                dt = objGeneral.GetDatasetByCommand(sqr);
                txtName.Text = dt.Rows[0]["Title"].ToString();
                txtDocumentLink.Text = dt.Rows[0]["DocumentLink"].ToString();
                txtVideoLink.Text = dt.Rows[0]["VideoLink"].ToString();
            }

            if (e.CommandName == "RemoveProfile")
            {
                int eid = Convert.ToInt32(e.CommandArgument);
                objCommon.RemoveHelpDocument(eid);
                BindData();
            }
        }

        protected void gvDocument_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkVideo = (HyperLink)e.Row.FindControl("lnkVideo");
                if (string.IsNullOrEmpty(lnkVideo.NavigateUrl))
                    lnkVideo.Visible = false;
                HyperLink lnkDocument = (HyperLink)e.Row.FindControl("lnkDocument");
                if (string.IsNullOrEmpty(lnkDocument.NavigateUrl))
                    lnkDocument.Visible = false;
            }
        }
    }
}
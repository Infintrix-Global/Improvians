using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.BAL_Classes;
using System.Data;

namespace Evo.Admin
{
    public partial class HelpFAQ : System.Web.UI.Page
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
            string sqr = "Select * from HelpFAQ where IsActive=1";

            dt = objGeneral.GetDatasetByCommand(sqr);
            gvFAQ.DataSource = dt;
            gvFAQ.DataBind();
        }

        protected void gvFAQ_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditProfile")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Session["FAQID"] = id;
                DataTable dt = new DataTable();
                string sqr = "Select * from HelpFAQ where ID=" + id.ToString();
                dt = objGeneral.GetDatasetByCommand(sqr);
                txtName.Text = dt.Rows[0]["Title"].ToString();
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
            }

            if (e.CommandName == "RemoveProfile")
            {
                int eid = Convert.ToInt32(e.CommandArgument);
                objCommon.RemoveHelpFAQ(eid);
                BindData();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["FAQID"] == null)
                {
                    int _isInserted = -1;
                    _isInserted = objCommon.InsertHelpFAQ(txtName.Text, txtDescription.Text);
                    if (_isInserted == -1)
                    {
                        lblmsg.Text = "Failed to Add FAQ";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }

                    else
                    {
                        BindData();
                        txtName.Text = "";
                        txtDescription.Text = "";
                        lblmsg.Text = "";
                    }

                }
                else
                {
                    int _isInserted = objCommon.UpdateHelpFAQ(Convert.ToInt32(Session["FAQID"]), txtName.Text, txtDescription.Text);
                    if (_isInserted == -1)
                    {
                        lblmsg.Text = "Failed to Update FAQ";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }

                    else
                    {
                        BindData();
                        txtName.Text = "";
                        txtDescription.Text = "";
                        lblmsg.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
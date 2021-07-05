using Evo.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class Help : System.Web.UI.Page
    {
        General objGeneral = new General();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetData();
            }
        }



        private void GetData()
        {
            DataSet ds = objGeneral.GetDatasetByCommand_SP("SP_GetHelp");
            repDocuments.DataSource = ds.Tables[0];
            repDocuments.DataBind();

            DataTable dt = ds.Tables[1];
            if (dt!=null && dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["Name"].ToString();
                lblPhone.Text = dt.Rows[0]["Phone"].ToString();
                lnkPhone.HRef = "tel:" + dt.Rows[0]["Phone"].ToString();
                lblEmail.Text = dt.Rows[0]["Email"].ToString();
                lnkEmail.HRef = "mailto:" + dt.Rows[0]["Email"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["Photo"].ToString()))
                    ImageProfile.Src = dt.Rows[0]["Photo"].ToString();
            }

            repFAQ.DataSource = ds.Tables[2];
            repFAQ.DataBind();
        }

        protected void repDocuments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HyperLink lnkVideo = (HyperLink)e.Item.FindControl("lnkVideo");
            if (string.IsNullOrEmpty(lnkVideo.NavigateUrl))
                lnkVideo.Visible = false;
            HyperLink lnkDocument = (HyperLink)e.Item.FindControl("lnkDocument");
            if (string.IsNullOrEmpty(lnkDocument.NavigateUrl))
                lnkDocument.Visible = false;
        }
    }
}
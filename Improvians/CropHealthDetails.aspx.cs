using Improvians.Admin.BAL_Classes;
using Improvians.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class CropHealthDetails : System.Web.UI.Page
    {
        CommonControlNavision objNav = new CommonControlNavision();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGridCropHealth();
            }
        }


        public void BindGridCropHealth()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Id","");
            dt = objCommon.GetDataTable("SP_GetCropHealthReport", nv);

            gvCropHealth.DataSource = dt;
            gvCropHealth.DataBind();

        }


        protected void btnManual_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CropHealthReport.aspx");
        }

        protected void gvCropHealth_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCropHealth.PageIndex = e.NewPageIndex;
            BindGridCropHealth();
        }
    }
}
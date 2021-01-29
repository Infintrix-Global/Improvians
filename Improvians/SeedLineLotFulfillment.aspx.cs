using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class SeedLineLotFulfillment : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridSeed();
               
            }
        }

        public void BindGridSeed()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobID", Session["JobID"].ToString());
            dt = objCommon.GetDataTable("SP_GetSeedLineOperatorTaskByJobID", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@PTCID", dt.Rows[0]["PTCID"].ToString());
            dt1 = objCommon.GetDataTable("SP_GetPTCSeedMapByPTCID", nv1);
            gvDetails.DataSource = dt1;
            gvDetails.DataBind();

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
    }
}
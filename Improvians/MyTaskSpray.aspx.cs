using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Evo
{
    public partial class MyTaskSpray : System.Web.UI.Page
    {
        clsCommonMasters objCommon = new clsCommonMasters();
        CommonControl objCommonControl = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CountTotal();
            }
        }


        public void CountTotal()
        {

            DataSet dt = new DataSet();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommonControl.GetDataSet("SP_GetSprayEachTaskCount", nv);

            lblFer.Text = dt.Tables[0].Rows.Count.ToString();

            lblPutAway.Text = dt.Tables[1].Rows.Count.ToString();

            lblGerm.Text = dt.Tables[2].Rows.Count.ToString();

            lblIrr.Text = dt.Tables[3].Rows.Count.ToString();

            lblpr.Text = dt.Tables[4].Rows.Count.ToString();

            lblChemical.Text = dt.Tables[5].Rows.Count.ToString();

            lblDumpCount.Text = dt.Tables[6].Rows.Count.ToString();

            lblMove.Text = dt.Tables[7].Rows.Count.ToString();

            lblGeneralCount.Text = dt.Tables[8].Rows.Count.ToString();

            lblCropHealthReport.Text = dt.Tables[9].Rows.Count.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Bal;
using System.Web.UI.HtmlControls;

namespace Evo
{
    public partial class MyTaskHeadGrower : System.Web.UI.Page
    {

        Bal_SeedingPlan objSP = new Bal_SeedingPlan();
        CommonControl objCommon = new CommonControl();
        // BAL_CommonMasters objCOm = new BAL_CommonMasters();
        public static DataTable AllData = new DataTable();
        DataTable dt = new DataTable();
        NameValueCollection nv = new NameValueCollection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CountTotal();
            }
        }


        public void CountTotal()
        {
            DataSet dt = new DataSet();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());
            dt = objCommon.GetDataSet("SP_GetGrowerEachTaskCount", nv);



            lblPutAway.Text = dt.Tables[0].Rows.Count.ToString();
            lblGerm.Text = dt.Tables[1].Rows.Count.ToString();
            lblFer.Text = dt.Tables[2].Rows.Count.ToString();
            lblIrr.Text = dt.Tables[3].Rows.Count.ToString();
            lblpr.Text = dt.Tables[4].Rows.Count.ToString();
            lblCropHealthReport.Text = dt.Tables[6].Rows.Count.ToString();

            lblChemical.Text = dt.Tables[7].Rows.Count.ToString();
            lblDumpTotal.Text = dt.Tables[8].Rows.Count.ToString();
            lblMove.Text = dt.Tables[9].Rows.Count.ToString();
            lblGeneralTotal.Text = dt.Tables[10].Rows.Count.ToString();
            //lnkMove.Text = dt.Tables[5].Rows.Count.ToString();
        }
    }
}
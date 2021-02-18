using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class MyTask1 : System.Web.UI.Page
    {
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

            dt = objCommonControl.GetDataSet("SP_GetGrowerEachTaskCount", nv);
            lnkPutAway.Text = dt.Tables[0].Rows.Count.ToString();
            lnkGerm.Text = dt.Tables[1].Rows.Count.ToString();
            lnkFer.Text = dt.Tables[2].Rows.Count.ToString();
            lnkIrr.Text = dt.Tables[3].Rows.Count.ToString();
            lnkpr.Text = dt.Tables[4].Rows.Count.ToString();
            lnkMove.Text = dt.Tables[5].Rows.Count.ToString();
        }
    }
}
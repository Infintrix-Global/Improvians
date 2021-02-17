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
    public partial class MyTaskGreenSupervisorFinal : System.Web.UI.Page
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
            nv.Add("@LoginID", Session["LoginID"].ToString());
            dt = objCommonControl.GetDataSet("SP_GetGreenhouseSupervisorEachTaskCount", nv);           
            lnkGerm.Text = dt.Tables[0].Rows.Count.ToString();            
            lnkIrr.Text = dt.Tables[1].Rows.Count.ToString();
            lnkpr.Text = dt.Tables[2].Rows.Count.ToString();
        }


    }
}
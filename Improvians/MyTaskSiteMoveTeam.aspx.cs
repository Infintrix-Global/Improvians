using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class MyTaskSiteMoveTeam : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CountTotal();
            }
        }


        public void CountTotal()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            // nv.Add("@Mode", "2");
            
                dt = objCommon.GetDataTable("SP_GetMoveSiteTeamTask", nv);
            lnkMove.Text = dt.Rows.Count.ToString();
            //dt = objCommon.GetDataTable("SP_GetGreenHouseLogisticTask", nv);
            //  lblPutAwayTotal.Text = dt.Rows.Count.ToString();
        }
    }
}
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
    public partial class DashBoard1 : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        public void BindData()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            dt = objCommon.GetDataTable("SP_GetHomeDetails", nv);
            if (dt != null && dt.Rows.Count > 0)
            {
                ltrGerminationRate.Text = Convert.ToString(dt.Rows[0]["Germination"]);
            }
            SetLink();

        }
        public void SetLink()
        {
            if (Session["Role"].ToString() == "1")
            {
                amytask.HRef ="MyTaskGrower.aspx";
            }
            if (Session["Role"].ToString() == "2")
            {
                amytask.HRef = "MyTaskGreenSupervisor.aspx";
            }
            if (Session["Role"].ToString() == "3")
            {
                amytask.HRef = "MyTaskGreenOperator.aspx";
            }
            if (Session["Role"].ToString() == "5")
            {
                amytask.HRef = "MyTaskLogisticManager.aspx";
            }
            if (Session["Role"].ToString() == "6")
            {
                amytask.HRef = "MyTaskShippingCoordinator.aspx";
            }
        }
    }
}
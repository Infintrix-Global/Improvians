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
                amytask.HRef = "MyTaskGreenSupervisorFinal.aspx";
            }
            if (Session["Role"].ToString() == "3")
            {
                amytask.HRef = "MyTaskGreenOperatorFinal.aspx";
            }
            if (Session["Role"].ToString() == "5")
            {
                //  amytask.HRef = "MyTaskLogisticManager.aspx";
                amytask.HRef = "MyTaskSiteMoveTeam.aspx";
            }
            if (Session["Role"].ToString() == "6")
            {
                amytask.HRef = "MyTaskShippingCoordinator.aspx";
            }
            if (Session["Role"].ToString() == "7")
            {
                amytask.HRef = "Seeding_Plan_Form.aspx";
            }
            if (Session["Role"].ToString() == "8")
            {
                amytask.HRef = "MyTaskSeedingTeam.aspx";
            }
            if (Session["Role"].ToString() == "9")
            {
                amytask.HRef = "MyTaskSeedLineOperator.aspx";
            }
            if (Session["Role"].ToString() == "10")
            {
                amytask.HRef = "MyTaskProductionPlanner.aspx";
            }
            if (Session["Role"].ToString() == "11")
            {
                amytask.HRef = "MyTaskSpray.aspx";
            }
            if (Session["Role"].ToString() == "12")
            {
                amytask.HRef = "MyTaskGrower.aspx";
            }
        }
    }
}
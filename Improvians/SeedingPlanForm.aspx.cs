using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Improvians.Bal;
namespace Improvians
{
    public partial class SeedingPlanForm : System.Web.UI.Page
    {
        Bal_SeedingPlan objSP = new Bal_SeedingPlan();
        public static DataTable AllData = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Fdate = "", TDate = "";
                 Fdate = Convert.ToDateTime(System.DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd");
                 TDate = (Convert.ToDateTime(Fdate)).AddDays(7).ToString("yyyy-MM-dd");
            //    Fdate = DateAdd(DateInterval.Day, -7, Now.Date);
          //  TDate = DateAdd(DateInterval.Day, 10, Now.Date);
                txtFromDate.Text = Fdate;
                txtToDate.Text = TDate;

                getDataDGJob();
            }
        }


        public void getDataDGJob()
        {
            AllData = objSP.GetDataSeedingPlan(txtFromDate.Text.Trim(), txtToDate.Text.Trim());

            DGJob.DataSource = AllData;
            DGJob.DataBind();

        }


        protected void DGJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGJob.PageIndex = e.NewPageIndex;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getDataDGJob();
        }
    }
}
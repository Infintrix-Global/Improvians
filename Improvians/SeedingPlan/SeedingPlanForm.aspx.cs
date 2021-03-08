using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.BAL_Classes;
using System.Data.SqlClient;
namespace Evo.SeedingPlan
{
    public partial class SeedingPlanForm : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["EvoNavision"].ToString();
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }


        }

        public void BindGrid()
        {
            //DataTable dt = new DataTable();
            // NameValueCollection nv = new NameValueCollection();
            //   dt = objCommon.GetDataTable("SP_GetFertilizerRequest", nv);
            // gvFer.DataSource = dt;
            //gvFer.DataBind();

            SqlConnection con = new SqlConnection(CS);
            string str = "select w.No_ wo, b.[Job No_] jobcode, j.[Bill-to Name] cname, p.[Starting Date] sodate, j.[Item Description] itmdescp, j.[Item No_] itm,j.[Variant Code] ts, b.[Container Qty_] sotrays, j.[Bill-to Customer No_] cusno, j.[Delivery Date] duedate, w.[Location Code] loc,";
            str += " w.[Calc_ Quantity] wotrays, w.[Planned Date] wodate, case when j.[Job Status] = 1 then 'Yes' else 'No' end alloc ";
            str += " from [GTI$IA Job Activity Scheme Line] b, [GTI$IA Job Production Scheme Line] p, ";
            str += " [GTI$Job] j left outer join[GTI$IA Work Order Header] w on j.No_ = w.[Job No_] ";
            str += " where b.[Job No_] = p.[Job No_] And b.[Job No_] = j.No_ And b.[Item Category] = 'SEED' and p.[Production Phase] = 'SEEDING' And ";
            str += " b.[Actual Date] < '1/1/2000' and j.[Job Status] in (1, 9) ";
            str += "  order by alloc desc, loc desc, sodate ";
            con.Open();
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();

            GridSeedingPlan.DataSource = dr;
            GridSeedingPlan.DataBind();
        }
    }
}
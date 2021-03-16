using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Bal;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace Evo
{
    public partial class MyTaskSeedlinePlanner : System.Web.UI.Page
    {
        Bal_SeedingPlan objSP = new Bal_SeedingPlan();
        CommonControl objCommon = new CommonControl();
        public static DataTable AllData = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getSeedLineCount();
            }
           
        }

        public void getSeedLineCount()
        {
            AllData = objSP.GetDataSeedingPlan("", "", "", "", "1", "");

            if (AllData != null && AllData.Rows.Count > 0)
            {
                DataTable dt11 = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@LoginID", "");
                nv.Add("@mode", "10");
                dt11 = objCommon.GetDataTable("SP_GetGreenHouseLogisticTask", nv);
                DataTable dtOnlyLeftWO = new DataTable();
                if (dt11 != null && dt11.Rows.Count > 0)
                {
                    var rows = (from a in AllData.AsEnumerable()
                                join b in dt11.AsEnumerable()
                                on a["WO"].ToString() equals b["WO"].ToString()
                                into g
                                where g.Count() == 0
                                select a);
                    if (rows.Any())
                        dtOnlyLeftWO = rows.CopyToDataTable();

                }
                else
                {
                    dtOnlyLeftWO = AllData;
                }
                lblSeedlineCount.Text = dtOnlyLeftWO.Rows.Count.ToString();
                
            }
            else
            {
                
            }

        }





    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Improvians.Bal;

namespace Improvians
{
    public partial class temp : System.Web.UI.Page
    {
        Bal_SeedingPlan objSP = new Bal_SeedingPlan();
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objCOm = new BAL_CommonMasters();
        public static DataTable AllData = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long _isInserted = 1;
            int SelectedItems = 0;
            NameValueCollection nv1 = new NameValueCollection();
            DataTable dt = objCommon.GetDataTable("SP_GetMaxSeedDateManual", nv1);
            string SeedDate = string.Empty;
            if (dt!= null && dt.Rows.Count>0)
            {
                SeedDate= dt.Rows[0]["SeedDate"].ToString(); 
            }
            AllData = objSP.GetDataSeedingPlanManual(SeedDate);
           // DGJob.DataSource = AllData;
          //  DGJob.DataBind();
            for (int i= 0; i < AllData.Rows.Count;i++)
            {

                string GreenHouseID = (AllData.Rows[i]["GreenHouseID"].ToString());
                string FacilityID =  (AllData.Rows[i]["FacilityID"].ToString());
                string jobcode = (AllData.Rows[i]["jobcode"].ToString() );
                string itemno = (AllData.Rows[i]["itemno"].ToString());
                string CustName = (AllData.Rows[i]["cname"].ToString());
                string seeddate = (AllData.Rows[i]["seeddate"].ToString());
                string Trays = (AllData.Rows[i]["Trays"].ToString());
                string TraySize = (AllData.Rows[i]["TraySize"].ToString());
                string itemdescp = AllData.Rows[i]["itemdescp"].ToString();
              
                        long result = 0;
                        NameValueCollection nv = new NameValueCollection();
                       // nv.Add("@jid", _isInserted.ToString());
                        
                        nv.Add("@jobcode", jobcode);
                        nv.Add("@Item", itemno);
                        nv.Add("@Itemdesc", itemdescp);
                        nv.Add("@Customer", CustName);
                        nv.Add("@GreenHouseID", GreenHouseID);
                        nv.Add("@Facility", FacilityID);
                        nv.Add("@TraySize", TraySize);
                        nv.Add("@TotalTray", Trays);
                        nv.Add("@Seeddate", seeddate);
                       
                   _isInserted = objCommon.GetDataExecuteScaler("SP_Addgti_jobs_Seeding_Plan_Manual", nv);

              //  DataTable dtISD = objSP.GetSeedDateData("IRRIGATE", lblGenusCode.Text, lblSeededTrays.Text);
               // DataTable dtFez = objSP.GetSeedDateData("FERTILIZE", lblGenusCode.Text, lblSeededTrays.Text);

               // if (dtISD != null && dtISD.Rows.Count > 0)
               // {
                  //  IrrigateSeedDate = (Convert.ToDateTime(lblSeedDate.Text).AddDays(Convert.ToInt32(dtISD.Rows[0]["DateShift"]))).ToString();
              //  }
             

                // _isInserted = 1;

                SelectedItems++;

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + SelectedItems + " ' Seeding Plan Save Successful ')", true);

        }

          
    }
}
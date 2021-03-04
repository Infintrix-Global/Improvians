using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Improvians.Bal;

namespace Improvians
{
    public partial class MyTask1 : System.Web.UI.Page
    {
        CommonControl objCommonControl = new CommonControl();
        Bal_SeedingPlan objSP = new Bal_SeedingPlan();
        CommonControl objCommon = new CommonControl();
        // BAL_CommonMasters objCOm = new BAL_CommonMasters();
        public static DataTable AllData = new DataTable();
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
            lblPutAway.Text = dt.Tables[0].Rows.Count.ToString();
            lblGerm.Text = dt.Tables[1].Rows.Count.ToString();
            lblFer.Text = dt.Tables[2].Rows.Count.ToString();
            lblIrr.Text = dt.Tables[3].Rows.Count.ToString();
            lblpr.Text = dt.Tables[4].Rows.Count.ToString();
            lblCropHealthReport.Text = dt.Tables[6].Rows.Count.ToString();

          
            //lnkMove.Text = dt.Tables[5].Rows.Count.ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long _isInserted = 1;
            long _isIGCodeInserted = 1;
            long _isFCdeInserted = 1;
            int SelectedItems = 0;

            NameValueCollection nv1 = new NameValueCollection();
            DataTable dt = objCommon.GetDataTable("SP_GetMaxSeedDateManual", nv1);
            string SeedDate = string.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                SeedDate = dt.Rows[0]["SeedDate"].ToString();
            }
            AllData = objSP.GetDataSeedingPlanManual(SeedDate);
            // DGJob.DataSource = AllData;
            //  DGJob.DataBind();
            for (int i = 0; i < AllData.Rows.Count; i++)
            {

                string GreenHouseID = (AllData.Rows[i]["GreenHouseID"].ToString());
                string FacilityID = (AllData.Rows[i]["FacilityID"].ToString());
                string jobcode = (AllData.Rows[i]["jobcode"].ToString());
                string itemno = (AllData.Rows[i]["itemno"].ToString());
                string CustName = (AllData.Rows[i]["cname"].ToString());
                string seeddate = (AllData.Rows[i]["seeddate"].ToString());
                string Trays = (AllData.Rows[i]["Trays"].ToString());
                string TraySize = (AllData.Rows[i]["TraySize"].ToString());
                string itemdescp = AllData.Rows[i]["itemdescp"].ToString();
                string GenusCode = AllData.Rows[i]["GenusCode"].ToString();
                string germcount = AllData.Rows[i]["germcount"].ToString();
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
                nv.Add("@germcount", germcount);
                _isInserted = objCommon.GetDataExecuteScaler("SP_Addgti_jobs_Seeding_Plan_Manual", nv);


                DataTable dtFez = objSP.GetSeedDateDatanew("FERTILIZE", GenusCode, TraySize);

                if (dtFez != null && dtFez.Rows.Count > 0)
                {


                    DataColumn col = dtFez.Columns["DateShift"];
                    foreach (DataRow row in dtFez.Rows)
                    {

                        string FertilizationDate = string.Empty;
                        int fvalue = 0;
                        if (int.TryParse(row[col].ToString(), out fvalue))
                        {
                            FertilizationDate = (Convert.ToDateTime(seeddate).AddDays(fvalue)).ToString();
                            NameValueCollection nv11 = new NameValueCollection();

                            //nv11.Add("@ManualID", _isInserted.ToString());
                            //nv11.Add("@Type", "FERTILIZE");
                            //nv11.Add("@FertilizationDate", FertilizationDate);
                            //nv11.Add("@FertilizationCode", FertilizationCode.ToString());
                            //nv11.Add("@GreenHouseID", GreenHouseID);
                            nv11.Add("@GrowerPutAwayId", "");
                            nv11.Add("@wo", "");
                            nv11.Add("@Jid", _isInserted.ToString());
                            nv11.Add("@jobcode", jobcode);
                            nv11.Add("@FacilityID", FacilityID);
                            nv11.Add("@GreenHouseID", GreenHouseID);
                            nv11.Add("@Trays", Trays);

                            nv11.Add("@SeedDate", seeddate);
                            nv11.Add("@CreateBy", Session["LoginID"].ToString());
                            nv11.Add("@Supervisor", "0");
                            nv11.Add("@IrrigateSeedDate", "");
                            nv11.Add("@FertilizeSeedDate", FertilizationDate);


                            _isFCdeInserted = objCommon.GetDataExecuteScaler("SP_AddGrowerPutAwayDetailsFertilizationMenual", nv11);
                        }
                    }

                }
                else
                {

                    NameValueCollection nv111 = new NameValueCollection();


                    nv111.Add("@GrowerPutAwayId", "");
                    nv111.Add("@wo", "");
                    nv111.Add("@Jid", _isInserted.ToString());
                    nv111.Add("@jobcode", jobcode);
                    nv111.Add("@FacilityID", FacilityID);
                    nv111.Add("@GreenHouseID", GreenHouseID);
                    nv111.Add("@Trays", Trays);

                    nv111.Add("@SeedDate", seeddate);
                    nv111.Add("@CreateBy", Session["LoginID"].ToString());
                    nv111.Add("@Supervisor", "0");
                    nv111.Add("@IrrigateSeedDate", "");
                    nv111.Add("@FertilizeSeedDate", seeddate);
                    nv111.Add("@ID", "");

                    _isFCdeInserted = objCommon.GetDataExecuteScaler("SP_AddGrowerPutAwayDetailsFertilizationMenual", nv111);
                }

                DataTable dtISD = objSP.GetSeedDateDatanew("IRRIGATE", GenusCode, TraySize);

                int IrrigateCode = 0;
                DataTable dtIG = new DataTable();
                NameValueCollection nv1IG = new NameValueCollection();
                nv1IG.Add("@Mode", "13");
                dtIG = objCommon.GetDataTable("GET_Common", nv1IG);
                IrrigateCode = Convert.ToInt32(dtIG.Rows[0]["ICode"]);

                if (dtISD != null && dtISD.Rows.Count > 0)
                {


                    DataColumn col = dtISD.Columns["DateShift"];
                    foreach (DataRow row in dtISD.Rows)
                    {
                        int ivalue = 0;
                        if (int.TryParse(row[col].ToString(), out ivalue))
                        {
                            string IrrigateDate = string.Empty;
                            IrrigateDate = (Convert.ToDateTime(seeddate).AddDays(ivalue)).ToString();
                            NameValueCollection nv11 = new NameValueCollection();

                            nv11.Add("@GrowerPutAwayIrrigatId", "");
                            nv11.Add("@wo", "");
                            nv11.Add("@Jid", _isInserted.ToString());
                            nv11.Add("@jobcode", jobcode);
                            nv11.Add("@FacilityID", FacilityID);
                            nv11.Add("@GreenHouseID", GreenHouseID);
                            nv11.Add("@Trays", Trays);

                            nv11.Add("@SeedDate", seeddate);
                            nv11.Add("@CreateBy", Session["LoginID"].ToString());
                            nv11.Add("@Supervisor", "0");
                            nv11.Add("@IrrigateSeedDate", IrrigateDate);
                            nv11.Add("@FertilizeSeedDate", "");
                            nv11.Add("@ID", "");




                            _isIGCodeInserted = objCommon.GetDataExecuteScaler("SP_AddGrowerPutAwayDetailsIrrigationMenual", nv11);
                        }
                    }
                }
                else
                {
                    string IrrigateSeedDate = string.Empty;
                    IrrigateSeedDate = seeddate;
                    NameValueCollection nv111 = new NameValueCollection();
                    nv111.Add("@GrowerPutAwayIrrigatId", "");
                    nv111.Add("@wo", "");
                    nv111.Add("@Jid", _isInserted.ToString());
                    nv111.Add("@jobcode", jobcode);
                    nv111.Add("@FacilityID", FacilityID);
                    nv111.Add("@GreenHouseID", GreenHouseID);
                    nv111.Add("@Trays", Trays);

                    nv111.Add("@SeedDate", seeddate);
                    nv111.Add("@CreateBy", Session["LoginID"].ToString());
                    nv111.Add("@Supervisor", "0");
                    nv111.Add("@IrrigateSeedDate", seeddate);
                    nv111.Add("@FertilizeSeedDate", "");
                    nv111.Add("@ID", "");
                    _isIGCodeInserted = objCommon.GetDataExecuteScaler("SP_AddGrowerPutAwayDetailsIrrigationMenual", nv111);
                }



                // _isInserted = 1;

                SelectedItems++;

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + SelectedItems + " ' Seeding Plan Save Successful ')", true);
            CountTotal();
        }

    }
}
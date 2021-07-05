using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Evo
{
    public partial class MyTaskSpray : System.Web.UI.Page
    {
        //clsCommonMasters objCommon = new clsCommonMasters();
        CommonControl objCommon = new CommonControl();
        DataTable dt = new DataTable();
        NameValueCollection nv = new NameValueCollection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CountTotal();
                BindGridGerm();
                BindGridPutAway();
                BindGridFer();
                BindGridChem();
                BindGridIrr();
                BindGridCrop();
                BindGridPR();
                BindGridMov();
                BindGridDum();
                BindGridGen();
            }
        }


        public void CountTotal()
        {

            DataSet dt = new DataSet();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataSet("SP_GetSprayEachTaskCount", nv);

            lblFer.Text = dt.Tables[0].Rows.Count.ToString();

            lblPutAway.Text = dt.Tables[1].Rows.Count.ToString();

            lblGerm.Text = dt.Tables[2].Rows.Count.ToString();

            lblIrr.Text = dt.Tables[3].Rows.Count.ToString();

            lblpr.Text = dt.Tables[4].Rows.Count.ToString();

            lblChemical.Text = dt.Tables[5].Rows.Count.ToString();

            lblDumpCount.Text = dt.Tables[6].Rows.Count.ToString();

            lblMove.Text = dt.Tables[7].Rows.Count.ToString();

            lblGeneralCount.Text = dt.Tables[8].Rows.Count.ToString();

            lblCropHealthReport.Text = dt.Tables[9].Rows.Count.ToString();
        }

        private void BindGridGen()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("SP_GetOperatorGeneralTask", nv);
            BindData(dt, Gen, "GeneralTaskDate");
        }

        private void BindGridDum()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("SP_GetOperatorDumpTask", nv);
            BindData(dt, Dum, "DumpDateR");
        }

        private void BindGridMov()
        {
            dt = new DataTable();
            nv.Clear();           
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("SP_GetSupervisorMoveDetails", nv);
            BindData(dt, Mov, "MoveDate");
        }

        private void BindGridPR()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginID", Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("SP_GetOperatorPlantReadyTask", nv);
            BindData(dt, PR, "PlanDate");
        }

        private void BindGridCrop()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("SP_GetCropReportRequestAssistantGrower", nv);
            BindData(dt, Crop, "CropHealthReportDate");
        }

        private void BindGridIrr()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetOperatorIrrigationTask", nv);
            BindData(dt, Irr, "SprayDate");
        }

        private void BindGridChem()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetChemical_RequestDetailsNew", nv);
            BindData(dt, Chem, "ChemicalDate");
        }

        private void BindGridFer()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetSprayRequestDetailsNew", nv);
            BindData(dt, Fer, "FertilizationDate");
        }

        private void BindGridPutAway()
        {
            dt = new DataTable();
            nv.Clear();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetShippingCoordinatorTask", nv);
            BindData(dt, Put, "SeededDate");
        }

        public void BindGridGerm()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetGreenHouseOperatorGerminationTask", nv);
            BindData(dt, Ger, "GermDate");
        }

        private void BindData(DataTable dt, HtmlAnchor html, string dateField)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dtimeString = Convert.ToDateTime(dt.Rows[i][dateField]).ToString("yyyy/MM/dd");

                    DateTime dtime = Convert.ToDateTime(dtimeString);

                    DateTime nowtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));                    

                    if (nowtime > dtime)
                    {
                        html.Attributes.Add("class", "dashboard__box dashboard__box-overdue");
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
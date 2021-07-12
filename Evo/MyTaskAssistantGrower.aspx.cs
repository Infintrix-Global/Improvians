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
    public partial class MyTaskAssistantGrower : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        DataTable dt = new DataTable();
        NameValueCollection nv = new NameValueCollection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  BindDepartment();
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
        //public void BindDepartment()
        //{
        //    ddlDept.DataSource = objCommon.GetDepartmentMaster();
        //    ddlDept.DataTextField = "DepartmentName";
        //    ddlDept.DataValueField = "DepartmentID";
        //    ddlDept.DataBind();
        //    ddlDept.Items.Insert(0, new ListItem("--- Select ---", "0"));
        //}

        public void CountTotal()
        {
            DataSet dt = new DataSet();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());

            dt = objCommon.GetDataSet("SP_GetAssistantGrowerEachTaskCountNew", nv);

            lblPutAway.Text = dt.Tables[0].Rows.Count.ToString();
            lblGerm.Text = dt.Tables[1].Rows.Count.ToString();
            lblFer.Text = dt.Tables[2].Rows.Count.ToString();
            lblIrr.Text = dt.Tables[3].Rows.Count.ToString();
            lblpr.Text = dt.Tables[4].Rows.Count.ToString();
            lblCropHealthReport.Text = dt.Tables[6].Rows.Count.ToString();

            lblChemical.Text = dt.Tables[7].Rows.Count.ToString();
            lblDumpTotal.Text = dt.Tables[8].Rows.Count.ToString();
            lblMove.Text = dt.Tables[10].Rows.Count.ToString();
            lblGeneralTotal.Text = dt.Tables[9].Rows.Count.ToString();

        }
        private void BindGridGen()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Status", "0");
            nv.Add("@BenchLocation", "");

            nv.Add("@Jobsource", "");
            nv.Add("@FromDate", "");
            nv.Add("@ToDate", "");
            nv.Add("@AssignedBy", "0");
            nv.Add("@Crop", "0");
            dt = objCommon.GetDataTable("SP_GetGeneralRequestAssistantGrower", nv);
            BindData(dt, Gen, "GeneralTaskDate");
        }

        private void BindGridDum()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Status", "0");
            nv.Add("@BenchLocation", "");

            nv.Add("@Jobsource", "0");
            nv.Add("@FromDate", "");
            nv.Add("@ToDate", "");
            nv.Add("@AssignedBy", "0");
            nv.Add("@Crop", "0");
            dt = objCommon.GetDataTable("SP_GetDumpRequestAssistantGrower", nv);
            BindData(dt, Dum, "DumpDateR");
        }

        private void BindGridMov()
        {
            dt = new DataTable();
            nv.Clear();
         
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Facility", Session["Facility"].ToString());

            nv.Add("@BenchLocation", "");
            nv.Add("@Jobsource", "");

            nv.Add("@FromDate", "");
            nv.Add("@ToDate", "");
            nv.Add("@AssignedBy", "0");
            nv.Add("@Crop", "0");
            nv.Add("@LoginId", Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("SP_GetMoveRequestAssistantGrower", nv);
            BindData(dt, Mov, "MoveDate");
        }

        private void BindGridPR()
        {

            DataTable dtSHDate = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            dtSHDate = objCommon.GetDataTable("GetPlantReadyShiftNo", nv);

            string Fdate = "", TDate = "";
            Fdate = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(Convert.ToInt32(dtSHDate.Rows[0]["ShiftNo"])).ToString("yyyy-MM-dd");





            dt = new DataTable();
            nv.Clear();
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Status", "0");
            nv.Add("@BenchLocation", "");

            nv.Add("@Jobsource", "");
            nv.Add("@FromDate", Fdate);
            nv.Add("@ToDate", TDate);
            nv.Add("@AssignedBy", "0");
            nv.Add("@Crop", "0");

            //nv.Add("@ToDate", DateTime.Now.ToString("yyyy-MM-dd"));
            dt = objCommon.GetDataTable("SP_GetPlantReadyRequestAssistantGrower", nv);
            BindData(dt, PR, "PlantReadySeedDate");
        }

        private void BindGridCrop()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@Status", "0");
            nv.Add("@BenchLocation", "");
            nv.Add("@Jobsource", "0");
            nv.Add("@LoginId", Session["LoginID"].ToString());
            nv.Add("@FromDate", "");
            nv.Add("@ToDate", "");
            nv.Add("@AssignedBy", "0");
            nv.Add("@Crop", "0");
            dt = objCommon.GetDataTable("SP_GetCropReportRequestAssistantGrower", nv);
            BindData(dt, Crop, "CropHealthReportDate");
        }

        private void BindGridIrr()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@Status", "0");
            nv.Add("@BenchLocation", "0");
            nv.Add("@Jobsource", "");
            nv.Add("@GermNo", "0");
            nv.Add("@FromDate", "");
            nv.Add("@ToDate", "");
            nv.Add("@AssignedBy", "0");
            nv.Add("@Crop", "0");
            dt = objCommon.GetDataTable("SP_GetIrrigationRequestAssistantGrower", nv);
            BindData(dt, Irr, "IrrigateSeedDate");
        }

        private void BindGridChem()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@Status", "0");
            nv.Add("@BenchLocation", "0");
            nv.Add("@Jobsource", "");
            nv.Add("@GermNo", "0");
            nv.Add("@FromDate", "");
            nv.Add("@ToDate", "");
            nv.Add("@AssignedBy", "0");
            nv.Add("@Crop", "0");
            dt = objCommon.GetDataTable("SP_GetChemicalRequestAssistantGrower", nv);
            BindData(dt, Chem, "ChemicalSeedDate");
        }

        private void BindGridFer()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@Status", "0");
            nv.Add("@BenchLocation", "0");
            nv.Add("@Jobsource", "");
            nv.Add("@GermNo", "0");
            nv.Add("@FromDate", "");
            nv.Add("@ToDate", "");
            nv.Add("@AssignedBy", "0");
            nv.Add("@Crop", "0");
            dt = objCommon.GetDataTable("SP_GetFertilizerRequestAAssistantGrower", nv);
            BindData(dt, Fer, "FertilizeSeedDate");
        }

        private void BindGridPutAway()
        {
            dt = new DataTable();
            nv.Clear();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Status", "0");
            nv.Add("@BenchLocation", "0");
            nv.Add("@Jobsource", "0");
            nv.Add("@FromDate", "");
            nv.Add("@ToDate", "");
            nv.Add("@AssignedBy", "0");
            nv.Add("@Crop", "0");


            dt = objCommon.GetDataTable("SP_GetMoveSiteTeamTasknew", nv);
            BindData(dt, Put, "SeededDate");
        }

        public void BindGridGerm()
        {
            dt = new DataTable();
            nv.Clear();
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", "0");

            nv.Add("@Status", "");
            nv.Add("@Jobsource", "");
            nv.Add("@GermNo", "");
            nv.Add("@FromDate", "");
            nv.Add("@ToDate", "");
            nv.Add("@AssignedBy", "0");
            nv.Add("@Crop", "0");
            nv.Add("@LoginID", Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("SP_GetGerminationRequestAssistantGrower", nv);
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

                    switch (html.Name)
                    {
                        case "Put":
                            dtime = dtime.AddDays(1);
                            break;

                    }

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Improvians.Bal;
using Improvians.BAL_Classes;

namespace Improvians
{
    public partial class FerJobBuildUp : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFertilizer();
                //BindUnit();

                if (Request.QueryString["Bench"] != null)
                {
                    Bench = Request.QueryString["Bench"].ToString();
                }
                txtDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                lblbench.Text = Bench;
                BindGridFerReq();
                BindGridFerDetails();
                BindSupervisor();
                BindSQFTofBench();
            }
        }

        public void BindSupervisor()
        {

            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@RoleID", "11");
            //ddlsupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;
            //ddlsupervisor.DataTextField = "EmployeeName";
            //ddlsupervisor.DataValueField = "ID";
            //ddlsupervisor.DataBind();
            //ddlsupervisor.Items.Insert(0, new ListItem("--Select--", "0"));


            NameValueCollection nv = new NameValueCollection();
            if (Session["Role"].ToString() == "1")
            {
                ddlsupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlsupervisor.DataTextField = "EmployeeName";
                ddlsupervisor.DataValueField = "ID";
                ddlsupervisor.DataBind();
                ddlsupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            if (Session["Role"].ToString() == "12")
            {
                ddlsupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlsupervisor.DataTextField = "EmployeeName";
                ddlsupervisor.DataValueField = "ID";
                ddlsupervisor.DataBind();
                ddlsupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }


        private string Bench
        {
            get
            {
                if (ViewState["Bench"] != null)
                {
                    return (string)ViewState["Bench"];
                }
                return "";
            }
            set
            {
                ViewState["Bench"] = value;
            }
        }
        public void BindGridFerReq()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Facility", "0");
            nv.Add("@BenchLocation", Bench);

            dt = objCommon.GetDataTable("SP_GetFertilizerRequest", nv);
            gvFer.DataSource = dt;
            gvFer.DataBind();

            int tray = 0;
            foreach (GridViewRow row in gvFer.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                tray = tray + Convert.ToInt32((row.FindControl("lblTotTray") as Label).Text);
                //}

            }
            txtTrays.Text = tray.ToString();
        }

        public void BindGridFerDetails()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", Bench);
            dt = objCommon.GetDataTable("SP_GetFertilizerRequestDetails", nv);

            DataTable dtManual = objFer.GetManualFertilizerRequest("", Bench,"");
            if (dtManual != null && dtManual.Rows.Count > 0)
            {
                dt.Merge(dtManual);
                dt.AcceptChanges();
            }
            gvJobHistory.DataSource = dt;
            gvJobHistory.DataBind();
        }

     


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int FertilizationCode = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Mode", "12");
            dt = objCommon.GetDataTable("GET_Common", nv1);
            FertilizationCode = Convert.ToInt32(dt.Rows[0]["FCode"]);


            foreach (GridViewRow row in gvFer.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{

                long result = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", ddlsupervisor.SelectedValue);
                nv.Add("@Type", radtype.SelectedValue);
                nv.Add("@WorkOrder", (row.FindControl("lblwo") as Label).Text);
                nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                //nv.Add("@WorkOrder", lblwo.Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@FertilizationCode", FertilizationCode.ToString());
                nv.Add("@FertilizationDate", txtDate.Text);

                result = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequest", nv);

                //  }

            }

            foreach (GridViewRow row in gvJobHistory.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                if ((row.FindControl("lblGrowerputawayID") as Label).Text =="0")
                {
                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlsupervisor.SelectedValue);
                    nv.Add("@Type", radtype.SelectedValue);
                    nv.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                    nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                    nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    //nv.Add("@WorkOrder", lblwo.Text);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@FertilizationCode", FertilizationCode.ToString());
                    nv.Add("@FertilizationDate", txtDate.Text);
                    result = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManual", nv);
                }
                else
                {
                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlsupervisor.SelectedValue);
                    nv.Add("@Type", radtype.SelectedValue);
                    nv.Add("@WorkOrder", (row.FindControl("lblwo") as Label).Text);
                    nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    //nv.Add("@WorkOrder", lblwo.Text);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@FertilizationCode", FertilizationCode.ToString());
                    nv.Add("@FertilizationDate", txtDate.Text);

                    result = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequest", nv);
                }
                //  }

            }
            dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text,"", txtTrays.Text, txtSQFT.Text);
            objTask.AddFertilizerRequestDetails(dtTrays, "0", FertilizationCode, lblbench.Text,txtBenchIrrigationFlowRate.Text,txtBenchIrrigationCoverage.Text,txtSprayCoverageperminutes.Text);

            string message = "Assignment Successful";
            string url = "MyTaskGrower.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

            Clear();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }


        public void Clear()
        {
            txtQty.Text = "";
            txtSQFT.Text = "";
            txtTrays.Text = "";
            txtBenchIrrigationFlowRate.Text = "";
            txtBenchIrrigationCoverage.Text = "";
            txtSprayCoverageperminutes.Text = "";
            radtype.SelectedValue = "Fertilizer";
            BindFertilizer();
            dtTrays.Clear();
        }

        protected void radtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radtype.SelectedValue == "Fertilizer")
            {
                // gvFerDetails.HeaderRow.Cells[0].Text = "Fertilizer";
                lbltype.Text = "Fertilizer";
                dtTrays.Rows.Clear();
                //  gvFerDetails.DataSource = dtTrays;
                //gvFerDetails.DataBind();
                BindFertilizer();


            }
            else if (radtype.SelectedValue == "Chemical")
            {

                //gvFerDetails.HeaderRow.Cells[0].Text = "Chemical";
                lbltype.Text = "Chemical";
                dtTrays.Rows.Clear();
                // gvFerDetails.DataSource = dtTrays;
                //gvFerDetails.DataBind();
                BindChemical();
            }
        }

        public void BindSQFTofBench()
        {

            DataTable dtSQFT = objFer.GetSQFTofBench(lblbench.Text);
            if (dtSQFT != null && dtSQFT.Rows.Count > 0)
            {
                txtSQFT.Text = Convert.ToDecimal(dtSQFT.Rows[0]["Sqft"]).ToString("#,0000.00");
            }
            else
            {
                txtSQFT.Text = "0.00";
            }
        }



        public void BindChemical()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlFertilizer.DataSource = objFer.GetChemicalList();
            ddlFertilizer.DataTextField = "Name";
            ddlFertilizer.DataValueField = "No_";
            ddlFertilizer.DataBind();
            ddlFertilizer.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindFertilizer()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlFertilizer.DataSource = objFer.GetFertilizerList();
            ddlFertilizer.DataTextField = "Name";
            ddlFertilizer.DataValueField = "No_";
            ddlFertilizer.DataBind();
            ddlFertilizer.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        //public void BindUnit()
        //{
        //    NameValueCollection nv = new NameValueCollection();
        //    ddlUnit.DataSource = objFer.GetUnitList();
        //    ddlUnit.DataTextField = "Description";
        //    ddlUnit.DataValueField = "Code";
        //    ddlUnit.DataBind();
        //    ddlUnit.Items.Insert(0, new ListItem("--- Select ---", "0"));
        //}

    }
}
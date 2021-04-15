using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Bal;
using Evo.BAL_Classes;
using System.Text.RegularExpressions;

namespace Evo
{
    public partial class ChemicalStart : System.Web.UI.Page
    {
        public static DataTable dtCTrays = new DataTable()
        { Columns = { "Fertilizer", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();
        clsCommonMasters objMaster = new clsCommonMasters();
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



                if (Request.QueryString["Start"] != null)
                {
                    StartButton = Request.QueryString["Start"].ToString();
                }

                if (Request.QueryString["jobCode"] != null)
                {
                    JobCode = Request.QueryString["jobCode"].ToString();
                }
                txtDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
              
             
              
             //   BindGridFerReqView(Request.QueryString["CCode"].ToString());

                if (Request.QueryString["CCode"].ToString() != "0")
                {
                    BindGridFerReqView(Request.QueryString["CCode"].ToString());
                }
                else
                {
                    BindGridFerDetails("'" + Bench + "'", "'" + JobCode + "'");
                    BindSQFTofBench("'" + Bench + "'");
                }
            }
        }


        public void BindGridFerReqView(string Foce)
        {
         


            int P = 0;
            string Q = "";
            string bjno = "";
            string chkSelected = "";
            string JobNo = "";
            DataSet dt = new DataSet();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Ccode", Foce);

            dt = objCommon.GetDataSet("SP_GetTaskAssignmentChemicalView", nv);

            if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            {
                ddlFertilizer.SelectedItem.Text = dt.Tables[0].Rows[0]["Fertilizer"].ToString();
                 ddlMethod.SelectedItem.Text = dt.Tables[0].Rows[0]["Method"].ToString();
            }


            if (dt.Tables[1] != null && dt.Tables[1].Rows.Count > 0)
            {
                DataColumn col = dt.Tables[1].Columns["GreenHouseID"];
                DataColumn col1 = dt.Tables[1].Columns["JobCode"];
                foreach (DataRow row in dt.Tables[1].Rows)
                {
                    //strJsonData = row[col].ToString();

                    P = 1;
                    Q += "'" + row[col].ToString() + "',";

                    bjno += "'" + row[col1].ToString() + "',";
                }
                if (P > 0)
                {
                    chkSelected = Q.Remove(Q.Length - 1, 1);
                    JobNo = bjno.Remove(bjno.Length - 1, 1);

                }
                else
                {

                }
            }
            BindGridFerDetails(chkSelected, JobNo);
            BindSQFTofBench(chkSelected);

        }

      
        private string Jid
        {
            get
            {
                if (ViewState["Jid"] != null)
                {
                    return (string)ViewState["Jid"];
                }
                return "";
            }
            set
            {
                ViewState["Jid"] = value;
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

        private string JobCode
        {
            get
            {
                if (ViewState["JobCode"] != null)
                {
                    return (string)ViewState["JobCode"];
                }
                return "";
            }
            set
            {
                ViewState["JobCode"] = value;
            }
        }

        private string StartButton
        {
            get
            {
                if (ViewState["StartButton"] != null)
                {
                    return (string)ViewState["StartButton"];
                }
                return "";
            }
            set
            {
                ViewState["StartButton"] = value;
            }
        }


     

        public void BindGridFerDetails(string BenchLoc, string JobNo)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", BenchLoc);
            dt = objCommon.GetDataTable("SP_GetFertilizerRequestDetails", nv);

            DataTable dtManual = objTask.GetManualRequestStart(Session["Facility"].ToString(), BenchLoc, JobNo);

            if (dt != null && dt.Rows.Count > 0 && dtManual != null && dtManual.Rows.Count > 0)
            {
                dt.Merge(dtManual);
                dt.AcceptChanges();
                gvJobHistory.DataSource = dt;
                gvJobHistory.DataBind();

            }
            else if (dtManual != null && dtManual.Rows.Count > 0)
            {
                gvJobHistory.DataSource = dtManual;
                gvJobHistory.DataBind();

            }
            else
            {
                gvJobHistory.DataSource = dt;
                gvJobHistory.DataBind();


            }
           // gvJobHistory.DataSource = dt;
           // gvJobHistory.DataBind();

            decimal tray = 0;
            foreach (GridViewRow row in gvJobHistory.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                tray = tray + Convert.ToDecimal((row.FindControl("lblTotTray") as Label).Text);
                //}

            }



            txtTrays.Text = tray.ToString();

        }

        //protected void gvJobHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvJobHistory.PageIndex = e.NewPageIndex;

        //}



        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            int ChemicalCode = 0;
            string Batchlocation = "";

            foreach (GridViewRow row in gvJobHistory.Rows)
            {
                dtCTrays.Clear();
                Batchlocation = (row.FindControl("lblGreenHouse") as Label).Text;

                NameValueCollection nv5 = new NameValueCollection();
                nv5.Add("@Mode", "2");
                nv5.Add("@Batchlocation", Batchlocation);
                DataTable dt = objCommon.GetDataTable("GET_CheckBatchlocation", nv5);

                if (dt != null && dt.Rows.Count > 0)
                {
                    ChemicalCode = Convert.ToInt32(dt.Rows[0]["ChemicalCode"]);
                }
                else
                {
                    dtCTrays.Clear();
                    DataTable dt1 = new DataTable();
                    NameValueCollection nv1 = new NameValueCollection();
                    nv1.Add("@Mode", "16");
                    dt1 = objCommon.GetDataTable("GET_Common", nv1);
                    ChemicalCode = Convert.ToInt32(dt1.Rows[0]["CCode"]);


                    dtCTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtTrays.Text, txtSQFT.Text);
                    objTask.AddChemicalRequestDetails(dtCTrays, "0", ChemicalCode, Batchlocation, txtResetSprayTaskForDays.Text, ddlMethod.SelectedValue, txtComments.Text);


                }

                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                   // nv.Add("@SupervisorID", ddlsupervisor.SelectedValue);
                    nv.Add("@Type", "Chemical");
                    nv.Add("@WorkOrder", (row.FindControl("lblwo") as Label).Text);
                    nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    //nv.Add("@WorkOrder", lblwo.Text);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@ChemicalCode", ChemicalCode.ToString());
                    nv.Add("@ChemicalDate", txtDate.Text);

                    nv.Add("@Jid", (row.FindControl("lbljid") as Label).Text);
                    nv.Add("@Comments", txtComments.Text);
                    nv.Add("@Method", ddlMethod.SelectedValue);
                

                    nv.Add("@SupervisorID", Session["LoginID"].ToString());
                    result = objCommon.GetDataExecuteScaler("SP_AddChemicalRequestManualCreateTaskStart", nv);
                   

            }

      //      dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtTrays.Text, txtSQFT.Text);
        //    objTask.AddChemicalRequestDetails(dtTrays, ddlFertilizer.SelectedValue, ChemicalCode, lblbench.Text, txtResetSprayTaskForDays.Text, ddlMethod.SelectedValue, txtComments.Text);

            string url = "";
            if (Session["Role"].ToString() == "1")
            {
                url = "MyTaskGrower.aspx";
            }
            else
            {
                url = "MyTaskAssistantGrower.aspx";
            }
            string message = "Assignment Successful";
            //  string url = "MyTaskGrower.aspx";
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

            txtSQFT.Text = "";
            txtTrays.Text = "";

            BindFertilizer();
            dtCTrays.Clear();
        }

        public void BindSQFTofBench(string Bench)
        {

            //  DataTable dtSQFT = objFer.GetSQFTofBench(lblbench.Text);
            DataTable dtSQFT = objFer.GetSQFTofBenchNew(Bench);
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
            ddlFertilizer.DataSource = objFer.GetChemicalList();
            ddlFertilizer.DataTextField = "Name";
            ddlFertilizer.DataValueField = "No_";
            ddlFertilizer.DataBind();
            ddlFertilizer.Items.Insert(0, new ListItem("--- Select ---", "0"));

            ddlMethod.DataSource = objMaster.GetAllChemicalList();
            ddlMethod.DataTextField = "ChemicalName";
            ddlMethod.DataValueField = "ChemicalName";
            ddlMethod.DataBind();
            ddlMethod.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

    








    }
}
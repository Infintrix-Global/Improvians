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
    public partial class FertilizerTaskStart : System.Web.UI.Page
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

               

                if (Request.QueryString["jobCode"] != null)
                {
                    JobCode = Request.QueryString["jobCode"].ToString();
                }

                if (Request.QueryString["Bench"] != null)
                {
                    Bench = Request.QueryString["Bench"].ToString();
                }

                txtDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");

             
          
                BindFertilizer();
                if (Request.QueryString["FCode"].ToString() != "0")
                {
                    BindGridFerReqView(Request.QueryString["FCode"].ToString());
                }
                else
                {
                    BindGridFerDetails("'" + Bench + "'", "'" + JobCode + "'");
                    BindSQFTofBench("'" + Bench + "'");
                }
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

        private string Pid
        {
            get
            {
                if (ViewState["Pid"] != null)
                {
                    return (string)ViewState["Pid"];
                }
                return "";
            }
            set
            {
                ViewState["Pid"] = value;
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

        protected void RadioBench_SelectedIndexChanged(object sender, EventArgs e)
        {
            // SelectBenchLocation();
            string chkSelected = "";
            if (RadioBench.SelectedValue == "1")
            {
                // Bench
                //  SelectBench();
                PanelBench.Visible = true;
                PanelBenchesInHouse.Visible = false;
                PanelHouse.Visible = false;
                int P1 = 0;
                string Q1 = "";

                string YourString = Bench;

                YourString = YourString.Remove(YourString.Length - 1);

                DataTable dt12 = objFer.GetSelectBench(YourString);
                if (dt12 != null && dt12.Rows.Count > 0)
                {
                    lblBench1.Text = dt12.Rows[0]["PositionCode"].ToString();


                    if (dt12 != null && dt12.Rows.Count > 0)
                    {
                        DataColumn col = dt12.Columns["PositionCode"];
                        foreach (DataRow row in dt12.Rows)
                        {
                            //strJsonData = row[col].ToString();

                            P1 = 1;
                            Q1 += "'" + row[col].ToString() + "',";
                        }
                        if (P1 > 0)
                        {
                            chkSelected = Q1.Remove(Q1.Length - 1, 1);

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        chkSelected = "'" + Bench + "'";
                    }


                    DataTable dt123 = new DataTable();
                    gvJobHistory.DataSource = dt123;
                    gvJobHistory.DataBind();
                    BindGridFerDetails(chkSelected,"");
                    BindSQFTofBench(chkSelected);
                }
            }
            else if (RadioBench.SelectedValue == "2")
            {
                SelectBenchLocation();
                PanelBench.Visible = false;
                PanelBenchesInHouse.Visible = true;
                PanelHouse.Visible = false;


            }
            else if (RadioBench.SelectedValue == "3")
            {
                // House
                PanelBench.Visible = false;
                PanelBenchesInHouse.Visible = false;
                PanelHouse.Visible = true;
                int P = 0;
                string Q = "";
                string[] words = Regex.Split(Bench, @"\W+");

                DataTable dt = objFer.GetSelectBenchLocation(words[0], words[1]);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataColumn col = dt.Columns["PositionCode"];
                    foreach (DataRow row in dt.Rows)
                    {
                        //strJsonData = row[col].ToString();

                        P = 1;
                        Q += "'" + row[col].ToString() + "',";
                    }
                    if (P > 0)
                    {
                        chkSelected = Q.Remove(Q.Length - 1, 1);

                    }
                    else
                    {

                    }
                }
                else
                {
                    chkSelected = "'" + Bench + "'";
                }



                DataTable dt123 = new DataTable();
                gvJobHistory.DataSource = dt123;
                gvJobHistory.DataBind();
                BindGridFerDetails(chkSelected,"");
                BindSQFTofBench(chkSelected);
            }
            else
            {

            }


        }


        protected void ListBoxBenchesInHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            int c = 0;
            string x = "";
            string chkSelected = "";
            foreach (ListItem item in ListBoxBenchesInHouse.Items)
            {

                if (item.Selected)
                {
                    c = 1;
                    x += "'" + item.Text + "',";

                }
            }
            if (c > 0)
            {
                chkSelected = x.Remove(x.Length - 1, 1);

            }
            else
            {

            }

            BindGridFerDetails(chkSelected,"");
            BindSQFTofBench(chkSelected);
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string chkSelected = "";
            if (RadioBench.SelectedValue == "1")
            {
                chkSelected = "'" + lblBench1.Text + "'";
            }
            else if (RadioBench.SelectedValue == "2")
            {
                int c = 0;
                string x = "";

                foreach (ListItem item in ListBoxBenchesInHouse.Items)
                {

                    if (item.Selected)
                    {
                        c = 1;
                        x += "'" + item.Text + "',";

                    }
                }
                if (c > 0)
                {
                    chkSelected = x.Remove(x.Length - 1, 1);

                }
                else
                {

                }


            }
            else if (RadioBench.SelectedValue == "3")
            {


            }

            BindGridFerDetails(chkSelected,"");



        }

        public void SelectBench()
        {
            string YourString = Bench;

            // ENC2 - SHADE - 2 - A
            //string input = Bench;
            //string[] array = input.Split('-');
            YourString = YourString.Remove(YourString.Length - 1);

            DataTable dt = objFer.GetSelectBench(YourString);

            lblBench1.Text = dt.Rows[0]["PositionCode"].ToString();

        }



        public void SelectBenchLocation()
        {


            // ENC2 - SHADE - 2 - A
            //string input = Bench;
            //string[] array = input.Split('-');

            string[] words = Regex.Split(Bench, @"\W+");

            DataTable dt = objFer.GetSelectBenchLocation(words[0], words[1]);

            ListBoxBenchesInHouse.DataSource = dt;
            ListBoxBenchesInHouse.DataTextField = "PositionCode";
            ListBoxBenchesInHouse.DataValueField = "PositionCode";
            ListBoxBenchesInHouse.DataBind();

        }

        public void BindGridFerReqView(string Foce)
        {
            //DataTable dt = new DataTable();
            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@Foce", Foce);

            //dt = objCommon.GetDataTable("SP_GetTaskAssignmentFertilizationView", nv);
            int P = 0;
            string Q = "";
            string bjno = "";
            string chkSelected = "";
            string JobNo = "";
            DataSet dt = new DataSet();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Foce", Foce);
            dt = objCommon.GetDataSet("SP_GetTaskAssignmentFertilizationView", nv);

            if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            {
                ddlFertilizer.SelectedItem.Text = dt.Tables[0].Rows[0]["Fertilizer"].ToString();
                //    Quantity
                txtQty.Text = dt.Tables[0].Rows[0]["Quantity"].ToString();
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
        ///    gvJobHistory.DataSource = dt;
      //      gvJobHistory.DataBind();
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
            int FertilizationCode = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Mode", "12");
            dt = objCommon.GetDataTable("GET_Common", nv1);
            FertilizationCode = Convert.ToInt32(dt.Rows[0]["FCode"]);



            foreach (GridViewRow row in gvJobHistory.Rows)
            {
                string Batchlocation = "";
                Batchlocation = (row.FindControl("lblGreenHouse") as Label).Text;

                NameValueCollection nv5 = new NameValueCollection();
                nv5.Add("@Mode", "1");
                nv5.Add("@Batchlocation", Batchlocation);
                DataTable dt23 = objCommon.GetDataTable("GET_CheckBatchlocation", nv5);

                if (dt23 != null && dt23.Rows.Count > 0)
                {

                    FertilizationCode = Convert.ToInt32(dt23.Rows[0]["FertilizationCode"]);
                }
                else
                {
                    dtTrays.Clear();
                    DataTable dt1 = new DataTable();
                    NameValueCollection nv14 = new NameValueCollection();
                    NameValueCollection nvimg = new NameValueCollection();
                    nv14.Add("@Mode", "12");
                    dt1 = objCommon.GetDataTable("GET_Common", nv14);
                    FertilizationCode = Convert.ToInt32(dt1.Rows[0]["FCode"]);

                    dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text, "", txtTrays.Text, txtSQFT.Text);

                    objTask.AddFertilizerRequestDetailsCreatTask(dtTrays, "0", FertilizationCode, Batchlocation, "", "", "", txtResetSprayTaskForDays.Text, "","");
                }


                //if ((row.FindControl("lblGrowerputawayID") as Label).Text == "0")
                //{
                //    long result = 0;
                //    NameValueCollection nv = new NameValueCollection();
                //    nv.Add("@SupervisorID", Session["LoginID"].ToString());
                //    nv.Add("@Type", "Fertilizer");
                //    nv.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                //    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                //    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                //    nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                //    nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                //    nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                //    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                //    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                //    //nv.Add("@WorkOrder", lblwo.Text);
                //    nv.Add("@LoginID", Session["LoginID"].ToString());
                //    nv.Add("@FertilizationCode", FertilizationCode.ToString());
                //    nv.Add("@FertilizationDate", txtDate.Text);

                //    result = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManual", nv);
                //}
                //else
                //{

                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                 
                    nv.Add("@Type", "Fertilizer");
                    nv.Add("@WorkOrder", (row.FindControl("lblwo") as Label).Text);
                    nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
              
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@FertilizationCode", FertilizationCode.ToString());
                    nv.Add("@FertilizationDate", txtDate.Text);
                    nv.Add("@Jid", (row.FindControl("lbljid") as Label).Text);
                    nv.Add("@SupervisorID", Session["LoginID"].ToString());
                    result = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManualCreateTaskStartJobbuil", nv);


            //    }
                //  }


            }


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
            //   string url = "MyTaskGrower.aspx";
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
            //txtBenchIrrigationFlowRate.Text = "";
            //txtBenchIrrigationCoverage.Text = "";
            //txtSprayCoverageperminutes.Text = "";

            BindFertilizer();
            dtTrays.Clear();
        }



        


        //public void BindChemical()
        //{
        //    NameValueCollection nv = new NameValueCollection();
        //    ddlFertilizer.DataSource = objFer.GetChemicalList();
        //    ddlFertilizer.DataTextField = "Name";
        //    ddlFertilizer.DataValueField = "No_";
        //    ddlFertilizer.DataBind();
        //    ddlFertilizer.Items.Insert(0, new ListItem("--- Select ---", "0"));
        //}

        public void BindFertilizer()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlFertilizer.DataSource = objFer.GetFertilizerList();
            ddlFertilizer.DataTextField = "Name";
            ddlFertilizer.DataValueField = "No_";
            ddlFertilizer.DataBind();
            ddlFertilizer.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {

            RadioBench.Items[0].Selected = false;
            ListBoxBenchesInHouse.Items.Clear();
            //To unselect all Items
            RadioBench.ClearSelection();
            BindGridFerDetails("'" + Bench + "'","");
            PanelBench.Visible = false;
            PanelBenchesInHouse.Visible = false;
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
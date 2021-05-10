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
    public partial class ChemicalJobBuildUp : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
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


                if (Request.QueryString["CCode"] != null)
                {
                    CCode = Request.QueryString["CCode"].ToString();
                }
                if (Request.QueryString["TaskRequestKey"] != null)
                {
                    TaskRequestKey = Request.QueryString["TaskRequestKey"].ToString();
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
                lblbench.Text = Bench;
                BindGridFerReq();
                BindGridFerDetails("'" + Bench + "'");
                BindGridFerReqView(Request.QueryString["CCode"].ToString());
                BindSupervisor();
                BindSQFTofBench("'" + Bench + "'");
            }
        }


        public void BindGridFerReqView(string Foce)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Ccode", Foce);

            dt = objCommon.GetDataTable("SP_GetTaskAssignmentChemicalView", nv);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlFertilizer.SelectedItem.Text = dt.Rows[0]["Fertilizer"].ToString();
                ddlMethod.SelectedItem.Text = dt.Rows[0]["Method"].ToString();
                txtComments.Text = dt.Rows[0]["Comments"].ToString();
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

        private string TaskRequestKey
        {
            get
            {
                if (ViewState["TaskRequestKey"] != null)
                {
                    return (string)ViewState["TaskRequestKey"];
                }
                return "";
            }
            set
            {
                ViewState["TaskRequestKey"] = value;
            }
        }

        private string CCode
        {
            get
            {
                if (ViewState["CCode"] != null)
                {
                    return (string)ViewState["CCode"];
                }
                return "";
            }
            set
            {
                ViewState["CCode"] = value;
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
                    BindGridFerDetails(chkSelected);
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
                BindGridFerDetails(chkSelected);
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

            BindGridFerDetails(chkSelected);
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

            BindGridFerDetails(chkSelected);


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


        public void BindGridFerReq()
        {
            //DataTable dt = new DataTable();
            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@JobCode", JobCode);
            //nv.Add("@CustomerName", "0");
            //nv.Add("@Facility", "0");
            //nv.Add("@BenchLocation", Bench);
            //nv.Add("@RequestType", "0");
            //nv.Add("@FromDate", "");
            //nv.Add("@ToDate", "");



            //if (Session["Role"].ToString() == "12")
            //{
            //    dt = objCommon.GetDataTable("SP_GetChemicalRequestAssistantGrower", nv);
            //}
            //else
            //{
            //    dt = objCommon.GetDataTable("SP_GetChemicalRequest", nv);
            //}


            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", JobCode);
            nv.Add("@BenchLocation", Bench);
            dt = objCommon.GetDataTable("SP_GetChemicalRequestStart", nv);
            gvFer.DataSource = dt;
            gvFer.DataBind();

            //   Jid = dt.Rows[0]["GrowerPutAwayId"].ToString();
            Jid = dt.Rows[0]["Jid"].ToString();

            decimal tray = 0;
            foreach (GridViewRow row in gvFer.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                tray = tray + Convert.ToDecimal((row.FindControl("lblTotTray") as Label).Text);
                //}

            }
            txtTrays.Text = tray.ToString();
        }

        public void BindGridFerDetails(string BenchLoc)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", BenchLoc);
            dt = objCommon.GetDataTable("SP_GetFertilizerRequestDetails", nv);

            DataTable dtManual = objTask.GetManualRequestStart1(Session["Facility"].ToString(), BenchLoc, "'" + JobCode + "'");


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


            decimal tray = 0;
            foreach (GridViewRow row in gvJobHistory.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                tray = tray + Convert.ToDecimal((row.FindControl("lblTotTray") as Label).Text);
                //}

            }


            txtTrays.Text =(Convert.ToInt32(txtTrays.Text)+ tray).ToString();

        }

        //protected void gvJobHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvJobHistory.PageIndex = e.NewPageIndex;

        //}



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int ChemicalCode = 0;

            if (CCode == "0")
            {
                DataTable dt = new DataTable();
                NameValueCollection nv1 = new NameValueCollection();
                nv1.Add("@Mode", "16");
                dt = objCommon.GetDataTable("GET_Common", nv1);
                ChemicalCode = Convert.ToInt32(dt.Rows[0]["CCode"]);

            }
            else
            {
                ChemicalCode = Convert.ToInt32(CCode);
            }


            long resultRID = 0;
            foreach (GridViewRow row in gvFer.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{


                long Mresult = 0;
                NameValueCollection nv = new NameValueCollection();

                nv.Add("@Type", "Chemical");
                nv.Add("@WorkOrder", (row.FindControl("lblwo") as Label).Text);
                nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                //nv.Add("@WorkOrder", lblwo.Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@ChemicalCode", ChemicalCode.ToString());
                nv.Add("@ChemicalDate", txtDate.Text);
                nv.Add("@Comments", txtComments.Text);
                nv.Add("@Method", ddlMethod.SelectedValue);
                nv.Add("@Jid", Jid);
                nv.Add("@TaskRequestKey", TaskRequestKey);
                nv.Add("@SupervisorID", ddlsupervisor.SelectedValue);
                nv.Add("@BanchLocation", (row.FindControl("lblGreenHouse") as Label).Text);

                resultRID = objCommon.GetDataExecuteScaler("SP_AddChemicalRequest", nv);


            

                NameValueCollection nameValue = new NameValueCollection();
                nameValue.Add("@LoginID", Session["LoginID"].ToString());
                nameValue.Add("@jobcode", JobCode);
                nameValue.Add("@GreenHouseID", Bench);
                nameValue.Add("@TaskName", "Chemical");


                var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);


                NameValueCollection nvn = new NameValueCollection();
                nvn.Add("@LoginID", Session["LoginID"].ToString());
                nvn.Add("@SupervisorID", ddlsupervisor.SelectedValue);
                nvn.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                nvn.Add("@TaskName", "Chemical");
                nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);


            }

            //dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtTrays.Text, txtSQFT.Text);
            //objTask.AddChemicalRequestDetails(dtTrays, resultRID.ToString(), ddlFertilizer.SelectedItem.Text, ChemicalCode, lblbench.Text, txtResetSprayTaskForDays.Text, ddlMethod.SelectedItem.Text, txtComments.Text);

            long result = 0;
            foreach (GridViewRow row in gvJobHistory.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                //if ((row.FindControl("lblGrowerputawayID") as Label).Text == "0")
                //{
                //    long result = 0;
                //    NameValueCollection nv = new NameValueCollection();
                //    nv.Add("@SupervisorID", ddlsupervisor.SelectedValue);
                //    nv.Add("@Type", "Chemical");
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
                //    nv.Add("@ChemicalCode", ChemicalCode.ToString());
                //    nv.Add("@ChemicalDate", txtDate.Text);
                //    nv.Add("@Comments", txtComments.Text);
                //    nv.Add("@Method", ddlMethod.SelectedValue);

                //    result = objCommon.GetDataExecuteScaler("SP_AddChemicalRequestManual", nv);
                //}
                //else
                //{

                //nv.Add("@WorkOrder", lblwo.Text);



                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", ddlsupervisor.SelectedValue);
                nv.Add("@Type", "Chemical");
                nv.Add("@WorkOrder", (row.FindControl("lblwo") as Label).Text);
                nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                //nv.Add("@WorkOrder", lblwo.Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@ChemicalCode", ChemicalCode.ToString());
                nv.Add("@ChemicalDate", txtDate.Text);
                nv.Add("@TaskRequestKey", TaskRequestKey);
                nv.Add("@BanchLocation", (row.FindControl("lblGreenHouse") as Label).Text);

                nv.Add("@Jid", (row.FindControl("lblJid") as Label).Text);

                nv.Add("@Comments", txtComments.Text);
                nv.Add("@Method", ddlMethod.SelectedValue);

                result = objCommon.GetDataExecuteScaler("SP_AddChemicalRequest", nv);


                // }

           
            }

            dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtTrays.Text, txtSQFT.Text);
            objTask.AddChemicalRequestDetails(dtTrays, result.ToString(), ddlFertilizer.SelectedItem.Text, ChemicalCode, lblbench.Text, txtResetSprayTaskForDays.Text, ddlMethod.SelectedItem.Text, txtComments.Text);

            long Mresult12 = 0;
            NameValueCollection nv123 = new NameValueCollection();
            nv123.Add("@BanchLocation", lblbench.Text);
            Mresult12 = objCommon.GetDataInsertORUpdate("SP_AddChemicalRequestMenualUpdate", nv123);


            Evo.BAL_Classes.General objGeneral = new General();
            objGeneral.SendMessage(int.Parse(ddlsupervisor.SelectedValue), "New Chemical Task Assigned", "New Chemical Task Assigned", "Chemical");
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
            dtTrays.Clear();
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

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {

            RadioBench.Items[0].Selected = false;
            ListBoxBenchesInHouse.Items.Clear();
            //To unselect all Items
            RadioBench.ClearSelection();
            BindGridFerDetails("'" + Bench + "'");
            PanelBench.Visible = false;
            PanelBenchesInHouse.Visible = false;
        }









    }
}
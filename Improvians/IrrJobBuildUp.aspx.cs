using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Bal;
using System.Text.RegularExpressions;
using Evo.BAL_Classes;

namespace Evo
{
    public partial class IrrJobBuildUp : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Bench"] != null)
                {
                    Bench = Request.QueryString["Bench"].ToString();
                }
                if (Request.QueryString["jobCode"] != null)
                {
                    JobCode = Request.QueryString["jobCode"].ToString();
                }
                if (Request.QueryString["ICode"] != null)
                {
                    ICode = Request.QueryString["ICode"].ToString();
                }

                BenchUp = "'" + Bench + "'";

                if (Request.QueryString["TaskRequestKey"] != null)
                {
                    TaskRequestKey = Request.QueryString["TaskRequestKey"].ToString();
                }
                txtSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                BindGridIrrigation();
                BindSupervisorList();
                BindGridIrrDetails("'" + Bench + "'");
                BindGridIrrDetailsViewReq();
                lblbench.Text = Bench;
            }
        }

        private string BenchUp
        {
            get
            {
                if (ViewState["BenchUp"] != null)
                {
                    return (string)ViewState["BenchUp"];
                }
                return "";
            }
            set
            {
                ViewState["BenchUp"] = value;
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
        private string ICode
        {
            get
            {
                if (ViewState["ICode"] != null)
                {
                    return (string)ViewState["ICode"];
                }
                return "";
            }
            set
            {
                ViewState["ICode"] = value;
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

                BenchUp = chkSelected;
                DataTable dt123 = new DataTable();
                gvJobHistory.DataSource = dt123;
                gvJobHistory.DataBind();
                BindGridIrrDetails(chkSelected);

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
                BenchUp = chkSelected;
                DataTable dt123 = new DataTable();
                gvJobHistory.DataSource = dt123;
                gvJobHistory.DataBind();
                BindGridIrrDetails(chkSelected);
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
            BenchUp = chkSelected;

            BindGridIrrDetails(chkSelected);
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
                int P = 0;
                string Q = "";
                string[] words = Regex.Split(Bench, @"\W+");

                DataTable dt = objFer.GetSelectBenchLocation(words[0], words[1]);

                if (dt.Rows.Count > 0)
                {
                    DataColumn col = dt.Columns["PositionCode"];
                    foreach (DataRow row in dt.Rows)
                    {
                        //strJsonData = row[col].ToString();

                        P = 1;
                        Q += "'" + row[col].ToString() + "',";
                    }
                }

                if (P > 0)
                {
                    chkSelected = Q.Remove(Q.Length - 1, 1);

                }
                else
                {

                }
            }
            BindGridIrrDetails(chkSelected);
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
            string[] words = Regex.Split(Bench, @"\W+");
            DataTable dt = objFer.GetSelectBenchLocation(words[0], words[1]);

            ListBoxBenchesInHouse.DataSource = dt;
            ListBoxBenchesInHouse.DataTextField = "PositionCode";
            ListBoxBenchesInHouse.DataValueField = "PositionCode";
            ListBoxBenchesInHouse.DataBind();

        }

        public void BindGridIrrigation()
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
            //    dt = objCommon.GetDataTable("SP_GetIrrigationRequestAssistantGrower", nv);
            //}
            //else
            //{
            //    dt = objCommon.GetDataTable("SP_GetIrrigationRequest", nv);
            //}
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", JobCode);
            nv.Add("@BenchLocation", Bench);

            dt = objCommon.GetDataTable("SP_GetIrrigationRequesStart", nv);
            GridIrrigation.DataSource = dt;
            GridIrrigation.DataBind();
            int tray = 0;

            Jid = dt.Rows[0]["GrowerPutAwayId"].ToString();
            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                tray = tray + Convert.ToInt32((row.FindControl("lbltotTray") as Label).Text);
            }
        }

        public void BindSupervisorList()
        {
            //NameValueCollection nv = new NameValueCollection();
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            //ddlSupervisor.DataTextField = "EmployeeName";
            //ddlSupervisor.DataValueField = "ID";
            //ddlSupervisor.DataBind();
            //ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            NameValueCollection nv = new NameValueCollection();
            DataTable dt = new DataTable();
            nv.Add("@RoleID", Session["Role"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetRoleForAssignementFacility", nv);

            ddlSupervisor.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlSupervisor.DataTextField = "EmployeeName";
            ddlSupervisor.DataValueField = "ID";
            ddlSupervisor.DataBind();
            ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindGridIrrDetailsViewReq()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@ICode", ICode);
            dt = objCommon.GetDataTable("SP_GetIrrigationTaskAssignmentView", nv);

            if (dt != null && dt.Rows.Count > 0)
            {
                txtNotes.Text = dt.Rows[0]["Nots"].ToString();
                txtResetSprayTaskForDays.Text = dt.Rows[0]["ResetSprayTaskForDays"].ToString();
                txtSprayDate.Text = Convert.ToDateTime(dt.Rows[0]["SprayDate"]).ToString("yyyy-MM-dd");
                txtWaterRequired.Text = dt.Rows[0]["WaterRequired"].ToString();
            }
        }

        public void BindGridIrrDetails(string BenchLoc)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", BenchLoc);
            dt = objCommon.GetDataTable("SP_GetIrrigationRequestSelect", nv);
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
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int IrrigationCode = 0;
            string SprayTaskForDaysDate = "";
            foreach (GridViewRow row1 in GridIrrigation.Rows)
            {
                string IrrigationCode1 = (row1.FindControl("lblIrrigationCode") as Label).Text;
                if (IrrigationCode1 != "")
                {
                    IrrigationCode = Convert.ToInt32(IrrigationCode1);
                    if (IrrigationCode == 0)
                    {
                        DataTable dt = new DataTable();
                        NameValueCollection nv1 = new NameValueCollection();
                        nv1.Add("@Mode", "13");
                        dt = objCommon.GetDataTable("GET_Common", nv1);
                        IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);
                    }
                    else
                    {
                        IrrigationCode = IrrigationCode;
                    }
                }
                else
                {
                    DataTable dt = new DataTable();
                    NameValueCollection nv1 = new NameValueCollection();
                    nv1.Add("@Mode", "13");
                    dt = objCommon.GetDataTable("GET_Common", nv1);
                    IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);
                }
            }

            if (txtResetSprayTaskForDays.Text != "")
            {
                SprayTaskForDaysDate = (Convert.ToDateTime(System.DateTime.Now.ToShortDateString()).AddDays(Convert.ToInt32(txtResetSprayTaskForDays.Text))).ToString();
            }
            else
            {
                SprayTaskForDaysDate = System.DateTime.Now.ToShortDateString();
            }

            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                long result = 0;
                long Mresult = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);

                //  nv.Add("@GrowerPutAwayId", lblGrowerID.Text);
                nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                nv.Add("@IrrigatedNoTrays", (row.FindControl("lbltotTray") as Label).Text);
                // nv.Add("@IrrigatedNoTrays", txtIrrigatedNoTrays.Text.Trim());
                nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                nv.Add("@IrrigationDuration", "");
                nv.Add("@SprayDate", txtSprayDate.Text.Trim());
                nv.Add("@SprayTime", "");
                nv.Add("@Nots", txtNotes.Text.Trim());
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                nv.Add("@IrrigationCode", IrrigationCode.ToString());
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@NoOfPasses", "");
                nv.Add("@ResetSprayTaskForDays", txtResetSprayTaskForDays.Text);
                nv.Add("@Role", ddlSupervisor.SelectedValue);
                nv.Add("@ISAG", "0");
                nv.Add("@jid", Jid);
                nv.Add("@TaskRequestKey", TaskRequestKey);
                nv.Add("@ResetTaskForDays", SprayTaskForDaysDate);



                result = objCommon.GetDataInsertORUpdate("SP_AddIrrigationRequestNew", nv);

                NameValueCollection nvn = new NameValueCollection();

                nvn.Add("@LoginID", Session["LoginID"].ToString());
                nvn.Add("@jobcode", (row.FindControl("lbljobID") as Label).Text);
                nvn.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                nvn.Add("@TaskName", "Irrigation");

                var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nvn);

                if (result > 0)
                {
                }
                else
                {
                }
            }

            foreach (GridViewRow row in gvJobHistory.Rows)
            {
                //if ((row.FindControl("lblGrowerputawayID") as Label).Text == "0")
                //{
                //    long result = 0;
                //    NameValueCollection nv = new NameValueCollection();
                //    nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);

                //    nv.Add("@Jobcode", (row.FindControl("lbljobID") as Label).Text);
                //    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                //    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                //    nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                //    nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                //    nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                //    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                //    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);

                //    nv.Add("@IrrigationCode", IrrigationCode.ToString());
                //    // nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                //    nv.Add("@IrrigatedNoTrays", (row.FindControl("lbltotTray") as Label).Text);
                //    nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                //    nv.Add("@IrrigationDuration", "");
                //    nv.Add("@SprayDate", txtSprayDate.Text.Trim());
                //    //nv.Add("@SprayTime", txtSprayTime.Text.Trim());
                //    nv.Add("@Nots", txtNotes.Text.Trim());
                //    nv.Add("@LoginID", Session["LoginID"].ToString());
                //    nv.Add("@Role", ddlSupervisor.SelectedValue);
                //    nv.Add("@ISAG", "0");
                //    nv.Add("@jid", Jid);

                //    result = objCommon.GetDataExecuteScaler("SP_AddIrrigationRequestManualNew", nv);
                //}
                //else
                //{

                long result = 0;
                NameValueCollection nv = new NameValueCollection();

                nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);
                nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                nv.Add("@IrrigatedNoTrays", (row.FindControl("lbltotTray") as Label).Text);
                // nv.Add("@IrrigatedNoTrays", txtIrrigatedNoTrays.Text.Trim());
                nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                nv.Add("@IrrigationDuration", "");
                nv.Add("@SprayDate", txtSprayDate.Text.Trim());
                nv.Add("@SprayTime", "");
                nv.Add("@Nots", txtNotes.Text.Trim());
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                nv.Add("@IrrigationCode", IrrigationCode.ToString());
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@NoOfPasses", "");
                nv.Add("@ResetSprayTaskForDays", txtResetSprayTaskForDays.Text);

                nv.Add("@Role", ddlSupervisor.SelectedValue);
                nv.Add("@ISAG", "0");
                nv.Add("@jid", (row.FindControl("lblJid") as Label).Text);
                nv.Add("@TaskRequestKey", TaskRequestKey);
                nv.Add("@ResetTaskForDays", SprayTaskForDaysDate);

                result = objCommon.GetDataInsertORUpdate("SP_AddIrrigationRequestNew", nv);
                // }

            }

            objTask.UpdateIsActiveIrrigation(BenchUp);
            string url = "";
            if (Session["Role"].ToString() == "1")
            {
                url = "MyTaskGrower.aspx";
            }
            else
            {
                url = "MyTaskAssistantGrower.aspx";
            }
            BAL_Classes.General objGeneral = new BAL_Classes.General();
            objGeneral.SendMessage(int.Parse(ddlSupervisor.SelectedValue), "New Irrigation Task Assigned", "New Irrigation Task Assigned", "Irrigation");

            string message = "Assignment Successful";
            // string url = "MyTaskGrower.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            // lblmsg.Text = "Assignment Successful";
            clear();

            var res = (Master.FindControl("r1") as Repeater);

            var lblCount = (Master.FindControl("lblNotificationCount") as Label);
            objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), res, lblCount);
        }

        public void clear()
        {
            ddlSupervisor.SelectedIndex = 0;
            txtWaterRequired.Text = "";
            txtNotes.Text = "";
            //  txtIrrigatedNoTrays.Text = "";
            //txtIrrigationDuration.Text = "";
            txtSprayDate.Text = "";
            // txtSprayTime.Text = "";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            RadioBench.Items[0].Selected = false;
            ListBoxBenchesInHouse.Items.Clear();
            //To unselect all Items
            RadioBench.ClearSelection();
            BindGridIrrDetails("'" + Bench + "'");

            PanelBench.Visible = false;
            PanelBenchesInHouse.Visible = false;
        }
    }
}
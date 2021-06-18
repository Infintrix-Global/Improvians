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

namespace Evo
{
    public partial class IrrigationReqManual : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                BindSupervisorList();
                Bindcname();
                BindBenchLocation(Session["Facility"].ToString());
            }
        }

        private string wo
        {
            get
            {
                if (ViewState["wo"] != null)
                {
                    return (string)ViewState["wo"];
                }
                return "";
            }
            set
            {
                ViewState["wo"] = value;
            }
        }

        private string Bench1
        {
            get
            {
                if (ViewState["Bench1"] != null)
                {
                    return (string)ViewState["Bench1"];
                }
                return "";
            }
            set
            {
                ViewState["Bench1"] = value;
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

                string YourString = Bench1;

                YourString = YourString.Remove(YourString.Length - 1);

                DataTable dt12 = objFer.GetSelectBench(YourString);

                lblBench1.Text = dt12.Rows[0]["PositionCode"].ToString();


                if (dt12.Rows.Count > 0)
                {
                    DataColumn col = dt12.Columns["PositionCode"];
                    foreach (DataRow row in dt12.Rows)
                    {
                        //strJsonData = row[col].ToString();

                        P1 = 1;
                        Q1 += "'" + row[col].ToString() + "',";
                    }
                }

                if (P1 > 0)
                {
                    chkSelected = Q1.Remove(Q1.Length - 1, 1);

                }
                else
                {

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
                string[] words = Regex.Split(Bench1, @"\W+");

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
            else
            {

            }

            DataTable dt85 = new DataTable();
            GridIrrigation.DataSource = dt85;
            GridIrrigation.DataBind();

            BindGridIrrigation(chkSelected);

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

            BindGridIrrigation(chkSelected);
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
                string[] words = Regex.Split(Bench1, @"\W+");

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

            BindGridIrrigation(chkSelected);


        }

        public void SelectBench()
        {
            string YourString = Bench1;

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

            string[] words = Regex.Split(Bench1, @"\W+");

            DataTable dt = objFer.GetSelectBenchLocation(words[0], words[1]);

            ListBoxBenchesInHouse.DataSource = dt;
            ListBoxBenchesInHouse.DataTextField = "PositionCode";
            ListBoxBenchesInHouse.DataValueField = "PositionCode";
            ListBoxBenchesInHouse.DataBind();

        }



        public void BindGridIrrigation(string BenchLoc)
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@wo","");
            //  nv.Add("@JobCode",ddlJobNo.SelectedValue);
            //  nv.Add("@CustomerName",ddlCustomer.SelectedValue);
            //   nv.Add("@Facility",ddlFacility.SelectedValue);
            //  nv.Add("@Mode", "1");
            // dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", BenchLoc);

            dt = objCommon.GetDataTable("SP_GetIrrigationRequest", nv);

            DataTable dtManual = objFer.GetManualFertilizerRequestSelect(Session["Facility"].ToString(), BenchLoc, ddlJobNo.SelectedValue);
            if (dtManual != null && dtManual.Rows.Count > 0)
            {
                dt.Merge(dtManual);
                dt.AcceptChanges();
            }
            GridIrrigation.DataSource = dt;
            GridIrrigation.DataBind();
        }

        public void Bindcname()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "8");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "cname";
            ddlCustomer.DataValueField = "cname";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));

        }


        public void BindJobCode(string ddlBench)
        {
            ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", ""));
        }

        public void BindBenchLocation(string ddlMain)
        {
            ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataTextField = "BenchName";
            ddlBenchLocation.DataValueField = "BenchName";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", ""));
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
            if (Session["Role"].ToString() == "1")
            {
                ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlSupervisor.DataTextField = "EmployeeName";
                ddlSupervisor.DataValueField = "ID";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            if (Session["Role"].ToString() == "12")
            {
                ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlSupervisor.DataTextField = "EmployeeName";
                ddlSupervisor.DataValueField = "ID";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation("");
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation("");
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindJobCode(ddlBenchLocation.SelectedValue);
            if (ddlBenchLocation.SelectedValue == "")
            {
                Panel_Bench.Visible = false;
            }
            else
            {
                Panel_Bench.Visible = true;

                Bench1 = ddlBenchLocation.SelectedItem.Text;

                BindGridIrrigation("'" + Bench1 + "'");
            }
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            ddlBenchLocation.SelectedIndex = 0;
            ddlCustomer.SelectedIndex = 0;
            ddlJobNo.SelectedIndex = 0;
            //   BindGridIrrigation();
        }
        protected void GridIrrigation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                // string rowIndex = e.CommandArgument.ToString();

                // wo = rowIndex;
                //DataTable dt = new DataTable();
                //NameValueCollection nv = new NameValueCollection();
                //nv.Add("@wo", wo);
                //nv.Add("@JobCode", ddlJobNo.SelectedValue);
                //nv.Add("@CustomerName", ddlCustomer.SelectedValue);
                //nv.Add("@Facility", ddlFacility.SelectedValue);
                //nv.Add("@Mode", "2");
                //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
                // txtIrrigatedNoTrays.Text = dt.Rows[0]["trays_actual"].ToString();
                //txtIrrigationDuration.Text = dt.Rows[0]["jobcode"].ToString();
                //  lblJobID.Text = dt.Rows[0]["jobcode"].ToString();

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                //  lblJobID.Text = GridIrrigation.DataKeys[rowIndex].Values[1].ToString();
                //  lblGrowerID.Text = GridIrrigation.DataKeys[rowIndex].Values[2].ToString();


                txtNotes.Focus();

            }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int IrrigationCode = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Mode", "13");
            dt = objCommon.GetDataTable("GET_Common", nv1);
            IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);
            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{

                long result = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);

                nv.Add("@Jobcode", (row.FindControl("lbljobID") as Label).Text);
                nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);

                nv.Add("@IrrigationCode", IrrigationCode.ToString());
                // nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                nv.Add("@IrrigatedNoTrays", (row.FindControl("lbltotTray") as Label).Text);
                nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                nv.Add("@IrrigationDuration", "");
                nv.Add("@SprayDate", txtSprayDate.Text.Trim());
                //nv.Add("@SprayTime", txtSprayTime.Text.Trim());
                nv.Add("@Nots", txtNotes.Text.Trim());
                nv.Add("@LoginID", Session["LoginID"].ToString());
                result = objCommon.GetDataExecuteScaler("SP_AddIrrigationRequestManual", nv);
               
            }
            string message = "Assignment Successful";
            string url = "MyTaskGrower.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            // lblmsg.Text = "Assignment Successful";
            clear();
        }

        public void clear()
        {

            ddlSupervisor.SelectedIndex = 0;
            txtWaterRequired.Text = "";
            txtNotes.Text = "";
            //txtIrrigatedNoTrays.Text = "";
            //txtIrrigationDuration.Text = "";
            txtSprayDate.Text = "";
            //txtSprayTime.Text = "";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }

        protected void GridIrrigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridIrrigation.PageIndex = e.NewPageIndex;
            // BindGridIrrigation();
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            userinput.Visible = true;
            ddlSupervisor.Focus();
            int tray = 0;
            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                tray = tray + Convert.ToInt32((row.FindControl("lbltotTray") as Label).Text);
                // }

            }
            //txtIrrigatedNoTrays.Text = tray.ToString();


        }

        protected void chckchanged(object sender, EventArgs e)
        {
            CheckBox chckheader = (CheckBox)GridIrrigation.HeaderRow.FindControl("CheckBoxall");
            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckheader.Checked == true)
                {
                    chckrw.Checked = true;
                }
                else
                {
                    chckrw.Checked = false;
                }
            }

        }

        protected void btnResetSearch_Click1(object sender, EventArgs e)
        {
            RadioBench.Items[0].Selected = false;
            ListBoxBenchesInHouse.Items.Clear();
            PanelBench.Visible = false;
            PanelBenchesInHouse.Visible = false;
            //To unselect all Items
            RadioBench.ClearSelection();
            BindGridIrrigation("'" + ddlBenchLocation.SelectedValue + "'");
        }
    }
}
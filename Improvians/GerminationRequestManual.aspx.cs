using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class GerminationRequestManual : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();

        BAL_Fertilizer objFer = new BAL_Fertilizer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindBenchLocation(Session["Facility"].ToString());
                txtDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                BindSupervisorList();
            }
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
            ddlBenchLocation.DataTextField = "p2";
            ddlBenchLocation.DataValueField = "p2";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", ""));
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
            gvGerm.DataSource = dt85;
            gvGerm.DataBind();

            BindGridGerm(chkSelected);

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

            BindGridGerm(chkSelected);
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            RadioBench.Items[0].Selected = false;
            ListBoxBenchesInHouse.Items.Clear();
            PanelBench.Visible = false;
            PanelBenchesInHouse.Visible = false;
            //To unselect all Items
            RadioBench.ClearSelection();
            BindGridGerm("'" + ddlBenchLocation.SelectedValue + "'");
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

            BindGridGerm(chkSelected);


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




        public void BindGridGerm(string BenchLoc)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            //nv.Add("@Week", radweek.SelectedValue);
            //nv.Add("@Status", radStatus.SelectedValue);
            dt = objCommon.GetDataTable("SP_GetGerminationManualRequest", nv);
            DataTable dtManual = objFer.GetManualFertilizerRequest(Session["Facility"].ToString(), ddlBenchLocation.SelectedValue, ddlJobNo.SelectedValue);
            if (dtManual != null && dtManual.Rows.Count > 0)
            {
                dt.Merge(dtManual);
                dt.AcceptChanges();
            }
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }
        public void BindSupervisorList()
        {
            NameValueCollection nv = new NameValueCollection();
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            if (Session["Role"].ToString() == "1")
            {
                ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForGrower", nv);

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

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvGerm.Rows[rowIndex];
                //string facName = (row.FindControl("lblFacility") as Label).Text;

                DataTable dt = new DataTable();
                //   NameValueCollection nv = new NameValueCollection();
                //  nv.Add("@FacilityName", facName);
                //  dt = objCommon.GetDataTable("SP_GetSupervisorNameByFacilityID", nv);
                lblJobID.Text = (row.FindControl("lbljobID") as Label).Text;
                lblGrowerID.Text = (row.FindControl("lblGrowerID") as Label).Text;
                lblfacsupervisor.InnerText = "Assignment"; //+ facName;
                                                           // lblSupervisorID.Text = dt.Rows[0]["ID"].ToString();
                                                           //lblSupervisorName.Text = dt.Rows[0]["EmployeeName"].ToString();
                txtDate.Focus();
            }


        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);
            //nv.Add("@InspectionDueDate", txtDate.Text);
            //nv.Add("@#TraysInspected", txtTrays.Text);
            //nv.Add("@GrowerID", lblGrowerID.Text);
            //nv.Add("@LoginID", Session["LoginID"].ToString());
            //result = objCommon.GetDataInsertORUpdate("SP_AddGerminationRequestManual", nv);

            foreach (GridViewRow row in gvGerm.Rows)
            {
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@Customer", "");
                nv.Add("@jobcode", (row.FindControl("lbljobID") as Label).Text);
                nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                nv.Add("@Seeddate", (row.FindControl("lblSeededDate") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);
                nv.Add("@InspectionDueDate", txtDate.Text);
                nv.Add("@TraysInspected", txtTrays.Text);

                nv.Add("@LoginId", Session["LoginID"].ToString());

                result = objCommon.GetDataInsertORUpdate("SP_AddGerminationRequesMenualDetails", nv);
            }
            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
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
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }
        }

        public void clear()
        {
            txtDate.Text = "";
            txtTrays.Text = "";
            //lblSupervisorID.Text = "";
            // lblSupervisorName.Text = "";
            lblfacsupervisor.InnerText = "";
            ddlSupervisor.SelectedIndex = 0;

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm("");
        }



        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm("");
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindJobCode(ddlBenchLocation.SelectedValue);

            //if (ddlBenchLocation.SelectedValue == "")
            //{
            //    Panel_Bench.Visible = false;
            //}
            //else
            //{
            //    Panel_Bench.Visible = true;
            //    Bench1 = ddlBenchLocation.SelectedItem.Text;
            //    BindGridGerm("'" + Bench1 + "'");
            //}

            BindGridGerm(Bench1);

        }


        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            Bindcname();
                       
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            userinput.Visible = true;
            // ddlsupervisor.Focus();
            int tray = 0;
            foreach (GridViewRow row in gvGerm.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                tray = tray + Convert.ToInt32((row.FindControl("lblTotTray") as Label).Text);
                //}
                //  Bench = (row.FindControl("lblGreenHouse") as Label).Text;
            }
            txtTrays.Text = tray.ToString();
            //  BindSQFTofBench(ddlBenchLocation.SelectedItem.Text);

        }

        protected void btnRRE_Click(object sender, EventArgs e)
        {
            RadioBench.Items[0].Selected = false;
            ListBoxBenchesInHouse.Items.Clear();
            PanelBench.Visible = false;
            PanelBenchesInHouse.Visible = false;
            //To unselect all Items
            RadioBench.ClearSelection();
            BindGridGerm("'" + ddlBenchLocation.SelectedValue + "'");
        }

      
    }
}
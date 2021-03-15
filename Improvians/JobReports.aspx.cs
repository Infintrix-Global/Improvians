
using Evo.Bal;
using Evo.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class JobReports : System.Web.UI.Page
    {
        public static string JobCode;
        CommonControlNavision objNav = new CommonControlNavision();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        clsCommonMasters objMaster = new clsCommonMasters();
        BAL_Task objTask = new BAL_Task();
        Bal_SeedingPlan objSP = new Bal_SeedingPlan();

        static string ReceiverEmail = "";


        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };

        public static DataTable dtCTrays = new DataTable()
        { Columns = { "Fertilizer", "Tray", "SQFT" } };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                JobCode = Request.QueryString["jobCode"];
                if (string.IsNullOrEmpty(JobCode))
                {
                    divFilter.Visible = true;
                    divFilter1.Visible = true;
                    BindBenchLocation(Session["Facility"].ToString());
                }
                else
                {
                    BindGridOne();
                }

                BindSupervisor();

                BindFacility();
                BindSupervisorList();
                BindFertilizer();
                BindJobCode("");
                BindChemical();
            }
        }



        public void BindGridOne()
        {
            string chkSelected = "";
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobID", JobCode);
            DataSet ds = objCommon.GetDataSet("GetJobTracibilityReport", nv);
            dt = ds.Tables[0];
            dt2 = ds.Tables[1];
            dt3 = ds.Tables[2];
            dt4 = ds.Tables[3];
            GV6.DataSource = ds.Tables[5];
            dt5 = ds.Tables[4];
            gv1.DataSource = dt;
            GV2.DataSource = dt2;
            DataTable dtTrays = objBAL.GetSeedLotWithDate(JobCode);
            if (dt3.Rows.Count==0 && dtTrays != null)
            {
                dt3.Merge(dtTrays);
                dt3.AcceptChanges();
            }
            Gv3.DataSource = dt3;
            GV4.DataSource = dt4;
            GV5.DataSource = dt5;
            gv1.DataBind();
            GV2.DataBind();
            Gv3.DataBind();
            GV4.DataBind();
            GV5.DataBind();           
            GV6.DataBind();
            int P = 0;
            string Q = "";
            if (dt5.Rows.Count > 0)
            {
                DataColumn col = dt5.Columns["GreenHouseID"];
                foreach (DataRow row in dt5.Rows)
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
            BindSQFTofBench(chkSelected);


            decimal tray = 0;
            string BatchLocd = string.Empty;
            foreach (GridViewRow row in GV5.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{
                tray = tray + Convert.ToDecimal((row.FindControl("lblTrays") as Label).Text);
                //}
                // BatchLocd = (row.FindControl("lblGreenHouse1") as Label).Text;
            }
            txtTGerTrays.Text = "10";
            txtFTrays.Text = tray.ToString();
            txtChemicalTrays.Text = tray.ToString();
        }
        public void BindBenchLocation(string ddlMain)
        {
            ddlBenchLocation.DataSource = objBAL.GetLocation(ddlMain);
            ddlBenchLocation.DataTextField = "p2";
            ddlBenchLocation.DataValueField = "p2";
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", ""));

        }
        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindJobCode(ddlBenchLocation.SelectedValue);
        }
        public void BindJobCode(string ddlBench)
        {
            ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", ""));
        }
        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            JobCode = ddlJobNo.SelectedValue.Trim();
            BindGridOne();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            JobCode = txtSearchJobNo.Text.Trim();
            BindGridOne();
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            txtSearchJobNo.Text = "JB";
            BindGridOne();
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Evo"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //and t.[Location Code]= '" + Session["Facility"].ToString() + "'
                    //cmd.CommandText = "select distinct t.[Job No_] as jobcode  from[GTI$IA Job Tracking Entry] t, [GTI$Job] j where j.No_ = t.[Job No_] and j.[Job Status] = 2  " +
                    //" AND t.[Job No_] like '" + prefixText + "%'";
                    string Facility = HttpContext.Current.Session["Facility"].ToString();
                    cmd.CommandText = " select distinct jobcode from gti_jobs_seeds_plan where loc_seedline ='" + Facility + "'  AND jobcode like '" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "'  AND jobcode like '" + prefixText + "%' order by jobcode" +
                        "";

                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(sdr["jobcode"].ToString());
                        }
                    }
                    conn.Close();
                    return customers;
                }
            }
        }
        protected void GV5_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV5.EditIndex = e.NewEditIndex;
            Label lblTray = (Label)(GV5.Rows[GV5.EditIndex].FindControl("lblTrays"));
            Label lblLocation = (Label)(GV5.Rows[GV5.EditIndex].FindControl("lblGHD"));
            Session["trays"] = lblTray.Text;
            Session["location"] = lblLocation.Text;
            BindGridOne();
            //DropDownList ddlPbx = (DropDownList)(GV5.Rows[GV5.EditIndex].FindControl("ddlBenchLocation"));
            //if (ddlPbx != null)
            //    ddlPbx.DataSource = objBAL.GetLocation(Session["Facility"].ToString());
            //ddlPbx.DataTextField = "p2";
            //ddlPbx.DataValueField = "p2";
            //ddlPbx.DataBind();
            //ddlPbx.Items.Insert(0, new ListItem("--- Select ---", ""));

        }

        protected void GV5_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            Label lblid = GV5.Rows[e.RowIndex].FindControl("lblgrowerId") as Label;

            HiddenField field = GV5.Rows[e.RowIndex].FindControl("HiddenField1") as HiddenField;
            TextBox city = GV5.Rows[e.RowIndex].FindControl("txt_Name") as TextBox;
            DropDownList ddlBenchLocation = GV5.Rows[e.RowIndex].FindControl("ddlBenchLocation") as DropDownList;
            long result1 = 0;
            General objGeneral = new General();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@GrowerPutAwayID", lblid.Text);
            nv1.Add("@GreenHouseID", ddlBenchLocation.SelectedValue);
            nv1.Add("@Trays", city.Text);
            nv1.Add("@JobId", JobCode);
            nv1.Add("@FromLocation", Session["location"].ToString());
            nv1.Add("@ToLocation", ddlBenchLocation.SelectedValue);
            nv1.Add("@OldTotalTrays", Session["trays"].ToString());
            nv1.Add("@NewTotalTrays", city.Text);
            nv1.Add("@UserId", Session["LoginID"].ToString());
            result1 = objCommon.GetDataInsertORUpdate("UpdateJobFacilityHouseDetail", nv1);


            GV5.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            BindGridOne();
        }

        protected void GV5_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV5.EditIndex = -1;
            BindGridOne();
        }

        protected void GV5_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DataSet ds = new DataSet();
                    DropDownList ddList = (DropDownList)e.Row.FindControl("ddlBenchLocation");

                    ddList.DataSource = objBAL.GetLocation(Session["Facility"].ToString()); ;
                    ddList.DataTextField = "p2";
                    ddList.DataValueField = "p2";
                    ddList.DataBind();

                    //DataRowView dr = e.Row.DataItem as DataRowView;
                    // ddList.SelectedValue = dr["department_id"].ToString();
                }
            }
        }

        //---------------------------------------------------------------------------------------------TASK Create ------------------

        public void BindSQFTofBench(string Bench)
        {

            //  DataTable dtSQFT = objFer.GetSQFTofBench(lblbench.Text);
            DataTable dtSQFT = objFer.GetSQFTofBenchNew(Bench);
            if (dtSQFT != null && dtSQFT.Rows.Count > 0)
            {
                txtSQFT.Text = Convert.ToDecimal(dtSQFT.Rows[0]["Sqft"]).ToString("#,0000.00");
                txtChemicalSQFTofBench.Text = Convert.ToDecimal(dtSQFT.Rows[0]["Sqft"]).ToString("#,0000.00");
            }
            else
            {
                txtSQFT.Text = "0.00";
                txtChemicalSQFTofBench.Text = "0.00";
            }


        }
        public void BindSupervisor()
        {


            NameValueCollection nv = new NameValueCollection();

            DataTable dt = new DataTable();
            if (Session["Role"].ToString() == "1")
            {
                dt = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
            }
            else if (Session["Role"].ToString() == "12")
            {
                dt = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
            }
            else
            { }


            ddlgerminationSupervisor.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlgerminationSupervisor.DataTextField = "EmployeeName";
            ddlgerminationSupervisor.DataValueField = "ID";
            ddlgerminationSupervisor.DataBind();
            ddlgerminationSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlFertilizationSupervisor.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlFertilizationSupervisor.DataTextField = "EmployeeName";
            ddlFertilizationSupervisor.DataValueField = "ID";
            ddlFertilizationSupervisor.DataBind();
            ddlFertilizationSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlirrigationSupervisor.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlirrigationSupervisor.DataTextField = "EmployeeName";
            ddlirrigationSupervisor.DataValueField = "ID";
            ddlirrigationSupervisor.DataBind();
            ddlirrigationSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlplant_readySupervisor.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlplant_readySupervisor.DataTextField = "EmployeeName";
            ddlplant_readySupervisor.DataValueField = "ID";
            ddlplant_readySupervisor.DataBind();
            ddlplant_readySupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlChemical_supervisor.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlChemical_supervisor.DataTextField = "EmployeeName";
            ddlChemical_supervisor.DataValueField = "ID";
            ddlChemical_supervisor.DataBind();
            ddlChemical_supervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlLogisticManager.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlLogisticManager.DataTextField = "EmployeeName";
            ddlLogisticManager.DataValueField = "ID";
            ddlLogisticManager.DataBind();
            ddlLogisticManager.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        public void BindChemical()
        {
            NameValueCollection nv = new NameValueCollection();
            ddlChemical.DataSource = objFer.GetChemicalList();
            ddlChemical.DataTextField = "Name";
            ddlChemical.DataValueField = "No_";
            ddlChemical.DataBind();
            ddlChemical.Items.Insert(0, new ListItem("--- Select ---", "0"));
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



        protected void btnFReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnFSubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row1 in GV5.Rows)
            {
                dtTrays.Clear();
                string Batchlocation = "";
                int FertilizationCode = 0;
                DataTable dt = new DataTable();
                NameValueCollection nv14 = new NameValueCollection();
                NameValueCollection nvimg = new NameValueCollection();
                nv14.Add("@Mode", "12");
                dt = objCommon.GetDataTable("GET_Common", nv14);
                FertilizationCode = Convert.ToInt32(dt.Rows[0]["FCode"]);


                foreach (GridViewRow row in GV2.Rows)
                {

                    long result2 = 0;
                    NameValueCollection nv4 = new NameValueCollection();
                    nv4.Add("@SupervisorID", ddlFertilizationSupervisor.SelectedValue);
                    nv4.Add("@Type", "Fertilizer");
                    nv4.Add("@Jobcode", JobCode);
                    nv4.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv4.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv4.Add("@Facility", Session["Facility"].ToString());
                    nv4.Add("@GreenHouseID", (row1.FindControl("lblGHD") as Label).Text);
                    nv4.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                    nv4.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv4.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    //nv.Add("@WorkOrder", lblwo.Text);
                    nv4.Add("@LoginID", Session["LoginID"].ToString());
                    nv4.Add("@FertilizationCode", FertilizationCode.ToString());
                    nv4.Add("@FertilizationDate", txtFDate.Text);
                    result2 = objCommon.GetDataExecuteScaler("SP_AddFertilizerRequestManual", nv4);
                   // Batchlocation = (row.FindControl("lblGreenHouse") as Label).Text;

                }

                dtTrays.Rows.Add(ddlFertilizer.SelectedItem.Text, txtQty.Text, "", txtFTrays.Text, txtSQFT.Text);

                objTask.AddFertilizerRequestDetailsCreatTask(dtTrays, "0", FertilizationCode, (row1.FindControl("lblGHD") as Label).Text, "", "", "", txtResetSprayTaskForDays.Text, txtFComments.Text.Trim());
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

        }

        protected void btngerminationSumit_Click(object sender, EventArgs e)
        {



            long result16 = 0;
            foreach (GridViewRow row1 in GV5.Rows)
            {
                foreach (GridViewRow row in GV2.Rows)
                {
                    //NameValueCollection nv = new NameValueCollection();
                    //nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    //nv.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                    //nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    //nv.Add("@Facility", "");
                    //nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                    //nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                    //nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    //nv.Add("@Seeddate", (row.FindControl("lblSeededDate") as Label).Text);
                    //nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    //nv.Add("@SupervisorID", ddlgerminationSupervisor.SelectedValue);
                    //nv.Add("@InspectionDueDate", txtGerDate.Text);
                    //nv.Add("@TraysInspected", txtTGerTrays.Text);
                    //nv.Add("@Chid", "");
                    //nv.Add("@LoginId", Session["LoginID"].ToString());

                    //result16 = objCommon.GetDataInsertORUpdate("SP_AddCropHealthGerminationReques", nv);

                NameValueCollection nv = new NameValueCollection();
                nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                nv.Add("@jobcode",JobCode);
                nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                nv.Add("@Facility", Session["Facility"].ToString());
                nv.Add("@GreenHouseID", "");
                nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                nv.Add("@Seeddate", (row.FindControl("lblSeededDate") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                nv.Add("@SupervisorID", ddlgerminationSupervisor.SelectedValue);
                nv.Add("@InspectionDueDate", txtGerDate.Text);
                nv.Add("@TraysInspected", txtTGerTrays.Text);

                    nv.Add("@LoginId", Session["LoginID"].ToString());
                    nv.Add("@Comments", txtGcomments.Text);

                    result16 = objCommon.GetDataInsertORUpdate("SP_AddGerminationRequesMenualDetailsCreateTask", nv);


                }
            }
            if (result16 > 0)
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
                //  clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }
        }

        protected void btngerminationReset_Click(object sender, EventArgs e)
        {

        }

        //protected void ddlAssignments_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    NameValueCollection nv = new NameValueCollection();
        //    nv.Add("@Uid", ddlAssignments.SelectedValue);
        //    DataTable dt = objCommon.GetDataTable("getReceiverEmail", nv);
        //    ReceiverEmail = dt.Rows[0]["Email"].ToString();
        //}
        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                long result = 0;

                NameValueCollection nv = new NameValueCollection();
                // nv.Add("@OperatorID", Session["LoginID"].ToString());
                //nv.Add("@wo", wo);
                nv.Add("@Comments", txtgeneralCommnet.Text.Trim());
                nv.Add("@AsssigneeID", ddlAssignments.SelectedValue);
                nv.Add("@TaskType", ddlTaskType.SelectedValue);
                nv.Add("@MoveFrom", txtFrom.Text.Trim());
                nv.Add("@MoveTo", txtTo.Text.Trim());
                nv.Add("@IsActive", "1");


                result = objCommon.GetDataInsertORUpdate("InsertGeneralTask", nv);
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                string FromMail = WebConfigurationManager.AppSettings["FromEmail"];
                string FromEmailPassword = WebConfigurationManager.AppSettings["FromEmailPassword"];
                smtpClient.Credentials = new System.Net.NetworkCredential(FromMail, FromEmailPassword);
                // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.Subject = "Crop Health Report";
                mail.Body = "Crop Health Report Comments:" + "";
                //Setting From , To and CC

                mail.From = new MailAddress(FromMail);
                mail.To.Add(new MailAddress(ReceiverEmail));
                //  Attachment atc = new Attachment(folderPath, "Uploded Picture");
                //   mail.Attachments.Add(atc);
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void BindSupervisorList()
        {
            NameValueCollection nv = new NameValueCollection();

            ddlAssignments.DataSource = objCommon.GetDataTable("SP_GetSeedsRoles", nv);
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlAssignments.DataTextField = "EmployeeName";
            ddlAssignments.DataValueField = "ID";
            ddlAssignments.DataBind();
            ddlAssignments.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void ddlAssignments_SelectedIndexChanged(object sender, EventArgs e)
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Uid", ddlAssignments.SelectedValue);
            DataTable dt = objCommon.GetDataTable("getReceiverEmail", nv);
            ReceiverEmail = dt.Rows[0]["Email"].ToString();
        }

        protected void btnirrigationReset_Click1(object sender, EventArgs e)
        {

        }

        protected void btnirrigationSubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row1 in GV5.Rows)
            {

                int IrrigationCode = 0;
                DataTable dt = new DataTable();
                NameValueCollection nv17 = new NameValueCollection();
                NameValueCollection nvimg = new NameValueCollection();
                nv17.Add("@Mode", "13");
                dt = objCommon.GetDataTable("GET_Common", nv17);
                IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);

                foreach (GridViewRow row in GV2.Rows)
                {

                    long result16 = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlirrigationSupervisor.SelectedValue);

                nv.Add("@Jobcode",JobCode);
                nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                nv.Add("@Facility", Session["Facility"].ToString());
                nv.Add("@GreenHouseID", "");
                nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);

                    nv.Add("@IrrigationCode", IrrigationCode.ToString());
                    // nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    nv.Add("@IrrigatedNoTrays", (row.FindControl("lblTotTray") as Label).Text);
                    nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                    nv.Add("@IrrigationDuration", "");
                    nv.Add("@SprayDate", txtirrigationSprayDate.Text.Trim());
                    //nv.Add("@SprayTime", txtSprayTime.Text.Trim());
                    nv.Add("@Nots", txtIrrComments.Text.Trim());
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    result16 = objCommon.GetDataExecuteScaler("SP_AddIrrigationRequestManual", nv);


                }

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
        }

        protected void btnplant_readySubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row1 in GV5.Rows)
            {
                int IrrigationCode = 0;
                DataTable dt = new DataTable();
                NameValueCollection nv11 = new NameValueCollection();
                nv11.Add("@Mode", "13");
                dt = objCommon.GetDataTable("GET_Common", nv11);
                IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);

                foreach (GridViewRow row in GV2.Rows)
                {

                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlplant_readySupervisor.SelectedValue);

                    nv.Add("@Jobcode", JobCode);
                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@Facility", Session["Facility"].ToString());
                    nv.Add("@GreenHouseID", (row1.FindControl("lblGHD") as Label).Text);
                    nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@ChId", "0");
                    nv.Add("@Comments", txtPlantComments.Text.Trim());
                    result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyRequestManuaCreateTask", nv);


                }

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
        }

        protected void btnplant_readyReset_Click(object sender, EventArgs e)
        {

        }




        protected void ddlTaskType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTaskType.SelectedItem.Value == "3")
            {
                divFrom.Style["display"] = "block";
                divTo.Style["display"] = "block";
            }
            else
            {
                divFrom.Style["display"] = "none";
                divTo.Style["display"] = "none";
            }

        }



        protected void btnChemicalReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnChemicalSubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row1 in GV5.Rows)
            {
                dtCTrays.Clear();
                
                int ChemicalCode = 0;
                DataTable dt = new DataTable();
                NameValueCollection nv1 = new NameValueCollection();
                nv1.Add("@Mode", "16");
                dt = objCommon.GetDataTable("GET_Common", nv1);
                ChemicalCode = Convert.ToInt32(dt.Rows[0]["CCode"]);


            foreach (GridViewRow row in GV2.Rows)
            {
                long result = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", ddlChemical_supervisor.SelectedValue);
                nv.Add("@Type", "Chemical");
                nv.Add("@Jobcode", JobCode);
                nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                nv.Add("@Item", Session["Facility"].ToString());
                nv.Add("@Facility", (row.FindControl("lblFacility") as Label).Text);
                //    nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                nv.Add("@GreenHouseID","");
                nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                //nv.Add("@WorkOrder", lblwo.Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@ChemicalCode", ChemicalCode.ToString());
                nv.Add("@ChemicalDate", txtChemicalSprayDate.Text);
               // nv.Add("@Comments", txtcomments.Text);
                nv.Add("@Method", ddlMethod.SelectedValue);
                result = objCommon.GetDataExecuteScaler("SP_AddChemicalRequestManual", nv);
            }

                dtCTrays.Rows.Add(ddlChemical.SelectedItem.Text, txtChemicalTrays.Text, txtSQFT.Text);
                objTask.AddChemicalRequestDetails(dtCTrays, ddlChemical.SelectedValue, ChemicalCode, (row1.FindControl("lblGHD") as Label).Text, txtResetSprayTaskForDays.Text, ddlMethod.SelectedValue, txtCComments.Text);
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
        }


        public void BindFacility()
        {
            ddlToFacility.DataSource = objBAL.GetMainLocation();
            ddlToFacility.DataTextField = "l1";
            ddlToFacility.DataValueField = "l1";
            ddlToFacility.DataBind();
            ddlToFacility.Items.Insert(0, new ListItem("--- Select ---", "0"));
            ddlToFacility.SelectedValue = Session["Facility"].ToString();
            BindBench_Location();
        }

        public void BindBench_Location()
        {
            //  nv.Add("@FacilityID", ddlToFacility.SelectedValue);
            ddlToGreenHouse.DataSource = objBAL.GetLocation(ddlToFacility.SelectedValue);
            ddlToGreenHouse.DataTextField = "p2";
            ddlToGreenHouse.DataValueField = "p2";
            ddlToGreenHouse.DataBind();
            ddlToGreenHouse.Items.Insert(0, new ListItem("--- Select ---", "0"));


        }



        protected void ddlToFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBench_Location();
        }

        protected void MoveReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnMoveSubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row1 in GV5.Rows)
            {

                foreach (GridViewRow row in GV2.Rows)
                {

                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlLogisticManager.SelectedValue);
                    nv.Add("@WorkOrder", "0");
                    nv.Add("@GrowerPutAwayID", "0");

                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@FromFacility", Session["Facility"].ToString());
                    nv.Add("@ToFacility", ddlToFacility.SelectedValue);
                    nv.Add("@ToGreenHouse", (row1.FindControl("lblGHD") as Label).Text);
                    nv.Add("@Trays", (row.FindControl("lblTotTray") as Label).Text);
                    nv.Add("@MoveDate", txtMoveDate.Text);

                    nv.Add("@Jobcode", JobCode);
                    nv.Add("@Customer", (row.FindControl("lblCustomer") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);
                    nv.Add("@ChId", "0");
                    nv.Add("@Comments", txtMoveComments.Text.Trim());
                    result = objCommon.GetDataExecuteScaler("SP_AddMoveRequestManualCreateTask", nv);

                }

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
        }
    }
}
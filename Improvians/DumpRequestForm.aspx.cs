using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class DumpRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                // BindFacility();
                BindGridPlantReady(0);
                BindSupervisorList();
            }
        }

        private string JobCode
        {
            get
            {
                if (Request.QueryString["jobId"] != null)
                {
                    return Request.QueryString["jobId"].ToString();
                }
                return "";
            }
            set
            {
                // JobCode = Request.QueryString["jobId"].ToString();
                // JobCode = value;
            }
        }

        private string benchLoc
        {
            get
            {
                if (Request.QueryString["benchLoc"] != null)
                {
                    return Request.QueryString["benchLoc"].ToString();
                }
                return "";
            }
            set
            {
                // JobCode = Request.QueryString["jobId"].ToString();
                // JobCode = value;
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

        private string TaskKey
        {
            get
            {
                if (Request.QueryString["Tkey"] != null)
                {
                    return Request.QueryString["Tkey"].ToString();
                }
                return "";
            }
            set
            {

            }
        }

        public void BindGridPlantReady(int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@wo", "");
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());

            dt = objCommon.GetDataTable("SP_GetDumpRequestAssistantGrower", nv);

            gvPlantReady.DataSource = dt;
            gvPlantReady.DataBind();

            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }
        }

        private void highlight(int limit)
        {
            var i = gvPlantReady.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvPlantReady.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblBenchLoc") as Label).Text;
                var tKey = gvPlantReady.DataKeys[row.RowIndex].Values[5].ToString();
                i--;
                if (checkJob == JobCode && checklocation == benchLoc && tKey == TaskKey)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvPlantReady.PageIndex++;
                    gvPlantReady.DataBind();
                    highlight((limit - 10));
                }
            }
        }
        public void BindSupervisorList()
        {
            NameValueCollection nv = new NameValueCollection();

            DataTable dt = new DataTable();

            if (Session["Role"].ToString() == "12" || Session["Role"].ToString() == "1")
            {

                dt = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
            }
            else if (Session["Role"].ToString() == "2")
            {
                dt = objCommon.GetDataTable("SP_GetRoleForSupervisor", nv);
            }
            else
            {
                // dt = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
            }

            ddlDumptAssignment.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlDumptAssignment.DataTextField = "EmployeeName";
            ddlDumptAssignment.DataValueField = "ID";
            ddlDumptAssignment.DataBind();
            ddlDumptAssignment.Items.Insert(0, new ListItem("--Select--", "0"));

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

        public void BindJobCode()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "7");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }


        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }

        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindJobCode(ddlBenchLocation.SelectedValue);
            //Bindcname(ddlBenchLocation.SelectedValue, "0", "0");
            //BindJobCode(ddlBenchLocation.SelectedValue, "0", "0");
            //BindAssignByList(ddlBenchLocation.SelectedValue, "0", "0");

            BindGridPlantReady(1);
        }

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }

        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridPlantReady(1);
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();
            BindGridPlantReady(1);
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

        protected void gvPlantReady_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                userinput.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                HiddenFieldDid.Value = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();
                HiddenFieldJid.Value = gvPlantReady.DataKeys[rowIndex].Values[2].ToString();
                ViewState["jobcode"] = gvPlantReady.DataKeys[rowIndex].Values[3].ToString();
                ViewState["benchloc"] = gvPlantReady.DataKeys[rowIndex].Values[4].ToString();
                TaskRequestKey = gvPlantReady.DataKeys[rowIndex].Values[5].ToString();

                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@DumpId", HiddenFieldDid.Value);

                dt = objCommon.GetDataTable("SP_GetDumpRequestView", nv);

                if (dt != null & dt.Rows.Count > 0)
                {
                    txtCommentsDump.Text = dt.Rows[0]["Comments"].ToString();
                    txtQuantityofTray.Text = dt.Rows[0]["QuantityOfTray"].ToString();
                    txtDumpDate.Text = Convert.ToDateTime(dt.Rows[0]["DumpDateR"]).ToString("yyyy-MM-dd");
                }
                //ddlSupervisor.Focus();
            }

            if (e.CommandName == "StartDump")
            {
                string ChId = "";
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string Did = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();
                //  ChId = gvPlantReady.DataKeys[rowIndex].Values[1].ToString();
                TaskRequestKey = gvPlantReady.DataKeys[rowIndex].Values[5].ToString();

                if (ChId == "")
                {
                    ChId = "0";
                }
                else
                {
                    ChId = ChId;
                }
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@OperatorID", Session["LoginID"].ToString());
                nv.Add("@Comments", "");
                nv.Add("@DumpId", Did);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@QuantityOfTray", "");
                nv.Add("@DumpDate", "");

                long result = objCommon.GetDataExecuteScaler("SP_AddDumpTaskStart", nv);


                if (result > 0)
                {
                    Response.Redirect(String.Format("~/DumpTaskCompletion.aspx?Did={0}&Chid={1}&DrId={2}&TaskRequestKey={3}", result, ChId, Did, TaskRequestKey));
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();

            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Aid", ddlDumptAssignment.SelectedValue);
            dt = objCommon.GetDataTable("spGeEmployeeRoleDetails", nv1);

            nv.Add("@SupervisorID", ddlDumptAssignment.SelectedValue);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Did", HiddenFieldDid.Value);
            nv.Add("@Comments", txtCommentsDump.Text);
            nv.Add("@wo", "0");
            nv.Add("@ManualID", HiddenFieldJid.Value);
            nv.Add("@DumpDate", txtDumpDate.Text);
            nv.Add("@QuantityOfTray", txtQuantityofTray.Text);
            nv.Add("@RoleId", dt.Rows[0]["RoleID"].ToString());

            nv.Add("@jobcode", ViewState["jobcode"].ToString());
            nv.Add("@GreenHouseID", ViewState["benchloc"].ToString());
            nv.Add("@TaskRequestKey", TaskRequestKey);
            result = objCommon.GetDataInsertORUpdate("SP_AddDumpRequestManua", nv);

            NameValueCollection nameValue = new NameValueCollection();
            nameValue.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@jobcode", ViewState["jobcode"].ToString());
            nv.Add("@GreenHouseID", ViewState["benchloc"].ToString());
            nameValue.Add("@TaskName", "Dump");

            var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);

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
                //string url = "MyTaskAssistantGrower.aspx";
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

            var res = (Master.FindControl("r1") as Repeater);

            var lblCount = (Master.FindControl("lblNotificationCount") as Label);
            objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), res, lblCount);
        }

        public void clear()
        {
            //  ddlSupervisor.SelectedIndex = 0;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskAssistantGrower.aspx");
        }

        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlantReady.PageIndex = e.NewPageIndex;
            BindGridPlantReady(1);
        }

        protected void gvPlantReady_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGermDate = (Label)e.Row.FindControl("lblDumpdate");
                string dtimeString = Convert.ToDateTime(lblGermDate.Text).ToString("yyyy/MM/dd");

                DateTime dtime = Convert.ToDateTime(dtimeString);

                DateTime nowtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));

                if (nowtime > dtime)
                {
                    e.Row.CssClass = "overdue";
                }
            }
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
                    cmd.CommandText = " select distinct jobcode from gti_jobs_seeds_plan where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "'  AND jobcode like '" + prefixText + "%' order by jobcode" +
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
    }
}
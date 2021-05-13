using Evo.Bal;
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
    public partial class MoveRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
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

        public void BindGridPlantReady(int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@wo", "");
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());

            dt = objCommon.GetDataTable("SP_GetMoveRequestAssistantGrower", nv);

            gvMoveReq.DataSource = dt;
            gvMoveReq.DataBind();

            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }
        }
        private void highlight(int limit)
        {
            var i = gvMoveReq.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvMoveReq.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                i--;
                if (checkJob == JobCode && checklocation == benchLoc)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvMoveReq.PageIndex++;
                    gvMoveReq.DataBind();
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

            }
            ddlLogisticManager.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlLogisticManager.DataTextField = "EmployeeName";
            ddlLogisticManager.DataValueField = "ID";
            ddlLogisticManager.DataBind();
            ddlLogisticManager.Items.Insert(0, new ListItem("--Select--", "0"));

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
            BindFacility();
            BindGridPlantReady(1);
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

        protected void gvMoveReq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                BindFacility();
                userinput.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                HiddenFieldDid.Value = gvMoveReq.DataKeys[rowIndex].Values[1].ToString();
                HiddenFieldJid.Value = gvMoveReq.DataKeys[rowIndex].Values[2].ToString();
                lblJobID.Text = gvMoveReq.DataKeys[rowIndex].Values[3].ToString();
                lblBenchlocation.Text = gvMoveReq.DataKeys[rowIndex].Values[4].ToString();
                lblTotalTrays.Text = gvMoveReq.DataKeys[rowIndex].Values[5].ToString();
                lblDescription.Text = gvMoveReq.DataKeys[rowIndex].Values[6].ToString();
                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@MoveId", HiddenFieldDid.Value);

                dt = objCommon.GetDataTable("SP_GetMOVERequestView", nv);

                if (dt != null & dt.Rows.Count > 0)
                {
                    BindFacility();
                    txtMoveComments.Text = dt.Rows[0]["Comments"].ToString();
                    txtMoveNumberOfTrays.Text = dt.Rows[0]["TraysRequest"].ToString();
                    txtMoveDate.Text = Convert.ToDateTime(dt.Rows[0]["MoveDate"]).ToString("yyyy-MM-dd");
                    ddlToFacility.SelectedValue = dt.Rows[0]["FacilityTo"].ToString();
                    ddlToGreenHouse.SelectedValue = dt.Rows[0]["GrenHouseToRequest"].ToString();
                }
                ddlLogisticManager.Focus();
            }

            if (e.CommandName == "StartDump")
            {
                string ChId = "";
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string Did = gvMoveReq.DataKeys[rowIndex].Values[1].ToString();
                Response.Redirect(String.Format("~/MoveCompletionStart.aspx?Did={0}", Did));
                //    Response.Redirect(String.Format("~/MoveTaskCompletion.aspx?PageType={0}&Did={1}&DrId={2}", "ManageTask", 0, dtR.Rows[0]["MoveID"].ToString()));
            }
        }

        protected void btnMoveSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();

            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Aid", ddlLogisticManager.SelectedValue);
            dt = objCommon.GetDataTable("spGeEmployeeRoleDetails", nv1);
            nv.Add("@SupervisorID", ddlLogisticManager.SelectedValue);
            nv.Add("@MoveNumberOfTrays", txtMoveNumberOfTrays.Text);

            nv.Add("@FromFacility", Session["LoginID"].ToString());
            nv.Add("@GrowerPutAwayID", "0");
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@ToFacility", ddlToFacility.SelectedValue);
            nv.Add("@ToGreenHouse", ddlToGreenHouse.SelectedValue);

            nv.Add("@MoveDate", txtMoveDate.Text);
            nv.Add("@Comments", txtMoveComments.Text);
            nv.Add("@mvoeId", HiddenFieldDid.Value);
            nv.Add("@RoleId", dt.Rows[0]["RoleID"].ToString());
            nv.Add("@ManualID", HiddenFieldJid.Value);

            result = objCommon.GetDataInsertORUpdate("SP_AddMoveRequestASManua", nv);

            NameValueCollection nameValue = new NameValueCollection();
            nameValue.Add("@LoginID", Session["LoginID"].ToString());
            nameValue.Add("@jobcode", lblJobID.Text);
            nameValue.Add("@GreenHouseID", lblBenchlocation.Text);
            nameValue.Add("@TaskName", "Move");

            var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

            NameValueCollection nvn = new NameValueCollection();
            nvn.Add("@LoginID", Session["LoginID"].ToString());
            nvn.Add("@SupervisorID", ddlLogisticManager.SelectedValue);
            nvn.Add("@Jobcode", lblJobID.Text);
            nvn.Add("@TaskName", "Move");
            nvn.Add("@TaskRequestKey", "");
            nvn.Add("@GreenHouseID", lblBenchlocation.Text);
            var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);

            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                BAL_Classes.General objGeneral = new BAL_Classes.General();
                objGeneral.SendMessage(int.Parse(ddlLogisticManager.SelectedValue), "New Move Task Assigned", "New Move Task Assigned", "Move");

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
                //   string url = "MyTaskAssistantGrower.aspx";
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
            //  ddlSupervisor.SelectedIndex = 0;
        }

        protected void MoveReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskAssistantGrower.aspx");
        }

        protected void gvMoveReq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMoveReq.PageIndex = e.NewPageIndex;
            BindGridPlantReady(1);
        }

        protected void gvMoveReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGermDate = (Label)e.Row.FindControl("lblMoveDate");
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
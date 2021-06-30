using Evo.Admin.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class GeneralTaskCompletion : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        General objGeneral = new General();
        private readonly EmailHelper _emailHelper;
        DataTable dtCompletion = new DataTable();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() == "13")
            {
                this.Page.MasterPageFile = "~/Customer/CustomerMaster.master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Did"] != null)
                {
                    Did = Request.QueryString["Did"].ToString();
                    GeneralAdd.Visible = false;
                }

                if (Request.QueryString["DrId"] != null)
                {
                    DrId = Request.QueryString["DrId"].ToString();
                }

                if (Request.QueryString["IsF"] != null && Request.QueryString["IsF"].ToString() == "1")
                {

                    PanelComplitionDetsil.Visible = true;
                    GeneralAdd.Visible = false;
                }
                else
                {
                    PanelComplitionDetsil.Visible = false;
                    GeneralAdd.Visible = true;
                }

             


                if (Request.QueryString["Chid"] != "0" && Request.QueryString["Chid"] != null)
                {
                    BindGridCropHealth(Convert.ToInt32(Request.QueryString["Chid"]));
                }

                if (Request.QueryString["TaskRequestKey"] != "0" && Request.QueryString["TaskRequestKey"] != null)
                {
                    TaskRequestKey = Request.QueryString["TaskRequestKey"].ToString();
                }
                BintTaskType();
                BindTask();
                BindViewDumpDetilas(0);
                BindGridGeneraComplition(Did);
               
            }
        }


        public void BintTaskType()
        {

            NameValueCollection nv = new NameValueCollection();

            DataTable dt = new DataTable();

            nv.Add("@mode", "22");

            dt = objCommon.GetDataTable("GET_Common", nv);

            ddlTaskType.DataSource = dt;
            ddlTaskType.DataTextField = "TaskType";
            ddlTaskType.DataValueField = "ID";
            ddlTaskType.DataBind();
            ddlTaskType.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindViewDumpDetilas(int RDid)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

           
            nv.Add("@TaskRequestKey", TaskRequestKey);
         
            dt = objCommon.GetDataTable("SP_GetTaskAssignmentGeneralViewStart1", nv);
            GridViewDumpView.DataSource = dt;
            GridViewDumpView.DataBind();

        }
        public void BindGridGeneraComplition(string Did)
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@GeneralTaskAssignmentId", Did.ToString());
            dt1 = objCommon.GetDataTable("SP_GetTaskAssignmenGeneralTaskCompletionView", nv1);
            if (dt1 != null && dt1.Rows.Count > 0)
            {

                PanelComplitionDetsil.Visible = true;
                GeneralAdd.Visible = false;

                GridPlantComplition.DataSource = dt1;
                GridPlantComplition.DataBind();
               
                //      lblComplitionUser.Text = dt1.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                PanelComplitionDetsil.Visible = false;
            }

        }
        public void BindGridCropHealth(int Chid)
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Chid", Chid.ToString());
            dt1 = objCommon.GetDataTable("SP_GetCropHealthReportSelect", nv1);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                PanelCropHealth.Visible = true;
                gvCropHealth.DataSource = dt1;
                gvCropHealth.DataBind();
                lblCommment.Text = dt1.Rows[0]["CropHealthCommit"].ToString();
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

        private string Did
        {
            get
            {
                if (ViewState["Did"] != null)
                {
                    return (string)ViewState["Did"];
                }
                return "";
            }
            set
            {
                ViewState["Did"] = value;
            }
        }

        private string DrId
        {
            get
            {
                if (ViewState["DrId"] != null)
                {
                    return (string)ViewState["DrId"];
                }
                return "";
            }
            set
            {
                ViewState["DrId"] = value;
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


        public void BindTask()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@GeneralTaskAssignmentId", DrId);
            dtCompletion = objCommon.GetDataTable("SP_GetOperatorGeneralTaskDetails", nv);
            dt = dtCompletion;
            gvTask.DataSource = dt;
            gvTask.DataBind();

            lbljid.Text = dt.Rows[0]["jid"].ToString();
            DateTime dNow = new DateTime();
            dNow = Convert.ToDateTime(dt.AsEnumerable().Select(r => r.Field<DateTime>("GeneralTaskDate")).FirstOrDefault().ToString("yyyy/MM/dd"));
            txtGeneralDate.Text = (dNow.ToString("yyyy-MM-dd"));

            txtComment.Text = dt.AsEnumerable().Select(r => r.Field<string>("Comments")).FirstOrDefault();

            ddlTaskType.SelectedValue = dt.Rows[0]["id1"].ToString();
            txtFrom.Text = dt.Rows[0]["MoveFrom"].ToString();
            txtTo.Text = dt.Rows[0]["MoveTo"].ToString();

            if (dt.Rows[0]["id1"].ToString() == "3")
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

        protected void ddlTaskType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Session["SelectedAssignment"] = ddlAssignments.SelectedValue;
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

        protected void gvTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTask.PageIndex = e.NewPageIndex;
            BindTask();
        }

        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table>";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            //  nv.Add("@OperatorID",Session["LoginID"].ToString());
            //   nv.Add("@Notes", txtNots.Text);
            //    nv.Add("@JobID", "");
            nv.Add("@Did", Did);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Comments", txtComment.Text);
            nv.Add("@QuantityOfTray", "");
            nv.Add("@GeneralTaskDate", txtGeneralDate.Text);
            nv.Add("@TaskType", ddlTaskType.SelectedItem.Text);
            nv.Add("@MoveFrom", txtFrom.Text);
            nv.Add("@MoveTo", txtTo.Text);

            result = objCommon.GetDataExecuteScaler("SP_AddGeneralTaskCompletion", nv);

            GridViewRow row = gvTask.Rows[0];
            var txtJobNo = (row.FindControl("lblID") as Label).Text;
            var txtBenchLocation = (row.FindControl("lblGreenHouseID") as Label).Text;

            NameValueCollection nameValue = new NameValueCollection();
            nameValue.Add("@LoginID", Session["LoginID"].ToString());
            nameValue.Add("@jobcode", txtJobNo);
            nameValue.Add("@GreenHouseID", txtBenchLocation);
            nameValue.Add("@TaskName", "General Task");
            var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

            var res = (Master.FindControl("r1") as Repeater);
            var lblCount = (Master.FindControl("lblNotificationCount") as Label);
            objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), res, lblCount);

            //SendMailAfterCompletion();
            if (result > 0)
            {
                // lblmsg.Text = "Completion Successful";

                string message = "Completion Successful";
                string url;
                // if (Session["Role"].ToString() == "3")
                //{


                url = "GeneralTaskAssignmentForm.aspx";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                clear();
            }
            else
            {
                lblmsg.Text = "Completion Not Successful";
            }
        }

        private void SendMailAfterCompletion()
        {
            string table1 = ConvertDataTableToHTML(dtCompletion);

            var pathToFile = Path.DirectorySeparatorChar.ToString()
                    + "Admin/EmailTemplates"
                    + Path.DirectorySeparatorChar.ToString()
                    + "GeneralTaskCompletionSendEmail.html";
            string filePath;
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                filePath = SourceReader.ReadToEnd();
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("#FirstName#", "FirstName");
            dic.Add("#JobNo#", "FirstName");
            dic.Add("#Item#", "FirstName");
            dic.Add("#BLoc#", "FirstName");
            dic.Add("#TTrays#", "FirstName");
            dic.Add("#TSize#", "FirstName");
            dic.Add("#TaskType#", "FirstName");
            dic.Add("#MoveF#", "FirstName");
            dic.Add("#MoveT#", "FirstName");
            dic.Add("#Cms#", "FirstName");

            //            dic.Add("#GDate#", collection.StartDate.ToString("dd/MM/yyyy"));
            dic.Add("#AssignBy#", "");


            string sbody = _emailHelper.getEmailBody(pathToFile, dic);
            //          objGeneral.SendMail(ToMail, CCMail, "General Task is completed by ", sbody);

        }

        public void clear()
        {
            //txtNots.Text = "";
            txtComment.Text = "";
            //  txtPlantHeight.Text = "";
            txtGeneralDate.Text = "";
            //txtQuantityOfTray.Text = "";


        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            if (Session["Role"].ToString() == "3")
            {
                //  Response.Redirect("~/PlantReadyCompletionForm.aspx");
            }

            if (Session["Role"].ToString() == "2")
            {
                // Response.Redirect("~/PlantReadyAssignmentForm.aspx");
            }
        }
    }
}
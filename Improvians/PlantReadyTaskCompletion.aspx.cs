using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class PlantReadyTaskCompletion : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        static string ReceiverEmail = "";
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
                if (Request.QueryString["PRAID"] != "0")
                {
                    PRAID = Request.QueryString["PRAID"].ToString();
                    PanelComplitionDetsil.Visible = true;
                }
                else
                {
                    PanelComplitionDetsil.Visible = false;
                }

                if (Request.QueryString["TaskRequestKey"] != "0" && Request.QueryString["TaskRequestKey"] != null)
                {
                    TaskRequestKey = Request.QueryString["TaskRequestKey"].ToString();
                }

                if (Request.QueryString["IsF"] != null && Request.QueryString["IsF"].ToString() == "1")
                {

                    PanelComplitionDetsil.Visible = true;
                    PantReadyAdd.Visible = false;
                    PanelView.Visible = false;
                }
                else
                {
                    PanelComplitionDetsil.Visible = false;
                    PantReadyAdd.Visible = true;
                    if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "2" || Session["Role"].ToString() == "12")
                    {
                        PanelView.Visible = true;

                    }
                    else
                    {
                        PanelView.Visible = false;
                    }
                }



                if (Request.QueryString["Chid"] != "0" && Request.QueryString["Chid"] != null)
                {
                    BindGridCropHealth(Convert.ToInt32(Request.QueryString["Chid"]));
                    BindGridCropHealthImage(Request.QueryString["Chid"].ToString());
                }
                BindPlantReady(Convert.ToInt32(Request.QueryString["PRID"]));
                PRID = Request.QueryString["PRID"].ToString();
                BindViewDetilas(Convert.ToInt32(Request.QueryString["PRID"]));
                BindSupervisorList();
              
                BindGridPalntReadyComplition(PRAID);
            }
        }



        private string PRID
        {
            get
            {
                if (ViewState["PRID"] != null)
                {
                    return (string)ViewState["PRID"];
                }
                return "";
            }
            set
            {
                ViewState["PRID"] = value;
            }
        }


        public void BindGridCropHealthImage(string ChId)
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Chid", ChId);
            dt1 = objCommon.GetDataTable("SP_GetCropHealthReportImages", nv1);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                PanelCropHealth.Visible = true;
                CropePhotos.DataSource = dt1;
                CropePhotos.DataBind();
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
        public void BindGridPalntReadyComplition(string PRAID)
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@PlantReadyTaskAssignmentId", PRAID.ToString());
            dt1 = objCommon.GetDataTable("SP_GetTaskAssignmenPlantReadyTaskCompletionView", nv1);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                PanelComplitionDetsil.Visible = true;
                PantReadyAdd.Visible = false;
                PanelView.Visible = false;
                GridPlantComplition.DataSource = dt1;
                GridPlantComplition.DataBind();
            }
            else
            {
                GridPlantComplition.DataSource = null;
                GridPlantComplition.DataBind();
                PanelComplitionDetsil.Visible = false;
            }
        }

        public void BindSupervisorList()
        {
            NameValueCollection nv = new NameValueCollection();

            ddlAssignments.DataSource = objCommon.GetDataTable("SP_GetSeedsRoles", nv);

            ddlAssignments.DataTextField = "EmployeeName";
            ddlAssignments.DataValueField = "ID";
            ddlAssignments.DataBind();
            ddlAssignments.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        public void BindViewDetilas(int PRid)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

          
            if (Request.QueryString["PRAID"] != "0")
            {
                nv.Add("@Login", "0");
            }
            else
            {
                nv.Add("@Login", Session["LoginID"].ToString());
            }

            if (TaskRequestKey == "")
            {
                nv.Add("@TaskRequestKey", "0");
            }
            else
            {
                nv.Add("@TaskRequestKey", TaskRequestKey);
            }

            dt = objCommon.GetDataTable("SP_GetTaskAssignmenPlantReadytViewStart1", nv);
            if (dt != null && dt.Rows.Count > 0)
            {
                GridViewDumpView.DataSource = dt;
                GridViewDumpView.DataBind();
            }
            else
            {
                VPantReady.Visible = false;
                GridViewDumpView.DataSource = null;
                GridViewDumpView.DataBind();
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

        private string PRAID
        {
            get
            {
                if (ViewState["PRAID"] != null)
                {
                    return (string)ViewState["PRAID"];
                }
                return "0";
            }
            set
            {
                ViewState["PRAID"] = value;
            }
        }

        public void BindPlantReady(int PRRID)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@wo", wo);
            //nv.Add("@JobCode", "");
            //nv.Add("@CustomerName", "");
            //nv.Add("@Facility", "");
            //nv.Add("@Mode", "11");
            nv.Add("@PRAID", PRRID.ToString());
            nv.Add("@RoleId", Session["Role"].ToString());
            dt = objCommon.GetDataTable("SP_GetOperatorPlantReadyTaskByPRAIDNew", nv);
            gvPlantReady.DataSource = dt;
            gvPlantReady.DataBind();
            lbljid.Text = dt.Rows[0]["jid"].ToString();
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    lblSeedlot.Text = dt.Rows[0]["SeedLots"].ToString();
            //}
        }

        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlantReady.PageIndex = e.NewPageIndex;
            BindPlantReady(0);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            //  nv.Add("@OperatorID",Session["LoginID"].ToString());
            //   nv.Add("@Notes", txtNots.Text);
            //    nv.Add("@JobID", "");
           
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@CropId", "");
            nv.Add("@UpdatedReadyDate", txtUpdatedReadyDate.Text);
            nv.Add("@PlantExpirationDate", txtPlantExpirationDate.Text);
            nv.Add("@RootQuality", ddlRootQuality.SelectedItem.Text);
            nv.Add("@PlantHeight", ddlPlantHeight.SelectedItem.Text);
            nv.Add("@Notes",txtComments.Text);
            
            if (Request.QueryString["PRAID"].ToString() == "0")
            {
                nv.Add("@PRRID", PRID);
                nv.Add("@Jid", lbljid.Text);
                result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyCompletionNew", nv);
            }
            else
            {
                nv.Add("@PRAID", PRAID);
        
                result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyCompletion", nv);
            }
            GridViewRow row = gvPlantReady.Rows[0];
            var txtJobNo = (row.FindControl("lblID") as Label).Text;
            var txtBenchLocation = (row.FindControl("lblGreenHouse") as Label).Text;

            NameValueCollection nameValue = new NameValueCollection();
            nameValue.Add("@LoginID", Session["LoginID"].ToString());
            nameValue.Add("@jobcode", txtJobNo);
            nameValue.Add("@GreenHouseID", txtBenchLocation);
            nameValue.Add("@TaskName", "Plant Ready");

            var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

            var res = (Master.FindControl("r1") as Repeater);
            var lblCount = (Master.FindControl("lblNotificationCount") as Label);
            objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), res, lblCount);

            if (result > 0)
            {
                // lblmsg.Text = "Completion Successful";
                clear();
                string message = "Completion Successful";
                string url;
                if (Session["Role"].ToString() == "12")
                {
                    url = "PlantReadyRequestForm.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
                else if (Session["Role"].ToString() == "2")
                {
                    url = "PlantReadyAssignmentForm.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
                else if (Session["Role"].ToString() == "1")
                {
                    url = "PlantReadyAssignmentForm.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
                else
                {
                    url = "PlantReadyCompletionForm.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
            }
            else
            {
                lblmsg.Text = "Completion Not Successful";
            }
        }

        public void clear()
        {
            //txtNots.Text = "";
            txtPlantExpirationDate.Text = "";
            //  txtPlantHeight.Text = "";
            txtUpdatedReadyDate.Text = "";
            ddlPlantHeight.SelectedValue = "0";
            ddlRootQuality.SelectedValue = "0";
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            if (Session["Role"].ToString() == "3")
            {
                Response.Redirect("~/PlantReadyCompletionForm.aspx");
            }

            if (Session["Role"].ToString() == "2")
            {
                Response.Redirect("~/PlantReadyAssignmentForm.aspx");
            }
        }
        protected void ddlTaskType_SelectedIndexChanged(object sender, EventArgs e)
        {
            general_task_request.Attributes.Add("class", "request__block-collapse collapse show");
            //  Session["SelectedAssignment"] = ddlAssignments.SelectedValue;
            txtgeneralComment.Focus();
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
        public void GeneraltaskSubmit(string Assigned)
        {
            long result16 = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Aid", Session["SelectedAssignment"].ToString());
            dt = objCommon.GetDataTable("spGeEmployeeRoleDetails", nv1);
            foreach (GridViewRow row in gvPlantReady.Rows)
            {

                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckrw.Checked == true)
                {
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@Customer", "");
                    nv.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                    nv.Add("@Item", (row.FindControl("lblitem") as Label).Text);
                    nv.Add("@Facility", Session["Facility"].ToString());
                    nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                    nv.Add("@TotalTray", (row.FindControl("lblTotTray") as Label).Text);
                    nv.Add("@TraySize", (row.FindControl("lblTraySize") as Label).Text);
                    nv.Add("@Seeddate", "");
                    nv.Add("@Itemdesc", (row.FindControl("lblitemdesc") as Label).Text);

                    nv.Add("@SupervisorID", Assigned);
                    //nv.Add("@SupervisorID", ddlAssignments.SelectedValue);
                    nv.Add("@TaskType", ddlTaskType.SelectedValue);
                    nv.Add("@MoveFrom", txtFrom.Text);
                    nv.Add("@MoveTo", txtTo.Text);
                    nv.Add("@date", txtgeneralDate.Text);
                    nv.Add("@RoleId", dt.Rows[0]["RoleID"].ToString());

                    nv.Add("@LoginId", Session["LoginID"].ToString());
                    nv.Add("@Comments", txtgeneralComment.Text);
                    nv.Add("@Jid", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    nv.Add("@GrowerputawayID", "0");
                    result16 = objCommon.GetDataInsertORUpdate("SP_AddGeneralRequesMenualDetailsCreateTask", nv);
                }
            }

            if (result16 > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                string CCEmail = "";

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

                NameValueCollection nv = new NameValueCollection();

                var getToMail = Session["SelectedAssignment"].ToString();
                // var getToMail = ddlAssignments.SelectedValue;
                nv.Add("@Uid", getToMail);
                DataTable dt1 = objCommon.GetDataTable("getReceiverEmail", nv);
                ReceiverEmail = dt1.Rows[0]["Email"].ToString();

                mail.To.Add(new MailAddress(ReceiverEmail));

                nv.Clear();
                var getCCMail = Session["Role"].ToString();
                nv.Add("@Uid", getCCMail);

                dt1 = objCommon.GetDataTable("getReceiverEmail", nv);
                CCEmail = dt1.Rows[0]["Email"].ToString();
                mail.CC.Add(new MailAddress(CCEmail));

                smtpClient.Send(mail);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
            }
        }
        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                long result = 0;

                NameValueCollection nv = new NameValueCollection();
                // nv.Add("@OperatorID", Session["LoginID"].ToString());
                //nv.Add("@wo", wo);
                nv.Add("@Comments", txtgeneralComment.Text.Trim());
                nv.Add("@AsssigneeID", Session["SelectedAssignment"].ToString());
                //nv.Add("@AsssigneeID", ddlAssignments.SelectedValue);

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
        protected void btnGeneraltask_Click(object sender, EventArgs e)
        {
            GeneraltaskSubmit(ddlAssignments.SelectedValue);
        }
        protected void btnGeneralReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnGeneral_Task1_Click(object sender, EventArgs e)
        {
            general_task_request.Attributes.Add("class", "request__block-collapse collapse show");
            btnGeneral_Task1.Attributes.Add("class", "request__block-head collapsed");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Evo
{
    public partial class GeneralTaskAssignment : System.Web.UI.Page
    {

        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Did"] != null)
                {
                    DId = Request.QueryString["Did"].ToString();
                }
                if (Request.QueryString["Chid"] != "0" && Request.QueryString["Chid"] != null)
                {
                    BindGridCropHealth(Convert.ToInt32(Request.QueryString["Chid"]));
                }

                BindTask();
                BindOperatorList();
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

        private string DId
        {
            get
            {
                if (ViewState["DId"] != null)
                {
                    return (string)ViewState["DId"];
                }
                return "";
            }
            set
            {
                ViewState["DId"] = value;
            }
        }


        public void BindOperatorList()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@RoleID", "3");
            ddlOperator.DataSource = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;
            ddlOperator.DataTextField = "EmployeeName";
            ddlOperator.DataValueField = "ID";
            ddlOperator.DataBind();
            ddlOperator.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindTask()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@DId", DId);
            dt = objCommon.GetDataTable("SP_GetSupervisorGeneralAssignTask", nv);
            gvTask.DataSource = dt;
            gvTask.DataBind();
            if(dt != null && gvTask.Rows.Count != 0)
            {
                GridViewRow row = gvTask.Rows[0];

                txtNotes.Text = (row.FindControl("lblComments") as Label).Text;
                ddlTaskType.SelectedValue = dt.Rows[0]["id1"].ToString();
                txtFrom.Text = dt.Rows[0]["MoveFrom"].ToString();
                txtTo.Text = dt.Rows[0]["MoveTo"].ToString();

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;

            NameValueCollection nv = new NameValueCollection();
            nv.Add("@OperatorID", ddlOperator.SelectedValue);
            nv.Add("@Comments", txtNotes.Text);

            nv.Add("@TaskType", ddlTaskType.SelectedValue);
            nv.Add("@MoveFrom", ddlTaskType.SelectedValue == "3" ? txtFrom.Text : "");
            nv.Add("@MoveTo", ddlTaskType.SelectedValue == "3" ? txtTo.Text : "");

            nv.Add("@GeneralId", DId);
            nv.Add("@LoginID", Session["LoginID"].ToString());

            nv.Add("@QuantityOfTray", "");

            //gvTask.DataKeys[0].Values[1].ToString()
            
            GridViewRow row = gvTask.Rows[0];

           var txtGeneralDate = Convert.ToDateTime((row.FindControl("lblGeneralDates") as Label).Text).ToString("yyyy-MM-dd");

              var txtJobNo = (row.FindControl("lblID") as Label).Text;
            var txtBenchLocation = (row.FindControl("lblBenchLocation") as Label).Text;


            nv.Add("@GeneralTaskDate", txtGeneralDate);
            nv.Add("@JobNo", txtJobNo);
            nv.Add("@BenchLocation", txtBenchLocation);


            result = objCommon.GetDataExecuteScaler("SP_AddGeneralTaskAssignment", nv);
            if (result > 0)
            {
                NameValueCollection nameValue = new NameValueCollection();
                nameValue.Add("@LoginID", Session["LoginID"].ToString());
                nameValue.Add("@jobcode", txtJobNo);
                nameValue.Add("@GreenHouseID", txtBenchLocation);
                nameValue.Add("@TaskName", "General Task");

                var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

                //lblmsg.Text = "Assignment Successful";
                clear();
                string message = "Assignment Successful";
                string url = "GeneralTaskAssignmentForm.aspx";
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
                lblmsg.Text = "Assignment Not Successful";
            }
        }

        public void clear()
        {
            ddlOperator.SelectedIndex = 0;
            txtNotes.Text = "";


        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/GeneralTaskAssignmentForm.aspx");
        }

        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTask.PageIndex = e.NewPageIndex;
            BindTask();
        }

        protected void gvTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTask.PageIndex = e.NewPageIndex;
            BindTask();
        }
    }
}
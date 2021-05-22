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
    public partial class MoveTaskAssignment : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["GrowerPutAwayId"] != null)
                {
                    GrowerPutAwayId = Request.QueryString["GrowerPutAwayId"].ToString();
                }
                BindGridMove();
                BindShippingCoordinatorList();
            }
        }


        private string GrowerPutAwayId
        {
            get
            {
                if (ViewState["GrowerPutAwayId"] != null)
                {
                    return (string)ViewState["GrowerPutAwayId"];
                }
                return "";
            }
            set
            {
                ViewState["GrowerPutAwayId"] = value;
            }
        }

        public void BindShippingCoordinatorList()
        {
            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@RoleID", "6");
            //ddlShippingCoordinator.DataSource = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;
            //ddlShippingCoordinator.DataTextField = "EmployeeName";
            //ddlShippingCoordinator.DataValueField = "ID";
            //ddlShippingCoordinator.DataBind();
            //ddlShippingCoordinator.Items.Insert(0, new ListItem("--- Select ---", "0"));

            NameValueCollection nv = new NameValueCollection();
            if (Session["Role"].ToString() == "2")
            {
                ddlShippingCoordinator.DataSource = objCommon.GetDataTable("SP_GetRoleForSupervisor", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlShippingCoordinator.DataTextField = "EmployeeName";
                ddlShippingCoordinator.DataValueField = "ID";
                ddlShippingCoordinator.DataBind();
                ddlShippingCoordinator.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            if (Session["Role"].ToString() == "12")
            {
                ddlShippingCoordinator.DataSource = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlShippingCoordinator.DataTextField = "EmployeeName";
                ddlShippingCoordinator.DataValueField = "ID";
                ddlShippingCoordinator.DataBind();
                ddlShippingCoordinator.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        public void BindGridMove()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@WoId", wo);
            nv.Add("@GrowerPutAwayId", GrowerPutAwayId);
            //nv.Add("@mode","1");
            dt = objCommon.GetDataTable("SP_GetMoveSiteTeamTaskByMoveID", nv);
            gvMove.DataSource = dt;
            gvMove.DataBind();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@CoordinatorId", ddlShippingCoordinator.SelectedValue);
            nv.Add("@GrowerPutAwayId", GrowerPutAwayId);
            nv.Add("@CreateBy", Session["LoginID"].ToString());
            result = objCommon.GetDataInsertORUpdate("SP_AddAssign_Task_Shipping_Coordinator", nv);

            if (result > 0)
            {
                GridViewRow row = gvMove.Rows[0];

                NameValueCollection nameValue = new NameValueCollection();
                nameValue.Add("@LoginID", Session["LoginID"].ToString());
                nameValue.Add("@jobcode", (row.FindControl("lblID") as Label).Text);
                nameValue.Add("@GreenHouseID", (row.FindControl("GreenHouseID") as Label).Text);
                nameValue.Add("@TaskName", "PutAway");

                var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

                NameValueCollection nvn = new NameValueCollection();
                nvn.Add("@LoginID", Session["LoginID"].ToString());
                nvn.Add("@SupervisorID", ddlShippingCoordinator.SelectedValue);
                nvn.Add("@Jobcode", (row.FindControl("lblID") as Label).Text);
                nvn.Add("@TaskName", "PutAway");
                nv.Add("@TaskRequestKey", "");
                nvn.Add("@GreenHouseID", (row.FindControl("GreenHouseID") as Label).Text);
                var nresult = objCommon.GetDataExecuteScaler("SP_AddNotification", nvn);

                //  lblmsg.Text = "Assignment Successful";
                Clear();

                var res = (Master.FindControl("r1") as Repeater);
                var lblCount = (Master.FindControl("lblNotificationCount") as Label);
                objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), res, lblCount);

                string message = "Assignment Successful";
                string url = "MyTaskLogisticManager.aspx";
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
            Response.Redirect("~/MyTaskLogisticManager.aspx");
        }
        public void Clear()
        {
            ddlShippingCoordinator.SelectedIndex = 0;
            //  txtNotes.Text = "";

        }

        protected void gvMove_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMove.PageIndex = e.NewPageIndex;
            BindGridMove();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class MoveTaskAssignment : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Wid"] != null)
                {
                    wo = Request.QueryString["Wid"].ToString();
                }
                    BindGridMove();
                BindShippingCoordinatorList();
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

        public void BindShippingCoordinatorList()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@RoleID", "6");
            ddlShippingCoordinator.DataSource = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;
            ddlShippingCoordinator.DataTextField = "EmployeeName";
            ddlShippingCoordinator.DataValueField = "ID";
            ddlShippingCoordinator.DataBind();
            ddlShippingCoordinator.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindGridMove()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@WoId", wo);
            nv.Add("@mode","1");
            dt = objCommon.GetDataTable("SP_GetGrowerPutAwayLogisticManagerAssignedJobByMoveID", nv);
            gvMove.DataSource = dt;
            gvMove.DataBind();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@CoordinatorId", ddlShippingCoordinator.SelectedValue);
            nv.Add("@wo",wo);
            nv.Add("@CreateBy", Session["LoginID"].ToString());
            result = objCommon.GetDataInsertORUpdate("SP_AddAssign_Task_Shipping_Coordinator", nv);
            if (result > 0)
            {
              //  lblmsg.Text = "Assignment Successful";
                Clear();
                string message = "Assignment Successful";
                string url= "MyTaskLogisticManager.aspx";
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
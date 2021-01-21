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
                BindGridMove();
                BindShippingCoordinatorList();
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
            nv.Add("@MoveID", Session["MoveID"].ToString());
            dt = objCommon.GetDataTable("SP_GetGreenHouseLogisticManagerAssignedJobByMoveID", nv);
            gvMove.DataSource = dt;
            gvMove.DataBind();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@ShippingCoordinatorID", ddlShippingCoordinator.SelectedValue);
            //nv.Add("@Notes", txtNotes.Text);
            nv.Add("@MoveID", Session["MoveID"].ToString());
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataInsertORUpdate("SP_AddMoveAssignment", nv);
            if (result > 0)
            {
                lblmsg.Text = "Assignment Successful";
                Clear();
            }
            else
            {
                lblmsg.Text = "Assignment Not Successful";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            ddlShippingCoordinator.SelectedIndex = 0;
          //  txtNotes.Text = "";
            Response.Redirect("~/MyTaskLogisticManager.aspx");
        }

        protected void gvMove_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMove.PageIndex = e.NewPageIndex;
            BindGridMove();
        }
    }
}
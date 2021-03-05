using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class GerminationAssignmentForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                BindFacility();
                BindGridGerm();

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

        public void BindFacility()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "9");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlFacility.DataSource = dt;
            ddlFacility.DataTextField = "loc_seedline";
            ddlFacility.DataValueField = "loc_seedline";
            ddlFacility.DataBind();
            ddlFacility.Items.Insert(0, new ListItem("--Select--", "0"));

        }


        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", ddlFacility.SelectedValue);
            dt = objCommon.GetDataTable("SP_GetGreenHouseSupervisorGerminationTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string Wo = "";
            string GTRID = "";
            string ChId = "";
            long result = 0;
            if (e.CommandName == "Start")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvGerm.Rows[rowIndex];

                Wo = (row.FindControl("lblWo") as Label).Text;
                GTRID = (row.FindControl("lblID") as Label).Text;
                ChId = (row.FindControl("lblChid") as Label).Text;
                if(ChId =="")
                {
                    ChId = "0";
                }
                else
                {
                    ChId = ChId;
                }
                                                                    
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@OperatorID", Session["LoginID"].ToString());
                nv.Add("@Notes", "");
                nv.Add("@WorkOrderID", Wo);
                nv.Add("@GTRID", GTRID);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                result = objCommon.GetDataExecuteScaler("SP_AddGerminationAssignment", nv);

                // Session["WorkOrder"] = JobID;
                Response.Redirect(String.Format("~/GreenHouseTaskCompletion.aspx?GTAID={0}&Chid={1}", result.ToString(), ChId));
            }
            if (e.CommandName == "Assign")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvGerm.Rows[rowIndex];
                GTRID = (row.FindControl("lblID") as Label).Text;
                ChId = (row.FindControl("lblChid") as Label).Text;
                if (ChId == "")
                {
                    ChId = "0";
                }
                else
                {
                    ChId = ChId;
                }
                Response.Redirect(String.Format("~/GerminationTaskAssignment.aspx?GTRID={0}&Chid={1}", GTRID, ChId));
            }
        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnAssign = (Button)e.Row.FindControl("btnAssign");
                Button btnSelect = (Button)e.Row.FindControl("btnSelect");

                int RoleId = Convert.ToInt32(Session["Role"]);
                if (RoleId == 11 || RoleId ==3 || RoleId==5)
                {
                    btnSelect.Visible = true;
                    btnAssign.Visible = false;
                }

            }
        }
    }
}
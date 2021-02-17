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
    public partial class IrrigationAssignmentForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                BindFacility();
                BindGridIrrigation();
              
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

        public void BindGridIrrigation()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@wo", "");
            //nv.Add("@JobCode", ddlJobNo.SelectedValue);
            //nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            //nv.Add("@Facility", ddlFacility.SelectedValue);
            //nv.Add("@Mode", "3");
            //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            nv.Add("@LoginID",Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("SP_GetSupervisorIrrigationTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();
        }


        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation();
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation();
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation();
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();
            BindFacility();
            BindGridIrrigation();
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
       



        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string JobID = "";
            string TaskID = "";
            if (e.CommandName == "Assign")
            {
             
                string IRID = e.CommandArgument.ToString();

                Response.Redirect(String.Format("~/IrrigationTaskAssignment.aspx?IRID={0}", IRID));

            }


            if (e.CommandName == "Select")
            {

                string WOID = e.CommandArgument.ToString();

                long result = 0;
                NameValueCollection nv = new NameValueCollection();
                //nv.Add("@OperatorID", Session["LoginID"].ToString());
                //nv.Add("@wo", WOID);
                //nv.Add("@SprayDate", "");
                //nv.Add("@TraysSprayed", "");
                //nv.Add("@SprayDuration", "");

                //nv.Add("@LoginID", Session["LoginID"].ToString());

                //nv.Add("@mode", "1");

                //result = objCommon.GetDataInsertORUpdate("SP_AddIrrigationTaskAssignment", nv);
                nv.Add("@OperatorID", Session["LoginID"].ToString());
                nv.Add("@IRID", e.CommandArgument.ToString());
              //  nv.Add("@SprayDate", "");
               // nv.Add("@TraysSprayed", "");
               // nv.Add("@SprayDuration", "");
                nv.Add("@LoginID", Session["LoginID"].ToString());
              //  nv.Add("@mode", "2");

                result = objCommon.GetDataExecuteScaler("SP_AddIrrigationTaskAssignment", nv);
                if (result > 0)
                {
                    Response.Redirect(String.Format("~/IrrigationTaskCompletion.aspx?IRAID={0}", result));
                }

            }
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridIrrigation();
        }

     

        protected void gvGerm_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnAssign = (Button)e.Row.FindControl("btnAssign");
                Button btnSelect = (Button)e.Row.FindControl("btnSelect");

                int RoleId = Convert.ToInt32(Session["Role"]);
                if (RoleId == 11)
                {
                    btnSelect.Visible = true;
                    btnAssign.Visible = false;
                }

            }
        }


      
    }
}
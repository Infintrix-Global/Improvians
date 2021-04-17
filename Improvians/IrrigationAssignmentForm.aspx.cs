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
    public partial class IrrigationAssignmentForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                BindGridIrrigation(0);

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

        public void BindGridIrrigation(int p)
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("SP_GetSupervisorIrrigationTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

            //foreach (GridViewRow row in gvGerm.Rows)
            //{
            //    var checkJob = (row.FindControl("lblGreenHouseID") as Label).Text;
            //    if (checkJob == benchLoc)
            //    {
            //        row.CssClass = "highlighted";
            //    }
            //}
            if (p != 1)
            {
                highlight(dt.Rows.Count);
            }
        }
        private void highlight(int limit)
        {
            var i = gvGerm.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvGerm.Rows)
            {               
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                i--;
                if (checklocation == benchLoc)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvGerm.PageIndex++;
                    gvGerm.DataBind();
                    highlight((limit - 10));
                }
            }
        }


        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation(1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridIrrigation(1);
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();
            BindGridIrrigation(1);
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

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string JobID = "";
            string TaskID = "";
            if (e.CommandName == "Assign")
            {

                string IrrigationCode = e.CommandArgument.ToString();

                Response.Redirect(String.Format("~/IrrigationTaskAssignment.aspx?IrrigationCode={0}", IrrigationCode));

            }


            if (e.CommandName == "Select")
            {

                // string WOID = e.CommandArgument.ToString();

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

                //  nv.Add("@SprayDate", "");
                // nv.Add("@TraysSprayed", "");
                // nv.Add("@SprayDuration", "");
                //  nv.Add("@mode", "2");


                string IrrigationCode = e.CommandArgument.ToString();
                nv.Add("@OperatorID", Session["LoginID"].ToString());
                nv.Add("@IrrigationCode", IrrigationCode);
                nv.Add("@LoginID", Session["LoginID"].ToString());


                result = objCommon.GetDataExecuteScaler("SP_AddIrrigationTaskAssignmentStart", nv);
                if (result > 0)
                {
                    Response.Redirect(String.Format("~/IrrigationTaskCompletion.aspx?IrrigationCode={0}", IrrigationCode));
                }


            }

            if (e.CommandName == "ViewDetails")
            {
                string IrrigationCode = e.CommandArgument.ToString();
                Response.Redirect(String.Format("~/IrrigationTaskViewDetails.aspx?IrrigationCode={0}", IrrigationCode));
            }


        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridIrrigation(1);
        }




        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnAssign = (Button)e.Row.FindControl("btnAssign");
                Button btnSelect = (Button)e.Row.FindControl("btnSelect");

                int RoleId = Convert.ToInt32(Session["Role"]);
                if (RoleId == 11 || RoleId == 3 || RoleId == 5)
                {
                    btnSelect.Visible = true;
                    btnAssign.Visible = false;
                }

                //GridView GridViewFields = e.Row.FindControl("GridViewDetails") as GridView;
                //GridView GridViewFShow = e.Row.FindControl("GridViewFShow") as GridView;
                //Label lblIrrigationCode = (Label)e.Row.FindControl("lblIrrigationCode");

                //DataTable dt = new DataTable();
                //NameValueCollection nv = new NameValueCollection();
                //nv.Add("@IrrigationCode", lblIrrigationCode.Text);
                //dt = objCommon.GetDataTable("SP_GetIrrigationRequestDetails", nv);
                //GridViewFields.DataSource = dt;
                //GridViewFields.DataBind();

                //DataTable dt1 = new DataTable();
                //NameValueCollection nv1 = new NameValueCollection();
                //nv1.Add("@IrrigationCode", lblIrrigationCode.Text);
                //dt1 = objCommon.GetDataTable("SP_GetIrrigationRequestGreenHouseDetails", nv1);
                //GridViewFShow.DataSource = dt1;
                //GridViewFShow.DataBind();
            }
        }
    }
}
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
    public partial class IrrigationCompletionForm1 : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Bindcname();
                //  BindJobCode();
                //  BindFacility();
                BindGridGerm(0);

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



        //public void Bindcname()
        //{

        //    DataTable dt = new DataTable();
        //    NameValueCollection nv = new NameValueCollection();

        //    nv.Add("@Mode", "8");
        //    dt = objCommon.GetDataTable("GET_Common", nv);
        //    ddlCustomer.DataSource = dt;
        //    ddlCustomer.DataTextField = "cname";
        //    ddlCustomer.DataValueField = "cname";
        //    ddlCustomer.DataBind();
        //    ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));

        //}


        //public void BindJobCode()
        //{

        //    DataTable dt = new DataTable();
        //    NameValueCollection nv = new NameValueCollection();

        //    nv.Add("@Mode", "7");
        //    dt = objCommon.GetDataTable("GET_Common", nv);
        //    ddlJobNo.DataSource = dt;
        //    ddlJobNo.DataTextField = "Jobcode";
        //    ddlJobNo.DataValueField = "Jobcode";
        //    ddlJobNo.DataBind();
        //    ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        //}

        //public void BindFacility()
        //{

        //    DataTable dt = new DataTable();
        //    NameValueCollection nv = new NameValueCollection();

        //    nv.Add("@Mode", "9");
        //    dt = objCommon.GetDataTable("GET_Common", nv);
        //    ddlFacility.DataSource = dt;
        //    ddlFacility.DataTextField = "loc_seedline";
        //    ddlFacility.DataValueField = "loc_seedline";
        //    ddlFacility.DataBind();
        //    ddlFacility.Items.Insert(0, new ListItem("--Select--", "0"));

        //}

        public void BindGridGerm(int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@wo", "");
            //nv.Add("@JobCode", ddlJobNo.SelectedValue);
            //nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            //nv.Add("@Facility", ddlFacility.SelectedValue);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            //nv.Add("@Mode", "6");
            //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            dt = objCommon.GetDataTable("SP_GetOperatorIrrigationTask", nv);
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

            if (p != 1 && !string.IsNullOrEmpty(benchLoc))
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
                //var checkJob = (row.FindControl("lbljobID") as Label).Text;
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
            BindGridGerm(1);
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm(1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm(1);
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            // Bindcname();
            // BindJobCode();
            //  BindFacility();
            BindGridGerm(1);
        }
        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string JobID = "";


            if (e.CommandName == "Select")
            {
                //long result = 0;
                //string WOID = e.CommandArgument.ToString();
                //NameValueCollection nv = new NameValueCollection();
                //nv.Add("@OperatorID", Session["LoginID"].ToString());
                //nv.Add("@wo", WOID);
                //nv.Add("@SprayDate", "");
                //nv.Add("@TraysSprayed", "");
                //nv.Add("@SprayDuration", "");

                //nv.Add("@LoginID", Session["LoginID"].ToString());

                //nv.Add("@mode", "1");
                //result = objCommon.GetDataInsertORUpdate("SP_AddIrrigationTaskAssignment", nv);
                //Response.Redirect(String.Format("~/IrrigationTaskCompletion.aspx?WOId={0}&ICom={1}", WOID, 0));
                Response.Redirect(String.Format("~/IrrigationTaskCompletion.aspx?IrrigationCode={0}", e.CommandArgument.ToString()));
            }

            if (e.CommandName == "ViewDetails")
            {
                Response.Redirect(String.Format("~/IrrigationTaskViewDetails.aspx?IrrigationCode={0}", e.CommandArgument.ToString()));
            }
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm(1);
        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

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
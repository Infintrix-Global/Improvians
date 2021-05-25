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
    public partial class GeneralTaskCompletionForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                BindFacility();
                BindTask(0);

            }
        }

        private string JobCode
        {
            get
            {
                if (Request.QueryString["jobId"] != null)
                {
                    return Request.QueryString["jobId"].ToString();
                }
                return "";
            }
            set
            {
                // JobCode = Request.QueryString["jobId"].ToString();
                // JobCode = value;
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

        private string TaskKey
        {
            get
            {
                if (Request.QueryString["Tkey"] != null)
                {
                    return Request.QueryString["Tkey"].ToString();
                }
                return "";
            }
            set
            {

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
        public void BindTask(int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //  nv.Add("@wo", "");
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", ddlFacility.SelectedValue);
            //  nv.Add("@Mode", "9");
            nv.Add("@LoginID", Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("SP_GetOperatorGeneralTask", nv);
            gvTask.DataSource = dt;
            gvTask.DataBind();


            //foreach (GridViewRow row in gvTask.Rows)
            //{
            //    var checkJob = (row.FindControl("lbljobID") as Label).Text;
            //    if (checkJob == JobCode)
            //    {
            //        row.CssClass = "highlighted";
            //    }
            //}

            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }
        }
        private void highlight(int limit)
        {
            var i = gvTask.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvTask.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                var tKey = gvTask.DataKeys[row.RowIndex].Values[2].ToString();
                i--;
                if (checkJob == JobCode && checklocation == benchLoc && tKey == TaskKey)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvTask.PageIndex++;
                    gvTask.DataBind();
                    highlight((limit - 10));
                }
            }
        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTask(1);
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTask(1);
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTask(1);
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();
            BindFacility();
            BindTask(1);
        }
        protected void gvTask_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Start")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string DRid = gvTask.DataKeys[rowIndex].Values[0].ToString();
                string Did = gvTask.DataKeys[rowIndex].Values[1].ToString();
                string TaskRequestKey = gvTask.DataKeys[rowIndex].Values[2].ToString();

                Response.Redirect(String.Format("~/GeneralTaskCompletion.aspx?Did={0}&Chid={1}&DrId={2}&IsF={3}&TaskRequestKey={4}", Did, "0", DRid, 0, TaskRequestKey));

                //  Response.Redirect(String.Format("~/PlantReadyTaskCompletion.aspx?PRAID={0}", PRAID));
                //   Response.Redirect(String.Format("~/GeneralTaskCompletion.aspx?Did={0}&Chid={1}", PRAID, 0));

            }
        }

        protected void gvTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTask.PageIndex = e.NewPageIndex;
            BindTask(1);
        }

        protected void gvTask_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGermDate = (Label)e.Row.FindControl("lblGenDate");
                string dtimeString = Convert.ToDateTime(lblGermDate.Text).ToString("yyyy/MM/dd");

                DateTime dtime = Convert.ToDateTime(dtimeString);

                DateTime nowtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));

                if (nowtime > dtime)
                {
                    e.Row.CssClass = "overdue";
                }
            }
        }
    }
}
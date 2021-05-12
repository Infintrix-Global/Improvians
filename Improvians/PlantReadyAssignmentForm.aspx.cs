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
    public partial class PlantReadyAssignmentForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                BindGridGerm(0);
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

        public void BindGridGerm(int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@wo", "");
            //nv.Add("@JobCode", ddlJobNo.SelectedValue);
            //   nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            // nv.Add("@Facility", ddlFacility.SelectedValue);
            //nv.Add("@Mode", "8");
            nv.Add("@LoginID", Session["LoginID"].ToString());

            //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            dt = objCommon.GetDataTable("SP_GetSupervisorPlantReadyTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();


            //foreach (GridViewRow row in gvGerm.Rows)
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
            var i = gvGerm.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvGerm.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                var tKey = gvGerm.DataKeys[row.RowIndex].Values[2].ToString();
                i--;
                if (checkJob == JobCode && checklocation == benchLoc && tKey == TaskKey)

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
            Bindcname();
            BindJobCode();
            BindGridGerm(1);
        }
        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string JobID = "";
            string ChId = "";
            string TaskID = "";
            if (e.CommandName == "Assign")
            {
                //int rowIndex = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = gvGerm.Rows[rowIndex];

                // string PRID = e.CommandArgument.ToString();
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string PRID = gvGerm.DataKeys[rowIndex].Values[0].ToString();
                ChId = gvGerm.DataKeys[rowIndex].Values[1].ToString();
                var TaskRequestKey = gvGerm.DataKeys[rowIndex].Values[2].ToString();
                if (ChId == "")
                {
                    ChId = "0";
                }
                else
                {
                    ChId = ChId;
                }
                Response.Redirect(String.Format("~/PlantReadyTaskAssignment.aspx?PRID={0}&Chid={1}&TaskRequestKey={2}", PRID, ChId, TaskRequestKey));

            }


            if (e.CommandName == "Select")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string PRID = gvGerm.DataKeys[rowIndex].Values[0].ToString();
                ChId = gvGerm.DataKeys[rowIndex].Values[1].ToString();

                if (ChId == "")
                {
                    ChId = "0";
                }
                else
                {
                    ChId = ChId;
                }
                //NameValueCollection nv = new NameValueCollection();
                //nv.Add("@OperatorID", Session["LoginID"].ToString());
                //nv.Add("@Notes", "");
                //nv.Add("@JobID", JobID);
                //nv.Add("@LoginID", Session["LoginID"].ToString());
                //nv.Add("@CropId", "");
                //nv.Add("@UpdatedReadyDate", "");
                //nv.Add("@PlantExpirationDate", "");
                //nv.Add("@RootQuality", "");
                //nv.Add("@PlantHeight", "");
                //nv.Add("@wo", WO);
                //nv.Add("@mode", "4");

                NameValueCollection nv = new NameValueCollection();
                nv.Add("@OperatorID", Session["LoginID"].ToString());
                nv.Add("@Notes", "");
                nv.Add("@PRID", PRID);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@PlantExpirationDate", "");

                long result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyTaskAssignmentNew", nv);

                //  int result = objCommon.GetDataInsertORUpdate("SP_AddPlantReadyTaskAssignment", nv);

                if (result > 0)
                {
                    Response.Redirect(String.Format("~/PlantReadyTaskCompletion.aspx?PRAID={0}&Chid={1}&PRID={2}", result, ChId, PRID));
                }
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

                Label lbljstatus = (Label)e.Row.FindControl("lbljstatus");
                Label lblTitla = (Label)e.Row.FindControl("lblTitla");

                if (lbljstatus.Text == "4")
                {
                    lblTitla.Text = "Plant Ready Request";
                }


            }
        }

        protected void gvGerm_RowDataBound1(object sender, GridViewRowEventArgs e)
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

                Label lblGermDate = (Label)e.Row.FindControl("lblSeededDate");
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
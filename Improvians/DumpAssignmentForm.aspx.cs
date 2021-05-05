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
    public partial class DumpAssignmentForm : System.Web.UI.Page
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
            nv.Add("@Facility", Session["Facility"].ToString());
            //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            dt = objCommon.GetDataTable("SP_GetSupervisorDumpTask", nv);
            gvDump.DataSource = dt;
            gvDump.DataBind();
            if (p != 1 && !string.IsNullOrEmpty(JobCode) && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count); 
            }


         }
        private void highlight(int limit)
        {
            var i = gvDump.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvDump.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                i--;
                if (checkJob == JobCode && checklocation == benchLoc)
                {
                    row.CssClass = "highlighted";                   
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvDump.PageIndex++;
                    gvDump.DataBind();
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
        protected void gvDump_RowCommand(object sender, GridViewCommandEventArgs e)
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
                string Did = gvDump.DataKeys[rowIndex].Values[0].ToString();
                ChId = gvDump.DataKeys[rowIndex].Values[1].ToString();
                string TaskRequestKey = gvDump.DataKeys[rowIndex].Values[2].ToString();
                if (ChId == "")
                {
                    ChId = "0";
                }
                else
                {
                    ChId = ChId;
                }
                Response.Redirect(String.Format("~/DumpTaskAssignment.aspx?Did={0}&Chid={1}&TaskRequestKey={2}", Did, ChId, TaskRequestKey));

            }


            if (e.CommandName == "Select")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string Did = gvDump.DataKeys[rowIndex].Values[0].ToString();
                ChId = gvDump.DataKeys[rowIndex].Values[1].ToString();
                string TaskRequestKey = gvDump.DataKeys[rowIndex].Values[2].ToString();


                if (ChId == "")
                {
                    ChId = "0";
                }
                else
                {
                    ChId = ChId;
                }


                NameValueCollection nv = new NameValueCollection();
                nv.Add("@OperatorID", Session["LoginID"].ToString());
                nv.Add("@Comments", "");
                nv.Add("@DumpId", Did);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@QuantityOfTray", "");
                nv.Add("@DumpDate", "");


                long result = objCommon.GetDataExecuteScaler("SP_AddDumpTaskStart", nv);


                if (result > 0)
                {
                    Response.Redirect(String.Format("~/DumpTaskCompletion.aspx?Did={0}&Chid={1}&DrId={2}&TaskRequestKey={3}", result, ChId, Did, TaskRequestKey));
                }
            }
        }

        protected void gvDump_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDump.PageIndex = e.NewPageIndex;
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

        protected void gvDump_RowDataBound1(object sender, GridViewRowEventArgs e)
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

            }
        }
    }
}
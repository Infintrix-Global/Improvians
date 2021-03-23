using Evo;
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
    public partial class GeneralTaskAssignmentForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                BindTask();
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

        public void BindTask()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@ai", "2");
            //dt = objCommon.GetDataTable("SP_GetAllTask", nv);

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
        
            dt = objCommon.GetDataTable("SP_GetSupervisorGeneralTask", nv);
            gvTask.DataSource = dt;
            gvTask.DataBind();            

        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTask();

        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTask();
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();
            BindTask();
        }

        protected void gvTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTask.PageIndex = e.NewPageIndex;
            BindTask();
        }

        protected void gvTask_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string JobID = "";
            string ChId = "";
            string TaskID = "";
            if (e.CommandName == "Assign")
            {
               
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string Did = gvTask.DataKeys[rowIndex].Values[0].ToString();
                ChId = gvTask.DataKeys[rowIndex].Values[1].ToString();

                if (ChId == "")
                {
                    ChId = "0";
                }
                else
                {
                    ChId = ChId;
                }
                Response.Redirect(String.Format("~/GeneralTaskAssignment.aspx?Did={0}&Chid={1}", Did, ChId));

            }


            if (e.CommandName == "Start")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string Did = gvTask.DataKeys[rowIndex].Values[0].ToString();
                ChId = gvTask.DataKeys[rowIndex].Values[1].ToString();

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
                nv.Add("@GeneralId", Did);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@QuantityOfTray", "");
                nv.Add("@GeneralTaskDate", "");
                long result = objCommon.GetDataExecuteScaler("SP_AddGeneralTaskAssignment", nv);
                if (result > 0)
                {
                    Response.Redirect(String.Format("~/GeneralTaskCompletion.aspx?Did={0}&Chid={1}", result, ChId));
                }
            }
        }

        protected void gvTask_RowDataBound(object sender, GridViewRowEventArgs e)
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
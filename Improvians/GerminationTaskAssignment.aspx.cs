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
    public partial class GerminationTaskAssignment : System.Web.UI.Page
    {
        string wo;
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["GTRID"] != null)
                {
                    gtrID = Request.QueryString["GTRID"].ToString();
                }
                if (Request.QueryString["Chid"] != "0" && Request.QueryString["Chid"] != null)
                {
                    BindGridCropHealth(Convert.ToInt32(Request.QueryString["Chid"]));
                }
                BindGTRView(gtrID);
                BindGridGerm();
                BindOperatorList();
            }
        }


        public void BindGTRView(string GTRID)
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@GTRId", GTRID);
            dt1 = objCommon.GetDataTable("SP_GetTaskAssignmenGerminationTaskView", nv1);

            if (dt1 != null && dt1.Rows.Count > 0)
            {
                txtDate.Text =Convert.ToDateTime (dt1.Rows[0]["InspectionDueDate"]).ToString("yyyy-MM-dd");
                txtNotes.Text = dt1.Rows[0]["Comments"].ToString();
                txtTrays.Text = dt1.Rows[0]["#TraysInspected"].ToString();
            }


        }


        public void BindGridCropHealth(int Chid)
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Chid", Chid.ToString());
            dt1 = objCommon.GetDataTable("SP_GetCropHealthReportSelect", nv1);

            if (dt1 != null && dt1.Rows.Count > 0)
            {
                PanelCropHealth.Visible =true;
                gvCropHealth.DataSource = dt1;
                gvCropHealth.DataBind();

                lblCommment.Text = dt1.Rows[0]["CropHealthCommit"].ToString();
            }


        }

        private string gtrID
        {
            get
            {
                if (ViewState["gtrID"] != null)
                {
                    return (string)ViewState["gtrID"];
                }
                return "";
            }
            set
            {
                ViewState["gtrID"] = value;
            }
        }
        public void BindOperatorList()
        {
            NameValueCollection nv = new NameValueCollection();
           // nv.Add("@RoleID", "3");

          //  ddlOperator.DataSource = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv);
            ddlOperator.DataSource = objCommon.GetDataTable("SP_GetRoleForSupervisor", nv);
            ddlOperator.DataTextField = "EmployeeName";
            ddlOperator.DataValueField = "ID";
            ddlOperator.DataBind();
            ddlOperator.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@GTRID", gtrID);
            dt = objCommon.GetDataTable("SP_GetGreenHouseSupervisorAssignedJobByGTRID", nv);
            wo = dt.Rows[0]["wo"].ToString();
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@OperatorID", ddlOperator.SelectedValue);
            nv.Add("@Notes", txtNotes.Text);
            nv.Add("@WorkOrderID", wo);
            nv.Add("@GTRID", gtrID);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataExecuteScaler("SP_AddGerminationAssignmentNew", nv);
            if (result > 0)
            {
                //lblmsg.Text = "Assignment Successful";
                clear();
                string message = "Assignment Successful";
                string url = "MyTaskGreenSupervisorFinal.aspx";
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

        public void clear()
        {
            ddlOperator.SelectedIndex = 0;
            txtNotes.Text = "";
           

        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGreenSupervisorFinal.aspx");
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                
                Label lblGermNo = (Label)e.Row.FindControl("lblGermNo");
              
                HyperLink lnkJobID = (HyperLink)e.Row.FindControl("lnkJobID");
                lnkJobID.NavigateUrl = "~/JobReports.aspx?JobCode=" + lnkJobID.Text + "&GermNo=" + lblGermNo.Text;
                //  lnkJobID.NavigateUrl(String.Format("~/CropHealthReport.aspx?Chid={0}", Chid));
            }
        }
    }
}
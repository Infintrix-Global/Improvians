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
    public partial class PlantReadyTaskAssignment : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["PRID"] != null)
                {
                    PRID = Request.QueryString["PRID"].ToString();
                }
                if (Request.QueryString["Chid"] != "0" && Request.QueryString["Chid"] != null)
                {
                    BindGridCropHealth(Convert.ToInt32(Request.QueryString["Chid"]));
                }

                BindGridGerm();
                BindOperatorList();
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
                PanelCropHealth.Visible = true;
                gvCropHealth.DataSource = dt1;
                gvCropHealth.DataBind();
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

        private string PRID
        {
            get
            {
                if (ViewState["PRID"] != null)
                {
                    return (string)ViewState["PRID"];
                }
                return "";
            }
            set
            {
                ViewState["PRID"] = value;
            }
        }


        public void BindOperatorList()
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@RoleID", "3");
            ddlOperator.DataSource = objCommon.GetDataTable("SP_GetRoleWiseEmployee", nv); ;
            ddlOperator.DataTextField = "EmployeeName";
            ddlOperator.DataValueField = "ID";
            ddlOperator.DataBind();
            ddlOperator.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@wo", wo);
            //nv.Add("@JobCode", "");
            //nv.Add("@CustomerName", "");
            //nv.Add("@Facility", "");
            //nv.Add("@Mode", "10");
            //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            nv.Add("@PRID", PRID);
            dt = objCommon.GetDataTable("SP_GetSupervisorPlantReadyTaskByPRID", nv);
            gvPlantReady.DataSource = dt;
            gvPlantReady.DataBind();


        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@OperatorID", ddlOperator.SelectedValue);
            //nv.Add("@Notes", txtNotes.Text);
            //nv.Add("@JobID","");
            //nv.Add("@LoginID", Session["LoginID"].ToString());
            //nv.Add("@CropId","");
            //nv.Add("@UpdatedReadyDate", "");
            //nv.Add("@PlantExpirationDate","");
            //nv.Add("@RootQuality","");
            //nv.Add("@wo",wo);
            //nv.Add("@PlantHeight","");

            //nv.Add("@mode", "3");
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@OperatorID", ddlOperator.SelectedValue);
            nv.Add("@Notes", txtNotes.Text);
            nv.Add("@PRID", PRID);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyTaskAssignment", nv);
            if (result > 0)
            {
                //lblmsg.Text = "Assignment Successful";
                clear();
                string message = "Assignment Successful";
                string url = "PlantReadyAssignmentForm.aspx";
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
            Response.Redirect("~/PlantReadyAssignmentForm.aspx");
        }

        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlantReady.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }
    }
}
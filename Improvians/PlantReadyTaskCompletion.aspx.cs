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
    public partial class PlantReadyTaskCompletion : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["PRAID"] != null)
                {
                    PRAID = Request.QueryString["PRAID"].ToString();
                }

                if (Request.QueryString["Chid"] != "0" && Request.QueryString["Chid"] != null)
                {
                    BindGridCropHealth(Convert.ToInt32(Request.QueryString["Chid"]));
                }
                BindPlantReady();
                BindViewDetilas(Convert.ToInt32(Request.QueryString["PRID"]));
            }
        }

        public void BindViewDetilas(int PRid)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@PlantReadyId", PRid.ToString());

            dt = objCommon.GetDataTable("SP_GetTaskAssignmenPlantReadytView", nv);
            GridViewDumpView.DataSource = dt;
            GridViewDumpView.DataBind();

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
                lblCommment.Text = dt1.Rows[0]["CropHealthCommit"].ToString();
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

        private string PRAID
        {
            get
            {
                if (ViewState["PRAID"] != null)
                {
                    return (string)ViewState["PRAID"];
                }
                return "";
            }
            set
            {
                ViewState["PRAID"] = value;
            }
        }


        public void BindPlantReady()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@wo", wo);
            //nv.Add("@JobCode", "");
            //nv.Add("@CustomerName", "");
            //nv.Add("@Facility", "");
            //nv.Add("@Mode", "11");
            nv.Add("@PRAID", PRAID);
            dt = objCommon.GetDataTable("SP_GetOperatorPlantReadyTaskByPRAID", nv);
            gvPlantReady.DataSource = dt;
            gvPlantReady.DataBind();
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    lblSeedlot.Text = dt.Rows[0]["SeedLots"].ToString();
            //}
        }

        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlantReady.PageIndex = e.NewPageIndex;
            BindPlantReady();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            //  nv.Add("@OperatorID",Session["LoginID"].ToString());
            //   nv.Add("@Notes", txtNots.Text);
            //    nv.Add("@JobID", "");
            nv.Add("@PRAID", PRAID);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@CropId", "");
            nv.Add("@UpdatedReadyDate", txtUpdatedReadyDate.Text);
            nv.Add("@PlantExpirationDate", txtPlantExpirationDate.Text);
            nv.Add("@RootQuality", ddlRootQuality.SelectedItem.Text);
            nv.Add("@PlantHeight", ddlPlantHeight.SelectedItem.Text);
            //   nv.Add("@wo",wo);
            //  nv.Add("@mode","2");

            result = objCommon.GetDataExecuteScaler("SP_AddPlantReadyCompletion", nv);



            if (result > 0)
            {
                // lblmsg.Text = "Completion Successful";
                clear();
                string message = "Completion Successful";
                string url;
                if (Session["Role"].ToString() == "3")
                {
                    url = "PlantReadyCompletionForm.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
                if (Session["Role"].ToString() == "2")
                {
                    url = "PlantReadyAssignmentForm.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }


            }
            else
            {
                lblmsg.Text = "Completion Not Successful";
            }
        }

        public void clear()
        {
            //txtNots.Text = "";
            txtPlantExpirationDate.Text = "";
            //  txtPlantHeight.Text = "";
            txtUpdatedReadyDate.Text = "";
            ddlPlantHeight.SelectedValue = "0";
            ddlRootQuality.SelectedValue = "0";


        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            if (Session["Role"].ToString() == "3")
            {
                Response.Redirect("~/PlantReadyCompletionForm.aspx");
            }

            if (Session["Role"].ToString() == "2")
            {
                Response.Redirect("~/PlantReadyAssignmentForm.aspx");
            }
        }
    }
}
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
    public partial class GeneralTaskCompletion: System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Did"] != null)
                {
                    Did = Request.QueryString["Did"].ToString();
                }

                if (Request.QueryString["Chid"] != "0" && Request.QueryString["Chid"] != null)
                {
                    BindGridCropHealth(Convert.ToInt32(Request.QueryString["Chid"]));
                }
                BindTask();

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

        private string Did
        {
            get
            {
                if (ViewState["Did"] != null)
                {
                    return (string)ViewState["Did"];
                }
                return "";
            }
            set
            {
                ViewState["Did"] = value;
            }
        }


        public void BindTask()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
           
            nv.Add("@GeneralTaskAssignmentId", Did);
            dt = objCommon.GetDataTable("SP_GetOperatorGeneralTaskDetails", nv);
            gvTask.DataSource = dt;
            gvTask.DataBind();
            DateTime dNow = new DateTime();
            dNow = Convert.ToDateTime(dt.AsEnumerable().Select(r => r.Field<DateTime>("GeneralTaskDate")).FirstOrDefault().ToString("yyyy/MM/dd"));
            txtGeneralDate.Text = (dNow.ToString("yyyy-MM-dd"));

        }

        protected void gvTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTask.PageIndex = e.NewPageIndex;
            BindTask();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            //  nv.Add("@OperatorID",Session["LoginID"].ToString());
            //   nv.Add("@Notes", txtNots.Text);
            //    nv.Add("@JobID", "");
            nv.Add("@Did", Did);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Comments", txtComment.Text);
            nv.Add("@QuantityOfTray", txtQuantityOfTray.Text);
            nv.Add("@GeneralTaskDate", txtGeneralDate.Text);



            result = objCommon.GetDataExecuteScaler("SP_AddGeneralTaskCompletion", nv);



            if (result > 0)
            {
                // lblmsg.Text = "Completion Successful";
                clear();
                string message = "Completion Successful";
                string url;
                if (Session["Role"].ToString() == "3")
                {
                    url = "GeneralTaskAssignmentForm.aspx";
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
                    url = "GeneralTaskAssignmentForm.aspx";
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
            txtComment.Text = "";
            //  txtPlantHeight.Text = "";
            txtGeneralDate.Text = "";
            txtQuantityOfTray.Text = "";


        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            if (Session["Role"].ToString() == "3")
            {
                //  Response.Redirect("~/PlantReadyCompletionForm.aspx");
            }

            if (Session["Role"].ToString() == "2")
            {
                // Response.Redirect("~/PlantReadyAssignmentForm.aspx");
            }
        }
    }
}
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
    public partial class IrrigationCompletionForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["IrrigationCode"] != null)
                {
                    IrrigationCode = Request.QueryString["IrrigationCode"].ToString();
                }
                txtSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                BindenchLocation();
                BindgvIrrigation();
               
            }
        }

        public void BindenchLocation()
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@IrrigationCode", IrrigationCode);
            dt1 = objCommon.GetDataTable("SP_GetIrrigationRequestGreenHouseDetails", nv1);

            lblBenchLocation.Text = dt1.Rows[0]["GreenHouseID"].ToString();
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

        private string IrrigationCode
        {
            get
            {
                if (ViewState["IrrigationCode"] != null)
                {
                    return (string)ViewState["IrrigationCode"];
                }
                return "";
            }
            set
            {
                ViewState["IrrigationCode"] = value;
            }
        }

      
        public void BindgvIrrigation()
        {
            string ChId = "";
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@wo", wo);
            //nv.Add("@JobCode", "");
            //nv.Add("@CustomerName", "");
            //nv.Add("@Facility", "");
            //nv.Add("@Mode", "5");
            //dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            nv.Add("@IrrigationCode", IrrigationCode);
            nv.Add("@RoleId", Session["Role"].ToString());
            dt = objCommon.GetDataTable("SP_GetOperatorIrrigationTaskByIrrigationCode", nv);
            if (dt != null & dt.Rows.Count > 0)
            {
                gvIrrigation.DataSource = dt;
                gvIrrigation.DataBind();

                // txtNoofPasses.Text = dt.Rows[0]["WaterRequired"].ToString();
                //  lblBenchLocation.Text = dt.Rows[0]["GreenHouseID"].ToString();

                ChId = dt.Rows[0]["CropHealth"].ToString();
            }
            if (ChId == "")
            {
                ChId = "0";
            }
            else
            {
                ChId = ChId;
            }
         
        }
      
        protected void gvIrrigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIrrigation.PageIndex = e.NewPageIndex;
            BindgvIrrigation();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            long result = 0;

            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@OperatorID", Session["LoginID"].ToString());
            //nv.Add("@wo", wo);
            nv.Add("@IrrigationCode", IrrigationCode);
            nv.Add("@SprayDate", txtSprayDate.Text.Trim());
            nv.Add("@TraysSprayed", "");
            nv.Add("@SprayDuration", "");
            nv.Add("@NoOfPasses", txtNoofPasses.Text.Trim());
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Banchloc", lblBenchLocation.Text);
            //if (Request.QueryString["ICom"] =="1")
            //{
            //    nv.Add("@mode", "1");

            //}
            //else
            //{
            //    nv.Add("@mode", "3");
            //}

            result = objCommon.GetDataInsertORUpdate("SP_AddIrrigationTaskCompletion", nv);
            GridViewRow row = gvIrrigation.Rows[0];
            var txtJobNo = (row.FindControl("lblID") as Label).Text;
            var txtBenchLocation = (row.FindControl("lblGreenHouseID") as Label).Text;

            NameValueCollection nameValue = new NameValueCollection();
            nameValue.Add("@LoginID", Session["LoginID"].ToString());
            nameValue.Add("@jobcode", txtJobNo);
            nameValue.Add("@GreenHouseID", txtBenchLocation);
            nameValue.Add("@TaskName", "Irrigation");

            var check = objCommon.GetDataInsertORUpdate("SP_RemoveCompletedTaskNotification", nameValue);

            var res = (Master.FindControl("r1") as Repeater);
            var lblCount = (Master.FindControl("lblNotificationCount") as Label);
            objCommon.GetAllNotifications(Session["LoginID"].ToString(), Session["Facility"].ToString(), res, lblCount);

            if (result > 0)
            {
                // lblmsg.Text = "Completion Successful";
                clear();
                string message = "Completion Successful";
                string url;
                if (Session["Role"].ToString() == "12")
                {
                    url = "IrrigationRequestForm.aspx";
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
                    url = "IrrigationAssignmentForm.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
                if (Session["Role"].ToString() == "1")
                {
                    url = "IrrigationAssignmentForm.aspx";
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
                    url = "IrrigationCompletionForm.aspx";
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
            txtSprayDate.Text = "";
            //   txtSprayDuration.Text = "";
            //  txtTraysSprayed.Text = "";

            txtNoofPasses.Text = "";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            if (Session["Role"].ToString() == "3")
            {
                Response.Redirect("~/IrrigationCompletionForm.aspx");
            }
        }
    }
}
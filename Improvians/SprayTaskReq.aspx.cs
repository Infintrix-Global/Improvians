using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Improvians.BAL_Classes;
using Improvians.Bal;

namespace Improvians
{
    public partial class SprayTaskReq : System.Web.UI.Page
    {

        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["FertilizationCode"] != null)
                {
                    FertilizationCode = Request.QueryString["FertilizationCode"].ToString();
                
                }

                txtSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
                BindenchLocation();
                BindGridSprayReq();
            }
        }

        private string FertilizationCode
        {
            get
            {
                if (ViewState["FertilizationCode"] != null)
                {
                    return (string)ViewState["FertilizationCode"];
                }
                return "";
            }
            set
            {
                ViewState["FertilizationCode"] = value;
            }
        }

        public void BindGridSprayReq()
        {
            string ChId = "";
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@JobCode", "0");
            nv.Add("@TraySize", "0");
            nv.Add("@itemno", "0");
            nv.Add("@BenchLocation", "0");
            nv.Add("@FertilizationCode", FertilizationCode);
            dt = objCommon.GetDataTable("SP_GetSprayRequestst", nv);
         
            ChId = dt.Rows[0]["CropHealth"].ToString();
            if (ChId == "")
            {
                ChId = "0";
            }
            else
            {
                ChId = ChId;
            }
            BindGridCropHealth(Convert.ToInt32(ChId));

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


        public void BindenchLocation()
        {
            DataTable dt1 = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            //nv1.Add("@JobCode", ddlJobNo.SelectedValue);
            //nv1.Add("@CustomerName", ddlCustomer.SelectedValue);
            //nv1.Add("@Facility", ddlFacility.SelectedValue);
            //nv1.Add("@LoginID", Session["LoginID"].ToString());
            //nv1.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            //nv1.Add("@FertilizationCode", lblFertilizationCode.Text);
            nv1.Add("@FertilizationCode", FertilizationCode);

            dt1 = objCommon.GetDataTable("SP_GetSprayRequestSelectDetails", nv1);
            lblBenchLocation.Text = dt1.Rows[0]["GreenHouseID"].ToString();



        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }


        public void clear()
        {


            txtNotes.Text = "";

            txtSprayDate.Text = "";

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@SupervisorID", Session["LoginID"].ToString());

            // nv.Add("@GrowerPutAwayId", lblGrowerID.Text);
            nv.Add("@FertilizationCode", FertilizationCode);
            nv.Add("@SprayDate", txtSprayDate.Text.Trim());

            nv.Add("@Nots", txtNotes.Text.Trim());
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataInsertORUpdate("SP_AddSprayRequest", nv);
            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                string message = "Completion Successful";
                string url = "SprayTaskRequest.aspx";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                // lblmsg.Text = "Assignment Successful";
                clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('not Completion')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }
        }




    


    }
}
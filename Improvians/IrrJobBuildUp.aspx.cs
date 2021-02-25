using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class IrrJobBuildUp : System.Web.UI.Page
    {

        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Bench"] != null)
                {
                    Bench = Request.QueryString["Bench"].ToString();
                }

                BindGridIrrigation();
                BindSupervisorList();
                BindGridIrrDetails();
                lblbench.Text = Bench;
            }
        }

        private string Bench
        {
            get
            {
                if (ViewState["Bench"] != null)
                {
                    return (string)ViewState["Bench"];
                }
                return "";
            }
            set
            {
                ViewState["Bench"] = value;
            }
        }

        public void BindGridIrrigation()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@wo","");
            //  nv.Add("@JobCode",ddlJobNo.SelectedValue);
            //  nv.Add("@CustomerName",ddlCustomer.SelectedValue);
            //   nv.Add("@Facility",ddlFacility.SelectedValue);
            //  nv.Add("@Mode", "1");
            // dt = objCommon.GetDataTable("SP_GetGTIJobsSeedsPlan", nv);
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Facility", "0");
            nv.Add("@BenchLocation", Bench);
           
            dt = objCommon.GetDataTable("SP_GetIrrigationRequest", nv);
            GridIrrigation.DataSource = dt;
            GridIrrigation.DataBind();
            int tray = 0;
            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                    tray = tray + Convert.ToInt32((row.FindControl("lbltotTray") as Label).Text);
               
            }
        }

        public void BindSupervisorList()
        {
            //NameValueCollection nv = new NameValueCollection();
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            //ddlSupervisor.DataTextField = "EmployeeName";
            //ddlSupervisor.DataValueField = "ID";
            //ddlSupervisor.DataBind();
            //ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));

            NameValueCollection nv = new NameValueCollection();
            if (Session["Role"].ToString() == "1")
            {
                ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlSupervisor.DataTextField = "EmployeeName";
                ddlSupervisor.DataValueField = "ID";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            if (Session["Role"].ToString() == "12")
            {
                ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlSupervisor.DataTextField = "EmployeeName";
                ddlSupervisor.DataValueField = "ID";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        public void BindGridIrrDetails()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", Bench);
            dt = objCommon.GetDataTable("SP_GetIrrigationRequestHistory", nv);
            gvJobHistory.DataSource = dt;
            gvJobHistory.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int IrrigationCode = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Mode", "13");
            dt = objCommon.GetDataTable("GET_Common", nv1);
            IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);
            foreach (GridViewRow row in GridIrrigation.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{

                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);

                    //  nv.Add("@GrowerPutAwayId", lblGrowerID.Text);
                    nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    nv.Add("@IrrigatedNoTrays", (row.FindControl("lbltotTray") as Label).Text);
                    // nv.Add("@IrrigatedNoTrays", txtIrrigatedNoTrays.Text.Trim());
                    nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                    nv.Add("@IrrigationDuration", "");
                    nv.Add("@SprayDate", txtSprayDate.Text.Trim());
                    nv.Add("@SprayTime", "");
                    nv.Add("@Nots", txtNotes.Text.Trim());
                    nv.Add("@IrrigationCode", IrrigationCode.ToString());
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@NoOfPasses", "");

                    result = objCommon.GetDataInsertORUpdate("SP_AddIrrigationRequest", nv);
                    if (result > 0)
                    {
                        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                        //  lblmsg.Text = "Assignment Not Successful";
                    }
                //}
            }
            foreach (GridViewRow row in gvJobHistory.Rows)
            {
                //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //{

                long result = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);

                //  nv.Add("@GrowerPutAwayId", lblGrowerID.Text);
                nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                nv.Add("@IrrigatedNoTrays", (row.FindControl("lbltotTray") as Label).Text);
                // nv.Add("@IrrigatedNoTrays", txtIrrigatedNoTrays.Text.Trim());
                nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                nv.Add("@IrrigationDuration", "");
                nv.Add("@SprayDate", txtSprayDate.Text.Trim());
                nv.Add("@SprayTime", "");
                nv.Add("@Nots", txtNotes.Text.Trim());
                nv.Add("@IrrigationCode", IrrigationCode.ToString());
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@NoOfPasses", "");

                result = objCommon.GetDataInsertORUpdate("SP_AddIrrigationRequest", nv);
               
            }
            string message = "Assignment Successful";
            string url = "MyTaskGrower.aspx";
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

        public void clear()
        {

            ddlSupervisor.SelectedIndex = 0;
            txtWaterRequired.Text = "";
            txtNotes.Text = "";
            //  txtIrrigatedNoTrays.Text = "";
            //txtIrrigationDuration.Text = "";
            txtSprayDate.Text = "";
            // txtSprayTime.Text = "";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }


    }
}
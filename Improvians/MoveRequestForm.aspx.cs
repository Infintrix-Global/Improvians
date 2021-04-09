using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Evo
{
    public partial class MoveRequestForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                // BindFacility();
                BindGridPlantReady();
                BindSupervisorList();
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

        public void BindGridPlantReady()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@wo", "");
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@LoginId", Session["LoginID"].ToString());


            dt = objCommon.GetDataTable("SP_GetMoveRequestAssistantGrower", nv);



            gvMoveReq.DataSource = dt;
            gvMoveReq.DataBind();

            foreach (GridViewRow row in gvMoveReq.Rows)
            {
                var checkJob = (row.FindControl("lbljobID") as Label).Text;
                if (checkJob == JobCode)
                {
                    row.CssClass = "highlighted";
                }
            }

        }
        public void BindSupervisorList()
        {


            NameValueCollection nv = new NameValueCollection();
            DataTable dt = new DataTable();

            if (Session["Role"].ToString() == "12")
            {

                dt = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
            }
            else if (Session["Role"].ToString() == "2")
            {
                dt = objCommon.GetDataTable("SP_GetRoleForSupervisor", nv);
            }
            else
            {

            }
            ddlLogisticManager.DataSource = dt;
            //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            ddlLogisticManager.DataTextField = "EmployeeName";
            ddlLogisticManager.DataValueField = "ID";
            ddlLogisticManager.DataBind();
            ddlLogisticManager.Items.Insert(0, new ListItem("--Select--", "0"));
            //}
            //if (Session["Role"].ToString() == "12")
            //{
            //    ddlLogisticManager.DataSource = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
            //    //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
            //    ddlLogisticManager.DataTextField = "EmployeeName";
            //    ddlLogisticManager.DataValueField = "ID";
            //    ddlLogisticManager.DataBind();
            //    ddlLogisticManager.Items.Insert(0, new ListItem("--Select--", "0"));
            //  }
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


        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady();
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady();
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridPlantReady();
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();
            BindFacility();
            BindGridPlantReady();
        }

        public void BindFacility()
        {
            ddlToFacility.DataSource = objBAL.GetMainLocation();
            ddlToFacility.DataTextField = "l1";
            ddlToFacility.DataValueField = "l1";
            ddlToFacility.DataBind();
            ddlToFacility.Items.Insert(0, new ListItem("--- Select ---", "0"));
            ddlToFacility.SelectedValue = Session["Facility"].ToString();
            BindBench_Location();
        }

        public void BindBench_Location()
        {
            //  nv.Add("@FacilityID", ddlToFacility.SelectedValue);
            ddlToGreenHouse.DataSource = objBAL.GetLocation(ddlToFacility.SelectedValue);
            ddlToGreenHouse.DataTextField = "p2";
            ddlToGreenHouse.DataValueField = "p2";
            ddlToGreenHouse.DataBind();
            ddlToGreenHouse.Items.Insert(0, new ListItem("--- Select ---", "0"));


        }



        protected void ddlToFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBench_Location();
        }


        protected void gvMoveReq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                BindFacility();

                userinput.Visible = true;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                HiddenFieldDid.Value = gvMoveReq.DataKeys[rowIndex].Values[1].ToString();
                HiddenFieldJid.Value = gvMoveReq.DataKeys[rowIndex].Values[2].ToString();
                lblJobID.Text = gvMoveReq.DataKeys[rowIndex].Values[3].ToString();
                lblBenchlocation.Text = gvMoveReq.DataKeys[rowIndex].Values[4].ToString();
                lblTotalTrays.Text = gvMoveReq.DataKeys[rowIndex].Values[5].ToString();
                lblDescription.Text = gvMoveReq.DataKeys[rowIndex].Values[6].ToString();
                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@MoveId", HiddenFieldDid.Value);

                dt = objCommon.GetDataTable("SP_GetMOVERequestView", nv);

                if (dt != null & dt.Rows.Count > 0)
                {
                    BindFacility();
                    txtMoveComments.Text = dt.Rows[0]["Comments"].ToString();
                    txtMoveNumberOfTrays.Text = dt.Rows[0]["TraysRequest"].ToString();
                    txtMoveDate.Text = Convert.ToDateTime(dt.Rows[0]["MoveDate"]).ToString("yyyy-MM-dd");
                    ddlToFacility.SelectedValue = dt.Rows[0]["FacilityTo"].ToString();
                    ddlToGreenHouse.SelectedValue = dt.Rows[0]["GrenHouseToRequest"].ToString();
                }
                ddlLogisticManager.Focus();

            }

            if (e.CommandName == "StartDump")
            {
                string ChId = "";
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string Did = gvMoveReq.DataKeys[rowIndex].Values[1].ToString();
                //  ChId = gvDump.DataKeys[rowIndex].Values[1].ToString();

                if (ChId == "")
                {
                    ChId = "0";
                }
                else
                {
                    ChId = ChId;
                }


                NameValueCollection nv = new NameValueCollection();
                nv.Add("@MoveDate", "");
                nv.Add("@Comments", "");
                nv.Add("@QuantityOfTray", "");
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@MoveID", Did);

                nv.Add("@OperatorID", Session["LoginID"].ToString());


                long result = objCommon.GetDataExecuteScaler("SP_AddMoveTaskAssignment", nv);


                if (result > 0)
                {
                    Response.Redirect(String.Format("~/MoveTaskCompletion.aspx?Did={0}&Chid={1}&DrId={2}", result, ChId, Did));
                }
            }

        }


        protected void btnMoveSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            //  nv.Add("@SupervisorID", ddlSupervisor.SelectedValue);
            //  nv.Add("@GrowerPutAwayId", lblGrowerID.Text);
            // nv.Add("@WO",wo);

            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Aid", ddlLogisticManager.SelectedValue);
            dt = objCommon.GetDataTable("spGeEmployeeRoleDetails", nv1);



            nv.Add("@SupervisorID", ddlLogisticManager.SelectedValue);
            nv.Add("@MoveNumberOfTrays", txtMoveNumberOfTrays.Text);

            nv.Add("@FromFacility", Session["LoginID"].ToString());
            nv.Add("@GrowerPutAwayID", "0");
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@ToFacility", ddlToFacility.SelectedValue);
            nv.Add("@ToGreenHouse", ddlToGreenHouse.SelectedValue);

            nv.Add("@MoveDate", txtMoveDate.Text);
            nv.Add("@Comments", txtMoveComments.Text);
            nv.Add("@mvoeId", HiddenFieldDid.Value);
            nv.Add("@RoleId", dt.Rows[0]["RoleID"].ToString());
            nv.Add("@ManualID", HiddenFieldJid.Value);






            result = objCommon.GetDataInsertORUpdate("SP_AddMoveRequestASManua", nv);


            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);


                string url = "";
                if (Session["Role"].ToString() == "1")
                {
                    url = "MyTaskGrower.aspx";
                }
                else
                {
                    url = "MyTaskAssistantGrower.aspx";
                }

                string message = "Assignment Successful";
                //   string url = "MyTaskAssistantGrower.aspx";
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment not Successful')", true);
                //  lblmsg.Text = "Assignment Not Successful";
            }
        }

        public void clear()
        {
            //  ddlSupervisor.SelectedIndex = 0;
        }

        protected void MoveReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskAssistantGrower.aspx");
        }

        protected void gvMoveReq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMoveReq.PageIndex = e.NewPageIndex;
            BindGridPlantReady();
        }
    }

}
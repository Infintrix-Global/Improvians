using Evo.Bal;
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
    public partial class MoveCompletionStart : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Did"] != null)
                {
                    Did = Request.QueryString["Did"].ToString();
                }

                BindFacility();
                BindBench_Location();
                BindPlantReady();
                binviewMoveDetails(Did);

            }
        }

        public void BindFacility()
        {
            ddlToFacility.DataSource = objBAL.GetMainLocation();
            ddlToFacility.DataTextField = "Facility";
            ddlToFacility.DataValueField = "Facility";
            ddlToFacility.DataBind();
            ddlToFacility.Items.Insert(0, new ListItem("--- Select ---", "0"));
            ddlToFacility.SelectedValue = Session["Facility"].ToString();
            BindBench_Location();
        }

        public void BindBench_Location()
        {
            //  nv.Add("@FacilityID", ddlToFacility.SelectedValue);
            ddlToGreenHouse.DataSource = objBAL.GetLocation(ddlToFacility.SelectedValue);
            ddlToGreenHouse.DataTextField = "BenchName";
            ddlToGreenHouse.DataValueField = "BenchName";
            ddlToGreenHouse.DataBind();
            ddlToGreenHouse.Items.Insert(0, new ListItem("--- Select ---", "0"));


        }

        protected void ddlToFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBench_Location();
        }


        public void binviewMoveDetails(string Mid)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@MoveId", Mid);

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
      

        public void BindPlantReady()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
        
            nv.Add("@MoveTaskAssignmentId", Did);
        
            dt = objCommon.GetDataTable("SP_GetOperatorMoveTaskDetailsStart", nv);
            gvPlantReady.DataSource = dt;
            gvPlantReady.DataBind();

        }

        protected void gvPlantReady_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlantReady.PageIndex = e.NewPageIndex;
            BindPlantReady();
        }


        protected void btnMoveSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
        

            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Aid", Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("spGeEmployeeRoleDetails", nv1);



            nv.Add("@SupervisorID", Session["LoginID"].ToString());
            nv.Add("@MoveNumberOfTrays", txtMoveNumberOfTrays.Text);

            nv.Add("@FromFacility", Session["LoginID"].ToString());
            nv.Add("@GrowerPutAwayID", "0");
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@ToFacility", ddlToFacility.SelectedValue);
            nv.Add("@ToGreenHouse", ddlToGreenHouse.SelectedValue);

            nv.Add("@MoveDate", txtMoveDate.Text);
            nv.Add("@Comments", txtMoveComments.Text);
            nv.Add("@mvoeId", Did);
            nv.Add("@RoleId", dt.Rows[0]["RoleID"].ToString());
            nv.Add("@ManualID", HiddenFieldJid.Value);

            result = objCommon.GetDataInsertORUpdate("SP_AddMoveRequestASManuaStart", nv);

         

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

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class MoveReqAsssignment : System.Web.UI.Page
    {
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");

                BindGridMoveReq();
            }
        }

        public void BindGridMoveReq()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@JobCode", ddlJobNo.SelectedValue);
            //nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            //nv.Add("@Facility", ddlFacility.SelectedValue);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            //nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            //nv.Add("@FertilizationCode","0");
            dt = objCommon.GetDataTable("SP_GetSupervisorMoveDetails", nv);
            gvFer.DataSource = dt;
            gvFer.DataBind();

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
           
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
            nv.Add("@MoveID", lblMoveID.Text);
            nv.Add("@MoveDate", txtSprayDate.Text.Trim());
            nv.Add("@Nots", txtNotes.Text.Trim());
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataInsertORUpdate("SP_AddMoveReqAssignment", nv);
            if (result > 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assignment Successful')", true);
                string message = "Completion Successful";
                string url = "MyTaskGreenSupervisorFinal.aspx";
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

        protected void gvFer_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                //   int rowIndex = Convert.ToInt32(e.CommandArgument);
                string MoveID = e.CommandArgument.ToString(); // gvFer.DataKeys[rowIndex].Values[0].ToString();
              
                userinput.Visible = true;


                lblMoveID.Text = MoveID;

                txtNotes.Focus();

            }
        }
    }
}
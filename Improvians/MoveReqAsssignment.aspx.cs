using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
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

                BindGridMoveReq(0);
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

        private string benchLoc
        {
            get
            {
                if (Request.QueryString["benchLoc"] != null)
                {
                    return Request.QueryString["benchLoc"].ToString();
                }
                return "";
            }
            set
            {
                // JobCode = Request.QueryString["jobId"].ToString();
                // JobCode = value;
            }
        }


        public void BindGridMoveReq(int p)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            //nv.Add("@JobCode", ddlJobNo.SelectedValue);
            //nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            //nv.Add("@Facility", ddlFacility.SelectedValue);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            //nv.Add("@BenchLocation", ddlBenchLocation.SelectedValue);
            nv.Add("@FacilityID", Session["Facility"].ToString());
            dt = objCommon.GetDataTable("SP_GetSupervisorMoveDetails", nv);
            gvFer.DataSource = dt;
            gvFer.DataBind();

            //foreach (GridViewRow row in gvFer.Rows)
            //{
            //    var checkJob = (row.FindControl("lblID") as Label).Text;
            //    if (checkJob == JobCode)
            //    {
            //        row.CssClass = "highlighted";
            //    }
            //}

            if (p != 1)
            {
                highlight();
            }
        }
        private void highlight()
        {
            var i = gvFer.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvFer.Rows)
            {
                var checkJob = (row.FindControl("lblID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouse") as Label).Text;
                i--;
                if (checkJob == JobCode && checklocation == benchLoc)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check)
                {
                    gvFer.PageIndex++;
                    gvFer.DataBind();
                    highlight();
                }
            }
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

                // userinput.Visible = true;
                //Response.Redirect(String.Format("~/MoveTaskCompletion.aspx?Did={0}&Chid={1}", MoveID, 0));

                //lblMoveID.Text = MoveID;

                //txtNotes.Focus();
                //DataTable dt = new DataTable();
                //NameValueCollection nv = new NameValueCollection();

                ////nv.Add("@MID", MoveID);
                ////dt = objCommon.GetDataTable("SP_GetSupervisorMoveViewDetails", nv);
                ////GridMoveDetails.DataSource = dt;
                ////GridMoveDetails.DataBind();
                ///
                string ChId = "";
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
                nv.Add("@MoveID", MoveID);

                nv.Add("@OperatorID", Session["LoginID"].ToString());



                long result = objCommon.GetDataExecuteScaler("SP_AddMoveTaskAssignment", nv);


                if (result > 0)
                {
                    Response.Redirect(String.Format("~/MoveTaskCompletion.aspx?Did={0}&Chid={1}&DrId={2}", result, ChId, MoveID));
                }
            }
        }
    }
}
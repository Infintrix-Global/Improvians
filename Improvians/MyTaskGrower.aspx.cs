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
    public partial class MyTask1 : System.Web.UI.Page
    {
        clsCommonMasters objCommon = new clsCommonMasters();
        CommonControl objCommonControl = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  BindDepartment();

                if (!IsPostBack)
                {
                    CountTotal();
                }
            }
        }
        //public void BindDepartment()
        //{
        //    ddlDept.DataSource = objCommon.GetDepartmentMaster();
        //    ddlDept.DataTextField = "DepartmentName";
        //    ddlDept.DataValueField = "DepartmentID";
        //    ddlDept.DataBind();
        //    ddlDept.Items.Insert(0, new ListItem("--- Select ---", "0"));
        //}

        public void CountTotal()
        {
            DataSet dt = new DataSet();
            NameValueCollection nv = new NameValueCollection();
           
            dt = objCommonControl.GetDataSet("SP_GetGrowerEachTaskCount", nv);
            lnkPutAway.Text = dt.Tables[0].Rows.Count.ToString();
            lnkGerm.Text = dt.Tables[1].Rows.Count.ToString();
            lnkFer.Text = dt.Tables[2].Rows.Count.ToString();
            lnkIrr.Text = dt.Tables[3].Rows.Count.ToString();
            lnkpr.Text = dt.Tables[4].Rows.Count.ToString();
            lnkMove.Text = dt.Tables[5].Rows.Count.ToString();
        }

        protected void ddlTaskRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlTaskRequest.SelectedValue=="4")
            {
                Response.Redirect("~/GerminationRequestForm.aspx");
            }
            if (ddlTaskRequest.SelectedValue == "5")
            {
                Response.Redirect("~/MoveForm.aspx");
            }
            if (ddlTaskRequest.SelectedValue == "6")
            {
                // Response.Redirect("~/PutAwayTaskCompletion.aspx");
                Response.Redirect("~/GrowerPutAwayForm.aspx");
            }
            if (ddlTaskRequest.SelectedValue == "7")
            {
                Response.Redirect("~/PlantReadyRequestForm.aspx");
            }
            if (ddlTaskRequest.SelectedValue == "8")
            {
                Response.Redirect("~/FertilizerTaskReq.aspx");
            }
            if (ddlTaskRequest.SelectedValue == "2")
            {
                Response.Redirect("~/Seeding_Plan_Form.aspx");
            }
            if (ddlTaskRequest.SelectedValue == "9")
            {
                Response.Redirect("~/IrrigationRequestForm.aspx");
            }
        }
    }
}
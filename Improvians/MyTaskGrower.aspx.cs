using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class MyTask1 : System.Web.UI.Page
    {
        clsCommonMasters objCommon = new clsCommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDepartment();
            }
        }
        public void BindDepartment()
        {
            ddlDept.DataSource = objCommon.GetDepartmentMaster();
            ddlDept.DataTextField = "DepartmentName";
            ddlDept.DataValueField = "DepartmentID";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("--- Select ---", "0"));
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
                Response.Redirect("~/PutAwayTaskCompletion.aspx");
            }
            if (ddlTaskRequest.SelectedValue == "7")
            {
                Response.Redirect("~/PlantReadyRequestForm.aspx");
            }
        }
    }
}
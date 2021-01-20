using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Improvians.Admin.BAL_Classes;

namespace Improvians.Admin
{ 
    public partial class ViewEmployee : System.Web.UI.Page
    {
        General objGeneral = new General();
        clsCommonMasters objCommon = new clsCommonMasters();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDepartment();
                BindRole();
                GetEmployeeList();
            }
        }
        public void GetEmployeeList()
        {
            DataTable dt = new DataTable();
            dt = objCommon.GetAllEmployeeList();
            GridEmployee.DataSource = dt;
            GridEmployee.DataBind();
            count.Text = "Number of Employees =" + dt.Rows.Count;
            ViewState["dirState"] = dt;
            ViewState["sortdr"] = "Asc";
        }

        public void BindDepartment()
        {
            ddlDepartment.DataSource = objCommon.GetDepartmentMaster();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindRole()
        {
            ddlDesignation.DataSource = objCommon.GetRoleMaster();
            ddlDesignation.DataTextField = "RoleName";
            ddlDesignation.DataValueField = "RoleID";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }


        protected void GridEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditProfile")

            {
                int eid = Convert.ToInt32(e.CommandArgument);
                Session["EmployeeID"] = eid;
                Response.Redirect("~/EditProfile.aspx");
            }

            if (e.CommandName == "RemoveProfile")

            {

                int eid = Convert.ToInt32(e.CommandArgument);
                Session["EmployeeID"] = eid;
                objCommon.RemoveEmployee(eid);
                GetEmployeeList();
            }
        }

        protected void GridEmployee_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)ViewState["dirState"];
            if (dtrslt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["sortdr"] = "Desc";
                }
                else
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["sortdr"] = "Asc";
                }
                GridEmployee.DataSource = dtrslt;
                GridEmployee.DataBind();
            }
        }

        protected void GridEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridEmployee.PageIndex = e.NewPageIndex;
            GetEmployeeList();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dtSearch1;
            string sqr = "Select * from Login L inner Join Department D on D.DepartmentID=L.DepartmentID inner join Role R on L.RoleID=R.RoleID where L.IsActive=1";
            if (txtName.Text != "")
            {
                sqr += "and L.EmployeeName like '%' +'" + txtName.Text + "'+ '%'";
            }
            if (txtMobile.Text != "")
            {
                sqr += "and L.Mobile like '%' + '" + txtMobile.Text + "'+ '%'";
            }
            if (ddlDepartment.SelectedIndex !=0)
            {
                sqr += "and L.DepartmentID =" + ddlDepartment.SelectedValue ;
            }
            if (ddlDesignation.SelectedIndex != 0)
            {
                sqr += "and L.RoleID =" + ddlDesignation.SelectedValue;
            }

            dtSearch1 = objGeneral.GetDatasetByCommand(sqr);
            GridFillSearch();

            void GridFillSearch()
            {
                if (dtSearch1 != null)
                {
                    //DataTable dtSearch = dtSearch1.CopyToDataTable();
                    GridEmployee.DataSource = dtSearch1;
                    GridEmployee.DataBind();

                    count.Text = "Number of Employee= " + (dtSearch1.Rows.Count).ToString();
                }
                else
                {
                    DataTable dt = new DataTable();
                    GridEmployee.DataSource = dt;
                    GridEmployee.DataBind();

                    count.Text = "Number of Employee= 0";
                }
                ViewState["dirState"] = dtSearch1;
                ViewState["sortdr"] = "Asc";


            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtMobile.Text = "";
            ddlDepartment.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
        }
    }
}
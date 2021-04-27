using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Admin.BAL_Classes;

namespace Evo.Admin
{
    public partial class ViewCustomer : System.Web.UI.Page
    {
        General objGeneral = new General();
        clsCommonMasters objCommon = new clsCommonMasters();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetEmployeeList();
            }
        }
        public void GetEmployeeList()
        {
            DataTable dt = new DataTable();
            string sqr = "Select L.*,L1.EmployeeName as Sales from Login L left outer join CustomerSalesMapping C on L.ID=C.CustomerID  left outer join Login L1 on C.SalesID=L1.ID where L.IsActive=1 and L.RoleID=13";

            dt = objGeneral.GetDatasetByCommand(sqr);
            GridEmployee.DataSource = dt;
            GridEmployee.DataBind();
            if (dt == null)
                count.Text = "Number of Customers =0";
            else
                count.Text = "Number of Customers =" + dt.Rows.Count;
            ViewState["dirState"] = dt;
            ViewState["sortdr"] = "Asc";
        }



        protected void GridEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditProfile")

            {
                int eid = Convert.ToInt32(e.CommandArgument);
                Session["EmployeeID"] = eid;
                Response.Redirect("~/Admin/EditCustomer.aspx");
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
            string sqr = "Select L.*,L1.EmployeeName as Sales from Login L left outer join CustomerSalesMapping C on L.ID=C.CustomerID  left outer join Login L1 on C.SalesID=L1.ID where L.IsActive=1 and L.RoleID=13";
            if (txtName.Text != "")
            {
                sqr += "and L.EmployeeName like '%' +'" + txtName.Text + "'+ '%'";
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

                    count.Text = "Number of Customer= " + (dtSearch1.Rows.Count).ToString();
                }
                else
                {
                    DataTable dt = new DataTable();
                    GridEmployee.DataSource = dt;
                    GridEmployee.DataBind();

                    count.Text = "Number of Customer= 0";
                }
                ViewState["dirState"] = dtSearch1;
                ViewState["sortdr"] = "Asc";


            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";

        }
    }
}
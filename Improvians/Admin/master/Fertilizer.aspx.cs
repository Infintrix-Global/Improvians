using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Admin.BAL_Classes;
using Evo.Admin;

namespace Evo.Admin
{
    public partial class Fertilizer : System.Web.UI.Page
    {
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
            dt = objCommon.GetAllFertilizerList();
            gvFertilizer.DataSource = dt;
            gvFertilizer.DataBind();
            count.Text = "Number of Fertilizer =" + dt.Rows.Count;
            ViewState["dirState"] = dt;
            ViewState["sortdr"] = "Asc";
        }
        protected void btAdd_Click(object sender, EventArgs e)
        {

            try
            {
               
               
                    int _isInserted = -1;

                    FertilizerMaster obj = new FertilizerMaster()
                    {

                        FertilizerName = txtName.Text,
                        IsActive = chkIsActive.Checked,
                      
                    };

                    _isInserted = objCommon.InsertFertilzerMaster(obj);

                    if (_isInserted == -1)
                    {

                        //lblmsg.Text = "Failed to Add Fertilizer";
                        //lblmsg.ForeColor = System.Drawing.Color.Red;

                    }
                  
                    else
                    {

                       // lblmsg.Text = "Fertilizer Added ";
                    txtName.Text = "";
                    }
                
            }
            catch (Exception ex)
            {

            }
        }

        protected void GridEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditProfile")

            {
                int eid = Convert.ToInt32(e.CommandArgument);
                Session["EmployeeID"] = eid;
                Response.Redirect("~/Admin/EditProfile.aspx");
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
           
        }

        protected void GridEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFertilizer.PageIndex = e.NewPageIndex;
            GetEmployeeList();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            //txtMobile.Text = "";
            //ddlDepartment.SelectedIndex = 0;
           
        }
    }
}
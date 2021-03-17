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
    public partial class TaskTypeMaster : System.Web.UI.Page
    {
        clsCommonMasters objCommon = new clsCommonMasters();
        General objGeneral = new General();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTaskTypeList();
                
            }
        }
        public void bindTaskType(int eid)
        {
            try
            {
                DataSet dt1 = objTask.GetTaskTypeByID(eid);


                if (dt1.Tables[0].Rows.Count > 0)
                {
                    txtName.Text = dt1.Tables[0].Rows[0]["TaskType"].ToString();
                    
                   
                }
                
            }
            catch (Exception ex)
            {

            }
        }
        public void GetTaskTypeList()
        {
            DataTable dt = new DataTable();
            dt = objCommon.GetAllTaskTypeList();
            gvChemical.DataSource = dt;
            gvChemical.DataBind();
            count.Text = "Number of TaskType =" + dt.Rows.Count;
            ViewState["dirState"] = dt;
            ViewState["sortdr"] = "Asc";
        }
        protected void btAdd_Click(object sender, EventArgs e)
        {

            try
            {
               
               
                    int _isInserted = -1;

                    TaskTypeMasters obj = new TaskTypeMasters()
                    {

                        TaskType = txtName.Text,
                        IsActive = true,
                      
                    };
                if (Session["tasktypeId"] != null)
                {
                    string id = Session["tasktypeId"].ToString();
                    int x = Convert.ToInt32(id);
                    obj.id = x;
                    _isInserted = objCommon.UpdateTaskType(obj);

                }
                else
                {
                    _isInserted = objCommon.InsertTaskTypeMaster(obj);
                }

                    if (_isInserted == -1)
                    {

                        //lblmsg.Text = "Failed to Add Fertilizer";
                        //lblmsg.ForeColor = System.Drawing.Color.Red;

                    }
                  
                    else
                    {

                    // lblmsg.Text = "Fertilizer Added ";
                    pnlList.Visible = true;
                    pnlAdd.Visible = false;
                    GetTaskTypeList();
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
                Session["tasktypeId"] = eid;
                pnlAdd.Visible = true;
                pnlList.Visible = false;
                if (Session["tasktypeId"] != null)
                {
                    string id = Session["tasktypeId"].ToString();
                    int x = Convert.ToInt32(id);
                    btAdd.Text = "Update";
                    bindTaskType(x);
                }
            }

            if (e.CommandName == "RemoveProfile")

            {

                int eid = Convert.ToInt32(e.CommandArgument);
                Session["tasktypeId"] = eid;
                objCommon.RemoveChemical(eid);
                GetTaskTypeList();
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
                gvChemical.DataSource = dtrslt;
                gvChemical.DataBind();
            }
        }

        protected void GridEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvChemical.PageIndex = e.NewPageIndex;
            GetTaskTypeList();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dtSearch1;
            string sqr = "Select * FROM TaskTypeMaster WHERE ISActive = 1";
            if (txtSearchName.Text != "")
            {
                sqr += "and TaskType like '%' +'" + txtSearchName.Text + "'+ '%'";
            }
           

            dtSearch1 = objGeneral.GetDatasetByCommand(sqr);
            GridFillSearch();

            void GridFillSearch()
            {
                if (dtSearch1 != null)
                {
                    //DataTable dtSearch = dtSearch1.CopyToDataTable();
                    gvChemical.DataSource = dtSearch1;
                    gvChemical.DataBind();

                    count.Text = "Number of Tasktype= " + (dtSearch1.Rows.Count).ToString();
                }
                else
                {
                    DataTable dt = new DataTable();
                    gvChemical.DataSource = dt;
                    gvChemical.DataBind();

                    count.Text = "Number of Tasktype= 0";
                }
                ViewState["dirState"] = dtSearch1;
                ViewState["sortdr"] = "Asc";


            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtSearchName.Text = "";
            GetTaskTypeList();
            //txtMobile.Text = "";
            //ddlDepartment.SelectedIndex = 0;

        }

        protected void btnAddFertilizer_Click(object sender, EventArgs e)
        {
            pnlAdd.Visible = true;
            pnlList.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlAdd.Visible = false;
            pnlList.Visible = true;
        }
    }
}
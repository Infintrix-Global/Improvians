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
    public partial class CropHealthMaster : System.Web.UI.Page
    {
        clsCommonMasters objCommon = new clsCommonMasters();
        General objGeneral = new General();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCropHealthList();

            }
        }
        public void bindCropHealth(int eid)
        {
            try
            {
                DataSet dt1 = objTask.GetCropHealthByID(eid);


                if (dt1.Tables[0].Rows.Count > 0)
                {
                    txtProblemType.Text = dt1.Tables[0].Rows[0]["TypeOfProblem"].ToString();
                    txtProblemCause.Text = dt1.Tables[0].Rows[0]["CauseOfProblem"].ToString();
                }

            }
            catch (Exception ex)
            {

            }
        }
        public void GetCropHealthList()
        {
           
            DataTable dt = new DataTable();
            dt = objCommon.GetAllCropHealthList();
            gvCropHealth.DataSource = dt;
            gvCropHealth.DataBind();
            count.Text = "Number of Crop Health =" + dt.Rows.Count;
            ViewState["dirState"] = dt;
            ViewState["sortdr"] = "Asc";
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int _isInserted = -1;
                CropHealthMasters obj = new CropHealthMasters()
                {
                    TypeOfProblem = txtProblemType.Text,
                    CauseOfProblem = txtProblemCause.Text,
                    IsActive = true,
                };
                if (Session["CropHealthID"] != null)
                {
                    string id = Session["CropHealthID"].ToString();
                    int x = Convert.ToInt32(id);
                    obj.id = x;
                    _isInserted = objCommon.UpdateCropHealth(obj);
                }
                else
                {
                    _isInserted = objCommon.InsertCropHealthMaster(obj);
                }

                if (_isInserted == -1)
                {

                }

                else
                {
                    pnlList.Visible = true;
                    pnlAdd.Visible = false;
                    GetCropHealthList();
                    txtProblemType.Text = "";
                    txtProblemCause.Text = "";
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
                Session["CropHealthID"] = eid;
                pnlAdd.Visible = true;
                pnlList.Visible = false;
                if (Session["CropHealthID"] != null)
                {
                    string id = Session["CropHealthID"].ToString();
                    int x = Convert.ToInt32(id);
                    btAdd.Text = "Update";
                    bindCropHealth(x);
                }
            }

            if (e.CommandName == "RemoveProfile")
            {
                int eid = Convert.ToInt32(e.CommandArgument);
                Session["CropHealthID"] = eid;
                objCommon.RemoveCropHealth(eid);
                GetCropHealthList();
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
                gvCropHealth.DataSource = dtrslt;
                gvCropHealth.DataBind();
            }
        }

        protected void GridEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCropHealth.PageIndex = e.NewPageIndex;
            GetCropHealthList();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dtSearch1;
            string sqr = "Select * FROM CropHealthMaster WHERE ";
            if (txtSearchName.Text != "")
            {
                sqr += "TypeOfProblem like '%' +'" + txtSearchName.Text + "'+ '%'  or CauseOfProblem like '%' +'" + txtSearchName.Text + "'+ '%' AND IsActive = 1";
            }


            dtSearch1 = objGeneral.GetDatasetByCommand(sqr);
            GridFillSearch();

            void GridFillSearch()
            {
                if (dtSearch1 != null)
                {                  
                    gvCropHealth.DataSource = dtSearch1;
                    gvCropHealth.DataBind();

                    count.Text = "Number of Crop Health= " + (dtSearch1.Rows.Count).ToString();
                }
                else
                {
                    DataTable dt = new DataTable();
                    gvCropHealth.DataSource = dt;
                    gvCropHealth.DataBind();

                    count.Text = "Number of Crop Health= 0";
                }
                ViewState["dirState"] = dtSearch1;
                ViewState["sortdr"] = "Asc";


            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtProblemType.Text = "";
            txtProblemCause.Text = "";
            txtSearchName.Text = "";
            GetCropHealthList();

        }

        protected void btnAddCropHealth_Click(object sender, EventArgs e)
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
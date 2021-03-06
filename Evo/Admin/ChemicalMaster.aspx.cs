﻿using System;
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
    public partial class ChemicalMaster : System.Web.UI.Page
    {
        clsCommonMasters objCommon = new clsCommonMasters();
        General objGeneral = new General();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetChemicalList();
                
            }
        }
        public void bindChemical(int eid)
        {
            try
            {
                DataSet dt1 = objTask.GetChemicalByID(eid);


                if (dt1.Tables[0].Rows.Count > 0)
                {
                    txtName.Text = dt1.Tables[0].Rows[0]["ChemicalName"].ToString();
                    
                   
                }
                
            }
            catch (Exception ex)
            {

            }
        }
        public void GetChemicalList()
        {
            DataTable dt = new DataTable();
            dt = objCommon.GetAllChemicalList();
            gvChemical.DataSource = dt;
            gvChemical.DataBind();
            count.Text = "Number of Chemical =" + dt.Rows.Count;
            ViewState["dirState"] = dt;
            ViewState["sortdr"] = "Asc";
        }
        protected void btAdd_Click(object sender, EventArgs e)
        {

            try
            {
               
               
                    int _isInserted = -1;

                    ChemicalMasters obj = new ChemicalMasters()
                    {

                        ChemicalName = txtName.Text,
                        IsActive = true,
                      
                    };
                if (Session["chemicalID"] != null)
                {
                    string id = Session["chemicalID"].ToString();
                    int x = Convert.ToInt32(id);
                    obj.id = x;
                    _isInserted = objCommon.UpdateChemical(obj);

                }
                else
                {
                    _isInserted = objCommon.InsertChemicalMaster(obj);
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
                    GetChemicalList();
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
                Session["chemicalID"] = eid;
                pnlAdd.Visible = true;
                pnlList.Visible = false;
                if (Session["chemicalID"] != null)
                {
                    string id = Session["chemicalID"].ToString();
                    int x = Convert.ToInt32(id);
                    btAdd.Text = "Update";
                    bindChemical(x);
                }
            }

            if (e.CommandName == "RemoveProfile")

            {

                int eid = Convert.ToInt32(e.CommandArgument);
                Session["chemicalID"] = eid;
                objCommon.RemoveChemical(eid);
                GetChemicalList();
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
            GetChemicalList();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dtSearch1;
            string sqr = "Select * FROM ChemicalMaster WHERE ISActive = 1";
            if (txtSearchName.Text != "")
            {
                sqr += "and ChemicalName like '%' +'" + txtSearchName.Text + "'+ '%'";
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

                    count.Text = "Number of Chemical= " + (dtSearch1.Rows.Count).ToString();
                }
                else
                {
                    DataTable dt = new DataTable();
                    gvChemical.DataSource = dt;
                    gvChemical.DataBind();

                    count.Text = "Number of Chemical= 0";
                }
                ViewState["dirState"] = dtSearch1;
                ViewState["sortdr"] = "Asc";


            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtSearchName.Text = "";
            GetChemicalList();
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
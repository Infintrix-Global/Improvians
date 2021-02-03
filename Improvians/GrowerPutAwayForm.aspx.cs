using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Improvians
{
    public partial class GetGrowerPutAwayForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
              
                BindGridGerm();
            }
        }

        private string wo
        {
            get
            {
                if (ViewState["wo"] != null)
                {
                    return (string)ViewState["wo"];
                }
                return "";
            }
            set
            {
                ViewState["wo"] = value;
            }
        }

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Mode", "1");
            nv.Add("@wo", "");
            dt = objCommon.GetDataTable("SP_GetGrowerPutAway", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }



        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Assign")
            {

                invoiceSplitJob();
                PanelAdd.Visible = true;
                PanelList.Visible = false;
                string wo_No = e.CommandArgument.ToString();
                wo = wo_No;
                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@Mode", "2");
                nv.Add("@wo", wo_No);
                dt = objCommon.GetDataTable("SP_GetGrowerPutAway", nv);

                if(dt !=null && dt.Rows.Count >0)
                {
                    lblJobID.Text = dt.Rows[0]["jobcode"].ToString();
                    lblSeedDate.Text = dt.Rows[0]["plan_date"].ToString();
                    lblSeededTrays.Text = dt.Rows[0]["trays_actual"].ToString();
                }

                //int rowIndex = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = gvGerm.Rows[rowIndex];
                //string benchID = (row.FindControl("ddlBench") as DropDownList).SelectedValue;
                //string id = (row.FindControl("lblID") as Label).Text;
                //if (benchID == "0")
                //{
                //    lblmsg.Text = "Failed-Please Select Bench Location ";
                //    lblmsg.ForeColor = System.Drawing.Color.Red;
                //}
                //else
                //{
                //    long result = 0;
                //    NameValueCollection nv = new NameValueCollection();
                //    nv.Add("@PTCID", id);
                //    nv.Add("@BenchLocation", benchID);
                //    nv.Add("@LoginID", Session["LoginID"].ToString());
                //    result = objCommon.GetDataInsertORUpdate("SP_UpdateProductionPlannerCompletion", nv);
                //    string message = "Bench Location Assignment Successful";
                //    string url = "PutAwayTaskCompletion.aspx";
                //    string script = "window.onload = function(){ alert('";
                //    script += message;
                //    script += "');";
                //    script += "window.location = '";
                //    script += url;
                //    script += "'; }";
                //    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                //}
            }

       
        }


        private void invoiceSplitJob()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
           

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
         


            dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //Store the DataTable in ViewState
            ViewState["CurrentTableGridDrowers"] = dt;

            GridSplitJob.DataSource = dt;
            GridSplitJob.DataBind();
        }

        private void AddNewRowToGridSetInitialInvoice()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTableGridDrowers"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableGridDrowers"];


                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        DropDownList box1 = (DropDownList)GridSplitJob.Rows[rowIndex].Cells[1].FindControl("ddlMain");
                        DropDownList box2 = (DropDownList)GridSplitJob.Rows[rowIndex].Cells[2].FindControl("ddlLocation");
                        TextBox box3 = (TextBox)GridSplitJob.Rows[rowIndex].Cells[3].FindControl("txtTrays");
                       


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
                        dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;
                      
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTableGridDrowers"] = dtCurrentTable;

                    GridSplitJob.DataSource = dtCurrentTable;
                    GridSplitJob.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousDataSetInitialinvoide();
        }

        private void SetPreviousDataSetInitialinvoide()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTableGridDrowers"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTableGridDrowers"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList box1 = (DropDownList)GridSplitJob.Rows[rowIndex].Cells[1].FindControl("ddlMain");
                        DropDownList box2 = (DropDownList)GridSplitJob.Rows[rowIndex].Cells[2].FindControl("ddlLocation");
                        TextBox box3 = (TextBox)GridSplitJob.Rows[rowIndex].Cells[3].FindControl("txtTrays");
                      

                        box1.Text = dt.Rows[i]["Column1"].ToString();
                        box2.Text = dt.Rows[i]["Column2"].ToString();
                        box3.Text = dt.Rows[i]["Column3"].ToString();
                       


                        rowIndex++;
                    }
                }
            }
        }
        protected void ButtonAddGridInvoice_Click(object sender, EventArgs e)
        {
            AddNewRowToGridSetInitialInvoice();
        }

        protected void GridSplitJob_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlMain = (DropDownList)e.Row.FindControl("ddlMain");
                DropDownList ddlLocation = (DropDownList)e.Row.FindControl("ddlLocation");
              
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@mode","4");
                ddlMain.DataSource = objCommon.GetDataTable("GET_Common", nv); ;
                ddlMain.DataTextField = "FacilityName";
                ddlMain.DataValueField = "FacilityID";
                ddlMain.DataBind();
                ddlMain.Items.Insert(0, new ListItem("--- Select ---", "0"));

                BindLocation();
            }
        }

        protected void ddlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLocation();
        }

        public void BindLocation()
        {

            foreach (GridViewRow row in GridSplitJob.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    DropDownList ddlLocation = (row.Cells[0].FindControl("ddlLocation") as DropDownList);
                    DropDownList ddlMain = (row.Cells[0].FindControl("ddlMain") as DropDownList);

                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@FacilityID", ddlMain.SelectedValue);
                    ddlLocation.DataSource = objCommon.GetDataTable("SP_GetGreenhouseByFacility", nv); ;
                    ddlLocation.DataTextField = "GreenHouseName";
                    ddlLocation.DataValueField = "GreenHouseID";
                    ddlLocation.DataBind();
                    ddlLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
                }
            }
        }
        protected void txtTrays_TextChanged(object sender, EventArgs e)
        {
            CalData();
        }

        public void CalData()
        {
          
            int Total = 0;
           


            foreach (GridViewRow row in GridSplitJob.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    TextBox txtTrays = (row.Cells[0].FindControl("txtTrays") as TextBox);


                    //  TexTotal = Total * Convert.ToInt32(ddlTAX.SelectedItem.Text) / 100;
                    if (txtTrays.Text != "")
                    {
                        Total += Convert.ToInt32(txtTrays.Text);
                    }

                }
            }


          //  lblTotalCost.Text = TotalCost1.ToString();
          
          
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            invoiceSplitJob();
            BindGridGerm();
            PanelAdd.Visible = false;
            PanelList.Visible = true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

     
    }
}
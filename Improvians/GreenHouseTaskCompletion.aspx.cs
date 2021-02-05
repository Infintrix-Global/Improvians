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
    public partial class GreenHouseTaskCompletion : System.Web.UI.Page
    {
        string wo;
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["GTAID"] != null)
                {
                    gtaID = Request.QueryString["GTAID"].ToString();
                }
                BindGridGerm();

            }
        }

        private string gtaID
        {
            get
            {
                if (ViewState["gtaID"] != null)
                {
                    return (string)ViewState["gtaID"];
                }
                return "";
            }
            set
            {
                ViewState["gtaID"] = value;
            }
        }

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@GTAID", gtaID);
            dt = objCommon.GetDataTable("SP_GetGreenHouseOperatorGerminationTaskByGTAID", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();
            if (dt != null && dt.Rows.Count > 0)
            {
                wo = dt.Rows[0]["wo"].ToString();
                lblSeedlot.Text = dt.Rows[0]["TraySize"].ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@InspectionDate", txtInspectionDate.Text);
            nv.Add("@GTAID", gtaID);
            nv.Add("@#TraysInspected", txtTrays.Text);
            nv.Add("@Germination", lblGerm.Text);
            nv.Add("@WorkOrderID", wo);
            nv.Add("@#BadPlants", lblbadplants.Text);
            nv.Add("@GermVigor", lblgermvigor.Text);
           // nv.Add("@GermHealth", lblcrophealth.Text);
           // nv.Add("@JobID", Session["JobID"].ToString());
            nv.Add("@LoginID", Session["LoginID"].ToString());
            result = objCommon.GetDataInsertORUpdate("SP_AddGerminationCompletion", nv);
            if (result > 0)
            {
               // lblmsg.Text = "Completion Successful";
                clear();
                string message = "Completion Successful";
                string url;
                if (Session["Role"].ToString() == "3")
                {
                     url = "MyTaskGreenOperatorFinal.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }

                else if (Session["Role"].ToString() == "2")
                {
                     url = "MyTaskGreenSupervisorFinal.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
               
               
               
            }
            else
            {
                lblmsg.Text = "Completion Not Successful";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            if (Session["Role"].ToString() == "3")
            {
                Response.Redirect("~/MyTaskGreenOperatorFinal.aspx");
            }

            else if (Session["Role"].ToString() == "2")
            {
                Response.Redirect("~/MyTaskGreenSupervisorFinal.aspx");
            }

        }

        public void clear()
        {
            txtTrays.Text = "";
            txtInspectionDate.Text = "";

        }

        protected void txtTrays_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtTrays.Text) <= 20)
            { 
                sbtTray.Visible = true;
                // Current row count.
                int rowCtr;
                // Total number of cells per row (columns).
                int cellCtr;
                // Current cell counter
                int cellCnt = Convert.ToInt32(txtTrays.Text);
                TableRow tRow;
                for (rowCtr = 0; rowCtr < 2; rowCtr++)
                {
                    // Create new row and add it to the table.
                    tRow = new TableRow();
                    tbltray.Rows.Add(tRow);
                    for (cellCtr = 0; cellCtr <= cellCnt; cellCtr++)
                    {
                        if (cellCtr == 0 && rowCtr == 0)
                        {
                            TableCell tCell1 = new TableCell();
                            tCell1.Text = "Tray #";
                            tRow.Cells.Add(tCell1);
                        }

                        else if (cellCtr == 0 && rowCtr == 1)
                        {
                            TableCell tCell1 = new TableCell();
                            tCell1.Text = "Bad Plants #";
                            tRow.Cells.Add(tCell1);
                        }
                        else if (cellCtr != 0 && rowCtr == 0)
                        {

                            // Create a new cell and add it to the row.
                            TableCell tCell = new TableCell();
                           
                            tCell.Text = cellCtr.ToString();
                            tRow.Cells.Add(tCell);
                        }
                        else
                        {

                            // Create a new cell and add it to the row.
                            TableCell tCell = new TableCell();
                            TextBox tb = new TextBox();
                            tb.Width = 50;
                            // Set a unique ID for each TextBox added
                            tb.ID = "TextBoxRow_" + rowCtr + "Col_" + cellCtr;
                            // Add the control to the TableCell
                            tCell.Controls.Add(tb);
                            tRow.Cells.Add(tCell);
                        }
                    }
                }
            }
        }

        protected void sbtTray_Click(object sender, EventArgs e)
        {
            lblJobid.Text = Session["JobID"].ToString();
            lblnotrays.Text = txtTrays.Text;

            Table table = (Table)Page.FindControl("tbltray");
            int count = 0;
            for (int j = 1; j < int.Parse(txtTrays.Text) + 1; j++)
            {
                //Print the values entered

                if (!string.IsNullOrEmpty(Request.Form["ctl00$ContentPlaceHolder1$TextBoxRow_1" + "Col_" + j]))
                {
                    count += int.Parse(Request.Form["ctl00$ContentPlaceHolder1$TextBoxRow_1" + "Col_" + j]);
                    //Response.Write(Request.Form["TextBoxRow_" + i + "Col_" + j] + "<BR/>");
                }
            }
            lblbadplants.Text = count.ToString();
            Decimal germ = Convert.ToDecimal(count) / (Convert.ToDecimal(lblSeedlot.Text) * Convert.ToDecimal(txtTrays.Text));
            lblGerm.Text = ((1 - germ) * 100).ToString();
           // lblgermvigor= (RoundUp((100 - (Convert.ToInt32(lblbadplants.Text) / (Convert.ToInt32(txtTrays.Text) * E38 * germ) * 100)), 0))
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Improvians.Bal;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;


namespace Improvians
{
    public partial class Seeding_Plan_Form : System.Web.UI.Page
    {
        Bal_SeedingPlan objSP = new Bal_SeedingPlan();
        CommonControl objCommon = new CommonControl();
        public static DataTable AllData = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Fdate = "", TDate = "";
                Fdate = Convert.ToDateTime(System.DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd");
                TDate = (Convert.ToDateTime(System.DateTime.Now)).AddDays(10).ToString("yyyy-MM-dd");
                //    Fdate = DateAdd(DateInterval.Day, -7, Now.Date);
                //  TDate = DateAdd(DateInterval.Day, 10, Now.Date);
                txtFromDate.Text = Fdate;
                txtToDate.Text = TDate;
                BindItem();
                BindSeedlineLocation();
                BindTraySize();
                BindSeedAllocation();

                getDataDGJob();
            }
        }

        public void BindSeedlineLocation()
        {
            ddlSeedlineLocation.DataSource = objSP.GetSeedlineLocation(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
            ddlSeedlineLocation.DataTextField = "loc";
            ddlSeedlineLocation.DataValueField = "loc";
            ddlSeedlineLocation.DataBind();
            ddlSeedlineLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        public void BindSeedAllocation()
        {
            ddlSeedAllocated.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            ddlSeedAllocated.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Yes", "1"));
            ddlSeedAllocated.Items.Insert(2, new System.Web.UI.WebControls.ListItem("No", "9"));
        }
        public void BindItem()
        {
            ddlItem.DataSource = objSP.GetItems(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
            ddlItem.DataTextField = "itmdescp";
            ddlItem.DataValueField = "itmdescp";
            ddlItem.DataBind();
            ddlItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        public void BindTraySize()
        {
            ddlTraySize.DataSource = objSP.GetTraysize(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
            ddlTraySize.DataTextField = "ts";
            ddlTraySize.DataValueField = "ts";
            ddlTraySize.DataBind();
            ddlTraySize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        public void getDataDGJob()
        {
            AllData = objSP.GetDataSeedingPlan(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), ddlSeedlineLocation.SelectedValue, ddlItem.SelectedValue, ddlSeedAllocated.SelectedValue, ddlTraySize.SelectedValue);

            if (AllData != null && AllData.Rows.Count > 0)
            {
                DataTable dt11 = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@LoginID", "");
                nv.Add("@mode", "10");
                dt11 = objCommon.GetDataTable("SP_GetGreenHouseLogisticTask", nv);
                DataTable dtOnlyLeftWO = new DataTable();
                if (dt11 != null && dt11.Rows.Count > 0)
                {
                   var rows  = (from a in AllData.AsEnumerable()
                                    join b in dt11.AsEnumerable()
                                    on a["WO"].ToString() equals b["WO"].ToString()
                                    into g
                                    where g.Count() == 0
                                    select a);
                    if (rows.Any())
                        dtOnlyLeftWO = rows.CopyToDataTable();

                }
                else
                {
                    dtOnlyLeftWO = AllData;
                }
                lblTotal.Text = dtOnlyLeftWO.Rows.Count.ToString() + " Records";
                DGJob.DataSource = dtOnlyLeftWO;
                DGJob.DataBind();
            }
            else
            {
                DGJob.DataSource = null;
                DGJob.DataBind();
            }

        }


        protected void DGJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGJob.PageIndex = e.NewPageIndex;
            getDataDGJob();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getDataDGJob();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void BtnPrint_Click(object sender, EventArgs e)
        {

        }

        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, BaseColor color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }
        public void ExportToPdf(DataTable myDataTable)
        {
            DataTable dt = myDataTable;
            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            Font font13 = FontFactory.GetFont("ARIAL", 13);
            Font font18 = FontFactory.GetFont("ARIAL", 18);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                if (dt.Rows.Count > 0)
                {
                    PdfPTable PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = 200f;
                    PdfTable.LockedWidth = true;

                    PdfPCell PdfPCell = new PdfPCell(new Phrase(new Chunk("Employee Details", font18)));
                    PdfPCell.Border = Rectangle.NO_BORDER;
                    PdfTable.AddCell(PdfPCell);
                    DrawLine(writer, 25f, pdfDoc.Top - 30f, pdfDoc.PageSize.Width - 25f, pdfDoc.Top - 30f, new BaseColor(System.Drawing.Color.Red));
                    pdfDoc.Add(PdfTable);

                    PdfTable = new PdfPTable(dt.Columns.Count);
                    PdfTable.SpacingBefore = 20f;
                    for (int columns = 0; columns <= dt.Columns.Count - 1; columns++)
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Columns[columns].ColumnName, font18)));
                        PdfTable.AddCell(PdfPCell);
                    }

                    for (int rows = 0; rows <= dt.Rows.Count - 1; rows++)
                    {
                        for (int column = 0; column <= dt.Columns.Count - 1; column++)
                        {
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Rows[rows][column].ToString(), font13)));
                            PdfTable.AddCell(PdfPCell);
                        }
                    }
                    pdfDoc.Add(PdfTable);
                }
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename=dsejReport_" + DateTime.Now.Date.Day.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Hour.ToString() + DateTime.Now.Date.Minute.ToString() + DateTime.Now.Date.Second.ToString() + DateTime.Now.Date.Millisecond.ToString() + ".pdf");
                System.Web.HttpContext.Current.Response.Write(pdfDoc);
                Response.Flush();
                Response.End();
            }
            catch (DocumentException de)
            {
            }
            // System.Web.HttpContext.Current.Response.Write(de.Message)
            catch (IOException ioEx)
            {
            }
            // System.Web.HttpContext.Current.Response.Write(ioEx.Message)
            catch (Exception ex)
            {
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                long _isInserted = 1;
                int SelectedItems = 0;



                foreach (GridViewRow item in DGJob.Rows)
                {

                    if (item.RowType == DataControlRowType.DataRow)
                    {


                        Label lblAllocated = (item.Cells[0].FindControl("lblAllocated") as Label);
                        // Label lblSeedline = (item.Cells[0].FindControl("lblSeedline") as Label);
                        Label lbljobcode = (item.Cells[0].FindControl("lbljobcode") as Label);
                        Label lblItem = (item.Cells[0].FindControl("lblItem") as Label);
                        Label lblCustName = (item.Cells[0].FindControl("lblCustName") as Label);
                        Label lblSODate = (item.Cells[0].FindControl("lblSODate") as Label);
                        Label lblSOTrays = (item.Cells[0].FindControl("lblSOTrays") as Label);
                        Label lblTraySize = (item.Cells[0].FindControl("lblTraySize") as Label);
                        TextBox Txtgtrays = (item.Cells[0].FindControl("Txtgtrays") as TextBox);
                        TextBox Txtgplantdt = (item.Cells[0].FindControl("Txtgplantdt") as TextBox);
                        DropDownList ddlBenchLocation = (item.Cells[0].FindControl("ddlBenchLocation") as DropDownList);
                        HiddenField HiddenFielditm = (item.Cells[0].FindControl("HiddenFielditm") as HiddenField);
                        HiddenField HiddenFieldcusno = (item.Cells[0].FindControl("HiddenFieldcusno") as HiddenField);
                        HiddenField HiddenFieldsotrays = (item.Cells[0].FindControl("HiddenFieldsotrays") as HiddenField);
                        HiddenField HiddenFieldsodate = (item.Cells[0].FindControl("HiddenFieldsodate") as HiddenField);
                        HiddenField HiddenFieldduedate = (item.Cells[0].FindControl("HiddenFieldduedate") as HiddenField);
                        HiddenField HiddenFieldwo = (item.Cells[0].FindControl("HiddenFieldwo") as HiddenField);



                        if (lblAllocated.Text == "Yes" && ddlBenchLocation.SelectedValue != "" && Txtgtrays.Text != "" && Txtgplantdt.Text != "")
                        {

                            String tim = System.DateTime.Now.ToString("HH:mm:ss");

                            long result = 0;
                            NameValueCollection nv = new NameValueCollection();
                            nv.Add("@jid", _isInserted.ToString());
                            nv.Add("@jstatus", "0");
                            nv.Add("@jobcode", lbljobcode.Text.Trim());
                            nv.Add("@itemno", HiddenFielditm.Value);
                            nv.Add("@itemdescp", lblItem.Text);
                            nv.Add("@cname", lblCustName.Text);
                            nv.Add("@cusno", HiddenFieldcusno.Value);
                            nv.Add("@loc_seedline", ddlBenchLocation.SelectedValue);
                            nv.Add("@trays_plan", lblSOTrays.Text);
                            nv.Add("@trays_actual", Txtgtrays.Text);
                            nv.Add("@seedsreceived", "0");
                            nv.Add("@plan_date", Txtgplantdt.Text);
                            nv.Add("@actual_date", HiddenFieldsodate.Value);
                            nv.Add("@due_date", HiddenFieldduedate.Value);
                            nv.Add("@cmt", "");
                            nv.Add("@moduser", Session["LoginID"].ToString());
                            nv.Add("@modtime", tim);
                            nv.Add("@modified_date", "");
                            nv.Add("@SoDate", lblSODate.Text);
                            nv.Add("@TraySize", lblTraySize.Text);
                            nv.Add("@wo", HiddenFieldwo.Value);
                            nv.Add("@mode", "1");
                            _isInserted = objCommon.GetDataExecuteScalerRetObj("SP_Addgti_jobs_Seeding_Plan", nv);

                            _isInserted = 1;
                        }


                        SelectedItems++;


                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + SelectedItems + " ' Seeding Plan Save Successful ')", true);


                }

                getDataDGJob();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MyTaskGrower.aspx");
        }
        protected void btnSearchReset_Click(object sender, EventArgs e)
        {
            ddlTraySize.SelectedIndex = 0;
            ddlItem.SelectedIndex = 0;
            ddlSeedAllocated.SelectedIndex = 0;
            ddlTraySize.SelectedIndex = 0;
            getDataDGJob();
        }

        protected void DGJob_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lbl_Seedline = (Label)e.Row.FindControl("lbl_Seedline");
                DropDownList ddlBenchLocation = (DropDownList)e.Row.FindControl("ddlBenchLocation");

                ddlBenchLocation.DataSource = objSP.GetSeedlineLocation(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                ddlBenchLocation.DataTextField = "loc";
                ddlBenchLocation.DataValueField = "loc";
                ddlBenchLocation.DataBind();
                ddlBenchLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                ddlBenchLocation.SelectedValue = lbl_Seedline.Text;
            }

        }
    }
}
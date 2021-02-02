using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Improvians.Bal;

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
                TDate = (Convert.ToDateTime(Fdate)).AddDays(7).ToString("yyyy-MM-dd");
                //    Fdate = DateAdd(DateInterval.Day, -7, Now.Date);
                //  TDate = DateAdd(DateInterval.Day, 10, Now.Date);
                txtFromDate.Text = Fdate;
                txtToDate.Text = TDate;

                getDataDGJob();
            }
        }


        public void getDataDGJob()
        {
            AllData = objSP.GetDataSeedingPlan(txtFromDate.Text.Trim(), txtToDate.Text.Trim());

            DGJob.DataSource = AllData;
            DGJob.DataBind();

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

        protected void BtnPrint_Click(object sender, EventArgs e)
        {

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
                        Label lblSeedline = (item.Cells[0].FindControl("lblSeedline") as Label);
                        Label lbljobcode = (item.Cells[0].FindControl("lbljobcode") as Label);
                        Label lblItem = (item.Cells[0].FindControl("lblItem") as Label);
                        Label lblCustName = (item.Cells[0].FindControl("lblCustName") as Label);
                        Label lblSODate = (item.Cells[0].FindControl("lblSODate") as Label);
                        Label lblSOTrays = (item.Cells[0].FindControl("lblSOTrays") as Label);
                        Label lblTraySize = (item.Cells[0].FindControl("lblTraySize") as Label);
                        TextBox Txtgtrays = (item.Cells[0].FindControl("Txtgtrays") as TextBox);
                        TextBox Txtgplantdt = (item.Cells[0].FindControl("Txtgplantdt") as TextBox);

                        HiddenField HiddenFielditm = (item.Cells[0].FindControl("HiddenFielditm") as HiddenField);
                        HiddenField HiddenFieldcusno = (item.Cells[0].FindControl("HiddenFieldcusno") as HiddenField);
                        HiddenField HiddenFieldsotrays = (item.Cells[0].FindControl("HiddenFieldsotrays") as HiddenField);
                        HiddenField HiddenFieldsodate = (item.Cells[0].FindControl("HiddenFieldsodate") as HiddenField);
                        HiddenField HiddenFieldduedate = (item.Cells[0].FindControl("HiddenFieldduedate") as HiddenField);
                        HiddenField HiddenFieldwo = (item.Cells[0].FindControl("HiddenFieldwo") as HiddenField);

                        

                        if (lblAllocated.Text == "Yes")
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
                            nv.Add("@loc_seedline", lblSeedline.Text);
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
                            nv.Add("@wo", HiddenFieldwo.Value);
                            nv.Add("@mode", "1");
                            _isInserted = objCommon.GetDataExecuteScalerRetObj("SP_Addgti_jobs_Seeding_Plan", nv);


                            //_isInserted = 1;
                        }


                        SelectedItems++;


                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Seeding Plan Save  Successful')", true);
                }

               
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MyTaskGrower.aspx");
        }
    }
}
using Evo.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class PrintSeedlineReport : System.Web.UI.Page
    {
        General objGeneral = new General();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDate();
            }
        }


        private void BindDate()
        {
            string strSQL = " select distinct  CONVERT(date, CreateOn) as CreateOn from gti_jobs_seeds_plan";
            DataTable dt = objGeneral.GetDatasetByCommand(strSQL);
            ddlDate.DataSource = dt;          
            ddlDate.DataBind();
            ddlDate.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/PrintSeedlinePlannerReport.aspx?Date=" + ddlDate.SelectedItem.Text);
            BindRepeater(ddlDate.SelectedItem.Text);

            //string message = "Completion Successful";
            //string url = "MyTaskGreenSupervisorFinal.aspx";
            //string script = "window.onload = function(){ alert('";
            //script += message;
            //script += "');";
            //script += "window.location = '";
            //script += url;
            //script += "'; }";
            //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

            string Title = Convert.ToDateTime(System.DateTime.Now).ToString("MM-dd-yyyy") + "_Seeding_Log_Sheet";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "printScript", "document.title='"+ Title + "'; window.print();", true);
        }

        private void BindRepeater(string strDate)
        {
            string strSQL = " select distinct loc_seedline, CONVERT(date, CreateOn) as CreateOn from gti_jobs_seeds_plan";
            if (!string.IsNullOrEmpty(strDate) && strDate != "--Select--")
                strSQL = strSQL + " where CONVERT(date, CreateOn)= '" + strDate + "'";
            DataTable dt = objGeneral.GetDatasetByCommand(strSQL);
            repReport.DataSource = dt;
            repReport.DataBind();
        }

        protected void repReport_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int A = 0;
            GridView DGJob = (GridView)e.Item.FindControl("DGJob");
            GridView DGJob1 = (GridView)e.Item.FindControl("DGJob1");
            GridView DGJob2 = (GridView)e.Item.FindControl("DGJob2");
            Label lblFacility = (Label)e.Item.FindControl("lblFacility");
            Label lblDate = (Label)e.Item.FindControl("lblDate");
            Panel PanelView = (Panel)e.Item.FindControl("PanelView");
            Panel PanelView1 = (Panel)e.Item.FindControl("PanelView1");
            General objGeneral = new General();
            string strSQL = "select Top 35 * from gti_jobs_seeds_plan where loc_seedline='" + lblFacility.Text + "' and CONVERT(date,createon)='" + lblDate.Text + "'";
            DataTable dt = objGeneral.GetDatasetByCommand(strSQL);
            DGJob.DataSource = dt;
            DGJob.DataBind();


            General objGeneral1 = new General();
            string strSQLCount = "select  * from gti_jobs_seeds_plan where loc_seedline='" + lblFacility.Text + "' and CONVERT(date,createon)='" + lblDate.Text + "'";
            DataTable dt12 = objGeneral1.GetDatasetByCommand(strSQLCount);


            if (dt12.Rows.Count > 35 && dt12.Rows.Count < 70)
            {
                PanelView.Visible = true;
                General objGeneral2 = new General();
                string strSQL1 = "select * from gti_jobs_seeds_plan where loc_seedline='" + lblFacility.Text + "' and CONVERT(date,createon)='" + lblDate.Text + "' and ID > '"+35+ "' and ID < '" + 70 + "'";
                DataTable dt1 = objGeneral2.GetDatasetByCommand(strSQL1);
                DGJob1.DataSource = dt1;
                DGJob1.DataBind();

            }


            if (dt12.Rows.Count > 70)
            {
                PanelView1.Visible = true;
                General objGeneral2 = new General();
                string strSQL1 = "select * from gti_jobs_seeds_plan where loc_seedline='" + lblFacility.Text + "' and CONVERT(date,createon)='" + lblDate.Text + "' and ID > '" + 70 + "'";
                DataTable dt1 = objGeneral2.GetDatasetByCommand(strSQL1);
                DGJob2.DataSource = dt1;
                DGJob2.DataBind();

            }


        }

        protected void DGJob_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblplan_date = (Label)e.Row.FindControl("lblplan_date");
                Label lblDaysEarly = (Label)e.Row.FindControl("lblDaysEarly");
                Label lblGreenhouseDays = (Label)e.Row.FindControl("lblGreenhouseDays");
                Label lblCreateDate = (Label)e.Row.FindControl("lblCreateDate");
                Label lbldue_date = (Label)e.Row.FindControl("lbldue_date");


                
                string dtimeString = Convert.ToDateTime(lblCreateDate.Text).ToString("yyyy/MM/dd");
                DateTime dtCreateDate = Convert.ToDateTime(dtimeString);

                DateTime dtplan_date = Convert.ToDateTime(Convert.ToDateTime(lblplan_date.Text).ToString("yyyy/MM/dd"));
                DateTime dtdue_date = Convert.ToDateTime(Convert.ToDateTime(lbldue_date.Text).ToString("yyyy/MM/dd"));

                TimeSpan objTimeSpan = dtplan_date - dtCreateDate;
                double Days = Convert.ToDouble(objTimeSpan.TotalDays);


                TimeSpan objTimeDue = dtdue_date - dtplan_date;
                double DueDays = Convert.ToDouble(objTimeDue.TotalDays);

                lblDaysEarly.Text = Days.ToString();
                lblGreenhouseDays.Text = DueDays.ToString();
                if (dtplan_date < dtCreateDate)
                {
                    e.Row.CssClass = "overdue";
                }
            }
        }

        protected void DGJob1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblplan_date = (Label)e.Row.FindControl("lblplan_date1");
                Label lblDaysEarly = (Label)e.Row.FindControl("lblDaysEarly1");
                Label lblGreenhouseDays = (Label)e.Row.FindControl("lblGreenhouseDays1");
                Label lblCreateDate = (Label)e.Row.FindControl("lblCreateDate1");
                Label lbldue_date = (Label)e.Row.FindControl("lbldue_date1");



                string dtimeString = Convert.ToDateTime(lblCreateDate.Text).ToString("yyyy/MM/dd");
                DateTime dtCreateDate = Convert.ToDateTime(dtimeString);

                DateTime dtplan_date = Convert.ToDateTime(Convert.ToDateTime(lblplan_date.Text).ToString("yyyy/MM/dd"));
                DateTime dtdue_date = Convert.ToDateTime(Convert.ToDateTime(lbldue_date.Text).ToString("yyyy/MM/dd"));

                TimeSpan objTimeSpan = dtplan_date - dtCreateDate;
                double Days = Convert.ToDouble(objTimeSpan.TotalDays);


                TimeSpan objTimeDue = dtdue_date - dtplan_date;
                double DueDays = Convert.ToDouble(objTimeDue.TotalDays);

                lblDaysEarly.Text = Days.ToString();
                lblGreenhouseDays.Text = DueDays.ToString();
                if (dtplan_date < dtCreateDate)
                {
                    e.Row.CssClass = "overdue";
                }
            }
        }

        protected void DGJob2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblplan_date = (Label)e.Row.FindControl("lblplan_date2");
                Label lblDaysEarly = (Label)e.Row.FindControl("lblDaysEarly2");
                Label lblGreenhouseDays = (Label)e.Row.FindControl("lblGreenhouseDays2");
                Label lblCreateDate = (Label)e.Row.FindControl("lblCreateDate2");
                Label lbldue_date = (Label)e.Row.FindControl("lbldue_date2");



                string dtimeString = Convert.ToDateTime(lblCreateDate.Text).ToString("yyyy/MM/dd");
                DateTime dtCreateDate = Convert.ToDateTime(dtimeString);

                DateTime dtplan_date = Convert.ToDateTime(Convert.ToDateTime(lblplan_date.Text).ToString("yyyy/MM/dd"));
                DateTime dtdue_date = Convert.ToDateTime(Convert.ToDateTime(lbldue_date.Text).ToString("yyyy/MM/dd"));

                TimeSpan objTimeSpan = dtplan_date - dtCreateDate;
                double Days = Convert.ToDouble(objTimeSpan.TotalDays);


                TimeSpan objTimeDue = dtdue_date - dtplan_date;
                double DueDays = Convert.ToDouble(objTimeDue.TotalDays);

                lblDaysEarly.Text = Days.ToString();
                lblGreenhouseDays.Text = DueDays.ToString();
                if (dtplan_date < dtCreateDate)
                {
                    e.Row.CssClass = "overdue";
                }
            }
        }
    }
}
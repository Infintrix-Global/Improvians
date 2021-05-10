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
            GridView DGJob = (GridView)e.Item.FindControl("DGJob");
            Label lblFacility = (Label)e.Item.FindControl("lblFacility");
            Label lblDate = (Label)e.Item.FindControl("lblDate");
            string strSQL = "select * from gti_jobs_seeds_plan where loc_seedline='" + lblFacility.Text + "' and CONVERT(date,createon)='" + lblDate.Text + "'";
            DataTable dt = objGeneral.GetDatasetByCommand(strSQL);
            DGJob.DataSource = dt;
            DGJob.DataBind();
        }
    }
}
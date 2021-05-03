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
            Response.Redirect("~/PrintSeedlinePlannerReport.aspx?Date=" + ddlDate.SelectedItem.Text);
        }
    }
}
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
    public partial class PlantReadyAssignmentForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridGerm();

            }
        }

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Mode", "6");
            dt = objCommon.GetDataTable("SP_GetGreenHouseLogisticTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string JobID = "";
            string TaskID = "";
            if (e.CommandName == "Assign")
            {
                //int rowIndex = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = gvGerm.Rows[rowIndex];

                string WO = e.CommandArgument.ToString();

                Response.Redirect(String.Format("~/PlantReadyTaskAssignment.aspx?WOId={0}", WO));


            }


            if (e.CommandName == "Select")
            {
               
                string WO = e.CommandArgument.ToString();

                NameValueCollection nv = new NameValueCollection();
                nv.Add("@OperatorID", Session["LoginID"].ToString());
                nv.Add("@Notes", "");
                nv.Add("@JobID", JobID);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                nv.Add("@CropId", "");
                nv.Add("@UpdatedReadyDate", "");
                nv.Add("@PlantExpirationDate", "");
                nv.Add("@RootQuality", "");
                nv.Add("@PlantHeight", "");
                nv.Add("@wo", WO);
                nv.Add("@mode", "3");


                int result = objCommon.GetDataInsertORUpdate("SP_AddPlantReadyTaskAssignment", nv);


                Response.Redirect(String.Format("~/PlantReadyTaskCompletion.aspx?WOId={0}", WO));

            }
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lbljstatus = (Label)e.Row.FindControl("lbljstatus");
                Label lblTitla = (Label)e.Row.FindControl("lblTitla");

                if (lbljstatus.Text == "4")
                {
                    lblTitla.Text = "Plant Ready Request";
                }


            }
        }
    }
}
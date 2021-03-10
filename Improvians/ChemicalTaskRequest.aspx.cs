using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.BAL_Classes;
using Evo.Bal;

namespace Evo
{
    public partial class ChemicalTaskRequest : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        CommonControl objCommon = new CommonControl();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridSprayReq();
            }
        }


       
        public void BindGridSprayReq()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
           
            nv.Add("@LoginID", Session["LoginID"].ToString());
           
            dt = objCommon.GetDataTable("SP_GetChemicalRequestDetailsNew", nv);
            gvSpray.DataSource = dt;
            gvSpray.DataBind();

        }


        protected void gvSpray_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string ChemicalCode = gvSpray.DataKeys[rowIndex].Values[0].ToString();
                Response.Redirect(String.Format("~/ChemicalTaskCompletion.aspx?ChemicalCode={0}", ChemicalCode));
                //userinput.Visible = true;


               
            }

            if (e.CommandName == "ViewDetails")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string ChemicalCode = gvSpray.DataKeys[rowIndex].Values[0].ToString();
                Response.Redirect(String.Format("~/ChemicalTaskViewDetails.aspx?ChemicalCode={0}", ChemicalCode));
              

            }
        }

        protected void gvSpray_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSpray.PageIndex = e.NewPageIndex;
            BindGridSprayReq();
        }


       
       

        
    }
}
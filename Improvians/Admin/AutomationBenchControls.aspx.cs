using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo.Admin
{
    public partial class AutomationBenchControls : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFacility();
                BindGridBanchLoaction();
            }
        }


        public void BindGridBanchLoaction()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@BanchLocation", "");
            nv.Add("@Facility", ddlFacility.SelectedValue);
            nv.Add("@Mode", "1");
            dt = objCommon.GetDataTable("SP_GetBanchLocation", nv);

            GridBanchLocation.DataSource = dt;
            GridBanchLocation.DataBind();

         
        }

        public void BindFacility()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@BanchLocation", "");
            nv.Add("@Facility", "");
            nv.Add("@Mode", "2");
            dt = objCommon.GetDataTable("SP_GetBanchLocation", nv);

            ddlFacility.ClearSelection();
            ddlFacility.DataSource = dt;
            ddlFacility.DataTextField = "Facility";
            ddlFacility.DataValueField = "Facility";
            ddlFacility.DataBind();
            ddlFacility.Items.Insert(0, new ListItem("--Select--", "0"));



        }


        protected void ddlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            long result = 0;
            foreach (GridViewRow row in GridBanchLocation.Rows)
            {
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@AutomationBenchControlsId", "0");
                nv.Add("@BenchName", (row.FindControl("lblLocation") as Label).Text);
                nv.Add("@Facility", (row.FindControl("lblloc_seedline") as Label).Text);
                nv.Add("@Automation", (row.FindControl("ddlMain") as DropDownList).SelectedValue);
                nv.Add("@RoleId", "16");
                nv.Add("@Login", Session["LoginID"].ToString());
                nv.Add("@Mode", "1");
                result = objCommon.GetDataExecuteScalerRetObj("SP_AddAutomationBenchControls", nv);

            }
        }

      

        protected void GridBanchLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridBanchLocation.PageIndex = e.NewPageIndex;
            BindGridBanchLoaction();
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridBanchLoaction();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            foreach (GridViewRow row in GridBanchLocation.Rows)
            {
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@AutomationBenchControlsId", "0");
                nv.Add("@BenchName", (row.FindControl("lblLocation") as Label).Text);
                nv.Add("@Facility", (row.FindControl("lblloc_seedline") as Label).Text);
                nv.Add("@Automation", (row.FindControl("ddlMain") as DropDownList).SelectedValue);
                nv.Add("@RoleId","16");
                nv.Add("@Login", Session["LoginID"].ToString());
                nv.Add("@Mode", "1");
                result = objCommon.GetDataExecuteScalerRetObj("SP_AddAutomationBenchControls", nv);

            }


        }
    }
}
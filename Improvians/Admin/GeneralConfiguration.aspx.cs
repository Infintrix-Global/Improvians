using Evo.Admin.BAL_Classes;
using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace Evo.Admin
{
    public partial class GeneralConfiguration : System.Web.UI.Page
    {
        General objGeneral = new General();
        clsCommonMasters objCommon = new clsCommonMasters();
        BAL_Task objTask = new BAL_Task();
        BAL_PlantProductionProfile objPlant = new BAL_PlantProductionProfile();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCrop();
            }
        }


        public void BindCrop()
        {
            GridProfile.DataSource = objCommon.GETCrop();
            GridProfile.DataBind();
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            BindCrop();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            long result1 = 0;
            //foreach (GridViewRow row in gvProductionProfile.Rows)
            //{
            //    if (!string.IsNullOrEmpty(((TextBox)row.FindControl("txtdateshift")).Text))
            //    {
            //        string s = ((TextBox)row.FindControl("txtdateshift")).Text;
            //        string id = ((Label)row.FindControl("lblID")).Text;
            //        ProfilePlanner obj = new ProfilePlanner()
            //        {
            //            pid = Convert.ToInt32(id),
            //            dateshift = Convert.ToInt32(s)
            //        };
            //        result1 = objCommon.UpdateDateShift(obj);
            //    }
            //}
            string message = "Record updated Successful";
            string url = "ViewPlantProductionProfile.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }

        protected void GridProfile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
    }

}

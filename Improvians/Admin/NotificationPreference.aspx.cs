using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Admin.BAL_Classes;
using Evo.Admin;

namespace Evo.Admin
{
    public partial class NotificationPreference : System.Web.UI.Page
    {
        General objGeneral = new General();
        protected void Page_Load(object sender, EventArgs e)
        {
            var sqr = "";
            DataTable dt = new DataTable(); 
            if (!IsPostBack)
            {
                sqr = "select distinct RTRIM(L.EmployeeName) + '_' + R.RoleAbbreviation as UserName from Role R inner join Login L on L.RoleID = R.RoleID " +
                    "where L.ISActive = 1  and R.RoleAbbreviation Is Not Null";                
                    dt = objGeneral.GetDatasetByCommand(sqr);
                dt.Columns.Add("Action");
                gvUsers.DataSource = dt;
                gvUsers.DataBind();

            }
        }
        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
         
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {

          
        }
    }
}
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
    public partial class ResetData : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            long _isInserted = 0;
            NameValueCollection nv = new NameValueCollection();

            _isInserted = objCommon.GetDataInsertORUpdate("SP_AddResetData", nv);

            string message = "Reset All Data Successful";
            string url;

            url = "ResetData.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);


        }
    }
}
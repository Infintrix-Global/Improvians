using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class Test : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            NameValueCollection nv = new NameValueCollection();
            DataTable dt = new DataTable();
            nv.Add("@mode", "28");
            dt = objCommon.GetDataTable("GET_Common", nv);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //SP_UpdatePlantReadyDate

                NameValueCollection nv1 = new NameValueCollection();

            
                nv1.Add("@Jid",dt.Rows[i]["Jid"].ToString());
                nv1.Add("@DDate", dt.Rows[i]["PlantDueDate"].ToString());
                objCommon.GetDataInsertORUpdate("SP_UpdatePlantReadyDate", nv1);
            }
        }
    }
}
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
                GetConfiguration();
            }
        }

        public void GetConfiguration()
        {
            DataTable dt = objCommon.GetPlantProductionConfiguration();
            DataRow dr = dt.Rows[0];
            txtGerm1.Text = dr["Germination1"].ToString();
            txtGerm2.Text = dr["Germination2"].ToString();
            txtGerm3.Text = dr["Germination3"].ToString();           

            GridProfile.DataSource = objCommon.GetPlantProductionCrop();
            GridProfile.DataBind();
        }



        protected void ButtonUpdateConfig_Click(object sender, EventArgs e)
        {
            long result = objCommon.UpdatePlantProductionConfiguration(Convert.ToInt32(txtGerm1.Text), Convert.ToInt32(txtGerm2.Text), Convert.ToInt32(txtGerm3.Text));
            if (result > 0)
            {
                GridProfile.DataSource = objCommon.GetPlantProductionCrop();
                GridProfile.DataBind();
            }

        }
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridProfile.Rows)
            {
                string Crop = ((Label)row.FindControl("lblCrop")).Text;
                TextBox Germ1 = ((TextBox)row.FindControl("txtGerm1"));
                TextBox Germ2 = ((TextBox)row.FindControl("txtGerm2"));
                // TextBox Germ3 = ((TextBox)row.FindControl("txtGerm3"));
                //if (Germ1.Text != txtGerm1.Text || Germ2.Text != txtGerm2.Text|| Germ3.Text != txtGerm3.Text)
                // {
                ////     long result = objCommon.AddPlantProductionCrop(Crop, Convert.ToInt32(Germ1.Text), Convert.ToInt32(Germ2.Text), Convert.ToInt32(Germ3.Text));
                //  }


                if (Germ1.Text != txtGerm1.Text || Germ2.Text != txtGerm2.Text)
                {
                    long result = objCommon.AddPlantProductionCrop(Crop, Convert.ToInt32(Germ1.Text), Convert.ToInt32(Germ2.Text),0);
                }
            }
        }
    }

}

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
    public partial class PlantReadyConfiguration : System.Web.UI.Page
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
                GetPlantReadyShiftNoDataBind();
            }
        }


        public void BindCrop()
        {
            GridProfile.DataSource = objCommon.GETCrop();
            GridProfile.DataBind();
        }

        public void GetConfiguration()
        {
            DataTable dt = objCommon.GetPlantProductionConfiguration();
            DataRow dr = dt.Rows[0];
            txtPlantReady.Text = dr["PlantDueDate"].ToString();
          
            
            GridProfile.DataSource = objCommon.GetPlantProductionCrop();
            GridProfile.DataBind();
        }



        protected void ButtonUpdateConfig_Click(object sender, EventArgs e)
        {
            long result = objCommon.UpdatePlantProductionPlantReadyConfiguration(Convert.ToInt32(txtPlantReady.Text));
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
                TextBox PlantReady = ((TextBox)row.FindControl("txtPlantReady"));
                if (PlantReady.Text != txtPlantReady.Text)
                {
                    long result = objCommon.AddPlantProductionCropPlantReady(Crop, Convert.ToInt32(PlantReady.Text));
                }
            }
        }


        public void GetPlantReadyShiftNoDataBind()
        {
            DataTable dt = objCommon.GetPlantReadyShiftNoData();
            DataRow dr = dt.Rows[0];
          
            txtDateShiftNo.Text = dr["ShiftNo"].ToString();

        }




        protected void btnPlantReadyDate_Click(object sender, EventArgs e)
        {
            long result = objCommon.UpdatePlantReadyDateShifNo(Convert.ToInt32(txtDateShiftNo.Text));
            GetPlantReadyShiftNoDataBind();
        }
    }

}

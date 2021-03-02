using Improvians.Admin.BAL_Classes;
using Improvians.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class CropHealthReport : System.Web.UI.Page
    {
        public static DataTable dtTrays = new DataTable()
        { Columns = { "Fertilizer", "Quantity", "Unit", "Tray", "SQFT" } };
        CommonControlNavision objNav = new CommonControlNavision();
        BAL_CommonMasters objBAL = new BAL_CommonMasters();
        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    BindSupervisor();
            //    BindFertilizer();
            //    //    BindUnit();
            //    BindJobCode(ddlBenchLocation.SelectedValue);
            //    Bindcname();
            //    BindFacility();
            //    dtTrays.Clear();
            //}
            if (Request.Browser.IsMobileDevice)
            {
                divMobile.Visible = true;
            }
            else
            {
                divLaptop.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            NameValueCollection nv = new NameValueCollection();
            string folderPath = Server.MapPath("~/images/");
            FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));
            nv.Add("@typeofProblem ", ddlpr.SelectedItem.Text);
            nv.Add("@Causeofproblem", DropDownListCause.SelectedItem.Text);
            nv.Add("@Severityofproblem", DropDownListSv.SelectedValue);
            nv.Add("@NoTrays", txtTrays.Text);
            nv.Add("@PerDamage", percentageDamage.Text);
            nv.Add("@Date", txtDate.Text);
            nv.Add("@Filepath", folderPath);

            result = objCommon.GetDataExecuteScaler("SP_AddCropHealthReport", nv);
            Clear();
        }

        public void Clear()
        {

            ddlpr.SelectedValue = "0";
            DropDownListCause.SelectedValue = "0";
            DropDownListSv.SelectedValue = "0";
            txtTrays.Text = "";
            txtDate.Text = "";
            percentageDamage.Text = "";
            dtTrays.Clear();
        }
        protected void UploadFile(object sender, EventArgs e)
        {
            string folderPath = Server.MapPath("~/Files/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            //Save the File to the Directory (Folder).
            FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

            //Display the success message.
            lblMessage.Text = Path.GetFileName(FileUpload1.FileName) + " has been uploaded.";

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
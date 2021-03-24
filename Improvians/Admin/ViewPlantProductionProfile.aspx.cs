using Evo.Admin.BAL_Classes;
using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace Evo.Admin
{
    public partial class ViewPlantProductionProfile : System.Web.UI.Page
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
                ddlActivityCode.Items.Insert(0, new ListItem("--- Select ---", "0"));
                ddlTrayCode.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            dt = objCommon.GetPlanProductionProfile(ddlCrop.SelectedValue, ddlActivityCode.SelectedValue, ddlTrayCode.SelectedValue);
            gvProductionProfile.DataSource = dt;
            gvProductionProfile.DataBind();

        }
        public void BindCrop()
        {
            ddlCrop.DataSource = objCommon.GETCrop();
            ddlCrop.DataTextField = "Crop";
            ddlCrop.DataValueField = "Crop";
            ddlCrop.DataBind();
            ddlCrop.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlCrop.SelectedIndex = 0;
            ddlActivityCode.SelectedIndex = 0;
            ddlTrayCode.SelectedIndex = 0;
            gvProductionProfile.DataSource = null;
            gvProductionProfile.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            long result1 = 0;
            foreach (GridViewRow row in gvProductionProfile.Rows)
            {
                if (!string.IsNullOrEmpty(((TextBox)row.FindControl("txtdateshift")).Text))
                {
                    string s = ((TextBox)row.FindControl("txtdateshift")).Text;
                    string id = ((Label)row.FindControl("lblID")).Text;
                    ProfilePlanner obj = new ProfilePlanner()
                    {
                        pid = Convert.ToInt32(id),
                        dateshift = Convert.ToInt32(s)
                    };
                    result1 = objCommon.UpdateDateShift(obj);
                }
            }
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
        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlActivityCode.DataSource = objCommon.GETActivityCode(ddlCrop.SelectedValue);
            ddlActivityCode.DataTextField = "ActivityCode";
            ddlActivityCode.DataValueField = "ActivityCode";
            ddlActivityCode.DataBind();
            ddlActivityCode.Items.Insert(0, new ListItem("--- Select ---", "0"));

            ddlTrayCode.DataSource = objCommon.GETTrayCode(ddlCrop.SelectedValue);
            ddlTrayCode.DataTextField = "TrayCode";
            ddlTrayCode.DataValueField = "TrayCode";
            ddlTrayCode.DataBind();
            ddlTrayCode.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }
        protected void gvProductionProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void ddlActivityCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = objCommon.GetPlanProductionProfile(ddlCrop.SelectedValue, ddlActivityCode.SelectedValue, ddlTrayCode.SelectedValue);
            gvProductionProfile.DataSource = dt;
            gvProductionProfile.DataBind();
        }

        protected void ddlTrayCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = objCommon.GetPlanProductionProfile(ddlCrop.SelectedValue, ddlActivityCode.SelectedValue, ddlTrayCode.SelectedValue);
            gvProductionProfile.DataSource = dt;
            gvProductionProfile.DataBind();
        }
        protected void btnAddProfile_Click(object sender, EventArgs e)
        {
            pnlAdd.Visible = true;
            pnlList.Visible = false;
            AddNewRow(true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlAdd.Visible = false;
            pnlList.Visible = true;
        }
        protected void btAdd_Click(object sender, EventArgs e)
        {
            AddNewRow(true);

        }
        public void Clear()
        {
            GridProfile.DataSource = null;
            GridProfile.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
            AddNewRow(true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int _isInserted = -1;
                foreach (GridViewRow item in GridProfile.Rows)
                {

                    if (item.RowType == DataControlRowType.DataRow)
                    {

                        DropDownList ddlActivity = (DropDownList)item.Cells[0].FindControl("ddlActivityCode");
                        DropDownList ddlCode = (DropDownList)item.Cells[0].FindControl("ddlCode");
                        DropDownList ddlCrop = (DropDownList)item.Cells[0].FindControl("ddlCrop");
                        DropDownList ddlTraySize = (DropDownList)item.Cells[0].FindControl("ddlTraySize");
                        TextBox txtDateShift = (item.Cells[0].FindControl("txtDateShift") as TextBox);

                        ProfilePlanner obj = new ProfilePlanner
                        {
                            code = ddlCode.SelectedValue,
                            crop = ddlCrop.SelectedValue,
                            dateshift = Convert.ToInt32(txtDateShift.Text),
                            activitycode = ddlActivity.SelectedValue,
                            traycode = ddlTraySize.SelectedValue
                        };

                        _isInserted = objCommon.InsertPlantProductionProfile(obj);
                    }
                }
                if (_isInserted == -1)
                {
                    lblmsg.Text = "Failed to Add Profile";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblmsg.Text = "";
                    pnlList.Visible = true;
                    pnlAdd.Visible = false;
                    DataTable dt = new DataTable();
                    dt = objCommon.GetPlanProductionProfile(ddlCrop.SelectedValue, ddlActivityCode.SelectedValue, ddlTrayCode.SelectedValue);
                    gvProductionProfile.DataSource = dt;
                    gvProductionProfile.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void GridProfile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlActivity = (DropDownList)e.Row.FindControl("ddlActivityCode");
                DropDownList ddlCode = (DropDownList)e.Row.FindControl("ddlCode");
                DropDownList ddlCrop = (DropDownList)e.Row.FindControl("ddlCrop");
                DropDownList ddlTraySize = (DropDownList)e.Row.FindControl("ddlTraySize");


                ddlActivity.DataSource = objPlant.GetActivityCodeList();
                ddlActivity.DataTextField = "ActivityCode";
                ddlActivity.DataValueField = "ActivityCode";
                ddlActivity.DataBind();

                ddlCrop.DataSource = objPlant.GetCropList();
                ddlCrop.DataTextField = "Crop";
                ddlCrop.DataValueField = "Crop";
                ddlCrop.DataBind();

                ddlCode.DataSource = objPlant.GetCodeList();
                ddlCode.DataTextField = "Code";
                ddlCode.DataValueField = "Code";
                ddlCode.DataBind();

                ddlTraySize.DataSource = objPlant.GetTraysizeList();
                ddlTraySize.DataTextField = "traysize";
                ddlTraySize.DataValueField = "traysize";
                ddlTraySize.DataBind();

            }
        }

        private List<PlantProductionProfileDetils> PlantProductionProfileData
        {
            get
            {
                if (ViewState["PlantProductionProfileData"] != null)
                {
                    return (List<PlantProductionProfileDetils>)ViewState["PlantProductionProfileData"];
                }
                return new List<PlantProductionProfileDetils>();
            }
            set
            {
                ViewState["PlantProductionProfileData"] = value;
            }
        }

        private void AddNewRow(bool AddBlankRow)
        {
            try
            {
                List<PlantProductionProfileDetils> objProfile = PlantProductionProfileData;

                //foreach (GridViewRow item in GridProfile.Rows)
                //{

                //    string Code = ((DropDownList)item.FindControl("ddlCode")).SelectedValue;
                //    string Crop = ((DropDownList)item.FindControl("ddlCrop")).SelectedValue;
                //    string TraySize = ((DropDownList)item.FindControl("ddlTraySize")).SelectedValue;
                //    string ActivityCode = ((DropDownList)item.FindControl("ddlActivityCode")).SelectedValue;
                //    TextBox txtDateShift = (TextBox)item.FindControl("txtDateShift");

                //    AddProfileDetail(ref objProfile, Code, Crop, TraySize, ActivityCode, Convert.ToInt16(txtDateShift.Text));

                //}
                if (AddBlankRow)
                    AddProfileDetail(ref objProfile, "", "", "", "", 0);

                PlantProductionProfileData = objProfile;
                GridProfileBind();
            }
            catch (Exception ex)
            {

            }

        }

        public void GridProfileBind()
        {
            GridProfile.DataSource = PlantProductionProfileData;
            GridProfile.DataBind();

        }

        private void AddProfileDetail(ref List<PlantProductionProfileDetils> objPD, string Code, string Crop, string ActivityCode, string TraySize, int DateShift)
        {
            PlantProductionProfileDetils objProfile = new PlantProductionProfileDetils();

            objProfile.RowNumber = objPD.Count + 1;
            objProfile.Code = Code;
            objProfile.Crop = Crop;
            objProfile.ActivityCode = ActivityCode;
            objProfile.TraySize = TraySize;
            objProfile.DateShift = DateShift;

            objPD.Add(objProfile);
            ViewState["objPD"] = objPD;
        }

        protected void GridProfile_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<PlantProductionProfileDetils> objProfile = PlantProductionProfileData;
            objProfile.RemoveAt(e.RowIndex);
            PlantProductionProfileData = objProfile;
            GridProfileBind();
        }
    }

}
[Serializable]
public class PlantProductionProfileDetils
{
    public int RowNumber { get; set; }
    public string Code { get; set; }
    public string Crop { get; set; }
    public string ActivityCode { get; set; }
    public string TraySize { get; set; }
    public int DateShift { get; set; }
}
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
        static string Code = string.Empty;
        static string Crop = string.Empty;
        static string TraySize = string.Empty;
        static string ActivityCode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCrop();
                ddlCrop.Items.Insert(0, new ListItem("--- Select ---", "0"));
                ddlActivityCode.Items.Insert(0, new ListItem("--- Select ---", "0"));
                ddlTrayCode.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = objCommon.GetPlanProductionProfile(ddlCode.SelectedValue, ddlCrop.SelectedValue, ddlActivityCode.SelectedValue, ddlTrayCode.SelectedValue);
            gvProductionProfile.DataSource = dt;
            gvProductionProfile.DataBind();

        }
        public void BindCrop()
        {
            ddlCode.DataSource = objCommon.GETCode();
            ddlCode.DataTextField = "Code";
            ddlCode.DataValueField = "Code";
            ddlCode.DataBind();
            ddlCode.Items.Insert(0, new ListItem("--- Select ---", "0"));

        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlCode.SelectedIndex = 0;
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
        protected void ddlCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDropdowns(ddlCode.SelectedValue);
            BindGrid();
        }
        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTrayCode.DataSource = objCommon.GETTrayCode(ddlCode.SelectedValue, ddlCrop.SelectedValue);
            ddlTrayCode.DataTextField = "TrayCode";
            ddlTrayCode.DataValueField = "TrayCode";
            ddlTrayCode.DataBind();
            ddlTrayCode.Items.Insert(0, new ListItem("--- Select ---", "0"));

            ddlActivityCode.DataSource = objCommon.GETActivityCode(ddlCrop.SelectedValue);
            ddlActivityCode.DataTextField = "ActivityCode";
            ddlActivityCode.DataValueField = "ActivityCode";
            ddlActivityCode.DataBind();
            ddlActivityCode.Items.Insert(0, new ListItem("--- Select ---", "0"));
            BindGrid();
        }

        private void BindDropdowns(string Code)
        {
            ddlCrop.DataSource = objCommon.GETCrop(Code);
            ddlCrop.DataTextField = "Crop";
            ddlCrop.DataValueField = "Crop";
            ddlCrop.DataBind();
            ddlCrop.Items.Insert(0, new ListItem("--- Select ---", "0"));    

          
        }
        protected void gvProductionProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteProfile")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                objCommon.DeletePlantProductionProfile(id);
                BindGrid();
            }
        }
        private void BindGrid()
        {
            DataTable dt = objCommon.GetPlanProductionProfile(ddlCode.SelectedValue, ddlCrop.SelectedValue, ddlActivityCode.SelectedValue, ddlTrayCode.SelectedValue);
            gvProductionProfile.DataSource = dt;
            gvProductionProfile.DataBind();
        }
        protected void ddlActivityCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlTrayCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
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
            Code = string.Empty;
            Crop = string.Empty;
            TraySize = string.Empty;
            ActivityCode = string.Empty;
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
                    pnlAdd.Visible = true;
                }
                else
                {
                    lblmsg.Text = "";
                    pnlList.Visible = true;
                    pnlAdd.Visible = false;
                    ddlCrop.SelectedValue = Crop;
                    BindDropdowns(Crop);
                    ddlActivityCode.SelectedValue = ActivityCode;
                    ddlTrayCode.SelectedValue = TraySize;
                    BindGrid();
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
                TextBox txtDateShift = (TextBox)e.Row.FindControl("txtDateShift");

                ddlCode.DataSource = objPlant.GetCodeList();
                ddlCode.DataTextField = "Code";
                ddlCode.DataValueField = "Code";
                ddlCode.DataBind();
                ddlCode.Items.Insert(0, new ListItem("--- Select ---", "0"));

                ddlActivity.DataSource = objPlant.GetActivityCodeList();
                ddlActivity.DataTextField = "ActivityCode";
                ddlActivity.DataValueField = "ActivityCode";
                ddlActivity.DataBind();
                ddlActivity.Items.Insert(0, new ListItem("--- Select ---", "0"));

                ddlCrop.Items.Insert(0, new ListItem("--- Select ---", "0"));
                ddlTraySize.Items.Insert(0, new ListItem("--- Select ---", "0"));

                HiddenField hdnActivity = (HiddenField)e.Row.FindControl("hdnActivityCode");
                HiddenField hdnCode = (HiddenField)e.Row.FindControl("hdnCode");
                HiddenField hdnCrop = (HiddenField)e.Row.FindControl("hdnCrop");
                HiddenField hdnTraySize = (HiddenField)e.Row.FindControl("hdnTraySize");
                HiddenField hdnDateShift = (HiddenField)e.Row.FindControl("hdnDateShift");

                ddlCode.SelectedValue = hdnCode.Value;
                BindCodeCascadingDropdowns(e.Row, Code);
                ddlCrop.SelectedValue = hdnCrop.Value;
                ddlActivity.SelectedValue = hdnActivity.Value;
                ddlTraySize.SelectedValue = hdnTraySize.Value;
                txtDateShift.Text = hdnDateShift.Value;
            }
        }

        protected void ddlGridCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCode = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlCode.NamingContainer;
            if (row != null)
            {
                BindCodeCascadingDropdowns(row, ddlCode.SelectedValue);
            }
        }

        private void BindCodeCascadingDropdowns(GridViewRow row, string Code)
        {
            DropDownList ddlCrop = (DropDownList)row.FindControl("ddlCrop");
            ddlCrop.DataSource = objPlant.GetCropList(Code);
            ddlCrop.DataTextField = "Crop";
            ddlCrop.DataValueField = "Crop";
            ddlCrop.DataBind();
            ddlCrop.Items.Insert(0, new ListItem("--- Select ---", "0"));

            DropDownList ddlTraySize = (DropDownList)row.FindControl("ddlTraySize");
            ddlTraySize.DataSource = objPlant.GetTraysizeList(Code);
            ddlTraySize.DataTextField = "traysize";
            ddlTraySize.DataValueField = "traysize";
            ddlTraySize.DataBind();
            ddlTraySize.Items.Insert(0, new ListItem("--- Select ---", "0"));

            //HiddenField hdnCrop = (HiddenField)row.FindControl("hdnCrop");
            //HiddenField hdnTraySize = (HiddenField)row.FindControl("hdnTraySize");
            //ddlCrop.SelectedValue = hdnCrop.Value;
            //ddlTraySize.SelectedValue = hdnTraySize.Value;
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
                List<PlantProductionProfileDetils> objProfile = new List<PlantProductionProfileDetils>();

                foreach (GridViewRow item in GridProfile.Rows)
                {
                    Code = ((DropDownList)item.FindControl("ddlCode")).SelectedValue;
                    Crop = ((DropDownList)item.FindControl("ddlCrop")).SelectedValue;
                    TraySize = ((DropDownList)item.FindControl("ddlTraySize")).SelectedValue;
                    ActivityCode = ((DropDownList)item.FindControl("ddlActivityCode")).SelectedValue;
                    TextBox txtDateShift = (TextBox)item.FindControl("txtDateShift");

                    AddProfileDetail(ref objProfile, Code, Crop, ActivityCode, TraySize, Convert.ToInt16(txtDateShift.Text));
                }
                if (AddBlankRow)
                    AddProfileDetail(ref objProfile, Code, Crop, ActivityCode, TraySize, 0);

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
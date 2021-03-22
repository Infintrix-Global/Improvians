using Evo.Admin.BAL_Classes;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // BindDepartment();
                BindCrop();
                ddlActivityCode.Items.Insert(0, new ListItem("--- Select ---", "0"));
                ddlTrayCode.Items.Insert(0, new ListItem("--- Select ---", "0"));
                ddlActivity.DataSource = objCommon.GETActivityCode();
                ddlActivity.DataTextField = "ActivityCode";
                ddlActivity.DataValueField = "ActivityCode";
                ddlActivity.DataBind();
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

        protected void gvProductionProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //foreach (GridViewRow row in gvProductionProfile.Rows)
            //{
            //    if (!string.IsNullOrEmpty(((TextBox)row.FindControl("txtdateshift")).Text))
            //    {
            //        string s = ((TextBox)row.FindControl("txtdateshift")).Text;
            //        string id = ((Label)row.FindControl("lblID")).Text;
            //    }
            //}
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
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlAdd.Visible = false;
            pnlList.Visible = true;
        }
        protected void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int _isInserted = -1;
                ProfilePlanner obj = new ProfilePlanner
                {
                    code = txtCode.Text,
                    crop = txtCrop.Text,
                    dateshift = Convert.ToInt32(txtName.Text),
                    activitycode = ddlActivity.SelectedValue,
                    traycode = Convert.ToInt32(txtTray.Text)
                };

                _isInserted = objCommon.InsertPlantProductionProfile(obj);
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCode(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Evo"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = " select distinct code from [gti_jobs_prodprofile] where  code like '%" + prefixText + "%'  order by code";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(sdr["code"].ToString());
                        }
                    }
                    conn.Close();
                    return customers;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCrop(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Evo"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = " select distinct crop from [gti_jobs_prodprofile] where  crop like '%" + prefixText + "%'  order by crop";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(sdr["crop"].ToString());
                        }
                    }
                    conn.Close();
                    return customers;
                }
            }
        }
    }

}
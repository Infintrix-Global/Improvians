using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class MyTaskProductionPlanner : System.Web.UI.Page
    {
        Bal_SeedingPlan objSP = new Bal_SeedingPlan();
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objCom = new BAL_CommonMasters();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Bindcname();
                //BindJobCode();
                //BindCropType();
                BindJobHistoryDropdown();
                // BindFacility();
                BindGridGerm("0");
            }
        }

        public void BindCropType()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Id", "");
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@Mode", "3");
            dt = objCommon.GetDataTable("SP_GetSeedLineSupervisorSearch", nv);
            ddlCopTYpe.DataSource = dt;
            ddlCopTYpe.DataTextField = "GenusCode";
            ddlCopTYpe.DataValueField = "GenusCode";
            ddlCopTYpe.DataBind();
            ddlCopTYpe.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void Bindcname()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Id", "");
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@Mode", "2");
            dt = objCommon.GetDataTable("SP_GetSeedLineSupervisorSearch", nv);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "cname";
            ddlCustomer.DataValueField = "cname";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        public void BindSeedlineLocation()
        {
            ddlSeedlineLocation.DataSource = objSP.GetSeedlineLocationProductionPlanner();
            ddlSeedlineLocation.DataTextField = "loc";
            ddlSeedlineLocation.DataValueField = "loc";
            ddlSeedlineLocation.DataBind();
            ddlSeedlineLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        public void BindJobCode()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Id", "");
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@Mode", "1");
            dt = objCommon.GetDataTable("SP_GetSeedLineSupervisorSearch", nv);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        }


        public void BindJobHistoryDropdown()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@JobCode", "0");
            nv.Add("@CustomerName", "0");
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@CropType", "0");

            dt = objCommon.GetDataTable("SP_GetProductionPlannerTask", nv);

            ddlJobNo.DataSource = SelectDistinct(dt, "Jobcode");
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--- Select ---", "0"));

            ddlCustomer.DataSource = SelectDistinct(dt, "cname");
            ddlCustomer.DataBind(); 
            ddlCustomer.Items.Insert(0, new ListItem("--- Select ---", "0"));


            ddlCopTYpe.DataSource = SelectDistinct(dt, "GenusCode");
            ddlCopTYpe.DataBind();
            ddlCopTYpe.Items.Insert(0, new ListItem("--- Select ---", "0"));
           
        }


        //public void BindFacility()
        //{

        //    DataTable dt = new DataTable();
        //    NameValueCollection nv = new NameValueCollection();

        //    nv.Add("@Mode", "9");
        //    dt = objCommon.GetDataTable("GET_Common", nv);
        //    ddlFacility.DataSource = dt;
        //    ddlFacility.DataTextField = "loc_seedline";
        //    ddlFacility.DataValueField = "loc_seedline";
        //    ddlFacility.DataBind();
        //    ddlFacility.Items.Insert(0, new ListItem("--Select--", "0"));

        //}

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindGridGerm("0");
            DataView dataView = BindGridGerm("0");
            
            if (ddlJobNo.SelectedIndex == 0)
            {
                ddlJobNo.DataSource = dataView.ToTable(true, "Jobcode");
                ddlJobNo.DataBind();
                ddlJobNo.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
            if (ddlCustomer.SelectedIndex == 0)
            {
                ddlCustomer.DataSource = dataView.ToTable(true, "cname"); 
                ddlCustomer.DataBind();
                ddlCustomer.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
            if (ddlCopTYpe.SelectedIndex == 0)
            {
                ddlCopTYpe.DataSource = dataView.ToTable(true, "GenusCode"); 
                ddlCopTYpe.DataBind();
                ddlCopTYpe.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }

        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
           // BindGridGerm(ddlJobNo.SelectedValue);
            DataView dataView = BindGridGerm(ddlJobNo.SelectedValue);

            if (ddlJobNo.SelectedIndex == 0)
            {
                ddlJobNo.DataSource = dataView.ToTable(true, "Jobcode");
                ddlJobNo.DataBind();
                ddlJobNo.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
            if (ddlCustomer.SelectedIndex == 0)
            {
                ddlCustomer.DataSource = dataView.ToTable(true, "cname");
                ddlCustomer.DataBind();
                ddlCustomer.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
            if (ddlCopTYpe.SelectedIndex == 0)
            {
                ddlCopTYpe.DataSource = dataView.ToTable(true, "GenusCode");
                ddlCopTYpe.DataBind();
                ddlCopTYpe.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm("0");
        }
        protected void ddlCopTYpe_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindGridGerm("0");

            DataView dataView = BindGridGerm("0");

            if (ddlJobNo.SelectedIndex == 0)
            {
                ddlJobNo.DataSource = dataView.ToTable(true, "Jobcode");
                ddlJobNo.DataBind();
                ddlJobNo.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
            if (ddlCustomer.SelectedIndex == 0)
            {
                ddlCustomer.DataSource = dataView.ToTable(true, "cname");
                ddlCustomer.DataBind();
                ddlCustomer.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
            if (ddlCopTYpe.SelectedIndex == 0)
            {
                ddlCopTYpe.DataSource = dataView.ToTable(true, "GenusCode");
                ddlCopTYpe.DataBind();
                ddlCopTYpe.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
        }

        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            BindGridGerm(txtSearchJobNo.Text);
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            Bindcname();
            BindJobCode();
          //  BindFacility();
            BindCropType();
            BindGridGerm("0");
        }

       // public dataView BindGridGerm(string JobNo)
        public DataView BindGridGerm(string JobNo)
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@JobCode", JobNo);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@CropType", ddlCopTYpe.SelectedValue);
            dt = objCommon.GetDataTable("SP_GetProductionPlannerTask", nv);
            DataView dataView = dt.DefaultView;

           // DataTable dt1 = objCom.GetSeedLot(lblID.Text);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    e.Row.Visible = true;
            //}
            //else
            //{
            //    e.Row.Visible = false;
            //}


            DataTable dtCopy = dt.Clone();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable dt1 = objCom.GetSeedLot(dt.Rows[i]["jobcode"].ToString());

                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    dtCopy.ImportRow(dt.Rows[i]);
                }
                else
                {
                  
                }
               
            }



            gvGerm.DataSource = dtCopy;
            gvGerm.DataBind();

            return dataView;
        }

        private string WO
        {
            get
            {
                if (ViewState["WO"] != null)
                {
                    return (string)ViewState["WO"];
                }
                return "";
            }
            set
            {
                ViewState["WO"] = value;
            }
        }
        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string WO = e.CommandArgument.ToString();
                //  Session["WorkOrder"] = e.CommandArgument.ToString();
                // Response.Redirect("~/ProductionPlannerTaskCompletionForm.aspx");
                //  Response.Redirect("~/SeedLineCompletionFinal.aspx");

                Response.Redirect(String.Format("~/SeedLineCompletionFinal.aspx?WOId={0}", WO));

            }

            if (e.CommandName == "Assign")
            {
                userinput.Visible = true;
                string WO1 = e.CommandArgument.ToString();
                WO = WO1;
                BindSeedlineLocation();
                ddlSeedlineLocation.Focus();
            }
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm("0");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            long result = 0;
            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@OperatorID", ddlOperator.SelectedValue);
            //nv.Add("@Notes", txtNotes.Text);
            //nv.Add("@JobID","");
            //nv.Add("@LoginID", Session["LoginID"].ToString());
            //nv.Add("@CropId","");
            //nv.Add("@UpdatedReadyDate", "");
            //nv.Add("@PlantExpirationDate","");
            //nv.Add("@RootQuality","");
            //nv.Add("@wo",wo);
            //nv.Add("@PlantHeight","");

            //nv.Add("@mode", "3");
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@loc_seedline", ddlSeedlineLocation.SelectedItem.Text);

            nv.Add("@WO", WO);
            result = objCommon.GetDataExecuteScaler("SP_UpdateProductionPlanner", nv);

            //lblmsg.Text = "Assignment Successful";
            clear();
            string message = "reassignment Successful";
            string url = "MyTaskProductionPlanner.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

        }
        public void clear()
        {
            ddlSeedlineLocation.SelectedIndex = 0;


        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskProductionPlanner.aspx");
        }

        #region DATASET HELPER  
        private bool ColumnEqual(object A, object B)
        {
            // Compares two values to see if they are equal. Also compares DBNULL.Value.             
            if (A == DBNull.Value && B == DBNull.Value) //  both are DBNull.Value  
                return true;
            if (A == DBNull.Value || B == DBNull.Value) //  only one is BNull.Value  
                return false;
            return (A.Equals(B)); // value type standard comparison  
        }
        public DataTable SelectDistinct(DataTable SourceTable, string FieldName)
        {
            // Create a Datatable – datatype same as FieldName  
            DataTable dt = new DataTable(SourceTable.TableName);
            dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);
            // Loop each row & compare each value with one another  
            // Add it to datatable if the values are mismatch  
            object LastValue = null;
            foreach (DataRow dr in SourceTable.Select("", FieldName))
            {
                if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
                {
                    LastValue = dr[FieldName];
                    dt.Rows.Add(new object[] { LastValue });
                }
            }
            return dt;
        }

        #endregion

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Evo"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //and t.[Location Code]= '" + Session["Facility"].ToString() + "'
                    //cmd.CommandText = "select distinct t.[Job No_] as jobcode  from[GTI$IA Job Tracking Entry] t, [GTI$Job] j where j.No_ = t.[Job No_] and j.[Job Status] = 2  " +
                    //" AND t.[Job No_] like '" + prefixText + "%'";


                    string Facility = HttpContext.Current.Session["Facility"].ToString();
                    cmd.CommandText = " select distinct jobcode from gti_jobs_seeds_plan where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%'";

                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(sdr["jobcode"].ToString());
                        }
                    }
                    conn.Close();

                    return customers;
                }
            }
        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

             
              
            }
        }
    }
}
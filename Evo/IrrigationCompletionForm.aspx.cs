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
    public partial class IrrigationCompletionForm1 : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Bindcname();
                //  BindJobCode();
                //  BindFacility();
                BindJobCode("0");
                BindJobHistoryDropdown();
                BindGridGerm(0,"0");
            }
        }

        private string benchLoc
        {
            get
            {
                if (Request.QueryString["benchLoc"] != null)
                {
                    return Request.QueryString["benchLoc"].ToString();
                }
                return "";
            }
            set
            {
                // JobCode = Request.QueryString["jobId"].ToString();
                // JobCode = value;
            }
        }

        private string TaskKey
        {
            get
            {
                if (Request.QueryString["Tkey"] != null)
                {
                    return Request.QueryString["Tkey"].ToString();
                }
                return "";
            }
            set
            {

            }
        }


        public void BindJobCode(string ddlBench)
        {
            //  ddlJobNo.Items[0].Selected = false;
            // ddlJobNo.ClearSelection();
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
            nv.Add("@BenchLocation", !string.IsNullOrEmpty(ddlBench) ? ddlBench : "0");
            nv.Add("@Customer", "0");
            nv.Add("@JobNo", "0");
            nv.Add("@GenusCode", "0");

            nv.Add("@Mode", "2");
            nv.Add("@Type", "Irr");

            dt = objCommon.GetDataTable("SP_TaskFilterSearchSupervisor", nv);
            //   ddlJobNo.DataSource = objBAL.GetJobsForBenchLocation(ddlBench);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataBind();

            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        }



        public void BindJobHistoryDropdown()
        {
           
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", "0");
            nv.Add("@jobcode","0");
            nv.Add("@AssignedBy", "0");
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());

            dt = objCommon.GetDataTable("SP_GetOperatorIrrigationTaskDetails", nv);

            //ddlJobNo.DataSource = SelectDistinct(dt, "Jobcode");
            //ddlJobNo.DataBind();
            //ddlJobNo.Items.Insert(0, new ListItem("--- Select ---", "0"));

            ddlBenchLocation.DataSource = SelectDistinct(dt, "BenchLocation");
            ddlBenchLocation.DataBind();
            ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));


            ddlAssignedBy.DataSource = SelectDistinct(dt, "AssignedBy");
            ddlAssignedBy.DataBind();
            ddlAssignedBy.Items.Insert(0, new ListItem("--- Select ---", "0"));

        }


        protected void ddlBenchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dataView = BindGridGerm(0,"0");
            BindJobCode(ddlBenchLocation.SelectedValue);
            //if (ddlJobNo.SelectedIndex == 0)
            //{
            //    ddlJobNo.DataSource = dataView.ToTable(true, "Jobcode");
            //    ddlJobNo.DataBind();
            //    ddlJobNo.Items.Insert(0, new ListItem("--- Select ---", "0"));
            //}
            if (ddlBenchLocation.SelectedIndex == 0)
            {
                ddlBenchLocation.DataSource = dataView.ToTable(true, "BenchLocation");
                ddlBenchLocation.DataBind();
                ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
            if (ddlAssignedBy.SelectedIndex == 0)
            {
                ddlAssignedBy.DataSource = dataView.ToTable(true, "AssignedBy");
                ddlAssignedBy.DataBind();
                ddlAssignedBy.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm(0, ddlJobNo.SelectedValue);
        }

        protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dataView = BindGridGerm(0,"0");

            //if (ddlJobNo.SelectedIndex == 0)
            //{
            //    ddlJobNo.DataSource = dataView.ToTable(true, "Jobcode");
            //    ddlJobNo.DataBind();
            //    ddlJobNo.Items.Insert(0, new ListItem("--- Select ---", "0"));
            //}
            if (ddlBenchLocation.SelectedIndex == 0)
            {
                ddlBenchLocation.DataSource = dataView.ToTable(true, "BenchLocation");
                ddlBenchLocation.DataBind();
                ddlBenchLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
            if (ddlAssignedBy.SelectedIndex == 0)
            {
                ddlAssignedBy.DataSource = dataView.ToTable(true, "AssignedBy");
                ddlAssignedBy.DataBind();
                ddlAssignedBy.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }
        }





        public DataView BindGridGerm(int p,string JobNo)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation",ddlBenchLocation.SelectedValue);
            nv.Add("@jobcode", JobNo);
            nv.Add("@AssignedBy", ddlAssignedBy.SelectedValue);
            nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@Facility", Session["Facility"].ToString());
          
            dt = objCommon.GetDataTable("SP_GetOperatorIrrigationTaskDetails", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

            DataView dataView = dt.DefaultView;
           

            if (p != 1 && !string.IsNullOrEmpty(benchLoc))
            {
                highlight(dt.Rows.Count);
            }

            return dataView;
        }
        private void highlight(int limit)
        {
            var i = gvGerm.Rows.Count;
            bool check = false;
            foreach (GridViewRow row in gvGerm.Rows)
            {
                //var checkJob = (row.FindControl("lbljobID") as Label).Text;
                var checklocation = (row.FindControl("lblGreenHouseID") as Label).Text;
                var tKey = gvGerm.DataKeys[row.RowIndex].Values[1].ToString();
                i--;
                if (checklocation == benchLoc && tKey == TaskKey)
                {
                    row.CssClass = "highlighted";
                    check = true;
                }
                if (i == 0 && !check && limit >= 10)
                {
                    gvGerm.PageIndex++;
                    gvGerm.DataBind();
                    highlight((limit - 10));
                }
            }
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


        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string JobID = "";


            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string IrrigationCode = gvGerm.DataKeys[rowIndex].Values[0].ToString();
                string TaskRequestKey = gvGerm.DataKeys[rowIndex].Values[1].ToString();
               
                Response.Redirect(String.Format("~/IrrigationTaskCompletion.aspx?IrrigationCode={0}", IrrigationCode));
            }

            if (e.CommandName == "ViewDetails")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string IrrigationCode = gvGerm.DataKeys[rowIndex].Values[0].ToString();
                string TaskRequestKey = gvGerm.DataKeys[rowIndex].Values[1].ToString();
                Response.Redirect(String.Format("~/IrrigationTaskViewDetails.aspx?PageType={0}&IrrigationCode={1}&ICID={2}&TaskRequestKey={3}", "", IrrigationCode, 0, TaskRequestKey));

               
            }
        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm(1,"0");
        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGermDate = (Label)e.Row.FindControl("lblSprayDate");
                string dtimeString = Convert.ToDateTime(lblGermDate.Text).ToString("yyyy/MM/dd");

                DateTime dtime = Convert.ToDateTime(dtimeString);

                DateTime nowtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));

                if (nowtime > dtime)
                {
                    e.Row.CssClass = "overdue";
                }

               
            }
        }
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
                    cmd.CommandText = " select distinct GPD.jobcode from gti_jobs_seeds_plan GTS inner join GrowerPutAwayDetails GPD on GPD.wo=GTS.wo  where  GPD.FacilityID ='" + Facility + "'  AND GPD.jobcode like '%" + prefixText + "%' union select distinct jobcode from gti_jobs_seeds_plan_Manual where loc_seedline ='" + Facility + "'  AND jobcode like '%" + prefixText + "%' order by jobcode" +
                     "";

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

        protected void txtSearchJobNo_TextChanged(object sender, EventArgs e)
        {
            BindGridGerm(0, txtSearchJobNo.Text);
        }

        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            txtSearchJobNo.Text = "";
            BindJobHistoryDropdown();
            BindGridGerm(0, "0");
        }
    }
}
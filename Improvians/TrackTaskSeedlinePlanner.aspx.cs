using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class TrackTaskSeedlinePlanner : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        public static DataTable AllData = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcname();
                BindJobCode();
                BindFacility();
                BindCropType();
            
                BindGridGerm();
            }
        }

        public void Bindcname()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "27");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "cname";
            ddlCustomer.DataValueField = "cname";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindCropType()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "26");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlCopTYpe.DataSource = dt;
            ddlCopTYpe.DataTextField = "GenusCode";
            ddlCopTYpe.DataValueField = "GenusCode";
            ddlCopTYpe.DataBind();
            ddlCopTYpe.Items.Insert(0, new ListItem("--Select--", "0"));

        }


        public void BindJobCode()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "7");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataTextField = "Jobcode";
            ddlJobNo.DataValueField = "Jobcode";
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        public void BindFacility()
        {

            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();

            nv.Add("@Mode", "9");
            dt = objCommon.GetDataTable("GET_Common", nv);
            ddlFacility.DataSource = dt;
            ddlFacility.DataTextField = "loc_seedline";
            ddlFacility.DataValueField = "loc_seedline";
            ddlFacility.DataBind();
            ddlFacility.Items.Insert(0, new ListItem("--Select--", "0"));
            if (Session["Role"].ToString() == "10")
            {
                ddlFacility.SelectedItem.Text = Session["Facility"].ToString();
                FID.Visible = false;
            }
            else
            {
                FID.Visible = true;
            }
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }




        protected void btnSearchRest_Click(object sender, EventArgs e)
        {
            BindCropType();
            Bindcname();
            BindJobCode();
            BindFacility();
            BindGridGerm();

        }

        public void BindGridGerm()
        {



            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            // nv.Add("@LoginID", Session["LoginID"].ToString());
            nv.Add("@JobCode", ddlJobNo.SelectedValue);
            nv.Add("@CustomerName", ddlCustomer.SelectedValue);
            nv.Add("@Facility", ddlFacility.SelectedValue);
            nv.Add("@CropTYpe", ddlCopTYpe.SelectedValue);

            AllData = objCommon.GetDataTable("SP_GetTrackTaskSeedlinePlanner", nv);
            gvGerm.DataSource = AllData;
            gvGerm.DataBind();


        }



        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvGerm_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblstatus = (Label)e.Row.FindControl("lblstatus");
                Label lblStatusValues = (Label)e.Row.FindControl("lblStatusValues");
                Label lblPudawayDate = (Label)e.Row.FindControl("lblPudawayDate");
                Label lblPutawayStatusValues = (Label)e.Row.FindControl("lblPutawayStatusValues");
                if (lblStatusValues.Text == "1" || lblStatusValues.Text == "2")
                {
                    lblstatus.Text = "Completed";
                }
                else
                {
                    lblstatus.Text = "Pending";
                }

                if (lblStatusValues.Text == "2")
                {
                    lblPudawayDate.Text = lblPudawayDate.Text;
                }
                else
                {
                    lblPudawayDate.Text = "Pending";
                }

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindGridGerm();
                string search = "";
                //if (txtSearch.Text != "")
                //{

                string SeedlineStatus = "", PutAwayStatus = "";

                //if (radJSeedlineStatus.SelectedValue == "2")
                //{
                //    SeedlineStatus = "";
                //}
                //else if (radJSeedlineStatus.SelectedValue == "1")
                //{
                //    SeedlineStatus = "1,2";
                //}
                //else
                //{
                //    SeedlineStatus = "0";
                //}

                //if (RadioPutAwayStatus.SelectedValue == "2")
                //{
                //    PutAwayStatus = "";
                //}
                //else if (RadioPutAwayStatus.SelectedValue == "1")
                //{
                //    PutAwayStatus = "2";
                //}
                //else
                //{
                //    PutAwayStatus = "0,1";
                //}



                if (radJSeedlineStatus.SelectedValue == "2")
                {
                    search += "jstatus in (1,2)";
                }
                else if (radJSeedlineStatus.SelectedValue == "1")
                {
                    search += "jstatus in (0)";
                }
                else
                {
                 //   search += "jstatus in (0,1,2)";
                }

               
                if (RadioPutAwayStatus.SelectedValue == "2")
                {
                    search += "jstatus in (2)";
                }
                else if (RadioPutAwayStatus.SelectedValue == "2")
                {
                    search += "jstatus in (0,1)";
                }
                else
                {
                   // search += "jstatus in (0,1,2)";
                }


                DataRow[] dtSearch1 = AllData.Select(search);
                if (dtSearch1.Count() > 0)
                {
                    DataTable dtSearch = dtSearch1.CopyToDataTable();
                    gvGerm.DataSource = dtSearch;
                    gvGerm.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    gvGerm.DataSource = dt;
                    gvGerm.DataBind();
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlCopTYpe_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGerm();
        }
    }
}
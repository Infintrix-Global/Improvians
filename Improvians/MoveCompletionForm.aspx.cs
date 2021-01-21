using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class MoveCompletionForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridMove();
                BindToLocation();
            }
        }
        public void BindToLocation()
        {
                NameValueCollection nv = new NameValueCollection();
        nv.Add("@FacilityID", lblToFacility.Text);
            ddlLocation.DataSource = objCommon.GetDataTable("SP_GetGreenhouseByFacility", nv); ;
            ddlLocation.DataTextField = "GreenHouseName";
            ddlLocation.DataValueField = "GreenHouseID";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
            }

    public void BindGridMove()
        {
            DataSet dt = new DataSet();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@MoveID", Session["MoveID"].ToString());
            dt = objCommon.GetDataSet("SP_GetShipmentCoordinatorTaskByMoveID", nv);
            gvMove.DataSource = dt.Tables[0];
            gvMove.DataBind();
            lblToFacility.Text = dt.Tables[0].Rows[0]["FacilityToID"].ToString();
            if (string.IsNullOrEmpty(dt.Tables[1].Rows[0]["CompletedTrays"].ToString()))
            {

                lblRemainingTrays.Text =dt.Tables[0].Rows[0]["TraysRequest"].ToString() ;

            }
            else
            {
                lblRemainingTrays.Text = (Convert.ToInt32(dt.Tables[0].Rows[0]["TraysRequest"].ToString())- Convert.ToInt32(dt.Tables[1].Rows[0]["CompletedTrays"].ToString())).ToString();
            }
        }

        protected void gvMove_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMove.PageIndex = e.NewPageIndex;
            BindGridMove();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtTrays.Text) <= Convert.ToDouble(lblRemainingTrays.Text))
            {
                long result = 0;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@MoveTaskID", Session["MoveID"].ToString());
                nv.Add("@GrenHouseToCompletion", ddlLocation.SelectedValue);
                nv.Add("@TraysCompletion", txtTrays.Text);
                nv.Add("@CompletionDate", txtMoveDate.Text);
                nv.Add("@LoginID", Session["LoginID"].ToString());
                result = objCommon.GetDataInsertORUpdate("SP_AddMoveCompletion", nv);
                if (result > 0)
                {
                    lblmsg.Text = "Completion Successful";
                    Clear();
                    if (Session["Role"].ToString() == "6")
                    {
                        Response.Redirect("~/MyTaskShippingCoordinator.aspx");
                    }

                    else if (Session["Role"].ToString() == "5")
                    {
                        Response.Redirect("~/MyTaskLogisticManager.aspx");
                    }
                }
                else
                {
                    lblmsg.Text = "Completion Not Successful";
                }
            }
            else
            {

                lblerrmsg.Text = "Number of Trays exceed Remaining trays";

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtMoveDate.Text = "";
            txtTrays.Text = "";
            ddlLocation.SelectedIndex = 0;
        }
    }
}
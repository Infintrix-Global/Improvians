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
                if (Request.QueryString["Wid"] != null)
                {
                    wo = Request.QueryString["Wid"].ToString();
                }
                txtMoveDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");

                BindGridMove();
                BindToLocation();
            }
        }

        private string wo
        {
            get
            {
                if (ViewState["wo"] != null)
                {
                    return (string)ViewState["wo"];
                }
                return "";
            }
            set
            {
                ViewState["wo"] = value;
            }
        }

        private string GrowerPutAwayId
        {
            get
            {
                if (ViewState["GrowerPutAwayId"] != null)
                {
                    return (string)ViewState["GrowerPutAwayId"];
                }
                return "";
            }
            set
            {
                ViewState["GrowerPutAwayId"] = value;
            }
        }

        public void BindToLocation()
        {
            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("@FacilityID", lblToFacility.Text);
            //ddlLocation.DataSource = objCommon.GetDataTable("SP_GetGreenhouseByFacility", nv); ;
            //ddlLocation.DataTextField = "GreenHouseName";
            //ddlLocation.DataValueField = "GreenHouseID";
            //ddlLocation.DataBind();
            //ddlLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }

        public void BindGridMove()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@WoId", wo);
            nv.Add("@mode", "3");
            dt = objCommon.GetDataTable("SP_GetGrowerPutAwayLogisticManagerAssignedJobByMoveID", nv);
            gvMove.DataSource = dt;
            gvMove.DataBind();


            //lblToFacility.Text = dt.Tables[0].Rows[0]["FacilityToID"].ToString();
            //if (string.IsNullOrEmpty(dt.Tables[1].Rows[0]["CompletedTrays"].ToString()))
            //{

            //    lblRemainingTrays.Text =dt.Tables[0].Rows[0]["TraysRequest"].ToString() ;

            //}
            //else
            //{
            //    lblRemainingTrays.Text = (Convert.ToInt32(dt.Tables[0].Rows[0]["TraysRequest"].ToString())- Convert.ToInt32(dt.Tables[1].Rows[0]["CompletedTrays"].ToString())).ToString();
            //}
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
                nv.Add("@GrowerPutAwayId", GrowerPutAwayId);
                nv.Add("@Wo",wo);
                nv.Add("@MoveDate", txtMoveDate.Text.Trim());
                nv.Add("@Put_Away_Location",txtPutAwayLocation.Text.Trim());
                nv.Add("@TraysMoved",txtTrays.Text.Trim());
                nv.Add("@Barcode", txtBarcode.Text.Trim());

                nv.Add("@CreateBy", Session["LoginID"].ToString());
                result = objCommon.GetDataInsertORUpdate("SP_AddMoveCompletionDetails", nv);
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
           // ddlLocation.SelectedIndex = 0;
        }

        protected void gvMove_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select1")
            {
                AddDetails.Visible = true;
                string traysTotal = "";
                int rowIndex = Convert.ToInt32(e.CommandArgument) -1;
                GridViewRow row = gvMove.Rows[rowIndex];
                GrowerPutAwayId = rowIndex.ToString();


                txtPutAwayLocation.Text = (row.FindControl("lblGreenHouseName") as Label).Text;

             //   traysTotal = (row.FindControl("lblTraysRequest") as Label).Text;

                lblRemainingTrays.Text = (row.FindControl("lblTraysRequest") as Label).Text;


                txtMoveDate.Focus();
            }
        }

        protected void txtTrays_TextChanged(object sender, EventArgs e)
        {

            if (txtTrays.Text != "")
            {
                
                lblRemainingTrays.Text = (Convert.ToInt32(lblRemainingTrays.Text) - Convert.ToInt32(txtTrays.Text)).ToString();
               
            }
        }

        protected void gvMove_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                Label lblGrowerPutAwayId = (Label)e.Row.FindControl("lblGrowerPutAwayId");
                Label lblTray = (Label)e.Row.FindControl("lblTray");
                Label lblTraysRequest = (Label)e.Row.FindControl("lblTraysRequest");

                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@WoId", lblGrowerPutAwayId.Text);
                nv.Add("@mode", "2");
                dt = objCommon.GetDataTable("SP_GetGrowerPutAwayLogisticManagerAssignedJobByMoveID", nv);

                lblTraysRequest.Text = (Convert.ToInt32(lblTray.Text) - Convert.ToInt32(dt.Rows[0]["TraysMovedTotal"])).ToString();

                if(Convert.ToUInt32(lblTray.Text) ==Convert.ToUInt32(lblTraysRequest.Text))
                {
                    NameValueCollection nv1 = new NameValueCollection();
                    nv1.Add("@WoId", lblGrowerPutAwayId.Text);
                    nv1.Add("@mode", "4");

                  int  result = objCommon.GetDataInsertORUpdate("SP_GetGrowerPutAwayLogisticManagerAssignedJobByMoveID", nv1);
                }

            }
        }
    }
}
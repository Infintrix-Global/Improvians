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
    public partial class PutAwayTaskCompletion : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridGerm();
            }
        }

        

        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            dt = objCommon.GetDataTable("SP_GetPutAwayTaskCompletion", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        protected void gvGerm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerm.PageIndex = e.NewPageIndex;
            BindGridGerm();
        }

       

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            if(e.CommandName=="Assign")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                            GridViewRow row = gvGerm.Rows[rowIndex];
                      string benchID = (row.FindControl("ddlBench") as DropDownList).SelectedValue;
                           string id = (row.FindControl("lblID") as Label).Text;
                if (benchID == "0")
                {
                    lblmsg.Text = "Failed-Please Select Bench Location ";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@PTCID", id);
                    nv.Add("@BenchLocation", benchID);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    result = objCommon.GetDataInsertORUpdate("SP_UpdateProductionPlannerCompletion", nv);
                    string message = "Bench Location Assignment Successful";
                    string url = "PutAwayTaskCompletion.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                }
            }

            if (e.CommandName == "Verify")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvGerm.Rows[rowIndex];
                string benchID = (row.FindControl("ddlBench") as DropDownList).SelectedValue;
                string id = (row.FindControl("lblID") as Label).Text;
                if (benchID == "0")
                {
                    lblmsg.Text = "Failed-Please Select Bench Location ";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@PTCID", id);
                    nv.Add("@BenchLocation", benchID);
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    result = objCommon.GetDataInsertORUpdate("SP_VerifyProductionPlannerCompletion", nv);
                    string message = "Bench Location Verification Successful";
                    string url = "PutAwayTaskCompletion.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                }
            }
         }

        protected void gvGerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@FacilityID", ((Label)e.Row.FindControl("lblFacility")).Text);
                ((DropDownList)e.Row.FindControl("ddlBench")).DataSource = objCommon.GetDataTable("SP_GetGreenhouseByFacility", nv); ;
                ((DropDownList)e.Row.FindControl("ddlBench")).DataTextField = "GreenHouseName";
                ((DropDownList)e.Row.FindControl("ddlBench")).DataValueField = "GreenHouseID";
                ((DropDownList)e.Row.FindControl("ddlBench")).DataBind();
                ((DropDownList)e.Row.FindControl("ddlBench")).Items.Insert(0, new ListItem("--- Select ---", "0"));


                if (((Label)e.Row.FindControl("lblStatus")).Text == "Completed")
                {
                    e.Row.BackColor = System.Drawing.Color.Green;
                    if (((Label)e.Row.FindControl("lblBench")).Text != "")
                    {
                        ((DropDownList)e.Row.FindControl("ddlBench")).SelectedValue = ((Label)e.Row.FindControl("lblBench")).Text;
                        ((Button)e.Row.FindControl("btnAssign")).Text = "Verify";
                        ((Button)e.Row.FindControl("btnAssign")).CommandName = "Verify";

                    }
                    else
                    {
                        ((Button)e.Row.FindControl("btnAssign")).Text = "Assign";
                        ((Button)e.Row.FindControl("btnAssign")).CommandName = "Assign";

                    }
                }

                else if (((Label)e.Row.FindControl("lblStatus")).Text == "Not Complete")
                {
                    if (((Label)e.Row.FindControl("lblBench")).Text != "")
                    {
                        ((Button)e.Row.FindControl("btnAssign")).Visible = false;
                        ((DropDownList)e.Row.FindControl("ddlBench")).SelectedValue = ((Label)e.Row.FindControl("lblBench")).Text;
                        ((DropDownList)e.Row.FindControl("ddlBench")).Enabled = false;
                       // ((Button)e.Row.FindControl("btnAssign")).Text = "Verify";
                       //   ((Button)e.Row.FindControl("btnAssign")).CommandName = "Verify";

                    }
                    else
                    {
                         ((Button)e.Row.FindControl("btnAssign")).Text = "Assign";
                        ((Button)e.Row.FindControl("btnAssign")).CommandName = "Assign";
                       
                    }
                }

                else if (((Label)e.Row.FindControl("lblStatus")).Text == "Verified")
                {
                    ((Button)e.Row.FindControl("btnAssign")).Visible = false;
                    ((DropDownList)e.Row.FindControl("ddlBench")).SelectedValue = ((Label)e.Row.FindControl("lblBench")).Text;
                    ((DropDownList)e.Row.FindControl("ddlBench")).Enabled = false;
                }



            }
        }




    }
}
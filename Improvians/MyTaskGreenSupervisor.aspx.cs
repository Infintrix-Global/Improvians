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
    public partial class MyTaskGreenSupervisor : System.Web.UI.Page
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
            nv.Add("@LoginID", Session["LoginID"].ToString());
            dt = objCommon.GetDataTable("SP_GetGreenHouseSupervisorTask", nv);
            gvGerm.DataSource = dt;
            gvGerm.DataBind();

        }

        protected void gvGerm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string JobID = "";
            string TaskID = "";
            if (e.CommandName == "Assign")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvGerm.Rows[rowIndex];

                 JobID = (row.FindControl("lblID") as Label).Text;
                 TaskID = (row.FindControl("HiddenFieldTaskID") as HiddenField).Value;


                if (Session["Role"].ToString() == "2")
                {
                    if (TaskID == "5")
                    {
                        Session["JobID"] = JobID;
                        Response.Redirect("~/GerminationTaskAssignment.aspx");
                    }
                    else if(TaskID == "11")
                    {
                        Session["JobID"] = JobID;
                        Response.Redirect("~/PlantReadyTaskAssignment.aspx");
                    }
                }

            }
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvGerm.Rows[rowIndex];

                JobID = (row.FindControl("lblID") as Label).Text;
                TaskID = (row.FindControl("HiddenFieldTaskID") as HiddenField).Value;


                if (Session["Role"].ToString() == "2")
                {
                    long result = 0;
                 

                    if (TaskID == "5")
                    {
                        NameValueCollection nv = new NameValueCollection();
                        nv.Add("@OperatorID", Session["LoginID"].ToString());
                        nv.Add("@Notes", "");
                        nv.Add("@JobID", JobID);
                        nv.Add("@LoginID", Session["LoginID"].ToString());
                        result = objCommon.GetDataInsertORUpdate("SP_AddGerminationAssignment", nv);

                        Session["JobID"] = JobID;
                        Response.Redirect("~/GreenHouseTaskCompletion.aspx");
                    }
                    else if(TaskID == "11")
                    {

                     
                        NameValueCollection nv = new NameValueCollection();
                        nv.Add("@OperatorID", Session["LoginID"].ToString());
                        nv.Add("@Notes","");
                        nv.Add("@JobID", JobID);
                        nv.Add("@LoginID", Session["LoginID"].ToString());
                        nv.Add("@CropId","");
                        nv.Add("@UpdatedReadyDate", "");
                        nv.Add("@PlantExpirationDate", "");
                        nv.Add("@RootQuality", "");

                        nv.Add("@PlantHeight", "");

                        nv.Add("@mode", "3");

                        result = objCommon.GetDataInsertORUpdate("SP_AddPlantReadyTaskAssignment", nv);



                        Session["JobID"] = JobID;
                   //     Response.Redirect("~/PlantReadyTaskCompletion.aspx?Tid={0}",1);
                        Response.Redirect(String.Format("~/PlantReadyTaskCompletion.aspx?Fid={0}", 1));

                    }
                    else
                    {

                    }

                }
                if (Session["Role"].ToString() == "5")
                {

                }
            }
        }
    }
}
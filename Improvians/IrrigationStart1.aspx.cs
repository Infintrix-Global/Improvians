using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Bal;
using System.Text.RegularExpressions;
using Evo.BAL_Classes;
namespace Evo
{
    public partial class IrrigationStart : System.Web.UI.Page
    {


        CommonControl objCommon = new CommonControl();
        BAL_Fertilizer objFer = new BAL_Fertilizer();
        BAL_Task objTask = new BAL_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Bench"] != null)
                {
                    Bench = Request.QueryString["Bench"].ToString();
                }
                if (Request.QueryString["jobCode"] != null)
                {
                    JobCode = Request.QueryString["jobCode"].ToString();
                }
                if (Request.QueryString["ICode"] != null)
                {
                    ICode = Request.QueryString["ICode"].ToString();
                }

                txtSprayDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
              
               
               
          

                if (Request.QueryString["ICode"].ToString() != "0")
                {
                    BindGridIrrDetailsViewReq();
                }
                else
                {
                    BindGridIrrDetails("'" + Bench + "'", "'" + JobCode + "'");
                   
                }
            }
        }
        private string ICode
        {
            get
            {
                if (ViewState["ICode"] != null)
                {
                    return (string)ViewState["ICode"];
                }
                return "";
            }
            set
            {
                ViewState["ICode"] = value;
            }
        }

        private string Bench
        {
            get
            {
                if (ViewState["Bench"] != null)
                {
                    return (string)ViewState["Bench"];
                }
                return "";
            }
            set
            {
                ViewState["Bench"] = value;
            }
        }
        private string JobCode
        {
            get
            {
                if (ViewState["JobCode"] != null)
                {
                    return (string)ViewState["JobCode"];
                }
                return "";
            }
            set
            {
                ViewState["JobCode"] = value;
            }
        }

        private string Jid
        {
            get
            {
                if (ViewState["Jid"] != null)
                {
                    return (string)ViewState["Jid"];
                }
                return "";
            }
            set
            {
                ViewState["Jid"] = value;
            }
        }


    
        public void BindGridIrrDetailsViewReq()
        {
           

            int P = 0;
            string Q = "";
            string bjno = "";
            string chkSelected = "";
            string JobNo = "";
            DataSet dt = new DataSet();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@ICode", ICode);

            dt = objCommon.GetDataSet("SP_GetIrrigationTaskAssignmentView", nv);

            if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            {
                  txtNotes.Text = dt.Tables[0].Rows[0]["Nots"].ToString();
                  txtResetSprayTaskForDays.Text = dt.Tables[0].Rows[0]["ResetSprayTaskForDays"].ToString();
                  txtSprayDate.Text = Convert.ToDateTime(dt.Tables[0].Rows[0]["SprayDate"]).ToString("yyyy-MM-dd");
                  txtWaterRequired.Text = dt.Tables[0].Rows[0]["WaterRequired"].ToString();
            }


            if (dt.Tables[1] != null && dt.Tables[1].Rows.Count > 0)
            {
                DataColumn col = dt.Tables[1].Columns["GreenHouseID"];
                DataColumn col1 = dt.Tables[1].Columns["JobCode"];
                foreach (DataRow row in dt.Tables[1].Rows)
                {
                    //strJsonData = row[col].ToString();

                    P = 1;
                    Q += "'" + row[col].ToString() + "',";

                    bjno += "'" + row[col1].ToString() + "',";
                }
                if (P > 0)
                {
                    chkSelected = Q.Remove(Q.Length - 1, 1);
                    JobNo = bjno.Remove(bjno.Length - 1, 1);

                }
                else
                {

                }
            }

            BindGridIrrDetails(chkSelected, JobNo);

            //BindSQFTofBench(chkSelected);
        }


        public void BindGridIrrDetails(string BenchLoc ,string JobNo)
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@BenchLocation", BenchLoc);
            dt = objCommon.GetDataTable("SP_GetIrrigationRequestSelect", nv);
            //   DataTable dtManual = objFer.GetManualFertilizerRequestSelect("", BenchLoc, JobNo);
           
            DataTable dtManual = objTask.GetManualRequestStart(Session["Facility"].ToString(), BenchLoc, JobNo);
            if (dt != null && dt.Rows.Count > 0 && dtManual != null && dtManual.Rows.Count > 0)
            {
                dt.Merge(dtManual);
                dt.AcceptChanges();
                gvJobHistory.DataSource = dt;
                gvJobHistory.DataBind();

            }
            else if (dtManual != null && dtManual.Rows.Count > 0)
            {
                gvJobHistory.DataSource = dtManual;
                gvJobHistory.DataBind();

            }
            else
            {
                gvJobHistory.DataSource = dt;
                gvJobHistory.DataBind();


            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int IrrigationCode = 0;
            DataTable dt = new DataTable();
            NameValueCollection nv1 = new NameValueCollection();
            nv1.Add("@Mode", "13");
            dt = objCommon.GetDataTable("GET_Common", nv1);
            IrrigationCode = Convert.ToInt32(dt.Rows[0]["ICode"]);



            foreach (GridViewRow row in gvJobHistory.Rows)
            {

                    long result = 0;
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@SupervisorID", Session["LoginID"].ToString());

                    nv.Add("@GrowerPutAwayID", (row.FindControl("lblGrowerputawayID") as Label).Text);
                    nv.Add("@IrrigatedNoTrays", (row.FindControl("lbltotTray") as Label).Text);
                   
                    nv.Add("@WaterRequired", txtWaterRequired.Text.Trim());
                    nv.Add("@IrrigationDuration", "");
                    nv.Add("@SprayDate", txtSprayDate.Text.Trim());
                    nv.Add("@SprayTime", "");
                    nv.Add("@Nots", txtNotes.Text.Trim());
                    nv.Add("@GreenHouseID", (row.FindControl("lblGreenHouse") as Label).Text);
                    nv.Add("@IrrigationCode", IrrigationCode.ToString());
                    nv.Add("@LoginID", Session["LoginID"].ToString());
                    nv.Add("@NoOfPasses", "");
                    nv.Add("@ResetSprayTaskForDays", "");
                    nv.Add("@Jid", (row.FindControl("lbljid") as Label).Text);

                   result = objCommon.GetDataInsertORUpdate("SP_AddIrrigationRequestStart", nv);
              


            }
            string url = "";
            if (Session["Role"].ToString() == "1")
            {
                url = "MyTaskGrower.aspx";
            }
            else
            {
                url = "MyTaskAssistantGrower.aspx";
            }

            string message = "Assignment Successful";
            // string url = "MyTaskGrower.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
         
            clear();
        }

        public void clear()
        {

         
            txtWaterRequired.Text = "";
            txtNotes.Text = "";
            //  txtIrrigatedNoTrays.Text = "";
            //txtIrrigationDuration.Text = "";
            txtSprayDate.Text = "";
            // txtSprayTime.Text = "";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("~/MyTaskGrower.aspx");
        }


    }
}
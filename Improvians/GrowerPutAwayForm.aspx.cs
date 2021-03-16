using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evo.Bal;

namespace Evo
{
    public partial class GetGrowerPutAwayForm : System.Web.UI.Page
    {
        CommonControl objCommon = new CommonControl();
        BAL_CommonMasters objCOm = new BAL_CommonMasters();
        Bal_SeedingPlan objSP = new Bal_SeedingPlan();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSupervisorList();
                BindGridGerm();
            }
        }

        private string PutAwayFacility
        {
            get
            {
                if (ViewState["PutAwayFacility"] != null)
                {
                    return (string)ViewState["PutAwayFacility"];
                }
                return "";
            }
            set
            {
                ViewState["PutAwayFacility"] = value;
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

        private string TraySize
        {
            get
            {
                if (ViewState["TraySize"] != null)
                {
                    return (string)ViewState["TraySize"];
                }
                return "";
            }
            set
            {
                ViewState["TraySize"] = value;
            }
        }

        public void BindSupervisorList()
        {
            NameValueCollection nv = new NameValueCollection();
            if (Session["Role"].ToString() == "1")
            {
                ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlSupervisor.DataTextField = "EmployeeName";
                ddlSupervisor.DataValueField = "ID";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            if (Session["Role"].ToString() == "12")
            {
                ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetRoleForAssistantGrower", nv);
                //ddlSupervisor.DataSource = objCommon.GetDataTable("SP_GetGreenHouseSupervisor", nv); ;
                ddlSupervisor.DataTextField = "EmployeeName";
                ddlSupervisor.DataValueField = "ID";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }


        public void BindGridGerm()
        {
            DataTable dt = new DataTable();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("@Mode", "1");
            nv.Add("@wo", "");
            dt = objCommon.GetDataTable("SP_GetGrowerPutAway", nv);
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

            if (e.CommandName == "Assign")
            {

                // invoiceSplitJob();
                PanelAdd.Visible = true;
                PanelList.Visible = false;
                string wo_No = e.CommandArgument.ToString();
                wo = wo_No;
                DataTable dt = new DataTable();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@Mode", "2");
                nv.Add("@wo", wo_No);
                dt = objCommon.GetDataTable("SP_GetGrowerPutAway", nv);

                if (dt != null && dt.Rows.Count > 0)
                {
                    lblJobID.Text = dt.Rows[0]["JobID"].ToString();
                    lblSeedDate.Text = Convert.ToDateTime(dt.Rows[0]["SeededDate"]).ToString("MM-dd-yyyy");
                    lblSeededTrays.Text = dt.Rows[0]["ActualTraySeeded"].ToString();
                    PutAwayFacility = dt.Rows[0]["loc_seedline"].ToString();
                    lblGenusCode.Text = dt.Rows[0]["GenusCode"].ToString();
                    TraySize = dt.Rows[0]["TraySize"].ToString();
                }

                AddGrowerPutRow(true);

            }


        }


        protected void ButtonAddGridInvoice_Click(object sender, EventArgs e)
        {
            //  AddNewRowToGridSetInitialInvoice();
            AddGrowerPutRow(true);

        }

        protected void GridSplitJob_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlMain = (DropDownList)e.Row.FindControl("ddlMain");
                DropDownList ddlLocation = (DropDownList)e.Row.FindControl("ddlLocation");
                Label lblMain = (Label)e.Row.FindControl("lblMain");
                Label lblLocation = (Label)e.Row.FindControl("lblLocation");


                //  NameValueCollection nv = new NameValueCollection();
                //   nv.Add("@mode", "4");
                //ddlMain.DataSource = objCommon.GetDataTable("GET_Common", nv); 
                ddlMain.DataSource = objCOm.GetMainLocation();
                ddlMain.DataTextField = "l1";
                ddlMain.DataValueField = "l1";
                ddlMain.DataBind();
                ddlMain.Items.Insert(0, new ListItem("--- Select ---", "0"));
                // ddlLocation.SelectedValue = "ENC1";
                //  BindLocation();
                BindLocationNew(ref ddlLocation, PutAwayFacility);
                ddlMain.SelectedValue = PutAwayFacility;
                ddlLocation.SelectedValue = lblLocation.Text;

            }
        }

        protected void ddlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            // BindLocation();
            DropDownList ddlMain = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlMain.NamingContainer;
            if (row != null)
            {

                DropDownList ddlLocation = (DropDownList)row.FindControl("ddlLocation");
                //  NameValueCollection nv = new NameValueCollection();
                //  nv.Add("@FacilityID", ddlMain.SelectedValue);
                //   ddlLocation.DataSource = objCommon.GetDataTable("SP_GetGreenhouseByFacility", nv); ;
                ddlLocation.DataSource = objCOm.GetLocation(ddlMain.SelectedValue);
                ddlLocation.DataTextField = "p2";
                ddlLocation.DataValueField = "p2";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));

            }
        }

        public void BindLocation()
        {


            foreach (GridViewRow row in GridSplitJob.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    DropDownList ddlLocation = (row.Cells[0].FindControl("ddlLocation") as DropDownList);
                    DropDownList ddlMain = (row.Cells[0].FindControl("ddlMain") as DropDownList);

                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("@FacilityID", ddlMain.SelectedValue);
                    ddlLocation.DataSource = objCommon.GetDataTable("SP_GetGreenhouseByFacility", nv); ;
                    ddlLocation.DataTextField = "GreenHouseName";
                    ddlLocation.DataValueField = "GreenHouseID";
                    ddlLocation.DataBind();
                    ddlLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));

                    //if(A=="")
                    //{
                    //    A = "0";
                    //}

                    //ddlLocation.SelectedValue = A;

                }

            }




        }



        public void BindLocationNew(ref DropDownList ddlLocation, string ddlMain)
        {

            //   NameValueCollection nv = new NameValueCollection();
            //    nv.Add("@FacilityID", ddlMain);
            //ddlLocation.DataSource = objCommon.GetDataTable("SP_GetGreenhouseByFacility", nv);
            ddlLocation.DataSource = objCOm.GetLocation(ddlMain);
            ddlLocation.DataTextField = "p2";
            ddlLocation.DataValueField = "p2";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }



        protected void txtTrays_TextChanged(object sender, EventArgs e)
        {
            CalData();
        }

        public void CalData()
        {

            int Total = 0;

            foreach (GridViewRow row in GridSplitJob.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    TextBox txtTrays = (row.Cells[0].FindControl("txtTrays") as TextBox);


                    //  TexTotal = Total * Convert.ToInt32(ddlTAX.SelectedItem.Text) / 100;
                    if (txtTrays.Text != "")
                    {
                        Total += Convert.ToInt32(txtTrays.Text);
                    }


                }
            }

            lblRemaining.Text = (Convert.ToInt32(lblSeededTrays.Text) - Total).ToString();


            //if (Convert.ToDouble(lblSeededTrays.Text) <  Convert.ToDouble(lblRemaining.Text))
            //{
            //   
            //}
            //else
            //{

            //}

        }

        public void Clear()
        {
            BindGridGerm();
            PanelAdd.Visible = false;
            PanelList.Visible = true;
            GridSplitJob.DataSource = null;
            GridSplitJob.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                long _isInserted = 1;
                int SelectedItems = 0;

                string IrrigateSeedDate = "";
                string FertilizeSeedDate = "";
                string ChemicalSeedDate = "";
                // IrrigateSeedDate 
                // GetSeedDataCheck
                DataTable dtISD = objSP.GetSeedDateData("IRRIGATE", lblGenusCode.Text, TraySize);
                DataTable dtFez = objSP.GetSeedDateData("FERTILIZE", lblGenusCode.Text, TraySize);
                DataTable dtChemical = objSP.GetSeedDateData("SPRAYING", lblGenusCode.Text, TraySize);











                //if (dtCem != null && dtCem.Rows.Count > 0)
                //{
                //    string IDay = dtCem.Rows[0]["DateShift"].ToString();
                //    int ivalue = 0;
                //    if (int.TryParse(IDay, out ivalue))
                //    {
                //        ChemicalSeedDate = (Convert.ToDateTime(lblSeedDate.Text).AddDays(ivalue)).ToString();
                //    }
                //    else
                //    {
                //        ChemicalSeedDate = lblSeedDate.Text;
                //    }

                //}
                //else
                //{
                //    ChemicalSeedDate = lblSeedDate.Text;
                //}


                //if (dtISD != null && dtISD.Rows.Count > 0)
                //{
                //    string IDay = dtISD.Rows[0]["DateShift"].ToString();
                //    int ivalue = 0;
                //    if (int.TryParse(IDay, out ivalue))
                //    {
                //        IrrigateSeedDate = (Convert.ToDateTime(lblSeedDate.Text).AddDays(ivalue)).ToString();
                //    }
                //    else
                //    {
                //        IrrigateSeedDate = lblSeedDate.Text;
                //    }

                //}
                //else
                //{
                //    IrrigateSeedDate = lblSeedDate.Text;
                //}

                //if (dtFez != null && dtFez.Rows.Count > 0)
                //{
                //    string FDay = dtFez.Rows[0]["DateShift"].ToString();
                //    int Fvalue = 0;
                //    if (int.TryParse(FDay, out Fvalue))
                //    {
                //        FertilizeSeedDate = (Convert.ToDateTime(lblSeedDate.Text).AddDays(Fvalue)).ToString();
                //    }
                //    else
                //    {
                //        FertilizeSeedDate = lblSeedDate.Text;
                //    }


                //}
                //else
                //{
                //    FertilizeSeedDate = lblSeedDate.Text;
                //}



                foreach (GridViewRow item in GridSplitJob.Rows)
                {

                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtTrays = (item.Cells[0].FindControl("txtTrays") as TextBox);
                        DropDownList ddlMain = (item.Cells[0].FindControl("ddlMain") as DropDownList);
                        DropDownList ddlLocation = (item.Cells[0].FindControl("ddlLocation") as DropDownList);
                        //-------------
                        string FertilizationDate = string.Empty;
                        string ChemicalDate = string.Empty;
                        string IrrigateDate = string.Empty;
                        string IrrigateNoCount = string.Empty;
                        string FertilizeNoCount = string.Empty;
                        string ChemicalNoCount = string.Empty;



                        NameValueCollection nvChDate = new NameValueCollection();

                        nvChDate.Add("@GreenHouseID", ddlLocation.SelectedValue);
                        DataTable ChFdt = objCommon.GetDataTable("SP_GetFertilizationCheckResetSprayTask", nvChDate);


                        if (dtFez != null && dtFez.Rows.Count > 0)
                        {

                            //  string A = dtFez.Rows[0]["DateShift"].ToString().Replace("\u0002", "");

                            DataColumn col = dtFez.Columns["DateShift"];

                            int Fcount = 0;
                            foreach (DataRow row in dtFez.Rows)
                            {
                                Fcount++;
                                string AD = row[col].ToString().Replace("\u0002", "");

                                FertilizationDate = (Convert.ToDateTime(lblSeedDate.Text).AddDays(Convert.ToInt32(AD))).ToString();

                                string TodatDate;
                                string ReSetSprayDate = "";

                                TodatDate = System.DateTime.Now.ToShortDateString();


                                if (ChFdt != null && ChFdt.Rows.Count > 0)
                                {
                                    ReSetSprayDate = Convert.ToDateTime(ChFdt.Rows[0]["CreateDate"]).AddDays(Convert.ToInt32(ChFdt.Rows[0]["ResetSprayTaskForDays"])).ToString();
                                }

                                if (DateTime.Parse(FertilizationDate) >= DateTime.Parse(TodatDate))
                                {

                                    if (ReSetSprayDate == "" || DateTime.Parse(FertilizationDate) >= DateTime.Parse(ReSetSprayDate))
                                    {

                                        FertilizationDate = FertilizationDate;
                                        FertilizeNoCount = Fcount.ToString();
                                        break;
                                    }
                                    else
                                    {
                                        FertilizationDate = ReSetSprayDate;
                                    }
                                }

                            }
                        }
                        else
                        {
                            FertilizationDate = lblSeedDate.Text;
                        }
                        ///-----

                        //------ Chemical

                        NameValueCollection nvChemChDate = new NameValueCollection();

                        nvChemChDate.Add("@GreenHouseID", ddlLocation.SelectedValue);
                        DataTable ChChemidt = objCommon.GetDataTable("SP_GetChemicalCheckResetSprayTas", nvChemChDate);


                        if (dtChemical != null && dtChemical.Rows.Count > 0)
                        {
                            DataColumn col = dtChemical.Columns["DateShift"];
                            int Ccount = 0;
                            foreach (DataRow row in dtChemical.Rows)
                            {
                                Ccount++;
                                string FDay = row[col].ToString().Replace("\u0002", "");

                                ChemicalDate = (Convert.ToDateTime(lblSeedDate.Text).AddDays(Convert.ToInt32(FDay))).ToString();


                                string TodatDate;
                                string ReSetChemicalDate = "";


                                TodatDate = System.DateTime.Now.ToShortDateString();



                                if (ChChemidt != null && ChChemidt.Rows.Count > 0)
                                {
                                    ReSetChemicalDate = Convert.ToDateTime(ChChemidt.Rows[0]["CreateDate"]).AddDays(Convert.ToInt32(ChChemidt.Rows[0]["ResetSprayTaskForDays"])).ToString();
                                }


                                if (DateTime.Parse(ChemicalDate) >= DateTime.Parse(TodatDate))
                                {

                                    if (ReSetChemicalDate == "" || DateTime.Parse(ChemicalDate) >= DateTime.Parse(ReSetChemicalDate))
                                    {
                                        ChemicalDate = ChemicalDate;
                                        ChemicalNoCount = Ccount.ToString();
                                        break;
                                    }
                                    else
                                    {
                                        ChemicalDate = ReSetChemicalDate;
                                    }
                                }

                            }

                        }
                        else
                        {
                            ChemicalDate = lblSeedDate.Text;
                        }

                        //---

                        //---
                        NameValueCollection nvIRRChDate = new NameValueCollection();

                        nvIRRChDate.Add("@GreenHouseID", ddlLocation.SelectedValue);
                        DataTable Irrigationdt = objCommon.GetDataTable("SP_GetIrrigationResetSprayTask", nvIRRChDate);

                        if (dtISD != null && dtISD.Rows.Count > 0)
                        {

                            DataColumn col = dtISD.Columns["DateShift"];
                            int Irrcount = 0;
                            foreach (DataRow row in dtISD.Rows)
                            {
                                Irrcount++;
                              
                                string IDay = row[col].ToString().Replace("\u0002", "");

                                IrrigateDate = (Convert.ToDateTime(lblSeedDate.Text).AddDays(Convert.ToInt32(IDay))).ToString();


                                string TodatDate1;
                                string ReSetIrrigateDate = "";

                                TodatDate1 = System.DateTime.Now.ToShortDateString();

                                if (Irrigationdt != null && Irrigationdt.Rows.Count > 0)
                                {
                                    ReSetIrrigateDate = Convert.ToDateTime(Irrigationdt.Rows[0]["CreatedOn"]).AddDays(Convert.ToInt32(Irrigationdt.Rows[0]["ResetSprayTaskForDays"])).ToString();
                                }

                                if (DateTime.Parse(IrrigateDate) >= DateTime.Parse(TodatDate1))
                                {

                                    if (ReSetIrrigateDate == "" || DateTime.Parse(IrrigateDate) >= DateTime.Parse(ReSetIrrigateDate))
                                    {
                                        IrrigateDate = IrrigateDate;

                                        IrrigateNoCount = Irrcount.ToString();
                                        break;
                                    }
                                    else
                                    {
                                        IrrigateDate = ReSetIrrigateDate;
                                    }
                                }


                            }
                        }

                        else
                        {
                            IrrigateDate = lblSeedDate.Text;
                        }


                        long result = 0;
                        NameValueCollection nv = new NameValueCollection();
                        nv.Add("@GrowerPutAwayId", "");
                        nv.Add("@wo", wo);

                        nv.Add("@jobcode", lblJobID.Text);
                        nv.Add("@FacilityID", ddlMain.SelectedValue);
                        nv.Add("@GreenHouseID", ddlLocation.SelectedValue);
                        nv.Add("@Trays", txtTrays.Text);

                        nv.Add("@SeedDate", lblSeedDate.Text);
                        nv.Add("@CreateBy", Session["LoginID"].ToString());
                        nv.Add("@Supervisor", ddlSupervisor.SelectedValue);
                        nv.Add("@IrrigateSeedDate", IrrigateSeedDate);
                        nv.Add("@FertilizeSeedDate", FertilizationDate);
                        nv.Add("@ChemicalSeedDate", ChemicalDate);
                        nv.Add("@IrrigateNoCount", IrrigateNoCount);
                        nv.Add("@FertilizeNoCount", FertilizeNoCount);
                        nv.Add("@ChemicalNoCount", ChemicalNoCount);


                        if (txtTrays.Text != "")
                        {
                            nv.Add("@mode", "1");
                            _isInserted = objCommon.GetDataExecuteScalerRetObj("SP_AddGrowerPutAwayDetails", nv);


                            //NameValueCollection nv11 = new NameValueCollection();
                            //nv11.Add("@WoId", wo);
                            //nv11.Add("@JobID", "");
                            //nv11.Add("@GrowerPutAwayId", _isInserted.ToString());
                            //nv11.Add("@CreatedBy", Session["LoginID"].ToString());
                            //int result1 = objCommon.GetDataInsertORUpdate("SP_AddCompletMoveFormDetails", nv11);


                        }
                        SelectedItems++;

                    }



                    NameValueCollection nv1 = new NameValueCollection();
                    nv1.Add("@WorkOrder", wo);
                    _isInserted = objCommon.GetDataInsertORUpdate("SP_UpdateGrowerPutAwayDetails", nv1);


                    string message = "Grower Put Away Save  Successful";
                    string url = "GrowerPutAwayForm.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                    //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Grower Put Away Save  Successful')", true);

                    Clear();
                }





            }
            catch (Exception ex)
            {
            }
        }

        private List<GrowerputDetils> GrowerPutData
        {
            get
            {
                if (ViewState["GrowerPutData"] != null)
                {
                    return (List<GrowerputDetils>)ViewState["GrowerPutData"];
                }
                return new List<GrowerputDetils>();
            }
            set
            {
                ViewState["GrowerPutData"] = value;
            }
        }

        private void AddGrowerPutRow(bool AddBlankRow)
        {
            try
            {
                string unit = "", ddlTAX1 = "", ddlStatusVal = "", hdnWOEmployeeIDVal = "";
                string MainId = "", LocationId = "";


                List<GrowerputDetils> objinvoice = new List<GrowerputDetils>();

                foreach (GridViewRow item in GridSplitJob.Rows)
                {
                    hdnWOEmployeeIDVal = ((HiddenField)item.FindControl("hdnWOEmployeeID")).Value;

                    MainId = ((DropDownList)item.FindControl("ddlMain")).SelectedValue;
                    LocationId = ((DropDownList)item.FindControl("ddlLocation")).SelectedValue;
                    TextBox txtTrays = (TextBox)item.FindControl("txtTrays");

                    AddGrowerput(ref objinvoice, Convert.ToInt32(hdnWOEmployeeIDVal), MainId, LocationId, txtTrays.Text);

                }
                if (AddBlankRow)
                    AddGrowerput(ref objinvoice, 1, "", "", "");

                GrowerPutData = objinvoice;
                GridSplitJob.DataSource = objinvoice;
                GridSplitJob.DataBind();
                ViewState["Data"] = objinvoice;


            }
            catch (Exception ex)
            {
                //  divMessage.Visible = true;
                //  divMessageSub.Attributes.Remove("class");
                // divMessageSub.Attributes.Add("class", "errormsg");
                //  lblMsg.Text = "Unable to process request. Please verify the details.<br />" + ex;
            }

        }

        public void GridSplitjob()
        {
            GridSplitJob.DataSource = ViewState["Data"];
            GridSplitJob.DataBind();

        }

        private void AddGrowerput(ref List<GrowerputDetils> objGP, int ID, string FacilityID, string GreenHouseID, string Trays)

        {
            GrowerputDetils objInv = new GrowerputDetils();
            objInv.ID = ID;
            objInv.RowNumber = objGP.Count + 1;
            objInv.FacilityID = FacilityID;

            objInv.GreenHouseID = GreenHouseID;
            objInv.Trays = Trays;

            objGP.Add(objInv);
            ViewState["ojbpro"] = objGP;
        }

        protected void GridSplitJob_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<GrowerputDetils> objinvoice = ViewState["ojbpro"] as List<GrowerputDetils>;
            objinvoice.RemoveAt(e.RowIndex);
            GridSplitJob.DataSource = objinvoice;
            GridSplitJob.DataBind();
        }
    }
}



[Serializable]
public class GrowerputDetils
{
    public int ID { get; set; }

    public int RowNumber { get; set; }
    public string FacilityID { get; set; }
    public string GreenHouseID { get; set; }

    public string Trays { get; set; }
}
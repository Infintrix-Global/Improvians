using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Evo.BAL_Classes
{
    public class BAL_Task
    {
        General objGeneral = new General();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
    

        private string strQuery = string.Empty;
        public int AddMoveRequest(DataTable dt, string jobID, string reqDate,string LoginID,string LogisticID,string wo,string GrowerPutAwayID)
        {
            int _isInserted = -1;
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objGeneral.ClearParameters();
                    objGeneral.AddParameterWithValueToSQLCommand("@JobID", jobID);
                    objGeneral.AddParameterWithValueToSQLCommand("@RequestDate", reqDate);
                    objGeneral.AddParameterWithValueToSQLCommand("@FromFacility", dt.Rows[i]["FromFacility"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@ToFacility", dt.Rows[i]["ToFacilityID"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@Greenhouse", dt.Rows[i]["GreenhouseID"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@Trays", dt.Rows[i]["Trays"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@LoginID", LoginID);
                    objGeneral.AddParameterWithValueToSQLCommand("@LogisticID", LogisticID);
                    objGeneral.AddParameterWithValueToSQLCommand("@wo", wo);
                    objGeneral.AddParameterWithValueToSQLCommand("@GrowerPutAwayID", GrowerPutAwayID);
                    _isInserted = objGeneral.GetExecuteNonQueryByCommand_SP("SP_AddMoveRequest");
                }
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int AddFertilizerRequestDetails(DataTable dt, string FertilizationID,int FertilizationCode,string bencLoc, string BenchIrrigationFlowRat, string BenchIrrigationCoverage, string SprayCoverageperminutes,string ResetSprayTaskForDays)
        {
            int _isInserted = -1;
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objGeneral.ClearParameters();
                    objGeneral.AddParameterWithValueToSQLCommand("@FertilizationID", FertilizationID);
                    objGeneral.AddParameterWithValueToSQLCommand("@Fertilizer", dt.Rows[i]["Fertilizer"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@Quantity", dt.Rows[i]["Quantity"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@Unit", dt.Rows[i]["Unit"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@Tray", dt.Rows[i]["Tray"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@SQFT", dt.Rows[i]["SQFT"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@FertilizationCode", FertilizationCode.ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@GreenHouseID", bencLoc);
                    objGeneral.AddParameterWithValueToSQLCommand("@BenchIrrigationFlowRat", BenchIrrigationFlowRat);
                    objGeneral.AddParameterWithValueToSQLCommand("@BenchIrrigationCoverage", BenchIrrigationCoverage);
                    objGeneral.AddParameterWithValueToSQLCommand("@SprayCoverageperminutes", SprayCoverageperminutes);
                    objGeneral.AddParameterWithValueToSQLCommand("@ResetSprayTaskForDays", ResetSprayTaskForDays);
               

                    _isInserted = objGeneral.GetExecuteNonQueryByCommand_SP("SP_AddFertilizerDetails");
                }
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }


        public int AddFertilizerRequestDetailsCreatTask(DataTable dt, string FertilizationID, int FertilizationCode, string bencLoc, string BenchIrrigationFlowRat, string BenchIrrigationCoverage, string SprayCoverageperminutes, string ResetSprayTaskForDays,string Comments)
        {
            int _isInserted = -1;
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objGeneral.ClearParameters();
                    objGeneral.AddParameterWithValueToSQLCommand("@FertilizationID", FertilizationID);
                    objGeneral.AddParameterWithValueToSQLCommand("@Fertilizer", dt.Rows[i]["Fertilizer"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@Quantity", dt.Rows[i]["Quantity"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@Unit", dt.Rows[i]["Unit"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@Tray", dt.Rows[i]["Tray"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@SQFT", dt.Rows[i]["SQFT"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@FertilizationCode", FertilizationCode.ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@GreenHouseID", bencLoc);
                    objGeneral.AddParameterWithValueToSQLCommand("@BenchIrrigationFlowRat", BenchIrrigationFlowRat);
                    objGeneral.AddParameterWithValueToSQLCommand("@BenchIrrigationCoverage", BenchIrrigationCoverage);
                    objGeneral.AddParameterWithValueToSQLCommand("@SprayCoverageperminutes", SprayCoverageperminutes);
                    objGeneral.AddParameterWithValueToSQLCommand("@ResetSprayTaskForDays", ResetSprayTaskForDays);
                    objGeneral.AddParameterWithValueToSQLCommand("@Comments", Comments);


                    _isInserted = objGeneral.GetExecuteNonQueryByCommand_SP("SP_AddFertilizerDetailsCreateTask");
                }
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }



        public int AddChemicalRequestDetails(DataTable dt, string ChemicalIReqid, string FertilizationID, int FertilizationCode, string bencLoc, string ResetSprayTaskForDays,string Method, string Comments)
        {
            int _isInserted = -1;
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objGeneral.ClearParameters();
                    objGeneral.AddParameterWithValueToSQLCommand("@ChemicalId", FertilizationID);
                    objGeneral.AddParameterWithValueToSQLCommand("@Fertilizer", dt.Rows[i]["Fertilizer"].ToString());
                
                    objGeneral.AddParameterWithValueToSQLCommand("@Tray", dt.Rows[i]["Tray"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@SQFT", dt.Rows[i]["SQFT"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@ChemicalCode", FertilizationCode.ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@GreenHouseID", bencLoc);
            
                    objGeneral.AddParameterWithValueToSQLCommand("@ResetSprayTaskForDays", ResetSprayTaskForDays);

                    objGeneral.AddParameterWithValueToSQLCommand("@Method", Method);

                    objGeneral.AddParameterWithValueToSQLCommand("@Comments", Comments);
                    objGeneral.AddParameterWithValueToSQLCommand("@ChemicalIReqid", ChemicalIReqid);
                    _isInserted = objGeneral.GetExecuteNonQueryByCommand_SP("SP_AddChemicalRequestDetails");
                }
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }


        public int AddPTCSeedAllocation(string STCID, string LotID, string ActualSeed, string SeedNo, string Type, string Partial,string InitialSeedLotWeightLb,string InitialSeedLotWeightOz, string FinalSeedLotWeightLb,string FinalSeedLotWeightOz, string LotComments)
        {
            int _isInserted = -1;
            try
            {
                objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@STCID", STCID);
                 objGeneral.AddParameterWithValueToSQLCommand("@LotID", LotID);
                objGeneral.AddParameterWithValueToSQLCommand("@ActualSeed", ActualSeed);
                objGeneral.AddParameterWithValueToSQLCommand("@SeedNo", SeedNo);
                objGeneral.AddParameterWithValueToSQLCommand("@Type", Type);
                objGeneral.AddParameterWithValueToSQLCommand("@Partial", Partial);
                objGeneral.AddParameterWithValueToSQLCommand("@FinalSeedLotWeightLb", InitialSeedLotWeightLb);
                objGeneral.AddParameterWithValueToSQLCommand("@FinalSeedLotWeightOz", InitialSeedLotWeightOz);
                objGeneral.AddParameterWithValueToSQLCommand("@InitialSeedLotWeightLb", FinalSeedLotWeightLb);
                objGeneral.AddParameterWithValueToSQLCommand("@InitialSeedLotWeightOz", FinalSeedLotWeightOz);
                objGeneral.AddParameterWithValueToSQLCommand("@LotComments", LotComments);
                

                _isInserted = objGeneral.GetExecuteNonQueryByCommand_SP("SP_AddPTCLotMap");
                
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int UpdatePTCSeedAllocation(string PTCMapID, string ActualSeed, string SeedNo, string Type, string Partial)
        {
            int _isInserted = -1;
            try
            {
                objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@PTCMapID", PTCMapID);
                objGeneral.AddParameterWithValueToSQLCommand("@ActualSeed", ActualSeed);
                objGeneral.AddParameterWithValueToSQLCommand("@SeedNo", SeedNo);
                objGeneral.AddParameterWithValueToSQLCommand("@Type", Type);
                objGeneral.AddParameterWithValueToSQLCommand("@Partial", Partial);
                _isInserted = objGeneral.GetExecuteNonQueryByCommand_SP("SP_UpdatePTCLotMap");

            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }


        public int UpdatePTCSeedAllocationBarCode(string PTCMapID, string BarCode)
        {
            int _isInserted = -1;
            try
            {
                objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@PTCMapID", PTCMapID);
                objGeneral.AddParameterWithValueToSQLCommand("@BarCode", BarCode);
              
                _isInserted = objGeneral.GetExecuteNonQueryByCommand_SP("SP_UpdatePTCLotMapBarCode");

            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public DataTable GetSeedNoBySeedLotID(string SeedLotID)
        {
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@SeedLotID", SeedLotID);
                ds = objGeneral.GetDatasetByCommand_SP("SP_GetSeedNoBySeedLotID");
            }
            catch (Exception ex)
            {

            }
          return  ds.Tables[0];
        }


        public DataTable GetCreateTaskRequestSelect(string FacilityLocation, string BenchLocation, string JobCode)
        {
            General objGeneral = new General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "Select GTS.jobcode,GTS.wo,GPD.GrowerPutAwayId,cname,GTS.itemdescp,GTS.itemno,GPD.FacilityID,GPD.GreenHouseID, GPD.Trays,GTS.TraySize,STC.SeededDate as SeededDate,GTS.GenusCode" +
                            "  from gti_jobs_seeds_plan GTS inner join SeedLineTaskCompletion STC on STC.wo=GTS.wo inner join GrowerPutAwayDetails GPD on GPD.wo=GTS.wo " +

                            "where ";
                if (!string.IsNullOrEmpty(FacilityLocation))
                {
                    strQuery += "  FacilityID ='" + FacilityLocation + "'";
                }
                if (!string.IsNullOrEmpty(BenchLocation))
                {
                    strQuery += " and  GPD.GreenHouseID in (" + BenchLocation + ")";
                }
                if (!string.IsNullOrEmpty(JobCode))
                {
                    strQuery += " and GTS.jobcode ='" + JobCode + "'";
                }
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable GetCreateTaskRequestSelectNew(string FacilityLocation, string BenchLocation, string JobCode, string CustName)
        {
            General objGeneral = new General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "Select GTS.jobcode,GTS.wo,'0' as jid,GPD.GrowerPutAwayId,cname,GTS.itemdescp,GTS.itemno,GPD.FacilityID,GPD.GreenHouseID, GPD.Trays,GTS.TraySize,STC.SeededDate as SeededDate,GTS.GenusCode,GTS.plan_date,GTS.due_date" +
                            "  from gti_jobs_seeds_plan GTS inner join SeedLineTaskCompletion STC on STC.wo=GTS.wo inner join GrowerPutAwayDetails GPD on GPD.wo=GTS.wo " +

                            "where GPD.IsActive=1 ";
                if (!string.IsNullOrEmpty(FacilityLocation))
                {
                    strQuery += " and  FacilityID ='" + FacilityLocation + "'";
                }
                if (!string.IsNullOrEmpty(BenchLocation))
                {
                    strQuery += " and  GPD.GreenHouseID in (" + BenchLocation + ")";
                }
                if (!string.IsNullOrEmpty(JobCode))
                {
                    strQuery += " and GTS.jobcode ='" + JobCode + "'";
                }
                if (!string.IsNullOrEmpty(CustName))
                {
                    strQuery += " and cname ='" + CustName + "'";
                }
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }




        public DataTable GetCreateTaskRequestStart(string FacilityLocation, string BenchLocation, string JobCode)
        {
            General objGeneral = new General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "Select GTS.jobcode,GTS.wo,GPD.GrowerPutAwayId,cname,GTS.itemdescp,GTS.itemno,GPD.FacilityID,GPD.GreenHouseID, GPD.Trays,GTS.TraySize,STC.SeededDate as SeededDate,GTS.GenusCode,'0' as jid " +
                            "  from gti_jobs_seeds_plan GTS inner join SeedLineTaskCompletion STC on STC.wo=GTS.wo inner join GrowerPutAwayDetails GPD on GPD.wo=GTS.wo " +

                            "where GPD.IsActive=1 ";
                if (!string.IsNullOrEmpty(FacilityLocation))
                {
                    strQuery += "  FacilityID ='" + FacilityLocation + "'";
                }
                if (!string.IsNullOrEmpty(BenchLocation))
                {
                    strQuery += " and  GPD.GreenHouseID in (" + BenchLocation + ")";
                }
                if (!string.IsNullOrEmpty(JobCode))
                {
                    strQuery += " and GTS.jobcode in (" + JobCode + ")";
                }
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }



        public DataTable GetManualRequestSelect(string FacilityLocation, string BenchLocation, string JobCode)
        {
            General objGeneral = new General();

            DataTable dt = new DataTable();
            try
            {
                strQuery = "Select GJSPM.jobcode,'0' as wo,GJSPM.jid as GrowerPutAwayId,GJSPM.cname,GJSPM.itemdescp,GJSPM.itemno,  " +
                            " GJSPM.loc_seedline as FacilityID,GJSPM.GreenHouseID, GJSPM.Trays,GJSPM.TraySize,GJSPM.SeedDate as SeededDate,GJSPM.GenusCode from [gti_jobs_seeds_plan_Manual] GJSPM where 1=1 ";

                if (!string.IsNullOrEmpty(FacilityLocation))
                {
                    strQuery += " and GJSPM.loc_seedline ='" + FacilityLocation + "'";
                }
                if (!string.IsNullOrEmpty(BenchLocation))
                {
                    strQuery += " and GJSPM.GreenHouseID in (" + BenchLocation + ")";
                }
                if (!string.IsNullOrEmpty(JobCode))
                {
                    strQuery += " and jobcode ='" + JobCode + "'";
                }


                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }



        public DataTable GetManualRequestSelectNew(string FacilityLocation, string BenchLocation, string JobCode,string CustName)
        {
            General objGeneral = new General();

            DataTable dt = new DataTable();
            try
            {
                strQuery = "Select GJSPM.jobcode,'0' as wo,GJSPM.jid,'0' as GrowerPutAwayId,GJSPM.cname,GJSPM.itemdescp,GJSPM.itemno,  " +
                            " GJSPM.loc_seedline as FacilityID,GJSPM.GreenHouseID, GJSPM.Trays,GJSPM.TraySize,GJSPM.SeedDate as SeededDate,GJSPM.GenusCode,GJSPM.PlantReadyDate as plan_date,GJSPM.PlantDueDate as due_date from [gti_jobs_seeds_plan_Manual] GJSPM where 1=1 ";


                //strQuery = "Select GTS.jobcode,GTS.wo,'0' as jid,GPD.GrowerPutAwayId,cname,GTS.itemdescp,GTS.itemno,GPD.FacilityID,GPD.GreenHouseID, GPD.Trays,GTS.TraySize,STC.SeededDate as SeededDate,GTS.GenusCode,GTS.plan_date,GTS.due_date" +
                //         "  from gti_jobs_seeds_plan GTS inner join SeedLineTaskCompletion STC on STC.wo=GTS.wo inner join GrowerPutAwayDetails GPD on GPD.wo=GTS.wo " +


                if (!string.IsNullOrEmpty(FacilityLocation)  )
                {
                    strQuery += " and GJSPM.loc_seedline ='" + FacilityLocation + "'";
                }
                if (!string.IsNullOrEmpty(BenchLocation) && BenchLocation != "''")
                {
                    strQuery += " and GJSPM.GreenHouseID in (" + BenchLocation + ")";
                }
                if (!string.IsNullOrEmpty(JobCode) && BenchLocation != "''")
                {
                    strQuery += " and jobcode ='" + JobCode + "'";
                }
                if (!string.IsNullOrEmpty(CustName))
                {
                    strQuery += " and GJSPM.cname ='" + CustName + "'";
                }


                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }






        public DataTable GetManualRequestStart(string FacilityLocation, string BenchLocation, string JobCode)
        {
            General objGeneral = new General();

            DataTable dt = new DataTable();
            try
            {
                strQuery = "Select GJSPM.jobcode,'0' as wo,GJSPM.jid,'0' as GrowerPutAwayId,GJSPM.cname,GJSPM.itemdescp,GJSPM.itemno,  " +
                            " GJSPM.loc_seedline as FacilityID,GJSPM.GreenHouseID, GJSPM.Trays,GJSPM.TraySize,GJSPM.SeedDate as SeededDate,GJSPM.GenusCode from [gti_jobs_seeds_plan_Manual] GJSPM where 1=1 ";

                if (!string.IsNullOrEmpty(FacilityLocation))
                {
                    strQuery += " and GJSPM.loc_seedline ='" + FacilityLocation + "'";
                }
                if (!string.IsNullOrEmpty(BenchLocation))
                {
                    strQuery += " and GJSPM.GreenHouseID in (" + BenchLocation + ")";
                }
                if (!string.IsNullOrEmpty(JobCode))
                {
                    strQuery += " and jobcode in (" + JobCode + ")";
                }


                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetManualRequestStart1(string FacilityLocation, string BenchLocation, string JobCode)
        {
            General objGeneral = new General();

            DataTable dt = new DataTable();
            try
            {
                strQuery = "Select GJSPM.jobcode,'0' as wo,GJSPM.jid,'0' as GrowerPutAwayId,GJSPM.cname,GJSPM.itemdescp,GJSPM.itemno,  " +
                            " GJSPM.loc_seedline as FacilityID,GJSPM.GreenHouseID, GJSPM.Trays,GJSPM.TraySize,GJSPM.SeedDate as SeededDate,GJSPM.GenusCode from [gti_jobs_seeds_plan_Manual] GJSPM where 1=1 ";

                if (!string.IsNullOrEmpty(FacilityLocation))
                {
                    strQuery += " and GJSPM.loc_seedline ='" + FacilityLocation + "'";
                }
                if (!string.IsNullOrEmpty(BenchLocation))
                {
                    strQuery += " and GJSPM.GreenHouseID in (" + BenchLocation + ")";
                }
                if (!string.IsNullOrEmpty(JobCode))
                {
                    strQuery += " and jobcode not in (" + JobCode + ")";
                }


                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }



        public DataTable GetManualRequestStartfff(string BenchLocation, string JobCode, string FacilityID, string RequestType,string Fid)
        {
            General objGeneral = new General();

            DataTable dt = new DataTable();
            try
            {
                strQuery = "Select GTS.jobcode,'' as wo,GTS.itemdescp,GTS.itemno,GTS.TraySize,GPD.Trays as trays_actual,GPD.SeedDate as  SeededDate,GTS.loc_seedline,GPD.FertilizeSeedDate,GPD.RequestType, GTS.GenusCode ,IsNull(L.EmployeeName,'System') as AssignedBy " +
                            ",GPD.FacilityID,GPD.Trays,'0' as GrowerPutAwayId,GPD.GreenHouseID, cname,'Fertilization Count-' + GPD.DateCountNo as DateCountNo,isnull(FD.FertilizationCode,0) as FertilizationCode,GTS.jid,FD.TaskRequestKey,GTS.PlantDueDate " +
                            "from gti_jobs_seeds_plan_Manual GTS   inner join GrowerPutAwayDetailsFertilizationMenual GPD on GPD.Jid=GTS.Jid  	 left join FertilizationRequest FD on GPD.Jid=FD.ManualID " +
                            "left join Login L on L.Id=FD.CreatedBy WHERE 	GTS.ISActiveSpray=1     and GPD.IsFertilize is null and GPD.IsAssistant in (0,1)  ";

                if (FacilityID !="0")
                {
                    strQuery += " and GPD.FacilityID ='" + FacilityID + "'";
                }
                if (!string.IsNullOrEmpty(BenchLocation))
                {
                    strQuery += " and GPD.GreenHouseID in (" + BenchLocation + ")";
                }
                if (JobCode != "0")
                {
                    strQuery += " and GTS.jobcode='"+ JobCode + "'";
                }
                if (RequestType != "0")
                {
                    strQuery += " and GPD.RequestType '" + RequestType + "'";
                }

                if (Fid == "D")
                {
                    strQuery += " order by GPD.FertilizeSeedDate DESC ";
                }
                else
                {
                    strQuery += "  order by GPD.FertilizeSeedDate ";
                   
                }



                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public int UpdateFDate(string BenchLocation,string FDate)
        {
            int IsId = 0;
            General objGeneral = new General();

            DataTable dt = new DataTable();
            try
            {
                strQuery = "Update GrowerPutAwayDetailsFertilizationMenual set FertilizeSeedDate ='"+ FDate + "' where GreenHouseID in (" + BenchLocation + ")";


               objGeneral.GetExecuteNonQueryByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsId;
        }


        public int UpdateIsActiveDatat(string BenchLocation)
        {
            int IsId = 0;
            General objGeneral = new General();

            DataTable dt = new DataTable();
            try
            {
                strQuery = "Update GrowerPutAwayDetailsFertilizationMenual set IsFertilize =1 where GreenHouseID in (" + BenchLocation + ")";


                objGeneral.GetExecuteNonQueryByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsId;
        }


        public int UpdateIsActiveChemical(string BenchLocation)
        {
            int IsId = 0;
            General objGeneral = new General();

            DataTable dt = new DataTable();
            try
            {
                strQuery = " update GrowerPutAwayDetailsChemicalMenual set IsChemical=1  where GreenHouseID in (" + BenchLocation + ")";


                objGeneral.GetExecuteNonQueryByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsId;
        }

        public int UpdateIsActiveIrrigation(string BenchLocation)
        {
            int IsId = 0;
            General objGeneral = new General();

            DataTable dt = new DataTable();
            try
            {
                strQuery = " update GrowerPutAwayDetailsIrrigationMenual set ISIrrigation=1  where GreenHouseID in (" + BenchLocation + ")";


                objGeneral.GetExecuteNonQueryByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsId;
        }
    }
}
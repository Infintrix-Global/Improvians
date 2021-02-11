using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Improvians.BAL_Classes
{
    public class BAL_Task
    {
        General objGeneral = new General();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

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

        public int AddFertilizerRequestDetails(DataTable dt, string FertilizationID)
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
                    _isInserted = objGeneral.GetExecuteNonQueryByCommand_SP("SP_AddFertilizerDetails");
                }
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int AddPTCSeedAllocation(string STCID, string LotID, string ActualSeed, string SeedNo, string Type, string Partial)
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
    }
}
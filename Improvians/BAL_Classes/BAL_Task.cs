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

        public int AddMoveRequest(DataTable dt, string jobID, string reqDate,string LoginID)
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
                    objGeneral.AddParameterWithValueToSQLCommand("@ToFacility", dt.Rows[i]["ToFacility"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@Greenhouse", dt.Rows[i]["Greenhouse"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@Trays", dt.Rows[i]["Trays"].ToString());
                    objGeneral.AddParameterWithValueToSQLCommand("@LoginID", LoginID);
                    _isInserted = objGeneral.GetExecuteNonQueryByCommand_SP("SP_AddMoveRequest");
                }
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

    }
}
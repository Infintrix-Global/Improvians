using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Improvians.Admin.BAL_Classes
{
    public class BAL_Task
    {
        General objGeneral = new General();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        public DataTable GetEmployeeByID(int eid)
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@EmployeeID", eid);

                ds = objGeneral.GetDatasetByCommand_SP("SP_GetEmployeeByID");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }
    }
}
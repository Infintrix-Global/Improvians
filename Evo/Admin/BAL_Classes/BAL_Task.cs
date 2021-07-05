using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Evo.Admin.BAL_Classes
{
    public class BAL_Task
    {
        General objGeneral = new General();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        public DataSet GetEmployeeByID(int eid)
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
            return ds;
        }

        public DataSet GetFertilizerByID(int eid)
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@id", eid);

                ds = objGeneral.GetDatasetByCommand_SP("GetFertilizerDetailById");
            }
            catch (Exception ex)
            {
            }
            return ds;
        }
        public DataSet GetChemicalByID(int eid)
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@id", eid);

                ds = objGeneral.GetDatasetByCommand_SP("GetChemicalDetailById");
            }
            catch (Exception ex)
            {
            }
            return ds;
        }
        public DataSet GetTaskTypeByID(int eid)
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@id", eid);

                ds = objGeneral.GetDatasetByCommand_SP("GetTaskTypeWorkHoursDetailById");
            }
            catch (Exception ex)
            {
            }
            return ds;
        }



        public DataSet GetTaskTypeDetsils(int eid)
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@id", eid);

                ds = objGeneral.GetDatasetByCommand_SP("GetTaskTypeWorkHoursDetailById1");
            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        public DataSet GetCropHealthByID(int eid)
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@id", eid);

                ds = objGeneral.GetDatasetByCommand_SP("GetCropHealthDetailById");
            }
            catch (Exception ex)
            {
            }
            return ds;
        }
    }
}
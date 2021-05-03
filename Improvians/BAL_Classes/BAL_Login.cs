using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace Evo.BAL_Classes
{
    public class BAL_Login
    {
        General objGeneral = new General();
        DataSet ds = new DataSet();
        private string strQuery = string.Empty;

        public DataTable getLoginDetails(LoginEntity _loginEntity)
        {

            DataSet ds = new DataSet();
            try
            {

                objGeneral.AddParameterWithValueToSQLCommand("@UserName", _loginEntity.UserName);
                objGeneral.AddParameterWithValueToSQLCommand("@Password", _loginEntity.Password);
                ds = objGeneral.GetDatasetByCommand_SP("SP_login");

            }
            catch (Exception ex)
            {


            }

            return ds.Tables[0];

        }
        public DataTable getCustomerLoginDetails(LoginEntity _loginEntity)
        {

            DataSet ds = new DataSet();
            try
            {

                objGeneral.AddParameterWithValueToSQLCommand("@UserName", _loginEntity.UserName);
                objGeneral.AddParameterWithValueToSQLCommand("@Password", _loginEntity.Password);
                ds = objGeneral.GetDatasetByCommand_SP("SP_Customerlogin");

            }
            catch (Exception ex)
            {


            }

            return ds.Tables[0];

        }
        public int UpdateFCMToken(int ID, string FCMToken)
        {
            int _isInserted = -1;
            try
            {
                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@ID", ID);
                objGeneral.AddParameterWithValueToSQLCommand("@FCMToken", FCMToken);
                _isInserted = objGeneral.GetExecuteNonQueryByCommand_SP("SP_UpdateFCMToken");

            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public string GetFCMToken(int ID)
        {
            string FCMToken = string.Empty;
            try
            {
                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@ID", ID);
                DataSet ds = objGeneral.GetDatasetByCommand_SP("SP_GetFCMToken");
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                        FCMToken = dt.Rows[0]["FCMToken"].ToString();
                }

            }
            catch (Exception ex)
            {

            }
            return FCMToken;
        }
    }
}
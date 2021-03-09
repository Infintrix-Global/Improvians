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
    }
}
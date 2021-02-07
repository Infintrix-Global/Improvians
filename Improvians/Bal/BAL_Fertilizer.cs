using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Evo.Bal;


namespace Improvians.Bal
{
    public class BAL_Fertilizer
    {
        Improvians_General objGeneral = new Improvians_General();
        DataSet ds = new DataSet();


        private string strQuery = string.Empty;
        public DataTable GetChemicalList()
        {
            Improvians_General objGeneral = new Improvians_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "	select i.No_, i.No_ + ' ** ' + i.Description as Name from [GTI$Item] i " +
                    " where i.[Gen_ Prod_ Posting Group] in ('CHEMICALS') ORDER BY i.No_";
                dt = objGeneral.GetDatasetByCommand(strQuery);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetFertilizerList()
        {
            Improvians_General objGeneral = new Improvians_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select i.No_, i.No_ + ' ** ' + i.Description as Name from [GTI$Item] i " +
                    " where i.[Gen_ Prod_ Posting Group] in ('FERTILIZER') ORDER BY i.No_";
                dt = objGeneral.GetDatasetByCommand(strQuery);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetUnitList()
        {
            Improvians_General objGeneral = new Improvians_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select Code , Description  from [GTI$Unit of Measure] order by Code";

                dt = objGeneral.GetDatasetByCommand(strQuery);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
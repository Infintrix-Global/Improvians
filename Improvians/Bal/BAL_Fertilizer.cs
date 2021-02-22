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
        public DataTable GetManualFertilizerRequest(string BenchLocation)
        {
            Improvians_General objGeneral = new Improvians_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select distinct t.[Job No_]  as jobcode,'' as wo,'' as GrowerPutAwayId, j.[Bill-to Name] as cname , j.[Item Description] as itemdescp, j.[Item No_] as itemno, t.[Location Code] as FacilityID, t.[Position Code] as GreenHouseID,CAST(t.Quantity AS INT) as Trays,CAST(t.[Qty_ per Unit of Measure] AS INT) as TraySize, t.[Posting Date] as SeededDate " +
                            " from[GTI$IA Job Tracking Entry] t, [GTI$Job] j where j.No_ = t.[Job No_] ";
                if (!string.IsNullOrEmpty(BenchLocation))
                {
                   string BenchLocations= BenchLocation.Remove(BenchLocation.Length - 1, 1);
                    strQuery += " and t.[Position Code] in(" + BenchLocations + ")";
                }
                strQuery += " group by t.[Job No_], j.[Bill-to Name], j.[Item Description], t.[Location Code], t.[Position Code],t.[Quantity],t.[Qty_ per Unit of Measure],j.[Item No_],t.[Posting Date] HAVING sum(t.Quantity) > 0";
              
                
                
                
                
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
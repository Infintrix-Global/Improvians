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
        public DataTable GetManualFertilizerRequest(string FacilityLocation, string BenchLocation, string JobCode)
        {
            Improvians_General objGeneral = new Improvians_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select distinct t.[Job No_]  as jobcode,'' as wo,0 as GrowerPutAwayId, j.[Bill-to Name] as cname , j.[Item Description] as itemdescp, j.[Item No_] as itemno " +
                            " ,t.[Location Code] as FacilityID,t.[Position Code] as GreenHouseID,CAST(sum(t.Quantity) AS int)  as Trays,j.[Variant Code] as TraySize,'' as SeededDate from[GTI$IA Job Tracking Entry] t, [GTI$Job] j where j.No_ = t.[Job No_] and j.[Job Status] = 2 ";
                if (!string.IsNullOrEmpty(FacilityLocation))
                {
                    strQuery += " and t.[Location Code] ='" + FacilityLocation + "'";
                }
                if (!string.IsNullOrEmpty(BenchLocation))
                {
                    strQuery += " and t.[Position Code] ='" + BenchLocation + "'";
                }
                if (!string.IsNullOrEmpty(JobCode))
                {
                    strQuery += " and t.[Job No_] ='" + JobCode + "'";
                }
                strQuery += " group by t.[Job No_], j.[Bill-to Name], j.[Item Description], t.[Location Code],j.[Item No_],t.[Position Code],t.[Location Code],j.[Variant Code] HAVING sum(t.Quantity) > 0";
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetSQFTofBench(string BenchLocation)
        {

            DataTable dt = new DataTable();
            try
            {
                strQuery = "select s.[Position Code] BenchLocation, s.[Net Area] Sqft from [GTI$IA Subsection] s where s.[Position Code] ='" + BenchLocation + "'";
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
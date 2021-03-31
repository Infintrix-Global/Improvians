using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Evo.Bal;


namespace Evo.Bal
{
    public class BAL_Fertilizer
    {
        Evo_General objGeneral = new Evo_General();
        DataSet ds = new DataSet();


        private string strQuery = string.Empty;
        public DataTable GetChemicalList()
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "	select i.No_, i.Description+' ** '+ i.No_ as Name from [GTI$Item] i " +
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
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select i.No_,i.Description+' ** '+ i.No_ as Name from [GTI$Item] i " +
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
            Evo_General objGeneral = new Evo_General();
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
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select distinct t.[Job No_]  as jobcode,'' as wo,0 as GrowerPutAwayId, j.[Bill-to Name] as cname , j.[Item Description] as itemdescp, j.[Item No_] as itemno, " +
                            "  (select top 1 t1.[Posting Date] from [GTI$IA Job Tracking Entry] t1 where t1.[Job No_] = t.[Job No_] and t1.[Posting Type] = 2 and t1.[Production Phase] = 'SEEDING' order by t1.[Posting Date] desc) as SeededDate " +
                            " ,t.[Location Code] as FacilityID,t.[Position Code] as GreenHouseID,CAST(sum(t.Quantity) AS int)  as Trays,j.[Variant Code] as TraySize   from[GTI$IA Job Tracking Entry] t, [GTI$Job] j where j.No_ = t.[Job No_] and j.[Job Status] = 2 ";
               
                
                if (!string.IsNullOrEmpty(FacilityLocation))
                {
                    strQuery += " and t.[Location Code] ='" + FacilityLocation + "'";
                }
                if (!string.IsNullOrEmpty(BenchLocation))
                {
                    strQuery += " and t.[Position Code]  ='" + BenchLocation + "'";
                }
                if (!string.IsNullOrEmpty(JobCode))
                {
                    strQuery += " and t.[Job No_] ='" + JobCode + "'";
                }
                strQuery += " group by t.[Job No_], j.[Bill-to Name], j.[Item Description], t.[Location Code],j.[Item No_],t.[Position Code],t.[Location Code],j.[Variant Code]  HAVING sum(t.Quantity) > 0";
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }



        public DataTable GetManualFertilizerRequestSelect(string FacilityLocation, string BenchLocation, string JobCode)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select distinct t.[Job No_]  as jobcode,'' as wo,0 as GrowerPutAwayId, j.[Bill-to Name] as cname , j.[Item Description] as itemdescp, j.[Item No_] as itemno " +
                            " ,t.[Location Code] as FacilityID,t.[Position Code] as GreenHouseID,CAST(sum(t.Quantity) AS int)  as Trays,j.[Variant Code] as TraySize ,t.[Genus Code] as GenusCode," +
                            " (select top 1 t1.[Posting Date] from [GTI$IA Job Tracking Entry] t1 where t1.[Job No_] = t.[Job No_] and t1.[Posting Type] = 2 and t1.[Production Phase] = 'SEEDING' order by t1.[Posting Date] desc) as SeededDate " +
                            " from[GTI$IA Job Tracking Entry] t, [GTI$Job] j where j.No_ = t.[Job No_] and j.[Job Status] = 2 ";
                if (!string.IsNullOrEmpty(FacilityLocation))
                {
                    strQuery += " and t.[Location Code] ='" + FacilityLocation + "'";
                }
                if (!string.IsNullOrEmpty(BenchLocation))
                {
                    strQuery += " and t.[Position Code] in (" + BenchLocation + ")";
                }
                if (!string.IsNullOrEmpty(JobCode))
                {
                    strQuery += " and t.[Job No_] ='" + JobCode + "'";
                }
                strQuery += " group by t.[Job No_], j.[Bill-to Name], j.[Item Description], t.[Location Code],j.[Item No_],t.[Position Code],t.[Location Code],t.[Genus Code],j.[Variant Code]  HAVING sum(t.Quantity) > 0  order by t.[Position Code] ASC";
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable GetManualFertilizerRequestCropHealthReport(string FacilityLocation, string BenchLocation, string JobCode)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select distinct t.[Job No_]  as jobcode,'' as wo,0 as GrowerPutAwayId, j.[Bill-to Name] as cname , j.[Item Description] as itemdescp, j.[Item No_] as itemno " +
                            " ,t.[Location Code] as FacilityID,t.[Position Code] as GreenHouseID,CAST(sum(t.Quantity) AS int)  as Trays,j.[Variant Code] as TraySize," +
                             " (select  t.[Posting Date] from [GTI$IA Job Tracking Entry] t where t.[Job No_] = '" + JobCode + "' and t.[Posting Type] = 2 and t.[Production Phase] = 'SEEDING') as SeededDate " +
                            "  from[GTI$IA Job Tracking Entry] t, [GTI$Job] j where j.No_ = t.[Job No_] and j.[Job Status] = 2 ,and t.[Activity Code] = 'PUTAWAY INSIDE' ";


                if (!string.IsNullOrEmpty(FacilityLocation))
                {
                    strQuery += " and t.[Location Code] ='" + FacilityLocation + "'";
                }
                if (!string.IsNullOrEmpty(BenchLocation))
                {
                    strQuery += " and t.[Position Code]  ='" + BenchLocation + "'";
                }
                if (!string.IsNullOrEmpty(JobCode))
                {
                    strQuery += " and t.[Job No_] ='" + JobCode + "'";
                }
                strQuery += " group by t.[Job No_], j.[Bill-to Name], j.[Item Description], t.[Location Code],j.[Item No_],t.[Position Code],t.[Location Code],j.[Variant Code]  HAVING sum(t.Quantity) > 0";
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
                //and t.[Position Code] in (" + BenchLocation + ")"
                strQuery = "select s.[Position Code] BenchLocation, s.[Net Area] Sqft from [GTI$IA Subsection] s where s.[Position Code] in (" + BenchLocation + ")";
                dt = objGeneral.GetDatasetByCommand(strQuery);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetSQFTofBenchNew(string BenchLocation)
        {

            DataTable dt = new DataTable();
            try
            {
                //and t.[Position Code] in (" + BenchLocation + ")"
                strQuery = "select  Sum(s.[Net Area]) Sqft from [GTI$IA Subsection] s where s.[Position Code] in (" + BenchLocation + ")";
                dt = objGeneral.GetDatasetByCommand(strQuery);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetSelectBenchLocation(string LocationCode,string SectionCode)
        {

            DataTable dt = new DataTable();
            try
            {
                strQuery = "select s.[Position Code] as PositionCode from [GTI$IA Subsection] s where [Location Code] ='" + LocationCode + "' and s.[Section Code] ='"+ SectionCode + "'  and s.Level=3";
                dt = objGeneral.GetDatasetByCommand(strQuery);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetSelectBench(string LocationCode)
        {

            DataTable dt = new DataTable();
            try
            {
                strQuery = " select  s.[Position Code] as PositionCode from [GTI$IA Subsection] s where s.[Position Code] like '" + LocationCode + "%' and s.Level=3 order by s.[Position Code] DESC";
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
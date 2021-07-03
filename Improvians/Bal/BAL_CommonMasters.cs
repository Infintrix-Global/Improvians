using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Evo.Bal;
using Evo.BAL_Classes;

namespace Evo.Bal
{
    public class BAL_CommonMasters
    {
        Evo_General objGeneral = new Evo_General();
        DataSet ds = new DataSet();


        private string strQuery = string.Empty;
        public DataTable GetSeedLot(string JobCode)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                //strQuery = "select le.[Lot No_] l1, le.[Lot No_] l2, sum(le.Quantity) as QTY from[GTI$Item Ledger Entry] le " +
                //  " where le.[Item No_] = (select i.[Purchase Item No_] from[GTI$Item] i " +
                //  "where i.No_ in (select j.[Item No_] from[GTI$Job] j where j.No_ = '" + JobCode + "'))  " +
                //   " group by[Lot No_] " +
                //    " having sum(le.Quantity) > 0 ";
                
                if (JobCode == "JB0200002" || JobCode == "JB0200003" || JobCode == "JB0200004")
                    strQuery = "select 1234 as l2, 82500 as QTY";
                else
                    strQuery = "select l.[Lot No_] l2, (l.Quantity * -1) QTY from[GTI$IA Lot Entry] l where l.Type = 2 and l.[Source Document No_] = '" + JobCode + "'";
                dt = objGeneral.GetDatasetByCommand(strQuery);



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetSeedLotWithDate(string JobCode)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {                
                strQuery = "select Date as SeededDate,Date as CreatedOn, l.[Lot No_] as LotID, CAST(CAST ((l.Quantity * -1)as int) as nvarchar(max)) as NumberOfSeed  from[GTI$IA Lot Entry] l where l.[Source Document No_] = '" + JobCode + "'";
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetJobHistoryDateFromNavision(string JobCode)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select w.[Activity Code] activitycode, w.Date plan_date from [GTI$IA Job Activity Scheme Line] w where w.[Job No_] = '" + JobCode + "' and w.Type = 2 and w.[Activity Code] in('PUTAWAY INSIDE')";
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable GetSeedLotNofset(string SeedlotNo)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select le.[Lot No_] l1, le.[Lot No_] l2, sum(le.Quantity) as Quantity from[GTI$Item Ledger Entry] le where  le.[Lot No_] ='" + SeedlotNo + "' group by[Lot No_]  ";
                dt = objGeneral.GetDatasetByCommand(strQuery);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetMainLocation()
        {
            General objGeneral = new General();
            DataTable dt = new DataTable();
            try
            {
                //strQuery = "Select distinct s.[Location Code] l1, s.[Location Code] l2 from [GTI$IA Subsection] s order by s.[Location Code]";
                strQuery = " Select  distinct Facility from AutomationBenchControls order by Facility";
                dt = objGeneral.GetDatasetByCommand(strQuery);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable GetMainLocation1()
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "Select distinct s.[Location Code] l1, s.[Location Code] l2 from [GTI$IA Subsection] s order by s.[Location Code]";
                //strQuery = " Select  distinct Facility from AutomationBenchControls order by Facility";
                dt = objGeneral.GetDatasetByCommand(strQuery);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetLocation(string MainLocation)
        {
            General objGeneral = new General();
            DataTable dt = new DataTable();
            try
            {
                // strQuery = "Select s.[Position Code], s.[Position Code] p2 from [GTI$IA Subsection] s where s.[Location Code] = '"+ MainLocation + "'";
                //strQuery = "Select s.[Position Code], s.[Position Code] p2 from [GTI$IA Subsection] s where Level =3 and s.[Location Code] = '" + MainLocation + "'  AND s.[Position Code] Not in ('" + MainLocation + "') ";

                strQuery = " Select  distinct BenchName from AutomationBenchControls where  Facility ='" + MainLocation + "'  order by BenchName ";


              dt = objGeneral.GetDatasetByCommand(strQuery);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetJobsForBenchLocation(string BenchLocation)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select distinct t.[Job No_]  as jobcode  from[GTI$IA Job Tracking Entry] t, [GTI$Job] j where j.No_ = t.[Job No_] and j.[Job Status] = 2  and t.[Position Code] = '" + BenchLocation + "'  group by t.[Job No_]  HAVING sum(t.Quantity) > 0";
                
                
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable GetJobsForBenchLocation1(string BenchLocation ,string Facility)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select distinct t.[Job No_]  as jobcode  from[GTI$IA Job Tracking Entry] t, [GTI$Job] j where j.No_ = t.[Job No_] and j.[Job Status] = 2 and t.[Location Code]='"+ Facility + "'  ";
                if (BenchLocation != "")
                {
                 
                    strQuery += " and t.[Position Code] = '" + BenchLocation + "' ";
                }
                strQuery += " group by t.[Job No_]  HAVING sum(t.Quantity) > 0";
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }



        public DataTable GetJobsSearch(string prefixText)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select distinct t.[Job No_]  as jobcode  from[GTI$IA Job Tracking Entry] t, [GTI$Job] j where j.No_ = t.[Job No_] and j.[Job Status] = 2  and t.[Job No_] like '" + prefixText + "%'  group by t.[Job No_]  HAVING sum(t.Quantity) > 0";
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
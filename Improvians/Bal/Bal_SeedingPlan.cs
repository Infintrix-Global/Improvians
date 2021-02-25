using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Improvians.Bal
{
    public class Bal_SeedingPlan
    {
        Improvians_General objGeneral = new Improvians_General();
        DataSet ds = new DataSet();


        private string strQuery = string.Empty;
        public DataTable GetDataSeedingPlan(string FromDate, string ToDate, string loc, string item, string SeedAllocation, string traysize)
        {
            Improvians_General objGeneral = new Improvians_General();
            DataTable dt = new DataTable();
            try
            {
                //strQuery = "select w.No_ wo, b.[Job No_] jobcode, j.[Bill-to Name] cname, p.[Starting Date] sodate, j.[Item Description] itmdescp, j.[Item No_] itm,j.[Variant Code] ts, b.[Container Qty_] sotrays, j.[Bill-to Customer No_] cusno, j.[Delivery Date] duedate, w.[Location Code] loc,";
                //strQuery += " w.[Calc_ Quantity] wotrays, w.[Planned Date] wodate, case when j.[Job Status] = 1 then 'Yes' else 'No' end alloc ";
                //strQuery += " from [GTI$IA Job Activity Scheme Line] b, [GTI$IA Job Production Scheme Line] p, ";
                //strQuery += " [GTI$Job] j left outer join[GTI$IA Work Order Header] w on j.No_ = w.[Job No_] ";
                //strQuery += " where b.[Job No_] = p.[Job No_] And b.[Job No_] = j.No_ And b.[Item Category] = 'SEED' and p.[Production Phase] = 'SEEDING'   And  w.[Location Code]='ENC1' "; //

                //if (FromDate != "" && ToDate != "")
                //    // strQuery += "  convert(date,p.[Starting Date],105) between convert(date,@FromDate,105) and convert(date,@ToDate,105)";
                //    strQuery += "And  p.[Starting Date] between @FromDate and @ToDate ";

                //strQuery += " and  b.[Actual Date] < '1/1/2000' and j.[Job Status] in (1, 9) ";
                //strQuery += "  order by alloc desc, loc desc, sodate ";


                strQuery = "select w.No_ wo, b.[Job No_] jobcode, j.[Bill-to Name] cname, p.[Starting Date] sodate, j.[Item Description] itmdescp, j.[Item No_] itm, j.[Variant Code] ts, b.[Container Qty_] sotrays, j.[Bill-to Customer No_] cusno, j.[Delivery Date] duedate,b.[Genus Code] GenusCode,w.[Location Code] loc,";
                strQuery += " w.[Calc_ Quantity] wotrays, w.[Planned Date] wodate, case when j.[Job Status] =1 then 'Yes' else 'No'end alloc ";
                strQuery += "from [GTI$IA Job Activity Scheme Line] b, [GTI$IA Job Production Scheme Line] p, ";
                strQuery += "[GTI$Job] j left outer join [GTI$IA Work Order Header] w on j.No_ = w.[Job No_] ";
                strQuery += "where b.[Job No_] = p.[Job No_] And b.[Job No_] = j.No_ And b.[Item Category] = 'SEED' and p.[Production Phase] = 'SEEDING' And ";
                strQuery += "p.[Starting Date] between @FromDate and @ToDate and b.[Actual Date] < '1/1/2000' and j.[Job Status] in (1,9) ";
                //strQuery += "and b.[Job No_] not in (select jobcode from gti_jobs_seeds_plan) ";

                if (loc != "0")
                    strQuery += " And w.[Location Code]= " + "'" + loc + "'";
                if (item != "0")
                    strQuery += " And j.[Item Description] = " + "'" + item + "'";
                if (SeedAllocation != "0")
                    strQuery += " And j.[Job Status]= " + "'" + SeedAllocation + "'";
                if (traysize != "0")
                    strQuery += " And  j.[Variant Code]= " + "'" + traysize + "'";

                strQuery += " order by alloc desc, loc desc, sodate";
                objGeneral.AddParameterWithValueToSQLCommand("@FromDate", FromDate);
                objGeneral.AddParameterWithValueToSQLCommand("@ToDate", ToDate);
                dt = objGeneral.GetDatasetByCommand(strQuery);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetSeedlineLocation(string FromDate, string ToDate)
        {
            Improvians_General objGeneral = new Improvians_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select  distinct  w.[Location Code] loc ";
                strQuery += "from [GTI$IA Job Activity Scheme Line] b, [GTI$IA Job Production Scheme Line] p, ";
                strQuery += "[GTI$Job] j left outer join [GTI$IA Work Order Header] w on j.No_ = w.[Job No_] ";
                strQuery += "where b.[Job No_] = p.[Job No_] And b.[Job No_] = j.No_ And b.[Item Category] = 'SEED' and p.[Production Phase] = 'SEEDING' And ";
                strQuery += "p.[Starting Date] between @FromDate and @ToDate and b.[Actual Date] < '1/1/2000' and j.[Job Status] in (1,9) ";
                strQuery += "and b.[Job No_] not in (select jobcode from gti_jobs_seeds_plan) ";
                strQuery += "order by loc";
                objGeneral.AddParameterWithValueToSQLCommand("@FromDate", FromDate);
                objGeneral.AddParameterWithValueToSQLCommand("@ToDate", ToDate);
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable GetSeedlineLocationProductionPlanner()
        {
            Improvians_General objGeneral = new Improvians_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select  distinct  w.[Location Code] loc ";
                strQuery += "from [GTI$IA Job Activity Scheme Line] b, [GTI$IA Job Production Scheme Line] p, ";
                strQuery += "[GTI$Job] j left outer join [GTI$IA Work Order Header] w on j.No_ = w.[Job No_] ";
                strQuery += "where b.[Job No_] = p.[Job No_] And b.[Job No_] = j.No_ And b.[Item Category] = 'SEED' and p.[Production Phase] = 'SEEDING' And ";
                strQuery += " b.[Actual Date] < '1/1/2000' and j.[Job Status] in (1,9) ";
                strQuery += "and b.[Job No_] not in (select jobcode from gti_jobs_seeds_plan) ";
                strQuery += "order by loc";
              
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable GetItems(string FromDate, string ToDate)
        {
            Improvians_General objGeneral = new Improvians_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select  distinct  j.[Item Description] itmdescp ";
                strQuery += "from [GTI$IA Job Activity Scheme Line] b, [GTI$IA Job Production Scheme Line] p, ";
                strQuery += "[GTI$Job] j left outer join [GTI$IA Work Order Header] w on j.No_ = w.[Job No_] ";
                strQuery += "where b.[Job No_] = p.[Job No_] And b.[Job No_] = j.No_ And b.[Item Category] = 'SEED' and p.[Production Phase] = 'SEEDING' And ";
                strQuery += "p.[Starting Date] between @FromDate and @ToDate and b.[Actual Date] < '1/1/2000' and j.[Job Status] in (1,9) ";
                strQuery += "and b.[Job No_] not in (select jobcode from gti_jobs_seeds_plan) ";
                strQuery += "order by itmdescp";
                objGeneral.AddParameterWithValueToSQLCommand("@FromDate", FromDate);
                objGeneral.AddParameterWithValueToSQLCommand("@ToDate", ToDate);
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable GetTraysize(string FromDate, string ToDate)
        {
            Improvians_General objGeneral = new Improvians_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select  distinct  j.[Variant Code] ts ";
                strQuery += "from [GTI$IA Job Activity Scheme Line] b, [GTI$IA Job Production Scheme Line] p, ";
                strQuery += "[GTI$Job] j left outer join [GTI$IA Work Order Header] w on j.No_ = w.[Job No_] ";
                strQuery += "where b.[Job No_] = p.[Job No_] And b.[Job No_] = j.No_ And b.[Item Category] = 'SEED' and p.[Production Phase] = 'SEEDING' And ";
                strQuery += "p.[Starting Date] between @FromDate and @ToDate and b.[Actual Date] < '1/1/2000' and j.[Job Status] in (1,9) ";
                strQuery += "and b.[Job No_] not in (select jobcode from gti_jobs_seeds_plan) ";
                strQuery += "order by ts";
                objGeneral.AddParameterWithValueToSQLCommand("@FromDate", FromDate);
                objGeneral.AddParameterWithValueToSQLCommand("@ToDate", ToDate);
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable GetSeedDateData(string ActivityCode, string GenusCode, string ContainerCode)
        {
            Improvians_General objGeneral = new Improvians_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select top 1 h.Code, h.[Container Code], h.[Genus Code], l.[Activity Code], l.[Date Shift] DateShift 	 ";
                strQuery += " from [GTI$IA Activity Scheme] h, [GTI$IA Activity Scheme Line] l ";
                strQuery += "where h.Code = l.[Activity Scheme Code]	 and l.[Activity Code] ='"+ ActivityCode + "' and  h.[Genus Code]='"+ GenusCode + "'  and h.[Container Code]='"+ ContainerCode + "' ";

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

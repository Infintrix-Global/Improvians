using Evo.Bal;
using Evo.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Evo.Bal
{
    public class Bal_SeedingPlan
    {
        Evo_General objGeneral = new Evo_General();
        DataSet ds = new DataSet();


        private string strQuery = string.Empty;
        public DataTable GetDataSeedingPlan(string FromDate, string ToDate, string loc, string item, string SeedAllocation, string traysize,string cname)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
              string cname1 = cname.Replace("'", "''");

                strQuery = "select w.No_ wo, b.[Job No_] jobcode, j.[Bill-to Name] cname, p.[Starting Date] sodate, j.[Item Description] itmdescp, j.[Item No_] itm, j.[Variant Code] ts, b.[Container Qty_] sotrays, j.[Bill-to Customer No_] cusno, j.[Delivery Date] duedate,b.[Genus Code] GenusCode,w.[Location Code] loc,";
                strQuery += " w.[Calc_ Quantity] wotrays, w.[Planned Date] wodate, case when j.[Job Status] =1 then 'Yes' else 'No'end alloc  , case when j.[Shortcut Property 2 Value] = 'Yes' then 'ORG' else 'CONV'end Soil  ";
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
                if (cname != "0")
                    strQuery += " And  j.[Bill-to Name] like " + "'%" + cname1 + "%'";
                 strQuery += " order by alloc desc, loc desc, sodate";
               // strQuery += " order by sodate";
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

        public DataTable GetDataSeedingPlanApp()
        {
            General objGeneral = new General();
            DataTable dt = new DataTable();
            try
            {


                strQuery = "Select wo,jobcode,cname,sodate, itemdescp as itmdescp,'BR-IMPERIAL ORG' as itm,TraySize as ts,320.00000000000000000000 as sotrays,cusno,due_date as duedate,GenusCode,loc_seedline as loc, ";
                strQuery += " 320.00000000000000000000 as wotrays,due_date as wodate, 'Yes' as alloc,'ORG' as Soil ";
                strQuery += " from gti_jobs_seeds_plan where jobcode='JB0200002'";
             
                dt = objGeneral.GetDatasetByCommand(strQuery);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }




        public DataTable GetDataSeedingPlanNew()
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
               


                strQuery = "select w.No_ wo, b.[Job No_] jobcode, j.[Bill-to Name] cname, p.[Starting Date] sodate, j.[Item Description] itmdescp, j.[Item No_] itm, j.[Variant Code] ts, b.[Container Qty_] sotrays, j.[Bill-to Customer No_] cusno, j.[Delivery Date] duedate,b.[Genus Code] GenusCode,w.[Location Code] loc,";
                strQuery += " w.[Calc_ Quantity] wotrays, w.[Planned Date] wodate, case when j.[Job Status] =1 then 'Yes' else 'No'end alloc ";
                strQuery += "from [GTI$IA Job Activity Scheme Line] b, [GTI$IA Job Production Scheme Line] p, ";
                strQuery += "[GTI$Job] j left outer join [GTI$IA Work Order Header] w on j.No_ = w.[Job No_] ";
                strQuery += "where b.[Job No_] = p.[Job No_] And b.[Job No_] = j.No_ And b.[Item Category] = 'SEED' and p.[Production Phase] = 'SEEDING' And ";
                strQuery += "  b.[Actual Date] < '1/1/2000' and j.[Job Status] in (1) ";
                //strQuery += "and b.[Job No_] not in (select jobcode from gti_jobs_seeds_plan) ";

              
                dt = objGeneral.GetDatasetByCommand(strQuery);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }




        public DataTable GetDataSeedingPlanManual(string SeedDate)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {

                strQuery = "select distinct  t.[Job No_] as jobcode,'' as wo,0 as GrowerPutAwayId, j.[Bill-to Name] as cname , j.[Item Description] as itemdescp, j.[Item No_] as itemno" +
                    " ,t.[Genus Code] GenusCode,t.[Posting Date] seeddate , j.[Delivery Date] as DueDate,t.[Location Code] as FacilityID,t.[Position Code] as GreenHouseID,CAST(sum(t.Quantity) AS int ) as Trays,j.[Variant Code] as TraySize, j.[Shortcut Property 1 Value] germcount" +
                    "  from[GTI$IA Job Tracking Entry] t, [GTI$Job] j  where j.No_ = t.[Job No_] and j.[Job Status] = 2 and t.[Activity Code] like 'PUTAWAY%'  " +
                    " group by t.[Job No_], j.[Bill-to Name], j.[Item Description],t.[Genus Code], t.[Location Code],j.[Item No_],t.[Position Code],t.[Location Code],j.[Variant Code],t.[Posting Date],j.[Shortcut Property 1 Value], j.[Delivery Date] " +
                    "  HAVING CAST(sum(t.Quantity) AS int )  > 0 ";


                //if (SeedDate != "")
                //{
                //    strQuery += " And t.[Posting Date]> " + "'" + SeedDate + "'";
                //}


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
            Evo_General objGeneral = new Evo_General();
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
            Evo_General objGeneral = new Evo_General();
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
            Evo_General objGeneral = new Evo_General();
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


        public DataTable GetCustomer(string FromDate, string ToDate)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select  distinct  j.[Bill-to Name] cname  ";
                strQuery += "from [GTI$IA Job Activity Scheme Line] b, [GTI$IA Job Production Scheme Line] p, ";
                strQuery += "[GTI$Job] j left outer join [GTI$IA Work Order Header] w on j.No_ = w.[Job No_] ";
                strQuery += "where b.[Job No_] = p.[Job No_] And b.[Job No_] = j.No_ And b.[Item Category] = 'SEED' and p.[Production Phase] = 'SEEDING' And ";
                strQuery += "p.[Starting Date] between @FromDate and @ToDate and b.[Actual Date] < '1/1/2000' and j.[Job Status] in (1,9) ";
                strQuery += "and b.[Job No_] not in (select jobcode from gti_jobs_seeds_plan) ";
                strQuery += "order by  j.[Bill-to Name]";
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
            Evo_General objGeneral = new Evo_General();
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
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select top 1 h.Code, h.[Container Code], h.[Genus Code], l.[Activity Code], l.[Date Shift] DateShift 	 ";
                strQuery += " from [GTI$IA Activity Scheme] h, [GTI$IA Activity Scheme Line] l ";
                strQuery += "where h.Code = l.[Activity Scheme Code]	 and l.[Activity Code] ='" + ActivityCode + "' and  h.[Genus Code]='" + GenusCode + "'  and h.[Container Code]='" + ContainerCode + "' ";

                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }




        public DataTable GetSeedDataCheck(string JobCode, string ActivityCode)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "	select w.[Activity Code] act, w.Date assigndate, w.[Actual Date] compdate from [GTI$IA Job Activity Scheme Line] w  ";
                strQuery += " where w.[Job No_] = '" + JobCode + "' and w.Type = 2 and  w.[Activity Code]='" + ActivityCode + "' order by w.Date 	 ";

                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }



        public DataTable GetSeedDateDatanew(string ActivityCode, string GenusCode, string ContainerCode)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                //strQuery = "select  h.Code, h.[Container Code], h.[Genus Code], l.[Activity Code], l.[Date Shift] DateShift 	 ";
                //strQuery += " from [GTI$IA Activity Scheme] h, [GTI$IA Activity Scheme Line] l ";
                //strQuery += "where h.Code = l.[Activity Scheme Code]	 and l.[Activity Code] ='" + ActivityCode + "' and  h.[Genus Code]='" + GenusCode + "'  and h.[Container Code]='" + ContainerCode + "' ";

                 strQuery = " Select *, dateshift as DateShift  from gti_jobs_prodprofile where activitycode = '"+ ActivityCode + "' and crop = '" + GenusCode + "'  and traycode = '" + ContainerCode + "' ";

                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetSeedDateDateShift(string ActivityCode, string GenusCode, string ContainerCode)
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "  Select * from [gti_jobs_prodprofile] where crop='"+ GenusCode + "' and activitycode='"+ ActivityCode + "' and traycode ='"+ ContainerCode + "'	 ";
              
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

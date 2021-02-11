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
        public DataTable GetDataSeedingPlan(string FromDate, string ToDate)
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


                strQuery = "select w.No_ wo, b.[Job No_] jobcode, j.[Bill-to Name] cname, p.[Starting Date] sodate, j.[Item Description] itmdescp, j.[Item No_] itm, j.[Variant Code] ts, b.[Container Qty_] sotrays, j.[Bill-to Customer No_] cusno, j.[Delivery Date] duedate, w.[Location Code] loc,";
                strQuery += " w.[Calc_ Quantity] wotrays, w.[Planned Date] wodate, case when j.[Job Status] =1 then 'Yes' else 'No'end alloc ";
                strQuery += "from [GTI$IA Job Activity Scheme Line] b, [GTI$IA Job Production Scheme Line] p, ";
                strQuery += "[GTI$Job] j left outer join [GTI$IA Work Order Header] w on j.No_ = w.[Job No_] ";
                strQuery += "where b.[Job No_] = p.[Job No_] And b.[Job No_] = j.No_ And b.[Item Category] = 'SEED' and p.[Production Phase] = 'SEEDING' And ";
                strQuery += "p.[Starting Date] between @FromDate and @ToDate and b.[Actual Date] < '1/1/2000' and j.[Job Status] in (1,9) ";
                strQuery += "and b.[Job No_] not in (select jobcode from gti_jobs_seeds_plan) ";
                strQuery += "order by alloc desc, loc desc, sodate";

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



    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Evo.Bal;


namespace Improvians.Bal
{
    public class BAL_CommonMasters
    {
        Improvians_General objGeneral = new Improvians_General();
        DataSet ds = new DataSet();


        private string strQuery = string.Empty;
        public DataTable GetSeedLot(string JobCode)
        {
            Improvians_General objGeneral = new Improvians_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "select le.[Lot No_] l1, le.[Lot No_] l2, sum(le.Quantity) from[GTI$Item Ledger Entry] le "+
                  " where le.[Item No_] = (select i.[Purchase Item No_] from[GTI$Item] i "+
                  "where i.No_ in (select j.[Item No_] from[GTI$Job] j where j.No_ = '"+ JobCode + "'))  " +
                   " group by[Lot No_] " +
                    " having sum(le.Quantity) > 0 ";
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
            Improvians_General objGeneral = new Improvians_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "Select distinct s.[Location Code] l1, s.[Location Code] l2 from [GTI$IA Subsection] s order by s.[Location Code]";
                
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
            Improvians_General objGeneral = new Improvians_General();
            DataTable dt = new DataTable();
            try
            {
                strQuery = "Select s.[Position Code], s.[Position Code] p2 from [GTI$IA Subsection] s where s.[Location Code] = '"+ MainLocation + "'";              
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
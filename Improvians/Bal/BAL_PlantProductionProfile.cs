using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Evo.Bal
{
    public class BAL_PlantProductionProfile
    {
        public DataTable GetCodeList()
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
              string  strQuery = "select distinct h.Code from [GTI$IA Activity Scheme] h order by [Code]";
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetCropList()
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                string strQuery = "select distinct h.[Genus Code] Crop from [GTI$IA Activity Scheme] h order by [Genus Code]";
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetTraysizeList()
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                string strQuery = "select distinct h.[Container Code] traysize from [GTI$IA Activity Scheme] h order by [Container Code] ";
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetActivityCodeList()
        {
            Evo_General objGeneral = new Evo_General();
            DataTable dt = new DataTable();
            try
            {
                string strQuery = "select distinct l.[Activity Code] ActivityCode from [GTI$IA Activity Scheme Line] l order by [Activity Code]";
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
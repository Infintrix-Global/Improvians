using Evo.Bal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class JobReport : System.Web.UI.Page
    {
        Improvians_General objGeneral = new Improvians_General();
        string jb;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BTrun_Click(object sender, EventArgs e)
        {
            jb = Txtjob.Text;
            FillDGHeader01();
        }
        public void FillDGHeader01()
        {

            string sql = "select j.No_ jobcode, j.[Shortcut Property 1 Value] germpct, j.[Bill-to Name] cname, j.[Item No_] itemno, j.[Item Description] itemdescp, " + "sum(t.Quantity) trays, j.[Delivery Date] ready_date, m.[Production Phase] pphase, " + "j.[Source No_] + '-' + convert(nvarchar,j.[Source Line No_]/1000) solines, j.[Variant Code] ts, j.[Source No_] sono,j.[Source Line No_] soline, " + "j.[Genus Code] crop, j.[Shortcut Property 10 Value] overage, " + "CASE WHEN m.[Closed at Date] < '2000-01-01' THEN m.[Posting Date] ELSE m.[Closed at Date] END seeddt, " + "CASE WHEN j.[Shortcut Property 2 Value] = 'Yes' THEN 'Yes' ELSE 'NO' END org " + "from [GTI$IA Job Tracking Entry] t, [GTI$Job] j " + "LEFT OUTER JOIN [GTI$IA Job Mutation Entry] m ON j.No_ = m.[Job No_] and m.[Production Phase] in ('SEEDING','RETURNS') " + "where j.No_ = t.[Job No_] And j.No_ = '" + jb + "' " + "group by j.No_, j.[Shortcut Property 2 Value], j.[Shortcut Property 1 Value], j.[Bill-to Name], j.[Item No_], j.[Item Description], " + "j.[Delivery Date], m.[Closed at Date], m.[Production Phase], m.[Posting Date], j.[Source No_], j.[Source Line No_], j.[Variant Code], j.[Genus Code], " + "j.[Shortcut Property 10 Value]";


            DGHead01.DataSource = objGeneral.GetDatasetByCommand(sql);
            DGHead01.DataBind();



            FillDGHeader02();
        }

        public void FillDGHeader02()
        {


            string sql1 = "select j.No_ jobcode, j.[Shortcut Property 1 Value] germpct, j.[Bill-to Name] cname, j.[Item No_] itemno, j.[Item Description] itemdescp, " + "sum(t.Quantity) trays, j.[Delivery Date] ready_date, m.[Production Phase] pphase, " + "j.[Source No_] + '-' + convert(nvarchar,j.[Source Line No_]/1000) solines, j.[Variant Code] ts, j.[Source No_] sono,j.[Source Line No_] soline, " + "j.[Genus Code] crop, j.[Shortcut Property 10 Value] overage, " + "CASE WHEN m.[Closed at Date] < '2000-01-01' THEN m.[Posting Date] ELSE m.[Closed at Date] END seeddt, " + "CASE WHEN j.[Shortcut Property 2 Value] = 'Yes' THEN 'Yes' ELSE 'NO' END org " + "from [GTI$IA Job Tracking Entry] t, [GTI$Job] j " + "LEFT OUTER JOIN [GTI$IA Job Mutation Entry] m ON j.No_ = m.[Job No_] and m.[Production Phase] in ('SEEDING','RETURNS') " + "where j.No_ = t.[Job No_] And j.No_ = '" + jb + "' " + "group by j.No_, j.[Shortcut Property 2 Value], j.[Shortcut Property 1 Value], j.[Bill-to Name], j.[Item No_], j.[Item Description], " + "j.[Delivery Date], m.[Closed at Date], m.[Production Phase], m.[Posting Date], j.[Source No_], j.[Source Line No_], j.[Variant Code], j.[Genus Code], " + "j.[Shortcut Property 10 Value]";


            DGHead02.DataSource = objGeneral.GetDatasetByCommand(sql1);
            DGHead02.DataBind();

            FillDGSeeds();
        }

        public void FillDGSeeds()
        {
            string sql2;


            sql2 = "select le.[Item No_] seed, le.[Lot No_] lot, le.Quantity qty " + "from [GTI$Item Ledger Entry] le " + "where le.[Job No_] = '" + jb + "' and le.[Item Category Code] = 'SEED'";

            DGSeeds.DataSource = objGeneral.GetDatasetByCommand(sql2);
            DGSeeds.DataBind();


            FillDGTasks();
        }

        public void FillDGTasks()
        {
            string sql3;
            sql3 = "select w.[Activity Code] act, w.Date assigndate, w.[Actual Date] compdate " + "from [GTI$IA Job Activity Scheme Line] w " + "where w.[Job No_] = '" + jb + "' and w.Type = 2 order by w.Date";


            DGTasks.DataSource = objGeneral.GetDatasetByCommand(sql3);

            DGTasks.DataBind();
            FillDGInventory();
        }

        public void FillDGInventory()
        {
            string sql4;
          
            sql4 = "select t.[Job No_] jobno, t.[Location Code] loc, t.[Position Code] bench, sum(t.Quantity) trays " + "from [GTI$IA Job Tracking Entry] t " + "where t.[Job No_] = '" + jb + "' " + "group by t.[Job No_], t.[Location Code], t.[Position Code]";
          
            DGInventory.DataSource = objGeneral.GetDatasetByCommand(sql4);
            DGInventory.DataBind();

          //  Lblinvct.Text = ct + " TRAYS";

            FillDGHealth();
        }

        public void FillDGHealth()
        {
            string sql5;
                      sql5 = "select h.Date dt, l.[Category Code] cat, l.Description descp, l.Remark " + "from [GTI$IA Obs_ Inspection Header] h, [GTI$IA Obs_ Inspection Line] l " + "where h.No_ = l.No_ and h.[Source Document No_] = '" + jb + "'";
           
            DGHealth.DataSource = objGeneral.GetDatasetByCommand(sql5);
            DGHealth.DataBind();

            //if (DGHealth.Rows.Count > 0)
            //    Pnlhealth.Visible = true;

            //cn5.CloseDB2();
        }
    }
}
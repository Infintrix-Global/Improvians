﻿using Evo.BAL_Classes;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evo
{
    public partial class PrintSeedlinePlannerReport : System.Web.UI.Page
    {
        General objGeneral = new General();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRepeater();
            }
        }

      
        private void BindRepeater()
        {
            string strSQL = " select distinct loc_seedline, CONVERT(date, CreateOn) as CreateOn from gti_jobs_seeds_plan";
            DataTable dt = objGeneral.GetDatasetByCommand(strSQL);
            repReport.DataSource = dt;
            repReport.DataBind();
        }

        protected void repReport_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            GridView DGJob = (GridView)e.Item.FindControl("DGJob");
            Label lblFacility = (Label)e.Item.FindControl("lblFacility");
            Label lblDate = (Label)e.Item.FindControl("lblDate");
            string strSQL = "select * from gti_jobs_seeds_plan where loc_seedline='" + lblFacility.Text + "' and CONVERT(date,createon)='" + lblDate.Text + "'" ;
            DataTable dt = objGeneral.GetDatasetByCommand(strSQL);
            DGJob.DataSource = dt;
            DGJob.DataBind();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string attachment = "attachment; filename=" + "abc" + ".pdf";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/pdf";
            StringWriter s_tw = new StringWriter();
            HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
            h_textw.AddStyleAttribute("font-size", "7pt");
            h_textw.AddStyleAttribute("color", "Black");
            Panel1.RenderControl(h_textw);//Name of the Panel  
            Document doc = new Document();
            doc = new Document(PageSize.LETTER, 5, 5, 15, 5);
            //FontFactory.GetFont("Verdana", 80, iTextSharp.text.Color.RED);
            PdfWriter.GetInstance(doc, Response.OutputStream);
            doc.Open();
            StringReader s_tr = new StringReader(s_tw.ToString());
            HTMLWorker html_worker = new HTMLWorker(doc);
            html_worker.Parse(s_tr);
            doc.Close();
            Response.Write(doc);
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}
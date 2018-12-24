using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eve4SalesApp.Models;
using System.IO;
using ClosedXML.Excel;


namespace Eve4SalesApp.Controllers
{
    public class ExcelExportViewModelsController : Controller
    {
        private SalesDb db = new SalesDb();

        // GET: ExcelExportViewModels
        public ActionResult Index()
        {
             
            return View();

        }


        public FileResult Export()
        {
            DataTable dt = new DataTable("EVE_RawData");

            DataColumn year = new DataColumn("Year");
            year.DataType = System.Type.GetType("System.Int32");

            DataColumn shipment = new DataColumn("Shipment");
            shipment.DataType = System.Type.GetType("System.Int32");

            DataColumn doneflag = new DataColumn("DoneFlag");
            doneflag.DataType = System.Type.GetType("System.Int32");

            DataColumn naflag = new DataColumn("NotAvailableFlag");
            naflag.DataType = System.Type.GetType("System.Int32");

            DataColumn tier = new DataColumn("Tier");
            tier.DataType = System.Type.GetType("System.Int32");

            DataColumn rank = new DataColumn("Rank");
            rank.DataType = System.Type.GetType("System.Int32");

            DataColumn portalstatus = new DataColumn("PortalStatus");
            portalstatus.DataType = System.Type.GetType("System.Int32");

            DataColumn manualadd = new DataColumn("ManualAdd");
            manualadd.DataType = System.Type.GetType("System.Int32");

            DataColumn salesfigurecalculatedfromrank = new DataColumn("Sales_RankBased");
            salesfigurecalculatedfromrank.DataType = System.Type.GetType("System.Int32");

            DataColumn mmid = new DataColumn("MarketModelId");
            mmid.DataType = System.Type.GetType("System.Int32");
            
            dt.Columns.AddRange(new DataColumn[15] { year,
                                                     new DataColumn("Market"),
                                                     new DataColumn("Make"),
                                                     new DataColumn("Model"),
                                                     shipment,
                                                     doneflag,
                                                     naflag,
                                                     tier,
                                                     rank,
                                                     new DataColumn("LastSourced"),
                                                     new DataColumn("Release"),
                                                     portalstatus,
                                                     manualadd,
                                                     salesfigurecalculatedfromrank,
                                                     mmid  });

            var xp = db.Database.SqlQuery<ExcelExportViewModel>("sp_DisplayFilteredView"); 

            foreach (var row in xp)
            {
                dt.Rows.Add(    row.Year, 
                                row.Market, 
                                row.Make, 
                                row.Model, 
                                row.Shipment, 
                                row.DoneFlag, 
                                row.NotAvailableFlag,
                                row.Tier,
                                row.Rank, 
                                row.LastSourced, 
                                row.Release,
                                row.PortalStatus, 
                                row.ManualAdd,
                                row.SalesFiguresCalculatedFromRank,
                                row.MarketModelId);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EVE_RawDataExport.xlsx");
                }
            }
        }






        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

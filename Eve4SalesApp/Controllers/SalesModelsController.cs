using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eve4SalesApp.Models;
using System.Data.SqlClient;

namespace Eve4SalesApp.Controllers
{
    public class SalesModelsController : Controller
    {
        private SalesDb db = new SalesDb();

        // GET: SalesModels
        public ActionResult Index(string marketCode = null, int timeFrame = 0, string refinedSearch = null)
        {
            
            if (marketCode == null) { marketCode = (string)TempData["mc"]; TempData["mc"] = marketCode; }
            else { TempData["mc"] = marketCode;  }

            if (timeFrame == 0) { timeFrame = (int)TempData["tf"]; TempData["tf"] = timeFrame; }
            else { TempData["tf"] = timeFrame; }

            if (refinedSearch == null) {

                            refinedSearch = (string)TempData["rs"]; TempData["rs"] = refinedSearch;

            } else if (refinedSearch == "."){

                            refinedSearch = "";
                            TempData["rs"] = refinedSearch;

            } else {

                            TempData["rs"] = refinedSearch;
            }

            ViewBag.CurrentMarketCode = marketCode;
            ViewBag.CurrentYear = timeFrame;
            ViewBag.refinedSearch = refinedSearch; 

         
       var model = from s in db.SalesModels
                        join m in db.SalesMarketModels on s.SalesModelID equals m.SalesModelId
                        join d in db.ModelYearDeclarations on m.MarketModelId equals d.MarketModelId
                        join f in db.SalesFigures on m.MarketModelId equals f.MarketModelId
                        join a in db.PortalStats on s.Make equals a.Make
                        where f.TimeFrame.Equals(timeFrame) && m.MarketCode.Contains(marketCode) && d.ForYear.Equals(timeFrame)
                   orderby s.SalesModelID ascending, d.ForYear ascending
                        select new MapViewModel
                        {
                            MarketModelId = m.MarketModelId,
                            Make = s.Make,
                            Model = s.Model,
                            Market = m.MarketCode,
                            TotalSales = f.TotalSales,
                            TransID = d.TransID, 
                            ForYear = d.ForYear,
                            DoneFlag = d.DoneFlag, 
                            NotAvailableFlag = d.NotAvailableFlag,
                            MakeColour = a.Colour, 
                            Tier = f.Tier, 
                            ModelRank = f.ModelRank,
                            BacklogGrp = s.BacklogGrp, 
                            GrpOID = s.GrpOID, 
                            ReleaseInfo = d.ReleaseInfo, 
                            ManualAdd = f.ManualAdd,
                            SalesCalculatedFromRank = f.SalesCalculatedFromRank
                        };

            if (refinedSearch != null) {

                model = model.Where( x => (x.Make.Contains(refinedSearch)) || (x.Model.Contains(refinedSearch)));
                
            }


            if (!String.IsNullOrEmpty(marketCode))
            {
                model = model.Where(s => s.Market.Contains(marketCode))
                             .OrderBy(s => s.ModelRank);

            }
            return View(model);



        }


        public ActionResult Stats()
        {
            
            var marketcodelist = (from mc in db.SalesMarketModels
                                  select mc.MarketCode).Distinct().ToList();

            ViewBag.ListofMarkets = marketcodelist;

            var yearlist = (from mc in db.SalesFigures
                            orderby mc.TimeFrame
                            select mc.TimeFrame).Distinct().ToList();

            ViewBag.ListofYears = yearlist;

            
            return View();
            
        }
         
         public ActionResult StatsResult(int timeframe = 2017, string marketcode = "NA")
        {


            var stats = db.Database.SqlQuery<StatsViewModel>("sp_CalculateMarketPercentage_Partial @SalesYear, @MarketCode",
    new SqlParameter("@SalesYear", timeframe),
    new SqlParameter("@MarketCode", marketcode));


            return View(stats); 


        }


        public ActionResult AggEu(int timeframe = 2018)
        {


            var list = db.Database.SqlQuery<AggEuList>("sp_CalculateEuAggSales @SalesYear",
    new SqlParameter("@SalesYear", timeframe));

            ViewBag.TimeFrame = timeframe;

            var yearlist = (from mc in db.SalesFigures
                            orderby mc.TimeFrame descending
                            select mc.TimeFrame).Distinct().ToList();

            ViewBag.ListofYears = yearlist;



            return View(list);


        }


        public ActionResult MarketOverview()
        {


            var overview = db.Database.SqlQuery<MarketOverviewViewModel>("sp_MarketPerCentageOverview"); 


            return View(overview);


        }
        
               

        // GET: SalesModels/Create
        [Authorize]
        public ActionResult Create()
        {
            var marketcodelist = (from mc in db.SalesMarketModels

           select mc.MarketCode).Distinct().ToList();

            ViewBag.ListofMarkets = marketcodelist;

            var makeList = (from mc in db.PortalStats
                            orderby mc.Make descending
                            select mc.Make).Distinct().ToList();

            ViewBag.ListOFMakes = makeList;

            var yearList = (from yl in db.SalesFigures
                            select yl.TimeFrame).Distinct().ToList();

            int maxYear = (from yl in db.SalesFigures
                            select yl.TimeFrame).Max();

            yearList.Add(maxYear + 1);
            yearList.Add(maxYear + 2);
            yearList.Add(maxYear + 3);

            ViewBag.ListOFYears = yearList;

            var rankList = (from yl in db.SalesFigures
                            select yl.ModelRank).Distinct().ToList();

            int maxRank = (from yl in db.SalesFigures
                           select yl.ModelRank).Max();


            rankList.Add(maxRank + 1);

            ViewBag.ListOfRanks = rankList;


            return View();
        }

        // POST: SalesModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Make,Model,Sales,SalesYearBegin,SalesYearEnd,MarketCode,UnknownSales,Rank")] UserEditModel ueModel)
        {

           if (ModelState.IsValid)
            {

                if (ueModel.SalesYearEnd < ueModel.SalesYearBegin)
                {

                    return View("CreateError");


                } else {

                    /*   try
                       { */



                           bool unknownsalesCheck = false;

                           if (ueModel.UnknownSales != null && ueModel.UnknownSales != false)
                           {

                               unknownsalesCheck = true;
                           }


                           int? rankCheck = 0;

                           if (ueModel.Rank == null)
                           {

                               rankCheck = 0;
                           }
                           else
                           {

                               rankCheck = ueModel.Rank;

                           }




                           foreach (string xMarket in ueModel.MarketCode)
                           {

                               for (int xYear = ueModel.SalesYearBegin; xYear <= ueModel.SalesYearEnd; xYear++)
                               {




                                   SqlParameter make = new SqlParameter("@Make", ueModel.Make);
                                   SqlParameter model = new SqlParameter("@Model", ueModel.Model);
                                   SqlParameter sales = new SqlParameter("@Sales", ueModel.Sales);
                                   SqlParameter salesyear = new SqlParameter("@SalesYear", xYear);
                                   SqlParameter marketcode = new SqlParameter("@MarketCode", xMarket);
                                   SqlParameter unknownsales = new SqlParameter("@UnknownSales", unknownsalesCheck);
                                   SqlParameter rank = new SqlParameter("@Rank", rankCheck);

                                   db.Database.ExecuteSqlCommand("sp_UIAddNewMarketModelTransaction @Make, @Model, @Sales, @SalesYear, @MarketCode, @UnknownSales, @Rank",
                                                      make, model, sales, salesyear, marketcode, unknownsales, rank);

                               }

                           }


                  /*     }
                       catch {

                           return View("CreateError");

                       } */


                    return View("CreateSummary");

                }
                               
            }
            else
            {

                return View("CreateError");

            }

          
            
        }

        // GET: SalesModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesModel salesModel = db.SalesModels.Find(id);
            if (salesModel == null)
            {
                return HttpNotFound();
            }
            return View(salesModel);
        }

        // POST: SalesModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesModelID,Make,Model,Market")] SalesModel salesModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salesModel);
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

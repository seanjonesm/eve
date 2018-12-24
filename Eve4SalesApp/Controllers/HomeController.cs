using Eve4SalesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eve4SalesApp.Controllers
{
    public class HomeController : Controller
    {

        private SalesDb db = new SalesDb();


        public ActionResult Index()
        {


            var marketcodelist = (from mc in db.SalesMarketModels
                                  select mc.MarketCode).Distinct().ToList();
                     
            
            ViewBag.ListofMarkets = marketcodelist;

            var yearlist = (from mc in db.SalesFigures
                                   orderby mc.TimeFrame descending
                                  select mc.TimeFrame).Distinct().ToList();

            ViewBag.ListofYears = yearlist;


            return View(); 

        }


    }
}
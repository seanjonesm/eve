using Eve4SalesApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Eve4SalesApp.Controllers
{
    public class V1IntegrationController : Controller
    {
        private SalesDb db = new SalesDb();

        [Authorize]
        public ActionResult ListOID(string cMake = "")
        {

            if (cMake == "") {
                cMake = (string)TempData["OidSelectedMake"];
                }
            TempData["OidSelectedMake"] = cMake;


            var makeList = (from mc in db.SalesModels
                            orderby mc.Make descending
                            select mc.Make).Distinct().ToList();
       
            ViewBag.ListOFMakes = makeList;

            if (cMake == "" || cMake == null)
            {
                ViewBag.CurrentMake = "Audi";
                ViewBag.MakeSelected = "no"; 
            }
            else {
                ViewBag.CurrentMake = cMake;
                ViewBag.MakeSelected = "yes";
            }



            if (cMake == "")
            {
                    var model = from s in db.SalesModels
                            join m in db.SalesMarketModels on s.SalesModelID equals m.SalesModelId
                            join f in db.SalesFigures on m.MarketModelId equals f.MarketModelId
                            where f.TimeFrame >= 2017
                            orderby s.Make ascending, s.Model ascending
                            select new ListOIDViewModel
                            {
                                SalesModelID = s.SalesModelID,
                                Make = s.Make,
                                Model = s.Model,
                                BacklogGrp = s.BacklogGrp,
                                GrpOID = s.GrpOID

                            };
                return View(model.Distinct().OrderBy(m => m.Make));
            }

            else {

                var model =     from s in db.SalesModels
                            join m in db.SalesMarketModels on s.SalesModelID equals m.SalesModelId
                            join f in db.SalesFigures on m.MarketModelId equals f.MarketModelId
                            where f.TimeFrame >= 2017 && s.Make.Contains(cMake.Trim())
                            orderby s.Model ascending
                            select new ListOIDViewModel
                            {
                                SalesModelID = s.SalesModelID,
                                Make = s.Make,
                                Model = s.Model,
                                BacklogGrp = s.BacklogGrp,
                                GrpOID = s.GrpOID

                            };
                return View(model.Distinct().OrderBy(m => m.Model));
            }
            
            }

        [Authorize]
        public ActionResult EditOID(int? id)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOID([Bind(Include = "SalesModelId,Make,Model,BacklogGrp,GrpOID")] SalesModel salesModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesModel).State = EntityState.Modified;
                db.SaveChanges();
                        
            }

            return RedirectToAction("ListOID", "V1Integration");
        }






    }
}
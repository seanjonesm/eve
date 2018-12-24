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
    public class MaintenanceController : Controller
    {
        private SalesDb db = new SalesDb();

        // GET: Maintenance
        [Authorize]
        public ActionResult UpdateRelease()
        {

            ReleaseModel rm = db.CurrentRelease.First();

            if (rm == null)
            {
                return HttpNotFound();
            }

            return View(rm);

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateRelease([Bind(Include = "TransID,CurrentRelease")] ReleaseModel rm)
        {


            if (ModelState.IsValid)
            {
                db.Entry(rm).State = EntityState.Modified;
                db.SaveChanges();

            }
            return View("UpdateReleaseSuccess");

        }



        public ActionResult EditModel(int transId)
        {

                  
            SqlParameter xid = new SqlParameter("@TransId", transId);

            EditModel em = db.Database.SqlQuery<EditModel>("sp_UIEditModel_Select @TransId", xid).FirstOrDefault();

            if (em == null)
            {
                return HttpNotFound();
            }

            return View(em);

        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditModel([Bind(Include = "CovTransId,Make,Model,Market,xMarket,Year,Sales,DeleteModel,ManualAdd,RollOn,RollOnYear")] EditModel em)
        {


            if (ModelState.IsValid)
            {
                /* int trId = em.CovTransId; 
                 var xmarket = (from c in db.ModelYearDeclarations
                                   join m in db.SalesMarketModels on c.MarketModelId equals m.MarketModelId
                                   where c.TransID.Equals(trId)
                                  select m.MarketCode);
                 string xxmarket = xmarket.First().ToString(); */

                
                             
               

                if (em.RollOn) {

                    UserEditModel ue = new UserEditModel();

                    ue.Make = em.Make;
                    ue.Model = em.Model;
                    ue.Sales = 1;
                    ue.Rank = em.Rank;
                    ue.SalesYearBegin = em.RollOnYear;
                    ue.SalesYearEnd = em.RollOnYear;
                    ue.UnknownSales = true;
                    ue.MarketCode = new List<string> { em.Market };

                    foreach (string xMarket in ue.MarketCode)
                    {

                        for (int xYear = ue.SalesYearBegin; xYear <= ue.SalesYearEnd; xYear++)
                        {




                            SqlParameter make = new SqlParameter("@Make", ue.Make);
                            SqlParameter model = new SqlParameter("@Model", ue.Model);
                            SqlParameter sales = new SqlParameter("@Sales", ue.Sales);
                            SqlParameter salesyear = new SqlParameter("@SalesYear", xYear);
                            SqlParameter marketcode = new SqlParameter("@MarketCode", xMarket);
                            SqlParameter unknownsales = new SqlParameter("@UnknownSales", ue.UnknownSales);
                            SqlParameter rank = new SqlParameter("@Rank", ue.Rank);

                            db.Database.ExecuteSqlCommand("sp_UIAddNewMarketModelTransaction @Make, @Model, @Sales, @SalesYear, @MarketCode, @UnknownSales, @Rank",
                                               make, model, sales, salesyear, marketcode, unknownsales, rank);

                        }

                    }









                } else { 


                    SqlParameter xid = new SqlParameter("@TransId", em.CovTransId);
                    SqlParameter sales = new SqlParameter("@Sales", em.Sales);
                    SqlParameter deleteModel = new SqlParameter("@DeleteModel", em.DeleteModel);
        
                  
                    SqlParameter year = new SqlParameter("@SalesYear", em.Year);
                SqlParameter zmarket = new SqlParameter("@MarketCode", em.Market);

                db.Database.ExecuteSqlCommand("sp_UIEditModel_Update @TransId, @Sales, @DeleteModel",
                                       xid, sales, deleteModel);

                db.Database.ExecuteSqlCommand("sp_AssignTiering @MarketCode, @SalesYear",
                                      zmarket, year);

                }


                return View("EditModelSuccess");

               
              


            }
            else { 
            return View("EditModelError");

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

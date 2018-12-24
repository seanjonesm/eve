using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eve4SalesApp.Models;
using System.Activities.Expressions;
using System.Data.SqlClient;

namespace Eve4SalesApp.Controllers
{
    public class ModelYearDeclarationsController : Controller
    {
        private SalesDb db = new SalesDb();

     
        // GET: ModelYearDeclarations/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelYearDeclaration modelYearDeclaration = db.ModelYearDeclarations.Find(id);
                       
                                   
            if (modelYearDeclaration == null)
            {
                return HttpNotFound();
            }
                        
            return PartialView("_UpdateCoveragePartial", modelYearDeclaration);

           
        }

        

        // POST: ModelYearDeclarations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransID,MarketModelId,ForYear,DoneFlag,NotAvailableFlag,BacklogInfo,ReleaseInfo")] ModelYearDeclaration modelYearDeclaration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modelYearDeclaration).State = EntityState.Modified;
                db.SaveChanges();

                
                SqlParameter release = new SqlParameter("@release", modelYearDeclaration.ReleaseInfo);
                SqlParameter releaseNull = new SqlParameter("@release", "empty");
                SqlParameter transid = new SqlParameter("@TransId", modelYearDeclaration.TransID);
                SqlParameter doneflag = new SqlParameter("@done", modelYearDeclaration.DoneFlag);
                SqlParameter naflag = new SqlParameter("@na", modelYearDeclaration.NotAvailableFlag);
                SqlParameter user = new SqlParameter("@User", User.Identity.Name);


                if (String.IsNullOrEmpty(modelYearDeclaration.ReleaseInfo)) {

                    db.Database.ExecuteSqlCommand("sp_UICoverageUpdateAdmin @TransId, @done, @na, @release, @User",
                                                   transid, doneflag, naflag, releaseNull, user);

                } else {

                    db.Database.ExecuteSqlCommand("sp_UICoverageUpdateAdmin @TransId, @done, @na, @release, @User",
                                                 transid, doneflag, naflag, release, user);

                }
                               
                            }

            return RedirectToAction("Index", "SalesModels");
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

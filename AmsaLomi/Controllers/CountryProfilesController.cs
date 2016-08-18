using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AmsaLomi.Models;

namespace AmsaLomi.Controllers
{
    public class CountryProfilesController : Controller
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: CountryProfiles
        public ActionResult Index()
        {
            return View(db.CountryProfiles.ToList());
        }

        // GET: CountryProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryProfile countryProfile = db.CountryProfiles.Find(id);
            if (countryProfile == null)
            {
                return HttpNotFound();
            }
            return View(countryProfile);
        }

        // GET: CountryProfiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountryProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] CountryProfile countryProfile)
        {
            if (ModelState.IsValid)
            {
                db.CountryProfiles.Add(countryProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(countryProfile);
        }

        // GET: CountryProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryProfile countryProfile = db.CountryProfiles.Find(id);
            if (countryProfile == null)
            {
                return HttpNotFound();
            }
            return View(countryProfile);
        }

        // POST: CountryProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] CountryProfile countryProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(countryProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(countryProfile);
        }

        // GET: CountryProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryProfile countryProfile = db.CountryProfiles.Find(id);
            if (countryProfile == null)
            {
                return HttpNotFound();
            }
            return View(countryProfile);
        }

        // POST: CountryProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CountryProfile countryProfile = db.CountryProfiles.Find(id);
            db.CountryProfiles.Remove(countryProfile);
            db.SaveChanges();
            return RedirectToAction("Index");
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AmsaLomi.Models;
using PagedList;

namespace AmsaLomi.Controllers
{
    public class CountryProfilesController : Controller
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: CountryProfiles
        public ActionResult Index(int? page, int? size, string searchString)
        {
            var list = (from item in db.CountryProfiles select item);

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(i => i.Name.Contains(searchString));
            }

            ViewBag.searchString = searchString;
            int pageNumber = (page ?? 1);
            int pageSize = (size ?? 20);

            return View(list.OrderBy(i => i.Name).ToPagedList(pageNumber, pageSize));
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

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
    public class RegionProfilesController : Controller
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: RegionProfiles
        public ActionResult Index(int? page, int? size, string searchString)
        {
            var list = (from item in db.RegionProfiles select item).Include(i=>i.CountryProfile);

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(i => i.Name.Contains(searchString)
                || i.Description.Contains(searchString));
            }

            ViewBag.searchString = searchString;
            int pageNumber = (page ?? 1);
            int pageSize = (size ?? 20);

            return View(list.OrderBy(i => i.Name).ToPagedList(pageNumber, pageSize));
        }

        // GET: RegionProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegionProfile regionProfile = db.RegionProfiles.Find(id);
            if (regionProfile == null)
            {
                return HttpNotFound();
            }
            return View(regionProfile);
        }

        // GET: RegionProfiles/Create
        public ActionResult Create()
        {
            ViewBag.CountryProfileId = new SelectList(db.CountryProfiles, "Id", "Name");
            return View();
        }

        // POST: RegionProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,CountryProfileId")] RegionProfile regionProfile)
        {
            if (ModelState.IsValid)
            {
                db.RegionProfiles.Add(regionProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryProfileId = new SelectList(db.CountryProfiles, "Id", "Name", regionProfile.CountryProfileId);
            return View(regionProfile);
        }

        // GET: RegionProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegionProfile regionProfile = db.RegionProfiles.Find(id);
            if (regionProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryProfileId = new SelectList(db.CountryProfiles, "Id", "Name", regionProfile.CountryProfileId);
            return View(regionProfile);
        }

        // POST: RegionProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CountryProfileId")] RegionProfile regionProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regionProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryProfileId = new SelectList(db.CountryProfiles, "Id", "Name", regionProfile.CountryProfileId);
            return View(regionProfile);
        }

        // GET: RegionProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegionProfile regionProfile = db.RegionProfiles.Find(id);
            if (regionProfile == null)
            {
                return HttpNotFound();
            }
            return View(regionProfile);
        }

        // POST: RegionProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegionProfile regionProfile = db.RegionProfiles.Find(id);
            db.RegionProfiles.Remove(regionProfile);
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

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
    public class ZoneProfilesController : Controller
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: ZoneProfiles
        public ActionResult Index(int? page, int? size, string searchString)
        {
            var list = (from item in db.ZoneProfiles select item).Include(i => i.RegionProfile);

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

        // GET: ZoneProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZoneProfile zoneProfile = db.ZoneProfiles.Find(id);
            if (zoneProfile == null)
            {
                return HttpNotFound();
            }
            return View(zoneProfile);
        }

        // GET: ZoneProfiles/Create
        public ActionResult Create()
        {
            ViewBag.RegionProfileId = new SelectList(db.RegionProfiles, "Id", "Name");
            return View();
        }

        // POST: ZoneProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,RegionProfileId")] ZoneProfile zoneProfile)
        {
            if (ModelState.IsValid)
            {
                db.ZoneProfiles.Add(zoneProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RegionProfileId = new SelectList(db.RegionProfiles, "Id", "Name", zoneProfile.RegionProfileId);
            return View(zoneProfile);
        }

        // GET: ZoneProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZoneProfile zoneProfile = db.ZoneProfiles.Find(id);
            if (zoneProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionProfileId = new SelectList(db.RegionProfiles, "Id", "Name", zoneProfile.RegionProfileId);
            return View(zoneProfile);
        }

        // POST: ZoneProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,RegionProfileId")] ZoneProfile zoneProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zoneProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RegionProfileId = new SelectList(db.RegionProfiles, "Id", "Name", zoneProfile.RegionProfileId);
            return View(zoneProfile);
        }

        // GET: ZoneProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZoneProfile zoneProfile = db.ZoneProfiles.Find(id);
            if (zoneProfile == null)
            {
                return HttpNotFound();
            }
            return View(zoneProfile);
        }

        // POST: ZoneProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZoneProfile zoneProfile = db.ZoneProfiles.Find(id);
            db.ZoneProfiles.Remove(zoneProfile);
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

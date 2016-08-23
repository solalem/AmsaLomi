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
    public class WoredaProfilesController : Controller
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: WoredaProfiles
        public ActionResult Index(int? page, int? size, string searchString)
        {
            var list = (from item in db.WoredaProfiles select item).Include(i => i.ZoneProfile);

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(i => i.Name.Contains(searchString)
                || i.Description.Contains(searchString));
            }

            ViewBag.searchString = searchString;
            int pageNumber = (page ?? 1);
            int pageSize = (size ?? 20);
            ViewBag.pageSize = pageSize;

            return View(list.OrderBy(i => i.Name).ToPagedList(pageNumber, pageSize));
        }

        // GET: WoredaProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WoredaProfile woredaProfile = db.WoredaProfiles.Find(id);
            if (woredaProfile == null)
            {
                return HttpNotFound();
            }
            return View(woredaProfile);
        }

        // GET: WoredaProfiles/Create
        public ActionResult Create()
        {
            ViewBag.ZoneProfileId = new SelectList(db.ZoneProfiles, "Id", "Name");
            return View();
        }

        // POST: WoredaProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ZoneProfileId")] WoredaProfile woredaProfile)
        {
            if (ModelState.IsValid)
            {
                db.WoredaProfiles.Add(woredaProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ZoneProfileId = new SelectList(db.ZoneProfiles, "Id", "Name", woredaProfile.ZoneProfileId);
            return View(woredaProfile);
        }

        // GET: WoredaProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WoredaProfile woredaProfile = db.WoredaProfiles.Find(id);
            if (woredaProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.ZoneProfileId = new SelectList(db.ZoneProfiles, "Id", "Name", woredaProfile.ZoneProfileId);
            return View(woredaProfile);
        }

        // POST: WoredaProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ZoneProfileId")] WoredaProfile woredaProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(woredaProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ZoneProfileId = new SelectList(db.ZoneProfiles, "Id", "Name", woredaProfile.ZoneProfileId);
            return View(woredaProfile);
        }

        // GET: WoredaProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WoredaProfile woredaProfile = db.WoredaProfiles.Find(id);
            if (woredaProfile == null)
            {
                return HttpNotFound();
            }
            return View(woredaProfile);
        }

        // POST: WoredaProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WoredaProfile woredaProfile = db.WoredaProfiles.Find(id);
            db.WoredaProfiles.Remove(woredaProfile);
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

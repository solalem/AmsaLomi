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
    public class MahibersController : Controller
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: Mahibers
        public ActionResult Index(int? page, int? size, string searchString)
        {
            var list = (from item in db.Mahibers select item).Include(i => i.WoredaProfile);

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(i => i.Description.Contains(searchString)
                || i.Name.Contains(searchString)
                || i.WoredaProfile.Name.Contains(searchString));
            }

            ViewBag.searchString = searchString;
            int pageNumber = (page ?? 1);
            int pageSize = (size ?? 20);
            ViewBag.pageSize = pageSize;

            return View(list.OrderBy(i => i.WoredaProfileId).ToPagedList(pageNumber, pageSize));

        }

        // GET: Mahibers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mahiber mahiber = db.Mahibers.Find(id);
            if (mahiber == null)
            {
                return HttpNotFound();
            }
            return View(mahiber);
        }

        // GET: Mahibers/Create
        public ActionResult Create()
        {
            ViewBag.WoredaProfileId = new SelectList(db.WoredaProfiles, "Id", "Name");
            return View();
        }

        // POST: Mahibers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,WoredaProfileId")] Mahiber mahiber)
        {
            if (ModelState.IsValid)
            {
                db.Mahibers.Add(mahiber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WoredaProfileId = new SelectList(db.WoredaProfiles, "Id", "Name", mahiber.WoredaProfileId);
            return View(mahiber);
        }

        // GET: Mahibers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mahiber mahiber = db.Mahibers.Find(id);
            if (mahiber == null)
            {
                return HttpNotFound();
            }
            ViewBag.WoredaProfileId = new SelectList(db.WoredaProfiles, "Id", "Name", mahiber.WoredaProfileId);
            return View(mahiber);
        }

        // POST: Mahibers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,WoredaProfileId")] Mahiber mahiber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mahiber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WoredaProfileId = new SelectList(db.WoredaProfiles, "Id", "Name", mahiber.WoredaProfileId);
            return View(mahiber);
        }

        // GET: Mahibers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mahiber mahiber = db.Mahibers.Find(id);
            if (mahiber == null)
            {
                return HttpNotFound();
            }
            return View(mahiber);
        }

        // POST: Mahibers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mahiber mahiber = db.Mahibers.Find(id);
            db.Mahibers.Remove(mahiber);
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

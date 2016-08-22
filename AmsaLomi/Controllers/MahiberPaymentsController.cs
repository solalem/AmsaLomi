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
    public class MahiberPaymentsController : Controller
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: MahiberPayments
        public ActionResult Index(int? page, int? size, string searchString)
        {
            var list = (from item in db.MahiberPayments select item).Include(i => i.Mahiber).Include(m => m.Payment);

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(i => i.Description.Contains(searchString)
                || i.Mahiber.Name.Contains(searchString)
                || i.Payment.Name.Contains(searchString));
            }

            ViewBag.searchString = searchString;
            int pageNumber = (page ?? 1);
            int pageSize = (size ?? 20);

            return View(list.OrderBy(i => i.MahiberId).ToPagedList(pageNumber, pageSize));
        }

        // GET: MahiberPayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MahiberPayment mahiberPayment = db.MahiberPayments.Find(id);
            if (mahiberPayment == null)
            {
                return HttpNotFound();
            }
            return View(mahiberPayment);
        }

        // GET: MahiberPayments/Create
        public ActionResult Create()
        {
            ViewBag.MahiberId = new SelectList(db.Mahibers, "Id", "Name");
            ViewBag.PaymentId = new SelectList(db.Payments, "Id", "Name");
            return View();
        }

        // POST: MahiberPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,MahiberId,PaymentId,PaymentNumber")] MahiberPayment mahiberPayment)
        {
            if (ModelState.IsValid)
            {
                db.MahiberPayments.Add(mahiberPayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MahiberId = new SelectList(db.Mahibers, "Id", "Name", mahiberPayment.MahiberId);
            ViewBag.PaymentId = new SelectList(db.Payments, "Id", "Name", mahiberPayment.PaymentId);
            return View(mahiberPayment);
        }

        // GET: MahiberPayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MahiberPayment mahiberPayment = db.MahiberPayments.Find(id);
            if (mahiberPayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.MahiberId = new SelectList(db.Mahibers, "Id", "Name", mahiberPayment.MahiberId);
            ViewBag.PaymentId = new SelectList(db.Payments, "Id", "Name", mahiberPayment.PaymentId);
            return View(mahiberPayment);
        }

        // POST: MahiberPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,MahiberId,PaymentId,PaymentNumber")] MahiberPayment mahiberPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mahiberPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MahiberId = new SelectList(db.Mahibers, "Id", "Name", mahiberPayment.MahiberId);
            ViewBag.PaymentId = new SelectList(db.Payments, "Id", "Name", mahiberPayment.PaymentId);
            return View(mahiberPayment);
        }

        // GET: MahiberPayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MahiberPayment mahiberPayment = db.MahiberPayments.Find(id);
            if (mahiberPayment == null)
            {
                return HttpNotFound();
            }
            return View(mahiberPayment);
        }

        // POST: MahiberPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MahiberPayment mahiberPayment = db.MahiberPayments.Find(id);
            db.MahiberPayments.Remove(mahiberPayment);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AmsaLomi.Models;

namespace AmsaLomi.Controllers
{
    public class MahiberPaymentsController : ApiController
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: api/MahiberPayments
        public IEnumerable<Dto.MahiberPayment> GetMahiberPayments()
        {
            // Convert to DTO
            return Dto.MahiberPayment.FromBusinessEntity(db.MahiberPayments.Include(s => s.Mahiber).Include(s => s.Payment).AsEnumerable());
        }

        // GET: api/MahiberPayments/5
        [ResponseType(typeof(Dto.MahiberPayment))]
        public IHttpActionResult GetMahiberPayment(int id)
        {
            MahiberPayment mahiberPayment = db.MahiberPayments.Find(id);
            if (mahiberPayment == null)
            {
                return NotFound();
            }

            return Ok(Dto.MahiberPayment.FromBusinessEntity(mahiberPayment));
        }

        // PUT: api/MahiberPayments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMahiberPayment(int id, Dto.MahiberPayment mahiberPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mahiberPayment.Id)
            {
                return BadRequest();
            }

            // convert it and save
            db.Entry(mahiberPayment.ToBusinessEntity()).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MahiberPaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MahiberPayments
        [ResponseType(typeof(Dto.MahiberPayment))]
        public IHttpActionResult PostMahiberPayment(Dto.MahiberPayment mahiberPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convert and save
            db.MahiberPayments.Add(mahiberPayment.ToBusinessEntity());
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mahiberPayment.Id }, mahiberPayment);
        }

        // DELETE: api/MahiberPayments/5
        [ResponseType(typeof(Dto.MahiberPayment))]
        public IHttpActionResult DeleteMahiberPayment(int id)
        {
            MahiberPayment mahiberPayment = db.MahiberPayments.Find(id);
            if (mahiberPayment == null)
            {
                return NotFound();
            }

            db.MahiberPayments.Remove(mahiberPayment);
            db.SaveChanges();

            // Return DTO
            return Ok(Dto.MahiberPayment.FromBusinessEntity(mahiberPayment));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MahiberPaymentExists(int id)
        {
            return db.MahiberPayments.Count(e => e.Id == id) > 0;
        }
    }
}
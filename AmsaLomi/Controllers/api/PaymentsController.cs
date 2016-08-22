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
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace AmsaLomi.Controllers.Api
{
    public class PaymentsController : ApiController
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: api/Payments
        [EnableQueryAttribute(AllowedQueryOptions = (AllowedQueryOptions.Skip | AllowedQueryOptions.Top), MaxTop = 100)]
        public IEnumerable<Dto.Payment> GetPayments()
        {
            // Convert to DTO
            return Dto.Payment.FromBusinessEntity(db.Payments.AsEnumerable());
        }

        // GET: api/Payments/5
        [ResponseType(typeof(Dto.Payment))]
        public IHttpActionResult GetPayment(int id)
        {
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }

            return Ok(Dto.Payment.FromBusinessEntity(payment));
        }

        // PUT: api/Payments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPayment(int id, Dto.Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payment.Id)
            {
                return BadRequest();
            }

            // convert it and save
            db.Entry(payment.ToBusinessEntity()).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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

        // POST: api/Payments
        [ResponseType(typeof(Dto.Payment))]
        public IHttpActionResult PostPayment(Dto.Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convert and save
            db.Payments.Add(payment.ToBusinessEntity());
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = payment.Id }, payment);
        }

        // DELETE: api/Payments/5
        [ResponseType(typeof(Dto.Payment))]
        public IHttpActionResult DeletePayment(int id)
        {
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }

            db.Payments.Remove(payment);
            db.SaveChanges();

            // Return DTO
            return Ok(Dto.Payment.FromBusinessEntity(payment));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentExists(int id)
        {
            return db.Payments.Count(e => e.Id == id) > 0;
        }
    }
}
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

namespace AmsaLomi.Controllers.Api
{
    public class DonationsController : ApiController
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: api/Donations
        public IEnumerable<Dto.Donation> GetDonations()
        {
            // Convert to DTO
            return Dto.Donation.FromBusinessEntity(db.Donations.Include(s => s.MahiberPayment).AsEnumerable());
        }

        // GET: api/Donations/5
        [ResponseType(typeof(Dto.Donation))]
        public IHttpActionResult GetDonation(int id)
        {
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return NotFound();
            }

            return Ok(Dto.Donation.FromBusinessEntity(donation));
        }

        // PUT: api/Donations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDonation(int id, Dto.Donation donation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != donation.Id)
            {
                return BadRequest();
            }

            // convert it and save
            db.Entry(donation.ToBusinessEntity()).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonationExists(id))
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

        // POST: api/Donations
        [ResponseType(typeof(Dto.Donation))]
        public IHttpActionResult PostDonation(Dto.Donation donation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convert and save
            db.Donations.Add(donation.ToBusinessEntity());
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = donation.Id }, donation);
        }

        // DELETE: api/Donations/5
        [ResponseType(typeof(Dto.Donation))]
        public IHttpActionResult DeleteDonation(int id)
        {
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return NotFound();
            }

            db.Donations.Remove(donation);
            db.SaveChanges();

            // Return DTO
            return Ok(Dto.Donation.FromBusinessEntity(donation));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DonationExists(int id)
        {
            return db.Donations.Count(e => e.Id == id) > 0;
        }
    }
}
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
    public class WoredaProfilesController : ApiController
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: api/WoredaProfiles
        public IEnumerable<Dto.WoredaProfile> GetWoredaProfiles()
        {
            // Convert to DTO
            return Dto.WoredaProfile.FromBusinessEntity(db.WoredaProfiles.Include(s => s.ZoneProfile).AsEnumerable());
        }

        // GET: api/WoredaProfiles/5
        [ResponseType(typeof(Dto.WoredaProfile))]
        public IHttpActionResult GetWoredaProfile(int id)
        {
            WoredaProfile woredaProfile = db.WoredaProfiles.Find(id);
            if (woredaProfile == null)
            {
                return NotFound();
            }

            return Ok(Dto.WoredaProfile.FromBusinessEntity(woredaProfile));
        }

        // PUT: api/WoredaProfiles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWoredaProfile(int id, Dto.WoredaProfile woredaProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != woredaProfile.Id)
            {
                return BadRequest();
            }

            // convert it and save
            db.Entry(woredaProfile.ToBusinessEntity()).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WoredaProfileExists(id))
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

        // POST: api/WoredaProfiles
        [ResponseType(typeof(Dto.WoredaProfile))]
        public IHttpActionResult PostWoredaProfile(Dto.WoredaProfile woredaProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convert and save
            db.WoredaProfiles.Add(woredaProfile.ToBusinessEntity());
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = woredaProfile.Id }, woredaProfile);
        }

        // DELETE: api/WoredaProfiles/5
        [ResponseType(typeof(Dto.WoredaProfile))]
        public IHttpActionResult DeleteWoredaProfile(int id)
        {
            WoredaProfile woredaProfile = db.WoredaProfiles.Find(id);
            if (woredaProfile == null)
            {
                return NotFound();
            }

            db.WoredaProfiles.Remove(woredaProfile);
            db.SaveChanges();

            // Return DTO
            return Ok(Dto.WoredaProfile.FromBusinessEntity(woredaProfile));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WoredaProfileExists(int id)
        {
            return db.WoredaProfiles.Count(e => e.Id == id) > 0;
        }
    }
}
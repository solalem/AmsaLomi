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
    public class RegionProfilesController : ApiController
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: api/RegionProfiles
        public IEnumerable<Dto.RegionProfile> GetRegionProfiles()
        {
            // Convert to DTO
            return Dto.RegionProfile.FromBusinessEntity(db.RegionProfiles.Include(s => s.CountryProfile).AsEnumerable());
        }

        // GET: api/RegionProfiles/5
        [ResponseType(typeof(Dto.RegionProfile))]
        public IHttpActionResult GetRegionProfile(int id)
        {
            RegionProfile regionProfile = db.RegionProfiles.Find(id);
            if (regionProfile == null)
            {
                return NotFound();
            }

            return Ok(Dto.RegionProfile.FromBusinessEntity(regionProfile));
        }

        // PUT: api/RegionProfiles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegionProfile(int id, Dto.RegionProfile regionProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != regionProfile.Id)
            {
                return BadRequest();
            }

            // convert it and save
            db.Entry(regionProfile.ToBusinessEntity()).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionProfileExists(id))
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

        // POST: api/RegionProfiles
        [ResponseType(typeof(Dto.RegionProfile))]
        public IHttpActionResult PostRegionProfile(Dto.RegionProfile regionProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convert and save
            db.RegionProfiles.Add(regionProfile.ToBusinessEntity());
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = regionProfile.Id }, regionProfile);
        }

        // DELETE: api/RegionProfiles/5
        [ResponseType(typeof(Dto.RegionProfile))]
        public IHttpActionResult DeleteRegionProfile(int id)
        {
            RegionProfile regionProfile = db.RegionProfiles.Find(id);
            if (regionProfile == null)
            {
                return NotFound();
            }

            db.RegionProfiles.Remove(regionProfile);
            db.SaveChanges();

            // Return DTO
            return Ok(Dto.RegionProfile.FromBusinessEntity(regionProfile));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegionProfileExists(int id)
        {
            return db.RegionProfiles.Count(e => e.Id == id) > 0;
        }
    }
}
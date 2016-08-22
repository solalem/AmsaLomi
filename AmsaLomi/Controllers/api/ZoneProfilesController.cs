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
    public class ZoneProfilesController : ApiController
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: api/ZoneProfiles
        [EnableQueryAttribute(AllowedQueryOptions = (AllowedQueryOptions.Skip | AllowedQueryOptions.Top), MaxTop = 100)]
        public IEnumerable<Dto.ZoneProfile> GetZoneProfiles()
        {
            // Convert to DTO
            return Dto.ZoneProfile.FromBusinessEntity(db.ZoneProfiles.Include(s => s.RegionProfile).AsEnumerable());
        }

        // GET: api/ZoneProfiles/5
        [ResponseType(typeof(Dto.ZoneProfile))]
        public IHttpActionResult GetZoneProfile(int id)
        {
            ZoneProfile zoneProfile = db.ZoneProfiles.Find(id);
            if (zoneProfile == null)
            {
                return NotFound();
            }

            return Ok(Dto.ZoneProfile.FromBusinessEntity(zoneProfile));
        }

        // PUT: api/ZoneProfiles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutZoneProfile(int id, Dto.ZoneProfile zoneProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zoneProfile.Id)
            {
                return BadRequest();
            }

            // convert it and save
            db.Entry(zoneProfile.ToBusinessEntity()).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneProfileExists(id))
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

        // POST: api/ZoneProfiles
        [ResponseType(typeof(Dto.ZoneProfile))]
        public IHttpActionResult PostZoneProfile(Dto.ZoneProfile zoneProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convert and save
            db.ZoneProfiles.Add(zoneProfile.ToBusinessEntity());
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = zoneProfile.Id }, zoneProfile);
        }

        // DELETE: api/ZoneProfiles/5
        [ResponseType(typeof(Dto.ZoneProfile))]
        public IHttpActionResult DeleteZoneProfile(int id)
        {
            ZoneProfile zoneProfile = db.ZoneProfiles.Find(id);
            if (zoneProfile == null)
            {
                return NotFound();
            }

            db.ZoneProfiles.Remove(zoneProfile);
            db.SaveChanges();

            // Return DTO
            return Ok(Dto.ZoneProfile.FromBusinessEntity(zoneProfile));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ZoneProfileExists(int id)
        {
            return db.ZoneProfiles.Count(e => e.Id == id) > 0;
        }
    }
}
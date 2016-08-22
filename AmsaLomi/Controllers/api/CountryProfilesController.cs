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
using System.Web.Http.OData.Query;
using System.Web.Http.OData;

namespace AmsaLomi.Controllers.Api
{
    public class CountryProfilesController : ApiController
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: api/CountryProfiles
        [EnableQueryAttribute(AllowedQueryOptions = (AllowedQueryOptions.Skip | AllowedQueryOptions.Top), MaxTop = 100)]
        public IEnumerable<Dto.CountryProfile> GetCountryProfiles()
        {
            // Convert to DTO
            return Dto.CountryProfile.FromBusinessEntity(db.CountryProfiles.AsEnumerable());
        }

        // GET: api/CountryProfiles/5
        [ResponseType(typeof(Dto.CountryProfile))]
        public IHttpActionResult GetCountryProfile(int id)
        {
            CountryProfile countryProfile = db.CountryProfiles.Find(id);
            if (countryProfile == null)
            {
                return NotFound();
            }

            return Ok(Dto.CountryProfile.FromBusinessEntity(countryProfile));
        }

        // PUT: api/CountryProfiles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCountryProfile(int id, Dto.CountryProfile countryProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != countryProfile.Id)
            {
                return BadRequest();
            }
            // convert it and save
            db.Entry(countryProfile.ToBusinessEntity()).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryProfileExists(id))
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

        // POST: api/CountryProfiles
        [ResponseType(typeof(Dto.CountryProfile))]
        public IHttpActionResult PostCountryProfile(Dto.CountryProfile countryProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Convert and save
            db.CountryProfiles.Add(countryProfile.ToBusinessEntity());
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = countryProfile.Id }, countryProfile);
        }

        // DELETE: api/CountryProfiles/5
        [ResponseType(typeof(Dto.CountryProfile))]
        public IHttpActionResult DeleteCountryProfile(int id)
        {
            CountryProfile countryProfile = db.CountryProfiles.Find(id);
            if (countryProfile == null)
            {
                return NotFound();
            }

            db.CountryProfiles.Remove(countryProfile);
            db.SaveChanges();

            // Return DTO
            return Ok(Dto.CountryProfile.FromBusinessEntity(countryProfile));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryProfileExists(int id)
        {
            return db.CountryProfiles.Count(e => e.Id == id) > 0;
        }
    }
}
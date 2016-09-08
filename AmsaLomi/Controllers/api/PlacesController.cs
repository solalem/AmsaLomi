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
    public class PlacesController : ApiController
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: api/Places
        [EnableQueryAttribute(AllowedQueryOptions = (AllowedQueryOptions.Skip | AllowedQueryOptions.Top), MaxTop = 100)]
        public IEnumerable<Dto.Place> GetPlaces()
        {
            // Convert to DTO
            return Dto.Place.FromBusinessEntity(db.Places.Include(s => s.ParentPlace).AsEnumerable());
        }

        // GET: api/Places/5
        [ResponseType(typeof(Dto.Place))]
        public IHttpActionResult GetPlace(int id)
        {
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return NotFound();
            }

            return Ok(Dto.Place.FromBusinessEntity(place));
        }

        // PUT: api/Places/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlace(int id, Dto.Place place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != place.Id)
            {
                return BadRequest();
            }

            // convert it and save
            db.Entry(place.ToBusinessEntity()).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceExists(id))
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

        // POST: api/Places
        [ResponseType(typeof(Dto.Place))]
        public IHttpActionResult PostPlace(Dto.Place place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convert and save
            db.Places.Add(place.ToBusinessEntity());
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = place.Id }, place);
        }

        // DELETE: api/Places/5
        [ResponseType(typeof(Dto.Place))]
        public IHttpActionResult DeletePlace(int id)
        {
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return NotFound();
            }

            db.Places.Remove(place);
            db.SaveChanges();

            // Return DTO
            return Ok(Dto.Place.FromBusinessEntity(place));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlaceExists(int id)
        {
            return db.Places.Count(e => e.Id == id) > 0;
        }
    }
}
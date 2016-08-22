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
    public class MahibersController : ApiController
    {
        private AmsaLomiContext db = new AmsaLomiContext();

        // GET: api/Mahibers
        [EnableQueryAttribute(AllowedQueryOptions = (AllowedQueryOptions.Skip | AllowedQueryOptions.Top), MaxTop = 100)]
        public IEnumerable<Dto.Mahiber> GetMahibers()
        {
            // Convert to DTO
            return Dto.Mahiber.FromBusinessEntity(db.Mahibers.Include(s => s.WoredaProfile).AsEnumerable());
        }

        // GET: api/Mahibers/5
        [ResponseType(typeof(Dto.Mahiber))]
        public IHttpActionResult GetMahiber(int id)
        {
            Mahiber mahiber = db.Mahibers.Find(id);
            if (mahiber == null)
            {
                return NotFound();
            }

            return Ok(Dto.Mahiber.FromBusinessEntity(mahiber));
        }

        // PUT: api/Mahibers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMahiber(int id, Dto.Mahiber mahiber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mahiber.Id)
            {
                return BadRequest();
            }

            // convert it and save
            db.Entry(mahiber.ToBusinessEntity()).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MahiberExists(id))
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

        // POST: api/Mahibers
        [ResponseType(typeof(Dto.Mahiber))]
        public IHttpActionResult PostMahiber(Dto.Mahiber mahiber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convert and save
            db.Mahibers.Add(mahiber.ToBusinessEntity());
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mahiber.Id }, mahiber);
        }

        // DELETE: api/Mahibers/5
        [ResponseType(typeof(Dto.Mahiber))]
        public IHttpActionResult DeleteMahiber(int id)
        {
            Mahiber mahiber = db.Mahibers.Find(id);
            if (mahiber == null)
            {
                return NotFound();
            }

            db.Mahibers.Remove(mahiber);
            db.SaveChanges();

            // Return DTO
            return Ok(Dto.Mahiber.FromBusinessEntity(mahiber));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MahiberExists(int id)
        {
            return db.Mahibers.Count(e => e.Id == id) > 0;
        }
    }
}
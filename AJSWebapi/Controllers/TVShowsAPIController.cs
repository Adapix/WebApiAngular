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
using AJSWebapi;

namespace AJSWebapi.Controllers
{
    public class TVShowsAPIController : ApiController
    {
        private AJSWebapiEntities db = new AJSWebapiEntities();

        // GET: api/TVShowsAPI
        public IQueryable<TVShow> GetTVShow()
        {
            return db.TVShow;
        }

        // GET: api/TVShowsAPI/5
        [ResponseType(typeof(TVShow))]
        public IHttpActionResult GetTVShow(int id)
        {
            TVShow tVShow = db.TVShow.Find(id);
            if (tVShow == null)
            {
                return NotFound();
            }

            return Ok(tVShow);
        }

        // PUT: api/TVShowsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTVShow(int id, TVShow tVShow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tVShow.Id)
            {
                return BadRequest();
            }

            db.Entry(tVShow).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TVShowExists(id))
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

        // POST: api/TVShowsAPI
        [ResponseType(typeof(TVShow))]
        public IHttpActionResult PostTVShow(TVShow tVShow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TVShow.Add(tVShow);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tVShow.Id }, tVShow);
        }

        // DELETE: api/TVShowsAPI/5
        [ResponseType(typeof(TVShow))]
        public IHttpActionResult DeleteTVShow(int id)
        {
            TVShow tVShow = db.TVShow.Find(id);
            if (tVShow == null)
            {
                return NotFound();
            }

            db.TVShow.Remove(tVShow);
            db.SaveChanges();

            return Ok(tVShow);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TVShowExists(int id)
        {
            return db.TVShow.Count(e => e.Id == id) > 0;
        }
    }
}
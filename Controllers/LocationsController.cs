using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using workWithME.Models;

namespace workWithME.Controllers
{
    public class LocationsController : ApiController
    {
        private projectsContext db = new projectsContext();

        // GET api/Locations
        public IEnumerable<locations> Getlocations()
        {
            return db.locations.AsEnumerable();
        }

        // GET api/Locations/5
        public locations Getlocations(int id)
        {
            locations locations = db.locations.Find(id);
            if (locations == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return locations;
        }

        // PUT api/Locations/5
        public HttpResponseMessage Putlocations(int id, locations locations)
        {
            if (ModelState.IsValid && id == locations.locationID)
            {
                db.Entry(locations).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Locations
        public HttpResponseMessage Postlocations(locations locations)
        {
            if (ModelState.IsValid)
            {
                db.locations.Add(locations);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, locations);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = locations.locationID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Locations/5
        public HttpResponseMessage Deletelocations(int id)
        {
            locations locations = db.locations.Find(id);
            if (locations == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.locations.Remove(locations);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, locations);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
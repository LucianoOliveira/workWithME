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
    public class ClientsController : ApiController
    {
        private projectsContext db = new projectsContext();

        // GET api/Clients
        public IEnumerable<clients> Getclients()
        {
            return db.clients.AsEnumerable();
        }

        // GET api/Clients/5
        public clients Getclients(int id)
        {
            clients clients = db.clients.Find(id);
            if (clients == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return clients;
        }

        // PUT api/Clients/5
        public HttpResponseMessage Putclients(int id, clients clients)
        {
            if (ModelState.IsValid && id == clients.clientID)
            {
                db.Entry(clients).State = EntityState.Modified;

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

        // POST api/Clients
        public HttpResponseMessage Postclients(clients clients)
        {
            if (ModelState.IsValid)
            {
                db.clients.Add(clients);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, clients);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = clients.clientID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Clients/5
        public HttpResponseMessage Deleteclients(int id)
        {
            clients clients = db.clients.Find(id);
            if (clients == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.clients.Remove(clients);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, clients);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
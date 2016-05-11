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
    public class CandidatesController : ApiController
    {
        private projectsContext db = new projectsContext();

        // GET api/Candidates
        public IEnumerable<candidate> Getcandidates()
        {
            return db.candidates.AsEnumerable();
        }

        // GET api/Candidates/5
        public candidate Getcandidate(int id)
        {
            candidate candidate = db.candidates.Find(id);
            if (candidate == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return candidate;
        }

        // PUT api/Candidates/5
        public HttpResponseMessage Putcandidate(int id, candidate candidate)
        {
            if (ModelState.IsValid && id == candidate.candidateId)
            {
                db.Entry(candidate).State = EntityState.Modified;

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

        // POST api/Candidates
        public HttpResponseMessage Postcandidate(candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.candidates.Add(candidate);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, candidate);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = candidate.candidateId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Candidates/5
        public HttpResponseMessage Deletecandidate(int id)
        {
            candidate candidate = db.candidates.Find(id);
            if (candidate == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.candidates.Remove(candidate);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, candidate);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
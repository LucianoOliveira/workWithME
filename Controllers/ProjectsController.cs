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
    public class ProjectsController : ApiController
    {
        private projectsContext db = new projectsContext();

        // GET api/Projects
        public IEnumerable<projects> Getprojects(string q = null, string sort = null, bool desc = false,
                                                        int? limit = null, int offset = 0)
        {
            var list = ((IObjectContextAdapter)db).ObjectContext.CreateObjectSet<projects>();

            IQueryable<projects> items = string.IsNullOrEmpty(sort) 
                ? list.OrderBy(o => o.priorityNum)
                : list.OrderBy(String.Format("it.{0} {1}", sort, desc ? "DESC" : "ASC"));

            if (!string.IsNullOrEmpty(q) && q != "undefined") 
                items = items.Where(t => t.project.Contains(q));

            if (offset > 0) items = items.Skip(offset);
            if (limit.HasValue) items = items.Take(limit.Value);
            return items;
        }

        // GET api/Projects/5
        public projects Getprojects(int id)
        {
            projects projects = db.projects.Find(id);
            if (projects == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return projects;
        }

        //Get api/Project exist?
        public bool projectExist(int id)
        {
            projects projects = db.projects.Find(id);
            if (projects == null)
            {
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                return false;
            }
            else
            {
                return true;
            }

        }

        // PUT api/Projects/5
        public HttpResponseMessage Putprojects(int id, projects projects)
        {
            if (ModelState.IsValid && id == projects.projectId)
            {
                db.Entry(projects).State = EntityState.Modified;

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

        // POST api/Projects
        public HttpResponseMessage Postprojects(projects projects)
        {
            if (ModelState.IsValid)
            {
                db.projects.Add(projects);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, projects);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = projects.projectId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Projects/5
        public HttpResponseMessage Deleteprojects(int id)
        {
            projects projects = db.projects.Find(id);
            if (projects == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.projects.Remove(projects);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, projects);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
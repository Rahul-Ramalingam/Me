using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MeApi.Data;
using MeApi.Models;

namespace MeApi.Controllers
{
    public class SubTasksController : ApiController
    {
        private MeApiContext db = new MeApiContext();

        // GET: api/SubTasks
        public IQueryable<SubTasks> GetSubTasks()
        {
            return db.SubTasks;
        }

        // GET: api/SubTasks/5
        [ResponseType(typeof(SubTasks))]
        public async Task<IHttpActionResult> GetSubTasks(int id)
        {
            SubTasks subTasks = await db.SubTasks.FindAsync(id);
            if (subTasks == null)
            {
                return NotFound();
            }

            return Ok(subTasks);
        }

        // PUT: api/SubTasks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSubTasks(int id, SubTasks subTasks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subTasks.SubTaskId)
            {
                return BadRequest();
            }

            db.Entry(subTasks).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubTasksExists(id))
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

        // POST: api/SubTasks
        [ResponseType(typeof(SubTasks))]
        public async Task<IHttpActionResult> PostSubTasks(SubTasks subTasks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubTasks.Add(subTasks);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = subTasks.SubTaskId }, subTasks);
        }

        // DELETE: api/SubTasks/5
        [ResponseType(typeof(SubTasks))]
        public async Task<IHttpActionResult> DeleteSubTasks(int id)
        {
            SubTasks subTasks = await db.SubTasks.FindAsync(id);
            if (subTasks == null)
            {
                return NotFound();
            }

            db.SubTasks.Remove(subTasks);
            await db.SaveChangesAsync();

            return Ok(subTasks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubTasksExists(int id)
        {
            return db.SubTasks.Count(e => e.SubTaskId == id) > 0;
        }
    }
}
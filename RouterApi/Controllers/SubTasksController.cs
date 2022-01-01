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
using RouterApi.Data;
using RouterApi.Models;

namespace RouterApi.Controllers
{
    public class SubTasksController : ApiController
    {
        private RouterApiContext db = new RouterApiContext();


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
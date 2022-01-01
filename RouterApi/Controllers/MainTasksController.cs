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
using RouterApi.DataService;

namespace RouterApi.Controllers
{
    public class MainTasksController : ApiController
    {
        private DataServices _dataService = new DataServices();

        [Route("api/tasks/MainTasks/{userId:int}")]
        [HttpGet]
        public async Task<IEnumerable<MainTasks>> GetMainTasks(int userId)
        {
            return await _dataService.GetMainTasks(userId);
        }

        [Route("api/tasks/{userId:int}/addMainTask", Name = "addMainTask")]
        [HttpPost]
        public async Task<IHttpActionResult> AddMainTask(int userId, MainTasks task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _dataService.AddMainTask(userId, task);
            return CreatedAtRoute("addMainTask", new { V = response.Id = task.MainTaskId }, response);
        }

        protected override void Dispose(bool disposing)
        {
            _dataService.Dispose(disposing);
        }
    }
}
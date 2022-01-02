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
using MeApi.DataService;
using MeApi.Models;

namespace MeApi.Controllers
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

        [Route("api/tasks/{mainTaskId:int}/editTask", Name = "updateMainTask")]
        [HttpPut]
        public async Task<IHttpActionResult> EditMainTask(int mainTaskId, MainTasks task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _dataService.EditMainTask(mainTaskId, task);
            return CreatedAtRoute("updateMainTask", new { V = response.Id = task.MainTaskId }, response);
        }

        [Route("api/tasks/deleteMainTask/{mainTaskId:int}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteMainTask(int mainTaskId)
        {
            var response = await _dataService.DeleteMainTask(mainTaskId);
            if(response.Status == 200)
            {
                return Ok(response);
            }
            else if(response.Status == 404)
            {
                return NotFound();
            }
            return BadRequest(response.Message);
        }

        protected override void Dispose(bool disposing)
        {
            _dataService.Dispose(disposing);
        }
    }
}
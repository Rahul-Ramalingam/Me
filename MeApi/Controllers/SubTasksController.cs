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
    public class SubTasksController : ApiController
    {
        private DataServices _dataService = new DataServices();

        [Route("api/tasks/MainTasks/{mainTaskId:int}/SubTasks")]
        [HttpGet]
        public async Task<IEnumerable<SubTasks>> GetSubTasks(int mainTaskId)
        {
            return await _dataService.GetSubTasks(mainTaskId);
        }

        [Route("api/tasks/{MaintaskId:int}/addSubTask", Name = "AddSubTask")]
        [HttpPost]
        public async Task<IHttpActionResult> AddSubTask(int MaintaskId, SubTasks task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _dataService.AddSubTask(MaintaskId, task);
            return CreatedAtRoute("AddSubTask", new { V = response.Id = task.SubTaskId }, response);
        }

        [Route("api/tasks/{mainTaskId:int}/editTask", Name = "editSubTask")]
        [HttpPut]
        public async Task<IHttpActionResult> EditSubTask(int subTaskId, SubTasks task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _dataService.EditSubTask(subTaskId, task);
            return CreatedAtRoute("editSubTask", new { V = response.Id = task.SubTaskId }, response);
        }

        [Route("api/tasks/deleteSubTask/{subTaskId:int}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteSubTask(int subTaskId)
        {
            var response = await _dataService.DeleteSubTask(subTaskId);
            if (response.Status == 200)
            {
                return Ok(response);
            }
            else if (response.Status == 404)
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
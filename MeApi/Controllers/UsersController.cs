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
    public class UsersController : ApiController
    {
        private MeApiContext db = new MeApiContext();
        private DataServices _dataService = new DataServices();

        [Route("api/allUsers")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllUsers()
        {
            try
            {
                var users = await _dataService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/user/{UserName}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserUsingUserName(string userName)
        {
            try
            {
                var user = await _dataService.GetUserUsingUserName(userName);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/addUser", Name = "addUser")]
        [HttpPost]
        public async Task<IHttpActionResult> PostAddUser(Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _dataService.AddUser(users);
            return CreatedAtRoute("addUser", new { V = response.Id = users.UserId }, response);
        }

        [Route("api/{userName}/editUser", Name = "editUser")]
        [HttpPut]
        public async Task<IHttpActionResult> EditUser(string userName, Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _dataService.EditUser(userName, users);
            return CreatedAtRoute("EditUser", new { V = response.Id = users.UserId }, response);
        }

        [ResponseType(typeof(Users))]
        [Route("api/deleteUser/{userName}")]
        public async Task<IHttpActionResult> DeleteUser(string userName)
        {
            var response = await _dataService.DeleteUser(userName);
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsersExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}
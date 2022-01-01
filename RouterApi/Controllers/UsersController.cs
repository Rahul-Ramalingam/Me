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
    public class UsersController : ApiController
    {
        private RouterApiContext db = new RouterApiContext();

        // GET: api/Users
        [Route("api/allUsers")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllUsers()
        {
            try
            {
                IList<Users> users = await db.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/user/{UserName}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserWithUserNameAsync(string userName)
        {
            try
            {
                Users user = await db.Users.FirstAsync(u => u.UserName == userName);
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

        // POST: api/Users
        [Route("api/addUser", Name = "addUser")]
        [HttpPost]
        public async Task<IHttpActionResult> PostAddUser(Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Users.Add(users);
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtRoute("addUser", new { name = users.UserName }, users);
        }

        [ResponseType(typeof(void))]
        [Route("api/{userName}/editUser")]
        [HttpPut]
        public async Task<IHttpActionResult> EditUser(string userName, Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userName != users.UserName)
            {
                return BadRequest("Invalid User Name");
            }

            db.Entry(users).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!UsersExists(users.UserId))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(e.Message);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Users))]
        [Route("api/deleteUser/{userName}")]
        public async Task<IHttpActionResult> DeleteUsers(string userName)
        {
            Users users = await db.Users.FirstAsync(u => u.UserName == userName);
            if (users == null)
            {
                return NotFound();
            }

            db.Users.Remove(users);
            await db.SaveChangesAsync();

            return Ok(users);
        }



        [Route("api/Users/MainTasks/{mainTaskId:int}/SubTasks")]
        [HttpGet]
        public IEnumerable<SubTasks> GetSubTasks(int mainTaskId)
        {
            return db.SubTasks.Include(st => st.MainTasks).Where(st => st.MainTaskId == mainTaskId).ToList();
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
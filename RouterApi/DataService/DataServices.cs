using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using RouterApi.Data;
using RouterApi.Models;

namespace RouterApi.DataService
{
    public class DataServices : ApiController
    {
        private RouterApiContext db = new RouterApiContext();
        public async Task<IEnumerable<MainTasks>> GetMainTasks(int userId)
        {
            return await db.MainTasks.Where(mt => mt.UserId == userId).Include(st => st.Users).ToListAsync();
        }
        public async Task<ResponseBody> AddMainTask(int userId, MainTasks task)
        {
            try
            {
                if(userId == task.UserId && UserExists(userId))
                {
                    db.MainTasks.Add(task);
                    await db.SaveChangesAsync();
                    db.Entry(task).Reference(mt => mt.Users).Load();
                    return new ResponseBody()
                    {
                        Id = task.MainTaskId,
                        Status = 200
                    };
                }
                else
                {
                    return new ResponseBody()
                    {
                        Id = task.MainTaskId,
                        Status = 400,
                        Message = "User not Found",
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBody()
                {
                    Id = task.MainTaskId,
                    Status = 400,
                    Message = ex.Message
                };
            }
        }
        private bool MainTasksExists(int id)
        {
            return db.MainTasks.Count(e => e.MainTaskId == id) > 0;
        }
        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
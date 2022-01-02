using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MeApi.Data;
using MeApi.Models;
using MeApi.DTO;

namespace MeApi.DataService
{
    public class DataServices : ApiController
    {
        private MeApiContext db = new MeApiContext();
        public async Task<IEnumerable<MainTasks>> GetMainTasks(int userId)
        {
            return await db.MainTasks.Where(mt => mt.UserId == userId).Include(st => st.Users).ToListAsync();
        }
        public async Task<ResponseBody> AddMainTask(int userId, MainTasks task)
        {
            try
            {
                if (userId == task.UserId && UserExists(userId))
                {
                    db.MainTasks.Add(task);
                    await db.SaveChangesAsync();
                    db.Entry(task).Reference(mt => mt.Users).Load();
                    return new ResponseBody()
                    {
                        Id = task.MainTaskId,
                        Status = 200,
                        Message ="Successful" 
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

        public async Task<ResponseBody> EditMainTask(int mainTaskId, MainTasks task)
        {
            try
            {
                if(mainTaskId != task.MainTaskId)
                {
                    return new ResponseBody()
                    {
                        Id = task.MainTaskId,
                        Status = 403,
                        Message = "Invalid TaskId"
                    };
                }
                db.Entry(task).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return new ResponseBody()
                {
                    Id = task.MainTaskId,
                    Status = 200,
                    Message = "Succeessful"
                };
            }
            catch(Exception ex)
            {
                if (!MainTasksExists(task.UserId))
                {
                    return new ResponseBody()
                    {
                        Id = task.MainTaskId,
                        Status = 404,
                        Message = "Main Task Not Found"
                    };
                }
                return new ResponseBody()
                {
                    Id = task.MainTaskId,
                    Status = 403,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseBody> DeleteMainTask(int mainTaskId)
        {
            try
            {
                MainTasks mainTask = await db.MainTasks.FirstAsync(mt => mt.MainTaskId == mainTaskId);
                if (mainTask == null)
                {
                    return new ResponseBody() 
                    {
                        Status = 404
                    };
                }
                db.MainTasks.Remove(mainTask);
                await db.SaveChangesAsync();
                return new ResponseBody()
                {
                    Id = mainTaskId,
                    Status = 200,
                    Message = "Successful"
                };
            }
            catch(Exception ex)
            {
                return new ResponseBody()
                {
                    Id = mainTaskId,
                    Status = 403,
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
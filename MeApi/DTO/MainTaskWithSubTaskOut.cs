using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeApi.Models;

namespace MeApi.DTO
{
    public class MainTaskWithSubTaskOut
    {
        public int MainTaskId { get; set; }
        public string MainTaskName { get; set; }
        public string MainTaskDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IList<SubTasksDto> SubTasks { get; set; }
    }

    public class SubTasksDto
    {
        public int SubTaskId { get; set; }
        public string SubTaskName { get; set; }
        public string SubTaskDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
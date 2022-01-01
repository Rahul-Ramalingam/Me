using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouterApi.Models
{
    public class SubTasks
    {
        public int Id { get; set; }
        [Required]
        public string SubTaskName { get; set; }
        public string SubTaskDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MainTaskId { get; set; } //Foreign Key
        public MainTasks MainTasks { get; set; } //Navigation Property
    }
}
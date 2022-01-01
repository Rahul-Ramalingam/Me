using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoApi.Models
{
    public class MainTasks
    {
        public int Id { get; set; }
        [Required]
        public string MainTaskName { get; set; }
        public string MainTaskDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int UserId { get; set; } //Foreign Key
        public Users Users { get; set; } //Navigation Property
    }
}
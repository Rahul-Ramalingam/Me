using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RouterApi.Models
{
    public class MainTasks
    {
        [Key]
        public int MainTaskId { get; set; }
        [Required]
        public string MainTaskName { get; set; }
        public string MainTaskDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        // Foreign key   
        [Display(Name = "UserId")]
        public virtual int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}
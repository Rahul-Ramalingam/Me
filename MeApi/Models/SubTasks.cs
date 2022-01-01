using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeApi.Models
{
    public class SubTasks
    {
        [Key]
        public int SubTaskId { get; set; }
        [Required]
        public string SubTaskName { get; set; }
        [MaxLength(2000)]
        public string SubTaskDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        //Foreign Key
        [Display(Name = "MainTaskId")]
        public virtual int MainTaskId { get; set; }
        [ForeignKey("MainTaskId")]
        public virtual MainTasks MainTasks { get; set; }
    }
}
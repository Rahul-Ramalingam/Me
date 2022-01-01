using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoApi.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string UltimateGoal { get; set; }
    }
}
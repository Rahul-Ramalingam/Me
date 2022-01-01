using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RouterApi.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [Index(IsUnique = true)]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string UltimateGoal { get; set; }
    }
}
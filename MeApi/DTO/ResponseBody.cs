using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeApi.DTO
{
    public class ResponseBody
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }
}
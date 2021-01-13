using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emperor_API.Responses
{
    public class StatusResponse
    {
        [Required(AllowEmptyStrings = false)]
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emperor_API.Responses
{
    public class LoginResponse : StatusResponse
    {
        [Required(AllowEmptyStrings = false)]
        public string SessionKey { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emperor_API.Requests
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
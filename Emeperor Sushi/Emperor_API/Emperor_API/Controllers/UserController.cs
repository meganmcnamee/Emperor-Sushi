using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Emperor_API.Requests;
using Emperor_API.Responses;
using Emperor_Backend;

namespace Emperor_API.Controllers
{
    public class UserController : ApiController
    {
        // PUT api/values/5
        [Route("api/User/Login")]
        
        public LoginResponse Post([FromBody] LoginRequest value)
        {
            Database db = new Database();

            var loginResult = db.Login(value.Username, value.Password);
            if(loginResult != null)
            {
                return new LoginResponse() { Status = "Success", Message = "", SessionKey = loginResult };
            }

            return new LoginResponse() { Status = "Failed", Message = "Username or password is incorrect.", SessionKey = "" };
        }

        // GET api/values/5
        [Route("api/User/Logout")]
        public StatusResponse Get()
        {
            if(Request.Headers.Contains("SessionKey"))
            {
                var db = new Database();
                var header = Request.Headers.GetValues("SessionKey").First();
                var user = db.databaseModel.Users.SingleOrDefault(x => x.SessionKey == header);
                if(user != null && user.SessionKey == header)
                {
                    user.SessionKey = "";
                    user.LastSeen = DateTime.Now;
                    db.databaseModel.SaveChanges();
                    return new StatusResponse() { Status = "Success", Message = "" };
                }
                //TODO: Add check for session key, if exists for user, remove in database.
                return new StatusResponse() { Status = "Failed", Message = "SessionKey is not valid." };
            }
            return new StatusResponse() { Status = "Failed", Message = "SessionKey was not passed to server." };
        }

        // PUT api/values/5
        [Route("api/User/Register")]

        public StatusResponse Post([FromBody] RegisterRequest value)
        {
            Database db = new Database();

            if(value.Password != value.ConfirmPassword)
            {
                return new StatusResponse() { Status = "Failed", Message = "Password Mismatch" };
            }

            var result = db.GenerateUserAccount(value.Username, value.Password);
            if(result)
            {
                return new StatusResponse() { Status = "Success", Message = "" };
            }
            
            return new StatusResponse() { Status = "Failed", Message = "Error code 25" };
        }

    }
}

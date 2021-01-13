using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Emperor_Backend;
using Newtonsoft.Json;

namespace Emperor_API.Controllers
{
    public class MenuController : ApiController
    {
        // GET api/Menu
        public MenuItem[] Get()
        {
            var db = new Database();
            var result = db.databaseModel.MenuItems.ToArray();
            return result;
        }

        // GET api/Menu/5
        public MenuItem Get(int id)
        {
            return new Database().databaseModel.MenuItems.SingleOrDefault(x => x.ID == id);
        }

        [Route("api/Menu/Search")]
        public MenuItem[] Get(string item)
        {
            var db = new Database();
            var result = db.databaseModel.MenuItems.Where(X => X.ItemName.Contains(item)).ToArray();
            return result;
        }

        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}

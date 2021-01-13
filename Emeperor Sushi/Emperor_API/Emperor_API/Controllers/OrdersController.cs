using Emperor_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Emperor_API.Responses;
using Emperor_API.Requests;

namespace Emperor_API.Controllers
{
    public class OrdersController : ApiController
    {
        // GET api/values
        public OrderResponse Get()
        {
            if (Request.Headers.Contains("SessionKey"))
            {
                var db = new Database();
                var header = Request.Headers.GetValues("SessionKey").First();
                var user = db.databaseModel.Users.SingleOrDefault(x => x.SessionKey == header);
                if (user != null && user.SessionKey == header)
                {
                    var orders = db.databaseModel.Orders.Where(x => x.User == user.ID).ToArray();
                    user.LastSeen = DateTime.Now;
                    db.databaseModel.SaveChanges();
                    return new OrderResponse() { Status = "Success", Message = "", Orders = orders };

                }
                //return new OrderResponse() { Status = "Failed", Message = "Internal Error", Orders = null };
            }
            return new OrderResponse() { Status = "Failed", Message = "Not authenticated", Orders = null };
        }

        // GET api/values/5
        public OrderResponse Get(int id)
        {
            if (Request.Headers.Contains("SessionKey"))
            {
                var db = new Database();
                var header = Request.Headers.GetValues("SessionKey").First();
                var user = db.databaseModel.Users.SingleOrDefault(x => x.SessionKey == header);
                if (user != null && user.SessionKey == header)
                {
                    var orders = db.databaseModel.Orders.Where(x => x.User == user.ID && x.ID == id).ToArray();
                    user.LastSeen = DateTime.Now;
                    db.databaseModel.SaveChanges();
                    return new OrderResponse() { Status = "Success", Message = "", Orders = orders };
                }
            }
            return new OrderResponse() { Status = "Failed", Message = "Not authenticated", Orders = null };
        }

        // POST api/values
        public StatusResponse Post(int id, [FromBody] PostOrderRequest request)
        {
            if (Request.Headers.Contains("SessionKey"))
            {
                var db = new Database();
                var header = Request.Headers.GetValues("SessionKey").First();
                var user = db.databaseModel.Users.SingleOrDefault(x => x.SessionKey == header);
                if (user != null && user.SessionKey == header)
                {
                    var cart = db.databaseModel.Carts.SingleOrDefault(x => x.ID == id && user.ID == x.User && x.Ordered == false);
                    cart.Ordered = true;
                    Order order = new Order()
                    {
                        Cart = cart.ID,
                        Cart1 = cart,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        Address = request.Address,
                        State = request.State,
                        City = request.City,
                        Zip = request.Zip,
                        CardName = request.CardName,
                        CardNumber = request.CardNumber,
                        ExpirationDate = request.ExpirationDate,
                        CVV = request.CVV,
                        User = user.ID,
                        User1 = user,
                        OrderedTime = DateTime.Now
                    };

                    db.databaseModel.Orders.Add(order);

                    user.LastSeen = DateTime.Now;
                    db.databaseModel.SaveChanges();
                    return new StatusResponse() { Status = "Success", Message = ""};
                }
            }
            return new StatusResponse() { Status = "Failed", Message = "Not authenticated" };
        }
    }
}

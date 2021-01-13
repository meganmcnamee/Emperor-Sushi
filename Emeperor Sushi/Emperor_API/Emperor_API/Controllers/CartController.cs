using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Emperor_API.Responses;
using Emperor_Backend;

namespace Emperor_API.Controllers
{
    public class CartController : ApiController
    {
        // GET api/values
        public CartResponse Get()
        {
            if (Request.Headers.Contains("SessionKey"))
            {
                var db = new Database();
                var header = Request.Headers.GetValues("SessionKey").First();
                var user = db.databaseModel.Users.SingleOrDefault(x => x.SessionKey == header);
                if (user != null && user.SessionKey == header)
                {
                    user.LastSeen = DateTime.Now;
                    var cart = user.Carts.FirstOrDefault(x => x.Ordered == false);
                    db.databaseModel.SaveChanges();
                    ICollection<CartItem> items;
                    if (cart != null && cart.CartItems != null)
                    {
                        items = cart.CartItems;
                        return new CartResponse() { Status = "Success", Message = "", Cart = items.ToArray() };
                    }
                    else
                    {
                        return new CartResponse() { Status = "Success", Message = "", Cart = new CartItem[] { } };
                    }
                }
            }
            return new CartResponse() { Status = "Failed", Message = "Not authenticated", Cart = null };
        }

        // GET api/values/5
        //public CartResponse Get(int id)
        //{
        //    if (Request.Headers.Contains("SessionKey"))
        //    {
        //        var db = new Database();
        //        var header = Request.Headers.GetValues("SessionKey").First();
        //        var user = db.databaseModel.Users.SingleOrDefault(x => x.SessionKey == header);
        //        if (user != null && user.SessionKey == header)
        //        {
        //            user.LastSeen = DateTime.Now;
        //            db.databaseModel.SaveChanges();
        //            return new CartResponse() { Status = "Success", Message = "", Cart = db.databaseModel.Carts.FirstOrDefault(x => x.ID == id) };
        //        }
        //        return new CartResponse() { Status = "Failed", Message = "You fucked up", Cart = null };
        //    }
        //    return new CartResponse() { Status = "Failed", Message = "Not authenticated", Cart = null };
        //}

        //// POST api/values
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/values/5
        public StatusResponse Put(int id)
        {
            if (Request.Headers.Contains("SessionKey"))
            {
                var db = new Database();
                var menuItem = db.databaseModel.MenuItems.FirstOrDefault(x => x.ID == id);
                var header = Request.Headers.GetValues("SessionKey").First();
                var user = db.databaseModel.Users.SingleOrDefault(x => x.SessionKey == header);
                if (user != null && user.SessionKey == header)
                {
                    user.LastSeen = DateTime.Now;
                    bool hasCart = user.Carts.Any(x => x.Ordered == false);
                    Cart cart = null;
                    if (hasCart)
                        cart = user.Carts.FirstOrDefault(x => x.Ordered == false);
                    else
                    {
                        cart = new Cart() { Ordered = false, User = user.ID, User1 = user };
                    }

                    CartItem item = null;
                    bool hasItem = cart.CartItems.Any(x => x.MenuItem.ID == id);
                    if(hasItem)
                    {
                        item = cart.CartItems.FirstOrDefault(x => x.MenuItem.ID == id);
                        item.Quantity++;
                    }
                    else
                    {
                        item = new CartItem() {MenuItem = menuItem, Item = menuItem.ID, Cart = cart.ID, Cart1 = cart, Quantity = 1 };
                        cart.CartItems.Add(item);
                    }
                    if(!hasCart)
                        db.databaseModel.Carts.Add(cart);
                    db.databaseModel.SaveChanges();
                    return new StatusResponse() { Status = "Success", Message = ""};
                }
            }
            return new StatusResponse() { Status = "Failed", Message = "Not authenticated"};
        }

        // DELETE api/values/5
        public StatusResponse Delete(int id)
        {
            if (Request.Headers.Contains("SessionKey"))
            {
                var db = new Database();
                var menuItem = db.databaseModel.MenuItems.FirstOrDefault(x => x.ID == id);
                var header = Request.Headers.GetValues("SessionKey").First();
                var user = db.databaseModel.Users.SingleOrDefault(x => x.SessionKey == header);
                if (user != null && user.SessionKey == header)
                {
                    user.LastSeen = DateTime.Now;
                    bool hasCart = user.Carts.Any(x => x.Ordered == false);
                    Cart cart = null;
                    if (hasCart)
                    {
                        cart = user.Carts.FirstOrDefault(x => x.Ordered == false);
                        bool hasItem = cart.CartItems.Any(x => x.MenuItem.ID == id);
                        if(hasItem)
                        {
                            //var item = cart.CartItems.FirstOrDefault(x => x.MenuItem.ID == id);
                            var item = db.databaseModel.CartItems.FirstOrDefault(x => x.MenuItem.ID == id && x.Cart == cart.ID);
                            db.databaseModel.CartItems.Remove(item);
                            //cart.CartItems.Remove(item);
                        }
                    }
                    
                    db.databaseModel.SaveChanges();
                    return new StatusResponse() { Status = "Success", Message = "" };
                }
            }
            return new StatusResponse() { Status = "Failed", Message = "Not authenticated" };
        }
    }
}

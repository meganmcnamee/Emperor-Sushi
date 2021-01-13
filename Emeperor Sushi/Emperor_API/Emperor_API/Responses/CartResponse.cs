using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Emperor_API.Responses;
using Emperor_Backend;

namespace Emperor_API.Responses
{
    public class CartResponse : StatusResponse
    {
        public CartItem[] Cart { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Emperor_Backend;

namespace Emperor_API.Responses
{
    public class OrderResponse : StatusResponse
    {
        public Order[] Orders { get; set; }
    }
}
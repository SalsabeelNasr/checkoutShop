using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckoutShop.Api.Models
{
    public class OrderModels
    {
        public class Product
        {
            public int Id { get; set; }
            public int Quantity { get;  set; }
        }
    }
}
using System;

namespace CheckoutShop.BL.Entities
{
    public class Product : Entity
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public decimal Price { get;  set; }
    }
}

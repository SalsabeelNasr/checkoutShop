using System.Collections.Generic;
namespace CheckoutShop.BL.Entities
{
    public class Order : Entity
    {
        public int Id { get; set; }
        public List<OrderItem> items { get; set; }
    }
}

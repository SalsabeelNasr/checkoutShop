using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutShop.BL.Entities
{
    public class OrderItem
    {
        public Product Item { get; set; }
        public int Quantity { get; set; }
    }
}

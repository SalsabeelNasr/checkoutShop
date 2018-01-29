using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutShop.BL.Entities
{
    public class AbstractList<T>
    {
        public AbstractList()
        {
            entitis = new List<T>();
            links = new List<Link>();
        }
        public List<T> entitis;
        public List<Link> links;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutShop.BL.Entities
{
    public abstract class Entity
    {
        public Entity()
        {
            this.Links = new List<Link>();
        }

        private List<Link> links;

        public List<Link> Links
        {
            get
            {
                return links;
            }

            set
            {
                links = value;
            }
        }
    }
}

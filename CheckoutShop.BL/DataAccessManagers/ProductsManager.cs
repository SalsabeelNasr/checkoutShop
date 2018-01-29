using CheckoutShop.DL;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutShop.BL.DataAccessManagers
{
    /// <summary>
    /// This controller is responsible for managing CRUD operations for the Products 
    /// </summary>
    public class ProductsManager
    {
        public ICheckoutShopEntities context;
        public ProductsManager()
        {
            this.context = new CheckoutShopEntities();
        }
        public ProductsManager(ICheckoutShopEntities context)
        {
            this.context = context;
        }
        /// <summary>
        /// retrieves all products
        /// </summary>
        /// <returns>List of Entities.Product</returns>
        public List<Entities.Product> retrieve()
        {
            try
            {
                using (context)
                {
                    return context.Products.Select(p => new Entities.Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price
                    }).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// retrieves product by id
        /// </summary>
        /// <returns>Entities.Product</returns>
        public Entities.Product retrieve(int id)
        {
            try
            {
                using (context)
                {
                    return context.Products.Where(o=>o.Id == id).Select(p => new Entities.Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price
                    }).FirstOrDefault();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

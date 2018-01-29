using CheckoutShop.DL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutShop.BL.DataAccessManagers
{
    public class OrdersManager
    {

        public ICheckoutShopEntities context;
        public OrdersManager()
        {
            this.context = new CheckoutShopEntities();
        }
        public OrdersManager(ICheckoutShopEntities context)
        {
            this.context = context;
        }
        public bool create(Entities.Order order , string userId)
        {
            try
            {
                using (context)
                {
                    DL.Order newOrder = new DL.Order();
                    newOrder.UserId = userId;
                    newOrder.Created = DateTime.Now;
                    AspNetUser user = context.AspNetUsers.Where(u => u.Id == userId).FirstOrDefault();
                    newOrder.AspNetUser = user;
                    List<OrderProduct> orderProducts = new List<OrderProduct>();
                    foreach (var item in order.items)
                    {
                        orderProducts.Add(new OrderProduct()
                        {
                            Product = context.Products.Where(p => p.Id == item.Item.Id).FirstOrDefault(),
                            Quantity = item.Quantity
                        });
                    }
                    newOrder.OrderProducts = orderProducts;
                    context.Orders.Add(newOrder);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public List<Entities.Order> retrieve(string userId)
        {
            try
            {
                using (context)
                {
                    return context.Orders.Where(o => o.UserId == userId).Select(up => new Entities.Order
                    {
                        Id = up.Id,
                        items = up.OrderProducts.Select(op => op.Product).Select(o => new Entities.OrderItem
                        {
                            Item = new Entities.Product()
                            {
                                Id = o.Id,
                                Name = o.Name,
                                Price = o.Price
                            }

                        }).ToList()
                    }
                    ).ToList();
                }

            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public Entities.Order retrieve(int id,string userId)
        {
            try
            {

                using (context)
                {
                    return context.Orders.Where(o => o.UserId == userId && o.Id == id).Select(up => new Entities.Order
                    {
                        Id = 1,
                        items = up.OrderProducts.Select(op => op.Product).Select(o => new Entities.OrderItem
                        {
                            Item = new Entities.Product()
                            {
                                Id = o.Id,
                                Name = o.Name,
                                Price = o.Price
                            }
                        }).ToList()
                    }
                    ).FirstOrDefault();
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public bool delete(int id)
        {
            try
            {

                using (context)
                {
                    DL.Order order = context.Orders.Where(a => a.Id == id).FirstOrDefault();
                    if (order != null)
                    {
                        context.Orders.Remove(order);
                        context.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
    }
}

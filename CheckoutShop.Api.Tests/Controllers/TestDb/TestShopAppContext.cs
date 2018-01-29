using CheckoutShop.DL;
using System;
using System.Data.Entity;

namespace CheckoutShop.Api.Tests.Controllers.TestDb
{
    public class TestShopAppContext : CheckoutShop.DL.ICheckoutShopEntities
    {
        public TestShopAppContext()
        {
            this.Products = new TestDbSet<Product>() { new Product() { Id = 1 } };
            this.Orders = new TestDbSet<Order>() { new Order() { Id = 1, UserId = "1234" } };
            this.AspNetUsers = new TestDbSet<AspNetUser> { new AspNetUser() { Id = "1234" } };
            this.OrderProducts = new TestDbSet<OrderProduct> { new OrderProduct() { OrderId = 1, ProductId = 1 } };
        }

        public DbSet<AspNetRole> AspNetRoles
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DbSet<AspNetUserClaim> AspNetUserClaims
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DbSet<AspNetUserLogin> AspNetUserLogins
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DbSet<AspNetUser> AspNetUsers { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }
        public void Dispose() { }

        public int SaveChanges()
        {
            return 0;
        }
    }
}

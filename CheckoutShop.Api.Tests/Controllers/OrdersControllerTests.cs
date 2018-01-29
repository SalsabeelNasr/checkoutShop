using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using CheckoutShop.Api.Tests.Controllers.TestDb;
using CheckoutShop.BL.DataAccessManagers;
using CheckoutShop.BL.Entities;
using System.Web.Http.Results;
using System.Collections.Generic;
using CheckoutShop.Api.Models;

namespace CheckoutShop.Api.Controllers.Tests
{
    [TestClass()]
    public class OrdersControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            //// Arrange
            var testContext = new TestShopAppContext();
            var controller = new OrdersController(new OrdersManager(testContext));
            controller.UserId = "1234";
            //// Act
            var actual = controller.Get() as OkNegotiatedContentResult<AbstractList<Order>>;

            //// Assert
            Assert.AreEqual(testContext.Orders.Count(), actual.Content.entitis.Count);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            //// Arrange
            var testContext = new TestShopAppContext();
            var controller = new OrdersController(new OrdersManager(testContext));
            controller.UserId = "1234";

            //// Act
            var actual = controller.Get(1) as OkNegotiatedContentResult<Order>;
            var expected = testContext.Products.Where(p => p.Id == 1).FirstOrDefault();

            //// Assert
            if (expected != null && actual != null)
                Assert.AreEqual(expected.Id, actual.Content.Id);
            else
                Assert.Fail();
        }

        [TestMethod()]
        public void GetByIdTest_NotFound()
        {
            //// Arrange
            var testContext = new TestShopAppContext();
            var controller = new OrdersController(new OrdersManager(testContext));
            controller.UserId = "1234";

            //// Act
            var actual = controller.Get(2) as NotFoundResult;

            //// Assert
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void DeleteTest()
        {
            //// Arrange
            var testContext = new TestShopAppContext();
            var controller = new OrdersController(new OrdersManager(testContext));
            controller.UserId = "1234";

            //// Act
            var actual = controller.Delete(1) as OkResult;

            //// Assert
            Assert.IsInstanceOfType(actual, typeof(OkResult));
            Assert.AreEqual(testContext.Orders.Count(), 0);
        }

        [TestMethod()]
        public void DeleteNotFoundTest()
        {
            //// Arrange
            var testContext = new TestShopAppContext();
            var controller = new OrdersController(new OrdersManager(testContext));
            controller.UserId = "1234";

            //// Act
            var actual = controller.Delete(2) as NotFoundResult;

            //// Assert
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
            Assert.AreEqual(testContext.Orders.Count(), 1);
        }

        [TestMethod()]
        public void PostTest()
        {
            //// Arrange
            var testContext = new TestShopAppContext();
            var controller = new OrdersController(new OrdersManager(testContext));
            controller.UserId = "1234";

            var order = new List<OrderModels.Product>();
            order.Add(new OrderModels.Product() { Id = 1, Quantity = 5 });
            //// Act
            var actual = controller.Post(order) as OkNegotiatedContentResult<bool>;
            //// Assert
            Assert.AreEqual(actual.Content,true);
        }

    }
}
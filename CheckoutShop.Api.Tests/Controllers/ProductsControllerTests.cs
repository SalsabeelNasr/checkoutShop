using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using CheckoutShop.BL.DataAccessManagers;
using CheckoutShop.Api.Tests.Controllers.TestDb;
using CheckoutShop.BL.Entities;
using System.Web.Http.Results;
using System.Collections.Generic;

namespace CheckoutShop.Api.Controllers.Tests
{
    [TestClass()]
    public class ProductsControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            //// Arrange
            var testContext = new TestShopAppContext();
            var controller = new ProductsController(new ProductsManager(testContext));

            //// Act
            var actual = controller.Get() as OkNegotiatedContentResult<AbstractList<Product>>;

            //// Assert
            Assert.AreEqual(testContext.Products.Count(), actual.Content.entitis.Count);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            //// Arrange
            var testContext = new TestShopAppContext();
            var controller = new ProductsController(new ProductsManager(testContext));

            //// Act
            var actual = controller.Get(1) as OkNegotiatedContentResult<Product>;
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
            var controller = new ProductsController(new ProductsManager(testContext));

            //// Act
            var actual = controller.Get(2) as NotFoundResult;

            //// Assert
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }
    }
}
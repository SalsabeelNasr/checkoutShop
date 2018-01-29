using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckoutShop.Api;
using CheckoutShop.Api.Controllers;

namespace CheckoutShop.Api.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}

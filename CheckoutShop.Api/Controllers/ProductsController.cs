using System; 
using System.Web.Http;
using CheckoutShop.BL.DataAccessManagers; 
using CheckoutShop.BL.Entities; 

namespace CheckoutShop.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        ProductsManager productsManager;
        public ProductsController()
        {
            this.productsManager = new ProductsManager();
        }
        public ProductsController(ProductsManager repository)
        {
            this.productsManager = repository;
        }

        [HttpGet, Route("", Name = "GetAllProducts")]
        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            try
            {
                AbstractList<Product> productsList = new AbstractList<Product>();
                productsList.entitis = productsManager.retrieve();
                productsList.entitis.ForEach(o => o.Links.Add(new Link()
                {
                    Rel = "Self",
                    Method = "Get",
                    Href = "/produts/" + o.Id
                }));
                productsList.links.Add(new Link()
                {
                    Rel = "Self",
                    Method = "Get",
                    Href = "/produts/"
                });
                return Ok(productsList);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet, Route("{id}", Name = "GetProductById")]
        /// <summary>
        /// Gets product by id 
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            try
            {
                var product = productsManager.retrieve(id);
                if(product != null)
                {
                    product.Links.Add(new Link()
                    {
                        Rel = "Self",
                        Method = "Get",
                        Href = "/produts/" + product.Id
                    });
                    return Ok(product);
                }
                return NotFound();
 
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
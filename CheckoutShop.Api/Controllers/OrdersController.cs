using CheckoutShop.Api.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using CheckoutShop.BL.DataAccessManagers;
using CheckoutShop.BL.Entities;

namespace CheckoutShop.Api.Controllers
{
    [RoutePrefix("api/Orders")]
    [Authorize]
    public class OrdersController : ApiController
    {
        private OrdersManager orderManager;

        private String userId;
        public String UserId
        {
            get
            {
                if (userId == null)
                    UserId = IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
                return userId;
            }
            set
            {
                userId = value;
            }
        }
        public OrdersController()
        {
            this.orderManager = new OrdersManager();
        }

        public OrdersController(OrdersManager repository)
        {
            this.orderManager = repository;
        }

        /// <summary>
        /// Get user orders
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("", Name = "GetUserOrders")]
        public IHttpActionResult Get()
        {
            try
            {
                AbstractList<Order> ordersList = new AbstractList<Order>();
                
                ordersList.entitis = orderManager.retrieve(userId);
                ordersList.entitis.ForEach(o => o.Links.Add(new Link()
                {
                    Rel = "Get Order Details",
                    Method = "Get",
                    Href = "/Orders/" + o.Id
                }));
                ordersList.entitis.ForEach(o => o.Links.Add(new Link()
                {
                    Rel = "Delete Order",
                    Method = "Delete",
                    Href = "/Orders/" + o.Id
                }));
                ordersList.links.Add(new Link()
                {
                    Rel = "Self",
                    Method = "Get",
                    Href = "/Orders/"
                });
                ordersList.links.Add(new Link()
                {
                    Rel = "Create New Order",
                    Method = "Post",
                    Href = "/Orders/"
                });
                return Ok(ordersList);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        /// <summary>
        /// Gets order by id 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}", Name = "GetOrderById")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Order order = orderManager.retrieve(id,this.UserId);
                if (order == null)
                {
                    return NotFound();
                }
                order.Links.Add(new Link()
                {
                    Rel = "Self",
                    Method = "Get",
                    Href = "/Orders/" + order.Id
                });
                order.Links.Add(new Link()
                {
                    Rel = "Delete Order",
                    Method = "Delete",
                    Href = "/Orders/" + order.Id
                });
                order.Links.Add(new Link()
                {
                    Rel = "Create Order",
                    Method = "Post",
                    Href = "/Orders/"
                });
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        /// <summary>
        /// Creates a new order and validates it  
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost, Route("", Name = "CreateOrder")]
        public IHttpActionResult Post(List<OrderModels.Product> order)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                List<OrderItem> orderItems = new List<OrderItem>();
                foreach (var item in order)
                {
                    orderItems.Add(new OrderItem() { Item =new Product() { Id = item.Id },Quantity = item.Quantity });
                }
                Order newOrder = new Order()
                {
                    items = orderItems
                };
                bool created = this.orderManager.create(newOrder,this.UserId);
                if (created)
                    return Ok(created);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
       
        /// <summary>
        /// Deletes order by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("{id}", Name = "DeleteOrderById")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                bool deleted = orderManager.delete(id);
                if (deleted)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
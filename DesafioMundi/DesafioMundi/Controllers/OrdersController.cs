using DesafioMundi.Entities;
using DesafioMundi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DesafioMundi.Entities.Response;
using Microsoft.AspNetCore.Authorization;

namespace DesafioMundi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<Dictionary<string,string>> Get()
        {
            return _orderService.GetAll();
         }
        [HttpGet("{id}")]
        public ActionResult<List<string>> Get(string id)
        {
            return _orderService.GetForId(id);
        }

        [HttpPost("{customerId}/{cardId}")]
        public ActionResult<OrderResponse> CreateOrder(string customerId, string cardId, [FromBody] Item[] item)
        {
           return _orderService.CreateOrder(customerId, cardId, item);
        }
    }
}

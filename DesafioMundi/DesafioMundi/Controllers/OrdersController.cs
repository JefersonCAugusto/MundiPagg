using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DesafioMundi.Entities;
using DesafioMundi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MundiAPI.PCL;
using MundiAPI.PCL.Models;

namespace DesafioMundi.Controllers
{
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
        public ActionResult<string> Get()
        {
            return "Esta funcionando";
        }

        [HttpPost("{customerId}/{cardId}")]
        public ActionResult<Order> CreateOrder(string customerId, string cardId, [FromBody] Item[] item)
        {
           return _orderService.CreateOrder(customerId, cardId, item);
        }
    }
}
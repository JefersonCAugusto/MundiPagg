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

        /// <summary>
        /// Solicita todas as Orders
        /// </summary>
        /// <returns>Retorna um dicionário contendo Id da Order associado ao Customer</returns>
        [HttpGet]
        public ActionResult<Dictionary<string,string>> Get()
        {
            return _orderService.GetAll();
         }
        /// <summary>
        /// Solicita ordens de serviço de um Customer específico 
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id do Customer</param>
        /// <returns>Retonra uma Lista de Orders do Customer informado</returns>
        [HttpGet("{id}")]
        public ActionResult<List<string>> Get(string id)
        {
            return _orderService.GetForId(id);
        }
        /// <summary> 
        /// Cria uma Order
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// [
        ///     {
        ///         "Amount":"10000",
        ///         "Description":"Mastro de bandeira", 
        ///         "Quantity":"2"
        ///     },
        ///     {
        ///         "Amount":"1",
        ///         "Description":"balinha de um centavo", 
        ///         "Quantity":"2"
        ///     },
        ///     {
        ///         "Amount":"100",
        ///         "Description":"Mouse Ps2", 
        ///         "Quantity":"2"
        ///     },
        ///     {
        ///         "Amount":"1500",
        ///         "Description":"Fone de ouvido", 
        ///         "Quantity":"15"
        ///     }
        /// ]
        ///
        /// </remarks>
        /// <param name="customerId">Id do Customer </param>
        /// <param name="cardId">Id do Cartão pertendente ao Customer</param>
        /// <param name="item">Aray de objetos contendo os itens do pedido</param>
        /// <returns></returns>
        [HttpPost("{customerId}/{cardId}")]
        public ActionResult<OrderResponse> CreateOrder(string customerId, string cardId, [FromBody] Item[] item)
        {
           return _orderService.CreateOrder(customerId, cardId, item);
        }
    }
}

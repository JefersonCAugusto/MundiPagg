using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DesafioMundi.Entities;
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
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Esta funcionando";
        }

        [HttpPost("{customerId}/{cardId}")]
        public ActionResult<GetOrderResponse> CreateOrder(string customerId, string cardId, [FromBody] Item[] item)
        {
            //validar token
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);

            //lista de itens 

            //lista de itens 
            var oss = new List<CreateOrderItemRequest>();
            foreach (var it in item)
            {
                oss.Add(new CreateOrderItemRequest()
                {
                    Description = it.Description,
                    Amount = it.Amount,
                    Quantity = it.Quantity
                });
            }


            //lista meios de pagamentos
            var payments = new List<CreatePaymentRequest>()
                {
                    new CreatePaymentRequest()
                    {
                        PaymentMethod = "credit_card",
                        CreditCard = new CreateCreditCardPaymentRequest
                        {
                            CardId= "card_nKJEMNgcVOh0j2b7",
                            Card = new CreateCardRequest
                            {
                                Cvv= "123"
                            }
                        }

                    }
                };

            //criação do pedido
            var order = new CreateOrderRequest()
            {
                Items = oss,
                CustomerId=customerId,
                Payments=payments
            };
           
            var pedido = client.Orders.CreateOrder(order);
            return pedido;

        }
    }
}
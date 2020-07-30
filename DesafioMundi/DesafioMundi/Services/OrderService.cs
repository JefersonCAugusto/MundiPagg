using DesafioMundi.Entities;
using MundiAPI.PCL;
using MundiAPI.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioMundi.Services.Interfaces;


namespace DesafioMundi.Services
{
    public class OrderService: IOrderService
    {
       
        public Order CreateOrder(string customerId, string cardId, Item[] item)
        {
            //validar token
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);


            //lista de itens 
         
            var listitems = new List<CreateOrderItemRequest>();
            foreach (var items in  item)
            {
                //lista para adicionar ao pedido
                listitems.Add(new CreateOrderItemRequest()
                { 
                    Description = items.Description,
                    Amount = items.Amount,
                    Quantity = items.Quantity
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
                        CardId= cardId
                    } 
                }
            }; 
            //criação do pedido
            var order = new CreateOrderRequest()
            {
                Items = listitems, 
                Payments = payments,
                CustomerId = customerId
            };
            var pedido = client.Orders.CreateOrder(order);
            // pedido finalizado

             
            var listdeitens = pedido.Items.Select(x => x.Id);

            
            var response = new Order()
            {
                Id = pedido.Id,
                Code = pedido.Code,
                ChargeId = pedido.Charges
                                 .Where(x => x.PaymentMethod == "credit_card")
                                 .FirstOrDefault().ToString(),
                Status= pedido.Status,
               
            };
            return response;
        }
    }
}

using DesafioMundi.Entities;
using MundiAPI.PCL;
using MundiAPI.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioMundi.Services.Interfaces;
using DesafioMundi.Context;
using DesafioMundi.Entities.Response;

namespace DesafioMundi.Services
{
    public class OrderService: IOrderService
    {
            private readonly MundiContext _context;

        public OrderService(MundiContext context)
        {
            _context = context;
        }

        public OrderResponse CreateOrder(string customerId, string cardId, Item[] item)
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

            //lista meios de pagamentos dada somente por uma opção o cartão de creditto do cliente
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


            //avalia se existe id válido
            if (!string.IsNullOrEmpty(pedido.Id))
            {
                //recupera o unico* charge da order
                var listCharges = pedido.Charges.FirstOrDefault();
                //recupera os itens na order
                var listItems = pedido.Items.ToList();
                var saveItem = new List<Item>();
                foreach (var items in listItems)
                {
                  saveItem.Add(new Item() { 
                            Amount = items.Amount,
                            Description = items.Description,
                            Id = items.Id,
                            Quantity = items.Quantity,
                           


                        }
                    );
                }
                //salva dados
                _context.Charges.Add(new Charge()
                {
                    Amount = listCharges.Amount,
                    Code = listCharges.Code,
                    CreditCard=_context.CreditCards.Find(cardId),
                    Customer= _context.Customers.Find(customerId),
                   //CreditCardId = cardId,
                    Id = listCharges.Id,
                   // CustomerId = customerId,
                  
                    Order = new Order()
                    {
                        Id = pedido.Id,
                        Code = pedido.Code,
                        Status = pedido.Status,
                       // ChargeId = listCharges.Id,
                        Items=saveItem 

                    },
                    
                });;

                _context.SaveChanges();
            } 
          
            var response = new OrderResponse()
            {
                Id = pedido.Id,
                Code = pedido.Code,
                Status= pedido.Status,
               
            };
            return response;
        }

        public Dictionary<string, string> GetAll()
        {
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);

            var response = client.Orders.GetOrders();
            Dictionary<string, string> orderCustomer = new Dictionary<string, string>();
            foreach (var d1 in response.Data)
            {
                orderCustomer.Add(d1.Id, d1.Customer.Id);
            }
            return orderCustomer;
        }
        
        public  List<string>  GetForId(string id)
        { 
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword); 

            var response = client.Orders.GetOrders();
            return response.Data.Where(x => x.Customer.Id == id).Select(x => x.Id).ToList(); 
        }
    }
}

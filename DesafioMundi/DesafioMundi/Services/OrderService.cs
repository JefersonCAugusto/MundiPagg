using DesafioMundi.Context;
using DesafioMundi.Entities;
using DesafioMundi.Entities.Response;
using DesafioMundi.Services.Interfaces;
using MundiAPI.PCL;
using MundiAPI.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword); 
            //Cria uma lista de itens 
            var listitems = new List<CreateOrderItemRequest>();
            foreach (var items in  item)
            { 
                listitems.Add(new CreateOrderItemRequest()
                { 
                    Description = items.Description,
                    Amount = items.Amount,
                    Quantity = items.Quantity 
                }); 
            } 
            //cria forma de pagamento
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

            // tenta fecha pedido
            GetOrderResponse pedido;
            try
            {
                 pedido = client.Orders.CreateOrder(order);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Não foi possivel criar o pedido " +
                    $", devido ao erro: " + e.Message);
            }


            //recupera a unica cobrança da lista de cobranças
            var listCharges = pedido.Charges.FirstOrDefault();
            //recupera os itens do pedido
            var listItems = pedido.Items.ToList(); 
            var saveItem = new List<Item>();
            foreach (var items in listItems)
            {
                saveItem.Add(new Item() 
                { 
                    Amount = items.Amount,
                    Description = items.Description,
                    Id = items.Id,
                    Quantity = items.Quantity, 
                });
                //Tenta persistir os dados no banco
                try
                {
                    var customer = _context.Customers.Find(customerId);
                    customer.Charges = new List<Charge>()
                    {
                        new Charge
                            {
                               
                                Amount = listCharges.Amount,
                                Code = listCharges.Code,
                                CreditCard = _context.CreditCards.Find(cardId),
                                // Customer = _context.Customers.Find(customerId),

                                Order = new Order()
                                {
                                    Id = pedido.Id,
                                    Code = pedido.Code,
                                    Status = pedido.Status,
                                    Items = saveItem
                                },
                                Id = listCharges.Id
                            }
                    };
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException($"Foi criado o pedido {pedido.Id} " +
                        $"com a cobrança {listCharges.Id}, mas não foi possivel persistir os daods no banco de dados " +
                        $"devido ao erro: " + e.Message);
                } 
            } 
          //monta response
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
            //Tenta recuperar os ordens de serviço
            ListOrderResponse response;
            try
            {
                  response = client.Orders.GetOrders();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Falha ao recuperar os dados das Ordens de serviço" +
                    $" devido ao erro: " + e.Message);
            }
            //monta uma estrutura chave valor(IdOrdem, IdCustomer)
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
            //Tenta recuperar os ordens de serviço
            ListOrderResponse response;
            try
            {
                response = client.Orders.GetOrders();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Falha ao recuperar os dados da Ordem de serviço" +
                    $" devido ao erro: " + e.Message);
            }

            return response.Data.Where(x => x.Customer.Id == id).Select(x => x.Id).ToList(); 
        }

    }
}

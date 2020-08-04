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
    public class CardService :ICardService
    {
        private readonly MundiContext _context;
        private readonly ICustomerService _customerService;

        public CardService(ICustomerService customerService, MundiContext context)
        {
            _customerService = customerService;
            _context = context;
        }

        public CreditCardResponse CreateCard(string customerId, CreditCard creditCard)
        {
            var customer = _customerService.GetCustomer(customerId);
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);
            //Define estrutura do request para o criar Cartao
            var createCardRequest = new CreateCardRequest
            {
                Number = creditCard.Number,
                HolderName = customer.Name,
                HolderDocument = customer.Document,
                Brand = creditCard.Brand,
                ExpMonth = creditCard.ExpMonth,
                ExpYear = creditCard.ExpYear,
                PrivateLabel = false,
                Cvv = creditCard.CVV,
            };
            GetCardResponse createCard; 
            //Tenta criar o cartão
            try
            {
                 createCard = client.Customers.CreateCard(customerId, createCardRequest);
            }
            catch(Exception e)
            {
                throw new InvalidOperationException($"Não foi possivel criar o cartão " +
                    $"{creditCard.Number.Substring(creditCard.Number.Length - 4)}, devido ao erro: "+e.Message);
            } 
            //Tenta Persistir os dados no banco
            try
            {
                var saveCustomerCard = _context.Customers.FirstOrDefault(x => x.Id == customerId);
                saveCustomerCard.CreditCard = new List<CreditCard>
                {
                   new CreditCard
                        {
                            Id = createCard.Id,
                            Brand = createCard.Brand,
                            LestFourNumbers = creditCard.Number.Substring(creditCard.Number.Length - 4)
                        }
                };
             
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Foi criado o Cartão {createCard.Id} para o Cliente {customerId.Split(" ")[0]}, mas não" +
                $" foi possivel salvar os dados do mesmo no banco de dados devido ao seguinte erro:"+ e.Message);

            }
            return new CreditCardResponse() 
            { 
                Id = createCard.Id,
                Brand = createCard.Brand,
                LestFourNumbers= creditCard.Number.Substring(creditCard.Number.Length - 4)
            };

        }
        public IEnumerable<CreditCardResponse> GetCards(string id)
        {
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);
            ListCardsResponse cards;
            //Tenta recuperar os cartões de um cliente a partir da base da mundi
            try
            {
               cards = client.Customers.GetCards(id);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Falha ao recuperar os dados do cleinte " +
                    $"{id} devido ao erro: " + e.Message); 
            } 
            //Recupera as informações para montar o CreditCardResponse
            var idCard = cards.Data.Select(x=>x.Id).ToList();  
            var cardIdList = new List<CreditCardResponse>();
            foreach (var x1 in cards.Data)
            {
                cardIdList.Add(new CreditCardResponse 
                {
                    Id=x1.Id,
                    Brand= x1.Brand,
                    LestFourNumbers= x1.LastFourDigits
                });
            }
            return cardIdList;
        }
        public List<CreditCardResponse> GetCards(string idCustomer, string idCard)
        {
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword); 
            //tenta recuperar os dados de um cartão espeecífico de um cliente
            GetCardResponse cards;
            try
            {
            cards = client.Customers.GetCard(idCustomer, idCard);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Falha ao recuperar os dados do Cleinte " +
                                    $"{idCustomer} devido ao erro: " + e.Message);
            }
            //Recupera as informações para montar o CreditCardResponse
            var creditCard = new List<CreditCardResponse>();
            creditCard.Add(new CreditCardResponse
            {
                Id= cards.Id,
                Brand = cards.Brand,
                LestFourNumbers = cards.LastFourDigits
            }); 
           return creditCard;
        } 
    }

}

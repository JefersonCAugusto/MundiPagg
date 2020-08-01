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

        public CreditCardResponse CreateCard(string id, CreditCard creditCard)
        {
            var customer = _customerService.GetCustomer(id);
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);
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

            var createCard = client.Customers.CreateCard(id, createCardRequest);
           

            if (string.IsNullOrEmpty(createCard.Id))
                throw new InvalidOperationException("Não foi possivel criar cartão");
            //recupera o cliente e dicionar cartão
            var saveCustomerCard = _context.Customers.Find(id);
            saveCustomerCard.creditCard.Add(  new CreditCard
            {
                Id = createCard.Id,
                Brand = createCard.Brand,
                LestFourNumbers = creditCard.Number.Substring(creditCard.Number.Length - 4),
               
            });
             
            _context.SaveChanges();
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
            var cards = client.Customers.GetCards(id);

            var idCard = cards.Data.Select(x=>x.Id).ToList(); 
            if (idCard.Any(x=>x.Equals(null)))//verifica se existe cartão
                throw new InvalidOperationException("Falha ao recuperar cartão");

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
            var cards = client.Customers.GetCard(idCustomer, idCard);

            //falta testar se os dados de card e customer estao correFtos
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

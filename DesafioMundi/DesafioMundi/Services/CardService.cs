using DesafioMundi.Entities;
using DesafioMundi.Services.Interfaces;
using MundiAPI.PCL;
using MundiAPI.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundi.Services
{
    public class CardService
    {
        private readonly ICustomerService _customerService;

        public CardService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public string CreateCard(string id, CreditCard creditCard)
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
            return createCard.Id;
        }
    }

}

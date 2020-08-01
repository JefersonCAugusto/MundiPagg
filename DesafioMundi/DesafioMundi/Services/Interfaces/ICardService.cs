using DesafioMundi.Entities;
using System;
using System.Collections.Generic;
using DesafioMundi.Entities.Response;
namespace DesafioMundi.Services.Interfaces
{
    public interface ICardService
    {
        CreditCardResponse CreateCard(string id, CreditCard creditCard);
        List<CreditCardResponse> GetCards(string idCustomer, string idCard);
        IEnumerable<CreditCardResponse> GetCards(string id);    }
}

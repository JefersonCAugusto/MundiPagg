using DesafioMundi.Entities;
using System;
using System.Collections.Generic;
using DesafioMundi.Entities.Response;
namespace DesafioMundi.Services.Interfaces
{
    public interface ICardService
    {
        CreditCardResponse CreateCard(string id, CreditCard creditCard);
        List<CreditCard> GetCards(string idCustomer, string idCard);
        IEnumerable<CreditCard> GetCards(string id);    }
}

using DesafioMundi.Entities;
using System;
using System.Collections.Generic;

namespace DesafioMundi.Services.Interfaces
{
    public interface ICardService
    {
        string CreateCard(string id, CreditCard creditCard);
        List<CreditCard> GetCards(string idCustomer, string idCard);
        IEnumerable<CreditCard> GetCards(string id);    }
}

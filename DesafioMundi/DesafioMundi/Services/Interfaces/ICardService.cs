using DesafioMundi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundi.Services.Interfaces
{
    public interface ICardService
    {
        string CreateCard(string id, CreditCard creditCard);
    }
}

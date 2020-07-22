using DesafioMundi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundi.Services.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(string customerId, string cardId, Item[] item);
    }
}

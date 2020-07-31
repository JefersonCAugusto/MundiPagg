using DesafioMundi.Entities;
using DesafioMundi.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundi.Services.Interfaces
{
    public interface IOrderService
    {
        OrderResponse CreateOrder(string customerId, string cardId, Item[] item);
        Dictionary<string, string> GetAll();
        List<string> GetForId(string id);

    }
}

using DesafioMundi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundi.Services.Interfaces
{
    public interface ICustomerService
    {

        IEnumerable<Customer> GetCustomer();
        Customer GetCustomer(string id);
        string PostCustomer(Customer customer);
        void PutCustomer(string id, Customer value);
       // string CreateCard(CreditCard creditCard );

    }
}

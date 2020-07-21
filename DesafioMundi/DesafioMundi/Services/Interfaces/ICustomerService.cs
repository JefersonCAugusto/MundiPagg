using DesafioMundi.Entities;
using System.Collections.Generic;

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

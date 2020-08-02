using DesafioMundi.Entities;
using DesafioMundi.Entities.Response;
using System.Collections.Generic;

namespace DesafioMundi.Services.Interfaces
{
    public interface ICustomerService
    {

        IEnumerable<Customer> GetCustomer();
        Customer  GetCustomer(string id);
        CustomerResponse PostCustomer(Customer customer);
        
        

    }
}

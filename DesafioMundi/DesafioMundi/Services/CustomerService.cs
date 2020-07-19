using DesafioMundi.Entities;
using DesafioMundi.Services.Interfaces;
using MundiAPI.PCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundi.Services
{
    public class CustomerService : ICustomerService
    {
        public IEnumerable<Customer> GetCustomer()
        {
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword); //conecta
            var response = client.Customers.GetCustomers();                             //faz requisição
            List<Customer> customer = new List<Customer>();
            foreach (var cus in response.Data.Select(x => x))
            {
                customer.Add(new Customer { Name = cus.Name, Email = cus.Email, Id = cus.Id, Document = cus.Document, Type = cus.Type });
            }
            return customer;
        }

        public Customer GetCustomer(string id)
        {
            throw new NotImplementedException();
        }

        public string PostCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void PutCustomer(string id, Customer value)
        {
            throw new NotImplementedException();
        }
    }
}

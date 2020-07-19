using DesafioMundi.Entities;
using DesafioMundi.Services.Interfaces;
using MundiAPI.PCL;
using MundiAPI.PCL.Models;
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
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);  
            var response = client.Customers.GetCustomers();                             
            List<Customer> customer = new List<Customer>();
            foreach (var cus in response.Data.Select(x => x))
            {
                customer.Add(new Customer { Name = cus.Name, Email = cus.Email, Id = cus.Id, Document = cus.Document, Type = cus.Type });
            }
            return customer;
        }

        public Customer GetCustomer(string id)
        {
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);
            var response = client.Customers.GetCustomer(id);


            return new Customer
            {
                Name = response.Name,
                Email = response.Email,
                Id = response.Id,
                Document = response.Document,
                Type = response.Type
            };
        }

        public string PostCustomer(Customer customer)
        {
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);
            var create = new CreateCustomerRequest
            {
                Name = customer.Name,
                Email = customer.Email,
                Document = customer.Document,
                Gender = customer.Gender,
                Type = customer.Type
            };
            var response = client.Customers.CreateCustomer(create);
            return response.Id;
        }

        public void PutCustomer(string id, Customer value)
        {
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);
            var getCustomer = client.Customers.GetCustomer(id);

            var update = new UpdateCustomerRequest
            {
                Name = value.Name,
                Email = value.Email,
                Document = value.Document
            };

            var response = client.Customers.UpdateCustomer(id, update);
        }

        public string CreateCard(CreditCard creditCard)
        {
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword); //conecta

            var createCardRequest = new CreateCardRequest
            {
                Brand = creditCard.Brand,
                Number = creditCard.Number,
                ExpMonth = creditCard.ExpMonth,
                ExpYear = creditCard.ExpYear,
                Cvv = creditCard.CVV,

            };

            var createCard = client.Customers.CreateCard(creditCard.CustomerId, createCardRequest);


            return createCard.Id;
        }

    }
}

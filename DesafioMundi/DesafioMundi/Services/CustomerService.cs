using DesafioMundi.Context;
using DesafioMundi.Entities;
using DesafioMundi.Entities.Response;
using DesafioMundi.Services.Interfaces;
using MundiAPI.PCL;
using MundiAPI.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioMundi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly MundiContext _context;

        public CustomerService(MundiContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetCustomer()
        {
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);
            //Tenta recuperar todos os  clientes a partir da base da mundi
            ListCustomersResponse response;
            try
            {
                response = client.Customers.GetCustomers();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Falha ao recuperar os dados dos clientes " +
                    $"devido ao erro: " + e.Message);
            }
            //monta lista de clientes com as informações do response obtido da mundi
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
            //Tenta recuperar  um cliente a partir da base da mundi
            GetCustomerResponse response;
            try
            {
                response = client.Customers.GetCustomer(id);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Falha ao recuperar os dados do cleinte " +
                    $"{id} devido ao erro: " + e.Message);
            }
            //Recupera as informações para montar o Customer(Não vou usar o customerResponse para obtermos informações mais completas)
            var customerResponse = new Customer
            {
                Name = response.Name,
                Email = response.Email,
                Id = response.Id,
                Document = response.Document,
                Type = response.Type
            };
            return customerResponse;

        }

        public CustomerResponse PostCustomer(Customer customer)
        {
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);
            //Define estrutura do request para o criar um Customer
            var create = new CreateCustomerRequest
            {
                Name = customer.Name,
                Email = customer.Email,
                Document = customer.Document,
                Gender = customer.Gender,
                Type = customer.Type
            };
            //Tenta criar o Customer
            GetCustomerResponse response;
            try
            {
                  response = client.Customers.CreateCustomer(create);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Não foi possivel criar o customer " +
                    $"{customer.Name}, devido ao erro: " + e.Message);
            }
            //Tenta Persistir os dados no banco
            try
            { 
                var saveCustomer = customer;
                saveCustomer.Id = response.Id;
                _context.Customers.Add(saveCustomer);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Foi criado o customer {response.Name.Split(" ")[0]} com o Id {response.Id}, mas não" +
                $" foi possivel salvar os dados do mesmo no banco de dados devido ao seguinte erro:" + e.Message); 
            } 
            return new CustomerResponse {Id= response.Id }; 
        }  
    }
}

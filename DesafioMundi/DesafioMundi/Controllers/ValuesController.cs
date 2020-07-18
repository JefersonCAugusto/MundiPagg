using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioMundi.Entities;
using Microsoft.AspNetCore.Mvc;
using MundiAPI.PCL;
using MundiAPI.PCL.Models;

namespace DesafioMundi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword); //conecta
            var response = client.Customers.GetCustomers();                             //faz requisição
            List<Customer> customer = new List<Customer>();
            foreach (var cus in response.Data.Select(x => x))
            {
                customer.Add(new Customer { Name = cus.Name, Email = cus.Email, Id = cus.Id, Document= cus.Document, Type=cus.Type});
            }
            return customer.ToList();

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(string id)
        {
           string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            string _basicAuthPassword = "";
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);
            var response = client.Customers.GetCustomer(id);


            return new Customer 
            { 
                Name =response.Name, Email =response.Email, Id =response.Id, Document = response.Document, Type =response.Type 
            };

            // return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Customer customer)
        {
            var teste = new Customer 
            {
                Name = customer.Name, Email = customer.Email, Id = customer.Id, Document = customer.Document, Type = customer.Type 
            };
            
            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DesafioMundi.Entities;
using DesafioMundi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MundiAPI.PCL;
using MundiAPI.PCL.Models;

namespace DesafioMundi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
       private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return _customerService.GetCustomer().ToList();

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(string id)
        {
            return _customerService.GetCustomer(id);
        }

        // POST api/values
        [HttpPost] 
        public ActionResult<string> Post([FromBody] Customer customer)
        {
            return _customerService.PostCustomer(customer);

        }

       

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Customer value)
        {
            
            throw new NotImplementedException();
            //_customerService.PutCustomer(id, value);
            //não consegui fazer funcionar o update. existe um 3º parâmetro no "UpdateCustomer" que não consegui entender.
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

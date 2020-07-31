using DesafioMundi.Entities;
using DesafioMundi.Entities.Response;
using DesafioMundi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return _customerService.GetCustomer().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> Get(string id)
        {
            return _customerService.GetCustomer(id);
        }

        [HttpPost] 
        public ActionResult<CustomerResponse> Post([FromBody] Customer customer)
        {
            return _customerService.PostCustomer(customer);
        }


    }
}

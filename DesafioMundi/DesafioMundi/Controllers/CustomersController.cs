using DesafioMundi.Entities;
using DesafioMundi.Entities.Response;
using DesafioMundi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
namespace DesafioMundi.Controllers
{
    [Produces("application/Json")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
 
        /// <summary>
        /// Obtem uma lista com todos os Customers
        /// </summary>
        /// <returns>Objetos Customers </returns>
      
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return _customerService.GetCustomer().ToList();
        }
     
        /// <summary>
        /// Obtem informações de um customer 
        /// </summary>
        /// <param name="id">Id do Customer</param>
        /// <returns>Objeto Customer  </returns>
    
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(string id)
        {
            return _customerService.GetCustomer(id);
        }
        /// <summary>
        /// Inclui um novo Customer
        /// </summary>
        /// <remarks>
        /// Exemplo de request: 
        ///     {
        ///         "Name": "Beto frigueredo", 
        ///         "Email": "beto.f@MeuEmail.com",
        ///         "Type": "individual",
        ///         "Gender": "male",
        ///         "Document": "33211455535" 
        ///     }
        /// </remarks>
        /// <param name="customer">Objeto Customer</param>
        /// <returns>Retorna o Id do novo Customer</returns>

         [HttpPost] 
        public ActionResult<CustomerResponse> Post([FromBody] Customer customer)
        {
            return _customerService.PostCustomer(customer);
        }


    }
}

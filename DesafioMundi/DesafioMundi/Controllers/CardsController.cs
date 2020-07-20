using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioMundi.Entities;
using DesafioMundi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MundiAPI.PCL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DesafioMundi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        // GET: api/<CardsController1>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //string _basicAuthUserName = "sk_test_alLk7EFV2iJ0dm9w";
            //string _basicAuthPassword = "";
            //var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);
            //var cards = client.Customers.GetCards(id);
            return new List<string> { "teste1", "testee2"};
        }

        // GET api/<CardsController1>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CardsController1>
        [HttpPost("{id}")]
        public ActionResult<string> Post(string id, [FromBody] CreditCard creditCard)
        {
            return _cardService.CreateCard(id, creditCard);
        }

        // PUT api/<CardsController1>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CardsController1>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

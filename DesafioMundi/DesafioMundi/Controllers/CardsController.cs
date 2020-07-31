using DesafioMundi.Entities;
using DesafioMundi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DesafioMundi.Entities.Response;
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

        [HttpGet("{idcustomer}/{idcard?}")]
        public ActionResult<IEnumerable<CreditCard>> Get(string idcustomer, string idcard)
        {
            if (string.IsNullOrEmpty(idcard))
                return _cardService.GetCards(idcustomer).ToList();
            return _cardService.GetCards(idcustomer,idcard).ToList();
        }

        [HttpPost("{id}")]
        public ActionResult<CreditCardResponse> Post(string id, [FromBody] CreditCard creditCard)
        {
            return _cardService.CreateCard(id, creditCard);
        }


    }
}

using DesafioMundi.Entities;
using DesafioMundi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DesafioMundi.Entities.Response;
using Microsoft.AspNetCore.Authorization;

namespace DesafioMundi.Controllers
{
    [Produces("application/Json")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Obtem os dados do cartão de crédito de um customer
        /// </summary> 
        /// <param name="idcustomer"> Id do Customer</param>
        /// <param name="idcard">Id do Cartão</param>
        /// <returns>Retorna um objeto com  dados do cartão de crédito</returns>
        [HttpGet("{idcustomer}/{idcard?}")]
        public ActionResult<IEnumerable<CreditCardResponse>> Get(string idcustomer, string idcard)
        {
            if (string.IsNullOrEmpty(idcard))
                return _cardService.GetCards(idcustomer).ToList();
            return _cardService.GetCards(idcustomer,idcard).ToList();
        }
        /// <summary>
        /// Cria um cartão de crédito para um cliente existente na base
        /// </summary>
        /// 
        /// <remarks> 
        /// Exemplo:
        ///		{
        ///     	"Number":"342793631858229",
        ///     	"CVV":"111",
        ///     	"ExpMonth":"11",
        ///     	"ExpYear":"30",
        ///     	"Brand":"visa"
        ///		}	
        /// 
        /// </remarks>
        /// <param name="id">Id do Customer</param>
        /// <param name="creditCard">Objeto Cartão de crédito</param>
        /// <returns>Retorna o Id do novo cartão</returns>
        [HttpPost("{id}")]
        public ActionResult<CreditCardResponse> Post(string id, [FromBody] CreditCard creditCard)
        {
            return _cardService.CreateCard(id, creditCard);
        }


    }
}

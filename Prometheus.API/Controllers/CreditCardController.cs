using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prometheus.DB.Entities;
using Prometheus.Service.CreditCard;

namespace Prometheus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardService creditCardService;
        public CreditCardController(ICreditCardService _creditCardService)
        {
            creditCardService = _creditCardService;
        }

        [HttpGet]
        public IActionResult GetCreditCards()
        {
            return Ok(creditCardService.GetCreditCards());
        }

        [HttpGet("{id}", Name = "GetCreditCard")]
        public IActionResult GetCreditCard(string id)
        {
            return Ok(creditCardService.GetCreditCard(id));
        }

        [HttpPost]
        public IActionResult AddCreditCard(CreditCard creditCard)
        {
            creditCardService.AddCreditCard(creditCard);
            //return Ok(creditCard);
            return CreatedAtRoute("GetCreditCard", new { id = creditCard.Id }, creditCard);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCreditCard(string id)
        {
            creditCardService.DeleteCreditCard(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateCreditCard(Prometheus.DB.Entities.CreditCard creditCard)
        {
            return Ok(creditCardService.UpdateCreditCard(creditCard));
        }
    }
}

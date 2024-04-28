using DTO.In;
using DTO.Out;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/requests")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketServices;

        public TicketController(ITicketService ticketServices)
        {
            _ticketServices = ticketServices;
        }

        [HttpPost]
        public IActionResult CreateTicket([FromBody] TicketCreateModel createTicketModel)
        {
            if (!IsValidCreateTicketModel(createTicketModel))
            {
                return BadRequest();
            }
            var newTicket = _ticketServices.CreateTicket(createTicketModel.ToEntity());
            var response = new TicketModel(newTicket);
            return Ok(response);
            
        }

        private bool IsValidCreateTicketModel(TicketCreateModel createTicketModel)
        {
            return createTicketModel != null && createTicketModel.Apartment != null && createTicketModel.Category != null && !string.IsNullOrEmpty(createTicketModel.Description);
        }

    }
}

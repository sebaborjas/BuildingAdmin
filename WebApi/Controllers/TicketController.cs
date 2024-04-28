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

            var ticket = createTicketModel.ToEntity();
            var createdTicket = _ticketServices.CreateTicket(ticket);
            var response = new TicketModel(createdTicket);
            return Ok(response);
        }

        [HttpGet("{category?}")]
        public IActionResult GetTickets(string category = null)
        {
            var tickets = _ticketServices.GetTickets(category);
            var response = new List<TicketModel>();
            foreach (var ticket in tickets)
            {
                response.Add(new TicketModel(ticket));
            }
            return Ok(response);
        }

        [HttpPut("{id}/assign")]
        public IActionResult AssignTicket(int id, [FromBody] int maintenanceOperatorId)
        {
            var ticket = _ticketServices.AssignTicket(id, maintenanceOperatorId);
            if (ticket == null)
            {
                return NotFound();
            }
            var response = new TicketModel(ticket);
            return Ok(response);
        }

        [HttpPut("{id}/start")]
        public IActionResult StartTicket(int id)
        {
            var ticket = _ticketServices.StartTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            var response = new TicketModel(ticket);
            return Ok(response);
        }

        [HttpPut("{id}/complete")]
        public IActionResult CompleteTicket(int id, [FromBody] float totalCost)
        {
            var ticket = _ticketServices.CompleteTicket(id, totalCost);
            if (ticket == null)
            {
                return NotFound();
            }
            var response = new TicketModel(ticket);
            return Ok(response);
        }

        private bool IsValidCreateTicketModel(TicketCreateModel createTicketModel)
        {

            if (createTicketModel == null)
            {
                return false;
            }

            if (createTicketModel.ApartmentId <= 0)
            {
                return false;
            }

            if (createTicketModel.CategoryId <= 0)
            {
                return false;
            }

            return true;
        }

    }
}

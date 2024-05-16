using DTO.In;
using DTO.Out;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Constants;

namespace WebApi.Controllers
{
    [Route("api/v1/tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketServices;

        public TicketController(ITicketService ticketServices)
        {
            _ticketServices = ticketServices;
        }

        [HttpPost]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
        public IActionResult CreateTicket([FromBody] TicketCreateModel createTicketModel)
        {
            var ticket = createTicketModel.ToEntity();
            var createdTicket = _ticketServices.CreateTicket(ticket);
            var response = new TicketModel(createdTicket);
            return Ok(response);
        }

        [HttpGet("{category?}")]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
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

        [HttpGet("assigned-tickets")]
        [AuthenticationFilter(Role = RoleConstants.MaintenanceOperatorRole)]
        public IActionResult GetAssignedTickets()
        {
            var tickets = _ticketServices.GetAssignedTickets();
            var response = new List<TicketModel>();
            foreach (var ticket in tickets)
            {
                response.Add(new TicketModel(ticket));
            }
            return Ok(response);
        }

        [HttpPut("{id}/assign")]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
        public IActionResult AssignTicket(int id, [FromBody] int maintenanceOperatorId)
        {
            var ticket = _ticketServices.AssignTicket(id, maintenanceOperatorId);
            var response = new TicketModel(ticket);
            return Ok(response);
        }

        [HttpPut("{id}/start")]
        [AuthenticationFilter(Role = RoleConstants.MaintenanceOperatorRole)]
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
        [AuthenticationFilter(Role = RoleConstants.MaintenanceOperatorRole)]
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

    }
}

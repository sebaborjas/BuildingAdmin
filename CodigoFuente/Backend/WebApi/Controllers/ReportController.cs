using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Constants;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/v2/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportServices _reportServices;

        public ReportController(IReportServices reportServices)
        {
            _reportServices = reportServices;
        }

        [HttpGet("buildings")]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
        public IActionResult GetTicketsByBuilding([FromQuery]string building = null)
        {
            if (building != null)
            {
                return Ok(_reportServices.GetTicketsByBuilding(building));
            }

            return Ok(_reportServices.GetTicketsByBuilding());
        }

        [HttpGet("operators")]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]  
        public IActionResult GetTicketsByMaintenanceOperator([FromQuery] string building, [FromQuery] string name = null)
        {
            if (name != null)
            {
                return Ok(_reportServices.GetTicketsByMaintenanceOperator(building, name));
            }
            
            return Ok(_reportServices.GetTicketsByMaintenanceOperator(building));
        }

        [HttpGet("categories")]
        [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
        public IActionResult GetTicketsByCategory([FromQuery] string building ,[FromQuery] string? category= null)
        {
            if (category != null)
            {
                return Ok(_reportServices.GetTicketsByCategory(building, category));
            }
            
            return Ok(_reportServices.GetTicketsByCategory(building));
        }

        [HttpGet("tickets-by-apartment")]
        public IActionResult GetTicketsByApartment([FromQuery] string building)
        {
            return Ok(_reportServices.GetTicketsByApartment(building));
        }
    }
}

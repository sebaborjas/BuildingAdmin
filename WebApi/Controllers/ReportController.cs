using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Constants;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/v1/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportServices _reportServices;

        public ReportController(IReportServices reportServices)
        {
            _reportServices = reportServices;
        }

        [HttpGet("tickets-by-building")]
        [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
        public IActionResult GetTicketsByBuilding([FromBody] string? name = null)
        {
            if (name != null)
            {
                return Ok(_reportServices.GetTicketsByBuilding(name));
            }

            return Ok(_reportServices.GetTicketsByBuilding());
        }

        [HttpGet("tickets-by-maintenance-operator")]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]  
        public IActionResult GetTicketsByMaintenanceOperator([FromBody] string buildingName ,string? operatorName= null)
        {
            if (operatorName != null)
            {
                return Ok(_reportServices.GetTicketsByMaintenanceOperator(buildingName, operatorName));
            }
            
            return Ok(_reportServices.GetTicketsByMaintenanceOperator(buildingName));
        }

        [HttpGet("tickets-by-category")]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
        public IActionResult GetTicketsByCategory([FromBody] string buildingName ,string? categoryName= null)
        {
            if (categoryName != null)
            {
                return Ok(_reportServices.GetTicketsByCategory(buildingName, categoryName));
            }
            
            return Ok(_reportServices.GetTicketsByCategory(buildingName));
        }
    }
}

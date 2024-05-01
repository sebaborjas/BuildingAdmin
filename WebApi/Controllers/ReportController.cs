using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("tickets-by-building/{name?}")]
        public IActionResult GetTicketsByBuilding(string? name = null)
        {
            if (name != null)
            {
                return Ok(_reportServices.GetTicketsByBuilding(name));
            }

            return Ok(_reportServices.GetTicketsByBuilding());
        }

        [HttpGet("tickets-by-maintenance-operator/{id?}")]
        public IActionResult GetTicketsByMaintenanceOperator(string buildingName ,string? operatorName= null)
        {
            if (operatorName != null)
            {
                return Ok(_reportServices.GetTicketsByMaintenanceOperator(buildingName, operatorName));
            }
            
            return Ok(_reportServices.GetTicketsByMaintenanceOperator(buildingName));
        }

        [HttpGet("tickets-by-category")]
        public IActionResult GetTicketsByCategory(string buildingName ,string? categoryName= null)
        {
            if (categoryName != null)
            {
                return Ok(_reportServices.GetTicketsByCategory(buildingName, categoryName));
            }
            
            return Ok(_reportServices.GetTicketsByCategory(buildingName));
        }
    }
}

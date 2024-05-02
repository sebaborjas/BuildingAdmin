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

        [HttpGet("tickets-by-building/{buildingName}")]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
        public IActionResult GetTicketsByBuilding(string? buildingName = null)
        {
            if (buildingName != null)
            {
                return Ok(_reportServices.GetTicketsByBuilding(buildingName));
            }

            return Ok(_reportServices.GetTicketsByBuilding());
        }

        [HttpGet("tickets-by-maintenance-operator/{buildingName}/{operatorName?}")]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]  
        public IActionResult GetTicketsByMaintenanceOperator(string buildingName , string? operatorName= null)
        {
            if (operatorName != null)
            {
                return Ok(_reportServices.GetTicketsByMaintenanceOperator(buildingName, operatorName));
            }
            
            return Ok(_reportServices.GetTicketsByMaintenanceOperator(buildingName));
        }

        [HttpGet("tickets-by-category/{buildingName}/{categoryName?}")]
        [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
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

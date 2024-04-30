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

        [HttpGet("tickets-by-building/{id?}")]
        public IActionResult GetTicketsByBuilding(int? id = null)
        {
            if (id.HasValue)
            {
                return Ok(_reportServices.GetTicketsByBuilding<string, Object>(id));
            }
            return Ok(_reportServices.GetTicketsByBuilding<string, Object>());
        }

        [HttpGet("tickets-by-maintenance-operator/{id?}")]
        public IActionResult GetTicketsByMaintenanceOperator(int? id = null)
        {
            if (id.HasValue)
            {
                return Ok(_reportServices.GetTicketsByMaintenanceOperator<string, Object>(id));
            }
            return Ok(_reportServices.GetTicketsByMaintenanceOperator<string, Object>());
        }

        [HttpGet("tickets-by-category")]
        public IActionResult GetTicketsByCategory()
        {
            return Ok(_reportServices.GetTicketsByCategory<string, Object>());
        }
    }
}

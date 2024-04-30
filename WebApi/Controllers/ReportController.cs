using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportServices _reportServices;

        public ReportController(IReportServices reportServices)
        {
            _reportServices = reportServices;
        }

        [HttpGet("requests-by-building/{id?}")]
        public IActionResult GetTicketsByBuilding(int? id = null)
        {
            if (id.HasValue)
            {
                return Ok(_reportServices.GetTicketsByBuilding<string, Object>(id));
            }
            return Ok(_reportServices.GetTicketsByBuilding<string, Object>());
        }

        [HttpGet("requests-by-maintenance-operator/{id?}")]
        public IActionResult GetTicketsByMaintenanceOperator(int? id = null)
        {
            return Ok(_reportServices.GetTicketsByMaintenanceOperator<string, Object>(id));
        }
    }
}

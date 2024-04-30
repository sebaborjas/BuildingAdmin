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
        public IActionResult GetRequestsByBuilding(int? id = null)
        {
            if (id.HasValue)
            {
                return BadRequest(_reportServices.GetRequestsByBuilding<string, Object>(id));
            }
            return Ok(_reportServices.GetRequestsByBuilding<string, Object>());
        }
    }
}

using DTO.In;
using DTO.Out;
using IImporter;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using WebApi.Constants;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v2/importers")]
    public class ImporterController : ControllerBase
    {
        private readonly IImportService _importService;
        private readonly IBuildingService _buildingService;

        public ImporterController(IImportService importService, IBuildingService buildingService)
        {
            _importService = importService;
            _buildingService = buildingService;
        }

        [HttpGet]
        [AuthenticationFilter(Role = RoleConstants.CompanyAdministratorRole)]
        public IActionResult GetAvailableImporters()
        {
            List<ImporterInterface> availableImporters = _importService.GetAllImporters();
            List<string> importerNames = availableImporters.Select(importer => importer.GetName()).ToList();
            return Ok(importerNames);

        }

        [HttpPost]
        [AuthenticationFilter(Role = RoleConstants.CompanyAdministratorRole)]
        public IActionResult ImportBuildings([FromBody] ImporterInput importerInput)
        {
            var buildings = _importService.ImportBuildings(importerInput.ImporterName, importerInput.Path);
            return Ok(buildings);
        }
    }
}

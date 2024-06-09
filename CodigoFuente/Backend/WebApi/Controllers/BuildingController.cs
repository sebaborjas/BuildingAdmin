using DTO.In;
using DTO.Out;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Constants;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/v2/buildings")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingServices;

        public BuildingController(IBuildingService buildingServices)
        {
            _buildingServices = buildingServices;
        }

        [HttpPost]
        [AuthenticationFilter(Role = RoleConstants.CompanyAdministratorRole)]
        public IActionResult CreateBuilding([FromBody] CreateBuildingInput createBuildingInput)
        {
            var newBuilding = _buildingServices.CreateBuilding(createBuildingInput.ToEntity());
            var response = new CreateBuildingOutput(newBuilding);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [AuthenticationFilter(Role = RoleConstants.CompanyAdministratorRole)]
        public IActionResult DeleteBuilding(int id)
        {
            _buildingServices.DeleteBuilding(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [AuthenticationFilter(Role = RoleConstants.CompanyAdministratorRole)]
        public IActionResult ModifyBuilding(int id, [FromBody] ModifyBuildingInput modifyBuildingInput)
        {
            _buildingServices.ModifyBuilding(id, modifyBuildingInput.ToEntity());
            return Ok();
        }

        [HttpGet]
        [AuthenticationFilter(Role = RoleConstants.CompanyAdministratorRole + "," + RoleConstants.ManagerRole +  "," + RoleConstants.AdministratorRole)]
        public IActionResult Get([FromQuery] int? id)
        {
           var buildings = _buildingServices.Get(id.Value);
            var response = new List<GetBuildingOutput>();
            buildings.ForEach(building =>
            {
                var managerName = _buildingServices.GetManagerName(building.Id);
                response.Add(new GetBuildingOutput(building, managerName));
            });
            return Ok(response);
        }

        [HttpPut("{buildingId}/manager")]
        [AuthenticationFilter(Role = RoleConstants.CompanyAdministratorRole)]
        public IActionResult ChangeBuildingManager(int buildingId, [FromBody] ChangeBuildingManagerInput changeBuildingManagerInput)
        {
            _buildingServices.ChangeBuildingManager(buildingId, changeBuildingManagerInput.ManagerId);
            return Ok();
        }
    }
}

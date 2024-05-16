using DTO.In;
using DTO.Out;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Constants;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/v1/buildings")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingServices;

        public BuildingController(IBuildingService buildingServices)
        {
            _buildingServices = buildingServices;
        }

        [HttpPost]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
        public IActionResult CreateBuilding([FromBody] CreateBuildingInput createBuildingInput)
        {
            var newBuilding = _buildingServices.CreateBuilding(createBuildingInput.ToEntity());
            var response = new CreateBuildingOutput(newBuilding);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
        public IActionResult DeleteBuilding(int id)
        {
            _buildingServices.DeleteBuilding(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
        public IActionResult ModifyBuilding(int id, [FromBody] ModifyBuildingInput modifyBuildingInput)
        {
            try
            {
                _buildingServices.ModifyBuilding(id, modifyBuildingInput.ToEntity());
                return Ok();
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
        public IActionResult GetAll()
        {
            var buildings = _buildingServices.GetAllBuildingsForUser();
            var response = new List<GetBuildingOutput>();
            buildings.ForEach(building =>
            {
                response.Add(new GetBuildingOutput(building));
            });
            return Ok(response);
        }

        [HttpGet("{id}")]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
        public IActionResult Get(int id)
        {
            var building = _buildingServices.Get(id);
            if (building == null)
            {
                return NotFound("Building not found");
            }
            return Ok(new GetBuildingOutput(building));
        }
    }
}

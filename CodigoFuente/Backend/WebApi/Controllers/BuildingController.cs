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
        [AuthenticationFilter(Role =RoleConstants.ManagerRole)]
        public IActionResult CreateBuilding([FromBody] CreateBuildingInput createBuildingInput)
        {
            if (!IsValidCreateBuildingInput(createBuildingInput) || !AreValidApartments(createBuildingInput.Apartments))
            {
                return BadRequest();
            }
            var newBuilding = _buildingServices.CreateBuilding(createBuildingInput.ToEntity());
            var response = new CreateBuildingOutput(newBuilding);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
        public IActionResult DeleteBuilding(int id)
        {
            try
            {
                _buildingServices.DeleteBuilding(id);
                return Ok();
            } catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
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
            catch(KeyNotFoundException exception)
            {
                return NotFound();
            } 
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        [HttpGet]
        [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
        public IActionResult Get([FromQuery] int? id)
        {
            if (id == null)
            {
                var buildings = _buildingServices.GetAllBuildingsForUser();
                var response = new List<GetBuildingOutput>();
                buildings.ForEach(building =>
                {
                    response.Add(new GetBuildingOutput(building));
                });
                return Ok(response);
            }
            else
            {
                var building = _buildingServices.Get(id.Value);
                if (building == null)
                {
                    return NotFound("Building not found");
                }
                return Ok(new GetBuildingOutput(building));
            }
            
        }

        private bool IsValidCreateBuildingInput(CreateBuildingInput createBuildingInput)
        {
            return createBuildingInput != null && !string.IsNullOrWhiteSpace(createBuildingInput.Name) && !string.IsNullOrWhiteSpace(createBuildingInput.Address) &&
                !string.IsNullOrWhiteSpace(createBuildingInput.Location) && !string.IsNullOrWhiteSpace(createBuildingInput.ConstructionCompany) && createBuildingInput.Expenses >= 0;
        }


        private bool AreValidApartments(List<NewApartmentInput> apartments)
        {
            try
            {
                apartments.ForEach(apartment =>
                {
                    apartment.ToEntity();
                });
                return true;
            } catch (Exception ex)
            {
                return false;
            }

        }
    }
}

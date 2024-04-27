using DTO.In;
using DTO.Out;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/buildings")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingServices _buildingServices;

        public BuildingController(IBuildingServices buildingServices)
        {
            _buildingServices = buildingServices;
        }

        [HttpPost]
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
        public IActionResult DeleteBuilding(int id)
        {
            _buildingServices.DeleteBuilding(id);
            return Ok();
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

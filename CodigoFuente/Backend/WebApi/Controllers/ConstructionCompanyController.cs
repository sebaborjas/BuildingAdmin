using Microsoft.AspNetCore.Mvc;
using IServices;
using DTO.In;
using DTO.Out;
using WebApi.Filters;
using WebApi.Constants;

namespace WebApi.Controllers
{
    [Route("api/v2/constructionCompanies")]
    [ApiController]
    public class ConstructionCompanyController : ControllerBase
    {
        private readonly IConstructionCompanyService _service;

        public ConstructionCompanyController(IConstructionCompanyService service)
        {
            _service = service;
        }

        [HttpPost]
        [AuthenticationFilter(Role = RoleConstants.CompanyAdministratorRole)]
        public IActionResult CreateConstructionCompany([FromBody] CreateConstructionCompanyInput createConstructionCompanyInput)
        {
            var newConstructionCompany = _service.CreateConstructionCompany(createConstructionCompanyInput.Name);
            return Ok(new ConstructionCompanyOutput(newConstructionCompany));
        }

        [HttpPut]
        [AuthenticationFilter(Role = RoleConstants.CompanyAdministratorRole)]
        public IActionResult ModifyConstructionCompany([FromBody] ModifyConstructionCompanyInput modifyConstructionCompanyInput)
        {
            var modifyConstructionCompany = _service.ModifyConstructionCompany(modifyConstructionCompanyInput.Name);
            return Ok(new ConstructionCompanyOutput(modifyConstructionCompany));
        }

        public IActionResult GetUserCompany()
        {
            var userCompany = _service.GetUserCompany();
            return Ok(new ConstructionCompanyOutput(userCompany));
        }

    }
}

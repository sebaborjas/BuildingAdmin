using Microsoft.AspNetCore.Mvc;
using Domain;
using IServices;
using DTO.In;
using DTO.Out;
using WebApi.Filters;
using WebApi.Constants;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v2/users")]
public class UserController : ControllerBase
{
  private readonly IUserServices _service;

  public UserController(IUserServices service)
  {
    _service = service;
  }

  [HttpPost("administrator")]
  [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
  public IActionResult CreateAdministrator([FromBody] AdministratorCreateInput newAdministrator)
  {
    AdministratorOutput administratorModel = new AdministratorOutput(_service.CreateAdministrator(newAdministrator.ToEntity()));
    return Ok(administratorModel);
  }

  [HttpPost("maintenance-operator")]
  [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
  public IActionResult CreateMaintenanceOperator([FromBody] MaintenanceOperatorCreateInput newMaintenanceOperator)
  {
    MaintenanceOperatorOutput maintenanceOperator = new MaintenanceOperatorOutput(_service.CreateMaintenanceOperator(newMaintenanceOperator.ToEntity()));
    return Ok(maintenanceOperator);
  }

  [HttpDelete("manager/{id}")]
  [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
  public IActionResult DeleteManager(int id)
  {
    _service.DeleteManager(id);
    return Ok("Se eliminó con éxito el encargado del sistema.");
  }

    [HttpPost("company-administrator")]
    [AuthenticationFilter(Role = RoleConstants.CompanyAdministratorRole)]
    public IActionResult CreateCompanyAdministrator([FromBody] CompanyAdministratorCreateInput newCompanyAdministrator)
    {
        CompanyAdministratorOutput companyAdministrator = new CompanyAdministratorOutput(_service.CreateCompanyAdministrator(newCompanyAdministrator.ToEntity()));
        return Ok(companyAdministrator);
    }
}
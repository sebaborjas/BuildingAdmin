using Microsoft.AspNetCore.Mvc;
using Domain;
using IServices;
using DTO.In;
using DTO.Out;
using WebApi.Filters;
using WebApi.Constants;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
  private readonly IUserServices _service;

  public UserController(IUserServices service)
  {
    _service = service;
  }

  [HttpPost("administrator")]
  [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
  public IActionResult CreateAdministrator([FromBody] AdministratorCreateModel newAdministrator)
  {
    AdministratorModel administratorModel = new AdministratorModel(_service.CreateAdministrator(newAdministrator.ToEntity()));
    return Ok(administratorModel);
  }

  [HttpPost("maintenance-operator")]
  [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
  public IActionResult CreateMaintenanceOperator([FromBody] MaintenanceOperatorCreateModel newMaintenanceOperator)
  {
    MaintenanceOperatorModel maintenanceOperator = new MaintenanceOperatorModel(_service.CreateMaintenanceOperator(newMaintenanceOperator.ToEntity()));
    return Ok(maintenanceOperator);
  }

  [HttpDelete("manager/{id}")]
  [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
  public IActionResult DeleteManager(int id)
  {
    _service.DeleteManager(id);
    return Ok("Se eliminó con éxito el encargado del sistema.");
  }
}
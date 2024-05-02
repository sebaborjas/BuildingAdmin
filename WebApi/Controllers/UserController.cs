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
    if(!IsNewAdministratorValid(newAdministrator))
    {
      return BadRequest("La solcitud no es válida.");
    }
    AdministratorModel administratorModel = new AdministratorModel(_service.CreateAdministrator(newAdministrator.ToEntity()));
    
    return Ok(administratorModel);
  }

  private bool IsNewAdministratorValid(AdministratorCreateModel administrator)
  {
    return administrator != null && 
    administrator.Name != null && 
    administrator.LastName != null && 
    administrator.Email != null &&
    administrator.Password != null;
  }

  [HttpPost("maintenance-operator")]
  [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
  public IActionResult CreateMaintenanceOperator([FromBody] MaintenanceOperatorCreateModel newMaintenanceOperator)
  {
    if(!IsNewMaintenanceOperatorValid(newMaintenanceOperator))
    {
      return BadRequest("La solcitud no es válida.");
    }
    MaintenanceOperatorModel maintenanceOperator = new MaintenanceOperatorModel(_service.CreateMaintenanceOperator(newMaintenanceOperator.ToEntity()));
    
    return Ok(maintenanceOperator);
  }

  private bool IsNewMaintenanceOperatorValid(MaintenanceOperatorCreateModel maintenanceOperator)
  {
    return maintenanceOperator != null && 
    maintenanceOperator.Name != null && 
    maintenanceOperator.LastName != null && 
    maintenanceOperator.Email != null &&
    maintenanceOperator.Password != null &&
    maintenanceOperator.BuildingId > 0;
  }

  [HttpDelete("manager/{id}")]
  [AuthenticationFilter(Role = RoleConstants.ManagerRole)]
  public IActionResult DeleteManager(int id)
  {
    try
    {
      _service.DeleteManager(id);
      return Ok("Se eliminó con éxito el encargado del sistema.");
    }
    catch (ArgumentOutOfRangeException)
    {
      return NotFound("No se encontró el encargado especificado.");
    }
  }
}
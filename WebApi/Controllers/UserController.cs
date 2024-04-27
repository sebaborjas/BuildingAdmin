using Microsoft.AspNetCore.Mvc;
using Domain;
using IServices;
using DTO.In;
using DTO.Out;

namespace WebApi;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
  private readonly IUserServices _service;

  public UserController(IUserServices service)
  {
    _service = service;
  }

  [HttpPost("administrator")]
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
  public IActionResult CreateMaintenanceOperator([FromBody] MaintenanceOperatorCreateModel newMaintenanceOperator)
  {
    if(newMaintenanceOperator == null || newMaintenanceOperator.Name == null || newMaintenanceOperator.LastName == null || newMaintenanceOperator.Email == null || newMaintenanceOperator.Password == null || newMaintenanceOperator.Building == null)
    {
      return BadRequest();
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
    maintenanceOperator.Building != null;
  }

  [HttpDelete("manager/{id}")]
  public IActionResult DeleteManager([FromBody] int id)
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
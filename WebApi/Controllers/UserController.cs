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
    if(newAdministrator == null  || newAdministrator.Name == null || newAdministrator.LastName == null || newAdministrator.Email == null)
    {
      return BadRequest();
    }
    AdministratorModel administrator = new AdministratorModel(_service.CreateAdministrator(newAdministrator.ToEntity()));
    
    return Ok(administrator);
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

  [HttpDelete("manager/{id}")]
  public IActionResult DeleteManager([FromBody] int id)
  {
    return BadRequest();
  }
}
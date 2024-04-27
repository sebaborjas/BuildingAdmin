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
    if (newAdministrator == null)
    {
      return BadRequest();
    }
    if (newAdministrator.Name == null)
    {
      return BadRequest();
    }
    AdministratorModel administrator = new AdministratorModel(_service.CreateAdministrator(newAdministrator.ToEntity()));
    
    return Ok(administrator);
  }
}
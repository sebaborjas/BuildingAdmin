using Microsoft.AspNetCore.Mvc;
using Domain;
using IServices;
using DTO.In;
using DTO.Out;

namespace WebApi;

[ApiController]
[Route("api/admin/invitations")]
public class AdministratorController : ControllerBase
{
  private readonly IService<Administrator> _service;

  public AdministratorController(IService<Administrator> service)
  {
    _service = service;
  }

  [HttpPost]
  public IActionResult Create([FromBody] AdministratorCreateModel newAdministrator)
  {
    var administrator = _service.Create(newAdministrator.ToEntity());
    
    return Ok(new AdministratorModel(administrator));
  }
}
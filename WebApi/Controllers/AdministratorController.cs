using Microsoft.AspNetCore.Mvc;
using Domain;
using IServices;

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
  public IActionResult Create(Administrator entity)
  {
    return Ok();
  }
}
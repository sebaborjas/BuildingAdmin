using DTO.Out;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ISessionService _sessionService;
        public LoginController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var session = _sessionService.Login(email, password);
            var response = new SuccessfulLoginOutput(session);
            return Ok(response);
        }

    }
}

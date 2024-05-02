using DTO.In;
using DTO.Out;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/v1/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ISessionService _sessionService;
        public LoginController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginInput loginInput)
        {
            try
            {
                var session = _sessionService.Login(loginInput.Email, loginInput.Password);
                var response = new SuccessfulLoginOutput(session);
                return Ok(response);
            } catch (Exception exception)
            {
                return Unauthorized(exception.Message);
            }
            
        }

    }
}

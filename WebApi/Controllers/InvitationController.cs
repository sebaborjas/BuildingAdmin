using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO.In;

namespace WebApi.Controllers
{
    [Route("api/invitations")]
    [ApiController]
    public class InvitationController : ControllerBase
    {

        private IInvitationServices _invitationServices;
        
        public InvitationController(IInvitationServices invitationServices)
        {
            _invitationServices = invitationServices;
        }

        public IActionResult CreateInvitation(CreateInvitationInput newInvitationInput)
        {
            return Ok();
        }
    }
}

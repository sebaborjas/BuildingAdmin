using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO.In;
using DTO.Out;

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

        [HttpPost]
        public IActionResult CreateInvitation([FromBody]CreateInvitationInput newInvitationInput)
        {
            if(!IsValidCreateInvitationInput(newInvitationInput))
            {
                return BadRequest();
            }
            var newInvitation = _invitationServices.CreateInvitation(newInvitationInput.ToEntity());
            var response = new CreateInvitationOutput(newInvitation);
            return Ok(response);
        }

        public IActionResult DeleteInvitation(int id)
        {
            try
            {
                _invitationServices.DeleteInvitation(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult ModifyInvitation(int id, [FromBody]ModifyInvitationInput modifyInvitationInput)
        {
            return Ok();
        }

        private bool IsValidCreateInvitationInput(CreateInvitationInput newInvitationInput)
        {
            return newInvitationInput != null && !string.IsNullOrWhiteSpace(newInvitationInput.Email) && !string.IsNullOrWhiteSpace(newInvitationInput.Name) && newInvitationInput.ExpirationDate > DateTime.Now;
        }
    }
}

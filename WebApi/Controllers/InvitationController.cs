using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO.In;
using DTO.Out;
using WebApi.Constants;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/v1/invitations")]
    [ApiController]
    public class InvitationController : ControllerBase
    {

        private IInvitationServices _invitationServices;
        
        public InvitationController(IInvitationServices invitationServices)
        {
            _invitationServices = invitationServices;
        }

        [HttpPost]
        [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
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

        [HttpDelete("{id}")]
        [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
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
        [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
        public IActionResult ModifyInvitation(int id, [FromBody]ModifyInvitationInput modifyInvitationInput)
        {
            if(!IsValidModifyInvitationInput(modifyInvitationInput))
            {
                return BadRequest();
            }
            try
            {
                _invitationServices.ModifyInvitation(id, modifyInvitationInput.ExpirationDate);
                return Ok();
            } catch (Exception exception)
            {
                return NotFound();
            }
        }

        [HttpPut("accept")]
        public IActionResult AcceptInvitation(AcceptInvitationInput acceptInvitationInput)
        {
            if(!IsValidAcceptInvitationInput(acceptInvitationInput))
            {
                return BadRequest();
            }
            try
            {
                var newManager = _invitationServices.AcceptInvitation(acceptInvitationInput.ToEntity());
                var result = new AcceptInvitationOutput(newManager);
                return Ok(result);
            } catch (Exception exception)
            {
                return NotFound();
            }
        }

        [HttpPut("reject")]
        public IActionResult RejectInvitation(RejectInvitationInput rejectInvitationInput)
        {
            if (!IsValidRejectInvitationInput(rejectInvitationInput))
            {
                return BadRequest();
            }
            try
            {
                _invitationServices.RejectInvitation(rejectInvitationInput.Email);
                return Ok();
            }
            catch (Exception exception)
            {
                return NotFound();
            }
            
        }

        private bool IsValidCreateInvitationInput(CreateInvitationInput newInvitationInput)
        {
            return newInvitationInput != null && !string.IsNullOrWhiteSpace(newInvitationInput.Email) && !string.IsNullOrWhiteSpace(newInvitationInput.Name) && newInvitationInput.ExpirationDate > DateTime.Now;
        }

        private bool IsValidModifyInvitationInput(ModifyInvitationInput modifyInvitationInput)
        {
            return modifyInvitationInput != null && modifyInvitationInput.ExpirationDate > DateTime.Now;
        }

        private bool IsValidAcceptInvitationInput(AcceptInvitationInput acceptInvitationInput)
        {
            return acceptInvitationInput != null && !string.IsNullOrWhiteSpace(acceptInvitationInput.Email) && !string.IsNullOrWhiteSpace(acceptInvitationInput.Password);
        }

        private bool IsValidRejectInvitationInput(RejectInvitationInput rejectInvitationInput)
        {
            return rejectInvitationInput != null && !string.IsNullOrWhiteSpace(rejectInvitationInput.Email);
        }
    }
}

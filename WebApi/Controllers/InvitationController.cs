using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO.In;
using DTO.Out;
using WebApi.Constants;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/v1/invitation")]
    [ApiController]
    public class InvitationController : ControllerBase
    {

        private IInvitationService _invitationService;
        
        public InvitationController(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }

        [HttpPost]
        [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
        public IActionResult CreateInvitation([FromBody]CreateInvitationInput newInvitationInput)
        {
            if(!IsValidCreateInvitationInput(newInvitationInput))
            {
                return BadRequest("Invalid input");
            }
            var newInvitation = _invitationService.CreateInvitation(newInvitationInput.ToEntity());
            var response = new CreateInvitationOutput(newInvitation);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
        public IActionResult DeleteInvitation(int id)
        {
            try
            {
                _invitationService.DeleteInvitation(id);
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
                _invitationService.ModifyInvitation(id, modifyInvitationInput.ExpirationDate);
                return Ok();
            } catch (Exception exception)
            {
                return NotFound(exception.Message);
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
                var newManager = _invitationService.AcceptInvitation(acceptInvitationInput.ToEntity(), acceptInvitationInput.Password);
                var result = new AcceptInvitationOutput(newManager);
                return Ok(result);
            } catch (Exception exception)
            {
                return NotFound(exception.Message);
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
                _invitationService.RejectInvitation(rejectInvitationInput.Email);
                return Ok();
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
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

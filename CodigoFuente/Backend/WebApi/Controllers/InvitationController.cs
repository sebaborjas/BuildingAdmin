using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO.In;
using DTO.Out;
using WebApi.Constants;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/v2/invitations")]
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
        public IActionResult CreateInvitation([FromBody] CreateInvitationInput newInvitationInput)
        {
            var newInvitation = _invitationService.CreateInvitation(newInvitationInput.ToEntity());
            var response = new CreateInvitationOutput(newInvitation);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
        public IActionResult DeleteInvitation(int id)
        {
            _invitationService.DeleteInvitation(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
        public IActionResult ModifyInvitation(int id, [FromBody] ModifyInvitationInput modifyInvitationInput)
        {
            _invitationService.ModifyInvitation(id, modifyInvitationInput.ExpirationDate);
            return Ok();
        }

        [HttpPut("accept")]
        public IActionResult AcceptInvitation(AcceptInvitationInput acceptInvitationInput)
        {
            var newManager = _invitationService.AcceptInvitation(acceptInvitationInput.ToEntity(), acceptInvitationInput.Password);
            var result = new AcceptInvitationOutput(newManager);
            return Ok(result);
        }

        [HttpPut("reject")]
        public IActionResult RejectInvitation(RejectInvitationInput rejectInvitationInput)
        {
            _invitationService.RejectInvitation(rejectInvitationInput.Email);
            return Ok();
        }

        [HttpGet]
        [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
        public IActionResult Get([FromQuery] int? id)
        {
            if (id == null)
            {
                var invitations = _invitationService.GetAllInvitations();
                var response = new List<GetInvitationOutput>();
                invitations.ForEach(invitation =>
                {
                    response.Add(new GetInvitationOutput(invitation));
                });
                return Ok(response);
            }
            else
            {
                var invitation = _invitationService.GetInvitation(id.Value);
                return Ok(new GetInvitationOutput(invitation));
            }
        }
    }
}

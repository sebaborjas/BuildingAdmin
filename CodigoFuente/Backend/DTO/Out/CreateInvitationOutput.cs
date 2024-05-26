using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Out
{
    public class CreateInvitationOutput
    {
        public CreateInvitationOutput(Invitation invitation)
        {
            InvitationId = invitation.Id;
        }
        public int InvitationId {  get; set; }
    }
}

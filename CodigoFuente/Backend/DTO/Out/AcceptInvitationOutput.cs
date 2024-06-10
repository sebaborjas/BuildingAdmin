using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Out
{
    public class AcceptInvitationOutput
    {
        public AcceptInvitationOutput(User user)
        {
            UserId = user.Id;
        }
        public int UserId {  get; set; }
    }
}

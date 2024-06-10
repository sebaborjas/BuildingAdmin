using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.In
{
    public class AcceptInvitationInput
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Invitation ToEntity()
        {
            return new Invitation
            {
                Email = Email
            };
        }
    }
}

using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Out
{
    public class GetInvitationOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }

        public GetInvitationOutput(Invitation invitation)
        {
            Id = invitation.Id;
            Name = invitation.Name;
            Email = invitation.Email;
            ExpirationDate = invitation.ExpirationDate;
            Role = invitation.Role.ToString();
            Status = invitation.Status.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is GetInvitationOutput output &&
                   Id == output.Id &&
                   Name == output.Name &&
                   Email == output.Email &&
                   ExpirationDate == output.ExpirationDate &&
                   Role == output.Role &&
                   Status == output.Status;
        }
    }
}

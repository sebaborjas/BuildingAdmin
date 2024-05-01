using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DTO.In
{
    public class CreateInvitationInput
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Invitation ToEntity()
        {
            return new Invitation()
            {
                Email = Email,
                ExpirationDate = ExpirationDate,
                Status = Domain.DataTypes.InvitationStatus.Pending,
                Name = Name
            };
        }
    }
}

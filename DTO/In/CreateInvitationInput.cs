using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.In
{
    public class CreateInvitationInput
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}

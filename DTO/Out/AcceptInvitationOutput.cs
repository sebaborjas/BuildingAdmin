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
        public AcceptInvitationOutput(Manager manager)
        {
            ManagerId = manager.Id;
        }
        public int ManagerId {  get; set; }
    }
}

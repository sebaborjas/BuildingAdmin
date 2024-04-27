using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace IServices
{
    public interface IInvitationServices
    {
        Invitation CreateInvitation(Invitation newInvitation);
    }
}

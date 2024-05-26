using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace IServices
{
    public interface IInvitationService
    {
        Invitation CreateInvitation(Invitation newInvitation);

        void DeleteInvitation(int invitationId);

        void ModifyInvitation(int invitationId, DateTime newExpirationDate);

        Manager AcceptInvitation(Invitation invitation, string Password);

        void RejectInvitation(string email);
    }
}

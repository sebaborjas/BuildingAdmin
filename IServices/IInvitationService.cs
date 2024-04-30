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

        void DeleteInvitation(int id);

        void ModifyInvitation(int id, DateTime newExpirationDate);

        Manager AcceptInvitation(Invitation invitation);

        void RejectInvitation(string email);
    }
}

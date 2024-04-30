using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServices;
using IDataAcess;
using Domain;

namespace Services
{
    public class InvitationService : IInvitationService
    {
        public readonly IGenericRepository<Invitation> _invitationRepository;

        public InvitationService(IGenericRepository<Invitation> invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public Invitation CreateInvitation(Invitation newInvitation)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteInvitation(int id)
        {
            throw new System.NotImplementedException();
        }

        public void ModifyInvitation(int id, DateTime newExpirationDate)
        {
            throw new System.NotImplementedException();
        }

        public Manager AcceptInvitation(Invitation invitation)
        {
            throw new System.NotImplementedException();
        }

        public void RejectInvitation(string email)
        {
            throw new System.NotImplementedException();
        }

    }
}

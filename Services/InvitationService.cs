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
        private IGenericRepository<Invitation> _invitationRepository;

        public InvitationService(IGenericRepository<Invitation> invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public Invitation CreateInvitation(Invitation newInvitation)
        {
            Invitation invitationAlreadyExist = _invitationRepository.GetByCondition(i => i.Email == newInvitation.Email);

            if (invitationAlreadyExist != null)
            {
                throw new ArgumentException("Invitation already exist");
            }

            _invitationRepository.Insert(newInvitation);
            return newInvitation;
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

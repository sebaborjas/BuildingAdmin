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
            Invitation invitation = _invitationRepository.Get(id);
            _invitationRepository.Delete(invitation);
        }

        public void ModifyInvitation(int id, DateTime newExpirationDate)
        {
            Invitation invitation = _invitationRepository.Get(id);
            invitation.ExpirationDate = newExpirationDate;
            _invitationRepository.Update(invitation);
        }

        public Manager AcceptInvitation(Invitation invitation)
        {
            Invitation invitationToAccept = _invitationRepository.GetByCondition(i => i.Email == invitation.Email);
            _invitationRepository.Update(invitationToAccept);
            return new Manager();
        }

        public void RejectInvitation(string email)
        {
            throw new System.NotImplementedException();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServices;
using IDataAccess;
using Domain;
using Domain.DataTypes;

namespace Services
{
    public class InvitationService : IInvitationService
    {
        private IGenericRepository<Invitation> _invitationRepository;
        private IGenericRepository<Administrator> _adminRepository;
        private IGenericRepository<Manager> _managerRepository;

        public InvitationService(IGenericRepository<Invitation> invitationRepository, IGenericRepository<Administrator> adminRepository, IGenericRepository<Manager> managerRepository)
        { 
            _invitationRepository = invitationRepository;
            _adminRepository = adminRepository;
            _managerRepository = managerRepository;
        }

        public Invitation CreateInvitation(Invitation newInvitation)
        {
            User userAlreadyExist = _adminRepository.GetByCondition(u => u.Email == newInvitation.Email);

            if (userAlreadyExist != null)
            {
                throw new ArgumentException("User already exist");
            }

            Invitation invitationAlreadyExist = _invitationRepository.GetByCondition(i => i.Email == newInvitation.Email);

            if (invitationAlreadyExist != null)
            {
                throw new ArgumentException("Invitation already exist");
            }

            _invitationRepository.Insert(newInvitation);
            return newInvitation;
        }

        public void DeleteInvitation(int invitationId)
        {
            Invitation invitation = _invitationRepository.Get(invitationId);
            if (invitation.Status != Domain.DataTypes.InvitationStatus.Accepted)
            {
                _invitationRepository.Delete(invitation);
            }
            else
            {
                throw new ArgumentException("Invitation can not be deleted");
            }
        }

        public void ModifyInvitation(int invitationId, DateTime newExpirationDate)
        {
            Invitation invitation = _invitationRepository.Get(invitationId);

            if (invitation.Status != Domain.DataTypes.InvitationStatus.Accepted)
            {
                if (invitation.ExpirationDate <= DateTime.Now || invitation.ExpirationDate <= DateTime.Now.AddDays(1))
                {
                    invitation.ExpirationDate = newExpirationDate;
                    _invitationRepository.Update(invitation);
                }
                else
                {
                    throw new ArgumentException("Invitation can not be modified");
                }
            }
            else
            {
                throw new InvalidOperationException("Invitation has already been accepted and cannot be modified");
            }
        }

        public Manager AcceptInvitation(Invitation invitation, string Password)
        {
            Invitation invitationToAccept = _invitationRepository.GetByCondition(i => i.Email == invitation.Email);
            if (invitationToAccept != null)
            {
                if (invitationToAccept.Status == InvitationStatus.Accepted)
                {
                    throw new InvalidOperationException("Invitation has already been accepted");
                }

                Manager manager = new Manager
                {
                    Email = invitation.Email,
                    Password = Password,
                    Name = invitationToAccept.Name, 
                    LastName = ""
                };

                _managerRepository.Insert(manager);

                invitationToAccept.Status = InvitationStatus.Accepted;
                _invitationRepository.Update(invitationToAccept);
                return manager;
            }
            else
            {
                throw new ArgumentException("Invitation does not exist");
            }
        }

        public void RejectInvitation(string email)
        {
            Invitation invitationToReject = _invitationRepository.GetByCondition(i => i.Email == email);
            if (invitationToReject != null)
            {
                _invitationRepository.Delete(invitationToReject);
            }
            else
            {
                throw new ArgumentException("Invitation does not exist");
            }
        }

    }
}

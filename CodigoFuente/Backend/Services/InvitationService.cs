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
        private IGenericRepository<CompanyAdministrator> _companyAdministratorRepository;
        private ISessionService _sessionService;

        public InvitationService(IGenericRepository<Invitation> invitationRepository, IGenericRepository<Administrator> adminRepository, IGenericRepository<Manager> managerRepository, IGenericRepository<CompanyAdministrator> companyAdministratorRepository, ISessionService sessionService)
        {
            _invitationRepository = invitationRepository;
            _adminRepository = adminRepository;
            _managerRepository = managerRepository;
            _companyAdministratorRepository = companyAdministratorRepository;
            _sessionService = sessionService;
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

            if (!IsValidCreateInvitation(newInvitation))
            {
                throw new ArgumentException("Invalid invitation");
            }

            try
            {
                _invitationRepository.Insert(newInvitation);
                return newInvitation;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error creating invitation");
            }

        }

        public void DeleteInvitation(int invitationId)
        {
            try
            {
                Invitation invitation = _invitationRepository.Get(invitationId);
                _invitationRepository.Delete(invitation);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error deleting invitation");
            }
        }

        public void ModifyInvitation(int invitationId, DateTime newExpirationDate)
        {

            Invitation modifyInvitation = _invitationRepository.Get(invitationId);

            if (modifyInvitation == null)
            {
                throw new ArgumentException("Invitation does not exist");
            }

            if (modifyInvitation.Status == InvitationStatus.Accepted)
            {
                throw new InvalidOperationException("Invitation has already been accepted and cannot be modified");
            }

            if (modifyInvitation.ExpirationDate >= DateTime.Now || modifyInvitation.ExpirationDate >= DateTime.Now.AddDays(1))
            {
                throw new ArgumentException("Invitation can not be modified");
            }
            try
            {

                modifyInvitation.ExpirationDate = newExpirationDate;
                _invitationRepository.Update(modifyInvitation);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error modifying invitation");
            }
        }

        public User AcceptInvitation(Invitation invitation, string Password)
        {

            Invitation invitationToAccept = _invitationRepository.GetByCondition(i => i.Email == invitation.Email);
            if (invitationToAccept == null)
            {
                throw new ArgumentException("Invitation does not exist");
            }

            if (invitationToAccept.Status == InvitationStatus.Accepted)
            {
                throw new InvalidOperationException("Invitation has already been accepted");
            }

            if (invitationToAccept.ExpirationDate < DateTime.Now)
            {
                throw new InvalidOperationException("Invitation has expired");
            }
            try
            {
                User userToAdd = null;
                if (invitationToAccept.Role == InvitationRoles.Manager)
                {
                    userToAdd = new Manager
                    {
                        Email = invitationToAccept.Email,
                        Name = invitationToAccept.Name,
                        Password = Password
                    };

                    _managerRepository.Insert((Manager)userToAdd);
                }
                else if (invitationToAccept.Role == InvitationRoles.ConstructionCompanyAdministrator)
                {
                    userToAdd = new CompanyAdministrator
                    {
                        Email = invitationToAccept.Email,
                        Name = invitationToAccept.Name,
                        Password = Password
                    };
                    _companyAdministratorRepository.Insert((CompanyAdministrator)userToAdd);
                }

                _invitationRepository.Delete(invitationToAccept);
                return userToAdd;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error accepting invitation", e);
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

        public Invitation GetInvitation(int invitationId)
        {
            var currentUser = _sessionService.GetCurrentUser() as Administrator;
            if (currentUser == null)
            {
                throw new InvalidOperationException("Current user is not an administrator");
            }

            var invitation = _invitationRepository.GetByCondition(i => i.Id == invitationId);
            if (invitation == null)
            {
                throw new ArgumentException("Invitation does not exist");
            }

            return invitation;
        }

        public List<Invitation> GetAllInvitations()
        {
            var currentUser = _sessionService.GetCurrentUser() as Administrator;
            if (currentUser == null)
            {
                throw new InvalidOperationException("Current user is not an administrator");
            }

            var invitations = _invitationRepository.GetAll<Invitation>()?.ToList();
            if (invitations == null)
            {
                throw new InvalidOperationException("No invitations found");
            }
            return invitations;

        }

        private bool IsValidCreateInvitation(Invitation invitation)
        {
            return invitation != null && !string.IsNullOrWhiteSpace(invitation.Email) && !string.IsNullOrWhiteSpace(invitation.Name) && invitation.ExpirationDate > DateTime.Now;
        }

    }
}

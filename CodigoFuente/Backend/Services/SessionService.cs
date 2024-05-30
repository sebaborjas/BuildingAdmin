using Domain;
using IDataAccess;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SessionService : ISessionService
    {
        private ISessionRepository _sessionRepository;
        private IGenericRepository<Manager> _managerRepository;
        private IGenericRepository<Administrator> _adminRepository;
        private IGenericRepository<MaintenanceOperator> _maintenanceOperatorRepository;
        private IGenericRepository<CompanyAdministrator> _companyAdministratorRepository;

        private User? _currentUser;

        public SessionService(ISessionRepository sessionRepository, IGenericRepository<Manager> managerRepository, IGenericRepository<Administrator> adminRepository, IGenericRepository<MaintenanceOperator> maintenanceOperatorRepository, IGenericRepository<CompanyAdministrator> companyAdministrator)
        {
            _sessionRepository = sessionRepository;
            _managerRepository = managerRepository;
            _adminRepository = adminRepository;
            _maintenanceOperatorRepository = maintenanceOperatorRepository;
            _companyAdministratorRepository = companyAdministrator;
        }

        public User? GetCurrentUser(Guid? token = null)
        {
            if (token == null) return _currentUser;
            var session = _sessionRepository.GetByToken(token.Value);
            if (session != null) _currentUser = session.User;
            return _currentUser;
        }

        public Session Login(string email, string password)
        {
            try
            {
                User user = null;
                user = _adminRepository.GetByCondition(user => user.Email == email);
                if (user == null)
                {
                    user = _managerRepository.GetByCondition(user => user.Email == email);
                }
                if (user == null)
                {
                    user = _maintenanceOperatorRepository.GetByCondition(user => user.Email == email);
                }
                if (user == null)
                {
                    user = _companyAdministratorRepository.GetByCondition(user => user.Email == email);
                }
                if (user == null || user.Password != password)
                {
                    throw new InvalidDataException();
                }

                var newSession = new Session()
                {
                    User = user
                };

                _sessionRepository.Insert(newSession);
                return newSession;
            }
            catch (InvalidDataException)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}

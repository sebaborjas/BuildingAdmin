using Domain;
using IDataAccess;
using IDataAcess;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SessionService: ISessionService
    {
        private ISessionRepository _sessionRepository;
        
        private User? _currentUser;

        public SessionService(ISessionRepository repository) { 
            _sessionRepository = repository;
        }

        public User? GetCurrentUser(Guid? token = null)
        {
            if(token == null) return _currentUser;
            var session = _sessionRepository.GetByToken(token.Value);
            if(session != null) _currentUser = session.User;
            return _currentUser;
        }

        public Session Login(string email, string password)
        {
            return null;
        }
    }
}

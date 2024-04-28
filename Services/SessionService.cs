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

        public SessionService(ISessionRepository repository) { 
            _sessionRepository = repository;
        }

        public User? GetCurrentUser(Guid? token = null)
        {
            var session = _sessionRepository.GetByToken(token.Value);
            if(session != null)
            {
                return session.User;
            }
            return null;
        }
    }
}

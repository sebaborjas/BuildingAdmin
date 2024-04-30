using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SessionRepository: GenericRepository<Session>, ISessionRepository
    {
        public SessionRepository(DbContext context) { 
            Context = context;
        }

        public Session? GetByToken(Guid token)
        {
            var result = Context.Set<Session>().Where(session => session.Token == token).FirstOrDefault();
            return result;
        }
    }
}

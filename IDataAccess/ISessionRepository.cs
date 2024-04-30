using Domain;
using IDataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataAccess
{
    public interface ISessionRepository: IGenericRepository<Session>
    {
        Session? GetByToken(Guid token);
    }
}

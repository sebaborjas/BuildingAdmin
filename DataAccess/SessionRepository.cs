using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SessionRepository: GenericRepository<Session>
    {
        public SessionRepository(DbContext context) { 
            Context = context;
        }
    }
}

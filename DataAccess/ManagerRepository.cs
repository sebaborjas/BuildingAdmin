using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace DataAccess
{
    public class ManagerRepository : GenericRepository<Manager>
    {
        public ManagerRepository(DbContext context)
        {
            Context = context;
        }
    }
}

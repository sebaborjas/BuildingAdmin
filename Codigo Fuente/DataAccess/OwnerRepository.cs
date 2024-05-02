using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class OwnerRepository : GenericRepository<Owner>
    {
        public OwnerRepository(DbContext context)
        {
            Context = context;
        }
    }
}

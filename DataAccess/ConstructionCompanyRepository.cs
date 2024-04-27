using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ConstructionCompanyRepository : GenericRepository<ConstructionCompany>
    {
        public ConstructionCompanyRepository(DbContext context)
        {
            Context = context;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace DataAccess
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(DbContext context) {
            
            Context = context;
        }
    }
}

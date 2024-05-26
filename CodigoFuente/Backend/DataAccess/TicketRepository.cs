using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TicketRepository: GenericRepository<Ticket>
    {
        public TicketRepository(DbContext context)
        {
            Context = context;
        }
    }
}

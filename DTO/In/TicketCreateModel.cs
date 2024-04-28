using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.In
{
    public class TicketCreateModel
    {
        public string Description { get; set; }
        public Apartment Apartment { get; set; }
        public Category Category { get; set; }

        public Ticket ToEntity()
        {
            return new Ticket()
            {
                Description = Description,
                Apartment = Apartment,
                Category = Category
            };
        }

    }
}

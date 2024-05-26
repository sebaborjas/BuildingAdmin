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
        public int ApartmentId { get; set; }
        public int CategoryId { get; set; }

        public Ticket ToEntity()
        {
            return new Ticket()
            {
                Description = Description,
                Apartment = new Apartment() { Id = ApartmentId },
                Category = new Category() { Id = CategoryId }
            };
        }
    }
}

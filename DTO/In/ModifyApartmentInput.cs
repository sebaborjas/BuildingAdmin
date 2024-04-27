using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DTO.In
{
    public class ModifyApartmentInput
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerEmail{ get; set; }

        public Apartment ToEntity()
        {
            var owner = new Owner()
            {
                Email = OwnerEmail,
                Name = OwnerName,
                LastName = OwnerLastName
            };
            return new Apartment()
            {
                Owner = owner,
                Id = Id
            };
        }
    }
}

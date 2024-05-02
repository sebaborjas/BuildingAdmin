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
        public string? OwnerName { get; set; }
        public string? OwnerLastName { get; set; }
        public string? OwnerEmail{ get; set; }

        public Apartment ToEntity()
        {
            var newOwner = new Owner();

            if(this.OwnerEmail != null)
            {
                newOwner.Email = this.OwnerEmail;
            }
            if(this.OwnerName != null)
            {
                newOwner.Name = this.OwnerName;
            }
            if(this.OwnerLastName != null)
            {
                newOwner.LastName = this.OwnerLastName;
            }
            return new Apartment()
            {
                Owner = newOwner,
                Id = Id
            };
        }
    }
}

using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.In
{
    public class NewApartmentInput
    {
        public short Floor { get; set; }
        public short DoorNumber { get; set; }
        public string OwnerName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerEmail { get; set; }
        public short Rooms { get; set; }
        public short Bathrooms { get; set; }
        public bool HasTerrace { get; set; }

        public Apartment ToEntity()
        {
            var owner = new Owner()
            {
                Email = OwnerEmail,
                LastName = OwnerLastName,
                Name = OwnerName
            };

            return new Apartment()
            {
                Bathrooms = Bathrooms,
                DoorNumber = DoorNumber,
                Floor = Floor,
                HasTerrace = HasTerrace,
                Owner = owner,
                Rooms = Rooms
            };
        }
    }
}

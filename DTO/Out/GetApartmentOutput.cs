using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Out
{
    public class GetApartmentOutput
    {
        public int Id { get; set; }
        public int DoorNumber { get; set; }
        public short Rooms { get; set; }
        public short Bathrooms { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerEmail { get; set; }

        public GetApartmentOutput(Apartment apartment)
        {
            this.Id = apartment.Id;
            this.DoorNumber = apartment.DoorNumber;
            this.Rooms = apartment.Rooms;
            this.Bathrooms = apartment.Bathrooms;
            this.OwnerId = apartment.Owner.Id;
            this.OwnerName = apartment.Owner.Name;
            this.OwnerLastName = apartment.Owner.LastName;
            this.OwnerEmail = apartment.Owner.Email;
        }
    }
}

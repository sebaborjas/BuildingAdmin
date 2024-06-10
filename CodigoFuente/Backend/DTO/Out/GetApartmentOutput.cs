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
        public int Floor { get; set; }
        public bool HasTerrace { get; set; }

        public GetApartmentOutput(Apartment apartment)
        {
            Id = apartment.Id;
            DoorNumber = apartment.DoorNumber;
            Rooms = apartment.Rooms;
            Bathrooms = apartment.Bathrooms;
            OwnerId = apartment.Owner.Id;
            OwnerName = apartment.Owner.Name;
            OwnerLastName = apartment.Owner.LastName;
            OwnerEmail = apartment.Owner.Email;
            HasTerrace = apartment.HasTerrace;
            Floor = apartment.Floor;
        }
    }
}

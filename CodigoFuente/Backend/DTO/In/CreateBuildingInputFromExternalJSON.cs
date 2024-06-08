using Domain;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DTO.In
{
    public class CreateBuildingInputFromExternalJSON
    {
        public CreateBuildingInputFromExternalJSON()
        {
            Apartments = new List<ApartmentInput>();
        }

        [JsonPropertyName("nombre")]
        public string Name { get; set; }

        [JsonPropertyName("direccion")]
        public AddressInput Address { get; set; }

        [JsonPropertyName("gps")]
        public GpsInput Gps { get; set; }

        [JsonPropertyName("gastos_comunes")]
        public float Expenses { get; set; }

        [JsonPropertyName("encargado")]
        public string ManagerEmail { get; set; }

        [JsonPropertyName("departamentos")]
        public List<ApartmentInput> Apartments { get; set; }
    }

    public class AddressInput
    {
        [JsonPropertyName("calle_principal")]
        public string Street { get; set; }

        [JsonPropertyName("numero_puerta")]
        public int Number { get; set; }

        [JsonPropertyName("calle_secundaria")]
        public string SecondaryStreet { get; set; }
    }

    public class GpsInput
    {
        [JsonPropertyName("latitud")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitud")]
        public double Longitude { get; set; }
    }

    public class ApartmentInput
    {
        [JsonPropertyName("piso")]
        public int Floor { get; set; }

        [JsonPropertyName("numero_puerta")]
        public int DoorNumber { get; set; }

        [JsonPropertyName("habitaciones")]
        public int Rooms { get; set; }

        [JsonPropertyName("conTerraza")]
        public bool HasTerrace { get; set; }

        [JsonPropertyName("baños")]
        public int Bathrooms { get; set; }

        [JsonPropertyName("propietarioEmail")]
        public string OwnerEmail { get; set; }

        public Apartment ToEntity()
        {
            return new Apartment()
            {
                Bathrooms = (short)Bathrooms,
                DoorNumber = DoorNumber,
                Floor = (short)Floor,
                HasTerrace = HasTerrace,
                Rooms = (short)Rooms,
                Owner = new Owner()
                {
                    Email = OwnerEmail
                }
            };
        }
    }
}

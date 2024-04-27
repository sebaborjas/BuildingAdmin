using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.In
{
    public class CreateBuildingInput
    {
        public CreateBuildingInput()
        {
            Apartments = new List<NewApartmentInput>();
        }

        public string Name {  get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ConstructionCompany { get; set; }
        public float Expenses { get; set; }
        public List<NewApartmentInput> Apartments { get; set; }

    }
}

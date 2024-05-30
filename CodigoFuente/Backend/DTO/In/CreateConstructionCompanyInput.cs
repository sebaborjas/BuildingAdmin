using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DTO.In
{
    public class CreateConstructionCompanyInput
    {
        public string Name { get; set; }

        public CreateConstructionCompanyInput ToEntity()
        {
            return new CreateConstructionCompanyInput()
            {
                Name = Name
            };
        }
    }
}

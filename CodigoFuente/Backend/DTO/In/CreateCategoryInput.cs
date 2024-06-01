using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DTO.In
{
    public class CreateCategoryInput
    {
        public string Name { get; set; }

        public Category ToEntity()
        {
            return new Category()
            {
                Name = Name
            };
        }
    }
}

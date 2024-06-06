using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DTO.Out
{
    public class ConstructionCompanyOutput
    {
        public ConstructionCompanyOutput(ConstructionCompany constructionCompany)
        {
            Id = constructionCompany.Id;
            Name = constructionCompany.Name;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            ConstructionCompanyOutput constructionCompanyOutput = (ConstructionCompanyOutput)obj;
            return Id == constructionCompanyOutput.Id;
        }
    }
}

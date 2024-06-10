using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Out
{
    public class GetCategoryOutput
    {
        public GetCategoryOutput(Category category) {
            Id = category.Id;
            Name = category.Name;
            ParentId = category.RelatedTo?.Id;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is GetCategoryOutput)
            {
                var cat = (GetCategoryOutput)obj;
                if(cat.Id == Id && cat.Name == Name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DTO.Out
{
    public class CategoryModel
    {
        public CategoryModel(Category category)
        {
            Id = category.Id;
        }

        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            CategoryModel categoryModel = (CategoryModel)obj;
            return Id == categoryModel.Id;
        }
    }
}

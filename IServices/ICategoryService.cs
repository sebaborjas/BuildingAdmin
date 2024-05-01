using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICategoryService
    {
        public Category CreateCategory(string name);

        public List<Category> GetAll();

        public Category Get(int id);
    }   
}

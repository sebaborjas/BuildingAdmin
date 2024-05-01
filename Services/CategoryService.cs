using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServices;
using IDataAccess;
using Domain;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        public IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category CreateCategory(string name)
        {
            Category category = new Category();
            category.Name = name;
            _categoryRepository.Insert(category);
            return category;
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll<Category>().ToList();
        }

        public Category Get(int id)
        {
            return _categoryRepository.Get(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServices;
using IDataAcess;
using Domain;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        public readonly IGenericRepository<Category> _categoryRepository;

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
    }
}

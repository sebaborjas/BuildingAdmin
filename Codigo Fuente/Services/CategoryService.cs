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
            try
            {
                if (!IsValidCreateCategory(name))
                {
                    throw new ArgumentException("Invalid category");
                }
                var category = new Category
                {
                    Name = name
                };
                _categoryRepository.Insert(category);
                return category;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error creating category");
            }
        }

        public List<Category> GetAll()
        {
            try
            {
                List<Category> categories = _categoryRepository.GetAll<Category>().ToList();
                if (categories.Count == 0)
                {
                    throw new ArgumentException("Categories not found");
                }
                return categories;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error getting categories");
            }
        }

        public Category Get(int id)
        {
            try
            {
                Category category = _categoryRepository.Get(id);
                if (category == null)
                {
                    throw new ArgumentException("Category not found");
                }
                return category;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error getting category");
            }
        }

        private bool IsValidCreateCategory(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            return true;
        }
    }
}

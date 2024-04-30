using Microsoft.AspNetCore.Mvc;
using IServices;
using DTO.In;
using DTO.Out;

namespace WebApi
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CreateCategoryModel createCategoryModel)
        {
            if (!IsValidCreateCategoryInput(createCategoryModel))
            {
                return BadRequest();
            }
            var category = _service.CreateCategory(createCategoryModel.Name);
            return Ok(new CategoryModel(category));
        }

        private bool IsValidCreateCategoryInput(CreateCategoryModel createCategoryModel)
        {
            return createCategoryModel != null && !string.IsNullOrEmpty(createCategoryModel.Name);
        }
    }
}

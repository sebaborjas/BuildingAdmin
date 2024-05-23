using Microsoft.AspNetCore.Mvc;
using IServices;
using DTO.In;
using DTO.Out;
using WebApi.Filters;
using WebApi.Constants;

namespace WebApi.Controllers
{
    [Route("api/v2/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
        public IActionResult CreateCategory([FromBody] CreateCategoryModel createCategoryModel)
        {
            if (!IsValidCreateCategoryInput(createCategoryModel))
            {
                return BadRequest();
            }
            var category = _service.CreateCategory(createCategoryModel.Name);
            return Ok(new CategoryModel(category));
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int? id)
        {
            if(id == null)
            {
                var categories = _service.GetAll();
                List<GetCategoryOutput> response = new List<GetCategoryOutput>();
                categories.ForEach(category =>
                {
                    response.Add(new GetCategoryOutput(category));
                });
                return Ok(response);
            }
            else 
            {
                var category = _service.Get(id.Value);
                if(category == null)
                {
                    return NotFound("Category not found");
                }
                return Ok(new GetCategoryOutput(category));
            }
            
        }

        private bool IsValidCreateCategoryInput(CreateCategoryModel createCategoryModel)
        {
            return createCategoryModel != null && !string.IsNullOrEmpty(createCategoryModel.Name);
        }
    }
}

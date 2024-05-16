using Microsoft.AspNetCore.Mvc;
using IServices;
using DTO.In;
using DTO.Out;
using WebApi.Filters;
using WebApi.Constants;

namespace WebApi.Controllers
{
    [Route("api/v1/categories")]
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
            var category = _service.CreateCategory(createCategoryModel.Name);
            return Ok(new CategoryModel(category));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _service.GetAll();
            List<GetCategoryOutput> response = new List<GetCategoryOutput>();
            categories.ForEach(category =>
            {
                response.Add(new GetCategoryOutput(category));
            });
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _service.Get(id);
            if(category == null)
            {
                return NotFound("Category not found");
            }
            return Ok(new GetCategoryOutput(category));
        }
    }
}

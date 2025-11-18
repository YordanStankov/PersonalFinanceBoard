using FinanceDashboard.Application.DTOs.Category;
using FinanceDashboard.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDashboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) 
            {
                _categoryService = categoryService;
            }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("CategoryController is working!");
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO dto)
        {
            var result = await _categoryService.CreateAsync(dto);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("List")]
        public async Task<IActionResult> ListCategoriesOfUserAsync([FromBody]string userId)
        {
            var categories = await _categoryService.GetAllOfOneUserAsync(userId);
            return Ok(categories);
        }
    }
}

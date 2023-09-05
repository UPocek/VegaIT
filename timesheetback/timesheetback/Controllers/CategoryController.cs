using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using timesheetback.Services;
using timesheetback.DTOs;

namespace timesheetback.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
	{

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
		{
            _categoryService = categoryService;
		}

        [HttpGet("all")]
        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            return await _categoryService.GetAllCategoriesAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CreateCategoryCredentialsDTO CategoryCredentials)
        {
            try
            {
                return await _categoryService.CreateCategoryAsync(CategoryCredentials);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(long id, CreateCategoryCredentialsDTO CategoryCredentials)
        {
            try
            {
                return await _categoryService.UpdateCategoryAsync(id, CategoryCredentials);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}


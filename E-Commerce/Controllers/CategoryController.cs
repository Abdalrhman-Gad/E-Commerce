using Application.Interfaces;
using Domain.DTOs.Category;
using Domain.Exceptions.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
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

        // ===============================
        // GET: api/Category
        // ===============================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCategoryDTO>>> GetAll([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            try
            {
                var categories = await _categoryService.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse("An error occurred while retrieving categories.", ex.Message));
            }
        }

        // ===============================
        // GET: api/Category/{id}
        // ===============================
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDTO>> GetById(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                return Ok(category);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse("An error occurred while retrieving the category.", ex.Message));
            }
        }

        // ===============================
        // POST: api/Category
        // ===============================
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<GetCategoryDTO>> AddCategory([FromBody] AddCategoryDTO addCategoryDto)
        {
            try
            {
                if (addCategoryDto == null)
                    return BadRequest(new ErrorResponse("Invalid category data."));

                var createdCategory = await _categoryService.AddAsync(addCategoryDto);
                return CreatedAtAction(nameof(GetById), new { id = createdCategory.CategoryId }, createdCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse("An error occurred while adding the category.", ex.Message));
            }
        }

        // ===============================
        // PUT: api/Category/{id}
        // ===============================
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<GetCategoryDTO>> UpdateCategory(int id, [FromBody] AddCategoryDTO updateCategoryDto)
        {
            try
            {
                if (updateCategoryDto == null)
                    return BadRequest(new ErrorResponse("Invalid category data."));

                var updatedCategory = await _categoryService.UpdateAsync(id, updateCategoryDto);
                return Ok(updatedCategory);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse("An error occurred while updating the category.", ex.Message));
            }
        }

        // ===============================
        // DELETE: api/Category/{id}
        // ===============================
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse("An error occurred while deleting the category.", ex.Message));
            }
        }
    }
}

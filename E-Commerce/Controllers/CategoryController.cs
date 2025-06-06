﻿using Application.Interfaces;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCategoryDTO>>> GetAll([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var categories = await _categoryService.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
            return Ok(categories);
        }

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
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<GetCategoryDTO>> AddCategory([FromBody] AddCategoryDTO addCategoryDto)
        {
            if (addCategoryDto == null)
                return BadRequest("Invalid category data.");

            var createdCategory = await _categoryService.AddAsync(addCategoryDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.CategoryId }, createdCategory);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<GetCategoryDTO>> UpdateCategory(int id, [FromBody] AddCategoryDTO updateCategoryDto)
        {
            if (updateCategoryDto == null)
                return BadRequest("Invalid category data.");

            try
            {
                var updatedCategory = await _categoryService.UpdateAsync(id, updateCategoryDto);
                return Ok(updatedCategory);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

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
                return NotFound(ex.Message);
            }
        }
    }
}

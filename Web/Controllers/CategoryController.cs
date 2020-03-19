using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Domain.DTO;
using Domain.Interfaces;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger _logger;

        public CategoryController(ICategoryService categoryService, ILogger logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategories()
        {
            try
            {
                var categories = _categoryService.GetAllCategories();
                if (categories == null) throw new ArgumentNullException(nameof(categories));
                _logger.LogInformation("User got categories information");
                return Ok(categories);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while user tried to get blogs info");
                throw;
            }
        }
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                var categories = _categoryService.GetCategoryById(id);
                if (categories == null) throw new ArgumentNullException(nameof(categories));
                _logger.LogInformation("User got category information");
                return Ok(categories);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while user tried to get category info");
                throw;
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Administrator,Moderator")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO category)
        {
            try
            {
                var result = await _categoryService.CreateCategory(category);
                if (result != null)
                {
                    _logger.LogInformation("Category created");
                    return CreatedAtAction(nameof(GetCategoryById), new { id = result.Id }, result);
                }
                else throw new ArgumentNullException();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while user tried to create category");
                throw;
            }
        }
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Administrator,Moderator")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while user tried to delete category");
                throw;
            }
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Administrator,Moderator")]
        public IActionResult UpdateCategory([FromBody]CategoryDTO category)
        {
            try
            {
                _categoryService.UpdateCategory(category);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while user tried to update category");
                throw;
            }
        }
    }
}
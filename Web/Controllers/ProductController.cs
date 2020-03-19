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
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        IActionResult GetProducts()
        {
            try
            {
                var products = _productService.GetAllProducts();
                if (products == null) throw new ArgumentNullException(nameof(products));
                _logger.LogInformation("User successfuly got products information");
                return Ok(products);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while user tried to get products info");
                throw;
            }
        }
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var product = _productService.GetProductById(id);
                if (product == null) throw new ArgumentNullException(nameof(product));
                _logger.LogInformation("User got category information");
                return Ok(product);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while user tried to get product info");
                throw;
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO product)
        {
            try
            {
                var result = await _productService.CreateProduct(product);
                if (result != null)
                {
                    _logger.LogInformation("Product created");
                    return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
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
                _logger.LogError(ex, "Error occurred while user tried to create product");
                throw;
            }
        }
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator,Moderator")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while user tried to delete product");
                throw;
            }
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator,Moderator")]
        public IActionResult UpdateProduct([FromBody]ProductDTO product)
        {
            try
            {
                _productService.UpdateProduct(product);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while user tried to delete product");
                throw;
            }
        }
    }
}
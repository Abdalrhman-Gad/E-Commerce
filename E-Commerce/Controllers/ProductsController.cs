using Application.DTOs.Image;
using Application.Interfaces;
using Domain.DTOs.Product;
using Domain.Exceptions.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;

        public ProductController(IProductService productService, IImageService imageService)
        {
            _productService = productService;
            _imageService = imageService;
        }

        // ===============================
        // GET: api/Product
        // ===============================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductDTO>>> GetAll([FromQuery] string? categoryName, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            try
            {
                IEnumerable<GetProductDTO> products;

                if (!string.IsNullOrEmpty(categoryName))
                {
                    products = await _productService.GetByCategoryNameAsync(categoryName, pageSize: pageSize, pageNumber: pageNumber);
                }
                else
                {
                    products = await _productService.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("An error occurred while retrieving products.", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        // ===============================
        // GET: api/Product/{id}
        // ===============================
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDTO>> GetById(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                return Ok(product);
            }
            catch (ProductNotFoundException ex)
            {
                var errorResponse = new ErrorResponse(ex.Message);
                return NotFound(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("An error occurred while retrieving the product.", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        // ===============================
        // POST: api/Product
        // ===============================
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<GetProductDTO>> AddProduct([FromBody] AddProductDTO addProductDto)
        {
            try
            {
                if (addProductDto == null)
                {
                    var errorResponse = new ErrorResponse("Invalid product data.");
                    return BadRequest(errorResponse);
                }

                var createdProduct = await _productService.AddAsync(addProductDto);
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.ProductId }, createdProduct);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("An error occurred while adding the product.", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        // ===============================
        // PUT: api/Product/{id}
        // ===============================
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<GetProductDTO>> UpdateProduct(int id, [FromBody] AddProductDTO updateProductDto)
        {
            try
            {
                if (updateProductDto == null)
                {
                    var errorResponse = new ErrorResponse("Invalid product data.");
                    return BadRequest(errorResponse);
                }

                var updatedProduct = await _productService.UpdateAsync(id, updateProductDto);
                return Ok(updatedProduct);
            }
            catch (ProductNotFoundException ex)
            {
                var errorResponse = new ErrorResponse(ex.Message);
                return NotFound(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("An error occurred while updating the product.", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        // ===============================
        // DELETE: api/Product/{id}
        // ===============================
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return NoContent();
            }
            catch (ProductNotFoundException ex)
            {
                var errorResponse = new ErrorResponse(ex.Message);
                return NotFound(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("An error occurred while deleting the product.", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        // ===============================
        // POST: api/Product/{productId}/upload-image
        // ===============================
        [Authorize(Roles = "Admin")]
        [HttpPost("{productId}/upload-image")]
        public async Task<IActionResult> UploadProductImage(int productId, [FromForm] ImageUploadRequestDTO request)
        {
            try
            {
                _imageService.ValidateImage(request);

                await _productService.UploadProductImageAsync(productId, request);

                return Ok(new { Message = "Image uploaded successfully." });
            }
            catch (ProductNotFoundException ex)
            {
                var errorResponse = new ErrorResponse(ex.Message);
                return NotFound(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("An error occurred while uploading the image.", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }
    }
}
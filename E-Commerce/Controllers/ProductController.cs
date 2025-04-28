using Domain.DTOs.Image;
using Application.Interfaces;
using Domain.DTOs.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductDTO>>> Get([FromQuery] int? categoryId, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            IEnumerable<GetProductDTO> products;

            if (categoryId.HasValue)
            {
                products = await _productService.GetAllAsync(
                    filter: p => p.CategoryId == categoryId,
                    pageSize: pageSize,
                    pageNumber: pageNumber
                );
            }
            else
            {
                products = await _productService.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
            }

            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDTO>> Get(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            
            return Ok(product);
        }

        // POST: api/Product
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<GetProductDTO>> Post([FromBody] AddProductDTO addProduct)
        {
            var createdProduct = await _productService.AddAsync(addProduct);
            
            return CreatedAtAction(nameof(Get), new { id = createdProduct.ProductId }, createdProduct);
        }

        // PUT: api/Product/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<GetProductDTO>> Put(int id, [FromBody] AddProductDTO updateProduct)
        {
            var updatedProduct = await _productService.UpdateAsync(id, updateProduct);
            
            return Ok(updatedProduct);
        }

        // DELETE: api/Product/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            
            return NoContent();
        }

        // POST: api/Product/5/upload-image
        [Authorize(Roles = "admin")]
        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadImage(int id, [FromForm] ImageUploadRequestDTO request)
        {
            await _productService.UploadProductImageAsync(id, request);
            
            return Ok(new { Message = "Image uploaded successfully." });
        }
    }
}

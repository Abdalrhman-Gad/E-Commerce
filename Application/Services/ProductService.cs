using Application.DTOs.Image;
using Application.Interfaces;
using AutoMapper;
using Domain.DTOs.Product;
using Domain.Exceptions.Product;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper, IImageService imageService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<GetProductDTO> AddAsync(AddProductDTO addProduct)
    {
        if (addProduct == null)
            throw new InvalidProductException("Product data is invalid.");

        var product = _mapper.Map<Product>(addProduct);

        try
        {
            await _productRepository.AddAsync(product);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while adding the product.", ex);
        }

        return _mapper.Map<GetProductDTO>(product);
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _productRepository.GetAsync(p => p.ProductId == id)
            ?? throw new ProductNotFoundException("Product not found.");

        await _productRepository.DeleteAsync(product);
    }

    public async Task<IEnumerable<GetProductDTO>> GetAllAsync(Expression<Func<Product, bool>> filter = null, string? includes = null, int pageSize = 0, int pageNumber = 1)
    {
        var products = await _productRepository.GetAllAsync(filter, includes, pageSize, pageNumber);
        return _mapper.Map<IEnumerable<GetProductDTO>>(products);
    }

    public async Task<IEnumerable<GetProductDTO>> GetByCategoryNameAsync(string categoryName, string? includes = null, int pageSize = 0, int pageNumber = 1)
    {
        var products = await _productRepository.GetAllAsync(p => p.Category.Name == categoryName, includes, pageSize, pageNumber);
        return _mapper.Map<IEnumerable<GetProductDTO>>(products);
    }

    public async Task<GetProductDTO> GetByIdAsync(int productId)
    {
        var product = await _productRepository.GetAsync(p => p.ProductId == productId)
            ?? throw new ProductNotFoundException("Product not found.");

        return _mapper.Map<GetProductDTO>(product);
    }

    public async Task<GetProductDTO> UpdateAsync(int id, AddProductDTO addProduct)
    {
        var product = await _productRepository.GetAsync(p => p.ProductId == id)
            ?? throw new ProductNotFoundException("Product not found.");

        if (addProduct == null)
            throw new ArgumentNullException(nameof(addProduct), "Product can't be null.");

        product = _mapper.Map(addProduct, product);

        await _productRepository.UpdateAsync(product);

        return _mapper.Map<GetProductDTO>(product);
    }

    public async Task<bool> UploadProductImageAsync(int productId, ImageUploadRequestDTO request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request), "Image upload request is null.");

        var product = await _productRepository.GetAsync(p => p.ProductId == productId)
            ?? throw new ProductNotFoundException("Product not found.");

        // If product already has an image, delete the old one
        if (product.ImageId.HasValue)
        {
            await _imageService.DeleteAsync(product.ImageId.Value);
        }

        // Upload the new image and get the new image id
        var newImageId = await _imageService.UploadImageAsync(request);

        // Update the product with the new image id
        product.ImageId = newImageId;
        await _productRepository.UpdateAsync(product);

        return true;
    }
}

using Domain.DTOs.Image;
using Application.Interfaces;
using AutoMapper;
using Domain.DTOs.Category;
using Domain.Exceptions.Category;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IImageService imageService)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<GetCategoryDTO> AddAsync(AddCategoryDTO dto)
    {
        if (dto == null)
            throw new InvalidCategoryException("Category data is invalid.");

        var category = _mapper.Map<Category>(dto);
        await _categoryRepository.AddAsync(category);
        return _mapper.Map<GetCategoryDTO>(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetAsync(c => c.CategoryId == id)
            ?? throw new CategoryNotFoundException("Category not found.");

        await _categoryRepository.DeleteAsync(category);
    }
    
    public async Task<IEnumerable<GetCategoryDTO>> GetAllAsync(Expression<Func<Category, bool>>? filter = null, int pageSize = 0, int pageNumber = 1)
    {
        var categories = await _categoryRepository.GetAllAsync(filter,null, pageSize, pageNumber);
        return _mapper.Map<IEnumerable<GetCategoryDTO>>(categories);
    }

    public async Task<GetCategoryDTO> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetAsync(c => c.CategoryId == id)
            ?? throw new CategoryNotFoundException("Category not found.");

        return _mapper.Map<GetCategoryDTO>(category);
    }

    public async Task<GetCategoryDTO> UpdateAsync(int id, AddCategoryDTO dto)
    {
        var category = await _categoryRepository.GetAsync(c => c.CategoryId == id)
            ?? throw new CategoryNotFoundException("Category not found.");

        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "Category update data can't be null.");

        category = _mapper.Map(dto, category);
        await _categoryRepository.UpdateAsync(category);

        return _mapper.Map<GetCategoryDTO>(category);
    }

    public async Task<IEnumerable<GetCategoryDTO>> SearchByNameAsync(string name)
    {
        var categories = await _categoryRepository.GetAllAsync(c => c.Name.Contains(name));
        return _mapper.Map<IEnumerable<GetCategoryDTO>>(categories);
    }

    //public async Task<GetCategoryDTO> GetWithProductsAsync(int id)
    //{
    //    var category = await _categoryRepository.GetAsync(c => c.CategoryId == id, "Products")
    //        ?? throw new CategoryNotFoundException("Category not found.");

    //    return _mapper.Map<GetCategoryDTO>(category);
    //}

    public async Task<bool> UploadCategoryImageAsync(int categoryId, ImageUploadRequestDTO request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request), "Image upload request is null.");

        var category = await _categoryRepository.GetAsync(c => c.CategoryId == categoryId)
            ?? throw new CategoryNotFoundException("Category not found.");

        if (category.ImageId.HasValue)
        {
            await _imageService.DeleteAsync(category.ImageId.Value);
        }

        var newImageId = await _imageService.UploadImageAsync(request);
        category.ImageId = newImageId;
        await _categoryRepository.UpdateAsync(category);

        return true;
    }

    
}

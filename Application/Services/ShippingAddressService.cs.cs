using Application.Interfaces;
using AutoMapper;
using Domain.DTOs.ShippingAddress;
using Domain.Exceptions.ShippingAddress;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Application.Services
{
    public class ShippingAddressService : IShippingAddressService
    {
        private readonly IShippingAddressRepository _shippingAddressRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShippingAddressService(IShippingAddressRepository shippingAddressRepository,
            IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _shippingAddressRepository = shippingAddressRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ShippingAddressDTO> AddAsync(AddShippingAddressDTO addShippingAddress)
        {
            if (addShippingAddress == null)
                throw new InvalidShippingAddressException("Review data is invalid.");

            var shippingAddress = _mapper.Map<ShippingAddress>(addShippingAddress);

            shippingAddress.UserId = GetCurrentUserId();

            await _shippingAddressRepository.AddAsync(shippingAddress);

            return _mapper.Map<ShippingAddressDTO>(shippingAddress);
        }

        public async Task DeleteAsync(int id)
        {
            var shippingAddress = await _shippingAddressRepository
                .GetAsync(a => a.ShippingAddressId == id)
                ?? throw new ShippingAddressNotFoundException("Shipping Address Not Found.");

            await _shippingAddressRepository.DeleteAsync(shippingAddress);
        }

        public async Task<IEnumerable<ShippingAddressDTO>> GetAllAsync(Expression<Func<ShippingAddress, bool>>? filter = null, int pageSize = 0, int pageNumber = 1)
        {
            var ShippingAddresses = await _shippingAddressRepository
                 .GetAllAsync(
                filter: a => a.UserId == GetCurrentUserId(),
                pageSize: pageSize,
                pageNumber: pageNumber);

            return _mapper.Map<IEnumerable<ShippingAddressDTO>>(ShippingAddresses);
        }

        public async Task<ShippingAddressDTO> GetByIdAsync(int id)
        {
            var shippingAddress = await _shippingAddressRepository
                .GetAsync(a => a.ShippingAddressId == id)
                ?? throw new ShippingAddressNotFoundException("Shipping Address Not Found.");

            return _mapper.Map<ShippingAddressDTO>(shippingAddress);
        }

        public async Task<ShippingAddressDTO> UpdateAsync(int id, AddShippingAddressDTO updateShippingAddress)
        {
            var shippingAddress = await _shippingAddressRepository
                .GetAsync(a => a.ShippingAddressId == id)
                ?? throw new ShippingAddressNotFoundException("Shipping Address Not Found.");

            _mapper.Map(updateShippingAddress, shippingAddress);

            await _shippingAddressRepository.UpdateAsync(shippingAddress);

            return _mapper.Map<ShippingAddressDTO>(shippingAddress);
        }

        private string GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId ?? throw new UnauthorizedAccessException("User ID not found.");
        }
    }
}
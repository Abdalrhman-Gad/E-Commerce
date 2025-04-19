// CartService.cs
using Application.Interfaces;
using AutoMapper;
using Domain.DTOs.Cart;
using Domain.Exceptions.Cart;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<CartDTO> AddAsync(AddCartDTO addCart)
        {
            var cart = _mapper.Map<Cart>(addCart);

            await _cartRepository.AddAsync(cart);

            return _mapper.Map<CartDTO>(cart);
        }

        public async Task DeleteAsync(string userId)
        {
            var cart = await _cartRepository.GetAsync(c => c.UserId == userId) ??
                       throw new CartNotFoundException("Cart not found.");

            await _cartRepository.DeleteAsync(cart);
        }

        public async Task<IEnumerable<CartDTO>> GetAllAsync(Expression<Func<Cart, bool>>? filter = null, int pageSize = 0, int pageNumber = 1)
        {
            var carts = await _cartRepository.GetAllAsync(filter: filter, pageSize: pageSize, pageNumber: pageNumber);

            return _mapper.Map<IEnumerable<CartDTO>>(carts);
        }

        public async Task<CartDTO> GetByIdAsync(int id)
        {
            var cart = await _cartRepository.GetAsync(c => c.CartId == id) ??
                       throw new CartNotFoundException("Cart not found.");

            return _mapper.Map<CartDTO>(cart);
        }

        public async Task<CartDTO> GetByUserIdAsync(string userId)
        {
            var cart = await _cartRepository.GetAsync(c => c.UserId == userId) ??
                       throw new CartNotFoundException("Cart not found.");

            return _mapper.Map<CartDTO>(cart);
        }
    }
}
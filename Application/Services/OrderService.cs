using Application.Interfaces;
using AutoMapper;
using Domain.DTOs.Order;
using Domain.Enums;
using Domain.Exceptions.Order;
using Domain.Exceptions.OrderItem;
using Domain.Exceptions.Product;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
        }

        public async Task<OrderDTO> AddAsync(AddOrderDTO addOrder)
        {
            if (addOrder == null)
                throw new InvalidOrderException("Order data is invalid.");

            var order = _mapper.Map<Order>(addOrder);

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            order.UserId = userId;

            using var transaction = await _orderRepository.BeginTransactionAsync();
            try
            {
                var addedOrder = await _orderRepository.AddAsync(order);

                addedOrder.Status = OrderStatus.Pending;

                foreach (var item in order.OrderItems)
                {
                    if (item == null)
                        throw new InvalidOrderItemException("Order item data is invalid.");

                    if (item.Quantity <= 0 || item.UnitPrice <= 0)
                        throw new InvalidOrderItemException("Order item must have positive quantity and price.");

                    var product = await _productRepository.GetAsync(p => p.ProductId == item.ProductId) ??
                        throw new ProductNotFoundException("Product not found.");

                    if (product.Stock < item.Quantity)
                        throw new InvalidOrderItemException("Order item stock is less than quantity.");

                    item.OrderId = addedOrder.OrderId;
                    item.OrderItemId = default;

                    await _orderItemRepository.AddAsync(item);
                }

                await transaction.CommitAsync();

                return _mapper.Map<OrderDTO>(addedOrder);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _orderRepository.GetAsync(o => o.OrderId == id)
                ?? throw new OrderNotFoundException("Order not found.");

            await _orderRepository.DeleteAsync(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllAsync(Expression<Func<Order, bool>>? filter = null, int pageSize = 0, int pageNumber = 1)
        {
            var orders = await _orderRepository
                .GetAllAsync(
                    filter,
                    includes: "Payment,OrderItems,ShippingAddress",
                    pageSize,
                    pageNumber);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> GetByIdAsync(int id)
        {
            var order = await _orderRepository
                .GetAsync(o => o.OrderId == id, includes: "Payment,OrderItems,ShippingAddress")
                ?? throw new OrderNotFoundException("Order not found.");

            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO> UpdateAsync(int id, UpdateOrderDTO updateOrder)
        {
            if (updateOrder == null)
                throw new InvalidOrderException("Order data is invalid.");

            var order = await _orderRepository
                .GetAsync(o => o.OrderId == id, includes: "Payment,OrderItems,ShippingAddress")
                ?? throw new OrderNotFoundException("Order not found.");

            order = _mapper.Map(updateOrder, order);

            await _orderRepository.UpdateAsync(order);

            return _mapper.Map<OrderDTO>(order);
        }
    }
}
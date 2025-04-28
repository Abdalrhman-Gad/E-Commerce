using Application.Interfaces;
using AutoMapper;
using Domain.DTOs.Order;
using Domain.Exceptions.Order;
using Domain.Exceptions.OrderItem;
using Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public async Task<OrderDTO> AddAsync(AddOrderDTO addOrder)
        {
            if (addOrder == null)
                throw new InvalidOrderException("Order data is invalid.");

            var order = _mapper.Map<Order>(addOrder);

            using var transaction = await _orderRepository.BeginTransactionAsync();
            try
            {
                var addedOrder = await _orderRepository.AddAsync(order);

                foreach (var item in order.OrderItems)
                {
                    if (item == null)
                        throw new InvalidOrderItemException("Order item data is invalid.");

                    if (item.Quantity <= 0 || item.UnitPrice <= 0)
                        throw new InvalidOrderItemException("Order item must have positive quantity and price.");

                    item.OrderId = addedOrder.OrderId;
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
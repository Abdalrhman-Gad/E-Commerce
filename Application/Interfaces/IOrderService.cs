using Application.Interfaces.IServices;
using Domain.DTOs.Order;

namespace Application.Interfaces
{
    public interface IOrderService : 
        IReadableService<Order,OrderDTO>,
        ICreatableService<AddOrderDTO,OrderDTO>,
        IUpdatableService<OrderDTO,UpdateOrderDTO>,
        IDeletableService<int>
    {

    }
}
using Application.Interfaces.IServices;
using Domain.DTOs.ShippingAddress;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IShippingAddressService : 
        IReadableService<ShippingAddress, ShippingAddressDTO>,
        ICreatableService<AddShippingAddressDTO, ShippingAddressDTO>,
        IUpdatableService<ShippingAddressDTO, AddShippingAddressDTO>,
        IDeletableService<int>
    {

    }
}
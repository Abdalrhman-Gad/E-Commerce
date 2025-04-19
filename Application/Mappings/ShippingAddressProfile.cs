using Application.Mappings.BaseProfiles;
using Domain.DTOs.ShippingAddress;
using Domain.Models;

namespace Application.Mappings
{
    public class ShippingAddressProfile :
        BaseMappingProfile<ShippingAddress, AddShippingAddressDTO, ShippingAddressDTO>
    {
    }
}
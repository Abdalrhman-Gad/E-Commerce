using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.ShippingAddress
{
    public class ShippingAddressDTO
    {
        public int ShippingAddressId { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string UserId { get; set; }
    }
}

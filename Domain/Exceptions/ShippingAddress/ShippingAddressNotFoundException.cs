namespace Domain.Exceptions.ShippingAddress
{
    public class ShippingAddressNotFoundException : Exception
    {
        public ShippingAddressNotFoundException(string message) : base(message)
        {
        }
    }
}
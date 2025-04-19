namespace Domain.Exceptions.ShippingAddress
{
    public class InvalidShippingAddressException : Exception
    {
        public InvalidShippingAddressException(string message) : base(message)
        { }
    }
}
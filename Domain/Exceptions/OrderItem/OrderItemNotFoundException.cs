namespace Domain.Exceptions.OrderItem
{
    public class OrderItemNotFoundException : Exception
    {
        public OrderItemNotFoundException(string message) : base(message)
        {
        }
    }
}

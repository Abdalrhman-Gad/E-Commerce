namespace Domain.Exceptions.OrderItem
{
    public class InvalidOrderItemException : Exception
    {
        public InvalidOrderItemException(string message) : base(message)
        {
        }
    }
}
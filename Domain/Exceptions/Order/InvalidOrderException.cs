namespace Domain.Exceptions.Order
{
    public class InvalidOrderException : Exception
    {
        public InvalidOrderException(string message) : base(message)
        {
        }
    }
}
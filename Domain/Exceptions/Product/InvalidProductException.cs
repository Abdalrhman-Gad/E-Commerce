namespace Domain.Exceptions.Product
{
    public class InvalidProductException : Exception
    {
        public InvalidProductException(string message) : base(message) { }
    }
}
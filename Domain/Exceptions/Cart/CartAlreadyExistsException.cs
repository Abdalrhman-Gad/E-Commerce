namespace Domain.Exceptions.Cart
{
    public class CartAlreadyExistsException : Exception
    {
        public CartAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
namespace Domain.Exceptions.CartItem
{
    public class CartItemAlreadyExistsException : Exception
    {
        public CartItemAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
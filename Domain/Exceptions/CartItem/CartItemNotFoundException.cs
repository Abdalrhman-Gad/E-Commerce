namespace Domain.Exceptions.CartItem
{
    public class CartItemNotFoundException : Exception
    {
        public CartItemNotFoundException(string message) : base(message)
        {
        }
    }
}
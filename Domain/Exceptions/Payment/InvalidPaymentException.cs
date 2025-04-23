namespace Domain.Exceptions.Payment
{
    public class InvalidPaymentException : Exception
    {
        public InvalidPaymentException(string message) : base(message) { }
    }
}

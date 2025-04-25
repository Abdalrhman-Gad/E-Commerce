namespace Domain.Exceptions.Payment
{
    public class PaymentNotFoundException : Exception
    {
        public PaymentNotFoundException(string message) : base(message) { }
    }
}

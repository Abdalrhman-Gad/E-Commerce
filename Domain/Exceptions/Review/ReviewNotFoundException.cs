namespace Domain.Exceptions.Review
{
    public class ReviewNotFoundException : Exception
    {
        public ReviewNotFoundException(string message) : base(message) { }
    }
}

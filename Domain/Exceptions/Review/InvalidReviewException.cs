namespace Domain.Exceptions.Review
{
    public class InvalidReviewException : Exception
    {
        public InvalidReviewException(string message) : base(message) { }
    }
}

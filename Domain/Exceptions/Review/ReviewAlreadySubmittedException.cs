namespace Domain.Exceptions.Review
{
    public class ReviewAlreadySubmittedException : Exception
    {
        public ReviewAlreadySubmittedException(string message):base(message)
        {
            
        }
    }
}

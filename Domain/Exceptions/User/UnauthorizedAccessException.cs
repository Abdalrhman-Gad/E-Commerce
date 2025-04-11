namespace Domain.Exceptions.User
{
    public class UnauthorizedAccessException : Exception
    {
        public UnauthorizedAccessException()
            : base("You are not authorized to perform this action.")
        {
        }
    }
}

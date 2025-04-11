namespace Domain.Exceptions.User
{
    public class InvalidEmailFormatException : Exception
    {
        public InvalidEmailFormatException(string email)
            : base($"The email address {email} is not in a valid format.")
        {
        }
    }
}

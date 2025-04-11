namespace Domain.Exceptions.User
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException(string email)
            : base($"The email address {email} is already in use.")
        {
        }
    }
}

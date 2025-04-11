namespace Domain.Exceptions.User
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string identifier)
            : base($"A user with the identifier {identifier} already exists.")
        {
        }
    }
}

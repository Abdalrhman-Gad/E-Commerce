namespace Domain.Exceptions.User
{
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException()
            : base("An error occurred while registering the user. Please try again.")
        {
        }
    }
}

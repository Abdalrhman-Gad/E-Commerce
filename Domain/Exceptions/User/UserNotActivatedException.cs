namespace Domain.Exceptions.User
{
    public class UserNotActivatedException : Exception
    {
        public UserNotActivatedException(string email)
            : base($"User with email {email} has not yet activated their account.")
        {
        }
    }
}

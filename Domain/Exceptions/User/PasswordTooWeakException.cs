namespace Domain.Exceptions.User
{
    public class PasswordTooWeakException : Exception
    {
        public PasswordTooWeakException(string message)
            : base($"Password must contains {message}")
        {
        }
    }
}

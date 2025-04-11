namespace Domain.Exceptions.User
{
    public class InvalidUserCredentialsException : Exception
    {
        public InvalidUserCredentialsException()
            : base("The email or password provided is incorrect.")
        {
        }
    }
}

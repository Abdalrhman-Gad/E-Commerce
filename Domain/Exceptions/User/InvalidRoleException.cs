namespace Domain.Exceptions.User
{
    public class InvalidRoleException : Exception
    {
        public InvalidRoleException(string role)
            : base($"The role {role} does not exist.")
        {
        }
    }
}

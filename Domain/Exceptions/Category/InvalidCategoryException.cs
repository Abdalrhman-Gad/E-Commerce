namespace Domain.Exceptions.Category
{
    

    public class InvalidCategoryException : Exception
    {
        public InvalidCategoryException(string message) : base(message) { }
    }
}
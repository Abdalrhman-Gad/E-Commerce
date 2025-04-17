namespace Domain.Exceptions.Category
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(string message) : base(message) { }
    }
}
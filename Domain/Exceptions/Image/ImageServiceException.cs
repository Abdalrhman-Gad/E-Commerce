namespace Domain.Exceptions.Image
{
    public class ImageServiceException : Exception
    {
        public ImageServiceException(string message) : base(message) { }
        public ImageServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}

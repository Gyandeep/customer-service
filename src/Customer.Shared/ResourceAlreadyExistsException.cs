namespace Customer.Shared
{
    public class ResourceAlreadyExistsException : Exception
    {
        public ResourceAlreadyExistsException(string errorMessage) : base(errorMessage)
        {
            
        }
    }
}
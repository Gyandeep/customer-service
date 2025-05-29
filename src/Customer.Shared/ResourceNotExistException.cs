namespace Customer.Shared
{
    public class ResourceNotExistException : Exception
    {
        public ResourceNotExistException(string message) : base(message)
        {

        }
    }
}

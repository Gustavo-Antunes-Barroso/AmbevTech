namespace AmbevTech.Domain.Exception
{
    public class BusinessException : IOException
    {
        public BusinessException(string message) : base (message)
        {
            
        }
    }
}

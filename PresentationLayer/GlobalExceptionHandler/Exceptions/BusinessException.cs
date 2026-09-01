namespace App.API.GlobalExceptionHandler.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message)
       : base(message)
        {
        }
    }
}

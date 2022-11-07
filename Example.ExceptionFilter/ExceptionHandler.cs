namespace Excample.ExceptionFilter
{
    public class ExceptionHandler
    {
        private readonly Logger _logger;
        
        public ExceptionHandler()
        {
            _logger = new Logger();
        }

        public bool HandleError(Exception pException)
        {
            _logger.Error("Error!", pException);

            return true;
        }
    }
}
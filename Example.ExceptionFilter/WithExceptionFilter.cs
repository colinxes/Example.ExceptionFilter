using System.Data.Entity.Core;
using System.Data.SqlClient;

namespace Excample.ExceptionFilter
{
    public class WithExceptionFilter
    {
        private readonly Logger _logger;
        private readonly ExceptionHandler _exceptionHandler;

        public WithExceptionFilter()
        {
            _logger = new Logger();
            _exceptionHandler = new ExceptionHandler();
        }

        public void DoSomehting()
        {
            try
            {
                Console.WriteLine("Do something that may throw an Exception!");
                throw new TimeoutException("Timeout!");
            }
            catch (AggregateException aggregateException) when (aggregateException.InnerExceptions.Any(_exceptionHandler.IsHandable))
            {
                foreach (Exception exception in aggregateException.InnerExceptions) 
                    _logger.Error($"Timeout while doing something..", exception);
            }
            catch (Exception exception) when (_exceptionHandler.IsHandable(exception))
            {
                _exceptionHandler.HandleError(exception);
            }
            catch (Exception exception)
            {
                _logger.Error("Unknown error while doing something.", exception);
            }
            
        }
        
        
    }
}
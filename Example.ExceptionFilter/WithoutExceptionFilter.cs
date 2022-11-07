using System.Data.Entity.Core;
using System.Data.SqlClient;

namespace Excample.ExceptionFilter
{
    public class WithoutExceptionFilter
    {
        private readonly Logger _logger;
        private readonly ExceptionHandler _exceptionHandler;
        
        public WithoutExceptionFilter()
        {
            _logger = new Logger();
            _exceptionHandler = new ExceptionHandler();
        }
        
        public void DoSomething()
        {
            bool errorOutOccured;
            int timeoutCount = 0;

            try
            {
                Console.WriteLine("Do something that may throw an Exception!");

                throw new TimeoutException("Timeout!");
            }
            catch (AggregateException aggregateException)
            {
                errorOutOccured = true;
                timeoutCount += 1;
            
                foreach (Exception exception in aggregateException.InnerExceptions)
                {
                    if (exception is TimeoutException ||
                        exception is EntityCommandExecutionException ||
                        exception is EntityException ||
                        exception is ArgumentNullException ||
                        exception is SqlException)
                    {
                        _logger.Error($"Timeout while starting outbound orders (Count: {timeoutCount}).", exception);
                    }
                    else
                    {
                        _logger.Error("Unknown error while transferring outbound orders.", exception);
                    }
                }
            }
            catch (EntityException exception)
            {
                errorOutOccured = _exceptionHandler.HandleError(exception);
            
                
            }
            catch (SqlException exception)
            {
                errorOutOccured = _exceptionHandler.HandleError(exception);
            }
            catch (TimeoutException exception)
            {
                errorOutOccured = _exceptionHandler.HandleError(exception);
            }
            catch (ArgumentNullException exception)
            {
                errorOutOccured = _exceptionHandler.HandleError(exception);
            }
            catch (Exception exception)
            {
                errorOutOccured = true;
                timeoutCount += 1;
                _logger.Error("Unknown error while transferring outbound orders.", exception);
            }
            
            if(errorOutOccured)
                Console.WriteLine($"Error! Timeout count: {timeoutCount}");
        }
    }
}
using System.Data.Entity.Core;
using System.Data.SqlClient;

namespace Excample.ExceptionFilter
{
    public class WithExceptionFilter
    {
        private readonly Logger _logger;
        private readonly ExceptionHandler _exceptionHandler;

        private static IEnumerable<Type> HandableExceptionsTypes =>
            new List<Type>
            {
                typeof(TimeoutException),
                typeof(EntityCommandExecutionException),
                typeof(EntityException),
                typeof(ArgumentNullException),
                typeof(SqlException)
            };
        
        public WithExceptionFilter()
        {
            _logger = new Logger();
            _exceptionHandler = new ExceptionHandler();
        }

        public void DoSomehting()
        {
            bool errorOutOccured;
            int timeoutCount = 0;

            try
            {
                Console.WriteLine("Do something that may throw an Exception!");
                throw new TimeoutException("Timeout!");
            }
            catch (AggregateException aggregateException) when (aggregateException.InnerExceptions.Any(IsHandable))
            {
                errorOutOccured = true;
                timeoutCount += 1;
            
                foreach (Exception exception in aggregateException.InnerExceptions) 
                    _logger.Error($"Timeout while starting outbound orders (Count: {timeoutCount}).", exception);
            }
            catch (Exception exception) when (IsHandable(exception))
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
        
        private static  bool IsHandable(Exception pException)
        {
            return HandableExceptionsTypes.Contains(pException.GetType());
        }
    }
}
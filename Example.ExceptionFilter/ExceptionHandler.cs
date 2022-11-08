using System.Data.Entity.Core;
using System.Data.SqlClient;

namespace Excample.ExceptionFilter
{
    public class ExceptionHandler
    {
        private readonly Logger _logger;
        
        private static IEnumerable<Type> HandableExceptionsTypes =>
            new List<Type>
            {
                typeof(TimeoutException),
                typeof(EntityCommandExecutionException),
                typeof(EntityException),
                typeof(ArgumentNullException),
                typeof(SqlException)
            };
        
        public ExceptionHandler()
        {
            _logger = new Logger();
        }

        public bool HandleError(Exception pException)
        {
            _logger.Error("Error!", pException);

            return true;
        }
        
        public bool IsHandable(Exception pException)
        {
            return HandableExceptionsTypes.Contains(pException.GetType());
        }
    }
}
namespace Excample.ExceptionFilter
{
    public class Logger
    {
        public void Error(string pMessage, Exception pException)
        {
            Console.WriteLine($"{pMessage} - {pException}");
        }
    }
}
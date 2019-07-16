namespace CrxApi.ExLogger
{
    public class ExceptionLoggerContext
    {
        config.Services.Add(typeof(IExceptionLogger), private new ExceptionManagerApi());  

    }
}
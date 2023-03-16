namespace app1.Logger
{
    public interface ILoggers
    {
        public void Loggers(string message, string type);
    }

    public interface ILoggersV2
    {
        public void LoggersV2(string message, string type);
    }
}

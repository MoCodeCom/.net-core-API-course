using app1.Logger;
namespace app1.Logger
{
    public class Loggers : ILoggers
    {
        void ILoggers.Loggers(string message, string type)
        {
            if (type.ToLower() == "error")
            {
                Console.WriteLine("Error - "+ message);
            }
            else
            {
                Console.WriteLine(message , type.ToLower());
            }
        }
    }

    public class LoggersV2 : ILoggersV2
    {

        void ILoggersV2.LoggersV2(string message, string type)
        {
            if (type.ToLower() == "error")
            {
                // Color issue
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Error v2 - " + message);
                //then background other colors to black
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                // Color issue
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(message, type.ToLower());
                //then background other colors to black
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}

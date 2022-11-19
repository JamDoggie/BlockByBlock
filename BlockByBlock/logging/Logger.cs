using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.logging
{
    /// <summary>
    /// This class is temporary.
    /// TODO: Make more robust port of java.util.logging.
    /// , or find a better logging library.
    /// </summary>
    public class Logger
    {
        private static Logger _logger = new Logger();

        public static Logger getLogger(string name)
        {
            return _logger;
        }

        public void log(Level level, string message)
        {
            Console.WriteLine($"[{level}]: {message}");
        }

        public void log(Level level, string message, Exception e)
        {
            Console.WriteLine($"[{level}]: {message}");
            Console.WriteLine($"Caused by: {typeof(Exception).Name}: {e.Message}");
        }
    }

    public enum Level
    {
        INFO,
        WARNING,
        SEVERE
    }
}

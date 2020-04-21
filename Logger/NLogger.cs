using NLog;
using NLog.Config;
using NLog.Targets;

namespace Logger
{
    public class NLogger
    {
        public static NLog.Logger Write()
        {
            NLog.Logger log;
            var config = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);
            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            consoleTarget.Layout = @"${date:format=yyyy-MM-ddTHH\:mm\:ss} ${logger} ${message}";
            fileTarget.FileName = "${basedir}/ErrorLog/Logs/errors.txt";
            fileTarget.Layout = @"--------------------- ${level}(${longdate})${windows-identity:domain=false}-------------------- ${newline}      
            Exception Type: ${exception:format=Type}${newline}  
            Message: ${exception:format=Message}${newline}
            Stack Trace: ${exception:format=Stack Trace}${newline}    
            User Info: ${message}${newline}";

            var consoleRule = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            config.LoggingRules.Add(consoleRule);
            var fileRule = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(fileRule);
            LogManager.Configuration = config;
            log = LogManager.GetLogger("Log");
            return log;
        }
    }
}
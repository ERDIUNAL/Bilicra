using Bilicra.Infrastructure.Domain.Interfaces;
using Bilicra.Infrastructure.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bilicra.Infrastructure.Processes
{
    public class LoggingProcess : ILoggingProcess
    {
        public LoggingProcess()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Logger(lc => lc.WriteTo.File("./Logs/Aydem_.log", rollingInterval: RollingInterval.Day,
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}\t{Level:u3}\t{Message:lj}\t{Exception}{NewLine}",
                    rollOnFileSizeLimit: true, retainedFileCountLimit: null)).CreateLogger();
            WriteInformationMessage("Logger Started.", null, MethodBase.GetCurrentMethod(), string.Empty);
        }

        public void WriteInformationMessage(string message, Exception exception, MethodBase method, string postfix)
        {
            Log.Information(LoggingService.FormatSerilogMessage(LoggingService.GetCallerClass(method), LoggingService.GetCallerMethod(method), message, exception, postfix));
        }
        public void WriteDebugMessage(string message, Exception exception, MethodBase method, string postfix)
        {
            Log.Debug(LoggingService.FormatSerilogMessage(LoggingService.GetCallerClass(method), LoggingService.GetCallerMethod(method), message, exception, postfix));
        }
        public void WriteErrorMessage(string message, Exception exception, MethodBase method, string postfix)
        {
            Log.Error(LoggingService.FormatSerilogMessage(LoggingService.GetCallerClass(method), LoggingService.GetCallerMethod(method), message, exception, postfix));
        }
        public void WriteWarningMessage(string message, Exception exception, MethodBase method, string postfix)
        {
            Log.Warning(LoggingService.FormatSerilogMessage(LoggingService.GetCallerClass(method), LoggingService.GetCallerMethod(method), message, exception, postfix));
        }
        public void WriteFatalMessage(string message, Exception exception, MethodBase method, string postfix)
        {
            Log.Fatal(LoggingService.FormatSerilogMessage(LoggingService.GetCallerClass(method), LoggingService.GetCallerMethod(method), message, exception, postfix));
        }
    }
}

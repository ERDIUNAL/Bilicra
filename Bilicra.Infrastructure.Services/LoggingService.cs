using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bilicra.Infrastructure.Services
{
    public static class LoggingService
    {
        public static string FormatSerilogMessage(string className, string methodName, string message, Exception exception, string postfix)
        {
            var postfixText = new StringBuilder();
            if ((postfix ?? "") != "")
            {
                postfixText.Append($", {postfix}");
            }
            if (exception != null)
            {
                postfixText.Append($". Inner Exception: {exception.Message}");

            }
            return $"{className}\t{methodName}\t{message}{postfixText}";
        }

        public static string GetCallerClass(MethodBase method)
        {
            return method.DeclaringType.FullName;
        }
        public static string GetCallerMethod(MethodBase method)
        {
            return method.Name;
        }
    }
}

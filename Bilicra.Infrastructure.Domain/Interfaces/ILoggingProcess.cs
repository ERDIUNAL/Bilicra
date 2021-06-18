using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bilicra.Infrastructure.Domain.Interfaces
{
    public interface ILoggingProcess
    {
        void WriteInformationMessage(string message, Exception exception, MethodBase method, string postfix);
        void WriteDebugMessage(string message, Exception exception, MethodBase method, string postfix);
        void WriteErrorMessage(string message, Exception exception, MethodBase method, string postfix);
        void WriteWarningMessage(string message, Exception exception, MethodBase method, string postfix);
        void WriteFatalMessage(string message, Exception exception, MethodBase method, string postfix);
    }
}

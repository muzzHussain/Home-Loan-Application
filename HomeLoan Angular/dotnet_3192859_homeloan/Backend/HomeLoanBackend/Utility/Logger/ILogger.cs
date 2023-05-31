using System;

namespace Utility
{   
    /// <summary>
    ///     This interface define Logger Functionality
    /// </summary>
    public interface ILogger
    {
        bool IsDebugEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsWarningEnabled { get; }
        bool IsTraceEnabled { get; }
        void LogDebug(object message);
        void LogError(string message);
        void LogError(Exception exceptionToLog);
        void LogError(object message, Exception exception);
        void LogInfo(object message);
        void LogWarning(object message);
        void LogTrace(object message);
    }
}




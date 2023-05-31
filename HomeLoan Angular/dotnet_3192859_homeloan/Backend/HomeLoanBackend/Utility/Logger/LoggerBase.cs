using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    /// <summary>
    ///     ILogger Methods Implementation
    /// </summary>
    public class LoggerBase : ILogger
    {
        public bool IsDebugEnabled { get; protected set; }
        public bool IsErrorEnabled { get; protected set; }
        public bool IsInfoEnabled { get; protected set; }
        public bool IsWarningEnabled { get; protected set; }
        public bool IsTraceEnabled { get; protected set; }

        public void LogDebug(object message)
        {
            if (this.IsDebugEnabled)
            {
                this.OnLogDebug(message);
            }
        }

        public void LogError(string message)
        {
            if (IsErrorEnabled)
            {
                this.OnLogError(message);
            }
        }

        public void LogError(Exception exceptionToLog)
        {
            if (IsErrorEnabled)
            {
                this.OnLogError(exceptionToLog);
            }
        }

        public void LogError(object message, Exception exception)
        {
            if (IsErrorEnabled)
            {
                this.OnLogError(message, exception);
            }
        }

        public void LogInfo(object message)
        {
            if (IsInfoEnabled)
            {
                this.OnLogInfo(message);
            }
        }

        public void LogWarning(object message)
        {
            if (IsWarningEnabled)
            {
                this.OnLogWarning(message);
            }
        }

        public void LogTrace(object message)
        {
            if (IsTraceEnabled)
            {
                this.OnLogTrace(message);
            }
        }

        protected virtual void OnLogDebug(object message)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnLogError(string message)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnLogError(Exception exceptionToLog)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnLogError(object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnLogInfo(object message)
        {
            throw new NotImplementedException();
        }
        
        protected virtual void OnLogWarning(object message)
        {
            throw new NotImplementedException();
        }
        
        protected virtual void OnLogTrace(object message)
        {
            throw new NotImplementedException();
        }
    }
}



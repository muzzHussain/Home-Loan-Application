using NLog;
using System;

namespace Utility
{
    public class Logger : LoggerBase
    {
        private readonly global::NLog.Logger _logger;
        public string Name { get; private set; }

        public Logger()
        {
            _logger = LogManager.GetCurrentClassLogger();
            // TODO : Read these settings based on NLog configuration.
            this.IsDebugEnabled = true;
            base.IsErrorEnabled = true;
            base.IsInfoEnabled = true;
            base.IsWarningEnabled = true;
        }

        /// <summary>
        ///     For Debug the error
        /// </summary>
        /// <param name="message"></param>
        protected override void OnLogDebug(object message)
        {
            _logger.Log(LogLevel.Debug, message.ToString());
        }

        /// <summary>
        ///     To log error
        /// </summary>
        /// <param name="message"></param>
        protected override void OnLogError(string message)
        {
            _logger.Log(LogLevel.Error, message);
        }

        /// <summary>
        ///     To log exceptions error
        /// </summary>
        /// <param name="exceptionToLog"></param>
        protected override void OnLogError(Exception exceptionToLog)
        {
            _logger.Log(LogLevel.Error, exceptionToLog);
        }

        /// <summary>
        ///     To log Diagnostic message and exception at specified level
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exceptionToLog"></param>
        protected override void OnLogError(object message, Exception exceptionToLog)
        {
            _logger.Log(LogLevel.Error, exceptionToLog, message.ToString());
        }

        /// <summary>
        ///     To log diagnostic message at specified level 
        /// </summary>
        /// <param name="message"></param>
        protected override void OnLogInfo(object message)
        {
            _logger.Log(LogLevel.Info, message.ToString());
        }

        /// <summary>
        ///     To log warning with message
        /// </summary>
        /// <param name="message"></param>
        protected override void OnLogWarning(object message)
        {
            _logger.Log(LogLevel.Warn, message.ToString());
        }
    }
}



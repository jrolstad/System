using System;
using log4net;
using Rolstad.System.Dates;
using Rolstad.System.Extensions;

namespace Rolstad.System.Logging
{
    /// <summary>
    /// Which level to log method log entries at
    /// </summary>
    public enum MethodLoggingLevel
    {
        Fatal,
        Error,
        Warn,
        Info,
        Debug,
        
    }

    /// <summary>
    /// Utility class to log method ins / outs
    /// </summary>
    public class MethodLogger
    {
        private string MethodName { get; set; }
        private ILog Log { get; set; }
        private DateTime StartTime { get; set; }
        private MethodLoggingLevel LoggingLevel { get; set; }

        /// <summary>
        /// Captures when the logger was initialized
        /// </summary>
        /// <param name="log">Log to use for logging</param>
        /// <param name="methodName">Name of the method being logged</param>
        /// <param name="loggingLevel">Level to log items at</param>
        public MethodLogger(ILog log, string methodName, MethodLoggingLevel loggingLevel = MethodLoggingLevel.Debug)
        {
            // Set the values           
           
            Log = log;
            LoggingLevel = loggingLevel;

            // Only capture what is going on if we are able to log
            if (IsLoggingLevelEnabled())
            {
                StartTime = Clock.Now;
                MethodName = methodName;
            
                // Log that we began
                var message = "{0} started".StringFormat(MethodName);
                this.LogMessage(message);
            }
        }

        /// <summary>
        /// Writes the method complete message and time frame
        /// </summary>
        public void Dispose()
        {
            if (Log != null
                && IsLoggingLevelEnabled())
            {
                // Log that we're done
                var message = "{0} complete - ({1}s)".StringFormat(MethodName, ( Clock.Now - StartTime ).TotalSeconds);
                this.LogMessage(message);
            }
           
        }

        /// <summary>
        /// Determines if the desired logging level is enabled
        /// </summary>
        /// <returns></returns>
        private bool IsLoggingLevelEnabled()
        {
            switch (this.LoggingLevel)
            {
                case MethodLoggingLevel.Debug:
                {
                    return this.Log.IsDebugEnabled;
                }
                case MethodLoggingLevel.Error:
                {
                    return this.Log.IsErrorEnabled;
                }
                case MethodLoggingLevel.Fatal:
                {
                    return this.Log.IsFatalEnabled;
                }
                case MethodLoggingLevel.Info:
                {
                    return this.Log.IsInfoEnabled;
                }
                case MethodLoggingLevel.Warn:
                {
                    return this.Log.IsWarnEnabled;
                }
                default:
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Logs a message to the correct logging level
        /// </summary>
        /// <param name="message">Message to log</param>
        private void LogMessage(string message)
        {
            switch (this.LoggingLevel)
            {
                case MethodLoggingLevel.Debug:
                {
                    Log.Debug(message);
                    break;
                }
                case MethodLoggingLevel.Error:
                {
                    Log.Error(message);
                    break;
                }
                case MethodLoggingLevel.Fatal:
                {
                    Log.Fatal(message);
                    break;
                }
                case MethodLoggingLevel.Info:
                {
                    Log.Info(message);
                    break;
                }
                case MethodLoggingLevel.Warn:
                {
                    Log.Warn(message);
                    break;
                }
            }
        }
    }
}
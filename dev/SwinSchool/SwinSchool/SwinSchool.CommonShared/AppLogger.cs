using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwinSchool.CommonShared
{
    public class AppLogger
    {
        public static readonly Logger _defaultLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Default message for an un-expected system error.
        /// </summary>
        public const string UNEXPECTED_ERROR = "An unexpected error occured. Please try again, and if the error persists, contact your system administrator.";

        private static readonly string MESSAGE_PATTERN = "Message : {0}" + Environment.NewLine;
        private static readonly string EXCEPTION_MESSAGE_PATTERN = "Exception Message : {0}" + Environment.NewLine;
        private static readonly string EXCEPTION_STACKTRACE_PATTERN = "Trace : {0}" + Environment.NewLine;
        private static readonly string INNER_MESSAGE_PATTERN = "Inner Exception : {0}" + Environment.NewLine;
        private static readonly string INNER_EXCEPTION_STACKTRACE_PATTERN = "Inner Exception Stack : {0}" + Environment.NewLine;

        /// <summary>
        /// Logs the specified message using default logger and ERROR level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Log(string message)
        {
            Log(message, null, LogLevel.Error);
        }

        /// <summary>
        /// Logs the specified validation.
        /// </summary>
        /// <param name="validation">The validation.</param>
        public static void Log(IEnumerable<string> validation)
        {
            Log(validation, LogLevel.Error);
        }

        /// <summary>
        /// Logs the specified validation.
        /// </summary>
        /// <param name="validation">The validation.</param>
        /// <param name="level">The level.</param>
        public static void Log(IEnumerable<string> validation, LogLevel level)
        {
            var messageBuilder = new StringBuilder();
            foreach (string message in validation)
            {
                if (messageBuilder.Length > 0)
                    messageBuilder.Append(Environment.NewLine);
                messageBuilder.Append(message);
            }
            if (messageBuilder.Length > 0)
                Log(messageBuilder.ToString(), level);
        }

        /// <summary>
        /// Logs the specified message using the given logger at ERROR level
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="loggerName">Name of the logger.</param>
        public static void Log(string message, string loggerName)
        {
            Log(message, null, LogLevel.Error, loggerName);
        }

        /// <summary>
        /// Logs the specified message using default logger at the specified level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        public static void Log(string message, LogLevel level)
        {
            Log(message, null, level, _defaultLogger);
        }

        /// <summary>
        /// Logs the specified message using default logger at INFO level
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Info(string message)
        {
            Log(message, null, LogLevel.Info, _defaultLogger);
        }

        /// <summary>
        /// Logs the specified message using default logger at INFO level
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="loggerName">Name of the logger.</param>
        public static void Info(string message, string loggerName)
        {
            Log(message, LogLevel.Info, loggerName);
        }

        /// <summary>
        /// Logs the specified message with specified logger at the specified level
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        /// <param name="loggerName">Name of the logger.</param>
        public static void Log(string message, LogLevel level, string loggerName)
        {
            Log(message, null, level, loggerName);
        }

        /// <summary>
        /// Logs the specified exception using default logger and ERROR level.
        /// </summary>
        /// <param name="e">The exception to log.</param>
        public static void Log(Exception e)
        {
            Log(null, e, LogLevel.Error);
        }

        /// <summary>
        /// Logs the specified exception using default logger at the specified level.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="level">The level.</param>
        public static void Log(Exception e, LogLevel level)
        {
            Log(null, e, level, _defaultLogger);
        }

        /// <summary>
        /// Logs the specified exception with the given logger at ERROR level
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="loggerName">Name of the logger.</param>
        public static void Log(Exception e, string loggerName)
        {
            Log(null, e, LogLevel.Error, loggerName);
        }

        /// <summary>
        /// Logs the specified exception using given logger at the specified level
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="level">The level.</param>
        /// <param name="loggerName">Name of the logger.</param>
        public static void Log(Exception e, LogLevel level, string loggerName)
        {
            Log(null, e, level, loggerName);
        }
        /// <summary>
        /// Logs the specified message and exception using default logger and ERROR level
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public static void Log(string message, Exception e)
        {
            Log(message, e, LogLevel.Error);
        }

        /// <summary>
        /// Logs the specified message and exception using default logger and a specified level
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        /// <param name="level">The level.</param>
        public static void Log(string message, Exception e, LogLevel level)
        {
            Log(message, e, level, _defaultLogger);
        }


        /// <summary>
        /// Logs the specified message and exception using specified logger and at a specified level
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        /// <param name="level">The level.</param>
        /// <param name="loggerName">Name of the logger.</param>
        public static void Log(string message, Exception e, LogLevel level, string loggerName)
        {
            Log(message, e, level, LogManager.GetLogger(loggerName));
        }

        private static void Log(string message, Exception e, LogLevel level, Logger logger)
        {
            string msg = CompositeLogMessage(message, e);
            if (level == LogLevel.Debug)
            {
                logger.Debug(msg);
            }
            else if (level == LogLevel.Info)
            {
                logger.Info(msg);
            }
            else if (level == LogLevel.Warn)
            {
                logger.Warn(msg);
            }
            else if (level == LogLevel.Fatal)
            {
                logger.Fatal(msg);
            }
            else
            {
                logger.Error(msg);
            }
        }

        /// <summary>
        /// Composites the log message from given message and exception, including inner exception wrapped in the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        private static string CompositeLogMessage(string message, Exception e)
        {
            var msgBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(message))
                msgBuilder.Append(string.Format(MESSAGE_PATTERN, message));

            if (e != null && e.Message != null)
                msgBuilder.Append(string.Format(EXCEPTION_MESSAGE_PATTERN, e.Message));

            if (e != null && e.StackTrace != null)
                msgBuilder.Append(string.Format(EXCEPTION_STACKTRACE_PATTERN, e.StackTrace));

            if (e != null && e.InnerException != null)
            {
                msgBuilder.Append(string.Format(INNER_MESSAGE_PATTERN, e.InnerException.Message));

                if (!string.IsNullOrEmpty(e.InnerException.StackTrace))
                    msgBuilder.Append(string.Format(INNER_EXCEPTION_STACKTRACE_PATTERN, e.InnerException.StackTrace));
            }
            return msgBuilder.ToString();
        }
    }
}

using Mrp.Logger.ConsoleLogger.Configure;
using Mrp.Logger.ConsoleLogger.Exceptions;
using Mrp.Logger.Enums;
using Mrp.Logger.Interfaces;
using System;

namespace Mrp.Logger.ConsoleLogger
{
	class ConsoleLogger : BaseLogger.BaseLogger, ILogger
	{
		object _sync = new object();
		private ConsoleLoggerConfigure _config;

		public ConsoleLogger(ConsoleLoggerConfigure config) : base(config.GetBaseLoggerConfigure())
		{
			_config = config;
		}

		protected override void OnBeginningLogging() { }
		protected override void OnFinishedLogging() { }

		protected override void WriteLogToSpecificDestination(LoggerMessage logMessage)
		{
			// The _sync is important, because the coloring rules have to be followed
			lock (_sync)
			{
				ConsoleColor originalColor = Console.ForegroundColor;
				SetConsolForegroundColor(logMessage.LogLevel);
				Console.WriteLine(logMessage.LogMessage);
				Console.ForegroundColor = originalColor;
			}
		}

		public override void Log(LoggerMessage message)
		{
			if (message.LogMessage?.Length > _config.MaxLengthLogMessage)
			{
				throw new StringTooLongException(_config.MaxLengthLogMessage, message.LogMessage.Length);
			}

			base.Log(message);
		}

		private static void SetConsolForegroundColor(MessageLevel type)
		{
			_ = type switch
			{
				MessageLevel.Info => Console.ForegroundColor = ConsoleColor.Green,
				MessageLevel.Error => Console.ForegroundColor = ConsoleColor.Red,
				_ => Console.ForegroundColor = ConsoleColor.Gray,
			};
		}
	}
}

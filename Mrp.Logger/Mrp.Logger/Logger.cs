using Mrp.Logger.Configure;
using Mrp.Logger.Enums;
using Mrp.Logger.Interfaces;
using System;

namespace Mrp.Logger
{
	public class Logger : ILogger
	{
		private ILogger _logger;
		private LoggerConfigure _config;

		public Logger()
		{
			InitializingDefaultConfiguration();
			InitializingLogger();
		}

		public Logger(Action<LoggerConfigure> loggerConfig)
		{
			InitializingDefaultConfiguration();
			loggerConfig(_config);
			InitializingLogger();
		}
		
		private void InitializingDefaultConfiguration()
			=> _config = LoggerConfigure.Default;

		private void InitializingLogger()
		{
			_logger = _config.LoggerType switch
			{
				LoggerType.FileLogger => new FileLogger.FileLogger(_config.GetFileLoggerConfigure()),
				LoggerType.ConsoleLogger => new ConsoleLogger.ConsoleLogger(_config.GetConsoleLoggerConfigure()),
				LoggerType.RestApiLogger => new RestApiLogger.RestApiLogger(_config.GetRestApiLoggerConfigure()),
				_ => new FileLogger.FileLogger(_config.GetFileLoggerConfigure())
			};
		}

		public void Log(LoggerMessage message)
			=> _logger.Log(message);
	}
}

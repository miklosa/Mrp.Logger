using Mrp.Logger.ConsoleLogger.Configure;
using Mrp.Logger.Enums;
using Mrp.Logger.FileLogger.Configure;
using Mrp.Logger.Interfaces;
using Mrp.Logger.RestApiLogger.Configure;
using System;

namespace Mrp.Logger.Configure
{
	public class LoggerConfigure: IConfigure
	{
		private ConsoleLoggerConfigure _consoleLoggerConfigure;
		private FileLoggerConfigure _fileLoggerConfigure;
		private RestApiLoggerConfigure _restApiLoggerConfigure;

		public static LoggerConfigure Default
			=> new LoggerConfigure() { _consoleLoggerConfigure = null, _fileLoggerConfigure = FileLoggerConfigure.Default };

		public ConsoleLoggerConfigure GetConsoleLoggerConfigure()
			=> _consoleLoggerConfigure;

		public FileLoggerConfigure GetFileLoggerConfigure()
			=> _fileLoggerConfigure;

		public RestApiLoggerConfigure GetRestApiLoggerConfigure()
			=> _restApiLoggerConfigure;

		public void UseFileLogger()
		{
			_consoleLoggerConfigure = null;
			_restApiLoggerConfigure = null;
			_fileLoggerConfigure = FileLoggerConfigure.Default;
		}

		public void UseConsoleLogger()
		{
			_fileLoggerConfigure = null;
			_restApiLoggerConfigure = null;
			_consoleLoggerConfigure = ConsoleLoggerConfigure.Default;
		}

		public void UseRestApiLogger()
		{
			_consoleLoggerConfigure = null;
			_fileLoggerConfigure = null;
			_restApiLoggerConfigure = RestApiLoggerConfigure.Default;
		}

		public void UseFileLogger(Action<FileLoggerConfigure> fileLoggerConfigurator)
		{
			UseFileLogger();
			fileLoggerConfigurator(_fileLoggerConfigure);
		}

		public void UseConsoleLogger(Action<ConsoleLoggerConfigure> consoleLoggerConfigurator)
		{
			UseConsoleLogger();
			consoleLoggerConfigurator(_consoleLoggerConfigure);
		}

		public void UseRestApiLogger(Action<RestApiLoggerConfigure> restApiLoggerConfigurator)
		{
			UseRestApiLogger();
			restApiLoggerConfigurator(_restApiLoggerConfigure);
		}

		public LoggerType LoggerType
		{
			get
			{
				if (_fileLoggerConfigure != null) return LoggerType.FileLogger;
				else if (_consoleLoggerConfigure != null) return LoggerType.ConsoleLogger;
				else if (_restApiLoggerConfigure != null) return LoggerType.RestApiLogger;
				else
				{
					UseFileLogger();
					return LoggerType.FileLogger;
				}
			}
		}
	}
}

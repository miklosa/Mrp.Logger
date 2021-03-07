using Mrp.Logger.Enums;

namespace Mrp.Logger.Interfaces
{
	public interface ILogger
	{
		void Info(string logMessage) => Log(new LoggerMessage() { LogMessage = logMessage ?? "", LogLevel = MessageLevel.Info });
		void Debug(string logMessage) => Log(new LoggerMessage() { LogMessage = logMessage ?? "", LogLevel = MessageLevel.Debug });
		void Error(string logMessage) => Log(new LoggerMessage() { LogMessage = logMessage ?? "", LogLevel = MessageLevel.Error });

		void Log(LoggerMessage message);
	}
}

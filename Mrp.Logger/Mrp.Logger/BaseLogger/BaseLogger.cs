using Mrp.Logger.BaseLogger.Configure;
using Mrp.Logger.Enums;
using Mrp.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mrp.Logger.BaseLogger
{
	public abstract class BaseLogger : ILogger
	{
		object _sync = new object();
		private Queue<LoggerMessage> _queue;
		private BaseLoggerConfigure _config;

		protected abstract void WriteLogToSpecificDestination(LoggerMessage logMessage);
		protected abstract void OnBeginningLogging();
		protected abstract void OnFinishedLogging();


		public BaseLogger(BaseLoggerConfigure config)
		{
			_config = config;

			_queue = new Queue<LoggerMessage>();

			if (_config.LoggerSyncType == LoggerSyncType.Async)
			{
				Task.Run(() => BackgroundLoggingWorker());
			}
		}

		public virtual void Log(LoggerMessage message)
		{
			if (message != null)
			{
				string logTime = DateTime.Now.ToString(_config.LogTimePattern);

				message.LogMessage = string.Format(_config.LogMessagePattern, logTime, message.LogLevel, message.LogMessage ?? "");

				if (_config.LoggerSyncType == LoggerSyncType.Async)
				{
					lock (_sync)
					{
						_queue.Enqueue(message);
					}
				}
				else
				{
					OnBeginningLogging();
					WriteLogToSpecificDestination(message);
					OnFinishedLogging();
				}
			}
		}

		private void BackgroundLoggingWorker()
		{
			while (true)
			{
				if (_queue.Count > 0)
				{
					OnBeginningLogging();

					while (_queue.Count > 0)
					{
						LoggerMessage message;
						lock (_sync)
						{
							message = _queue.Dequeue();
						}

						WriteLogToSpecificDestination(message);
					}
					OnFinishedLogging();
				}

				Thread.Sleep(1);
			}
		}
	}
}

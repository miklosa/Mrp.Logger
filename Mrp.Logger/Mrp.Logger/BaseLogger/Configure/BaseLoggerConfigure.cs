using Mrp.Logger.Enums;
using Mrp.Logger.Interfaces;

namespace Mrp.Logger.BaseLogger.Configure
{
	public class BaseLoggerConfigure : IConfigure
	{
		/// <summary>
		/// Default value is "yyyy/MM/dd HH:mm:ss.ffff"
		/// </summary>
		public string LogTimePattern { get; set; } = "yyyy/MM/dd HH:mm:ss.ffff";

		/// <summary>
		/// Default value is "{0} [{1}] {2}"
		/// <br>{0} - LogTime</br>
		/// <br>{1} - LogLevel [Info,Debug,Error]</br>
		/// <br>{2} - LogMessage</br>
		/// </summary>
		public string LogMessagePattern { get; set; } = "{0} [{1}] {2}";

		/// <summary>
		/// Default value is ASYNC
		/// <br>It could be:</br>
		/// <br>LoggerSyncType.Sync</br>
		/// <br>LoggerSyncType.Async</br>
		/// </summary>
		public LoggerSyncType LoggerSyncType { get; set; } = LoggerSyncType.Async;


		public static BaseLoggerConfigure Default => new BaseLoggerConfigure();
	}
}

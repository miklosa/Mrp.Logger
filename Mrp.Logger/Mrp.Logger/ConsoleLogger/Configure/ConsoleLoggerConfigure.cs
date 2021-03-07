using Mrp.Logger.BaseLogger.Configure;
using Mrp.Logger.Interfaces;

namespace Mrp.Logger.ConsoleLogger.Configure
{
	public class ConsoleLoggerConfigure : BaseConfigure, IConfigure
	{
		private ConsoleLoggerConfigure() { }

		/// <summary>
		/// Default value is 1000
		/// </summary>
		public int MaxLengthLogMessage { get; set; } = 1000;

		public static ConsoleLoggerConfigure Default
			=> new ConsoleLoggerConfigure();
	}
}

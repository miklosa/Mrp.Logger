using Mrp.Logger.BaseLogger.Configure;
using Mrp.Logger.Interfaces;

namespace Mrp.Logger.RestApiLogger.Configure
{
	public class RestApiLoggerConfigure : BaseConfigure, IConfigure
	{
		/// <summary>
		/// Default value is "https://localhost:44370/logger"
		/// </summary>
		public string EndPointUrl { get; set; } = "https://localhost:44370/logger";

		public static RestApiLoggerConfigure Default
			=> new RestApiLoggerConfigure();
	}
}

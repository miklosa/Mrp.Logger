using Mrp.Logger.RestApiLogger.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mrp.Logger.RestApiLogger
{
	public class RestApiLogger : BaseLogger.BaseLogger
	{
		private HttpClient _client;
		private RestApiLoggerConfigure _config;

		public RestApiLogger(RestApiLoggerConfigure config) : base(config.GetBaseLoggerConfigure())
		{
			_config = config;
			_client = new HttpClient();
			_client.DefaultRequestHeaders.Accept.Clear();
		}

		protected override void OnBeginningLogging() { }
		protected override void OnFinishedLogging() { }

		protected override void WriteLogToSpecificDestination(LoggerMessage logMessage)
		{
			string jsonContent = JsonSerializer.Serialize(logMessage);
			var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
			_ = _client.PostAsync(_config.EndPointUrl, content);
		}
	}
}

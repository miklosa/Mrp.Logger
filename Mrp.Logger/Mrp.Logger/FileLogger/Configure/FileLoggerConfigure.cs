using Mrp.Logger.BaseLogger.Configure;
using Mrp.Logger.Interfaces;
using System.IO;
using System.Reflection;

namespace Mrp.Logger.FileLogger.Configure
{
	public class FileLoggerConfigure : BaseConfigure, IConfigure
	{
		private FileLoggerConfigure() { }

		/// <summary>
		/// Default value is 5k
		/// </summary>
		public int ArchiveFileSize { get; set; } = 5 * 1024;

		/// <summary>
		/// Default value is "log.{0}.txt"
		/// <br>{0} - serial number of the archive file</br>
		/// </summary>
		public string ArchivedLogFilePattern { get; set; } = "log.{0}.txt";

		/// <summary>
		/// Default value is "log.*.txt"
		/// <br>* - position sign of the serial number of archive file</br>
		/// </summary>
		public string ArchivingFileSearchingPattern { get; set; } = "log.*.txt";

		/// <summary>
		/// Default value is the root folder of the running application
		/// </summary>
		public string RootFolder { get; set; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		/// <summary>
		/// Default value is "{0}\\Logs"
		/// <br>{0} - RootFolder</br>
		/// </summary>
		public string LogDirectoryPathPattern { get; set; } = "{0}\\Logs";

		/// <summary>
		/// Default value is "log.txt"
		/// </summary>
		public string LogFileName { get; set; } = "log.txt";

		public string LogDirectoryPath => string.Format(LogDirectoryPathPattern, RootFolder);
		public string LogFilePath => $"{LogDirectoryPath}\\{LogFileName}";
		public string NextArchivedLogFilePathPattern => LogDirectoryPath + "\\" + ArchivedLogFilePattern;

		public static FileLoggerConfigure Default
			=> new FileLoggerConfigure();
	}
}

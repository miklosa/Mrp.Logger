using Mrp.Logger.FileLogger.Configure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mrp.Logger.FileLogger
{
	public class FileLogger : BaseLogger.BaseLogger
	{
		private FileLoggerConfigure _config;
		private StreamWriter _fileStreamWriter;

		public FileLogger(FileLoggerConfigure config) : base(config.GetBaseLoggerConfigure())
		{
			_config = config;
			CreateLogDirectoryIfNotExists();
		}

		protected override void WriteLogToSpecificDestination(LoggerMessage logMessage)
		{
			int lengthOfFile;

			_fileStreamWriter.WriteLine(logMessage.LogMessage);

			lengthOfFile = (int)_fileStreamWriter.BaseStream.Length;

			if (IsLogFileReachedArchivedSize(lengthOfFile))
			{
				CloseLoggingFile();
				Archive();
				OpenLoggingFile();
			}
		}

		protected override void OnBeginningLogging()
			=> OpenLoggingFile();

		protected override void OnFinishedLogging()
			=> CloseLoggingFile();

		private void CreateLogDirectoryIfNotExists()
		{
			if (!Directory.Exists(_config.LogDirectoryPath))
			{
				Directory.CreateDirectory(_config.LogDirectoryPath);
			}
		}

		private void OpenLoggingFile()
			=> _fileStreamWriter = File.AppendText(_config.LogFilePath);

		private void CloseLoggingFile()
			=> _fileStreamWriter.Close();

		private bool IsLogFileReachedArchivedSize(long lengthOfFile)
			=> lengthOfFile >= _config.ArchiveFileSize;

		private void Archive()
			=> File.Move(_config.LogFilePath, GetNextArchiveLogFilePath());

		private List<string> GetAllArchivedLogFiles()
			=> Directory.GetFiles(_config.LogDirectoryPath, _config.ArchivingFileSearchingPattern).ToList();

		private int GetMaxArchivedLogEntry(List<string> allArchivedLogFiles)
			=> allArchivedLogFiles.Select(s => Convert.ToInt32(Path.GetFileNameWithoutExtension(s).Split('.')[1])).Max();

		private string GetNextArchiveLogFilePath()
		{
			// Default value of Next Archiving file. This is important when no one archived file exists.
			var maxArchivedNumber = 0;

			var allArchivedLogFiles = GetAllArchivedLogFiles();

			if (allArchivedLogFiles != null && allArchivedLogFiles.Count > 0)
			{
				maxArchivedNumber = GetMaxArchivedLogEntry(allArchivedLogFiles);
			}
			return string.Format(_config.NextArchivedLogFilePathPattern, maxArchivedNumber + 1); // +1 means next
		}
	}
}

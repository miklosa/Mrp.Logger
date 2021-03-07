using Mrp.Logger;
using Mrp.Logger.Interfaces;
using System;

namespace TestConsoleLogger
{
	class Program
	{
		static void Main(string[] args)
		{
			ILogger logger = new Logger(config =>
			{
				config.UseConsoleLogger();
			});

			Console.WriteLine("Hello World!");

			for (int i = 0; i < 100; i++)
			{
				logger.Info($" {i} Info");
				logger.Error($" {i} Error");
				logger.Debug($" {i} Debug");
			}

			Console.Read();
		}
	}
}

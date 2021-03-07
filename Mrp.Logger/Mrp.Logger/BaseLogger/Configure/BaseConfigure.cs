using System;

namespace Mrp.Logger.BaseLogger.Configure
{
	public abstract class BaseConfigure
	{
		protected BaseLoggerConfigure _baseLoggerConfigure = BaseLoggerConfigure.Default;

		public BaseLoggerConfigure GetBaseLoggerConfigure()
			=> _baseLoggerConfigure;

		public void BaseConfigurations(Action<BaseLoggerConfigure> baseLoggerConfigurator)
			=> baseLoggerConfigurator(_baseLoggerConfigure);
	}
}

# Mrp.Logger

Mrp.Logger is a library which can be used in other projects to log different messages. The type of the logger can be easily changed in every project or multiple logger can be add in each project.


There are three different message levels: debug, info, error


There are three types of the loggers:
 * console logger: logs to the console
 * file logger: logs to a file
 * rest api logger: logs to a remote rest api endpoint


The console logger throws an appropriate exception [StringTooLongException](https://github.com/miklosa/Mrp.Logger/blob/main/Mrp.Logger/Mrp.Logger/ConsoleLogger/Exceptions/StringTooLongException.cs) if the log message is longer than the configured value (default is 1000) characters. The console logger set the color of the text depending on the message level:
* debug - gray
* info - green
* error - red

The file logger rotates the files by size. If a logfile reaches the size of the configured value (default is 5k) it can be archived and the logging can be continued with the original filename.
E.g.: original log name is: log.txt. The first rotation can create log.1.txt, the second rotation creates the log.2.txt file.

Every logger uses the same log formatting as defult: #{LogTime} [#{LogLevel}] #{LogMessage}, but this value also can be configured.


## Examples

### Usings
```csharp
using Mrp.Logger;
using Mrp.Logger.Enums;
using Mrp.Logger.Interfaces;
```

### Sample configuration
###### FileLogger with Default settings - [BaseConfigure.cs](https://github.com/miklosa/Mrp.Logger/blob/main/Mrp.Logger/Mrp.Logger/BaseLogger/Configure/BaseConfigure.cs)
```csharp
ILogger logger = new Logger(config =>
{
  config.UseFileLogger();
});
```
###### FileLogger with archive file size 500MB - [FileLoggerConfigure.cs](https://github.com/miklosa/Mrp.Logger/blob/main/Mrp.Logger/Mrp.Logger/FileLogger/Configure/FileLoggerConfigure.cs)
```csharp
ILogger logger = new Logger(config =>
{
  config.UseFileLogger(fileConfig =>
  {
    fileConfig.ArchiveFileSize = 500 * 1024 * 1024; // 500 MB
  });
});
```
###### FileLogger synchronized logger - [BaseLoggerConfigure.cs](https://github.com/miklosa/Mrp.Logger/blob/main/Mrp.Logger/Mrp.Logger/BaseLogger/Configure/BaseLoggerConfigure.cs)
```csharp
ILogger logger = new Logger(config =>
{
  config.UseFileLogger(fileConfig =>
  {
    fileConfig.BaseConfigurations(baseConfig =>
    {
      baseConfig.LoggerSyncType = LoggerSyncType.Sync;
    });
  });
});
```

###### ConsoleLogger with Default settings [ConsoleLoggerConfigure.cs](https://github.com/miklosa/Mrp.Logger/blob/main/Mrp.Logger/Mrp.Logger/ConsoleLogger/Configure/ConsoleLoggerConfigure.cs)
```csharp
ILogger logger = new Logger(config =>
{
  config.UseConsoleLogger();
});
```
###### RestApiLogger with Default settings - [RestApiLoggerConfigure.cs](https://github.com/miklosa/Mrp.Logger/blob/main/Mrp.Logger/Mrp.Logger/RestApiLogger/Configure/RestApiLoggerConfigure.cs)
```csharp
ILogger logger = new Logger(config =>
{
  config.UseRestApiLogger();
});
```

### Useage
```csharp
logger.Info("Info message");
logger.Error("Error message");
logger.Debug("Debug message");
```


## Test results of FileLogger
During the 2 instances of FileLogger were using. The test application is attached to source, [Program.cs](https://github.com/miklosa/Mrp.Logger/blob/main/Mrp.Logger/Test/TestFileLogger/Program.cs)
```csharp
ILogger logger = new Logger(config =>
{
  config.UseFileLogger(fileConfig =>
  {
    fileConfig.ArchiveFileSize = 500 * 1024 * 1024; // 500 MB
    fileConfig.LogDirectoryPathPattern = "{0}\\Logs1";
  });
});

ILogger logger2 = new Logger(config =>
{
  config.UseFileLogger(fileConfig =>
  {
    fileConfig.ArchiveFileSize = 500 * 1024 * 1024; // 500 MB
    fileConfig.LogDirectoryPathPattern = "{0}\\Logs2";
  });
});
```
##### Test Summary
I hope you will be able to reproduce my results
* The test was running time 2 minutes 15-20 seconds.
* The size of the generated log files was 19,7 GB
* Average writing speed was higher than 140 MB / sec
* Usage of RAM was less than 210 MB

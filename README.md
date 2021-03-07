# Mrp.Logger

Mrp.Logger is a library which can be used in other projects to log different messages. The type of the logger can be easily changed in every project or multiple logger can be add in each project.


There are three different message levels: debug, info, error


There are three types of the loggers:
 * console logger: logs to the console
 * file logger: logs to a file
 * rest api logger: logs to a remote rest api endpoint


The console logger throws an appropriate exception if the log message is longer than the configured value (default is 1000) characters. The console logger set the color of the text depending on the message level:
* debug - gray
* info - green
* error - red

The file logger rotates the files by size. If a logfile reaches the size of the configured value (default is 5k) it can be archived and the logging can be continued with the original filename.
E.g.: original log name is: log.txt. The first rotation can create log.1.txt, the second rotation creates the log.2.txt file.

Every logger uses the same log formatting as defult: #{LogTime} [#{LogLevel}] #{LogMessage}, but this value also can be configured.

using System;

namespace Mrp.Logger.ConsoleLogger.Exceptions
{
	[Serializable]
	public class StringTooLongException : Exception
	{
		const string DEFAULT_MESSAGE = "String has too many characters. ";

		const string PATTERN_MESSAGE_WITH_MAX_LENGTH = DEFAULT_MESSAGE + "Max length of the string must be less than {0}. ";

		const string PATTERN_MESSAGE_WITH_MAX_AND_CURRENT_LENGTH = PATTERN_MESSAGE_WITH_MAX_LENGTH + "Current length of the string is {1}. ";

		public StringTooLongException()
			: base(DEFAULT_MESSAGE) { }

		public StringTooLongException(string message)
			: base(message) { }

		public StringTooLongException(int length)
			: base(string.Format(PATTERN_MESSAGE_WITH_MAX_LENGTH, length)) { }

		public StringTooLongException(int length, int currentLenght)
			: base(string.Format(PATTERN_MESSAGE_WITH_MAX_AND_CURRENT_LENGTH, length, currentLenght)) { }

		public StringTooLongException(string message, Exception inner)
			: base(message, inner) { }
	}
}

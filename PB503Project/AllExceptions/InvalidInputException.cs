using System;
namespace PB503Project.AllExceptions
{
	public class InvalidInputException : Exception
	{
		public InvalidInputException() { }

		public InvalidInputException(string message) : base(message) { }
	}
}


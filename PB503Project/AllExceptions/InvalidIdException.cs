using System;
namespace PB503Project.AllExceptions
{
	public class InvalidIdException : Exception
	{
		public InvalidIdException() { }

		public InvalidIdException(string message) : base(message) { }
		
	}
}


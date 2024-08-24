namespace Operators.Server.Entities.CustomException
{
	/// <summary>
	/// Exception for send to client error text
	/// </summary>
	public class BusinessLogicException : Exception
	{
		public BusinessLogicException(string message) : base(message)
		{

		}

		public BusinessLogicException(string message, Exception inner) : base(message, inner)
		{

		}
	}
}

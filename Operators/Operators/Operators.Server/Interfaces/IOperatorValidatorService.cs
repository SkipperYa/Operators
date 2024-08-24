namespace Operators.Server.Interfaces
{
	public interface IOperatorValidatorService
	{
		void ValidateCode(long code);
		void ValidateName(string name);
	}
}

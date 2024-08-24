using Operators.Server.Entities.CustomException;
using Operators.Server.Interfaces;

namespace Operators.Server.Services
{
	public class OperatorValidatorService : IOperatorValidatorService
	{
		public void ValidateCode(long code)
		{
			if (code <= 0)
			{
				throw new BusinessLogicException("Invalid Operator Code");
			}
		}

		public void ValidateName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new BusinessLogicException("Invalid Operator Name");
			}
		}
	}
}

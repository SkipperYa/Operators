using Operators.Server.Entities.Operators;
using Operators.Server.Entities.ViewModels;

namespace Operators.Server.Interfaces
{
	public interface IOperatorsRepositoryService
	{
		/// <summary>
		/// Get operator by unique code
		/// </summary>
		/// <param name="code">Code operator</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns></returns>
		Task<Operator> GetOperator(long code, CancellationToken cancellationToken);

		/// <summary>
		/// Get operators
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List of Operators</returns>
		Task<List<Operator>> GetOperators(CancellationToken cancellationToken);

		/// <summary>
		/// Create Operator
		/// </summary>
		/// <param name="operatorViewModel">Info about of Operator</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Created Operator</returns>
		Task<Operator> CreateOperator(OperatorViewModelCreate operatorViewModel, CancellationToken cancellationToken);

		/// <summary>
		/// Update Operator by Code
		/// </summary>
		/// <param name="operatorViewModel">Info about of Operator</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Update Operator</returns>
		Task<Operator> UpdateOperator(OperatorViewModelUpdate operatorViewModel, CancellationToken cancellationToken);

		/// <summary>
		/// Delete Operator by Code
		/// </summary>
		/// <param name="code">unique Code of Operator</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>True if Operator has been deleted</returns>
		Task<bool> DeleteOperator(long code, CancellationToken cancellationToken);
	}
}

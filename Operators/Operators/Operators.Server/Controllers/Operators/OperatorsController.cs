using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Operators.Server.Entities.CustomException;
using Operators.Server.Entities.ViewModels;
using Operators.Server.Interfaces;

namespace Operators.Server.Controllers.Operators
{
	public class OperatorsController : BaseController
	{
		private readonly IOperatorsRepositoryService _operatorsRepositoryService;
		private readonly IOperatorValidatorService _operatorsValidatorService;

		public OperatorsController(IOperatorsRepositoryService operatorsRepositoryService, IOperatorValidatorService operatorsValidatorService)
		{
			_operatorsRepositoryService = operatorsRepositoryService;
			_operatorsValidatorService = operatorsValidatorService;
		}

		[HttpGet]
		public async Task<IActionResult> GetList(CancellationToken cancellationToken)
		{
			var list = await _operatorsRepositoryService.GetOperators(cancellationToken);

			return Ok(list);
		}

		[HttpGet("{code}")]
		public async Task<IActionResult> GetOperator(long code, CancellationToken cancellationToken)
		{
			var entity = await _operatorsRepositoryService.GetOperator(code, cancellationToken);

			if (entity is null)
			{
				throw new BusinessLogicException("Invalid Code");
			}

			return Ok(entity);
		}

		[HttpPost]
		public async Task<IActionResult> CreateOperator([FromBody] OperatorViewModelCreate operatorModel, CancellationToken cancellationToken)
		{
			_operatorsValidatorService.ValidateName(operatorModel.Name);

			try
			{
				var entity = await _operatorsRepositoryService.CreateOperator(operatorModel, cancellationToken);

				return Ok(entity);
			}
			catch (Exception e)
			{
				// Check on duplicate Name
				if (e.InnerException != null && e.InnerException is SqlException sqle && sqle.Number == 2601)
				{
					throw new BusinessLogicException("Operator name is already exist");
				}

				throw;
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateOperator([FromBody] OperatorViewModelUpdate operatorModel, CancellationToken cancellationToken)
		{
			_operatorsValidatorService.ValidateCode(operatorModel.Code);

			_operatorsValidatorService.ValidateName(operatorModel.Name);

			var entity = await _operatorsRepositoryService.UpdateOperator(operatorModel, cancellationToken);

			return Ok(entity);
		}

		[HttpDelete("{code}")]
		public async Task<IActionResult> DeleteOperator(long code, CancellationToken cancellationToken)
		{
			_operatorsValidatorService.ValidateCode(code);

			var result = await _operatorsRepositoryService.DeleteOperator(code, cancellationToken);

			if (!result)
			{
				throw new BusinessLogicException("Invalid Code");
			}

			return Ok(true);
		}
	}
}

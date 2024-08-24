using Microsoft.AspNetCore.Mvc;
using Operators.Server.Entities.Operators;

namespace Operators.Server.Controllers.Operators
{
	public class OperatorsController : BaseController
	{
		public OperatorsController()
		{

		}

		[HttpGet]
		public async Task<IActionResult> GetList()
		{
			return Ok();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetOperator(long id)
		{
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> CreateOperator([FromBody] Operator operatorModel)
		{
			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateOperator([FromBody] Operator operatorModel)
		{
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOperator(long id)
		{
			return Ok();
		}
	}
}

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Operators.Server.Controllers;
using System.Text;
using Operators.Server.Entities.CustomException;
using System.Net;

namespace Operators.Server.Entities.ControllerFilters
{
	public class GlobalExceptionFilter : Attribute, IExceptionFilter
	{
		protected readonly ILogger<BaseController> _logger;

		public GlobalExceptionFilter(ILogger<BaseController> logger)
		{
			_logger = logger;
		}

		public void OnException(ExceptionContext context)
		{
			string actionName = context.ActionDescriptor.DisplayName;

			var exception = context.Exception;

			var stringBuilder = new StringBuilder();

			stringBuilder.Append($"ActionName: {actionName} \n");

			stringBuilder.Append($"ExceptionStackTrace: {exception.StackTrace} \n");
			stringBuilder.Append($"ExceptionMessage: {exception.Message} \n");
			stringBuilder.Append('\n');

			if (exception.InnerException != null)
			{
				stringBuilder.Append($"ExceptionStackTrace: {exception.InnerException.StackTrace} \n");
				stringBuilder.Append($"ExceptionMessage: {exception.InnerException.Message} \n");
				stringBuilder.Append('\n');
			}

			var error = stringBuilder.ToString();

			// Send message to client if it BusinessLogicException
			if (exception is BusinessLogicException logicException)
			{
				_logger.LogError(error);

				context.Result = new JsonResult(logicException.Message)
				{
					StatusCode = (int)HttpStatusCode.BadRequest
				};
			}
			// If it is TaskCanceledException set message Service Unavailable.
			else if (exception is TaskCanceledException)
			{
				_logger.LogWarning(error);

				context.Result = new JsonResult("Service Unavailable.")
				{
					StatusCode = (int)HttpStatusCode.ServiceUnavailable
				};
			}
			// Otherwise set message Something went wrong
			else
			{
				_logger.LogCritical(error);

				context.Result = new JsonResult("Internal server error.")
				{
					StatusCode = (int)HttpStatusCode.InternalServerError
				};
			}
		}
	}
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Commons;

public class ErrorHandlerMiddleware
{
	readonly RequestDelegate _next;
	readonly ILogger<ErrorHandlerMiddleware> _logger;
	public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception error)
		{
			var response = context.Response;
			response.ContentType = "application/json";
			response.StatusCode = StatusCodes.Status500InternalServerError;
			var problemDetails = new ProblemDetails
			{
				Status = response.StatusCode,
				Title = error.Message,
			};
			_logger.LogError(error.ToString());
			var result = JsonSerializer.Serialize(problemDetails);
			await response.WriteAsync(result);
		}
	}

}


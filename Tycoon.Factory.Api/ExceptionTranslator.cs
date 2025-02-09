using Microsoft.AspNetCore.Mvc;
using Tycoon.Factory.Core.Scheduling;

namespace Tycoon.Factory.Api;
    public class ExceptionTranslator : IExceptionTranslator
    {
        public IActionResult TranslateException(Exception ex)
        {
            switch (ex)
            {
            case ArgumentException:
                return Result(StatusCodes.Status400BadRequest, ex.Message);

            case NotImplementedException:
                return Result(StatusCodes.Status501NotImplemented, ex.Message);

            case WorkerBusyException:
                return Result(StatusCodes.Status409Conflict, ex.Message);
            
            default:
                return Result(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    private static ActionResult Result(int statusCode, string reason) => new ContentResult
    {
        StatusCode = statusCode,
        Content = $"Status Code: {statusCode}; {reason}",
        ContentType = "text/plain",
    };
}

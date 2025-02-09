using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Tycoon.Factory.Api;

public static class ExceptionHandler
{
    public static Task HandleException(HttpContext httpContext)
    {
        // Try and retrieve the error from the ExceptionHandler middleware
        var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
        var translator = httpContext.RequestServices.GetService<IExceptionTranslator>()!;
        var ex = exceptionDetails?.Error;
        var result = translator.TranslateException(ex ?? new Exception());
        return result.ExecuteResultAsync(new ActionContext { HttpContext = httpContext });
    }
}

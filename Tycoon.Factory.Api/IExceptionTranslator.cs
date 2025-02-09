using Microsoft.AspNetCore.Mvc;

namespace Tycoon.Factory.Api;

public interface IExceptionTranslator
{
    IActionResult TranslateException(Exception ex);
}

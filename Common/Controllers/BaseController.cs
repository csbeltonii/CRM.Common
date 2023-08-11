using Microsoft.AspNetCore.Mvc;

namespace Common.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected new IActionResult Ok()
        {
            return base.Ok();
        }

        protected IActionResult CreatedAtAction<T>(string? actionName,
                                                    object? routeValue,
                                                    T? value)
        {
            return base.CreatedAtAction(actionName, routeValue, Package.Ok(value));

        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Package.Ok(result));
        }

        protected IActionResult Error(string message) => BadRequest(message);
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Common.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected new IActionResult Ok() => base.Ok(Package.Ok());

        protected IActionResult CreatedAtAction<T>(string actionName,
                                                   object routeValue,
                                                   T value) 
            => base.CreatedAtAction(actionName, routeValue, Package.Ok(value));

        protected IActionResult Ok<T>(T result) => base.Ok(Package.Ok(result));

        protected IActionResult Error(string message) => BadRequest(message);
    }
}

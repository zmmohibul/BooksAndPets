using API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]

public class BaseApiController : ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        return result.StatusCode switch
        {
            200 => Ok(result.Data),
            204 => NoContent(),
            401 => Unauthorized(new ApiErrorResponse() { StatusCode = result.StatusCode, ErrorMessage = result.Message }),
            403 => Forbid(result.Message),
            404 => NotFound(new ApiErrorResponse() { StatusCode = result.StatusCode, ErrorMessage = result.Message }),
            _ => BadRequest(new ApiErrorResponse() { StatusCode = result.StatusCode, ErrorMessage = result.Message })
        };
    }
}
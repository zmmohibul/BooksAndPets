using API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]

public class BaseApiController : ControllerBase
{
    public IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.StatusCode == 200)
        {
            return Ok(result.Data);
        }
        
        if (result.StatusCode == 204)
        {
            return NoContent();
        }
        
        if (result.StatusCode == 401)
        {
            return Unauthorized(new ApiErrorResponse()
            {
                StatusCode = result.StatusCode,
                ErrorMessage = result.Message
            });
        }

        if (result.StatusCode == 404)
        {
            return NotFound(new ApiErrorResponse()
            {
                StatusCode = result.StatusCode,
                ErrorMessage = result.Message
            });
        }

        return BadRequest(new ApiErrorResponse()
        {
            StatusCode = result.StatusCode,
            ErrorMessage = result.Message
        });
    }
}
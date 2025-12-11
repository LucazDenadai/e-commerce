using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new
        {
            status = "ok",
            service = "user-service"
        });
    }

    [HttpGet("profile")]
    public IActionResult Profile()
    {
        return Ok(new
        {
            id = "user-123",
            name = "John Doe",
            email = ""
        });
    }
}
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private static readonly Dictionary<string, User> Users = new();

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { error = "Email and password are required" });

        if (Users.ContainsKey(request.Email))
            return Conflict(new { error = "User already exists" });

        var user = new User
        {
            Id = Guid.NewGuid().ToString("N"),
            Email = request.Email,
            Password = request.Password,
            Name = request.Name
        };

        Users[user.Email] = user;

        return Created("", new
        {
            user.Id,
            user.Email,
            user.Name
        });
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        if (!Users.TryGetValue(request.Email, out var user))
            return Unauthorized();

        if (user.Password != request.Password)
            return Unauthorized();

        return Ok(new
        {
            token = "dev-token",
            user = new
            {
                user.Id,
                user.Email,
                user.Name
            }
        });
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserApi.entities;
using UserApi.Repositories;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private static readonly Dictionary<string, UserEntity> Users = new();
    private readonly IPasswordHasher<UserEntity> _passwordHasher;
    private readonly IConfiguration _config;
    private readonly IUserRepository _repo;

    public AuthController(IPasswordHasher<UserEntity> passwordHasher, IConfiguration config,
        IUserRepository repo)
    {
        _passwordHasher = passwordHasher;
        _config = config;
        _repo = repo;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { error = "Email and password are required" });

        if (Users.ContainsKey(request.Email))
            return Conflict(new { error = "User already exists" });

        var user = new UserEntity
        {
            Id = Guid.NewGuid().ToString("N"),
            Email = request.Email,
            Password = request.Password,
            Name = request.Name
        };

        user.Password = _passwordHasher.HashPassword(user, request.Password);
        Users[user.Email] = user;

        await _repo.AddAsync(user);
        await _repo.SaveChangesAsync();

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

        var verify = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
        if (verify == PasswordVerificationResult.Failed)
            return Unauthorized();

        var token = GenerateJwtToken(user);

        return Ok(new
        {
            token,
            user = new { user.Id, user.Email, user.Name }
        });
    }

    private string GenerateJwtToken(UserEntity user)
    {
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"] ?? "");
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Name, user.Name)
        };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiresMinutes"] ?? "1200")),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
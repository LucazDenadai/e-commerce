//tipo de referência imutável por padrão, criado para representar dados
public record RegisterRequest(string Email, string Password, string Name);
public record LoginRequest(string Email, string Password);

public class User
{
    public string Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Name { get; set; } = default!;
}
using System.Data;
using Npgsql;

public sealed class NpgsqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public NpgsqlConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não foi encontrada.");
    }

    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_connectionString);
}

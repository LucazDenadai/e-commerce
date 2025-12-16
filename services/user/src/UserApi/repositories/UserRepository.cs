using Dapper;

namespace UserApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task CreateAsync(User user)
        {
            const string sql = """
            INSERT INTO users (id, email, password_hash, name)
            VALUES (@Id, @Email, @Password, @Name);
        """;

            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {

                await connection.ExecuteAsync(sql, user, transaction);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            const string sql = """
            SELECT id, email, password_hash
            FROM users
            WHERE email = @Email
        """;

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
        }
    }
}
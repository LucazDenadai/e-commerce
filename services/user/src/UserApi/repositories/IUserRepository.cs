using UserApi.entities;

namespace UserApi.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}
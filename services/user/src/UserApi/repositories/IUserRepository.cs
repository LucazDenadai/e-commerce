using UserApi.entities;

namespace UserApi.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetByEmailAsync(string email);
        Task<UserEntity?> GetByIdAsync(string id);
        Task AddAsync(UserEntity user);
        Task SaveChangesAsync();
    }
}
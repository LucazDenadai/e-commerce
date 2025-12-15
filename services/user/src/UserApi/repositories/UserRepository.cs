using System.Collections.Concurrent;
using System.Linq;
using UserApi.entities;

namespace UserApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<string, UserEntity> _store = new();

        public Task AddAsync(UserEntity user)
        {
            _store[user.Id] = user;
            return Task.CompletedTask;
        }

        public Task<UserEntity?> GetByEmailAsync(string email)
        {
            var user = _store.Values.FirstOrDefault(u => u.Email == email);
            return Task.FromResult(user);
        }

        public Task<UserEntity?> GetByIdAsync(string id)
        {
            return Task.FromResult(_store.TryGetValue(id, out var user) ? user : null);
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
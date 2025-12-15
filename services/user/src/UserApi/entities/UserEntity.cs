namespace UserApi.entities
{
    public class UserEntity
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
using TopStyle.Domain.Entities;

namespace TopStyle.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> AddUserAsync(User user);
    }
}

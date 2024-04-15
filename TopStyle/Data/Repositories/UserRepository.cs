using Microsoft.EntityFrameworkCore;
using TopStyle.Data.Context;
using TopStyle.Data.Interfaces;
using TopStyle.Domain.Entities;

namespace TopStyle.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TopStyleDbContext _context;

        public UserRepository(TopStyleDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                                 .FirstOrDefaultAsync(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

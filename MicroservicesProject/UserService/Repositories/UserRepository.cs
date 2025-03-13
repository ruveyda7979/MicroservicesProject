using Microsoft.EntityFrameworkCore;
using Shared.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Models;

namespace UserService.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly UserDbContext _userContext;

        public UserRepository(UserDbContext context) : base(context)
        {
            _userContext = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<UserAddress>> GetUserAddressesAsync(int userId)
        {
            return await _userContext.UserAddresses
                .Where(ua => ua.UserId == userId)
                .ToListAsync();
        }
    }
}
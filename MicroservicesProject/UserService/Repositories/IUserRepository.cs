using Shared.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserAddress>> GetUserAddressesAsync(int userId);
    }
}
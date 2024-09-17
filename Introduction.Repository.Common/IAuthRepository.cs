using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Repository.Common
{
    public interface IAuthRepository
    {
        Task<bool> RegisterUser(User user);
        Task<User> GetUserByUsernameAndPassword(string username, string password);
        Task<bool> UpdateUser(Guid id, User user);
    }
}

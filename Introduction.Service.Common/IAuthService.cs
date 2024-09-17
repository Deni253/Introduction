using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service.Common
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(User user);
        Task<User> LoginUser(Login login);
        Task<bool> UpdateUser(Guid id, User user);
        Task<string> CreateToken(TokenRequest request);
    }
}

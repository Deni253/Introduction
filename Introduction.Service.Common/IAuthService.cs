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
        Task<string> LoginUser(string username,string password);
        Task<bool> UpdateUser(Guid id, User user);
        string CreateToken(User user);
    }
}

using Introduction.Service.Common;
using Introduction.Repository.Common;
using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        public AuthService(IAuthRepository repository) 
        { 
            _repository=repository;
        
        }
        public Task<bool> RegisterUser(User user)
        {
            return _repository.RegisterUser(user);
        }

        public Task<bool> LoginUser(Login login)
        {
            return _repository.LoginUser(login);
        }

        public Task<bool> UpdateUser(Guid id,User user)
        {
            return _repository.UpdateUser(id, user);
        }
    }
}

using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction
{
    public interface IAuthentactionService
    {
        public Task<UserResultDTO> LoginAsync(LoginDTO loginModel);
        public Task<UserResultDTO> RegisterAsync(UserRegisterDTO registerModel);
    }
}
